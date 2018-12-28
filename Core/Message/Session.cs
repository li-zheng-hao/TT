using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Core
{
    public sealed class Session 
    {
        #region 属性
        
        /// <summary>
        /// 通信使用的channel
        /// </summary>
        private AChannel channel;

        private Action<ushort, MemoryStream, Session> onReadCallBack;
      
        /// <summary>
        /// 数据接收事件
        /// </summary>
        public event Action<ushort,MemoryStream,Session> OnReadCallBack
        {
            add { onReadCallBack += value; }
            remove { onReadCallBack -= value; }
        }

        /// <summary>
        /// 消息类型
        /// </summary>
        private readonly List<byte[]> byteses = new List<byte[]>() {new byte[2] };

        /// <summary>
        /// 资源销毁标志
        /// </summary>
        private bool IsDisposed;

        /// <summary>
        /// 错误类型
        /// </summary>
        public int Error
        {
            get
            {
                return this.channel.Error;
            }
            set
            {
                this.channel.Error = value;
            }
        }

        /// <summary>
        /// IP地址
        /// </summary>
        public IPEndPoint RemoteAddress
        {
            get
            {
                return this.channel.RemoteAddress;
            }
        }

        /// <summary>
        /// 信道信息
        /// </summary>
        public ChannelType ChannelType
        {
            get
            {
                return this.channel.ChannelType;
            }
        }

        /// <summary>
        /// 数据内存流
        /// </summary>
        public MemoryStream Stream
        {
            get
            {
                return this.channel.Stream;
            }
        }

        #endregion

        #region 初始化及连接



        /// <summary>
        /// 初始化Session，添加ReadCallBack回调
        /// </summary>
        /// <param name="aChannel"></param>
        public void Awake(AChannel aChannel)
        {
            this.channel = aChannel;
            channel.ReadCallback += this.OnRead;
        }

        /// <summary>
        /// 开始连接服务器
        /// </summary>
        public void Start()
        {
            this.channel.Start();
        }
        #endregion

        #region 数据读取

        

        /// <summary>
        /// 不用手动调用，已经注册，会自动调用
        /// </summary>
        /// <param name="memoryStream"></param>
        public void OnRead(MemoryStream memoryStream)
        {
            try
            {
                this.Run(memoryStream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="memoryStream"></param>
        private void Run(MemoryStream memoryStream)
        {
            memoryStream.Seek(Packet.MessageIndex, SeekOrigin.Begin);
            ushort opcode = BitConverter.ToUInt16(memoryStream.GetBuffer(), Packet.OpcodeIndex);
            try
            {
                memoryStream.Seek(2, SeekOrigin.Begin);
                onReadCallBack.Invoke(opcode,memoryStream,this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            
            
        }

        #endregion

        #region 数据发送
        /// <summary>
        /// 发送消息包
        /// </summary>
        /// <param name="opcode"></param>
        /// <param name="message"></param>
        public void Send(ushort opcode, object message)
        {
            if (this.IsDisposed)
            {
                throw new Exception("session已经被Dispose了");
            }

            MemoryStream stream = this.Stream;
            stream.Seek(Packet.MessageIndex, SeekOrigin.Begin);
            stream.SetLength(Packet.MessageIndex);
            ProtobufHelper.ToStream(message,stream);

            long i=stream.Length;
            stream.Seek(0, SeekOrigin.Begin);

            this.byteses[0].WriteTo(0, opcode);
            int index = 0;
            foreach (var bytes in this.byteses)
            {
                Array.Copy(bytes, 0, stream.GetBuffer(), index, bytes.Length);
                index += bytes.Length;
            }

            i = stream.Length;
            this.Send(stream);

        }

        private void Send(MemoryStream stream)
        {
            channel.Send(stream);
        }
        #endregion

        #region 销毁资源


        public void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }
            this.channel.Dispose();

            this.IsDisposed = true;
        }
        #endregion
    }
}