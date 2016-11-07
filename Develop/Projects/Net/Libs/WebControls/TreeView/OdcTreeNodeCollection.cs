using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace Savchin.Web.UI.TreeView
{
    /// <summary>
    /// OdcTreeNodeCollection
    /// </summary>
    [Serializable]
    public class OdcTreeNodeCollection : IStateManager, IList<OdcTreeNode>, ICollection
    {
        private readonly List<OdcTreeNode> list = new List<OdcTreeNode>();
        private int? level;
        private OdcTreeView treeView;

        /// <summary>
        /// Initializes a new instance of the <see cref="OdcTreeNodeCollection"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public OdcTreeNodeCollection(OdcTreeNode owner)
        {
            Owner = owner;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OdcTreeNodeCollection"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public OdcTreeNodeCollection(OdcTreeView owner)
        {
            Owner = owner;
        }

        /// <summary>
        /// Gets the depth.
        /// </summary>
        /// <value>The depth.</value>
        public int Depth
        {
            get
            {
                if (level == null)
                {
                    int depth = 0;
                    var col = Owner as OdcTreeNode;
                    while (col != null)
                    {
                        depth++;
                        col = col.owner as OdcTreeNode;
                    }
                    level = depth;
                }
                return level.Value;
            }
        }

        /// <summary>
        /// Gets the tree view.
        /// </summary>
        /// <value>The tree view.</value>
        public OdcTreeView TreeView
        {
            get
            {
                if (treeView == null)
                {
                    treeView = Owner as OdcTreeView;
                    if (treeView == null)
                    {
                        treeView = (Owner as OdcTreeNode).TreeView;
                    }
                }
                return treeView;
            }
        }


        /// <summary>
        /// Gets the Owner which is eighter OdcTreeView or OdcTreeNode.S
        /// </summary>
        public object Owner { get; private set; }

        #region ICollection Members

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.ICollection"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="index"/> is less than zero.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// 	<paramref name="array"/> is multidimensional.
        /// -or-
        /// <paramref name="index"/> is equal to or greater than the length of <paramref name="array"/>.
        /// -or-
        /// The number of elements in the source <see cref="T:System.Collections.ICollection"/> is greater than the available space from <paramref name="index"/> to the end of the destination <paramref name="array"/>.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// The type of the source <see cref="T:System.Collections.ICollection"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.
        /// </exception>
        public void CopyTo(Array array, int index)
        {
            list.CopyTo(array as OdcTreeNode[], index);
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe).
        /// </summary>
        /// <value></value>
        /// <returns>true if access to the <see cref="T:System.Collections.ICollection"/> is synchronized (thread safe); otherwise, false.
        /// </returns>
        public bool IsSynchronized
        {
            get { return true; }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection"/>.
        /// </returns>
        public object SyncRoot
        {
            get { return list; }
        }

        #endregion

        #region IList<OdcTreeNode> Members

        /// <summary>
        /// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1"/>.</param>
        /// <returns>
        /// The index of <paramref name="item"/> if found in the list; otherwise, -1.
        /// </returns>
        public int IndexOf(OdcTreeNode item)
        {
            return list.IndexOf(item);
        }

        /// <summary>
        /// Inserts an item to the <see cref="T:System.Collections.Generic.IList`1"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="T:System.Collections.Generic.IList`1"/>.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.IList`1"/> is read-only.
        /// </exception>
        public void Insert(int index, OdcTreeNode item)
        {
            RegisterNode(item);
            list.Insert(index, item);
        }

        /// <summary>
        /// Removes the <see cref="T:System.Collections.Generic.IList`1"/> item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="index"/> is not a valid index in the <see cref="T:System.Collections.Generic.IList`1"/>.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.IList`1"/> is read-only.
        /// </exception>
        public void RemoveAt(int index)
        {
            OdcTreeNode node = this[index];
            UnRegisterNode(node);
            list.RemoveAt(index);
        }

        /// <summary>
        /// Gets or sets the <see cref="Senia.BBKing.WebConstrols.OdcTreeNode"/> at the specified index.
        /// </summary>
        /// <value></value>
        public OdcTreeNode this[int index]
        {
            get { return list[index]; }
            set { list[index] = value; }
        }

        /// <summary>
        /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public void Add(OdcTreeNode item)
        {
            RegisterNode(item);
            list.Add(item);
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public void Clear()
        {
            foreach (OdcTreeNode node in this)
            {
                UnRegisterNode(node);
            }
            list.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
        /// </returns>
        public bool Contains(OdcTreeNode item)
        {
            return list.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// 	<paramref name="arrayIndex"/> is less than 0.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// 	<paramref name="array"/> is multidimensional.
        /// -or-
        /// <paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>.
        /// -or-
        /// The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.
        /// -or-
        /// Type <paramref name="T"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.
        /// </exception>
        public void CopyTo(OdcTreeNode[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public int Count
        {
            get { return list.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </summary>
        /// <value></value>
        /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
        /// </returns>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
        /// <returns>
        /// true if <paramref name="item"/> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
        /// </exception>
        public bool Remove(OdcTreeNode item)
        {
            if (item != null) UnRegisterNode(item);
            return list.Remove(item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<OdcTreeNode> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        #region IStateManager Members

        /// <summary>
        /// When implemented by a class, gets a value indicating whether a server control is tracking its view state changes.
        /// </summary>
        /// <value></value>
        /// <returns>true if a server control is tracking its view state changes; otherwise, false.
        /// </returns>
        public bool IsTrackingViewState { get; internal set; }

        /// <summary>
        /// When implemented by a class, loads the server control's previously saved view state to the control.
        /// </summary>
        /// <param name="state">An <see cref="T:System.Object"/> that contains the saved view state values for the control.</param>
        public void LoadViewState(object state)
        {
            var bags = (object[]) state;
            Dictionary<int, OdcTreeNode> sorted = this.ToDictionary(x => x.Key);
            int index = 0;
            foreach (OdcTreeNode.TreeNodeState bag in bags)
            {
                OdcTreeNode node;
                if (sorted.ContainsKey(bag.key))
                {
                    node = sorted[bag.key];
                }
                else
                {
                    // this node was added during PopulateOnDemand or delayed loading:
                    node = new OdcTreeNode();
                    node.Key = bag.key;
                    Insert(index, node);
                }
                index++;
                node.LoadViewState(bag);
            }
        }

        /// <summary>
        /// When implemented by a class, saves the changes to a server control's view state to an <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Object"/> that contains the view state changes.
        /// </returns>
        public object SaveViewState()
        {
            var bags = new object[Count];
            int index = 0;
            foreach (OdcTreeNode node in this)
            {
                bags[index++] = node.SaveViewState();
            }
            return bags;
        }

        /// <summary>
        /// When implemented by a class, instructs the server control to track changes to its view state.
        /// </summary>
        public void TrackViewState()
        {
            IsTrackingViewState = true;
        }

        #endregion

        /// <summary>
        /// Prepare the node with additional data required for the TreeView.
        /// </summary>
        /// <param name="node">The node.</param>
        protected virtual void RegisterNode(OdcTreeNode node)
        {
            node.owner = Owner;
            if (TreeView != null)
            {
                TreeView.RegisterNode(node);
                foreach (OdcTreeNode sub in node.ChildNodes)
                {
                    node.ChildNodes.RegisterNode(sub);
                }
            }
        }

        /// <summary>
        /// Uns the register node.
        /// </summary>
        /// <param name="node">The node.</param>
        protected virtual void UnRegisterNode(OdcTreeNode node)
        {
            node.owner = null;
            TreeView.UnRegisterNode(node);
        }

        #region GetNode
        /// <summary>
        /// NodeMatch
        /// </summary>
        public delegate bool NodeMatcher(OdcTreeNode node);

        /// <summary>
        /// Gets the nodes by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public List<OdcTreeNode> GetNodesByKey(int key)
        {
            return GetNodes(delegate(OdcTreeNode node )
                                {
                                    return node.Key == key;
                                });
        }

        /// <summary>
        /// Gets the nodes.
        /// </summary>
        /// <param name="matcher">The matcher.</param>
        /// <returns></returns>
        public List<OdcTreeNode> GetNodes(NodeMatcher matcher)
        {
            var result = new List<OdcTreeNode>();
            GetNodes(result, matcher);
            return result;
        }
        /// <summary>
        /// Gets the nodes.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="matcher">The matcher.</param>
        public void GetNodes(List<OdcTreeNode> result, NodeMatcher matcher)
        {
            foreach (var node in this)
            {
                if (matcher(node))
                    result.Add(node);
                node.ChildNodes.GetNodes(result, matcher);

            }
        } 
        #endregion
    }
}