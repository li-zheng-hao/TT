using System;
using System.IO;
using System.Threading;

namespace Core
{
    public class ClientTestDemo
    {
        public TService service;
        private Session session;
        static void Main(string[] args)
        {
            var client = new ClientTestDemo();
            LoginMessage ms = new LoginMessage()
            {
                UserName = "lizhenghao",
                Password = "123"
            };

            while (true)
            {
                client.session.Send(OperationCode.Login, ms);
                Thread.Sleep(5000);
            }
        }
     
        private void OnRead(ushort opCode, MemoryStream messageStream, Session session)
        {
            var rm=ProtobufHelper.FromStream(typeof(ResponseMessage), messageStream) as ResponseMessage;
            Console.WriteLine($"客户端收到服务端发来的消息，操作码为{opCode},消息内容为{rm.Message}");
        }

        public ClientTestDemo()
        {
            service=new TService(packetSizeLength:2);
            var channel=service.ConnectChannel("127.0.0.1:55555");
            session = new Session();
            session.Awake(channel);
            session.Start();
            session.OnReadCallBack += OnRead;

        }


    }

}
