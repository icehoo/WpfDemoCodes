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
using System.ComponentModel;

namespace GISPath
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		LineSegment lineSegment1;
		LineSegment lineSegment2;
		LineSegment lineSegment3;
		LineSegment lineSegment4;
		LineSegment lineSegment5;
		LineSegment lineSegment6;
		Path path;
		int radius = 6;



		//public double PathWidth
		//{
		//    get
		//    {
		//        return (double)GetValue(PathWidthProperty);
		//    }
		//    set
		//    {
		//        SetValue(PathWidthProperty, value);
		//    }
		//}

		//// Using a DependencyProperty as the backing store for PathWidth.  This enables animation, styling, binding, etc...
		//public static readonly DependencyProperty PathWidthProperty =
		//    DependencyProperty.Register("PathWidth", typeof(double), typeof(MainWindow), new UIPropertyMetadata(0.0));


		private double _PathWidth;

		public double PathWidth
		{
			get { return _PathWidth; }
			set
			{
				_PathWidth = value;
				RaisePropertyChanged("PathWidth");
			}
		}

		public MainWindow()
		{
			InitializeComponent();

			Binding bind = new Binding("ActualWidth") { Source = textBox1 };

			//this.SetBinding(PathWidthProperty, bind);
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			//lineSegment2.Point = new Point(220, 10);
		}

		void CreatePath(double width, double height, int radiusValue)
		{
			path = new Path();
			//path.Fill = new SolidColorBrush(Colors.Red);
			PathGeometry pathGeometry = new PathGeometry();
			PathFigure pathFigure = new PathFigure();
			pathFigure.StartPoint = new Point(0, 0);
			PathSegmentCollection segmentCollection = new PathSegmentCollection();

			lineSegment1 = new LineSegment() { Point = new Point(width - radiusValue, 0) };
			lineSegment2 = new LineSegment() { Point = new Point(width, radiusValue) };
			lineSegment3 = new LineSegment() { Point = new Point(width, height) };
			lineSegment4 = new LineSegment() { Point = new Point(radiusValue, height) };
			lineSegment5 = new LineSegment() { Point = new Point(0, height - radiusValue) };
			lineSegment6 = new LineSegment() { Point = new Point(0, -1) };
			segmentCollection.Add(lineSegment1);
			segmentCollection.Add(lineSegment2);
			segmentCollection.Add(lineSegment3);
			segmentCollection.Add(lineSegment4);
			segmentCollection.Add(lineSegment5);
			segmentCollection.Add(lineSegment6);
			pathFigure.Segments = segmentCollection;
			pathGeometry.Figures = new PathFigureCollection() { pathFigure };
			path.Data = pathGeometry;
			path.Stroke = new SolidColorBrush(Colors.BlueViolet);
			path.StrokeThickness = 1;
			path.Margin = textBox1.Margin;
			path.SnapsToDevicePixels = true;
			lay.Children.Add(path);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			double width = this.textBox1.ActualWidth;
			double height = this.textBox1.ActualHeight;
			CreatePath(width, height, radius);
		}

		private void textBox1_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			double width = this.textBox1.ActualWidth;
			double height = this.textBox1.ActualHeight;
			if (lineSegment1 != null)
			{
				lineSegment1.Point = new Point(width - radius, 0);
				lineSegment2.Point = new Point(width, radius);
				lineSegment3.Point = new Point(width, height);
				lineSegment4.Point = new Point(radius, height);
				lineSegment5.Point = new Point(0, height - radius);
			}

			double wi = PathWidth;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void RaisePropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
