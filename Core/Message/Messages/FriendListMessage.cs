using System.Collections.Generic;
using ProtoBuf;
namespace Core
{
    [ProtoContract]
    public class Friend
    {
        [ProtoMember(1)]
        public string UserName;
        [ProtoMember(2)]
        public string Phone;
        [ProtoMember(3)]
        public string Email;
    }

    [ProtoContract]
    public class FriendListMessage:IMessage
    {
        [ProtoMember(1)]
        public string OwnerId;
        [ProtoMember(2)]
        public List<Friend> Friends;
        
    }
}
