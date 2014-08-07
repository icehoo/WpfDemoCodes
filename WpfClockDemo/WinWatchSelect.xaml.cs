using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfClockDemo
{
    /// <summary>
    /// WinWatchSelect.xaml 的交互逻辑
    /// </summary>
    public partial class WinWatchSelect : Window
    {
        public WinWatchSelect()
        {
            this.InitializeComponent();

            // 在此点之下插入创建对象所需的代码。
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WinClock win;
            if ((bool)rbtMechan.IsChecked)
            {
                win = new WinClock(true);
            }
            else
            {
                win = new WinClock(false);
            }
            this.Hide();
            win.Show();
        }
    }
}