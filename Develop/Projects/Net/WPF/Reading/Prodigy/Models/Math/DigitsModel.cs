using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prodigy.Models.Core;
using Prodigy.Properties;

namespace Prodigy.Models.Math
{
    public class DigitsModel : ListModel<int>
    {
        private int[] _digits;

        public int[] Digits
        {
            get { return _digits; }
            set { SetSetting(ref _digits, value); }
        }

        public override string Title => "Числа";

        protected override List<int> BuildList()
        {
            return new List<int>(_digits);
        }

        protected override void Initialize(Settings settings)
        {
            base.Initialize(settings);

            _digits = settings.Digits;
        }

        protected override void SaveSettings(Settings settings)
        {
            base.SaveSettings(settings);

            settings.Digits = _digits;

        }
    }
}
