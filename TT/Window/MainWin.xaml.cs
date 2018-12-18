using System.Windows;
using MahApps.Metro.Controls.Dialogs;

namespace TT.Window
{
    /// <summary>
    /// MainWin.xaml 的交互逻辑
    /// </summary>
    public partial class MainWin
    {
        public MainWin()
        {
            InitializeComponent();
        }

        private void Test()
        {
            
        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = "确定";
            setting.NegativeButtonText = "返回";
            setting.AnimateShow = false;
            this.ShowInputAsync("添加好友", "", setting);

        }
    }
}
