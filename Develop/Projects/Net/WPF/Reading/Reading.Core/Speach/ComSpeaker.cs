using System.Collections.Generic;
using System.Linq;
using SpeechLib;

namespace Reading.Core.Speach
{
    public sealed class ComSpeaker : ISpeaker
    {
        SpVoice _instance = new SpVoice();
        private Dictionary<string, SpObjectToken> _voicesMap;
        private string[] _voices;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComSpeaker"/> class.
        /// </summary>
        public ComSpeaker()
        {



            var tokens = _instance.GetVoices().Cast<SpObjectToken>().ToArray();
            _voicesMap = tokens.Select(e => new { Name = e.GetDescription(49), Value = e }).ToDictionary(e => e.Name, e => e.Value);
            _voices = _voicesMap.Keys.ToArray();


        }

        public void Dispose()
        {
            _instance = null;
            _voicesMap.Clear();
            _voicesMap = null;
            _voices = null;
        }

        public string[] GetVoices()
        {
            return _voices;
        }

        public int Rate
        {
            get { return _instance.Rate; }
            set { _instance.Rate = value; }
        }

        public string Voice
        {
            get { return _instance.Voice.GetDescription(49); }
            set { _instance.Voice = _voicesMap[value]; }
        }

        public int Volume
        {
            get { return _instance.Volume; }
            set { _instance.Volume = value; }
        }

        public bool IsEnabled { get; set; }

        public void Speak(string text)
        {
            if (IsEnabled)
                _instance.Speak(text);
        }
    }
}
