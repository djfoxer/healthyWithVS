using djfoxer.HealthyWithVS.Helpers;
using djfoxer.HealthyWithVS.StatusBar;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace djfoxer.HealthyWithVS.Services
{
    public class MainService
    {
        private MainService()
        {

        }

        public void TogglePomodoroTimerStatusBar(bool forceEnabled)
        {
            var statusBarObj = UIHelper.FindChildControl<DockPanel>(Application.Current.MainWindow, Consts.VisualStudioStatusBarName);
            if (statusBarObj != null)
            {
                if (statusBarObj.Children.Count > 0 && (statusBarObj.Children[0] is FrameworkElement frameworkElement && frameworkElement.Name == Consts.HealthyWithVS_Element_PomodoroTimer))
                {
                    if (!forceEnabled)
                    {
                        statusBarObj.Children.RemoveAt(0);
                        HealthyWithVSSettingsService.Instance.TomatoStatusBarInstance = null;
                    }
                }
                else
                {
                    HealthyWithVSSettingsService.Instance.TomatoStatusBarInstance = new TomatoStatusBar();
                    statusBarObj.Children.Insert(0, HealthyWithVSSettingsService.Instance.TomatoStatusBarInstance);
                }
            }
        }

        public bool IsPomodoroTimerStatusBarVisible()
        {
            var statusBarObj = UIHelper.FindChildControl<DockPanel>(Application.Current.MainWindow, Consts.VisualStudioStatusBarName);
            return (statusBarObj.Children[0] is FrameworkElement frameworkElement && frameworkElement.Name == Consts.HealthyWithVS_Element_PomodoroTimer);
        }

        public void WriteToActivityLog(IServiceProvider serviceProvider, string message, __ACTIVITYLOG_ENTRYTYPE logWarning)
        {
            var log = serviceProvider.GetService(typeof(SVsActivityLog)) as IVsActivityLog;
            if (log != null)
            {
                log.LogEntry((UInt32)logWarning, Consts.PluginName, message);
            }
        }

        private static MainService _Instance { get; set; }

        public static MainService Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new MainService();
                }
                return _Instance;

            }
        }
    }
}
