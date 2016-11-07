using System;

namespace KnowledgeBase.BussinesLayer.Security
{
    /// <summary>
    /// 
    /// </summary>
    [global::System.Serializable]
    public class PermissionException : Exception
    {
       
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionException"/> class.
        /// </summary>
        public PermissionException(Permission permission) : base(string.Format("Operation required {0} permission ", permission)) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionException"/> class.
        /// </summary>
        /// <param name="permission">The permission.</param>
        public PermissionException(string permission) : base(string.Format("Operation required {0} permission ", permission)) { }

 
        /// <summary>
        /// Initializes a new instance of the <see cref="PermissionException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
        /// </exception>
        protected PermissionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
