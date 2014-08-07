using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Timers;

namespace WpfClockDemo
{
    /// <summary>
    /// WinClock.xaml 的交互逻辑
    /// </summary>
    public partial class WinClock : Window
    {
        #region 变量
        double screenWorkWidth;
        double screenWorkHeight;
        RotateTransform rtSecond;
        #endregion

        #region 委托

        #endregion

        #region 属性
        public double MaxLeft
        {
            get
            {
                return screenWorkWidth - this.Width;
            }
        }

        public double MaxTop
        {
            get
            {
                return screenWorkHeight - this.Height;
            }

        }

        public DateTime dtNow
        {
            get
            {
                return DateTime.Now;
            }
        }
        public bool IsMechanical { get; set; }
        #endregion

        #region 枚举

        #endregion

        #region 构造函数
        public WinClock()
        {
            this.InitializeComponent();

            // 在此点之下插入创建对象所需的代码。
            NameScope.SetNameScope(this, new NameScope());
        }

        public WinClock(bool isMechanical)
            : this()
        {
            IsMechanical = isMechanical;
        }
        #endregion

        #region 业务
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime now = dtNow;
            if (IsMechanical)
            {
                StartAnimationSecond(now);
            }
            else
            {
                StartTimerSecond(now);

            }
            StartAnimationMinute(now);
            StartAnimationHour(now);


            screenWorkWidth = SystemParameters.WorkArea.Width;
            screenWorkHeight = SystemParameters.WorkArea.Height;
            this.Left = MaxLeft;
            this.Top = 30;
        }

        private void StartAnimationSecond(DateTime now)
        {
            Storyboard stS = new Storyboard();
            stS.RepeatBehavior = RepeatBehavior.Forever;

            DoubleAnimationUsingKeyFrames daSecond = new DoubleAnimationUsingKeyFrames();
            daSecond.Duration = new Duration(TimeSpan.FromMinutes(1));

            LinearDoubleKeyFrame keySecond = new LinearDoubleKeyFrame();
            //keySecond.Value = 360;
            daSecond.KeyFrames.Add(keySecond);

            double second = dtNow.Second;
            double angleSecond = 360 * ((double)now.Second / 60);
            keySecond.Value = angleSecond + 360;

            RotateTransform rtSecond = ((TransformGroup)pathSecond.RenderTransform).Children[2] as RotateTransform;
            rtSecond.Angle = angleSecond;
            this.RegisterName("RtSecond", rtSecond);

            Storyboard.SetTargetName(daSecond, "RtSecond");
            Storyboard.SetTargetProperty(daSecond, new PropertyPath(RotateTransform.AngleProperty));
            stS.Children.Add(daSecond);
            stS.Begin(this);
        }

        private void StartTimerSecond(DateTime now)
        {
            double angleSecond = 360 * ((double)now.Second / 60);
            rtSecond = ((TransformGroup)pathSecond.RenderTransform).Children[2] as RotateTransform;
            rtSecond.Angle = angleSecond;

            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Send, new Action(() =>
            {
                rtSecond.Angle += 6;
                if (rtSecond.Angle >= 360)
                {
                    rtSecond.Angle = 0;
                }
            }));
        }

        private void StartAnimationMinute(DateTime now)
        {
            Storyboard stM = new Storyboard();
            stM.RepeatBehavior = RepeatBehavior.Forever;

            DoubleAnimationUsingKeyFrames daMinute = new DoubleAnimationUsingKeyFrames();
            daMinute.Duration = new Duration(TimeSpan.FromHours(1));

            LinearDoubleKeyFrame keyMinute = new LinearDoubleKeyFrame();
            //keyMinute.Value = 360;
            daMinute.KeyFrames.Add(keyMinute);

            double minute = now.Minute;
            double angleMinute = 360 * ((minute + (now.Second / 60)) / 60);
            keyMinute.Value = angleMinute + 360;

            RotateTransform rtMinute = ((TransformGroup)pathMinute.RenderTransform).Children[2] as RotateTransform;
            rtMinute.Angle = angleMinute;
            this.RegisterName("RtMinute", rtMinute);

            Storyboard.SetTargetName(daMinute, "RtMinute");
            Storyboard.SetTargetProperty(daMinute, new PropertyPath(RotateTransform.AngleProperty));
            stM.Children.Add(daMinute);
            stM.Begin(this);
        }

        private void StartAnimationHour(DateTime now)
        {
            Storyboard stH = new Storyboard();
            stH.RepeatBehavior = RepeatBehavior.Forever;

            DoubleAnimationUsingKeyFrames daHour = new DoubleAnimationUsingKeyFrames();
            daHour.Duration = new Duration(TimeSpan.FromHours(12));

            LinearDoubleKeyFrame keyHour = new LinearDoubleKeyFrame();
            //keyHour.Value = 360;
            daHour.KeyFrames.Add(keyHour);

            double hour = now.Hour;
            if (hour >= 12)
            {
                hour = hour - 12;
            }
            double angleHour = 360 * ((hour + (double)now.Minute / 60 + (double)now.Second / 3600) / 12);
            keyHour.Value = angleHour + 360;

            RotateTransform rtHour = ((TransformGroup)pathHour.RenderTransform).Children[2] as RotateTransform;
            rtHour.Angle = angleHour;
            this.RegisterName("RtHour", rtHour);
            Storyboard.SetTargetName(daHour, "RtHour");
            Storyboard.SetTargetProperty(daHour, new PropertyPath(RotateTransform.AngleProperty));
            stH.Children.Add(daHour);
            stH.Begin(this);
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            btnClose.Visibility = System.Windows.Visibility.Visible;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            btnClose.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            double left = this.Left;
            double top = this.Top;

            if (left < 0)
            {
                this.Left = 0;
            }
            if (left > MaxLeft)
            {
                this.Left = MaxLeft;
            }
            if (top < 0)
            {
                this.Top = 0;
            }
            if (top > MaxTop)
            {
                this.Top = MaxTop;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown();
        }
        #endregion
    }
}