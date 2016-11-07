namespace FlatSearcher.Core
{
    public interface IMap
    {
        /// <summary>
        /// Determines whether [is in region] [the specified LNG].
        /// </summary>
        /// <param name="lng">The LNG.</param>
        /// <param name="lat">The lat.</param>
        /// <returns>
        ///   <c>true</c> if [is in region] [the specified LNG]; otherwise, <c>false</c>.
        /// </returns>
        bool IsInRegion(string lng, string lat);
    }
}