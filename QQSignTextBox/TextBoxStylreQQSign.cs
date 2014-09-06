using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;

namespace QQSignTextBox
{
	public class TextBoxStylreQQSign : TextBox
	{
		CheckBox chbShowFace;
		ScrollViewer scrollViewerConent;
		TextBlock tblEllipsis;

		[Browsable(true), Category("Appearance")] 
		public double WidthLitmed { get; set; }

		public override void BeginInit()
		{
			base.BeginInit();
			this.Style = this.FindResource("TextBoxStyleQQSign") as Style;
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			chbShowFace = this.Template.FindName("chbShowFace", this) as CheckBox;
			tblEllipsis = this.Template.FindName("tblEllipsis", this) as TextBlock;
			scrollViewerConent = this.Template.FindName("PART_ContentHost", this) as ScrollViewer;
			//chbShowFace.Checked += new RoutedEventHandler(chbShowFace_Checked);
		}

		void chbShowFace_Checked(object sender, RoutedEventArgs e)
		{
			//if (MessageBox.Show("请选择表情") == MessageBoxResult.OK)
			//{
			//    chbShowFace.IsChecked = false;
			//}
		}

		protected override void OnGotFocus(RoutedEventArgs e)
		{
			base.OnGotFocus(e);
			tblEllipsis.Visibility = System.Windows.Visibility.Collapsed;
			this.SelectAll();
		}

		protected override void OnLostFocus(RoutedEventArgs e)
		{
			base.OnLostFocus(e);
			if (scrollViewerConent.ActualWidth >= WidthLitmed)
			{
				tblEllipsis.Visibility = System.Windows.Visibility.Visible;
			}
			else
			{
				tblEllipsis.Visibility = System.Windows.Visibility.Collapsed;
			}
		}

	}
}
