using System;
using System.Xml.Serialization;

namespace Savchin.CodeGeneration
{

    [Serializable()]
    public class Property
    {
        private  String _Name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlAttribute()]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }


        private String _Value;
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [XmlAttributeAttribute()]
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class.
        /// </summary>
        public Property()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Property"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public Property(String name, String value)
        {
            this.Name = name;
            this.Value = value;
        }




    }

}
