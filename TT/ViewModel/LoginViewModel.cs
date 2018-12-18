using System;
using System.Windows;
using Core;
using MahApps.Metro.Controls.Dialogs;
using TT.Window;

namespace TT
{
    public class LoginViewModel
    {
        private LoginWin loginWin;

        public LoginViewModel(LoginWin loginWin)
        {
            this.loginWin = loginWin;
            this.loginWin.LoginBtn.Click += Login_Click;
            this.loginWin.RegisterBtn.Click += Register_Click;
        }

        /// <summary>
        /// 注册按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWin regist = new RegisterWin();
            regist.Show();
        }

        /// <summary>
        /// 登录按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private  void Login_Click(object sender, RoutedEventArgs e)
        {

            var networkManager = NetworkManager.GetInstance;
            networkManager.LoginCallBack += LoginCallBack;
            LoginMessage msg = new LoginMessage()
            {
                Email = loginWin.UserNameTextBox.Text,
                Password = loginWin.PasswordBox.Password
            };
            networkManager.SendMessage(OperationCode.Login, msg);
        }

        /// <summary>
        /// 服务端消息传回判断是否登陆成功
        /// </summary>
        /// <param name="message"></param>
        private  void LoginCallBack(IMessage message)
        {
            var login = message as LoginMessage;
            if (login.Message.Equals("Y"))
            {
                App.Current.Dispatcher.Invoke((Action)(() =>
                {
                    MainWin win = new MainWin();
                    win.Show();
                    this.loginWin.Close();
                }));
            }
            else
            {
                App.Current.Dispatcher.Invoke((Action)(() =>
                {

                    // This demo runs on .Net 4.0, but we're using the Microsoft.Bcl.Async package so we have async/await support
                    // The package is only used by the demo and not a dependency of the library!
                    var mySettings = new MetroDialogSettings()
                    {
                        AffirmativeButtonText = "错误",
                        NegativeButtonText = "Go away!",
                        FirstAuxiliaryButtonText = "Cancel",
                    };

                    this.loginWin.ShowMessageAsync("错误", "账号密码错误",
                        MessageDialogStyle.Affirmative, null);
//                    MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary


                }));
            }
        }

    }
}
