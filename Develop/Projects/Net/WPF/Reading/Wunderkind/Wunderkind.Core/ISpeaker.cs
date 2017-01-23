﻿using System;

namespace Wunderkind.Core
{
    public interface ISpeaker : IDisposable
    {
        string[] GetVoices();
   
        int Rate { get; set; }
        string Voice { get; set; }
        int Volume { get; set; }

        bool IsEnabled { get; set; }
        void Speak(string text);

    }
}
