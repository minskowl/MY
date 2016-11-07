using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reading.Speach
{
    public interface ISpeaker : IDisposable
    {
        string[] GetVoices();
   
        int Rate { get; set; }
        string Voice { get; set; }
        int Volume { get; set; }
        void Speak(string text);

    }
}
