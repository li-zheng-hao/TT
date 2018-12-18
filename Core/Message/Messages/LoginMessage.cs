using ProtoBuf;

namespace Core
{
    [ProtoContract]
    public class LoginMessage:IMessage
    {
        [ProtoMember(1)] public string UserName;
        [ProtoMember(2)] public string Password;
          
    }
}
