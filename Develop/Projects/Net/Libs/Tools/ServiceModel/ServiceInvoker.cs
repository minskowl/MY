using System;
using System.ServiceModel;
using Effectivesoft.AMCS.Core;

namespace Savchin.ServiceModel
{
    /// <summary>
    /// ServiceInvoker
    /// </summary>
    public static class ServiceInvoker
    {
        /// <summary>
        /// WCF proxys do not clean up properly if they throw an exception. This method ensures that the service proxy is handeled correctly.
        /// Do not call TService.Close() or TService.Abort() within the action lambda.
        /// </summary>
        /// <typeparam name="TService">The type of the service to use</typeparam>
        /// <param name="action">Lambda of the action to performwith the service</param>
        public static void Using<TService>(Action<TService> action)
            where TService : ICommunicationObject, IDisposable, new()
        {
            var service = new TService();
            var success = false;
            try
            {
                action(service);
                if (service.State != CommunicationState.Faulted)
                {
                    service.Close();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                throw ServiceExceptionConverter.ConvertException(ex);
            }
            finally
            {
                if (!success)
                {
                    service.Abort();
                }
            }
        }

        /// <summary>
        /// Usings the specified service.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="factory">The factory.</param>
        /// <param name="action">The action.</param>
        public static void Using<TService>(Func<TService> factory, Action<TService> action)
           where TService : ICommunicationObject, IDisposable, new()
        {
            var service = factory();
            var success = false;
            try
            {
                action(service);
                if (service.State != CommunicationState.Faulted)
                {
                    service.Close();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                throw ServiceExceptionConverter.ConvertException(ex);
            }
            finally
            {
                if (!success)
                {
                    service.Abort();
                }
            }
        }
    }
}
