using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BotvaSpider.Core
{
    [global::System.Serializable]
    public class NoMoneyException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public NoMoneyException() { }
        public NoMoneyException(string message) : base(message) { }
        public NoMoneyException(string message, Exception inner) : base(message, inner) { }
        protected NoMoneyException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}