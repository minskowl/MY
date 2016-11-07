namespace Savchin.Threading.IOCP
{
    /// <summary>
    /// Node class used by all other data structures
    /// </summary>
    public class Node<T> where T: class
    {
        #region Public Constructors
        /// <summary>
        /// Creates an instance of a Node class with 
        /// data element as null
        /// </summary>
        public Node()
        {
            Init(null);
        }

        /// <summary>
        /// Creates an instance of the Node class with
        /// data element as the passed in object
        /// </summary>
        /// <param name="data">Data of the Node instance</param>
        public Node(T data)
        {
            Init(data);
        }
        #endregion

        #region Private Methods
        private void Init(T data)
        {
            Data = data;
            NextNode = null;
        }
        #endregion

        #region Public Data Members
        public T Data;
        public Node<T> NextNode;
        #endregion
    }
}