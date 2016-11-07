namespace Savchin.Core
{
    /// <summary>
    /// IRange
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRange<T> 
    {
        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
        T From { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        T To { get; set; }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        void SetValue(T from, T to);

        /// <summary>
        /// Determines whether [is in range] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if [is in range] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        bool IsInRange(T value);
    }
}