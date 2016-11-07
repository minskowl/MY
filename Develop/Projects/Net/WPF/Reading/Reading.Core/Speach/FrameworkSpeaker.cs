using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;

namespace Reading.Speach
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
            get { return _synthesizer.Voice == null ? null : _synthesizer.Voice.Name; }
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

        public void Speak(string text)
        {
            _synthesizer.Speak(text);
        }
    }
}
