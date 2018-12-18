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
                Email = "1263212577@qq.com",
                Password = "123456"
            };

            while (true)
            {
                client.session.Send(OperationCode.Login, ms);
                Thread.Sleep(5000);
            }
        }
     
        private void OnRead(ushort opCode, MemoryStream messageStream, Session session)
        {
            var ms=ProtobufHelper.FromStream(typeof(LoginMessage), messageStream) as LoginMessage;

            if (ms.Message=="Y")
            {
                Console.WriteLine($"登录成功，当前用户名字为{ms.Username}");
            }
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
