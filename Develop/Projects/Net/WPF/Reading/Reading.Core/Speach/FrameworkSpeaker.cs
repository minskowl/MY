using System.Linq;
using System.Speech.Synthesis;

namespace Reading.Core.Speach
{
    public sealed class FrameworkSpeaker : ISpeaker
    {
        private readonly SpeechSynthesizer _synthesizer = new SpeechSynthesizer();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _synthesizer.Dispose();
        }

        public string[] GetVoices()
        {
            return _synthesizer.GetInstalledVoices().Select(e => e.VoiceInfo.Name).ToArray();
        }

        public int Rate
        {
            get { return _synthesizer.Rate; }
            set { _synthesizer.Rate = value; }
        }

        public string Voice
        {
            get { return _synthesizer.Voice?.Name; }
            set
            {
                if (Voice != value)
                    _synthesizer.SelectVoice(value);
            }
        }

        public int Volume
        {
            get { return _synthesizer.Volume; }
            set { _synthesizer.Volume = value; }
        }

        public bool IsEnabled { get; set; }

        public void Speak(string text)
        {
            if (IsEnabled)
                _synthesizer.Speak(text);
        }
    }
}
