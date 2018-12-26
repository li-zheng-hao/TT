using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace TT
{
    public class GetFriendListHandler : IHandler
    {
        public void HandlerMessage(MemoryStream memoryStreamMsg, Session session)
        {
            var friend = ProtobufHelper.FromStream(typeof(FriendListMessage), memoryStreamMsg) as FriendListMessage;
            NetworkManager.GetInstance.GetFriendListCallBack?.Invoke(friend);
        }
    }
}
