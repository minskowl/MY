using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using BotvaSpider.Core;

namespace BotvaSpider.BookKeeping
{
    /// <summary>
    /// BalanceItem
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class BalanceItem
    {
        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>The category.</value>
        [DisplayName("Разряд покупки")]
        public BalanceCategory Category { get; set; }

        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>The item.</value>
        [DisplayName("Название")]
        public string Item { get; set; }


        /// <summary>
        /// Gets or sets the cristal.
        /// </summary>
        /// <value>The cristal.</value>
        [DisplayName("Кристаллы")]
        public int Cristal { get; set; }
        /// <summary>
        /// Gets or sets the gold.
        /// </summary>
        /// <value>The gold.</value>
        [DisplayName("Золото")]
        public int Gold { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [small ticket].
        /// </summary>
        /// <value><c>true</c> if [small ticket]; otherwise, <c>false</c>.</value>
        [DisplayName("Билет на малую")]
        public bool SmallTicket { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [big ticket].
        /// </summary>
        /// <value><c>true</c> if [big ticket]; otherwise, <c>false</c>.</value>
        [DisplayName("Билет на большую")]
        public bool BigTicket { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BalanceItem"/> is benefit.
        /// </summary>
        /// <value><c>true</c> if benefit; otherwise, <c>false</c>.</value>
        [DisplayName("Прибыль")]
        public bool IsProfit { get; set; }
        /// <summary>
        /// Gets or sets the purchase ID.
        /// </summary>
        /// <value>The purchase ID.</value>
        [DisplayName("ID")]
        public int ID { get; set; }


        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        [DisplayName("Дата")]
        public DateTime Date { get; set; }


        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} {1} Золото:{2} Кристалы:{3} Билеты: {4} ",
                IsProfit ? "Прибыль с" : "Затраты на", 
                Item,
                Gold, 
                Cristal, 
                (BigTicket ? "б" : (SmallTicket ? "м" : "-")));
        }
    }
}
