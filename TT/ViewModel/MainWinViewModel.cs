using MahApps.Metro.Controls.Dialogs;
using System;
using System.Windows;
using TT.Window;
using Core;
using System.Collections.Generic;

namespace TT
{
    public class MainWinViewModel
    {
        private MainWin Win { get; set; }
        /// <summary>
        /// current user data
        /// </summary>
        public LoginMessage User { get; set; }
        /// <summary>
        /// friend list
        /// </summary>
        private List<Friend> FriendList { get; set; }

        public MainWinViewModel(MainWin win)
        {
            this.Win = win;
            OnInit();
        }
        public MainWinViewModel(LoginMessage msg,MainWin win)
        {
            this.Win = win;
            this.User = msg;
            Win.DataContext = msg;
            OnInit();
        }
        
        private  void OnInit()
        {
            
            Win.AddFriendBtn.Click +=ButtonBase_OnClickAsync;
            Win.DataContext = User;
            FriendListMessage msg = new FriendListMessage()
            {
                OwnerId = User.Id,
            };
            NetworkManager.GetInstance.SendMessage(OperationCode.GetFriendList, msg);
            NetworkManager.GetInstance.GetFriendListCallBack += GetFriendList;


        }

        private void GetFriendList(IMessage obj)
        {
            var friend = obj as FriendListMessage;
            FriendList = friend.Friends;
            Win.FriendsList.ItemsSource = FriendList;
        }

        public async void ButtonBase_OnClickAsync(object sender, RoutedEventArgs e)
        {
            MetroDialogSettings setting = new MetroDialogSettings
            {
                AffirmativeButtonText = "确定",
                NegativeButtonText = "返回",
                AnimateShow = false,
               
            };
            string result=await Win.ShowInputAsync("添加好友", "", setting);
            //todo 在这添加好友
            AddFriendMessage msg = new AddFriendMessage()
            {
                OwnerId = User.Id,
                FriendEmail = result
            };
            NetworkManager.GetInstance.SendMessage(OperationCode.AddFriend, msg);


        }
    }
}
