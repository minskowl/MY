namespace Savchin.Data.MSSQL
{
    internal class Parser
    {
        /// <summary>
        /// Parses the name of the table.
        /// </summary>
        /// <param name="fullName">The full name.</param>
        /// <returns></returns>
        internal static string ParseTableName(string fullName)
        {
            string[] parts = fullName.Split('.');
            return parts[parts.Length - 1];
        }
    }
}
