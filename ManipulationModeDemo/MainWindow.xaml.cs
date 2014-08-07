using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ManipulationModeDemo
{
    public partial class MainWindow : Window
    {
        ManipulationModes currentMode = ManipulationModes.All;

        public MainWindow()
        {
            InitializeComponent();

            // Build list of radio buttons
            foreach (ManipulationModes mode in Enum.GetValues(typeof(ManipulationModes)))
            {
                RadioButton radio = new RadioButton
                {
                    Content = mode,
                    IsChecked = mode == currentMode,
                };
                radio.Checked += new RoutedEventHandler(OnRadioChecked);
                modeList.Children.Add(radio);
            }
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
