//------------------------------------------------------------------------------
// <copyright file="TimerToolWindowControl.xaml.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace VSTimer
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Timers;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for TimerToolWindowControl.
    /// </summary>
    public partial class TimerToolWindowControl : UserControl
    {
        DispatcherTimer disTimer = new DispatcherTimer();
        uint seconds = 0;
        uint maxSeconds = 1500;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerToolWindowControl"/> class.
        /// </summary>
        public TimerToolWindowControl()
        {
            this.InitializeComponent();
            DataContext = this;
            disTimer.Interval = TimeSpan.FromSeconds(1);
            disTimer.Tick += DisTimer_Tick;
            seconds = maxSeconds;
            BackgroundColor = Brushes.Black;
            SetTimerText();
        }

        private void DisTimer_Tick(object sender, System.EventArgs e)
        {
            if (seconds <= 0)
            {
                disTimer.Stop();
                BackgroundColor = Brushes.Red;
            }
            else
            {
                seconds--;
            }
            SetTimerText();
        }

        private void Button_Click_start(object sender, RoutedEventArgs e)
        {
            disTimer.Start();
            BackgroundColor = Brushes.Black;
        }

        private void Button_Click_stop(object sender, RoutedEventArgs e)
        {
            disTimer.Stop();
        }

        private void Button_Click_clear(object sender, RoutedEventArgs e)
        {
            seconds = maxSeconds;
            SetTimerText();
            disTimer.Stop();
            BackgroundColor = Brushes.Black;
        }

        private void SetTimerText()
        {
            SecondsText = ((seconds - (seconds % 60)) / 60).ToString("D2") + " : " + (seconds % 60).ToString("D2");
        }


        public SolidColorBrush BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(SolidColorBrush), typeof(TimerToolWindowControl), new PropertyMetadata(Brushes.Transparent));



        public string SecondsText
        {
            get { return (string)GetValue(SecondsTextProperty); }
            set { SetValue(SecondsTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SecondsText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SecondsTextProperty =
            DependencyProperty.Register("SecondsText", typeof(string), typeof(TimerToolWindowControl), new PropertyMetadata(string.Empty));

    }
}