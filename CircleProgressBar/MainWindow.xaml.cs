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
using System.Timers;

namespace CircleProgressBar
{
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window
	{
		Timer timer;

		public MainWindow()
		{
			InitializeComponent();
			timer = new Timer();
			timer.Interval = 10;
			timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
		}

		void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			this.Dispatcher.Invoke(new Action(() =>
			{
				pbMain.Value += 1;
				pbMain_Copy.Value += 1;
			}));
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			timer.Start();
		}
	}
}
