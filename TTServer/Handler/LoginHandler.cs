using System.IO;
using Core;

namespace TTServer
{
    public class LoginHandler:IHandler
    {
        public void HandleMessage(MemoryStream memoryStream, Session session)
        {
            var login = ProtobufHelper.FromStream(typeof(LoginMessage), memoryStream) as LoginMessage;
            //todo 这里验证用户是否存在,先假设存在
            ResponseMessage rm = new ResponseMessage()
            {
                Message = "Y",
                RpcId = OperationCode.Response
            };
            ResponseMessage(rm,session);
        }

        public void ResponseMessage(IMessage msg, Session session)
        {
            session.Send(OperationCode.Response,msg);
        }
    }
}
