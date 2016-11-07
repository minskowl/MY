using System;

namespace Savchin.Comparer
{
    /// <summary>
    /// CompareResultBases
    /// </summary>
    public abstract class CompareResultBase
    {
        #region Properties

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>The source.</value>
        public object Source { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is comparable.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is comparable; otherwise, <c>false</c>.
        /// </value>
        internal bool IsComparable { get; private set; }



        /// <summary>
        /// Gets the destination.
        /// </summary>
        /// <value>The destination.</value>
        public object Destination { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is equals.
        /// </summary>
        /// <value><c>true</c> if this instance is equals; otherwise, <c>false</c>.</value>
        public bool IsEquals { get; internal set; }

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        public Type ObjectType { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is sync.
        /// </summary>
        /// <value><c>true</c> if this instance is sync; otherwise, <c>false</c>.</value>
        public bool IsSync { get; set; }

        #endregion

  
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareResultBase"/> class.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="name">The name.</param>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        internal CompareResultBase(Type objectType, string name, Object source, Object destination)
        {
            IsSync = false;
            ObjectType = objectType;
            Name = name;
            Source = source;
            Destination = destination;


            if (source == null && destination == null)
            {
                IsEquals = true;
                IsComparable = false;
            }
            else if (source == null || destination == null)
            {
                IsEquals = false;
                IsComparable = false;
            }
            else if (source.GetType() != destination.GetType())
            {
                IsEquals = false;
                IsComparable = false;
                throw new ArgumentException("Different type objects");
            }
            else
            {
                IsEquals = true;
                IsComparable = true;
            }

        }

        /// <summary>
        /// Syncs this instance.
        /// </summary>
        public abstract void Sync();
        /// <summary>
        /// Sets the sync.
        /// </summary>
        public void SetSync()
        {
            IsSync = !IsEquals;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Name, IsEquals ? "==" : "!=");
        }

    }
}
