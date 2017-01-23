using System.Collections.Generic;
using System.Windows.Input;
using Windows.UI.Xaml.Media;
using CITrader.Controls.Commands;
using Reading.Core;
using Wunderkind.Core;


namespace Reading.Models
{
   public  class SettingsModel : SpeakModel
    {
        #region Settings


        /// <summary>
        /// Gets or sets the voices.
        /// </summary>
        /// <value>The voices.</value>
        public string[] Voices { get; private set; }
        /// <summary>
        /// Gets or sets the voice.
        /// </summary>
        /// <value>
        /// The voice.
        /// </value>
        public string Voice { get; set; }
        /// <summary>
        /// Gets or sets the voice volume.
        /// </summary>
        /// <value>
        /// The voice volume.
        /// </value>
        public int VoiceVolume { get; set; }

        public bool isVoiceEnabled { get; set; }
        public int VoiceRate { get; set; }
        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        /// <value>
        /// The size of the font.
        /// </value>
        public double FontSize { get; set; }

        public FontFamily SelectedFont { get; set; }
        /// <summary>
        /// Gets the save command.
        /// </summary>
        public ICommand SaveCommand { get; private set; }

        /// <summary>
        /// Gets the test sound command.
        /// </summary>
        public ICommand TestSoundCommand { get; private set; }

        public ICollection<FontFamily> FontFamilies { get; private set; }

        public override string Title
        {
            get { return "Настройки"; }
        }


        #endregion

        public SettingsModel() 
        {
          
            Voices =Speaker.GetVoices() ;
            FontFamilies = Fonts.SystemFontFamilies;

            SaveCommand = new DelegateCommandEx(OnSaveCommandExecute);
            TestSoundCommand = new DelegateCommandEx(OnTestSoundCommandExecute);
        }



        protected override void Initialize(Settings settings)
        {
            base.Initialize(settings);
            isVoiceEnabled = settings.VoiceEnabled;
            Voice = settings.Voice;
            VoiceVolume = settings.VoiceVolume;
            VoiceRate = settings.VoiceRate;
            FontSize = settings.FontSize;
            SelectedFont = new FontFamily(settings.FontFamily);
        }

        private void OnTestSoundCommandExecute()
        {
            if(Speaker==null) return;

            var voice = Speaker.Voice;
            var volume = Speaker.Volume;
            var rate = Speaker.Rate;
            try
            {
                Speaker.IsEnabled = isVoiceEnabled;
                Speaker.Voice = Voice;
                Speaker.Volume = VoiceVolume;
                Speaker.Rate = VoiceRate;
                Speaker.Speak("паравоз");
            }
            finally
            {
                Speaker.IsEnabled = isVoiceEnabled;
                Speaker.Voice = voice;
                Speaker.Volume = volume;
                Speaker.Rate = rate;
            }
        }

        private void OnSaveCommandExecute()
        {
            var settings = Settings.Default;
            settings.VoiceEnabled = isVoiceEnabled;
            settings.Voice = Voice;
            settings.VoiceVolume = VoiceVolume;
            settings.VoiceRate = VoiceRate;
            settings.FontSize = FontSize;
            settings.FontFamily = SelectedFont.Source;

          //  settings.Save();

           // App.CurrentApp.ApplySettings();
        }
    }
}
