using UnityEngine;
using System.Collections;
using xk_System.Model;
using System.Collections.Generic;

public class PlayerDataManager:Singleton<PlayerDataManager>
{
    public Dictionary<ulong, PlayerInfo> mPlayerInfoDic = new Dictionary<ulong, PlayerInfo>();
    private ulong my_serverId;

    public PlayerInfo getMyInfo(ulong myId)
    {
        return getPlayerInfo(my_serverId);
    }

    public PlayerInfo getPlayerInfo(ulong myId)
    {
        return new PlayerInfo();
    }

}

public class PlayerInfo
{

}
