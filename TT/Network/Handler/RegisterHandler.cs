using System.IO;
using Core;

namespace TT
{
    public class RegisterHandler:IHandler
    {
        public void HandlerMessage(MemoryStream memoryStreamMsg, Session session)
        {
            var regist = ProtobufHelper.FromStream(typeof(ResponseMessage), memoryStreamMsg) as ResponseMessage;
            NetworkManager.GetInstance.RegisterCallBack?.Invoke(regist);

        }
    }
}
