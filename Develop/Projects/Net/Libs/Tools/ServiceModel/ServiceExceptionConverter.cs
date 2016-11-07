using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Savchin.ServiceModel;

namespace Effectivesoft.AMCS.Core
{
    /// <summary>
    /// Service ExceptionConverter
    /// </summary>
    public static class ServiceExceptionConverter
    {
        /// <summary>
        /// Convers the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public  static Exception ConvertException(Exception ex)
        {

            var message = ex.Message;
            if (ex is EndpointNotFoundException)
            {
                //Network error
                if (message.StartsWith("No DNS entries exist for host"))
                {
                    var pos = message.LastIndexOf(' ') + 1;
                    var host = message.Substring(pos, message.Length - pos - 1);

                    return new NetworkException(
                        string.Format("Network configuration error. Host '{0}' is unavailable.", host)
                        , ex);
                }
                //Server Unavalible(Shotdowned)
                if (message.StartsWith("Could not connect to"))
                {
                    return new ServerException(ServerError.ServerUnavalible, "Server is unavailable.", ex);
                }
            }

            if (ex is TimeoutException)
            {
                return new ServerException(ServerError.Timeout, "Server not responding.", ex);
            }
            if (ex is FaultException)
            {
                return new ServerException(ServerError.ServerFault, "Server fault.", ex);
            }

            return ex;
        }
    }
}
