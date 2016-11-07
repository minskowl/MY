using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Savchin.Validation
{
    /// <summary>
    /// ErrorCollection
    /// </summary>
    public class ErrorCollection : ICollection<ValidationError>
    {
        private List<ValidationError> storage = new List<ValidationError>();

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="attribute">The attribute.</param>
        internal void Add(MemberInfo member, ValidationAttribute attribute)
        {
            var message = string.Format(attribute.Message, GetDisplayName(member));

            storage.Add(new ValidationError(member.Name, message));
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public string GetMessage(string propertyName)
        {
            var builder = new StringBuilder();
            foreach (var error in storage)
            {
                if (propertyName.CompareTo(error.PropertyName) == 0)
                    builder.Append(error.Message);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <returns></returns>
        public string GetMessage()
        {
            var builder = new StringBuilder();
            foreach (var error in storage)
            {
                builder.AppendLine(error.Message);
            }
            return builder.ToString();
        }

        private string GetDisplayName(MemberInfo member)
        {
            var attr = member.GetCustomAttributes(typeof(DisplayNameAttribute), true);
            if (attr != null && attr.Length > 0)
            {
                return ((DisplayNameAttribute)attr[0]).DisplayName;
            }
            return member.Name;
        }
        
        #region Implementation of IEnumerable

        /// <summary>
        ///                     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///                     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<ValidationError> GetEnumerator()
        {
            return storage.GetEnumerator();
        }

        /// <summary>
        ///                     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///                     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        /// <summary>
        ///                     Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <param name="item">
        ///                     The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.
        ///                 </param>
        /// <exception cref="T:System.NotSupportedException">
        ///                     The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        ///                 </exception>
        public void Add(ValidationError item)
        {
            storage.Add(item);
        }

        /// <summary>
        ///                     Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        ///                     The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only. 
        ///                 </exception>
        public void Clear()
        {
            storage.Clear();
        }

        /// <summary>
        ///                     Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
        /// </summary>
        /// <returns>
        /// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
        /// </returns>
        /// <param name="item">
        ///                     The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        ///                 </param>
        public bool Contains(ValidationError item)
        {
            return storage.Contains(item);
        }

        /// <summary>
        ///                     Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">
        ///                     The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.
        ///                 </param>
        /// <param name="arrayIndex">
        ///                     The zero-based index in <paramref name="array" /> at which copying begins.
        ///                 </param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="array" /> is null.
        ///                 </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex" /> is less than 0.
        ///                 </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="array" /> is multidimensional.
        ///                     -or-
        ///                 <paramref name="arrayIndex" /> is equal to or greater than the length of <paramref name="array" />.
        ///                     -or-
        ///                     The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.
        ///                     -or-
        ///                     Type <paramref name="T" /> cannot be cast automatically to the type of the destination <paramref name="array" />.
        ///                 </exception>
        public void CopyTo(ValidationError[] array, int arrayIndex)
        {
            storage.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///                     Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <returns>
        /// true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        /// <param name="item">
        ///                     The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.
        ///                 </param>
        /// <exception cref="T:System.NotSupportedException">
        ///                     The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        ///                 </exception>
        public bool Remove(ValidationError item)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///                     Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </summary>
        /// <returns>
        ///                     The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
        /// </returns>
        public int Count
        {
            get { return storage.Count; }
        }

        /// <summary>
        ///                     Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.
        /// </returns>
        public bool IsReadOnly
        {
            get { return false; }
        }
    }
}
