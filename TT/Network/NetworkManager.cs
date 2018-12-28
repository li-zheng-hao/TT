using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Core;

namespace TT
{
    public class NetworkManager
    {
        public static NetworkManager GetInstance { get; } = new NetworkManager();

        public TService service;
        private Session session;

        #region 回调方法

        public Action<IMessage> LoginCallBack;
        public Action<IMessage> RegisterCallBack;
        public Action<IMessage> GetFriendListCallBack;
        public Action<IMessage> AddFriendCallBack;
        #endregion

        private void OnRead(ushort opCode, MemoryStream messageStream, Session session)
        {
            switch (opCode)
            {
                case OperationCode.Login:
                    new LoginHandler().HandlerMessage(messageStream, session);
                    break;
                case OperationCode.Register:
                    new RegisterHandler().HandlerMessage(messageStream,session);
                    break;
                case OperationCode.GetFriendList:
                    new GetFriendListHandler().HandlerMessage(messageStream, session);
                    break;
                case OperationCode.AddFriend:
                    new AddFriendHandler().HandlerMessage(messageStream, session);
                    break;
                default:
                    MessageBox.Show($"无法处理消息{opCode}");
                    break;
            }
            
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        private NetworkManager()
        {
            service = new TService(packetSizeLength: 2);
            var channel = service.ConnectChannel("127.0.0.1:55555");
            session = new Session();
            session.Awake(channel);
            session.Start();
            session.OnReadCallBack += OnRead;

        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="opcode"></param>
        /// <param name="message"></param>
        public void SendMessage(ushort opcode,IMessage message)
        {
            this.session.Send(opcode,message);
        }
    }
}
