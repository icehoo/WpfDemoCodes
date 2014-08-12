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

namespace SliderTest {
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
		}
	}
	public class Group : List<User> {
		public Group() {
            for (int i = 0; i < 1000;i++ )
            {
                this.Add(new User { Name="User"+i.ToString()});
            }
			this.Add(new User { Name = "User1" });
			this.Add(new User { Name = "User2" });
			this.Add(new User { Name = "User3" });
			this.Add(new User { Name = "User4" });
			this.Add(new User { Name = "User5" });
			this.Add(new User { Name = "User6" });
			this.Add(new User { Name = "User7" });
			this.Add(new User { Name = "User8" });
			this.Add(new User { Name = "User9" });
			this.Add(new User { Name = "User10" });
			this.Add(new User { Name = "User11" });
			this.Add(new User { Name = "User12" });
			this.Add(new User { Name = "User13" });
			this.Add(new User { Name = "User14" });
			this.Add(new User { Name = "User15" });
			this.Add(new User { Name = "User16" });
			this.Add(new User { Name = "User17" });
			this.Add(new User { Name = "User18" });
		}
	}
	public class User {
		public string Name { get; set; }
	}
}

