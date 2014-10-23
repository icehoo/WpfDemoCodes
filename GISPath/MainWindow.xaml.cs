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

namespace GISPath
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		LineSegment lineSegment1;
		LineSegment lineSegment2;
		Path path;

		public MainWindow()
		{
			InitializeComponent();
			CreatePath();
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			//StreamGeometry streamGeometry = rec.Data as StreamGeometry;
			//PathGeometry pathGeometry = streamGeometry.
			//foreach (var item in pathGeometry.Figures)
			//{

			//}	
			lineSegment1.Point = new Point(200,300);
			//this.UpdateLayout();
			//path.UpdateLayout();
		}

		void CreatePath()
		{
			path = new Path();
			PathGeometry pathGeometry = new PathGeometry();
			PathFigure pathFigure = new PathFigure();
			pathFigure.StartPoint = new Point(100, 100);
			PathSegmentCollection segmentCollection = new PathSegmentCollection();

			lineSegment1 = new LineSegment(){ Point = new Point(200, 200) };
			lineSegment2 = new LineSegment(){ Point = new Point(300, 200) };
			segmentCollection.Add(lineSegment1);
			segmentCollection.Add(lineSegment2);
			//segmentCollection.Add(new LineSegment() { Point = new Point(200, 200) });
			//segmentCollection.Add(new LineSegment() { Point = new Point(300, 200) });
			pathFigure.Segments = segmentCollection;
			pathGeometry.Figures = new PathFigureCollection() { pathFigure };
			path.Data = pathGeometry;
			path.Stroke = new SolidColorBrush(Colors.BlueViolet);
			path.StrokeThickness = 3;

			lay.Children.Add(path);
		}
	}
}
