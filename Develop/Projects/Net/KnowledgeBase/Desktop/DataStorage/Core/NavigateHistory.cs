using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeBase.Desktop.Core
{
    class NavigateHistory
    {
        private List<int> storage = new List<int>();
        private int position = 0;

        /// <summary>
        /// Gets a value indicating whether this instance can backward.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can backward; otherwise, <c>false</c>.
        /// </value>
        public bool CanBackward
        {
            get { return position > 0; }
        }
        /// <summary>
        /// Gets a value indicating whether this instance can forward.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance can forward; otherwise, <c>false</c>.
        /// </value>
        public bool CanForward
        {
            get { return position < storage.Count; }
        }

        /// <summary>
        /// Adds the specified category id.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        public void Add(int categoryId)
        {
            //Ignore duplicates
            if (position > 0 && storage[position - 1] == categoryId)
                return;

            if (storage.Count < position)
            {
                storage[position] = categoryId;
            }
            else
            {
                storage.Add(categoryId);
            }
            position++;
        }

        /// <summary>
        /// Backwards this instance.
        /// </summary>
        /// <returns></returns>
        public int Backward()
        {
            if (CanBackward)
            {
                position--;
                return storage[position];
            }
            return -1;

        }

        /// <summary>
        /// Forwards this instance.
        /// </summary>
        /// <returns></returns>
        public int Forward()
        {
            if (CanForward)
            {
                position++;
                return storage[position - 1];
            }

            return -1;
        }

    }
}
