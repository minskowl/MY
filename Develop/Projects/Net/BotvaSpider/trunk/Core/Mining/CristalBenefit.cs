using System.ComponentModel;

namespace BotvaSpider.Automation.Mining
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class CristalBenefit
    {
        /// <summary>
        /// Gets or sets the cristals.
        /// </summary>
        /// <value>The cristals.</value>
        [DisplayName("Кол-во кристалов")]
        public byte Cristals { get; set; }

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
        /// Gets a value indicating whether this instance has benefit.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has benefit; otherwise, <c>false</c>.
        /// </value>
        public bool HasBenefit
        {
            get
            {
                return SmallTicket || BigTicket || Cristals > 0;
            }
        }
    }
}