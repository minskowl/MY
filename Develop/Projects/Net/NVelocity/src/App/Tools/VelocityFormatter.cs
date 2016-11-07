namespace NVelocity.App.Tools
{
    /*
    * Copyright (c) 2001 The Java Apache Project.  All rights reserved.
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
    * 3. All advertising materials mentioning features or use of this
    *    software must display the following acknowledgment:
    *    "This product includes software developed by the Java Apache
    *    Project for use in the Apache JServ servlet engine project
    *    <http://java.apache.org/>."
    *
    * 4. The names "Apache JServ", "Apache JServ Servlet Engine", "Turbine",
    *    "Apache Turbine", "Turbine Project", "Apache Turbine Project" and
    *    "Java Apache Project" must not be used to endorse or promote products
    *    derived from this software without prior written permission.
    *
    * 5. Products derived from this software may not be called "Apache JServ"
    *    nor may "Apache" nor "Apache JServ" appear in their names without
    *    prior written permission of the Java Apache Project.
    *
    * 6. Redistributions of any form whatsoever must retain the following
    *    acknowledgment:
    *    "This product includes software developed by the Java Apache
    *    Project for use in the Apache JServ servlet engine project
    *    <http://java.apache.org/>."
    *
    * THIS SOFTWARE IS PROVIDED BY THE JAVA APACHE PROJECT "AS IS" AND ANY
    * EXPRESSED OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
    * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
    * PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL THE JAVA APACHE PROJECT OR
    * ITS CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
    * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
    * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
    * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
    * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
    * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
    * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED
    * OF THE POSSIBILITY OF SUCH DAMAGE.
    *
    * This software consists of voluntary contributions made by many
    * individuals on behalf of the Java Apache Group. For more information
    * on the Java Apache Project and the Apache JServ Servlet Engine project,
    * please see <http://java.apache.org/>.
    *
    */
    // Java Core Classes
    using System;
    using NVelocity.Context;
    using NVelocity;

    /// <summary> Formatting tool for inserting into the Velocity WebContext.  Can
    /// format dates or lists of objects.
    /// *
    /// <p>Here's an example of some uses:
    /// *
    /// <code><pre>
    /// $formatter.formatShortDate($object.Date)
    /// $formatter.formatLongDate($db.getRecord(232).getDate())
    /// $formatter.formatArray($array)
    /// $formatter.limitLen(30, $object.Description)
    /// </pre></code>
    ///
    /// </summary>
    /// <author> <a href="sean@somacity.com">Sean Legassick</a>
    /// </author>
    /// <author> <a href="dlr@collab.net">Daniel Rall</a>
    /// </author>
    /// <version> $Id: VelocityFormatter.cs,v 1.4 2003/10/27 13:54:08 corts Exp $
    ///
    /// </version>
    public class VelocityFormatter {
	private void  InitBlock() {
	    nf = SupportClass.TextNumberFormat.getTextNumberInstance();
	}
	internal IContext context = null;
	//UPGRADE_NOTE: The initialization of  'nf' was moved to method 'InitBlock'. 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="jlca1005"'
	internal SupportClass.TextNumberFormat nf;

	/// <summary> Constructor needs a backpointer to the context.
	/// *
	/// </summary>
	/// <param name="context">A Context.
	///
	/// </param>
	public VelocityFormatter(IContext context) {
	    InitBlock();
	    this.context = context;
	}

	/// <summary> Formats a date in 'short' style.
	/// *
	/// </summary>
	/// <param name="date">A Date.
	/// </param>
	/// <returns>A String.
	///
	/// </returns>
	public virtual System.String formatShortDate(System.DateTime date) {
	    return SupportClass.FormatDateTime(SupportClass.GetDateTimeFormatInstance(3, -1, System.Globalization.CultureInfo.CurrentCulture), date);
	}

	/// <summary> Formats a date in 'long' style.
	/// *
	/// </summary>
	/// <param name="date">A Date.
	/// </param>
	/// <returns>A String.
	///
	/// </returns>
	public virtual System.String formatLongDate(System.DateTime date) {
	    return SupportClass.FormatDateTime(SupportClass.GetDateTimeFormatInstance(1, -1, System.Globalization.CultureInfo.CurrentCulture), date);
	}

	/// <summary> Formats a date/time in 'short' style.
	/// *
	/// </summary>
	/// <param name="date">A Date.
	/// </param>
	/// <returns>A String.
	///
	/// </returns>
	public virtual System.String formatShortDateTime(System.DateTime date) {
	    return SupportClass.FormatDateTime(SupportClass.GetDateTimeFormatInstance(3, 3, System.Globalization.CultureInfo.CurrentCulture), date);
	}

	/// <summary> Formats a date/time in 'long' style.
	/// *
	/// </summary>
	/// <param name="date">A Date.
	/// </param>
	/// <returns>A String.
	///
	/// </returns>
	public virtual System.String formatLongDateTime(System.DateTime date) {
	    return SupportClass.FormatDateTime(SupportClass.GetDateTimeFormatInstance(1, 1, System.Globalization.CultureInfo.CurrentCulture), date);
	}

	/// <summary> Formats an array into the form "A, B and C".
	/// *
	/// </summary>
	/// <param name="array">An Object.
	/// </param>
	/// <returns>A String.
	///
	/// </returns>
	public virtual System.String formatArray(System.Object array) {
	    return formatArray(array, ", ", " and ");
	}

	/// <summary> Formats an array into the form
	/// "A&lt;delim&gt;B&lt;delim&gt;C".
	/// *
	/// </summary>
	/// <param name="array">An Object.
	/// </param>
	/// <param name="delim">A String.
	/// </param>
	/// <returns>A String.
	///
	/// </returns>
	public virtual System.String formatArray(System.Object array, System.String delim) {
	    return formatArray(array, delim, delim);
	}

	/// <summary> Formats an array into the form
	/// "A&lt;delim&gt;B&lt;finaldelim&gt;C".
	/// *
	/// </summary>
	/// <param name="array">An Object.
	/// </param>
	/// <param name="delim">A String.
	/// </param>
	/// <param name="finalDelim">A String.
	/// </param>
	/// <returns>A String.
	///
	/// </returns>
	public virtual System.String formatArray(System.Object array, System.String delim, System.String finaldelim) {

	    // TODO: if this is not right - it will blow up
	    Array a = (Array)array;

	    System.Text.StringBuilder sb = new System.Text.StringBuilder();
	    int arrayLen = ((double[]) array).Length;
	    for (int i = 0; i < arrayLen; i++) {
		// Use the Array.get method as this will automatically
		// wrap primitive types in a suitable Object-derived
		// wrapper if necessary.
		//UPGRADE_TODO: The equivalent in .NET for method 'java.Object.toString' may return a different value. 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="jlca1043"'
		//UPGRADE_ISSUE: Method 'java.lang.reflect.Array.get' was not converted. 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="jlca1000_javalangreflectArrayget_javalangObject_int"'

		//TODO: not sure if this is right
		//sb.Append(Array.get(array, i).ToString());
		sb.Append(a.GetValue(i).ToString());

		if (i < arrayLen - 2) {
		    sb.Append(delim);
		} else if (i < arrayLen - 1) {
		    sb.Append(finaldelim);
		}
	    }
	    return sb.ToString();
	}

	/// <summary> Formats a vector into the form "A, B and C".
	/// *
	/// </summary>
	/// <param name="vector">A Vector.
	/// </param>
	/// <returns>A String.
	///
	/// </returns>
	public virtual System.String formatVector(System.Collections.ArrayList vector) {
	    return formatVector(vector, ", ", " and ");
	}

	/// <summary> Formats a vector into the form "A&lt;delim&gt;B&lt;delim&gt;C".
	/// *
	/// </summary>
	/// <param name="vector">A Vector.
	/// </param>
	/// <param name="delim">A String.
	/// </param>
	/// <returns>A String.
	///
	/// </returns>
	public virtual System.String formatVector(System.Collections.ArrayList vector, System.String delim) {
	    return formatVector(vector, delim, delim);
	}

	/// <summary> Formats a vector into the form
	/// "Adelim&gt;B&lt;finaldelim&gt;C".
	/// *
	/// </summary>
	/// <param name="vector">A Vector.
	/// </param>
	/// <param name="delim">A String.
	/// </param>
	/// <param name="finalDelim">A String.
	/// </param>
	/// <returns>A String.
	///
	/// </returns>
	public virtual System.String formatVector(System.Collections.ArrayList vector, System.String delim, System.String finaldelim) {
	    System.Text.StringBuilder sb = new System.Text.StringBuilder();
	    for (int i = 0; i < vector.Count; i++) {
		//UPGRADE_TODO: The equivalent in .NET for method 'java.Object.toString' may return a different value. 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="jlca1043"'
		sb.Append(vector[i].ToString());
		if (i < vector.Count - 2) {
		    sb.Append(delim);
		} else if (i < vector.Count - 1) {
		    sb.Append(finaldelim);
		}
	    }
	    return sb.ToString();
	}

	/// <summary> Limits 'string' to 'maxlen' characters.  If the string gets
	/// curtailed, "..." is appended to it.
	/// *
	/// </summary>
	/// <param name="maxlen">An int with the maximum length.
	/// </param>
	/// <param name="string">A String.
	/// </param>
	/// <returns>A String.
	///
	/// </returns>
	public virtual System.String limitLen(int maxlen, System.String string_Renamed) {
	    return limitLen(maxlen, string_Renamed, "...");
	}

	/// <summary> Limits 'string' to 'maxlen' character.  If the string gets
	/// curtailed, 'suffix' is appended to it.
	/// *
	/// </summary>
	/// <param name="maxlen">An int with the maximum length.
	/// </param>
	/// <param name="string">A String.
	/// </param>
	/// <param name="suffix">A String.
	/// </param>
	/// <returns>A String.
	///
	/// </returns>
	public virtual System.String limitLen(int maxlen, System.String string_Renamed, System.String suffix) {
	    System.String ret = string_Renamed;
	    if (string_Renamed.Length > maxlen) {
		ret = string_Renamed.Substring(0, (maxlen - suffix.Length) - (0)) + suffix;
	    }
	    return ret;
	}

	/// <summary> Class that returns alternating values in a template.  It stores
	/// a list of alternate Strings, whenever alternate() is called it
	/// switches to the next in the list.  The current alternate is
	/// retrieved through toString() - i.e. just by referencing the
	/// object in a Velocity template.  For an example of usage see the
	/// makeAlternator() method below.
	/// </summary>
	//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'VelocityAlternator' to access its enclosing instance. 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="jlca1019"'
	public class VelocityAlternator {
	    private void  InitBlock(VelocityFormatter enclosingInstance) {
		this.enclosingInstance = enclosingInstance;
	    }
	    private VelocityFormatter enclosingInstance;
	    public VelocityFormatter Enclosing_Instance
	    {
		get {
		    return enclosingInstance;
		}

	    }
	    protected internal System.String[] alternates = null;
	    protected internal int current = 0;

	    /// <summary> Constructor takes an array of Strings.
	    /// *
	    /// </summary>
	    /// <param name="alternates">A String[].
	    ///
	    /// </param>
	    public VelocityAlternator(VelocityFormatter enclosingInstance, System.String[] alternates) {
		InitBlock(enclosingInstance);
		this.alternates = alternates;
	    }

	    /// <summary> Alternates to the next in the list.
	    /// *
	    /// </summary>
	    /// <returns>The current alternate in the sequence.
	    ///
	    /// </returns>
	    public virtual System.String alternate() {
		current++;
		current %= alternates.Length;
		return "";
	    }

	    /// <summary> Returns the current alternate.
	    /// *
	    /// </summary>
	    /// <returns>A String.
	    ///
	    /// </returns>
	    public override System.String ToString() {
		return alternates[current];
	    }
	}

	/// <summary> As VelocityAlternator, but calls <code>alternate()</code>
	/// automatically on rendering in a template.
	/// </summary>
	//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'VelocityAutoAlternator' to access its enclosing instance. 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="jlca1019"'
    public class VelocityAutoAlternator : VelocityAlternator {
	    private void  InitBlock(VelocityFormatter enclosingInstance) {
		this.enclosingInstance = enclosingInstance;
	    }
	    private VelocityFormatter enclosingInstance;
	    public new VelocityFormatter Enclosing_Instance
	    {
		get {
		    return enclosingInstance;
		}

	    }
	    /// <summary> Constructor takes an array of Strings.
	    /// *
	    /// </summary>
	    /// <param name="alternates">A String[].
	    ///
	    /// </param>
	    public VelocityAutoAlternator(VelocityFormatter enclosingInstance, System.String[] alternates):base(enclosingInstance, alternates) {
		InitBlock(enclosingInstance);
	    }

	    /// <summary> Returns the current alternate, and automatically alternates
	    /// to the next alternate in its sequence (trigged upon
	    /// rendering).
	    /// *
	    /// </summary>
	    /// <returns>The current alternate in the sequence.
	    ///
	    /// </returns>
	    public override System.String ToString() {
		System.String s = alternates[current];
		alternate();
		return s;
	    }
	}

	/// <summary> Makes an alternator object that alternates between two values.
	/// *
	/// <p>Example usage in a Velocity template:
	/// *
	/// <code><pre>
	/// &lt;table&gt;
	/// $formatter.makeAlternator("rowcolor", "#c0c0c0", "#e0e0e0")
	/// #foreach $item in $items
	/// #begin
	/// &lt;tr&gt;&lt;td bgcolor="$rowcolor"&gt;$item.Name&lt;/td&gt;&lt;/tr&gt;
	/// $rowcolor.alternate()
	/// #end
	/// &lt;/table&gt;
	/// </pre></code>
	/// *
	/// </summary>
	/// <param name="name">The name for the alternator int the context.
	/// </param>
	/// <param name="alt1">The first alternate.
	/// </param>
	/// <param name="alt2">The second alternate.
	/// </param>
	/// <returns>The newly created instance.
	///
	/// </returns>
	public virtual System.String makeAlternator(System.String name, System.String alt1, System.String alt2) {
	    System.String[] alternates = new System.String[]{alt1, alt2};
	    context.Put(name, new VelocityAlternator(this, alternates));
	    return "";
	}

	/// <summary> Makes an alternator object that alternates between three
	/// values.
	/// *
	/// </summary>
	/// <seealso cref=" #makeAlternator(String name, String alt1, String alt2)
	///
	/// "/>
	public virtual System.String makeAlternator(System.String name, System.String alt1, System.String alt2, System.String alt3) {
	    System.String[] alternates = new System.String[]{alt1, alt2, alt3};
	    context.Put(name, new VelocityAlternator(this, alternates));
	    return "";
	}

	/// <summary> Makes an alternator object that alternates between four values.
	/// *
	/// </summary>
	/// <seealso cref=" #makeAlternator(String name, String alt1, String alt2)
	///
	/// "/>
	public virtual System.String makeAlternator(System.String name, System.String alt1, System.String alt2, System.String alt3, System.String alt4) {
	    System.String[] alternates = new System.String[]{alt1, alt2, alt3, alt4};
	    context.Put(name, new VelocityAlternator(this, alternates));
	    return "";
	}

	/// <summary> Makes an alternator object that alternates between two values
	/// automatically.
	/// *
	/// </summary>
	/// <seealso cref=" #makeAlternator(String name, String alt1, String alt2)
	///
	/// "/>
	public virtual System.String makeAutoAlternator(System.String name, System.String alt1, System.String alt2) {
	    System.String[] alternates = new System.String[]{alt1, alt2};
	    context.Put(name, new VelocityAutoAlternator(this, alternates));
	    return "";
	}

	/// <summary> Returns a default value if the object passed is null.
	/// </summary>
	public virtual System.Object isNull(System.Object o, System.Object dflt) {
	    if (o == null) {
		return dflt;
	    } else {
		return o;
	    }
	}
    }
}
