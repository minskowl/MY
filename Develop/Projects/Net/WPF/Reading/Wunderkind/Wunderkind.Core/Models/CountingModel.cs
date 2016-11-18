using System;
using System.Collections.Generic;
using System.Linq;
using Wunderkind.Core;

namespace Reading.Models
{
    public class CountingModel : ListModel<int?>
    {
   
        public override string Title
        {
            get { return "Счёт"; }
        }


        private int _numberFrom;
        /// <summary>
        /// Gets or sets the NumberFrom.
        /// </summary>
        /// <value>The name.</value> 
        public int NumberFrom
        {
            get { return _numberFrom; }
            set
            {
                if (_numberFrom == value) return;
                _numberFrom = value;
                OnSettingChanged("NumberFrom");
            }
        }


        private int _numberTo;
        /// <summary>
        /// Gets or sets the NumberTo.
        /// </summary>
        /// <value>The name.</value> 
        public int NumberTo
        {
            get { return _numberTo; }
            set
            {
                if (_numberTo == value) return;
                _numberTo = value;
                OnSettingChanged("NumberTo");
            }
        }


        /// <summary>
        /// Builds the list.
        /// </summary>
        /// <returns></returns>
        protected override List<int?> BuildList()
        {
            var from = Math.Min(NumberFrom, NumberTo);
            var to = Math.Max(NumberFrom, NumberTo);
            return Enumerable.Range(from, to-from+1).Cast<int?>().ToList();
        }

        /// <summary>
        /// Initializes the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        protected override void Initialize(Settings settings)
        {
            base.Initialize(settings);

            NumberFrom = settings.CountingNumberFrom;
            NumberTo = settings.CountingNumberTo;
            ItemsRepetable = settings.CountingRepeatable;

            SetNewItem();
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        protected override void SaveSettings(Settings settings)
        {
            base.SaveSettings(settings);

            settings.CountingNumberFrom = NumberFrom;
            settings.CountingNumberTo = NumberTo;
            settings.CountingRepeatable = ItemsRepetable;
        }
    }
}
