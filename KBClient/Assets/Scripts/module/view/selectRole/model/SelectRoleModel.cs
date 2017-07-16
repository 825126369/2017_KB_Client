using UnityEngine;
using System.Collections;
using xk_System.Model;
using System.Collections.Generic;
using xk_System.Debug;
using System;

public class SelectRoleModel : DataModel
{

    public Dictionary<UInt64, Dictionary<string, object>> ui_avatarList = null;
    public ulong LastSelectRoleId;
    public override void initModel()
    {
        base.initModel();
        // select-avatars(register by scripts)
        KBEngine.Event.registerOut("onReqAvatarList", this, "onReqAvatarList");
        KBEngine.Event.registerOut("onCreateAvatarResult", this, "onCreateAvatarResult");
        KBEngine.Event.registerOut("onRemoveAvatar", this, "onRemoveAvatar");
    }

    public override void destroyModel()
    {
        base.destroyModel();
        KBEngine.Event.deregisterOut(this);
    }

    public void request_creteRole(string playerName)
    {
        KBEngine.Event.fireIn("reqCreateAvatar", (Byte)1, playerName);
    }

    public void onReqAvatarList(Dictionary<UInt64, Dictionary<string, object>> avatarList)
    {
        ui_avatarList = avatarList;
        updateBind("ui_avatarList");
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
