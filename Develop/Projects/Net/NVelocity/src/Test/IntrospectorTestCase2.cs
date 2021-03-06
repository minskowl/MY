namespace org.apache.velocity.test
{
    /*
    * The Apache Software License, Version 1.1
    *
    * Copyright (c) 2001 The Apache Software Foundation.  All rights
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

    /// <summary> Test case for the Velocity Introspector which
    /// tests the ability to find a 'best match'
    /// *
    /// *
    /// </summary>
    /// <author> <a href="mailto:geirm@apache.org">Geir Magnusson Jr.</a>
    /// </author>
    /// <version> $Id: IntrospectorTestCase2.cs,v 1.2 2003/10/27 13:54:11 corts Exp $
    ///
    /// </version>
    public class IntrospectorTestCase2:BaseTestCase {

	internal IntrospectorTestCase2():base("IntrospectorTestCase2") {}

	/// <summary> Creates a new instance.
	/// </summary>
	public IntrospectorTestCase2(System.String name):base(name) {}


	public virtual void  runTest() {
	    try {
		Velocity.init();

		System.Reflection.MethodInfo method;
		System.String result;
		Tester t = new Tester();

		System.Object[] params_Renamed = new System.Object[]{new Foo(), new Foo()};

		method = RuntimeSingleton.Introspector.getMethod(typeof(Tester), "find", params_Renamed);

		if (method == null)
		    fail("Returned method was null");

		result = (System.String) method.Invoke(t, (System.Object[]) params_Renamed);

		if (!result.Equals("Bar-Bar")) {
		    fail("Should have gotten 'Bar-Bar' : recieved '" + result + "'");
		}

		/*
		*  now test for failure due to ambiguity
		*/

		method = RuntimeSingleton.Introspector.getMethod(typeof(Tester2), "find", params_Renamed);

		if (method != null)
		    fail("Introspector shouldn't have found a method as it's ambiguous.");
	    } catch (System.Exception e) {
		fail(e.ToString());
	    }
	}

	public interface Woogie {}

    public class Bar : Woogie {
	    internal int i;
	}

    public class Foo:Bar {
	    internal int j;
	}

	public class Tester {
	    public static System.String find(Woogie w, System.Object o) {
		return "Woogie-Object";
	    }

	    public static System.String find(System.Object w, Bar o) {
		return "Object-Bar";
	    }

	    public static System.String find(Bar w, Bar o) {
		return "Bar-Bar";
	    }

	    public static System.String find(System.Object o) {
		return "Object";
	    }

	    public static System.String find(Woogie o) {
		return "Woogie";
	    }
	}

	public class Tester2 {
	    public static System.String find(Woogie w, System.Object o) {
		return "Woogie-Object";
	    }

	    public static System.String find(System.Object w, Bar o) {
		return "Object-Bar";
	    }

	    public static System.String find(Bar w, System.Object o) {
		return "Bar-Object";
	    }

	    public static System.String find(System.Object o) {
		return "Object";
	    }

	    public static System.String find(Woogie o) {
		return "Woogie";
	    }
	}
    }
}
