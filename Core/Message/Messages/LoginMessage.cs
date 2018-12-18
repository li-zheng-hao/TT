using ProtoBuf;

namespace Core
{
    [ProtoContract]
    public class LoginMessage:IMessage
    {
        [ProtoMember(1)] public string Email;
        [ProtoMember(2)] public string Password;
        [ProtoMember(3)] public string Phone;
        [ProtoMember(4)] public string Username;
        [ProtoMember(5)] public string Message;
    }
}
