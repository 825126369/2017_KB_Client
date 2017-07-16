using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using xk_System.Debug;
using xk_System.Model;
using System.Collections.Generic;
using xk_System.View.Modules;

public class SelectHeroItem : MonoBehaviour
{
    public Text mName;
    public Text mGender;
    public Text mProfession;
    public Button mClickBtn;

    private RoleSelectView mView;
    private SelectRoleModel mModel;
    private ulong m_playerid;

    private void Awake()
    {
        mClickBtn.onClick.AddListener(() => mModel.LastSelectRoleId = m_playerid);
    }

    public void RefreshItem(RoleSelectView view,ulong playerId)
    {
        mView = view;
        mModel= ModelSystem.Instance.GetModel<SelectRoleModel>();
        DebugSystem.Log("role id: "+playerId);
        m_playerid = playerId;
        Dictionary<string, object> info = mModel.ui_avatarList[playerId];
        //	Byte roleType = (Byte)info["roleType"];
        string name = (string)info["name"];
        //	UInt16 level = (UInt16)info["level"];
        //UInt64 idbid = (UInt64)info["dbid"];
        mName.text="姓名："+ name;


        /*if (mHeroInfo.Gender == 1)
        {
            mGender.text = "性别：男";
        }else if(mHeroInfo.Gender ==2)
        {
            mGender.text = "性别：女";
        }

        if(mHeroInfo.Profession==1)
        {
            mProfession.text = "职业：战士";
        }
        else if(mHeroInfo.Profession==2)
        {
            mProfession.text = "职业：法师";
        }
        else if(mHeroInfo.Profession==3)
        {
            mProfession.text = "职业：道士";
        }*/
    }

}
