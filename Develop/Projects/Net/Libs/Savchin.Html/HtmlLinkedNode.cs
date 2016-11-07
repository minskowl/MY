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
using System.Collections;
using System.IO;
using System.Text;

using Savchin.Html;

namespace Savchin.Html
{
	/// <summary>
	/// Represents a node that may have child nodes (a container for child nodes).
	/// </summary>
	public abstract class HtmlLinkedNode: HtmlNode
	{
	    private HtmlNodeCollection _childNodes;
	    private HtmlAttributeCollection _attributes;

		/// <summary>
		/// Initializes a new instance of the <see cref="HtmlLinkedNode"/>
		/// </summary>
		public HtmlLinkedNode()
		{
			_childNodes = new HtmlNodeCollection();
		    _attributes = new HtmlAttributeCollection();
		}

	    /// <summary>
	    /// The node's child nodes.
	    /// </summary>
	    /// <value>
	    /// An <see cref="HtmlNodeCollection"/> that holds the node's
	    /// child nodes list.
	    /// </value>
	    public override HtmlNodeCollection ChildNodes
	    {
	        get
	        {
	            return _childNodes;
	        }
	    }

		/// <summary>
		/// Checks if the node has attributes.
		/// </summary>
		/// <value>true if the node has attributes; false otherwise.</value>
		public override bool HasAttributes
		{
		    get
		    {
		        if((null == _attributes) ||
		           (0 == _attributes.Count))
		           return false;
		        return true;
		    }
		}

		/// <summary>
		/// Gets an <see cref="HtmlAttributeCollection"/> containing
		/// the attributes of the element.
		/// </summary>
		/// <value>An <paramref cref="HtmlAttributeCollection"/> containing
		/// the attributes of the element.</value>
		public override HtmlAttributeCollection Attributes
		{
			get
			{
				return _attributes;
			}
		}

	    /// <summary>
	    /// Loads and parses an HTML file from a <see cref="HtmlReader"/>.
	    /// </summary>
	    /// <param name="reader">The <see cref="HtmlReader"/> to read HTML
	    /// text from.</param>
	    /// <remarks>
	    /// <para>This method is the one that actually loads and parses
	    /// the HTML text.</para>
	    /// <para>The caller is responsible for disposing
	    /// the <paramref name="reader"/> if required.</para>
	    /// </remarks>
	    public void Load(HtmlReader reader)
	    {
	    	HtmlNode toAdd;
	    	Stack nodeStack = new Stack();
	    	nodeStack.Push(this);
	    	while(reader.Read())
	    	{
	    		toAdd = null;

	    		switch(reader.NodeType)
	    		{
	    			case HtmlNodeType.Comment:
   						toAdd = new HtmlComment(reader.Value);
    					break;
	    			case HtmlNodeType.DocumentType:
   						toAdd = new HtmlDocumentType();
    					break;
	    			case HtmlNodeType.Tag:
	    			{
	    				HtmlElement element = new HtmlElement(reader.Name);
    					if(Tag.IsAtomic(reader.Name))
    						toAdd = element;
    					else
    						nodeStack.Push(element);
	    				break;
	    			}
	    			case HtmlNodeType.EndTag:
	    			{
    					HtmlElement element = (HtmlElement)nodeStack.Pop();
    					if(0 != nodeStack.Count)
    						toAdd = element;
    					// And if the stack is empty  we, probably, reached
    					// the end of the document
	    				break;
	    			}
	    			case HtmlNodeType.ClosedTag:
   						toAdd = new HtmlElement(reader.Name);
    					break;
	    			case HtmlNodeType.Text:
   						toAdd = new HtmlText(reader.Value);
	    				break;
	    			case HtmlNodeType.Whitespace:
   						toAdd = new HtmlWhitespace(reader.Value);
	    				break;
	    		}
	    		if(null != toAdd)
	    			((HtmlNode)nodeStack.Peek()).ChildNodes.Add(toAdd);
	    	}
	    }

	    private static void SaveNode(HtmlNode node, HtmlWriter writer)
	    {
	        foreach(HtmlNode n in node.ChildNodes)
	        {
	            switch(n.Type)
	            {
	                case HtmlNodeType.Attribute:
	                    break;
	                case HtmlNodeType.Comment:
	                    break;
	                case HtmlNodeType.Document:
	                    break;
	                case HtmlNodeType.DocumentType:
	                    break;
	                case HtmlNodeType.Tag:
	                    break;
	                case HtmlNodeType.EndTag:
	                    break;
	                case HtmlNodeType.Text:
	                    break;
	            }
	        }
	    }

        /// <summary>
        /// Saves the node's contents to the specified <see cref="HtmlWriter"/>.
        /// </summary>
        /// <param name="writer">An <see cref="HtmlWriter"/> to write HTML
        /// text to.</param>
        /// <seealso cref="HtmlWriter"/>
	    public void Save(HtmlWriter writer)
	    {
	        SaveNode(this, writer);
	    }
	}
}
