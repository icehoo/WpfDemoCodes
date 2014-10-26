using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Diagnostics;
using System.ComponentModel;

namespace ChatBubble
{
	public class RectangleTextBox : TextBox, INotifyPropertyChanged
	{
		#region 变量
		Grid gridRoot;
		ScrollViewer sv;
		LineSegment lineSegment1;
		LineSegment lineSegment2;
		LineSegment lineSegment3;
		LineSegment lineSegment4;
		LineSegment lineSegment5;
		LineSegment lineSegment6;
		Path path;
		int radius = 10;
		double stroke = 1;//边框粗细
		double oldWidth = 0;
		double oldHeight = 0;
		#endregion

		#region 属性
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

		private double _PathHeight;

		public double PathHeight
		{
			get { return _PathHeight; }
			set
			{
				_PathHeight = value;
				RaisePropertyChanged("PathHeight");
			}
		}
		#endregion

		#region 构造函数
		public RectangleTextBox()
		{
			Style style = this.FindResource("RectangleTextBoxStyle") as Style;
			if (style != null)
			{
				this.Style = style;
			}
		}
		#endregion

		#region 业务
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			gridRoot = this.Template.FindName("GridRoot", this) as Grid;
			sv = this.Template.FindName("PART_ContentHost", this) as ScrollViewer;
			sv.SizeChanged += new SizeChangedEventHandler(sv_SizeChanged);
			//lbc = this.Template.FindName("Bd", this) as ListBoxChrome;
			//lbc.SizeChanged += new SizeChangedEventHandler(lbc_SizeChanged);
			//CreatePath(20, 20, radius);
			CreatePath(this.ActualWidth, this.ActualHeight, radius);
		}

		void sv_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			bool isSizeChenged = false;
			if (oldWidth != sv.ActualWidth)
			{
				isSizeChenged = true;
				oldWidth = sv.ActualWidth;
			}
			if (oldHeight != sv.ActualHeight)
			{
				isSizeChenged = true;
				oldHeight = sv.ActualHeight;
			}

			if (isSizeChenged)
			{
				double width = oldWidth;
				double height = oldHeight;
				if (lineSegment1 != null)
				{
					lineSegment1.Point = new Point(width - radius, 0);
					lineSegment2.Point = new Point(width, radius);
					lineSegment3.Point = new Point(width, height);
					lineSegment4.Point = new Point(radius, height);
					lineSegment5.Point = new Point(0, height - radius);
					Console.WriteLine(path.Margin.ToString());
					//Debug.Assert(path.Margin != new Thickness(0, 0, 0, 0));
				}
			}
		}

		void CreatePath(double width, double height, int radiusValue)
		{
			path = new Path();
			path.Fill = new SolidColorBrush(Colors.Green);
			PathGeometry pathGeometry = new PathGeometry();
			PathFigure pathFigure = new PathFigure();
			pathFigure.StartPoint = new Point(0, 0);
			PathSegmentCollection segmentCollection = new PathSegmentCollection();

			lineSegment1 = new LineSegment() { Point = new Point(width - radiusValue, 0) };
			lineSegment2 = new LineSegment() { Point = new Point(width, radiusValue) };
			lineSegment3 = new LineSegment() { Point = new Point(width, height) };
			lineSegment4 = new LineSegment() { Point = new Point(radiusValue, height) };
			lineSegment5 = new LineSegment() { Point = new Point(0, height - radiusValue) };
			lineSegment6 = new LineSegment() { Point = new Point(0, -stroke / 2) };
			segmentCollection.Add(lineSegment1);
			segmentCollection.Add(lineSegment2);
			segmentCollection.Add(lineSegment3);
			segmentCollection.Add(lineSegment4);
			segmentCollection.Add(lineSegment5);
			segmentCollection.Add(lineSegment6);
			pathFigure.Segments = segmentCollection;
			pathGeometry.Figures = new PathFigureCollection() { pathFigure };
			path.Data = pathGeometry;
			path.Stroke = new SolidColorBrush(Colors.White);
			path.StrokeThickness = stroke;
			//path.SnapsToDevicePixels = true;
			if (gridRoot != null)
			{
				gridRoot.Children.Add(path);
			}
			Grid.SetZIndex(path, -1);
			this.UseLayoutRounding = true;
		}
		#endregion

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
