using Savchin.Comparer;

namespace ToolsTest.Comparer
{
    class NestedObject
    {


        private string name;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Compare]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Gets or sets the int value.
        /// </summary>
        /// <value>The int value.</value>
        [Compare]
        public int IntValue
        {
            get { return intValue; }
            set { intValue = value; }
        }

        private int intValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="NestedObject"/> class.
        /// </summary>
        public NestedObject()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="NestedObject"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="intValue">The int value.</param>
        public NestedObject(string name, int intValue)
        {
            this.name = name;
            this.intValue = intValue;
        }
    }
}
