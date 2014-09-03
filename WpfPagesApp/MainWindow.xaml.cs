using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfPagesApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Random random = new Random();

        private Color GetRandomColor()
        {
            return Color.FromRgb((byte)random.Next(0, 255), (byte)random.Next(0, 255), (byte)random.Next(0, 255));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int num = int.Parse(textBox1.Text);
            List<Panel> Panels = new List<Panel>();
            for (int i = 0; i < num; i++)
            {
                WrapPanel rectangle = new WrapPanel();
                rectangle.Width = tPageControl1.GetPageWidth();
                rectangle.Height = tPageControl1.GetPageHieght();
                rectangle.Background = new SolidColorBrush(GetRandomColor());
                Panels.Add(rectangle);
            }
            tPageControl1.AddPage(Panels, 1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tPageControl1.ClearPage();
        }
    }
}
