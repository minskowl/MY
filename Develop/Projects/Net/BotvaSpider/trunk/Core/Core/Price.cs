using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Savchin.ComponentModel;
using Savchin.Core;


namespace BotvaSpider.Core
{
    /// <summary>
    /// Price
    /// </summary>
    [Serializable]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Price : IComparable<Price>, IComparable, IEquatable<Price>
    {
        /// <summary>
        /// Gets or sets the resource.
        /// </summary>
        /// <value>The resource.</value>
        [XmlAttribute]
        [DisplayName("Чего")]
        [TypeConverter(typeof(EnumTypeConverter))]
        public Resource Resource { get; set; }

        /// <summary>
        /// Gets or sets the ammount.
        /// </summary>
        /// <value>The ammount.</value>
        [XmlAttribute]
        [DisplayName("Кол-во")]
        public int Ammount { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Price"/> class.
        /// </summary>
        public Price()
        {
            Resource = Resource.Gold;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Price"/> class.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="ammount">The ammount.</param>
        public Price(Resource resource, int ammount)
        {
            Resource = resource;
            Ammount = ammount;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Price"/> class.
        /// </summary>
        /// <param name="ammount">The ammount.</param>
        public Price(int ammount)
        {
            Resource = Resource.Gold;
            Ammount = ammount;
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has the following meanings:
        /// Value
        /// Meaning
        /// Less than zero
        /// This object is less than the <paramref name="other"/> parameter.
        /// Zero
        /// This object is equal to <paramref name="other"/>.
        /// Greater than zero
        /// This object is greater than <paramref name="other"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">Thrown if different types of resources</exception>
        public int CompareTo(Price other)
        {
            if (Resource != other.Resource) throw new InvalidOperationException("diferent resources not comparable");

            return Ammount.CompareTo(other.Ammount);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has these meanings:
        /// Value
        /// Meaning
        /// Less than zero
        /// This instance is less than <paramref name="obj"/>.
        /// Zero
        /// This instance is equal to <paramref name="obj"/>.
        /// Greater than zero
        /// This instance is greater than <paramref name="obj"/>.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        /// 	<paramref name="obj"/> is not the same type as this instance.
        /// </exception>
        public int CompareTo(object obj)
        {
            if (obj == null || !(obj is Price)) return -1;

            return CompareTo((Price)obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Price other)
        {
            if (Resource != other.Resource) return false;
            return Ammount == other.Ammount;
        }

        /// <summary>
        /// Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing a fully qualified type name.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", Ammount, Resource.GetDescription());
        }

        #region Factories
        /// <summary>
        /// Golds the specified ammount.
        /// </summary>
        /// <param name="ammount">The ammount.</param>
        /// <returns></returns>
        public static Price Gold(int ammount)
        {
            return new Price(Resource.Gold, ammount);
        }
        /// <summary>
        /// Crystalses the specified ammount.
        /// </summary>
        /// <param name="ammount">The ammount.</param>
        /// <returns></returns>
        public static Price Crystals(int ammount)
        {
            return new Price(Resource.Crystals, ammount);
        }
        /// <summary>
        /// Greens the specified ammount.
        /// </summary>
        /// <param name="ammount">The ammount.</param>
        /// <returns></returns>
        public static Price Green(int ammount)
        {
            return new Price(Resource.Green, ammount);
        } 
        #endregion
    }
}
