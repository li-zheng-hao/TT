using System;
using System.Net;

namespace Core
{
    public enum NetworkProtocol
    {
        KCP,
        TCP,
        WebSocket,
    }
    /// <summary>
    /// 服务基类
    /// </summary>
    public abstract class AService
    {
        public abstract AChannel GetChannel(long id);

        public bool IsDisposed { get; set; }

        private Action<AChannel> acceptCallback;

        public event Action<AChannel> AcceptCallback
        {
            add => this.acceptCallback += value;
            remove
            {
                if (acceptCallback != null) acceptCallback -= value;
            }
        }

        protected void OnAccept(AChannel channel)
        {
            this.acceptCallback.Invoke(channel);
        }

        public abstract AChannel ConnectChannel(IPEndPoint ipEndPoint);

        public abstract AChannel ConnectChannel(string address);

        public abstract void Remove(long channelId);

    }
}