using System.Collections.Generic;
using Savchin.Comparer;

namespace ToolsTest.Comparer
{
    class CompareObject
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
        public int IntValue
        {
            get { return intValue; }
            set { intValue = value; }
        }

        private int intValue;

        /// <summary>
        /// Gets the linked object.
        /// </summary>
        /// <value>The linked object.</value>
        [Compare]
        public NestedObject LinkedObject { get; private set; }

        [Compare]
        [CompareKey("IntValue")]
        public List<NestedObject> KeyObjects { get; private set; }

        /// <summary>
        /// Gets the nested objects.
        /// </summary>
        /// <value>The nested objects.</value>
        [Compare]
        public Dictionary<string, NestedObject> NestedObjects { get; private set; }


        public CompareObject()
        {
            LinkedObject = new NestedObject();
            NestedObjects = new Dictionary<string, NestedObject>();
            KeyObjects=new List<NestedObject>();
        }
    }
}
