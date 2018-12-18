using System.IO;
using Core;

namespace TT
{
    public class LoginHandler:IHandler
    {
        public void HandlerMessage(MemoryStream memoryStream, Session session)
        {
            var login = ProtobufHelper.FromStream(typeof(LoginMessage), memoryStream) as LoginMessage;
            NetworkManager.GetInstance.LoginCallBack?.Invoke(login);
            
        }
    }
}
