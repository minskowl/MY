using Wunderkind.Core;

namespace Reading.Models
{
    public abstract class SpeakModel : BaseModel
    {
        public ISpeaker Speaker { get; set; }




        /// <summary>
        /// Speaks the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        protected void Speak(string text)
        {
            if (!string.IsNullOrWhiteSpace(text) && Speaker != null)
                Speaker.Speak(text);

            Status = text;
        }
    }
}
