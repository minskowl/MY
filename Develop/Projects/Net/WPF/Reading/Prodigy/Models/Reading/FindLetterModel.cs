using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prodigy.Models.Core;
using Prodigy.Properties;
using Reading.Core;
using Reading.Core.Settings;
using Savchin.Core;

namespace Prodigy.Models.Reading
{
    public class FindLetterModel : SpeakModel
    {
        #region Data
        private readonly Primer _primer = new Primer();

        public int Count
        {
            get { return _count; }
            set { SetSetting(ref _count, value); }
        }

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
        private int _count;

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

        public override string Title => "Найди букву";

        public string[] Letters { get; set; }

        #endregion

        public FindLetterModel()
        {
            Modes = Translation.Translate<SelectionMode>().ToArray();
            Types = Translation.Translate<LettersTypes>().ToArray();

        }



        protected override void Initialize(Settings settings)
        {
            base.Initialize(settings);
            var s = settings.FindLetterSettings;
            if (s == null) return;
            _type = s.LettersTypes;
            _mode = s.Mode;
            _count = s.LettersCount;
            Enumerable.Range(0,Count).Select()
            for (int i = 0; i < Count; i++)
            {
                
            }
            Letters
        }

        protected override void SaveSettings(Settings settings)
        {
            base.SaveSettings(settings);
            if (settings.FindLetterSettings == null)
                settings.FindLetterSettings = new FindLetterSettings();

            var s = settings.FindLetterSettings;

            s.LettersTypes = _type;
            s.Mode = _mode;
            s.LettersCount = _count;
        }
    }
}
