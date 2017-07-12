using UnityEngine;
using System.Collections;
using xk_System.Model;
using System.Collections.Generic;

public class SelectRoleModel : DataModel
{

    public ulong LastSelectRoleId;
    public override void initModel()
    {
        base.initModel();
      
       // LastSelectRoleId = mdata.LastSelectRoleId;
    }

    public override void destroyModel()
    {
        base.destroyModel();
        
    }

  /*  private void Receive_CreateRoleListInfo(scCreateRole mdata)
    {
        if (mdata.Result == 1)
        {
            if (mdata.Role != null)
            {
                mHaveRoleList.Add(mdata.Role);
                updateBind("mHaveRoleList");
            }
        }
    }*/
}
