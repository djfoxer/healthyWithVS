using djfoxer.HealthyWithVS.Helpers;
using djfoxer.HealthyWithVS.StatusBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace djfoxer.HealthyWithVS.Services
{
    public class UIService
    {
        private UIService()
        {

        }

        public void TogglePomodoroTimerStatusBar()
        {
            var statusBarObj = UIHelper.FindChildControl<DockPanel>(Application.Current.MainWindow, Consts.VisualStudioStatusBarName);
            if ((statusBarObj.Children[0] is FrameworkElement frameworkElement && frameworkElement.Name == Consts.HealthyWithVS_Element_PomodoroTimer))
            {
                statusBarObj.Children.RemoveAt(0);
            }
            else
            {
                statusBarObj.Children.Insert(0, new TomatoStatusBar());
            }
        }

        public bool IsPomodoroTimerStatusBarVisible()
        {
            var statusBarObj = UIHelper.FindChildControl<DockPanel>(Application.Current.MainWindow, Consts.VisualStudioStatusBarName);
            return (statusBarObj.Children[0] is FrameworkElement frameworkElement && frameworkElement.Name == Consts.HealthyWithVS_Element_PomodoroTimer);
        }

        private static UIService _Instance { get; set; }

        public static UIService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new UIService();
                }
                return _Instance;

            }
        }
    }
}
