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

namespace TestTransform2 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) {
            UpdateViewportSize();

            TranslateTargetToCenter();
        }

        void TranslateTargetToCenter() {
            Rect targetRect =
                target.RenderTransform.TransformBounds(new Rect(target.RenderSize));

            translater.X = (Container.RenderSize.Width - targetRect.Size.Width) / 2;
            translater.Y = (Container.RenderSize.Height - targetRect.Size.Height) / 2;
        }

        void UpdateViewportSize() {
            _viewportSize = new Size(Container.RenderSize.Width, Container.RenderSize.Height);
        }

        private void Window_MouseWheel(object sender, MouseWheelEventArgs e) {
            if (e.Delta == 0) return;


            double d = e.Delta / Math.Abs(e.Delta);

            if (_scaleValue < 0.5 && d < 0) return;

            if (_scaleValue > 20 && d > 0) return;

            _scaleValue += d * .2;

            //获取鼠标在缩放之前的目标上的位置
            Point targetZoomFocus1 = e.GetPosition(target);

            //获取目标在缩放之前的Rect
            Rect beforeScaleRect =
                target.RenderTransform.TransformBounds(new Rect(target.RenderSize));

            //缩放的中心点为左上角（0,0）
            scaler.ScaleX = _scaleValue;
            scaler.ScaleY = _scaleValue;

            //获取鼠标在缩放之后的目标上的位置
            Point targetZoomFocus2 = new Point(targetZoomFocus1.X * (1 + d * .2), targetZoomFocus1.Y * (1 + d * .2));

            //获取目标在缩放之后的Rect
            Rect afterScaleRect = target.RenderTransform.TransformBounds(new Rect(target.RenderSize));

            //算的缩放前后鼠标的位置间的差
            Vector v = targetZoomFocus2 - targetZoomFocus1;
                        

            if (afterScaleRect.Size.Width <= _viewportSize.Width) {
                //缩放之后居中
                double widthHalfDelta = (Container.RenderSize.Width - afterScaleRect.Width) / 2;
                translater.X = widthHalfDelta;
            }
            else if (afterScaleRect.X - v.X > 0) {
                //目标左边界与可视左边界对齐
                translater.X = 0;
            }
            else if (afterScaleRect.X + afterScaleRect.Width - v.X < Container.RenderSize.Width) {
                //目标右边界与可视右边界对齐
                translater.X = Container.RenderSize.Width - afterScaleRect.Size.Width;
            }
            else {
                //减去鼠标点在缩放前后之间的差值，实际上就是以鼠标点为中心进行缩放
                translater.X -= v.X;
            }


            if (afterScaleRect.Size.Height <= _viewportSize.Height) {
                double heightHalfDleta = (Container.RenderSize.Height - afterScaleRect.Height) / 2;
                translater.Y = heightHalfDleta;
            }
            else if (afterScaleRect.Y - v.Y > 0) {
                translater.Y = 0;
            }
            else if (afterScaleRect.Y + afterScaleRect.Height - v.Y < Container.RenderSize.Height) {
                translater.Y = Container.RenderSize.Height - afterScaleRect.Size.Height;
            }
            else {
                translater.Y -= v.Y;
            }
        }

        private void target_MouseDown(object sender, MouseButtonEventArgs e) {
            target.CaptureMouse();
        }

        private void target_MouseMove(object sender, MouseEventArgs e) {
            if (target.IsMouseCaptured) {
                var p = e.GetPosition(this);
                if (last.HasValue) {
                    var offsetX = p.X - last.Value.X;
                    var offsetY = p.Y - last.Value.Y;

                    //获得目标经过变换之后的Rect
                    Rect targetRect =
                        target.RenderTransform.TransformBounds(new Rect(target.RenderSize));

                    //当变化之后的矩形区域的宽或者高超过可视区域，并且目标的某一边界在可视区边界外时，保持目标边界在可视边界上
                    if (targetRect.Size.Width > Container.RenderSize.Width) {
                        if (targetRect.X + offsetX > 0) {
                            offsetX = -targetRect.X;
                        }
                        else if (targetRect.X + targetRect.Size.Width + offsetX < Container.RenderSize.Width) {
                            offsetX = Container.RenderSize.Width - (targetRect.Size.Width - Math.Abs(targetRect.X));
                        }

                        translater.X += offsetX;
                    }

                    if (targetRect.Size.Height > Container.RenderSize.Height) {
                        if (targetRect.Y + offsetY > 0) {
                            offsetY = -targetRect.Y;
                        }
                        else if (targetRect.Y + targetRect.Size.Height + offsetY < Container.RenderSize.Height) {
                            offsetY = Container.RenderSize.Height - (targetRect.Size.Height - Math.Abs(targetRect.Y));
                        }

                        translater.Y += offsetY;
                    }

                }
                last = p;
            }
        }

        private void target_MouseUp(object sender, MouseButtonEventArgs e) {
            target.ReleaseMouseCapture();
            last = null;
        }

        Point? last;
        Size _viewportSize;
        double _scaleValue = 1;


    }
}
