/*
 * Alexey A. Popov
 * Copyright (c) 2004, Alexey A. Popov
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, are
 * permitted provided that the following conditions are met:
 *
 * - Redistributions of source code must retain the above copyright notice, this list
 *   of conditions and the following disclaimer.
 *
 * - Redistributions in binary form must reproduce the above copyright notice, this list
 *   of conditions and the following disclaimer in the documentation and/or other materials
 *   provided with the distribution.
 *
 * - Neither the name of the Alexey A. Popov nor the names of its contributors may be used to
 *   endorse or promote products derived from this software without specific prior written
 *   permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS &AS IS& AND ANY EXPRESS
 * OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
 * AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
 * IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
 * OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.IO;
using System.Text;

namespace Savchin.Html
{
	/// <summary>
	/// Represents an abstract node in HTML DOM tree.
	/// </summary>
	public abstract class HtmlNode: ICloneable
	{
	    private HtmlDocument _document;

	    private HtmlNode _parent;

	    /// <summary>
	    /// Creates and initialize a new HTML DOM node.
	    /// </summary>
	    protected HtmlNode()
	    {
	        _document = null;
	        _parent = null;
	    }

		/// <summary>
		/// Creates a deep copy of this node.
		/// </summary>
		/// <returns>The node's copy.</returns>
		public abstract object Clone();

        /// <summary>
        /// Gets if the object has attributes.
        /// </summary>
        /// <value>true if the object has attributes; false otherwise.</value>
        public virtual bool HasAttributes
        {
		    get
        	{
		        return false;
		    }
        }

		/// <summary>
	    /// The node's attributes.
	    /// </summary>
	    /// <value>
	    /// An <see cref="HtmlAttributeCollection"/> that holds the node's
	    /// attributes.
	    /// </value>
	    public virtual HtmlAttributeCollection Attributes
	    {
			get
			{
				return null;
			}
	    }

	    /// <summary>
	    /// The node's child nodes.
	    /// </summary>
	    /// <value>
	    /// An <see cref="HtmlNodeCollection"/> that holds the node's
	    /// child nodes list.
	    /// </value>
	    public virtual HtmlNodeCollection ChildNodes
	    {
	        get
	        {
	            return null;
	        }
	    }

	    /// <summary>
	    /// The node's owner HTML document.
	    /// </summary>
	    /// <value>
	    /// An <see cref="HtmlDocument"/>.
	    /// </value>
	    public HtmlDocument Document
	    {
	        get
	        {
	            return _document;
	        }
	    }

	    /// <summary>
	    /// This special method allows the implementation to change the node's
	    /// owner document.
	    /// </summary>
	    /// <param name="document">An <see cref="HtmlDocument"/> that becomes
	    /// the node's owner.</param>
	    /// <seealso cref="Document"/>
	    protected virtual void SetDocument(HtmlDocument document)
	    {
	        _document = document;
	    }

	    /// <summary>
	    /// The node's first child node, if any.
	    /// </summary>
	    /// <value>The node's first child <see cref="HtmlNode"/>.</value>
	    /// <remarks>Some nodes like <see cref="HtmlComment"/> have no
	    /// child nodes. For these node types the method returns null.</remarks>
	    /// <seealso cref="HasChildNodes"/>
	    public HtmlNode FirstChild
	    {
	        get
	        {
	            if(HasChildNodes)
	                return ChildNodes[0];
	            return null;
	        }
	    }

	    /// <summary>
	    /// Allows to check is the node has any child.
	    /// </summary>
	    /// <remarks>For nodes that cannot have children the value is always false.</remarks>
	    /// <value>true if the node has children, false otherwise.</value>
	    public bool HasChildNodes
	    {
	        get
	        {
	            if(null == ChildNodes)
	                return false;
	            return ChildNodes.Count > 0;
	        }
	    }

	    /// <summary>
	    /// The node's last child node, if any.
	    /// </summary>
	    /// <remarks>For nodes that cannot have children the value is always false.</remarks>
	    /// <value>The node's last child <see cref="HtmlNode"/>.</value>
	    /// <seealso cref="HasChildNodes"/>
	    public HtmlNode LastChild
	    {
	        get
	        {
	            if(HasChildNodes)
	                return ChildNodes[ChildNodes.Count - 1];
	            return null;
	        }
	    }

	    /// <summary>
	    /// Checks if the node is known, i.e. it's name is one of the standard
	    /// node names.
	    /// </summary>
	    public virtual bool IsKnown
	    {
	        get
	        {
	            return Tag.IsKnown(this);
	        }
	    }

	    /// <summary>
	    /// The qualified name of the node.
	    /// </summary>
	    /// <value>A <see cref="string"/> that hold the textual representation
	    /// of the node's name.</value>
	    /// <remarks>Note that some node types have no name or have names that
	    /// cannot be changed.</remarks>
	    public abstract string Name
	    {
	        get;
	    	set;
	    }

	    /// <summary>
	    /// The node's next sibling.
	    /// </summary>
	    /// <value>The <see cref="HtmlNode"/> - next sibling.</value>
	    public virtual HtmlNode NextSibling
	    {
	        get
	        {
	            if(null == ParentNode)
	                return null;

	            int index = ParentNode.ChildNodes.IndexOf(this);
	            if(index < ParentNode.ChildNodes.Count - 1)
	                return ParentNode.ChildNodes[index + 1];

	            return null;
	        }
	    }

	    /// <summary>
	    /// A node in HTML DOM tree that holds this node as a child node.
	    /// </summary>
	    /// <value>The parent <see cref="HtmlNode"/>.</value>
	    public virtual HtmlNode ParentNode
	    {
	        get
	        {
	            return _parent;
	        }
	    }

	    /// <summary>
	    /// This special method allows the implementation to change the node's
	    /// parent node.
	    /// </summary>
	    /// <param name="parent">An <see cref="HtmlNode"/> that becomes
	    /// the node's parent.</param>
	    /// <seealso cref="ParentNode"/>
	    protected void SetParentNode(HtmlNode parent)
	    {
	        _parent = parent;
	    }

	    /// <summary>
	    /// The node's previous sibling.
	    /// </summary>
	    /// <value>The <see cref="HtmlNode"/> - previous sibling.</value>
	    public virtual HtmlNode PreviousSibling
	    {
	        get
	        {
	            if(null == ParentNode)
	                return null;

	            int index = ParentNode.ChildNodes.IndexOf(this);
	            if(index > 0)
	                return ParentNode.ChildNodes[index - 1];

	            return null;
	        }
	    }

	    /// <summary>
	    /// The node's type.
	    /// </summary>
	    /// <value>
	    /// The node's type.
	    /// </value>
	    /// <seealso cref="HtmlNodeType"/>
	    public abstract HtmlNodeType Type
	    {
	        get;
	    }

	    /// <summary>
	    /// Checks if the object has a value.
	    /// </summary>
	    /// <value>true if the has a value; false otherwise.</value>
	    /// <remarks>Not all node objects have values.</remarks>
	    public virtual bool HasValue
	    {
	        get
	        {
	        	return false;
	        }
	    }

	    /// <summary>
	    /// The node's value.
	    /// </summary>
	    /// <value>The textual representation of the node's contents.</value>
	    /// <remarks>Although for some nodes the value of this property
	    /// is the same as the result of <see cref="ToString()"/> call,
	    /// it is not generally true. For instance, an
	    /// <see cref="HtmlElement"/> node that reprenents a tag has
	    /// different value and it's textual representation.</remarks>
	    public abstract string Value
	    {
	        get;
	    	set;
	    }

	    /// <summary>
	    /// Appends a child node to a list of child nodes of
	    /// the current node.
	    /// </summary>
	    /// <param name="node">Node to append.</param>
	    /// <returns>The appended node if successfully added.</returns>
	    /// <remarks>
	    /// This method sets up the <paramref name="node"/>'s owner document.
	    /// </remarks>
	    public HtmlNode AppendChild(HtmlNode node)
	    {
	        if(null != ChildNodes)
	        {
	            ChildNodes.Add(node);
	            node.SetDocument(this.Document);
	        	node.SetParentNode(this.ParentNode);
	        }

	        return node;
	    }

	    /// <summary>
	    /// Prepends a child node to the list of child nodes of
	    /// the current node.
	    /// </summary>
	    /// <param name="node">Node to append.</param>
	    /// <returns>The prepended node if successfully added.</returns>
	    /// <remarks>This method sets up the <paramref name="node"/>'s
	    /// owner document.</remarks>
	    public HtmlNode PrependChild(HtmlNode node)
	    {
	        if(null != ChildNodes)
	        {
	            ChildNodes.Insert(0, node);
	            node.SetDocument(this.Document);
	            node.SetParentNode(this.ParentNode);
	        }

	        return node;
	    }

	    /// <summary>
	    /// Append an attribute to the list of atributes of the current node.
	    /// </summary>
	    /// <param name="attr">The attribute to append.</param>
	    /// <returns>The node iself.</returns>
	    /// <remarks>This call returns the node this allowing chaned appedinging
	    /// of attributes.</remarks>
	    public HtmlNode AppendAttribute(HtmlAttribute attr)
	    {
	    	if(null != Attributes)
	    	{
	    		Attributes.Add(attr);
	    		attr.SetDocument(this.Document);
	    		attr.SetParentNode(this);
	    	}

	    	return this;
	    }

	    /// <summary>
	    /// Prepend an attribute to the list of atributes of the current node.
	    /// </summary>
	    /// <param name="attr">The attribute to prepend.</param>
	    /// <returns>The node iself.</returns>
	    /// <remarks>This call returns the node this allowing chaned appedinging
	    /// of attributes.</remarks>
	    public HtmlNode PrependAttribute(HtmlAttribute attr)
	    {
	    	if(null != Attributes)
	    	{
	    		Attributes.Insert(0, attr);
	    		attr.SetDocument(this.Document);
	    		attr.SetParentNode(this);
	    	}

	    	return this;
	    }

        /// <summary>
        /// Removes all the child nodes and/or attributes of
        /// the current node.
        /// </summary>
        public virtual void RemoveAll()
        {
            if(null != ChildNodes)
                ChildNodes.Clear();
            if(null != Attributes)
                Attributes.Clear();
        }

        /// <summary>
        /// Removes specified child node.
        /// </summary>
        /// <param name="oldChild">The node about to be removed.</param>
        /// <returns>The node removed.</returns>
        public virtual HtmlNode RemoveChild(HtmlNode oldChild)
        {
            if(null != ChildNodes)
                ChildNodes.Remove(oldChild);
            return oldChild;
        }

        /// <summary>
        /// Find a node by it's name.
        /// </summary>
        /// <param name="name">The name of the node to find.</param>
        /// <returns>A first node with the specified name.</returns>
        public virtual HtmlNode FindByName(string name)
        {
            if(null == ChildNodes)
                return null;
            foreach(HtmlNode node in ChildNodes)
                if(0 == String.Compare(node.Name, name, true))
                    return node;
            return null;
        }

	    /// <summary>
	    /// Normalizes the node.
	    /// </summary>
	    /// <remarks>
	    /// <para>The normalization depends on the node type. For text nodes,
	    /// all double-spaces are replaced with single spaces. For
	    /// container nodes adjacent text nodes are merged.</para>
	    /// <para>The <see cref="HtmlNode"/> provides a generic implementation
	    /// of this method.</para>
	    /// </remarks>
	    public virtual void Normalize()
	    {
	        if(null == ChildNodes)
	            return;

	        for(int i = 0; i < ChildNodes.Count - 1; i++)
	        {
	            Type curT = ChildNodes[i].GetType();
	            Type nextT = ChildNodes[i + 1].GetType();
	            if(curT.Equals(nextT))
	            {
	                if(curT == typeof(HtmlText))
	                {
	                    StringBuilder sb = new StringBuilder(ChildNodes[i].ToString());
	                    sb.Append(" ");
	                    sb.Append(ChildNodes[i + 1].ToString());
	                    ChildNodes[i].Value = sb.ToString();
	                    RemoveChild(ChildNodes[i + 1]);
	                    ChildNodes[i].Normalize();
	                    continue;
	                }
	                if(curT == typeof(HtmlWhitespace))
	                {
	                    ChildNodes[i].Normalize();
	                    RemoveChild(ChildNodes[i + 1]);
	                    continue;
	                }
                    ChildNodes[i].Normalize();
	            }
	            else
	                ChildNodes[i].Normalize();
	        }
	    }

	    /// <summary>
	    /// The textual representation of the node.
	    /// </summary>
	    /// <returns>The textual representation of the node.</returns>
	    /// <remarks>This method simply returns the node's value.</remarks>
	    /// <seealso cref="Value"/>
	    public override string ToString()
	    {
	        return Value;
	    }
	}
}
