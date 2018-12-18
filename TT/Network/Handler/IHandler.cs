using System.IO;
using Core;

namespace TT
{
    public interface IHandler
    {
        void HandlerMessage(MemoryStream memoryStreamMsg, Session session);
    }
}
