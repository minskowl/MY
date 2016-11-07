using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;

namespace Savchin.Data
{
	/// <summary>
	/// UnhandledSqlException
	/// </summary>
	/// 

	[Serializable()]
    public class UnhandledSqlException : DbException
	{


		private SqlErrorCollection _errors;
		/// <summary>
		/// Gets the errors.
		/// </summary>
		/// <value>The errors.</value>
		public SqlErrorCollection Errors
		{
			get
			{
				return _errors;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:UnhandledSqlException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="errors">The errors.</param>
		/// <param name="inner">The inner.</param>
		public UnhandledSqlException(string message, SqlErrorCollection errors, DbException inner)
			: base(message, inner)
		{
			_errors = errors;
		}



		/// <summary>
		/// Determines whether the specified code has error.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <returns>
		/// 	<c>true</c> if the specified code has error; otherwise, <c>false</c>.
		/// </returns>
		public bool HasError(int code)
		{
			return (GetError(code) != null);
		}

		/// <summary>
		/// Determines whether the specified code has error.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <param name="message">The mes.</param>
		/// <returns>
		/// 	<c>true</c> if the specified code has error; otherwise, <c>false</c>.
		/// </returns>
		public bool HasError(int code, string message)
		{
			return (GetError(code, message) != null);
		}

		/// <summary>
		/// Gets the error.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <returns></returns>
		public SqlError GetError(int code)
		{
			foreach (SqlError err in _errors)
				if (err.Number  == code) return err;

			return null;
		}
		/// <summary>
		/// Gets the error.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <param name="message">The mes.</param>
		/// <returns></returns>
		public SqlError GetError(int code, string message)
		{
			message = message.ToLower(CultureInfo.CurrentCulture );

			foreach (SqlError err in _errors)
				if (err.Number == code && err.Message.ToLower(CultureInfo.CurrentCulture).IndexOf(message) > -1) return err;

			return null;
		}
	}



}
