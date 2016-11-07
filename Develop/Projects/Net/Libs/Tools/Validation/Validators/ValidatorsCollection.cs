using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Savchin.Validation.Validators
{
    class ValidatorsCollection
    {
        readonly Dictionary<string, List<IValidator>> _storage = new Dictionary<string, List<IValidator>>();
        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(string key, IValidator value)
        {
            if (_storage.ContainsKey(key))
            {
                _storage[key].Add(value);
            }
            else
            {
                _storage.Add(key, new List<IValidator> { value });
            }
        }
        /// <summary>
        /// Gets the validators.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public  List<IValidator> GetValidators(string key)
        {
            return _storage.ContainsKey(key) ? _storage[key] : null;
        }
    }
}
