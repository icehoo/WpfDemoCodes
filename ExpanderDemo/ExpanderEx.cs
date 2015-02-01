using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Animation;
using System.ComponentModel;

namespace ExpanderDemo
{
	public class ExpanderEx : Expander
	{
		#region 变量
		ContentPresenter cpExpandSite;
		Grid gridRoot;
		double totalHeight;
		Storyboard sbCollapsed;
		Storyboard sbExpanded;
		Duration dur = TimeSpan.FromSeconds(0.2);
		#endregion

		#region 构造函数
		public ExpanderEx()
		{
			Style style = this.FindResource("ExpanderStyleHeight") as Style;
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
			cpExpandSite = this.Template.FindName("ExpandSite", this) as ContentPresenter;
			cpExpandSite.Visibility = System.Windows.Visibility.Visible;
			cpExpandSite.Height = 0;
			gridRoot = this.Content as Grid;
			if (gridRoot != null)
			{
				foreach (UserControl item in ((StackPanel)gridRoot.Children[0]).Children)
				{
					totalHeight += item.Height;
				}
			}

			if (!DesignerProperties.GetIsInDesignMode(this))
			{
				InitStoryboard();
			}
		}

		private void InitStoryboard()
		{
			BackEase be = new BackEase();
			be.EasingMode = EasingMode.EaseOut;

			DoubleAnimation daCollapsed = new DoubleAnimation(0, dur);
			Storyboard.SetTarget(daCollapsed, cpExpandSite);
			Storyboard.SetTargetProperty(daCollapsed, new PropertyPath(Control.HeightProperty));
			sbCollapsed = new Storyboard();
			sbCollapsed.Children.Add(daCollapsed);

			DoubleAnimation daExpanded = new DoubleAnimation(totalHeight, dur);
			//daExpanded.EasingFunction = be;
			Storyboard.SetTarget(daExpanded, cpExpandSite);
			Storyboard.SetTargetProperty(daExpanded, new PropertyPath(Control.HeightProperty));
			sbExpanded = new Storyboard();
			sbExpanded.Children.Add(daExpanded);
		}

		protected override void OnCollapsed()
		{
			if (!DesignerProperties.GetIsInDesignMode(this))
			{
				sbCollapsed.Begin();
			}
		}

		protected override void OnExpanded()
		{
			if (!DesignerProperties.GetIsInDesignMode(this))
			{
				sbExpanded.Begin();
			}
		}
		#endregion
	}
}