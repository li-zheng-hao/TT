using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{

    public static class ProtobufHelper
    {
        /// <summary>
        /// 将对象序列化成字节数组
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static byte[] ToBytes(object message)
        {
            using (MemoryStream ms = new MemoryStream())
            {

                ProtoBuf.Serializer.Serialize(ms, message);

                return ms.ToArray();
            }

        }

        public static void ToStream<T>(T message, MemoryStream stream)
        {
            ProtoBuf.Serializer.Serialize<T>(stream, message);
        }

        public static object FromStream(Type type, MemoryStream stream)
        {
            var message = ProtoBuf.Serializer.Deserialize(type, stream);
            return message;
        }
    }
}
