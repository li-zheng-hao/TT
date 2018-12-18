using System.IO;
using Core;

namespace TTServer
{
    public interface IHandler
    {
        void HandleMessage(MemoryStream msg, Session session);

        void ResponseMessage(IMessage msg,Session session);

    }
}
