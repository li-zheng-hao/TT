using System.IO;
using Core;

namespace TTServer
{
    public class GetFriendHandler:IHandler
    {
        public void HandleMessage(MemoryStream msg, Session session)
        {
            var msgs=ProtobufHelper.FromStream(typeof(FriendListMessage), msg) as FriendListMessage;
            var conn = DBHelper.Instance.GetConnection();
            FriendDAO fDao = new FriendDAO();
            var friendList=fDao.GetFriendIdList(conn,msgs.OwnerId);
            AccountDAO accountDAO = new AccountDAO();
            FriendListMessage message = new FriendListMessage();
            foreach(var i in friendList)
            {
                var connT = DBHelper.Instance.GetConnection();
                var friend=accountDAO.GetAccountById(connT,i);
                Core.Friend friendObj = new Core.Friend()
                {
                    UserName = friend.UserName,
                    Phone = friend.Phone,
                    Email = friend.Email
                };
                message.Friends.Add(friendObj);
            }

            ResponseMessage(message, session);

        }

        public void ResponseMessage(IMessage msg, Session session)
        {
            session.Send(OperationCode.GetFriendList,msg);
        }
    }
}
