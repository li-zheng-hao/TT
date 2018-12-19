using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Microsoft.IO;

namespace Core
{
    public sealed class TService : AService
    {
        #region 属性
   
        /// <summary>
        /// 所有用户
        /// </summary>
        private readonly Dictionary<long, TChannel> idChannels = new Dictionary<long, TChannel>();

        private readonly SocketAsyncEventArgs innArgs = new SocketAsyncEventArgs();

        private Socket acceptor;

        public RecyclableMemoryStreamManager MemoryStreamManager = new RecyclableMemoryStreamManager();

        public int PacketSizeLength { get; }
        #endregion

        #region 构造函数

        /// <summary>
        /// 即可做client也可做server
        /// </summary>
        public TService(int packetSizeLength, IPEndPoint ipEndPoint, Action<AChannel> acceptCallback)
        {
            this.PacketSizeLength = packetSizeLength;
            this.AcceptCallback += acceptCallback;

            this.acceptor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.acceptor.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            this.innArgs.Completed += this.OnComplete;

            this.acceptor.Bind(ipEndPoint);
            this.acceptor.Listen(1000);

            this.AcceptAsync();
            Console.WriteLine("服务器已经开启监听....");
        }

        public TService(int packetSizeLength)
        {
            this.PacketSizeLength = packetSizeLength;
        }
        #endregion

        #region 资源销毁

        

        /// <summary>
        /// 资源销毁
        /// </summary>
        public void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            foreach (long id in this.idChannels.Keys.ToArray())
            {
                TChannel channel = this.idChannels[id];
                channel.Dispose();
            }
            this.acceptor?.Close();
            this.acceptor = null;
            this.innArgs.Dispose();
        }
        #endregion

        #region 接收连接

        private void OnComplete(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Accept:
                    OnAcceptComplete(e);
                    break;
                default:
                    throw new Exception($"socket accept error: {e.LastOperation}");
            }
        }

        public void AcceptAsync()
        {
            this.innArgs.AcceptSocket = null;
            if (this.acceptor.AcceptAsync(this.innArgs))
            {
                return;
            }
            OnAcceptComplete(this.innArgs);
        }

        private void OnAcceptComplete(object o)
        {
            if (this.acceptor == null)
            {
                return;
            }
            SocketAsyncEventArgs e = (SocketAsyncEventArgs)o;

            if (e.SocketError != SocketError.Success)
            {
                Console.WriteLine($"accept error {e.SocketError}");
                return;
            }
            TChannel channel = new TChannel(e.AcceptSocket, this);
            this.idChannels[channel.Id] = channel;

            try
            {
                this.OnAccept(channel);
                
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            if (this.acceptor == null)
            {
                return;
            }

            this.AcceptAsync();
        }

        #endregion

        #region channel连接以及删除

        public override AChannel GetChannel(long id)
        {
            TChannel channel = null;
            this.idChannels.TryGetValue(id, out channel);
            return channel;
        }

        public override AChannel ConnectChannel(IPEndPoint ipEndPoint)
        {
            TChannel channel = new TChannel(ipEndPoint, this);
            this.idChannels[channel.Id] = channel;

            return channel;
        }

        public override AChannel ConnectChannel(string address)
        {
            IPEndPoint ipEndPoint = NetworkHelper.ToIPEndPoint(address);
            return this.ConnectChannel(ipEndPoint);
        }

        public override void Remove(long id)
        {
            TChannel channel;
            if (!this.idChannels.TryGetValue(id, out channel))
            {
                return;
            }
            if (channel == null)
            {
                return;
            }
            this.idChannels.Remove(id);
            channel.Dispose();
        }
        #endregion


    }
}