using System;
using System.Configuration;
using System.Windows;
using System.Windows.Media;
using Prodigy.Properties;
using Reading.Core;
using Reading.Core.Speach;
using Reading.Speach;
using Savchin.Development;
using Savchin.Logging;
using Savchin.Wpf.Controls.Core;
using Savchin.Wpf.Controls.Localization;

namespace Prodigy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        #region Properties


        internal Settings Settings { get; private set; }
        internal ILogger Logger { get; private set; }
        internal ISpeaker Speaker { get; private set; }

        public static App CurrentApp => (App)Current;

        #endregion


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);



            Logger = new LoggerLog4Net("App");
            var engine = ConfigurationManager.AppSettings["SpeakEngine"];
            Speaker = string.IsNullOrWhiteSpace(engine) || engine.ToUpper() == "COM" ? (ISpeaker)new ComSpeaker() : new FrameworkSpeaker();
            Settings = Settings.Default;


            AppContext.CurrentApp = new ReadingContext(new SingleThreadProvider());
            ReadingContext.Current.Logger = Logger;
            ReadingContext.Current.Speaker = Speaker;

            TranslationManager.Instance.TranslationProvider = new ResxTranslationProvider("Reading.Properties.Resources", GetType().Assembly);
            try
            {
                ApplySettings();
            }
            catch (Exception ex)
            {
                ReadingContext.Current.Logger.AddMessage(Severity.Error, "Error load settings", ex);
            }
        }


        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                Logger?.AddMessage(Severity.FatalError, "Unhadled Exception", e.Exception);

                ErrorForm.Show(e, "Сбой приложения");

            }
            catch (Exception)
            {
                Logger?.AddMessage(Severity.FatalError, "Fail handle exception", e.Exception);
            }

        }

        public void ApplySettings()
        {
            try
            {
                if (!string.IsNullOrEmpty(Settings.Voice))
                    Speaker.Voice = Settings.Voice;
                Speaker.Volume = Settings.VoiceVolume;
                Speaker.Rate = Settings.VoiceRate;
                Speaker.IsEnabled = Settings.VoiceEnabled;
            }
            catch (Exception ex)
            {
                Logger.AddMessage(Severity.Warning, "Error set voice", ex);
            }

            Resources["SyllableFontFamily"] = new FontFamily(Settings.FontFamily);
            Resources["SyllableFontSize"] = Settings.FontSize;
        }

    }
}
