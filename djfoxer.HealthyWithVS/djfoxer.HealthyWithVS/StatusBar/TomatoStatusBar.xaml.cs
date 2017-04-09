using djfoxer.HealthyWithVS.Helpers;
using djfoxer.HealthyWithVS.Resources;
using System.Media;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace djfoxer.HealthyWithVS.StatusBar
{
    /// <summary>
    /// Interaction logic for TomatoStatusBar.xaml
    /// </summary>
    public partial class TomatoStatusBar : UserControl
    {
        Timer disTimer = new Timer(1000);
        uint seconds = 0;
        uint maxSeconds = 1500;

        public TomatoStatusBar()
        {
            InitializeComponent();
            disTimer.Elapsed += DisTimer_Elapsed;
            seconds = maxSeconds;
            SetTimerText();
            Play.Visibility = Visibility.Visible;
            Stop.Visibility = Visibility.Visible;
            Pause.Visibility = Visibility.Hidden;

            Name = Consts.HealthyWithVS_Element_PomodoroTimer;
        }

        private void DisTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (seconds <= 0)
            {
                disTimer.Stop();
                SoundPlayer player = new SoundPlayer(MainResource.alarm);
                player.Load();
                player.Play();
            }
            else
            {
                seconds--;
            }
            SetTimerText();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            disTimer.Start();
            Play.Visibility = Visibility.Hidden;
            Stop.Visibility = Visibility.Visible;
            Pause.Visibility = Visibility.Visible;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {

            seconds = maxSeconds;
            SetTimerText();
            disTimer.Stop();
            Play.Visibility = Visibility.Visible;
            Stop.Visibility = Visibility.Visible;
            Pause.Visibility = Visibility.Hidden;
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            disTimer.Stop();
            Play.Visibility = Visibility.Visible;
            Stop.Visibility = Visibility.Visible;
            Pause.Visibility = Visibility.Hidden;
        }

        private void SetTimerText()
        {
            this.Dispatcher.Invoke(() =>
            {
                SecondsText.Content = ((seconds - (seconds % 60)) / 60).ToString("D2") + " : " + (seconds % 60).ToString("D2");

            });
        }
    }
}
