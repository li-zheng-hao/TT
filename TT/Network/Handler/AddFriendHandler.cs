using System.IO;
using Core;

namespace TT
{
    public class AddFriendHandler : IHandler
    {
        public void HandlerMessage(MemoryStream memoryStreamMsg, Session session)
        {
            var response = ProtobufHelper.FromStream(typeof(ResponseMessage), memoryStreamMsg) as ResponseMessage;
            NetworkManager.GetInstance.AddFriendCallBack?.Invoke(response);
        }
    }
}
