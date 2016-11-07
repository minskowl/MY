using System.ComponentModel;
using Savchin.Core;

namespace Advertiser.Entities
{

    public enum Operator
    {
        [Description]
        None = 0,
        [Description("Белтелеком")]
        Mgts = 1,
        Velcom = 2,
        [Description("MTC")]
        Mts = 3,
        Dialog = 4,
        Life = 5
    }

    public class Phone : Entity
    {
        #region Properties
        private string _number;
        public string Number
        {
            get { return _number; }
            set
            {
                if (value == _number) return;
                _number = value;
                OnPropertyChanged("Number");
                OnPropertyChanged("Title");
            }
        }
        private Operator _operator;
        public Operator Operator
        {
            get { return _operator; }
            set
            {
                if (value == _operator) return;
                _operator = value;
                OnPropertyChanged("Operator");
                OnPropertyChanged("Title");
            }
        }
        public int CountryCode { get; set; }
        public string Code { get; set; }
        public int From { get; set; }
        public int To { get; set; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title
        {
            get { return string.Format("{0} {1}", Number, Operator.GetDescription()); }
        } 
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Phone"/> class.
        /// </summary>
        public Phone()
        {
            From = -1;
            To = -1;
        }
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Title;
        }

    }

}
