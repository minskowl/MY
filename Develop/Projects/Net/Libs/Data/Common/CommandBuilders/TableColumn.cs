using System;
using System.Collections.Generic;
using System.Text;

namespace Savchin.Data.Common
{
    public class TableColumn
    {
        private string name;
        private Type dataType;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Type DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TableColumn"/> class.
        /// </summary>
         public TableColumn()
         {
         }

        /// <summary>
        /// Initializes a new instance of the <see cref="TableColumn"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dataType">Type of the data.</param>
        public TableColumn(string name, Type dataType)
        {
            this.Name = name;
            this.DataType = dataType;
        }


    }
}
