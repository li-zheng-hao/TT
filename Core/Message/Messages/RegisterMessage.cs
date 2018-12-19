using ProtoBuf;
namespace Core
{
    [ProtoContract]
    public class RegisterMessage:IMessage
    {
        [ProtoMember(1)]
        public string Username;
        [ProtoMember(2)]
        public string Password;
        [ProtoMember(3)]
        public string Email;
        [ProtoMember(4)]
        public string Phone;
    }
}
