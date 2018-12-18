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
            LoginViewModel vm=new LoginViewModel(this);
        }
    }
}
