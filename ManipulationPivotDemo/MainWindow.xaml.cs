using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ManipulationPivotDemo
{
    public partial class MainWindow : Window
    {
        ManipulationModes currentMode = ManipulationModes.All;

        public MainWindow()
        {
            InitializeComponent();
        }

        void OnRadioChecked(object sender, RoutedEventArgs args)
        {
            currentMode = (ManipulationModes)(sender as RadioButton).Content;
        }

        protected override void OnManipulationStarting(ManipulationStartingEventArgs args)
        {
            args.ManipulationContainer = this;
            args.Mode = currentMode;

            // Adjust Z-order
            FrameworkElement element = args.Source as FrameworkElement;
            Panel pnl = element.Parent as Panel;

            for (int i = 0; i < pnl.Children.Count; i++)
                Panel.SetZIndex(pnl.Children[i],
                    pnl.Children[i] == element ? pnl.Children.Count : i);

            // Set Pivot
            Point center = new Point(element.ActualWidth / 2, element.ActualHeight / 2);
            center = element.TranslatePoint(center, this);
            args.Pivot = new ManipulationPivot(center, 48);

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
            matrix.Translate(-center.X, -center.Y);
            matrix.Scale(delta.Scale.X, delta.Scale.Y);
            matrix.Rotate(delta.Rotation);
            matrix.Translate(center.X, center.Y);
            matrix.Translate(delta.Translation.X, delta.Translation.Y);
            xform.Matrix = matrix;

            args.Handled = true;
            base.OnManipulationDelta(args);
        }
    }
}