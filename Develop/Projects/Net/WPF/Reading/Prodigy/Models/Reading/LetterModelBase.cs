using Reading.Core;
using Savchin.Core;

namespace Prodigy.Models.Reading
{
    public abstract class LetterModelBase : SyllablesModelBase
    {
        private readonly Primer _primer = new Primer();
        private SelectionMode _mode;
        /// <summary>
        /// Gets or sets the Operation.
        /// </summary>
        /// <value>The name.</value> 
        public SelectionMode Mode
        {
            get { return _mode; }
            set { SetSetting(ref _mode, value); }
        }


        private LettersTypes _type = LettersTypes.All;

        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        /// <value>The name.</value> 
        public LettersTypes Type
        {
            get { return _type; }
            set{SetSetting(ref _type, value);}
        }

        public NameValuePair[] Modes { get; set; }
        public NameValuePair[] Types { get; set; }

        protected string GetLetter()
        {
            return new string(_primer.GetLetter(Type, Mode), 1).ToUpper();
        }
    }
}