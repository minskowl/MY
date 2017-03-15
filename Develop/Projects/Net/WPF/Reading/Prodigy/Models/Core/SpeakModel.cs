using Reading.Core;
using Reading.Core.Speach;

namespace Prodigy.Models.Core
{
    public abstract class SpeakModel : BaseModel
    {
        protected ISpeaker Speaker => _speaker ?? (_speaker = ReadingContext.Current.Speaker);

        private ISpeaker _speaker;

        /// <summary>
        /// Speaks the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        protected void Speak(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
                Speaker?.Speak(text);

            Status = text;
        }
    }
}
