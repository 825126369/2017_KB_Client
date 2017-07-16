using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using xk_System.Debug;
using System;
using xk_System.View.Modules;

namespace xk_System.Model.Module
{
    public class LoginModel :DataModel
    {
        private string mCreateAccount;
        public bool orLoginSuccess;
        public bool orRegisterSuccess;

        public Dictionary<UInt64, Dictionary<string, object>> ui_avatarList = null;
        public override void initModel()
        {
            base.initModel();
            installEvents();
        }

        public override void destroyModel()
        {
            base.destroyModel();
            KBEngine.Event.deregisterOut(this);
        }

        void installEvents()
        {
            // common
            KBEngine.Event.registerOut("onKicked", this, "onKicked");
            KBEngine.Event.registerOut("onDisconnected", this, "onDisconnected");
            KBEngine.Event.registerOut("onConnectionState", this, "onConnectionState");

            // login
            KBEngine.Event.registerOut("onCreateAccountResult", this, "onCreateAccountResult");
            KBEngine.Event.registerOut("onLoginFailed", this, "onLoginFailed");
            KBEngine.Event.registerOut("onVersionNotMatch", this, "onVersionNotMatch");
            KBEngine.Event.registerOut("onScriptVersionNotMatch", this, "onScriptVersionNotMatch");
            KBEngine.Event.registerOut("onLoginBaseappFailed", this, "onLoginBaseappFailed");
            KBEngine.Event.registerOut("onLoginSuccessfully", this, "onLoginSuccessfully");
            KBEngine.Event.registerOut("onReloginBaseappFailed", this, "onReloginBaseappFailed");
            KBEngine.Event.registerOut("onReloginBaseappSuccessfully", this, "onReloginBaseappSuccessfully");
            KBEngine.Event.registerOut("onLoginBaseapp", this, "onLoginBaseapp");
            KBEngine.Event.registerOut("Loginapp_importClientMessages", this, "Loginapp_importClientMessages");
            KBEngine.Event.registerOut("Baseapp_importClientMessages", this, "Baseapp_importClientMessages");
            KBEngine.Event.registerOut("Baseapp_importClientEntityDef", this, "Baseapp_importClientEntityDef");

            // select-avatars(register by scripts)
            KBEngine.Event.registerOut("onReqAvatarList", this, "onReqAvatarList");
            KBEngine.Event.registerOut("onCreateAvatarResult", this, "onCreateAvatarResult");
            KBEngine.Event.registerOut("onRemoveAvatar", this, "onRemoveAvatar");
        }

        public void request_login(string Account, string Password)
        {
            DebugSystem.Log("connect to server...(连接到服务端...)");
            KBEngine.Event.fireIn("login", Account, Password, System.Text.Encoding.UTF8.GetBytes("kbengine_unity3d_demo"));
        }

        public void requeset_createAccount(string Account, string Password)
        {
            DebugSystem.Log("connect to server...(连接到服务端...)");
            KBEngine.Event.fireIn("createAccount", Account, Password, System.Text.Encoding.UTF8.GetBytes("kbengine_unity3d_demo"));
            mCreateAccount = Account;
        }

        public void onCreateAccountResult(UInt16 retcode, byte[] datas)
        {
            if (retcode != 0)
            {
                DebugSystem.LogError("createAccount is error(注册账号错误)! DebugSystem.LogError=" + KBEngine.KBEngineApp.app.serverErr(retcode));
                orRegisterSuccess = false;
            }
            else
            {
                orRegisterSuccess = true;
                if (KBEngine.KBEngineApp.validEmail(mCreateAccount))
                {
                    DebugSystem.Log("createAccount is successfully, Please activate your Email!(注册账号成功，请激活Email!)");
                }
                else
                {
                    DebugSystem.Log("createAccount is successfully!(注册账号成功!)");
                }
            }

            updateBind("orRegisterSuccess");
        }

        public void onConnectionState(bool success)
        {
            if (!success)
                DebugSystem.LogError("connect(" + KBEngine.KBEngineApp.app.getInitArgs().ip + ":" + KBEngine.KBEngineApp.app.getInitArgs().port + ") is error! (连接错误)");
            else
                DebugSystem.Log("connect successfully, please wait...(连接成功，请等候...)");
        }

        public void onLoginFailed(UInt16 failedcode)
        {
            if (failedcode == 20)
            {
                DebugSystem.LogError("login is failed(登陆失败), DebugSystem.LogError=" + KBEngine.KBEngineApp.app.serverErr(failedcode) + ", " + System.Text.Encoding.ASCII.GetString(KBEngine.KBEngineApp.app.serverdatas()));
            }
            else
            {
                DebugSystem.LogError("login is failed(登陆失败), DebugSystem.LogError=" + KBEngine.KBEngineApp.app.serverErr(failedcode));
            }

            orLoginSuccess = false;
            updateBind("orLoginSuccess");
        }

        public void onVersionNotMatch(string verInfo, string serVerInfo)
        {
            DebugSystem.LogError("版本不匹配");
        }

        public void onScriptVersionNotMatch(string verInfo, string serVerInfo)
        {
            DebugSystem.LogError("脚本版本不匹配");
        }

        public void onLoginBaseappFailed(UInt16 failedcode)
        {
            DebugSystem.LogError("loginBaseapp is failed(登陆网关失败), DebugSystem.LogError=" + KBEngine.KBEngineApp.app.serverErr(failedcode));
        }

        public void onLoginBaseapp()
        {
            DebugSystem.Log("connect to loginBaseapp, please wait...(连接到网关， 请稍后...)");
        }

        public void onReloginBaseappFailed(UInt16 failedcode)
        {
            DebugSystem.LogError("relogin is failed(重连网关失败), DebugSystem.LogError=" + KBEngine.KBEngineApp.app.serverErr(failedcode));
        }

        public void onReloginBaseappSuccessfully()
        {
            DebugSystem.Log("relogin is successfully!(重连成功!)");
        }

        public void onLoginSuccessfully(UInt64 rndUUID, Int32 eid,KBEngine.Account accountEntity)
        {
            DebugSystem.Log("login is successfully!(登陆成功!)");
            orLoginSuccess = true;
            updateBind("orLoginSuccess");
        }

        public void onKicked(UInt16 failedcode)
        {
            DebugSystem.LogError("kick, disconnect!, reason=" + KBEngine.KBEngineApp.app.serverErr(failedcode));
        }

        public void Loginapp_importClientMessages()
        {
            DebugSystem.Log("Loginapp_importClientMessages ...");
        }

        public void Baseapp_importClientMessages()
        {
            DebugSystem.Log("Baseapp_importClientMessages ...");
        }

        public void Baseapp_importClientEntityDef()
        {
            DebugSystem.Log("importClientEntityDef ...");
        }

        public void onDisconnected()
        {
            DebugSystem.LogError("disconnect! will try to reconnect...(你已掉线，尝试重连中!)");
            //startRelogin = true;
            //Invoke("onReloginBaseappTimer", 1.0f);
        }

        public void onReloginBaseappTimer()
        {
           /* if (ui_state == 0)
            {
                DebugSystem.LogError("disconnect! (你已掉线!)");
                return;
            }

            KBEngineApp.app.reloginBaseapp();

            if (startRelogin)
                Invoke("onReloginBaseappTimer", 3.0f);*/
        }

        public void onReqAvatarList(Dictionary<UInt64, Dictionary<string, object>> avatarList)
        {
            ui_avatarList = avatarList;
            GetModel<SelectRoleModel>().ui_avatarList = ui_avatarList;
            KBEngine.Event.fireOut("EnterSelectRoleView");
        }

        public void onCreateAvatarResult(Byte retcode, object obj, Dictionary<UInt64, Dictionary<string, object>> avatarList)
        {
            if (retcode != 0)
            {
                DebugSystem.LogError("Error creating avatar, errcode=" + retcode);
                return;
            }

            onReqAvatarList(avatarList);
        }

        public void onRemoveAvatar(UInt64 dbid, Dictionary<UInt64, Dictionary<string, object>> avatarList)
        {
            if (dbid == 0)
            {
                DebugSystem.LogError("Delete the avatar error!(删除角色错误!)");
                return;
            }

            onReqAvatarList(avatarList);
        }
    }
}