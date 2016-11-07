using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advertiser.Controls;
using Advertiser.Entities;
using Savchin.Core;

namespace Advertiser.Views
{
    public class PhonesListView : EntityListView<Phone>
    {
        private PhoneUserControl _control = new PhoneUserControl();

        public List<NameValuePair> Countries { get; private set; }
        public NameValuePair[] Operators { get; private set; }
        public NameValuePair[] Hours { get; private set; }

        public override object ActiveView
        {
            get { return _control; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhonesListView"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public PhonesListView(List<Phone> source)
            : base("Телефоны", source)
        {
            CreateContries();
            Operators = EnumHelper.GetData(typeof(Operator));
            Hours = Enumerable.Range(-1, 25)
                .Select(e => new NameValuePair(e == -1 ? string.Empty : e.ToString(), e)).ToArray();
        }

        protected override bool CanSave()
        {
            return base.CanSave() && SelectedItem != null &&
                !string.IsNullOrWhiteSpace(SelectedItem.Number) &&
                SelectedItem.Operator!=Operator.None;
        }

        private void CreateContries()
        {
            Countries = new List<NameValuePair>
                          {
                              new NameValuePair("(+375) Беларусь", 1),
                              new NameValuePair("(+32) Бельгия", 4),
                              new NameValuePair("(+359) Болгария", 5),
                              new NameValuePair("(+36) Венгрия", 6),
                              new NameValuePair("(+49) Германия", 7),
                              new NameValuePair("(+31) Голландия", 8),
                              new NameValuePair("(+34) Испания", 9),
                              new NameValuePair("(+39) Италия", 19),
                              new NameValuePair("(+1) Канада", 10),
                              new NameValuePair("(+371) Латвия", 11),
                              new NameValuePair("(+370) Литва", 17),
                              new NameValuePair("(+373) Молдова", 12),
                              new NameValuePair("(+48) Польша", 13),
                              new NameValuePair("(+7) Россия", 2),
                              new NameValuePair("(+1) США", 3),
                              new NameValuePair("(+380) Украина", 14),
                              new NameValuePair("(+33) Франция", 15),
                              new NameValuePair("(+420) Чехия", 16),
                              new NameValuePair("(+46) Швеция", 18),
                              new NameValuePair("(+82) Южная Корея", 20),
                              new NameValuePair("(+971) ОАЭ", 21),
                              new NameValuePair("(+381) Сербия", 22),
                              new NameValuePair("(+43) Австрия", 23),
                              new NameValuePair("(+355) Албания", 24),
                              new NameValuePair("(+376) Андорра", 25),
                              new NameValuePair("(+387) Босния и Герцеговина", 26),
                              new NameValuePair("(+44) Великобритания", 27),
                              new NameValuePair("(+30) Греция", 28),
                              new NameValuePair("(+45) Дания", 29),
                              new NameValuePair("(+972) Израиль", 30),
                              new NameValuePair("(+353) Ирландия", 31),
                              new NameValuePair("(+354) Исландия", 32),
                              new NameValuePair("(+82) Республика Корея", 33),
                              new NameValuePair("(+40) Румыния", 34),
                              new NameValuePair("(+421) Словакия", 35),
                              new NameValuePair("(+386) Словения", 36),
                              new NameValuePair("(+358) Финляндия", 37),
                              new NameValuePair("(+385) Хорватия", 38),
                              new NameValuePair("(+420) Чешская Республика", 39),
                              new NameValuePair("(+41) Швейцария", 40),
                              new NameValuePair("(+372) Эстония", 41),
                              new NameValuePair("(+81) Япония", 42)
                          };
        }
    }
}
