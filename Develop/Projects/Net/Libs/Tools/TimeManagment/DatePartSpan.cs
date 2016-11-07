#region Version & Copyright
/* 
 * Id 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.TimeManagment
{

    /// <summary>
    /// TimeSpan
    /// </summary>
    public struct DatePartSpan : IEquatable<DatePartSpan>
    {
        private static readonly Dictionary<string, DatePart> datePartMap = new Dictionary<string, DatePart>();
        private static readonly Dictionary<DatePart, string> partsNames = new Dictionary<DatePart, string>();
        private static readonly List<DatePart> parts = new List<DatePart>();

        private int value;
        private DatePart part;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public int Value
        {
            get { return value; }
            set { this.value = value; }
        }
        /// <summary>
        /// Gets the parts.
        /// </summary>
        /// <value>The parts.</value>
        public static IEnumerable<DatePart> Parts
        {
            get { return parts; }
        }
        /// <summary>
        /// Gets or sets the part.
        /// </summary>
        /// <value>The part.</value>
        public DatePart Part
        {
            get { return part; }
            set { part = value; }
        }

        /// <summary>
        /// Gets the short name of the part.
        /// </summary>
        /// <value>The short name of the part.</value>
        public string PartName
        {
            get { return partsNames[part]; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DatePartSpan"/> struct.
        /// </summary>
        /// <param name="part">The part.</param>
        /// <param name="value">The value.</param>
        public DatePartSpan(DatePart part, int value)
        {
            this.part = part;
            this.value = value;
        }

        /// <summary>
        /// Initializes the <see cref="DatePartSpan"/> struct.
        /// </summary>
        static DatePartSpan()
        {

            Array values = Enum.GetValues(typeof(DatePart));
            foreach (DatePart o in values)
            {
                string name = o.ToString();

                parts.Add(o);

                datePartMap.Add(name, o);
                partsNames.Add(o, name);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatePartSpan"/> struct.
        /// </summary>
        /// <param name="span">The span.</param>
        public DatePartSpan(DatePartSpan span)
            : this(span.Part, span.Value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatePartSpan"/> struct.
        /// </summary>
        /// <param name="part">The part.</param>
        /// <param name="value">The value.</param>
        public DatePartSpan(string part, int value)
        {
            this.part = datePartMap[part];
            this.value = value;
        }

        #region Equals

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterPriority>2</filterPriority>
        public override int GetHashCode()
        {
            return value ^ part.GetHashCode();
        }

        /// <summary>
        /// Equalses the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is TimeSpan))
                return false;

            return Equals((DatePartSpan)obj);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(DatePartSpan other)
        {

            if (part != other.part)
            {
                return false;
            }

            return value == other.Value;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="span1">The span1.</param>
        /// <param name="span2">The span2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(DatePartSpan span1, DatePartSpan span2)
        {
            return span1.Equals(span2);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="span1">The span1.</param>
        /// <param name="span2">The span2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(DatePartSpan span1, DatePartSpan span2)
        {
            return !span1.Equals(span2);
        }
        #endregion

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        /// <filterPriority>2</filterPriority>
        public override string ToString()
        {
            return string.Format("{0} {1}{2}",value, part.ToString(),
                                 (value == 0 || value == 1) ? string.Empty : "s");

        }
    }
}
