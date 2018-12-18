using ProtoBuf;

namespace Core
{
    [ProtoContract]
    public interface IMessage
    {

    }
    
    [ProtoContract]
    public class ResponseMessage : IMessage
    {
        [ProtoMember(1)]
        public int Error { get; set; }
        [ProtoMember(2)]
        public string Message { get; set; }
        [ProtoMember(3)]
        public int RpcId { get; set; }
    }

}
