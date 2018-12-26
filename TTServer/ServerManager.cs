using System;
using System.Collections.Generic;
using System.IO;
using Core;

namespace TTServer
{
    /// <summary>
    /// 服务器管理类，单例模式
    /// </summary>
    public class ServerManager
    {
        private static ServerManager Instance;

        public static ServerManager GetInstance
        {
            get
            {
                if (Instance==null)
                {
                    Instance=new ServerManager("127.0.0.1:55555",2);
                }

                return Instance;
            }
        }

        #region 属性

        private TService service;
        private Dictionary<int, Session> sessions;
        private List<Session> tempSessions;

        #endregion

        #region 构造函数

        public ServerManager(string ipaddress, int packagesize = 2)
        {
            service = new TService(packagesize, NetworkHelper.ToIPEndPoint(ipaddress), OnAcceptClient);
            sessions = new Dictionary<int, Session>();
            tempSessions = new List<Session>();
            
        }

        #endregion

        #region 方法
        /// <summary>
        /// 有用户连接进来了
        /// </summary>
        /// <param name="channel"></param>
        public void OnAcceptClient(AChannel channel)
        {
            Console.WriteLine("一个用户连接进来了");
            Session s = new Session();
            s.Awake(channel);
            s.Start();
            s.OnReadCallBack += OnRead;
        }

        private void OnRead(ushort opCode, MemoryStream message, Session session)
        {
            switch (opCode)
            {
                case OperationCode.Login:
                    new LoginHandler().HandleMessage(message, session);
                    break;
                case OperationCode.Register:
                    new RegisterHandler().HandleMessage(message,session);
                    break;
                case OperationCode.GetFriendList:
                    new GetFriendHandler().HandleMessage(message, session);
                    break;
                default:
                    Console.WriteLine($"没有办法处理操作码{opCode}");
                    break;
            }
        }

        #endregion

    }
}
