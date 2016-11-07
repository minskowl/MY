using System;

namespace Savchin.Data
{
    /// <summary>
    /// LongFieldException throw when string data in Entity longer then field in DB
    /// </summary>
    /// 

    [Serializable()]
    public class LongFieldException : IntegrityException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:LongFieldException"/> class.
        /// </summary>
        public LongFieldException()
            : base("Fields to long")
        { }
    }
}
