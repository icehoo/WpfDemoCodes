using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace CircleProgressBar
{
	/// <summary>
	/// 作者：xxh 
	/// 时间：2014-10-08 13:30:07
	/// 版本：V1.0.0 	 
	/// </summary>
	public class CircleProgressBarStyle : ProgressBar
	{
		TextBlock tblPercent;

		public CircleProgressBarStyle()
		{
			this.Style = this.FindResource("ProgressBarStyleCircle") as Style;
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			tblPercent = this.Template.FindName("tblPercent", this) as TextBlock;
		}

		protected override void OnValueChanged(double oldValue, double newValue)
		{
			base.OnValueChanged(oldValue, newValue);
			tblPercent.Text = string.Format("{0}%", Math.Round(Value / Maximum, 2) * 100);
		}
	}
}
