
using System;

namespace Savchin.Data.CSV
{
	/// <summary>
	/// Specifies the action to take when a field is missing.
	/// </summary>
	public enum MissingFieldAction
	{
		/// <summary>
		/// Treat as a parsing error.
		/// </summary>
		TreatAsParseError = 0,

		/// <summary>
		/// Returns an empty value.
		/// </summary>
		ReturnEmptyValue = 1,

		/// <summary>
		/// Returns a null value (<see langword="null"/>).
		/// </summary>
		ReturnNullValue = 2,

		/// <summary>
		/// Returns the partially parsed value.
		/// </summary>
		ReturnPartiallyParsedValue = 3
	}
}