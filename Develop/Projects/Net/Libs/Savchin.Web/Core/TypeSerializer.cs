using System.Web.Script.Serialization;


namespace Savchin.Web.Core
{
    /// <summary>
    /// TypeSerializer
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class TypeSerializer<T>
    {
        private static JavaScriptSerializer _serializerJson;
        /// <summary>
        /// Gets the serializer json.
        /// </summary>
        /// <value>The serializer json.</value>
        private static JavaScriptSerializer serializerJson
        {
            get { return _serializerJson ?? (_serializerJson = new JavaScriptSerializer()); }
        }

        /// <summary>
        /// Froms the json string.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static T FromJsonString(string s)
        {
            return serializerJson.Deserialize<T>(s);
        }

        /// <summary>
        /// Toes the JSON string.
        /// </summary>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public static string ToJsonString(object o)
        {
            return serializerJson.Serialize(o);
        }
    }
}
