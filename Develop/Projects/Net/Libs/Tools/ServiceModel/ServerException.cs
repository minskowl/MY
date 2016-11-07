using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Savchin.ServiceModel
{
    /// <summary>
    /// ServerError
    /// </summary>
    public enum ServerError
    {
        /// <summary>
        /// Server thrown exception
        /// </summary>
        ServerFault,
        /// <summary>
        /// Server Unavalible(Shotdowned)
        /// </summary>
        ServerUnavalible,
        /// <summary>
        /// Requests timeouted
        /// </summary>
        Timeout
    }

    /// <summary>
    /// Service Unavalible Exception
    /// </summary>
    [global::System.Serializable]
    public class ServerException : Exception
    {
        private readonly ServerError error;
        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>The error.</value>
        public ServerError Error
        {
            get { return error; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerException"/> class.
        /// </summary>
        public ServerException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerException"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="message">The message.</param>
        public ServerException(ServerError error, string message)
            : base(message)
        {
            this.error = error;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerException"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public ServerException(ServerError error, string message, Exception inner)
            : base(message, inner)
        {
            this.error = error;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
        /// </exception>
        protected ServerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            this.error = (ServerError)info.GetValue("Error", typeof(ServerError));

        }
        /// <summary>
        /// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic).
        /// </exception>
        /// <PermissionSet>
        /// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*"/>
        /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter"/>
        /// </PermissionSet>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Error", this.error, typeof(ServerError));
        }




    }
}