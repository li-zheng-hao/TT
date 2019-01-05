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
        public MainWinViewModel(LoginMessage msg, MainWin win)
        {
            this.Win = win;
            this.User = msg;
            Win.DataContext = msg;
            OnInit();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void OnInit()
        {

            Win.AddFriendBtn.Click += ButtonBase_OnClickAsync;
            Win.DataContext = User;
            Win.Tree.MouseDoubleClick += Tree_MouseDoubleClick;
            FriendListMessage msg = new FriendListMessage()
            {
                OwnerId = User.Id,
            };
            NetworkManager.GetInstance.SendMessage(OperationCode.GetFriendList, msg);
            NetworkManager.GetInstance.GetFriendListCallBack += GetFriendListCallBack;
            NetworkManager.GetInstance.AddFriendCallBack += AddFriendCallBack;


        }

        private void Tree_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                var friend = Win.Tree.SelectedValue as Friend;

                ChatWin win = new ChatWin();
                
                ChatViewModel chatViewModel = new ChatViewModel(win, friend);
                win.Show();
            });
        }

       

        /// <summary>
        /// 双击好友列表事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FriendsList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           
            
        }

        /// <summary>
        /// 添加好友完成回调
        /// </summary>
        /// <param name="obj"></param>
        private void AddFriendCallBack(IMessage obj)
        {
            ResponseMessage ms = obj as ResponseMessage;
            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                if (ms.Message.Equals("Y"))
                {
                    Win.ShowMessageAsync("添加成功", "现在可以和该好友聊天啦！");
                }
                else
                    Win.ShowMessageAsync("添加失败", "该用户不存在！");
            }));


        }

        /// <summary>
        /// 获取好友完成回调
        /// </summary>
        /// <param name="obj"></param>
        private void GetFriendListCallBack(IMessage obj)
        {
            var friend = obj as FriendListMessage;
            if (friend.Friends != null)
            {
                App.Current.Dispatcher.Invoke((Action)(() =>
                {
                    FriendList = friend.Friends;
                    Win.Friend.ItemsSource = FriendList;
                }));
                
            }
        }

        /// <summary>
        /// 添加好友按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ButtonBase_OnClickAsync(object sender, RoutedEventArgs e)
        {
            MetroDialogSettings setting = new MetroDialogSettings
            {
                AffirmativeButtonText = "确定",
                NegativeButtonText = "返回",
                AnimateShow = false,

            };
            string result = await Win.ShowInputAsync("添加好友", "", setting);
            if (string.IsNullOrEmpty(result))
                return;
            AddFriendMessage msg = new AddFriendMessage()
            {
                OwnerId = User.Id,
                FriendEmail = result
            };
            NetworkManager.GetInstance.SendMessage(OperationCode.AddFriend, msg);


        }
    }
}
