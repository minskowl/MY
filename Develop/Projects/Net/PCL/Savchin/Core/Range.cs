using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace Savchin.Core
{
    /// <summary>
    /// Value range
    /// </summary>
    /// <typeparam name="T"></typeparam>
   
    public class Range<T> : IXmlSerializable, IRange<T> where T : IComparable, IComparable<T>
    {
        #region Properties
        private T _from;
        private T _to;
        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
        public T From
        {
            get { return _from; }
            set
            {
                Validate(value, _to);
                _from = value;
            }
        }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        public T To
        {
            get { return _to; }
            set
            {
                Validate(_from, value);
                _to = value;
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Range&lt;T&gt;"/> class.
        /// </summary>
        public Range()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Range&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public Range(T from, T to)
        {
            SetValue(from, to);
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public void SetValue(T from, T to)
        {
            Validate(from, to);
            _from = from;
            _to = to;
        }

        /// <summary>
        /// Determines whether [is in range] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if [is in range] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsInRange(T value)
        {
            return (value.CompareTo(_from) != -1 && value.CompareTo(_to) != 1);
        }

        /// <summary>
        /// Determines whether the specified range is overlaped.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <returns>
        ///   <c>true</c> if the specified range is overlaped; otherwise, <c>false</c>.
        /// </returns>
        public bool IsOverlaped(Range<T> range)
        {
            var fromComp = range.From.CompareTo(_from);
            var toComp = range.To.CompareTo(_to);
            return (fromComp == 0 || toComp == 0 || fromComp != toComp);
        }

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        private static void Validate(T from, T to)
        {
            try
            {
                if (from.CompareTo(to) == 1)
                    throw new ArgumentException("from must be less than to");
            }
            catch (NullReferenceException)// if class than can has open ranges
            {
            }

        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> stream from which the object is deserialized.</param>
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            var type = typeof(T);
            reader.ReadToDescendant("From");
            var from = (T)reader.ReadElementContentAs(type, null);
            var to = (T)reader.ReadElementContentAs(type, null);
            reader.ReadEndElement();

            SetValue(from, to);
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized.</param>
        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("From");
            writer.WriteValue(_from);
            writer.WriteEndElement();
            writer.WriteStartElement("To");
            writer.WriteValue(_to);
            writer.WriteEndElement();
        }
    }
}
