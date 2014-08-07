using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CanvasMoveDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Point targetPoint;//选中控件的鼠标位置偏移量

        public MainWindow()
        {
            InitializeComponent();
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var targetElement = e.Source as IInputElement;

            if (targetElement != null)
            {
                targetPoint = e.GetPosition(targetElement);
                //开始捕获鼠标
                targetElement.CaptureMouse();
            }
        }

        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //取消捕获鼠标
            Mouse.Capture(null);
        }



        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            //确定鼠标左键处于按下状态并且有元素被选中
            var targetElement = Mouse.Captured as UIElement;

            if (e.LeftButton == MouseButtonState.Pressed && targetElement != null)
            {
                var pCanvas = e.GetPosition(canvas);
                //设置最终位置
                Canvas.SetLeft(targetElement, pCanvas.X - targetPoint.X);
                Canvas.SetTop(targetElement, pCanvas.Y - targetPoint.Y);
            }
        }
    }
}
