

using System;

namespace Savchin.Threading.IOCP
{
	/// <summary>
	/// Base class for all ManagedIOCP exceptions
	/// </summary>
	[Serializable()]
	public class ManagedIOCPException : ApplicationException
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedIOCPException"/> class.
        /// </summary>
		public ManagedIOCPException()
		{
		}
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedIOCPException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
		public ManagedIOCPException(string message) 
			: base(message)
		{
		}
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagedIOCPException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
		public ManagedIOCPException(string message,Exception innerException)
			: base(message,innerException)
		{
		}
	}
}
