using System.IO;
using Core;

namespace TTServer
{
    public class RegisterHandler:IHandler
    {
        public void HandleMessage(MemoryStream msg, Session session)
        {
            var registMsg = ProtobufHelper.FromStream(typeof(RegisterMessage), msg) as RegisterMessage;
            var conn = DBHelper.Instance.GetConnection();
            AccountDAO dao = new AccountDAO();

            Account account = new Account()
            {
                Email = registMsg.Email,
                Password = registMsg.Password,
                Phone = registMsg.Phone,
                UserName = registMsg.Username
            };
            var isResult=dao.ExistAccount(conn, account);
            ResponseMessage rm = new ResponseMessage();

            if (isResult==ErrorCode.ERR_EXISTEMAIL)
            {
                rm.Error = ErrorCode.ERR_EXISTEMAIL;
                rm.Message = "N";
            }else if (isResult==ErrorCode.ERR_EXISTPHONE)
            {
                rm.Error = ErrorCode.ERR_EXISTPHONE;
                rm.Message = "N";
            }
            else
            {
                int res=dao.InsertAccount(DBHelper.Instance.GetConnection(), account);
                if (res==0)
                {
                    rm.Message = "N";
                    rm.Error = ErrorCode.ERR_UNKNOWN;
                }
                else
                {
                    rm.Message = "Y";
                }
            }
            ResponseMessage(rm,session);
        }

        public void ResponseMessage(IMessage msg, Session session)
        {
            session.Send(OperationCode.Register,msg);
        }
    }
}
