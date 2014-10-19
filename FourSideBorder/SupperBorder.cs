using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace FourSideBorder
{
	class SupperBorder : Border
	{
		public Brush LeftBorderBrush
		{
			get { return (Brush)GetValue(LeftBorderBrushProperty); }
			set { SetValue(LeftBorderBrushProperty, value); }
		}


		public static readonly DependencyProperty LeftBorderBrushProperty =
			DependencyProperty.Register("LeftBorderBrush", typeof(Brush), typeof(SupperBorder), new PropertyMetadata(null));

		public Brush TopBorderBrush
		{
			get { return (Brush)GetValue(TopBorderBrushProperty); }
			set { SetValue(TopBorderBrushProperty, value); }
		}


		public static readonly DependencyProperty TopBorderBrushProperty =
			DependencyProperty.Register("TopBorderBrush", typeof(Brush), typeof(SupperBorder), new PropertyMetadata(null));

		public Brush RightBorderBrush
		{
			get { return (Brush)GetValue(RightBorderBrushProperty); }
			set { SetValue(RightBorderBrushProperty, value); }
		}


		public static readonly DependencyProperty RightBorderBrushProperty =
			DependencyProperty.Register("RightBorderBrush", typeof(Brush), typeof(SupperBorder), new PropertyMetadata(null));

		public Brush BottomBorderBrush
		{
			get { return (Brush)GetValue(BottomBorderBrushProperty); }
			set { SetValue(BottomBorderBrushProperty, value); }
		}


		public static readonly DependencyProperty BottomBorderBrushProperty =
			DependencyProperty.Register("BottomBorderBrush", typeof(Brush), typeof(SupperBorder), new PropertyMetadata(null));

		protected override void OnRender(System.Windows.Media.DrawingContext dc)
		{

			base.OnRender(dc);
			bool useLayoutRounding = base.UseLayoutRounding;

			Thickness borderThickness = this.BorderThickness;
			CornerRadius cornerRadius = this.CornerRadius;
			double topLeft = cornerRadius.TopLeft;
			bool flag = !DoubleUtil.IsZero(topLeft);
			Brush borderBrush = null;

			Pen pen = null;
			if (pen == null)
			{
				pen = new Pen();
				borderBrush = LeftBorderBrush;
				pen.Brush = LeftBorderBrush;
				if (useLayoutRounding)
				{
					pen.Thickness = UlementEx.RoundLayoutValue(borderThickness.Left, DoubleUtil.DpiScaleX);
				}
				else
				{
					pen.Thickness = borderThickness.Left;
				}
				if (borderBrush != null)
				{
					if (borderBrush.IsFrozen)
					{
						pen.Freeze();
					}
				}


				if (DoubleUtil.GreaterThan(borderThickness.Left, 0.0))
				{
					double num = pen.Thickness * 0.5;
					dc.DrawLine(pen, new Point(num, 0.0), new Point(num, base.RenderSize.Height));
				}
				if (DoubleUtil.GreaterThan(borderThickness.Right, 0.0))
				{

					pen = new Pen();
					pen.Brush = RightBorderBrush;
					if (useLayoutRounding)
					{
						pen.Thickness = UlementEx.RoundLayoutValue(borderThickness.Right, DoubleUtil.DpiScaleX);
					}
					else
					{
						pen.Thickness = borderThickness.Right;
					}
					if (borderBrush != null)
					{
						if (borderBrush.IsFrozen)
						{
							pen.Freeze();
						}
					}

					double num = pen.Thickness * 0.5;
					dc.DrawLine(pen, new Point(base.RenderSize.Width - num, 0.0), new Point(base.RenderSize.Width - num, base.RenderSize.Height));
				}
				if (DoubleUtil.GreaterThan(borderThickness.Top, 0.0))
				{


					pen = new Pen();
					pen.Brush = TopBorderBrush;
					if (useLayoutRounding)
					{
						pen.Thickness = UlementEx.RoundLayoutValue(borderThickness.Top, DoubleUtil.DpiScaleY);
					}
					else
					{
						pen.Thickness = borderThickness.Top;
					}
					if (borderBrush != null)
					{
						if (borderBrush.IsFrozen)
						{
							pen.Freeze();
						}
					}


					double num = pen.Thickness * 0.5;
					dc.DrawLine(pen, new Point(0.0, num), new Point(base.RenderSize.Width, num));
				}
				if (DoubleUtil.GreaterThan(borderThickness.Bottom, 0.0))
				{


					pen = new Pen();
					pen.Brush = BottomBorderBrush;
					if (useLayoutRounding)
					{
						pen.Thickness = UlementEx.RoundLayoutValue(borderThickness.Bottom, DoubleUtil.DpiScaleY);
					}
					else
					{
						pen.Thickness = borderThickness.Bottom;
					}
					if (borderBrush != null)
					{
						if (borderBrush.IsFrozen)
						{
							pen.Freeze();
						}
					}

					double num = pen.Thickness * 0.5;
					dc.DrawLine(pen, new Point(0.0, base.RenderSize.Height - num), new Point(base.RenderSize.Width, base.RenderSize.Height - num));
				}
			}
		}
	}
}
