using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace xk_System.Model.Modules
{
    public class ChatItemData
    {
        public int ChannelId;
        public ulong playrId;
        public string name;
        public string content;
        public ulong time;
    }

    public class ChatModel : DataModel
    {
        public List<ChatItemData> mChatDataList = new List<ChatItemData>();
        /// <summary>
        /// 聊天频道ID
        /// </summary>
        public const int channel_Id_System = 1;
        public const int channel_Id_World = 2;
        public const int channel_Id_Guild = 3;
        public const int channel_Id_Team = 4;
        public const int channel_Id_Private = 5;
        public const int channel_Id_Nearby = 6;

        public override void initModel()
        {
            base.initModel();
            
        }

        public override void destroyModel()
        {
            base.destroyModel();
        }

        private void GetServerData(string mdata)
        {
            ReceiveData(GetCLientData(mdata));
        }

        private ChatItemData GetCLientData(string mdata)
        {
            ChatItemData mClientData = new ChatItemData();
            return mClientData;
        }

        public void ReceiveData(ChatItemData mdata)
        {
            mChatDataList.Insert(0,mdata);
            updateBind("mChatDataList");
        }
    }

   
}