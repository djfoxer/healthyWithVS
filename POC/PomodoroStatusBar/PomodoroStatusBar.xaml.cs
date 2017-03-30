using PomodoroStatusBar.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PomodoroStatusBar
{
    /// <summary>
    /// Interaction logic for PomodoroStatusBar.xaml
    /// </summary>
    public partial class PomodoroStatusBar : UserControl
    {
        Timer disTimer = new Timer(1000);
        uint seconds = 0;
        uint maxSeconds = 1500;

        public PomodoroStatusBar()
        {
            InitializeComponent();
            disTimer.Elapsed += DisTimer_Elapsed;
            seconds = maxSeconds;
            SetTimerText();
            Play.Visibility = Visibility.Visible;
            Stop.Visibility = Visibility.Visible;
            Pause.Visibility = Visibility.Hidden;
        }

        private void DisTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (seconds <= 0)
            {
                disTimer.Stop();
                SoundPlayer player = new SoundPlayer(Resource.alarm);
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
