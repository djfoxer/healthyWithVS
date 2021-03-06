﻿using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Threading;
using System.ComponentModel;
using djfoxer.HealthyWithVS.Resources;
using djfoxer.HealthyWithVS.Helpers;
using djfoxer.HealthyWithVS.Services;

namespace djfoxer.HealthyWithVS.Options
{
    public class OptionPage : DialogPage
    {
        public OptionPage()
        {
            AutostartPomodoroStatusBar = true;
            WorkoutActive = true;
            WorkTimerSeconds = 25;
        }

        private bool _AutostartPomodoroStatusBar { get; set; }
        [Category(Consts.OptionsCategoryBasicName)]
        [DisplayName(Consts.OptionsCategoryBasicStatusBarAutostartText)]
        [Description(Consts.OptionsCategoryBasicStatusBarAutostartInfoText)]
        public bool AutostartPomodoroStatusBar
        {
            get { return _AutostartPomodoroStatusBar; }
            set
            {
                _AutostartPomodoroStatusBar = value;
                HealthyWithVSSettingsService.Instance.AutostartPomodoroStatusBar = value;
            }
        }

        private bool _WorkoutActive { get; set; }
        [Category(Consts.OptionsCategoryBasicName)]
        [DisplayName(Consts.OptionsCategoryBasicWorkoutActiveText)]
        [Description(Consts.OptionsCategoryBasicWorkoutActiveInfoText)]
        public bool WorkoutActive
        {
            get { return _WorkoutActive; }
            set
            {
                _WorkoutActive = value;
                HealthyWithVSSettingsService.Instance.WorkoutActive = value;
            }
        }

        private int _WorkTimerSeconds { get; set; }
        [Category(Consts.OptionsCategoryBasicName)]
        [DisplayName(Consts.OptionsCategoryBasicWorkTimeText)]
        [Description(Consts.OptionsCategoryBasicWorkTimeInfoText)]
        public int WorkTimerSeconds
        {
            get { return _WorkTimerSeconds; }
            set
            {
                if (value > 0)
                {
                    _WorkTimerSeconds = value;
                    HealthyWithVSSettingsService.Instance.WorkTimerSeconds = value;
                }
            }
        }

        public OptionPage(JoinableTaskContext joinableTaskContext) : base(joinableTaskContext)
        {

        }


    }
}
