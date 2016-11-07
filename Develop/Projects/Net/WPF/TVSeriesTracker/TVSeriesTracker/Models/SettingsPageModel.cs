using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Savchin.Core;
using Savchin.Wpf.Controls.Localization;
using Savchin.Wpf.Input;
using TVSeriesTracker.Properties;


namespace TVSeriesTracker.Models
{
    class SettingsPageModel : ModelBase
    {
        #region Properties
        public string DbPath { get; set; }
        public int EpisdeNotifierDelta { get; set; }
        public string Language { get; set; }
        public bool CloseToTray { get; set; }
        public bool RunOnStartup { get; set; }
        public int CheckInterval { get; set; }

        public ICommand ApplyCommand { get; set; }
        public string[] Languages { get; private set; } 
        #endregion

        public SettingsPageModel()
        {
            ApplyCommand = new DelegateCommand(OnApplyCommand);
            Languages= new string[]{"en-US", "ru-RU"};

            var set = Settings.Default;
            DbPath = set.DbPath;
            EpisdeNotifierDelta = set.EpisdeNotifierDelta;
            Language = set.Language;
            CloseToTray = set.CloseToTray;
            RunOnStartup = set.RunOnStartup;
            CheckInterval = set.CheckInterval;

        }

        private void OnApplyCommand()
        {
            if (!string.IsNullOrWhiteSpace(DbPath) && !File.Exists(DbPath))
            {
                MessageBox.Show("Invalid Db Path");
                return;
            }

            var set = Settings.Default;

            set.EpisdeNotifierDelta = EpisdeNotifierDelta;
            set.DbPath = DbPath;
            set.Language = Language;
            set.CloseToTray = CloseToTray;
            set.RunOnStartup = RunOnStartup;
            set.CheckInterval = CheckInterval;

            set.Save();

            SetRegistry(set);
        }

        private void SetRegistry(Settings set)
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            var node = AppInfo.ProductLabel;
            if (set.RunOnStartup)
            {
                rkApp.SetValue(node, AppInfo.ApplicationExePath+ " autorun");
            }
            else
            {
                rkApp.DeleteValue(node, false);
            }
            TranslationManager.Instance.CurrentLanguage = new CultureInfo(Language);
        }
    }
}
