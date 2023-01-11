using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SIMS_Projekat_Bolnica_Zdravo.PatientWindows
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        public String notificationTitle { get; set; }
        public String notificationContent { get; set; }
        private DoubleAnimation anim { get; set; }
        public NotificationWindow(string notificationTitle, string notificationContent)
        {
            Loaded += scaleUp;
            anim = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut },
            };
            this.notificationContent = notificationContent;
            this.notificationTitle = notificationTitle;
            this.Top = 140;
            this.Left = 559;
            this.DataContext = this;
            InitializeComponent();
            CloseNotification();
        }
        private async void CloseNotification()
        {
            await Task.Delay(2000);
            anim = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut },
            };
            RenderTransform = new ScaleTransform(1, 1, ActualWidth / 2, 0);
            anim.To = 0;
            RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
        }
        void scaleUp(object sender, RoutedEventArgs e)
        {
            RenderTransform = new ScaleTransform(1, 1, ActualWidth / 2, 0);
            anim.From = 0;
            RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
