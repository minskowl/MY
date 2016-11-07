namespace Savchin.Comparer
{
    public class KeyMatcher
    {
        private string key;
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyMatcher"/> class.
        /// </summary>
         public KeyMatcher()
         {
         }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyMatcher"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        public KeyMatcher(string key)
        {
            this.Key = key;
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key
        {
            get { return key; }
            set { key = value; }
        }
    }
}
