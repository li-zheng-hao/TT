using ProtoBuf;

namespace Core
{
    [ProtoContract]
    public class LoginMessage : IMessage
    {
        [ProtoMember(1)] public int Id { get; set; }
        [ProtoMember(2)] public string Email { get; set; }
        [ProtoMember(3)] public string Password { get; set; }
        [ProtoMember(4)] public string Phone { get; set; }
        [ProtoMember(5)] public string Username { get; set; }
        [ProtoMember(6)] public string Message { get; set; }
    }
}
