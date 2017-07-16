using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using xk_System.Debug;
using xk_System.Model;
using System.Collections.Generic;
using System;

namespace xk_System.View.Modules
{
    public class RoleSelectView : xk_WindowView
    {
        public Button mEnterGameBtn;
        public Button mGoCreateRoleBtn;

        public Transform mHero2DParent;
        public SelectHeroItem mHero2DItemPrefab;

        private SelectRoleModel mSelectRoleModel = null;
        protected override void Awake()
        {
            base.Awake();
            mSelectRoleModel = GetModel<SelectRoleModel>();
            mEnterGameBtn.onClick.AddListener(Click_EnterGameBtn);
            mGoCreateRoleBtn.onClick.AddListener(Click_CreateRoleBtn);

            mSelectRoleModel.addDataBind(RefreshView, " ui_avatarList");
            RefreshView();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            mSelectRoleModel.removeDataBind(RefreshView, " ui_avatarList");
        }

        private void Click_EnterGameBtn()
        {
            if (mSelectRoleModel.LastSelectRoleId > 0)
            {
                KBEngine.Event.fireIn("selectAvatarGame", mSelectRoleModel.LastSelectRoleId);
            }
            else
            {
                DebugSystem.LogError("role id is zero");
            }
        }

        private void Click_CreateRoleBtn()
        {
            HideView<RoleSelectView>();
            ShowView<RoleCreateView>();
        }

        public void GetSelectRoleResult(ulong playerId)
        {
            mSelectRoleModel.LastSelectRoleId = playerId;
        }

        private void RefreshView(object data=null)
        {
            Dictionary<UInt64, Dictionary<string, object>> mPlayerList = mSelectRoleModel.ui_avatarList;
            int i = 0;
            foreach(var v in mPlayerList)
            {
                ulong uuid = v.Key;
                if (i>=mHero2DParent.childCount)
                {
                    GameObject obj = Instantiate<GameObject>(mHero2DItemPrefab.gameObject);
                    obj.transform.SetParent(mHero2DParent);
                    obj.transform.localPosition = Vector3.zero;
                    obj.transform.localScale = Vector3.one;
                }
                SelectHeroItem mItem = mHero2DParent.GetChild(i).GetComponent<SelectHeroItem>();
                mItem.RefreshItem(this,uuid);
                mItem.gameObject.SetActive(true);
                i++;
            }

            for(int j=mPlayerList.Count;j<mHero2DParent.childCount;j++)
            {
                mHero2DParent.GetChild(j).gameObject.SetActive(false);
            }

            if(mSelectRoleModel.LastSelectRoleId==0 && mPlayerList.Count>0)
            {
               
            }
        }
    }
}