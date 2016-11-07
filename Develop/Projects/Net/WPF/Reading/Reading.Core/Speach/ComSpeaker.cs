using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SpeechLib;

namespace Reading.Speach
{
    public sealed class ComSpeaker : ISpeaker
    {
        SpVoice instance = new SpVoice();
        private Dictionary<string, SpObjectToken> _voicesMap;
        private string[] _voices;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComSpeaker"/> class.
        /// </summary>
        public ComSpeaker()
        {



            var tokens = instance.GetVoices().Cast<SpObjectToken>().ToArray();
            _voicesMap = tokens.Select(e => new { Name = e.GetDescription(49), Value = e }).ToDictionary(e => e.Name, e => e.Value);
            _voices = _voicesMap.Keys.ToArray();

          
        }

        public void Dispose()
        {
            instance = null;
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
            get { return instance.Rate; }
            set { instance.Rate = value; }
        }

        public string Voice
        {
            get { return instance.Voice.GetDescription(49); }
            set { instance.Voice = _voicesMap[value]; }
        }

        public int Volume
        {
            get { return instance.Volume; }
            set { instance.Volume = value; }
        }

        public void Speak(string text)
        {
            instance.Speak(text);
        }
    }
}
