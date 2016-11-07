using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Savchin.CodeEditor.Services
{
    /// <summary>
    /// Is thrown when the ServiceManager cannot find a required service.
    /// </summary>
    [Serializable()]
    public class ServiceNotFoundException : Exception
    {
        public ServiceNotFoundException()
            : base()
        {
        }

        public ServiceNotFoundException(Type serviceType)
            : base("Required service not found: " + serviceType.FullName)
        {
        }

        public ServiceNotFoundException(string message)
            : base(message)
        {
        }

        public ServiceNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ServiceNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
