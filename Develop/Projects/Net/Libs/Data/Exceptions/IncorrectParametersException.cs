using System;
using System.Data.Common;

namespace Savchin.Data
{

    /// <summary>
    /// IncorrectParametersException
    /// </summary>
    /// 

    [Serializable()]
    public class IncorrectParametersException : DbException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:IncorrectParametersException"/> class.
        /// </summary>
        public IncorrectParametersException()
            : base("Incorrect Parameters")
        {
        }
    }

}
