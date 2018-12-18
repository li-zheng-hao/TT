using System;
using System.IO;
using System.Net;

namespace Core
{
    /// <summary>
    /// 信道类型，用于连接或者接收
    /// </summary>
    public enum ChannelType
    {
        Connect,
        Accept,
    }

    public abstract class AChannel
    {

        #region 属性
        public long Id { get;  }
        public bool IsDisposed { get; set; }
        public ChannelType ChannelType { get; }

        public AService Service { get; }

        public abstract MemoryStream Stream { get; }

        public int Error { get; set; }

        public IPEndPoint RemoteAddress { get; protected set; }

        private Action<AChannel, int> errorCallback;

        public event Action<AChannel, int> ErrorCallback
        {
            add
            {
                this.errorCallback += value;
            }
            remove
            {
                this.errorCallback -= value;
            }
        }

        private Action<MemoryStream> readCallback;

        public event Action<MemoryStream> ReadCallback
        {
            add
            {
                readCallback += value;
            }
            remove
            {
                readCallback -= value;
            }
        }

        #endregion

        #region 方法

        

        protected void OnRead(MemoryStream memoryStream)
        {
            this.readCallback.Invoke(memoryStream);
        }

        protected void OnError(int e)
        {
            Console.WriteLine($"报错了,错误号为{e}");
            this.Error = e;
            this.errorCallback?.Invoke(this, e);
        }

        protected AChannel(AService service, ChannelType channelType)
        {
            this.Id = IdGenerater.GenerateId();
            this.ChannelType = channelType;
            this.Service = service;
        }

        public abstract void Start();

        public abstract void Send(MemoryStream stream);

        public void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            this.Service.Remove(this.Id);

        }
        #endregion

    }
}