using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class OperationCode
    {
        public const ushort Login=1;
        public const ushort Register=2;
        public const ushort Error=3;
        public const ushort Response = 4;
        public const ushort GetFriendList = 5;
        public const ushort AddFriend = 6;

    }
}
