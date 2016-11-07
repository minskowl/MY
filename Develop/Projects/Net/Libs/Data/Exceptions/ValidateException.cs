using System;
using System.Data.Common;
using System.Runtime.Serialization;

namespace Savchin.Data
{

    /// <summary>
    /// Это исключение бросается методом Validate сущности содержит понятное для пользователя сообщение на требуемом языке.
    /// </summary>
    /// 

    [Serializable()]
    public class ValidateException : DbException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ValidateException"/> class.
        /// </summary>
        public ValidateException()
            : base("ValidateException")
        { }
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ValidateException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ValidateException(string message)
            : base(message)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidateException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ValidateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ValidateException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"></see> is zero (0). </exception>
        /// <exception cref="T:System.ArgumentNullException">The info parameter is null. </exception>
        protected ValidateException(SerializationInfo info, StreamingContext context)
            : base(info, context) 
        { }

        ///// <summary>
        ///// Creates the instance.
        ///// </summary>
        ///// <param name="messageKey">The message key.</param>
        ///// <param name="innerException">The inner exception.</param>
        ///// <returns></returns>
        //public static ValidateException CreateInstance(string messageKey, Exception innerException)
        //{
        //    ExceptionManager manager = new ExceptionManager();
        //    return new ValidateException(manager.Translate(messageKey),innerException);
            
        //}

        ///// <summary>
        ///// Creates the instance not unique field.
        ///// </summary>
        ///// <param name="fieldName">Name of the field.</param>
        ///// <param name="innerException">The inner exception.</param>
        ///// <returns></returns>
        //public static ValidateException CreateInstanceNotUniqueField(string fieldName, Exception innerException)
        //{
        //    ExceptionManager manager = new ExceptionManager();
        //    string message = string.Format(manager.Translate("field_not_unique"), fieldName);
        //    return new ValidateException(message, innerException);
        //}    
   }
}
