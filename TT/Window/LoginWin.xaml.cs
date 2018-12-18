using System.Windows;

namespace TT.Window
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWin
    {
        public LoginWin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 登陆按钮单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            
            MainWin main = new MainWin();
            main.Show();
            Close();

        }

        /// <summary>
        /// 注册按钮单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWin regist = new RegisterWin();

            regist.Show();
        }
    }
}
