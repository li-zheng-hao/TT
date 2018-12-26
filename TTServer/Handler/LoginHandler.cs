using System.IO;
using Core;

namespace TTServer
{
    public class LoginHandler:IHandler
    {
        public void HandleMessage(MemoryStream memoryStream, Session session)
        {
            var login = ProtobufHelper.FromStream(typeof(LoginMessage), memoryStream) as LoginMessage;
            var conn = DBHelper.Instance.GetConnection();
            AccountDAO dao = new AccountDAO();
            LoginMessage ms = new LoginMessage();
            if (login==null)
            {
                ms.Message = "N";
            }
            else
            {
                Account result = dao.VerifyAccount(conn, login.Email, login.Password, null);
                if (result==null)
                {
                    ms.Message = "N";
                }
                else
                {
                    ms.Message = "Y";
                    ms.Id = result.Id;
                    ms.Username = result.UserName;
                    ms.Password = result.Password;
                    ms.Email = result.Email;
                    ms.Phone = result.Phone;
                }
            }
            ResponseMessage(ms, session);
        }

        public void ResponseMessage(IMessage msg, Session session)
        {
            session.Send(OperationCode.Login,msg);
        }
    }
}
