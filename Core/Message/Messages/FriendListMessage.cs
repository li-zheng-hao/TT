using System.Collections.Generic;
using ProtoBuf;
namespace Core
{
    [ProtoContract]
    public class Friend
    {
        [ProtoMember(1)]
        public string UserName { get; set; }
        [ProtoMember(2)]
        public string Phone { get; set; }
        [ProtoMember(3)]
        public string Email { get; set; }
    }

    [ProtoContract]
    public class FriendListMessage:IMessage
    {
        [ProtoMember(1)]
        public int OwnerId;
        [ProtoMember(2)]
        public List<Friend> Friends;

        public FriendListMessage()
        {
            Friends = new List<Friend>();
        }
        
    }
}
