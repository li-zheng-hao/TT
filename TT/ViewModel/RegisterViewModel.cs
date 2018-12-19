using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Core;
using MahApps.Metro.Controls.Dialogs;
using TT.Window;

namespace TT
{
    public class RegisterViewModel
    {
        private RegisterWin registerWin;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="win"></param>
        public RegisterViewModel(RegisterWin win)
        {
            this.registerWin = win;
            registerWin.RegistBtn.Click += RegistBtn_Click;
            registerWin.CancelBtn.Click += CancelBtn_Click;
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.registerWin.Close();
        }

        /// <summary>
        /// 注册按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RegistBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var res = await ValidateValue(); 
            if (!res)
                return;
            var service = NetworkManager.GetInstance;
            if (service.RegisterCallBack == null)
                service.RegisterCallBack += RegisterCallBack;
            RegisterMessage msg = new RegisterMessage()
            {
                Email = registerWin.EmailBox.Text,
                Password = registerWin.PasswordBox.Text,
                Phone = registerWin.PhoneBox.Text,
                Username = registerWin.NameBox.Text
            };
            service.SendMessage(OperationCode.Register, msg);
        }
        
        /// <summary>
        /// 服务器发过来的注册结果
        /// </summary>
        /// <param name="obj"></param>
        private void RegisterCallBack(IMessage obj)
        {
            
            ResponseMessage msg = obj as ResponseMessage;
            if (!msg.Message.Equals("Y"))
            {
                System.Windows.Application.Current.Dispatcher.Invoke((Action)(async () =>
                {
                    string error = String.Empty;
                    switch (msg.Error)
                    {
                        case ErrorCode.ERR_EXISTPHONE:
                            error = "手机号已注册";
                            break;
                        case ErrorCode.ERR_EXISTEMAIL:
                            error = "邮箱已注册";
                            break;
                        case ErrorCode.ERR_UNKNOWN:
                            error = "服务器未知错误";
                            break;
                    }
                    await registerWin.ShowMessageAsync("", $"注册失败，{error}", MessageDialogStyle.Affirmative);
                }));
            }
            else
            {
                System.Windows.Application.Current.Dispatcher.Invoke((Action)(async () =>
                {
                    await registerWin.ShowMessageAsync("", "注册成功", MessageDialogStyle.Affirmative);
                    registerWin.Close();
                }));

            }
        }

        #region 验证输入

        /// <summary>
        /// 验证用户输入是否合法
        /// </summary>
        /// <returns></returns>
        private async Task<bool> ValidateValue()
        {
            if (!registerWin.PasswordBox.Text.Equals(registerWin.RepeatBox.Text))
            {
                await registerWin.ShowMessageAsync("", "重复密码错误", MessageDialogStyle.Affirmative);
                return false;
            }
            else if (registerWin.NameBox.Text.Equals(""))
            {
                await registerWin.ShowMessageAsync("", "用户名不能为空", MessageDialogStyle.Affirmative);
                return false;
            }
            else if (registerWin.PasswordBox.Text.Equals(""))
            {
                await registerWin.ShowMessageAsync("", "密码不能为空", MessageDialogStyle.Affirmative);
                return false;
            }
            else if (registerWin.PhoneBox.Text.Equals(""))
            {
                await registerWin.ShowMessageAsync("", "手机号不能为空", MessageDialogStyle.Affirmative);
                return false;
            }
            else if (registerWin.RepeatBox.Text.Equals(""))
            {
                await registerWin.ShowMessageAsync("", "请再次确认密码", MessageDialogStyle.Affirmative);
                return false;
            }
            else if (!IsMobilePhone(registerWin.PhoneBox.Text))
            {
                await registerWin.ShowMessageAsync("", "手机号不合法", MessageDialogStyle.Affirmative);
                return false;
            }
            else if (!IsEmail(registerWin.EmailBox.Text))
            {
                await registerWin.ShowMessageAsync("", "邮箱不合法", MessageDialogStyle.Affirmative);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 校验手机号是否合法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(string input)
        {
            if (input.Length == 0)
            {
                return false;
            }
            else if (input.Length != 11)
            {
                return false;
            }
            return System.Text.RegularExpressions.Regex.IsMatch(input, @"^[1]+[3,4,5,7,8]+\d{9}");
        }

        /// <summary>
        /// 验证邮箱是否合法
        /// </summary>
        /// <param name="str_postalcode"></param>
        /// <returns></returns>
        public bool IsEmail(string str_postalcode)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_postalcode, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }

        #endregion


    }
}
