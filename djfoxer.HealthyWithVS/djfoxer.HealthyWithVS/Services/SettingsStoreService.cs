using djfoxer.HealthyWithVS.Options;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace djfoxer.HealthyWithVS.Services
{
    public class SettingStore
    {
        public SettingsManager SettingsManager { get; set; }
        public SettingsStore SettingsStoreReadOnly { get; set; }

        public SettingStore(IServiceProvider serviceProvider)
        {
            SettingsManager = new ShellSettingsManager(serviceProvider);
            SettingsStoreReadOnly = SettingsManager.GetReadOnlySettingsStore(SettingsScope.UserSettings);
        }

        public bool GetOptionBool(string collectionPath, string propertyName)
        {
            return SettingsStoreReadOnly.GetBoolean(collectionPath, propertyName, true);
        }
    }
}
