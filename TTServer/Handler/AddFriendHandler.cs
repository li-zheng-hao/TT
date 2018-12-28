using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace TTServer
{
    public class AddFriendHandler : IHandler
    {
        public void HandleMessage(MemoryStream msg, Session session)
        {
            var addFriend=ProtobufHelper.FromStream(typeof(AddFriendMessage), msg) as AddFriendMessage;
            AccountDAO accountDAO = new AccountDAO();
            var id=accountDAO.GetAccountIdByEmail(DBHelper.Instance.GetConnection(), addFriend.FriendEmail);
            FriendDAO friendDAO = new FriendDAO();
            ResponseMessage responseMessage = new ResponseMessage();
            if (id != -1)
            {
                var result = friendDAO.InsertFriend(DBHelper.Instance.GetConnection(), addFriend.OwnerId, id);
                if (result)
                {
                    responseMessage.Message = "Y";
                }
                else
                {
                    responseMessage.Message = "N";
                }
            }
            else
            {
                responseMessage.Message = "N";
            }
            ResponseMessage(responseMessage, session);

        }

        public void ResponseMessage(IMessage msg, Session session)
        {
            session.Send(OperationCode.AddFriend, msg);
        }
    }
}
