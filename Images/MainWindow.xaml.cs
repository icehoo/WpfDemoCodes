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
using System.IO;
using System.Collections;
using System.Windows.Media.Effects;



namespace Images
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public int i = 0;
        public string str = "";
        bool state = false;
        public Point currentPosition;

        ArrayList newArrayList = new ArrayList();
        Image canvasImage = new Image();

        
        

        public MainWindow() 
        {
            InitializeComponent();
            DirectoryInfo Dir = new DirectoryInfo(Environment.CurrentDirectory + "//Image");
            foreach (FileInfo file in Dir.GetFiles("*.jpg", SearchOption.AllDirectories))
            {
                str = file.ToString();
                newArrayList.Add(str);
            }
            for (; i < newArrayList.Count; i++)
            {
                Image listImage = new Image();
                Border br = new Border();
                Border bs = new Border();
                BitmapImage bitmapImage = new BitmapImage();
                ListBoxItem newListBox = new ListBoxItem();

                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(Environment.CurrentDirectory + "//Image//" + newArrayList[i].ToString());
                bitmapImage.EndInit();
                listImage.Source = bitmapImage;

                listImage.Width = 280;
                listImage.Height = 280;

                //Image添加到Border中
                br.Child = listImage;
                
                br.Padding = new System.Windows.Thickness((float)1.1f);
                br.Background = Brushes.White;
                br.BorderThickness = new System.Windows.Thickness((int)3);
                br.Margin = new System.Windows.Thickness((int)1);
               // br.BorderBrush = Brushes.Green;

                //阴影效果
                DropShadowEffect myDropShadowEffect = new DropShadowEffect();

                // Set the color of the shadow to Black.
                Color myShadowColor = new Color();
                myShadowColor.A = 255; // Note that the alpha value is ignored by Color property. 
                // The Opacity property is used to control the alpha.
                myShadowColor.B = 50;
                myShadowColor.G = 50;
                myShadowColor.R = 50;

                myDropShadowEffect.Color = myShadowColor;

                // Set the direction of where the shadow is cast to 320 degrees.
                myDropShadowEffect.Direction = 310;

                // Set the depth of the shadow being cast.
                myDropShadowEffect.ShadowDepth = 20;

                // Set the shadow softness to the maximum (range of 0-1).
                myDropShadowEffect.BlurRadius = 10;

                // Set the shadow opacity to half opaque or in other words - half transparent.
                // The range is 0-1.
                myDropShadowEffect.Opacity = 0.4;

                // Apply the effect to the Button.
              //  listImage.Effect = myDropShadowEffect;

                newListBox.Effect = myDropShadowEffect;

                listImage.TouchUp += list1_TouchUp;     //TouchUp时才能被选中
                newListBox.Content = br;

                newListBox.Margin = new System.Windows.Thickness((int)10);

                list1.Items.Add(newListBox);
            }
        }

        private void canvas_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)e.Source;
            Matrix matrix = ((MatrixTransform)element.RenderTransform).Matrix;
            ManipulationDelta deltaManipulation = e.DeltaManipulation;
            Point center = new Point(element.ActualWidth / 2, element.ActualHeight / 2);
            center = matrix.Transform(center);
            matrix.ScaleAt(deltaManipulation.Scale.X, deltaManipulation.Scale.Y, center.X, center.Y);
            matrix.RotateAt(e.DeltaManipulation.Rotation, center.X, center.Y);
            matrix.Translate(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);
            ((MatrixTransform)element.RenderTransform).Matrix = matrix;
        }
        private void list1_TouchUp(object sender, TouchEventArgs e)
        {
                if (list1.SelectedIndex >= 0)
                {
                    BitmapImage bitmapImage = new BitmapImage();

                    bitmapImage.BeginInit();
                    bitmapImage.UriSource = new Uri(Environment.CurrentDirectory + "//Image//" + newArrayList[list1.SelectedIndex].ToString());
                    bitmapImage.EndInit();
                    canvasImage.Source = bitmapImage;

                    canvasImage.Width = 300;
                    canvasImage.Height = 300;

                    canvasImage.IsManipulationEnabled = true;
                    canvasImage.RenderTransform = new MatrixTransform();


                    Canvas.SetTop(canvasImage, 200);
                    Canvas.SetLeft(canvasImage, 700);

                    if (!state)
                    {
                        canvas.Children.Add(canvasImage);
                        state = true;
                    }
                }
        }

        private void canvas_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = canvas;
            e.Mode = ManipulationModes.All;
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) 
            {
                canvas.Children.Remove(canvasImage);
                state = false;
            }
        }

        private void list1_TouchMove(object sender, TouchEventArgs e)
        {
            this.scrolls.PanningMode = PanningMode.HorizontalOnly;
            this.scrolls.CanContentScroll = false;
          
        }

        private void list1_TouchEnter(object sender, TouchEventArgs e)
        {
            if (list1.SelectedIndex >= 0)
            {
                BitmapImage bitmapImage = new BitmapImage();

                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(Environment.CurrentDirectory + "//Image//" + newArrayList[list1.SelectedIndex].ToString());
                bitmapImage.EndInit();
                canvasImage.Source = bitmapImage;

                canvasImage.Width = 300;
                canvasImage.Height = 300;

                canvasImage.IsManipulationEnabled = true;
                canvasImage.RenderTransform = new MatrixTransform();

                Canvas.SetTop(canvasImage, 200);
                Canvas.SetLeft(canvasImage, 700);

                if (!state)
                {
                    canvas.Children.Add(canvasImage);
                    state = true;
                }
            }
        }

        private void list1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (list1.SelectedIndex >= 0)
            {
                BitmapImage bitmapImage = new BitmapImage();

                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(Environment.CurrentDirectory + "//Image//" + newArrayList[list1.SelectedIndex].ToString());
                bitmapImage.EndInit();
                canvasImage.Source = bitmapImage;

                canvasImage.Width = 300;
                canvasImage.Height = 300;

                //阴影效果
                DropShadowEffect myDropShadowEffect = new DropShadowEffect();

                // Set the color of the shadow to Black.
                Color myShadowColor = new Color();
                myShadowColor.A = 255; // Note that the alpha value is ignored by Color property. 
                // The Opacity property is used to control the alpha.
                myShadowColor.B = 50;
                myShadowColor.G = 50;
                myShadowColor.R = 50;

                myDropShadowEffect.Color = myShadowColor;

                // Set the direction of where the shadow is cast to 320 degrees.
                myDropShadowEffect.Direction = 310;

                // Set the depth of the shadow being cast.
                myDropShadowEffect.ShadowDepth = 25;

                // Set the shadow softness to the maximum (range of 0-1).
                myDropShadowEffect.BlurRadius = 13;

                // Set the shadow opacity to half opaque or in other words - half transparent.
                // The range is 0-1.
                myDropShadowEffect.Opacity = 0.4;

                // Apply the effect to the Button.
                //  listImage.Effect = myDropShadowEffect;

                canvasImage.Effect = myDropShadowEffect;

                canvasImage.IsManipulationEnabled = true;
                canvasImage.RenderTransform = new MatrixTransform();

                Canvas.SetTop(canvasImage, 200);
                Canvas.SetLeft(canvasImage, 700);


                if (!state)
                {
                    canvas.Children.Add(canvasImage);
                    state = true;
                }
            }
        }

        //private void list1_TouchLeave(object sender, TouchEventArgs e)
        //{
        //    if (list1.SelectedIndex >= 0)
        //    {
        //        BitmapImage bitmapImage = new BitmapImage();

        //        bitmapImage.BeginInit();
        //        bitmapImage.UriSource = new Uri(Environment.CurrentDirectory + "//Image//" + newArrayList[list1.SelectedIndex].ToString());
        //        bitmapImage.EndInit();
        //        canvasImage.Source = bitmapImage;

        //        canvasImage.Width = 300;
        //        canvasImage.Height = 300;

        //        canvasImage.IsManipulationEnabled = true;
        //        canvasImage.RenderTransform = new MatrixTransform();

        //        Canvas.SetTop(canvasImage, 200);
        //        Canvas.SetLeft(canvasImage, 700);

        //        if (!state)
        //        {
        //            canvas.Children.Add(canvasImage);
        //            state = true;
        //        }
        //    }
        //}
    }
}
