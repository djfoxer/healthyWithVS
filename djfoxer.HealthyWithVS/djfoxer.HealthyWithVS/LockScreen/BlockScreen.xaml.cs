using System;
using System.Collections.Generic;
using System.Linq;
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

namespace djfoxer.HealthyWithVS.LockScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BlockScreen : Window
    {
        Timer timer = null;
        int imageCounter = 1;

        public BlockScreen()
        {
            InitializeComponent();
            timer = new Timer(5000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            imageCounter++;
            if (imageCounter == 5)
            {
                timer.Stop();
                this.Dispatcher.Invoke(() =>
                {
                    this.Close();
                });
                imageCounter = 0;
            }
            else
            {
                this.Dispatcher.Invoke(() =>
                {
                    WorkoutImage.Source = new BitmapImage(new Uri($"/djfoxer.HealthyWithVS;component/Resources/workout{imageCounter}.png", UriKind.Relative));
                });
            }
        }

        protected void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        
        }
    }
}
