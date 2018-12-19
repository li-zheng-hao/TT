

namespace TT.Window
{
    /// <summary>
    /// RegisterWin.xaml 的交互逻辑
    /// </summary>
    public partial class RegisterWin 
    {
        public RegisterWin()
        {
            InitializeComponent();
            RegisterViewModel rm=new RegisterViewModel(this);
        }

     
    }
}
