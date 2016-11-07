using System.Windows.Controls;
using Reading.Core;
using Reading.Core.Speach;
using Reading.Speach;

namespace Reading.Controls
{
    public class SpeakButton : Button
    {
        private ISpeaker _speech;


        /// <summary>
        /// Called when a <see cref="T:System.Windows.Controls.Button"/> is clicked.
        /// </summary>
        protected override void OnClick()
        {
            base.OnClick();
            if (_speech == null)
            {
                _speech = ReadingContext.Current.Speaker;
            }
            _speech.Speak(Content.ToString());
        }
    }
}
