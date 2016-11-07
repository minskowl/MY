namespace NVelocity.Runtime.Parser.Node
{
    /*
    * The Apache Software License, Version 1.1
    *
    * Copyright (c) 2000-2001 The Apache Software Foundation.  All rights
    * reserved.
    *
    * Redistribution and use in source and binary forms, with or without
    * modification, are permitted provided that the following conditions
    * are met:
    *
    * 1. Redistributions of source code must retain the above copyright
    *    notice, this list of conditions and the following disclaimer.
    *
    * 2. Redistributions in binary form must reproduce the above copyright
    *    notice, this list of conditions and the following disclaimer in
    *    the documentation and/or other materials provided with the
    *    distribution.
    *
    * 3. The end-user documentation included with the redistribution, if
    *    any, must include the following acknowlegement:
    *       "This product includes software developed by the
    *        Apache Software Foundation (http://www.apache.org/)."
    *    Alternately, this acknowlegement may appear in the software itself,
    *    if and wherever such third-party acknowlegements normally appear.
    *
    * 4. The names "The Jakarta Project", "Velocity", and "Apache Software
    *    Foundation" must not be used to endorse or promote products derived
    *    from this software without prior written permission. For written
    *    permission, please contact apache@apache.org.
    *
    * 5. Products derived from this software may not be called "Apache"
    *    nor may "Apache" appear in their names without prior written
    *    permission of the Apache Group.
    *
    * THIS SOFTWARE IS PROVIDED ``AS IS'' AND ANY EXPRESSED OR IMPLIED
    * WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
    * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
    * DISCLAIMED.  IN NO EVENT SHALL THE APACHE SOFTWARE FOUNDATION OR
    * ITS CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
    * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
    * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF
    * USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
    * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
    * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
    * OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF
    * SUCH DAMAGE.
    * ====================================================================
    *
    * This software consists of voluntary contributions made by many
    * individuals on behalf of the Apache Software Foundation.  For more
    * information on the Apache Software Foundation, please see
    * <http://www.apache.org/>.
    */
    using System;
    using Parser = NVelocity.Runtime.Parser.Parser;
    using IntrospectionCacheData = NVelocity.Util.Introspection.IntrospectionCacheData;
    using Introspector = NVelocity.Util.Introspection.Introspector;
    using MethodInvocationException = NVelocity.Exception.MethodInvocationException;
    using InternalContextAdapter = NVelocity.Context.InternalContextAdapter;

    /// <summary>  ASTIdentifier.java
    /// *
    /// Method support for identifiers :  $foo
    /// *
    /// mainly used by ASTRefrence
    /// *
    /// Introspection is now moved to 'just in time' or at render / execution
    /// time. There are many reasons why this has to be done, but the
    /// primary two are   thread safety, to remove any context-derived
    /// information from class member  variables.
    /// *
    /// </summary>
    /// <author> <a href="mailto:jvanzyl@apache.org">Jason van Zyl</a>
    /// </author>
    /// <author> <a href="mailto:geirm@optonline.net">Geir Magnusson Jr.</a>
    /// </author>
    /// <version> $Id: ASTIdentifier.cs,v 1.4 2003/10/27 13:54:10 corts Exp $
    ///
    /// </version>
    public class ASTIdentifier:SimpleNode {
	private System.String identifier = "";

	public ASTIdentifier(int id):base(id) {}

	public ASTIdentifier(Parser p, int id):base(p, id) {}

	/// <summary>Accept the visitor. *
	/// </summary>
	public override System.Object jjtAccept(ParserVisitor visitor, System.Object data) {
	    return visitor.visit(this, data);
	}

	/// <summary>  simple init - don't do anything that is context specific.
	/// just get what we need from the AST, which is static.
	/// </summary>
	public override System.Object init(InternalContextAdapter context, System.Object data) {
	    base.init(context, data);

	    identifier = FirstToken.image;

	    return data;
	}

	/// <summary>  introspects the class to find the method name of the node,
	/// or if that fails, treats the reference object as a map
	/// and treats the identifier as a key in that map.
	/// This needs work.
	/// *
	/// </summary>
	/// <param name="data">Class to be introspected
	///
	/// </param>
	private AbstractExecutor doIntrospection(System.Type data) {
	    AbstractExecutor executor;

	    /*
	    *  first try for a getFoo() type of property
	    *  (also getfoo() )
	    */

	    executor = new PropertyExecutor(rsvc, data, identifier);

	    /*
	    *  if that didn't work, look for get("foo")
	    */

	    if (executor.isAlive() == false) {
		executor = new GetExecutor(rsvc, data, identifier);
	    }

	    /*
	    *  finally, look for boolean isFoo() 
	    */

	    if (executor.isAlive() == false) {
		executor = new BooleanPropertyExecutor(rsvc, data, identifier);
	    }

	    return executor;
	}

	/// <summary>  invokes the method on the object passed in
	/// </summary>
	public override System.Object execute(System.Object o, InternalContextAdapter context) {
	    AbstractExecutor executor = null;

	    try {
		System.Type c = o.GetType();

		/*
		*  first, see if we have this information cached.
		*/

		IntrospectionCacheData icd = context.ICacheGet(this);

		/*
		* if we have the cache data and the class of the object we are 
		* invoked with is the same as that in the cache, then we must
		* be allright.  The last 'variable' is the method name, and 
		* that is fixed in the template :)
		*/

		if (icd != null && icd.contextData == c) {
		    executor = (AbstractExecutor) icd.thingy;
		} else {
		    /*
		    *  otherwise, do the introspection, and cache it
		    */

		    executor = doIntrospection(c);

		    if (executor != null) {
			icd = new IntrospectionCacheData();
			icd.contextData = c;
			icd.thingy = executor;
			context.ICachePut(this, icd);
		    }
		}
	    } catch (System.Exception e) {
		rsvc.error("ASTIdentifier.execute() : identifier = " + identifier + " : " + e);
	    }

	    /*
	    *  we have no executor... punt...
	    */
	    if (executor == null) {
		return null;
	    }

	    /*
	    *  now try and execute.  If we get a MIE, throw that
	    *  as the app wants to get these.  If not, log and punt.
	    */
	    try {
		return executor.execute(o, context);
	    } catch (MethodInvocationException mie) {
		throw mie;
	    } catch (System.Exception e) {
		rsvc.error("ASTIdentifier() : exception invoking method for identifier '" + identifier + "' in " + o.GetType() + " : " + e);
	    }

	    return null;
	}
    }
}
