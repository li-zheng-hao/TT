namespace Core
{
    public static class ErrorCode
    {
        /// <summary>
        /// 网络错误 1-1000
        /// </summary>
        public const int ERR_PeerDisconnect = 1;
        public const int ERR_SocketCantSend = 2;
        public const int ERR_SocketError = 3;
        
        /// <summary>
        /// 数据库错误 1001-2000
        /// </summary>
        public const int ERR_EXISTPHONE = 1001;
        public const int ERR_EXISTEMAIL = 1002;
        public const int ERR_UNKNOWN = 1003;
        
    }
}
