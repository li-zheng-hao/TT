using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [ProtoContract]
    public class AddFriendMessage:IMessage
    {
        [ProtoMember(1)]
        public int OwnerId { get; set; }
        [ProtoMember(2)]
        public string FriendEmail { get; set; }
        [ProtoMember(3)]
        public string Phone { get; set; }
    }
}
