using djfoxer.HealthyWithVS.Helpers;
using djfoxer.HealthyWithVS.LockScreen;
using djfoxer.HealthyWithVS.Resources;
using System;
using System.Media;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace djfoxer.HealthyWithVS.StatusBar
{
    /// <summary>
    /// Interaction logic for TomatoStatusBar.xaml
    /// </summary>
    public partial class TomatoStatusBar : System.Windows.Controls.UserControl
    {
        System.Timers.Timer disTimer = new System.Timers.Timer(1000);
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
                Workout();
                System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
                {
                    StopClick();
                });

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

        private void StopClick()
        {
            seconds = maxSeconds;
            SetTimerText();
            disTimer.Stop();
            Play.Visibility = Visibility.Visible;
            Stop.Visibility = Visibility.Visible;
            Pause.Visibility = Visibility.Hidden;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            StopClick();
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

        private void Workout()
        {



            System.Windows.Application.Current.Dispatcher.Invoke((Action)delegate
            {
                foreach (var s in System.Windows.Forms.Screen.AllScreens)
                {
                    var blockScreen = new BlockScreen();
                    blockScreen.Top = s.Bounds.Top;
                    blockScreen.Left = s.Bounds.Left;
                    blockScreen.Width = s.Bounds.Width;
                    blockScreen.Height = s.Bounds.Height;
                    blockScreen.Show();
                }
            });



            //foreach (Window item in System.Windows.Application.Current.Windows)
            //{
            //    item.Closing += Item_Closing;
            //    item.Closed += Item_Closed;
            //}


            //System.Windows.Forms.Application.AddMessageFilter(new AltF4Filter());
        }

        private void Item_Closed(object sender, System.EventArgs e)
        {

        }

        private void Item_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;


        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }
    }

    public class AltF4Filter : IMessageFilter
    {
        public bool PreFilterMessage(ref Message m)
        {
            const int WM_SYSKEYDOWN = 0x0104;
            if (m.Msg == WM_SYSKEYDOWN)
            {
                bool alt = ((int)m.LParam & 0x20000000) != 0;
                if (alt && (m.WParam == new System.IntPtr((int)Keys.F4)))
                    return true; // eat it!                
            }
            return false;
        }
    }
}
