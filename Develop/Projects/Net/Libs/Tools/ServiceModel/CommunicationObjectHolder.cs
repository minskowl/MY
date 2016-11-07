using System;
using System.ServiceModel;
using Effectivesoft.AMCS.Core;
using Savchin.Logging;


namespace Savchin.ServiceModel
{
    /// <summary>
    /// ServiceBase
    /// </summary>
    public abstract class CommunicationObjectHolder : IDisposable
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is disposed.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is disposed; otherwise, <c>false</c>.
        /// </value>
        protected bool IsDisposed { get; private set; }

        /// <summary>
        /// Gets or sets the log.
        /// </summary>
        /// <value>The log.</value>
        public ILogger Log { get; set; }

        private readonly object _syncRoot = new object();
        /// <summary>
        /// client
        /// </summary>
        private ICommunicationObject _client;
        /// <summary>
        /// Gets the client.
        /// </summary>
        /// <value>The client.</value>
        protected internal ICommunicationObject Client
        {
            get
            {
                if (IsDisposed) 
                    throw new InvalidOperationException("Service was disposed");

                lock (_syncRoot)
                {
                    if (_client == null ||
                        _client.State == CommunicationState.Faulted ||
                        _client.State == CommunicationState.Closed)
                    {
                        Close();
                        _client = CreateClient();
                    }
                }
                return _client;
            }
        }
        protected CommunicationObjectHolder()
        {
            Log = new NullLoger();
        }

        /// <summary>
        /// Creates the client.
        /// </summary>
        /// <returns></returns>
        protected abstract ICommunicationObject CreateClient();


        /// <summary>
        /// Closes the client.
        /// </summary>
        public void Close()
        {
            lock (_syncRoot)
            {
                if (_client == null) return;

                if (_client.State == CommunicationState.Opened ||
                                    _client.State == CommunicationState.Opening)
                {
                    OnClose(_client);
                    try
                    {
                        _client.Close();
                    }
                    catch (Exception ex)
                    {
                        Log.AddMessage(Severity.Warning,"Cannot close client.", ex);
                    }
                }
                if (_client.State == CommunicationState.Faulted)
                {
                    _client.Abort();
                }

                _client = null;
            }
        }

        /// <summary>
        /// Called when [close].
        /// </summary>
        /// <param name="client">The client.</param>
        protected virtual void OnClose(ICommunicationObject client)
        {

        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !IsDisposed)
            {
                Close();
                IsDisposed = true;
            }
        }


        /// <summary>
        /// Convers the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        protected virtual Exception ConvertException(Exception ex)
        {
            return ServiceExceptionConverter.ConvertException(ex);
        }


        #endregion
    }
}