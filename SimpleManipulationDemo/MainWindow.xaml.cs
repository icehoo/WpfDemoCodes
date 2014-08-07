using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SimpleManipulationDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnManipulationStarting(ManipulationStartingEventArgs args)
        {
            args.ManipulationContainer = this;

            // Adjust Z-order
            FrameworkElement element = args.Source as FrameworkElement;
            Panel pnl = element.Parent as Panel;

            for (int i = 0; i < pnl.Children.Count; i++)
                Panel.SetZIndex(pnl.Children[i],
                    pnl.Children[i] == element ? pnl.Children.Count : i);

            args.Handled = true;
            base.OnManipulationStarting(args);
        }

        protected override void OnManipulationDelta(ManipulationDeltaEventArgs args)
        {
            UIElement element = args.Source as UIElement;
            MatrixTransform xform = element.RenderTransform as MatrixTransform;
            Matrix matrix = xform.Matrix;
            ManipulationDelta delta = args.DeltaManipulation;
            Point center = args.ManipulationOrigin;
            matrix.ScaleAt(delta.Scale.X, delta.Scale.Y, center.X, center.Y);
            matrix.RotateAt(delta.Rotation, center.X, center.Y);
            matrix.Translate(delta.Translation.X, delta.Translation.Y);
            xform.Matrix = matrix;

            args.Handled = true;
            base.OnManipulationDelta(args);
        }
    }
}
