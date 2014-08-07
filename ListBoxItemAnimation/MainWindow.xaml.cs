using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Media.Animation;

namespace ListBoxItemAnimation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Person> CollectionPerson = new ObservableCollection<Person>();
        ObservableCollection<Person> CollectionPersonSource = new ObservableCollection<Person>();
        Timer timerLoad = new Timer();
        TimeSpan tsUnload = TimeSpan.FromSeconds(0.1);
        int itemCount = 0;
        int RenderFlag = 0;//RenderTransform注册名

        public MainWindow()
        {
            this.InitializeComponent();

            // 在此点下面插入创建对象所需的代码。
            LoadListBoxSource();
            listBoxMain.ItemsSource = CollectionPersonSource;
        }

        private void LoadListBoxSource()
        {
            CollectionPerson.Add(new Person { Name = "Tom" });
            CollectionPerson.Add(new Person { Name = "Jim" });
            CollectionPerson.Add(new Person { Name = "Neal" });
            CollectionPerson.Add(new Person { Name = "Mitchell" });
            CollectionPerson.Add(new Person { Name = "David" });
            CollectionPerson.Add(new Person { Name = "Jack" });
            CollectionPerson.Add(new Person { Name = "Lina" });
            CollectionPerson.Add(new Person { Name = "Sykes" });
            CollectionPerson.Add(new Person { Name = "Kennedy" });
            CollectionPerson.Add(new Person { Name = "Potter" });

            timerLoad.Interval = 150;
            timerLoad.Elapsed += new ElapsedEventHandler(timerLoad_Elapsed);
        }

        void timerLoad_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (itemCount < CollectionPerson.Count)
            {
                Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() =>
                {
                    CollectionPersonSource.Add(CollectionPerson[itemCount++]);
                }));
            }
            else
            {
                itemCount = 0;
                timerLoad.Stop();
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            timerLoad.Start();
        }

        private void btnUnLoad_Click(object sender, RoutedEventArgs e)
        {
            tsUnload = TimeSpan.FromSeconds(0);
            for (int i = 0; i < listBoxMain.Items.Count; i++)
            {
                UnLoadAnimationSource(i);
            }
        }

        private void UnLoadAnimationSource(int index)
        {
            Storyboard sbTemp = new Storyboard();
            sbTemp.AccelerationRatio = 0.7;
            sbTemp.DecelerationRatio = 0.3;
            sbTemp.Completed += (o, s) =>
            {
                if (index == listBoxMain.Items.Count - 1)
                {
                    CollectionPersonSource.Clear();
                }
            };
            var g = listBoxMain.ItemContainerGenerator;
            ListBoxItem item = g.ContainerFromIndex(index) as ListBoxItem;
            if (item == null)
            {
                return;
            }
            TranslateTransform translate = new TranslateTransform();
            item.RenderTransform = translate;

            string RenderName = string.Format("translate{0}", RenderFlag);
            RenderFlag++;
            this.RegisterName(RenderName, translate);
            Duration duration = new Duration(TimeSpan.FromSeconds(0.3));
            DoubleAnimation daMargin = new DoubleAnimation(-205, duration);
            daMargin.BeginTime = tsUnload;
            tsUnload += TimeSpan.FromSeconds(0.1);
            Storyboard.SetTargetProperty(daMargin, new PropertyPath(TranslateTransform.XProperty));
            Storyboard.SetTargetName(daMargin, RenderName);
            sbTemp.Children.Add(daMargin);

            sbTemp.Begin(this);
        }
    }

    public class Person
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
    }
}