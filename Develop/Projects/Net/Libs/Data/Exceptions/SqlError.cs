using System;

namespace Savchin.Data
{
	/// <summary>
	/// Error Sql class for repacking SqlError
	/// </summary> 
	/// 

	[Serializable()]
	public class SqlError
	{
	    
        #region Properties
	    
	    
        private int lineNumber;
        /// <summary>
        /// Gets or sets the line number.
        /// </summary>
        /// <value>The line number.</value>
        public int LineNumber
        {
            get { return lineNumber; }
        }

        private int number;

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        /// <value>The number.</value>
        public int Number
        {
            get { return number; }
        }

        private string message;
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message
        {
            get { return message; }
        } 
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="T:SqlError"/> class.
        /// </summary>
        /// <param name="error">The err.</param>
		public SqlError(System.Data.SqlClient.SqlError error)
		{
			if (error == null) throw new ArgumentNullException("error");
			lineNumber = error.LineNumber;
			number = error.Number;
			message = error.Message;

		}

	}

}
