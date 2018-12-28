using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TT.Window;

namespace TT
{
    public class ChatViewModel
    {
        private ChatWin Win;
        private Friend Friend;
        public ChatViewModel(ChatWin win,Friend friend)
        {
            this.Win = win;
            this.Friend = friend;
            OnInit();
        }

        private void OnInit()
        {
            Win.Title = $"与{Friend.UserName}的聊天";
        }
    }
}
