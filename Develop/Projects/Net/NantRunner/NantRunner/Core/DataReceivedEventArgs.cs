using System;
using System.Collections.Generic;
using System.Text;

namespace NantRunner.Core
{
    delegate void SimpleCallback();
    delegate string GetTextCallback();

    delegate void SetTextCallback(string text);
    
    public class DataReceivedEventArgs : EventArgs
    {
        private readonly string data;

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>The data.</value>
        public string Data
        {
            get { return data; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public DataReceivedEventArgs(string data)
            : base()
        {
            this.data = data;
        }
    }
}
