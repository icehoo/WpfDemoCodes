using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CanvasZoomDemo
{
    /// <summary>
    /// 作者：热咖
    /// 此demo用Canvas和ViewBox实现了类似Blend设计器里缩放和移动的效果
    /// 算法无非是计算偏移量
    /// 缩放时以鼠标为中心自动偏移
    /// 移动则通过捕获鼠标计算偏移量
    /// 最重要的是105行，读者可以注掉对比效果
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 变量
        //用于移动
        Point targetPoint;        //鼠标落下时鼠标在CanvasRoot里的位置
        Point pMoved;             //鼠标移动后鼠标在CanvasRoot里的新位置
        Point ViewBoxMainLocation;//鼠标落下时外层ViewBoxMain在CanvasRoot里的位置
        //用于缩放
        Point mousePoint;         //缩放前鼠标在ViewBoxMain的位置
        Point locationPoint;      //缩放前ViewBoxMain在CanvasRoot里的位置
        Point NewPoint;           //缩放后计算ViewBoxMain的新坐标
        double biliUp = 1.1;      //增大比例
        double biliDown = 0.9;    //缩小比例
        #endregion

        #region 构造函数
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region 业务
        private void CanvasMain_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var targetElement = e.Source as IInputElement;

            if (targetElement != null)
            {
                ViewBoxMainLocation.X = Canvas.GetLeft(ViewBoxMain);
                ViewBoxMainLocation.Y = Canvas.GetTop(ViewBoxMain);
                targetPoint = e.GetPosition(CanvasRoot);
                //开始捕获鼠标
                targetElement.CaptureMouse();
            }
        }

        private void CanvasMain_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //取消捕获鼠标   
            Mouse.Capture(null);
            this.Cursor = System.Windows.Input.Cursors.Arrow;
        }

        private void CanvasMain_MouseMove(object sender, MouseEventArgs e)
        {
            //确定鼠标左键处于按下状态并且有元素被选中

            var targetElement = Mouse.Captured as UIElement;

            if (e.LeftButton == MouseButtonState.Pressed && targetElement != null)
            {
                pMoved = e.GetPosition(CanvasRoot);
                //设置最终位置
                Canvas.SetLeft(ViewBoxMain, pMoved.X - targetPoint.X + ViewBoxMainLocation.X);
                Canvas.SetTop(ViewBoxMain, pMoved.Y - targetPoint.Y + ViewBoxMainLocation.Y);

                this.Cursor = System.Windows.Input.Cursors.ScrollAll;
            }
        }

        private void CanvasMain_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mousePoint = e.GetPosition(ViewBoxMain);//获取当前的点
            locationPoint.X = Canvas.GetLeft(ViewBoxMain);
            locationPoint.Y = Canvas.GetTop(ViewBoxMain);

            double width = ViewBoxMain.Width;
            double height = ViewBoxMain.Height;

            if (e.Delta > 0)//放大
            {
                ViewBoxMain.Width = width * biliUp;
                ViewBoxMain.Height = height * biliUp;

                NewPoint.X = locationPoint.X - mousePoint.X * (biliUp - 1);
                NewPoint.Y = locationPoint.Y - mousePoint.Y * (biliUp - 1);
            }
            else
            {
                ViewBoxMain.Width = width * biliDown;
                ViewBoxMain.Height = height * biliDown;

                NewPoint.X = locationPoint.X + mousePoint.X * (1 - biliDown);
                NewPoint.Y = locationPoint.Y + mousePoint.Y * (1 - biliDown);
            }

            Canvas.SetLeft(ViewBoxMain, NewPoint.X);
            Canvas.SetTop(ViewBoxMain, NewPoint.Y);

            e.Handled = true;//一定要有这行代码，否则会有偏差
        }
        #endregion
    }
}
