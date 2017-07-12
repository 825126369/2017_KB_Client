using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using xk_System.Debug;
using xk_System.Model.Module;

namespace xk_System.View.Modules
{
    public class LoginView : xk_WindowView
    {
        public GameObject mLoginViewObj;
        public GameObject mRegisterViewObj;

        public InputField mAccount;
        public InputField mPassword;

        public InputField mRegisterAccount;
        public InputField mRegisterPassword;
        public InputField mRepeatPassword;

        public Button mLoginBtn;
        public Button mShowRegtisterViewBtn;

        public Button mReturnLoginBtn;
        public Button mRegisterBtn;

        private LoginModel mLoginModel = null;

        protected override void Awake()
        {
            base.Awake();
            mLoginModel = GetModel<LoginModel>();

            mLoginBtn.onClick.AddListener(OnClick_Login);
            mShowRegtisterViewBtn.onClick.AddListener(OnClick_ShowRegisterView);
            mRegisterBtn.onClick.AddListener(OnClick_Register);
            mReturnLoginBtn.onClick.AddListener(OnClick_ReturnLogin);

            mAccount.text = PlayerPrefs.GetString(CacheManager.cache_key_account, "");
            mPassword.text = PlayerPrefs.GetString(CacheManager.cache_key_password, "");

            mLoginViewObj.SetActive(true);
            mRegisterViewObj.SetActive(false);


            mLoginModel.addDataBind(JudegeOrLoginSuccess, "orLoginSuccess");
            mLoginModel.addDataBind(JudgeOrRegisterSuccess, "orRegisterSuccess");
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            mLoginModel.addDataBind(JudegeOrLoginSuccess, "orLoginSuccess");
            mLoginModel.addDataBind(JudgeOrRegisterSuccess, "orRegisterSuccess");
        }


        private void OnClick_Login()
        {
            if (string.IsNullOrEmpty(mAccount.text))
            {
                DebugSystem.LogError("账号不能为空");
                return;
            }
            if (string.IsNullOrEmpty(mPassword.text))
            {
                DebugSystem.LogError("密码不能为空");
                return;
            }
            DebugSystem.Log("点击登陆");
            mLoginModel.request_login(mAccount.text.Trim(), mPassword.text.Trim());
        }

        private void OnClick_ShowRegisterView()
        {
            mLoginViewObj.SetActive(false);
            mRegisterViewObj.SetActive(true);
        }


        private void OnClick_Register()
        {
            if (string.IsNullOrEmpty(mRegisterAccount.text.Trim()))
            {
                DebugSystem.LogError("注冊账号不能为空");
                return;
            }
            if (string.IsNullOrEmpty(mRegisterPassword.text.Trim()))
            {
                DebugSystem.LogError("注冊密码不能为空");
                return;
            }
            if (mRepeatPassword.text.Trim() != mRegisterPassword.text.Trim())
            {
                DebugSystem.LogError("Register Password no Equal");
                return;
            }
            DebugSystem.Log("Click RegisterBtn");
            mLoginModel.requeset_createAccount(mRegisterAccount.text, mRegisterPassword.text);
        }

        private void OnClick_ReturnLogin()
        {
            mLoginViewObj.SetActive(true);
            mRegisterViewObj.SetActive(false);
        }

        public void JudgeOrRegisterSuccess(object result)
        {
            bool orSuccess = (bool)result;
            if (orSuccess)
            {
                DebugSystem.LogError("Register Success");
                mAccount.text = mRegisterAccount.text;
                mPassword.text = mRegisterPassword.text;
                mLoginViewObj.SetActive(true);
                mRegisterViewObj.SetActive(false);
            }
            else
            {
                
            }
        }

        public void JudegeOrLoginSuccess(object result)
        {
            bool orSuccess = (bool)result;
            if (orSuccess)
            {
                ShowView<SelectServerView>();
                HideView<LoginView>();

                PlayerPrefs.SetString(CacheManager.cache_key_account,mAccount.text);
                PlayerPrefs.SetString(CacheManager.cache_key_password,mPassword.text);
            }
            else
            {
               
            }
        }
    }
}