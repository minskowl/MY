using System;
using ParseException = NVelocity.Runtime.Parser.ParseException;
using SimpleNode = NVelocity.Runtime.Parser.Node.SimpleNode;
using ResourceLoader = NVelocity.Runtime.Resource.Loader.ResourceLoader;
using ResourceNotFoundException = NVelocity.Exception.ResourceNotFoundException;
using ParseErrorException = NVelocity.Exception.ParseErrorException;


namespace NVelocity.Runtime.Resource {

    /// <summary> This class represent a general text resource that
    /// may have been retrieved from any number of possible
    /// sources.
    /// *
    /// </summary>
    /// <author> <a href="mailto:jvanzyl@apache.org">Jason van Zyl</a>
    /// </author>
    /// <author> <a href="mailto:geirm@optonline.net">Geir Magnusson Jr.</a>
    /// </author>
    /// <version> $Id: Resource.cs,v 1.4 2003/10/27 13:54:10 corts Exp $
    ///
    /// </version>
    public abstract class Resource {
	private void  InitBlock() {
	    encoding = NVelocity.Runtime.RuntimeConstants_Fields.ENCODING_DEFAULT;
	}
	public virtual RuntimeServices RuntimeServices {
	    set {
		rsvc = value;
	    }

	}
	public virtual long ModificationCheckInterval {
	    set {
		this.modificationCheckInterval = value;
	    }

	}
	public virtual System.String Name {
	    get {
		return name;
	    }

	    set {
		this.name = value;
	    }

	}
	public virtual System.String Encoding {
	    get {
		return encoding;
	    }

	    set {
		this.encoding = value;
	    }

	}
	public virtual long LastModified {
	    get {
		return lastModified;
	    }

	    set {
		this.lastModified = value;
	    }

	}
	public virtual ResourceLoader ResourceLoader {
	    get {
		return resourceLoader;
	    }

	    set {
		this.resourceLoader = value;
	    }

	}
	public virtual System.Object Data {
	    get {
		return data;
	    }

	    set {
		this.data = value;
	    }

	}
	protected internal RuntimeServices rsvc = null;

	/// <summary> The template loader that initially loaded the input
	/// stream for this template, and knows how to check the
	/// source of the input stream for modification.
	/// </summary>
	protected internal ResourceLoader resourceLoader;

	/// <summary> The number of milliseconds in a minute, used to calculate the
	/// check interval.
	/// </summary>
	protected internal const long MILLIS_PER_SECOND = 1000;

	/// <summary> How often the file modification time is checked (in milliseconds).
	/// </summary>
	protected internal long modificationCheckInterval = 0;

	/// <summary> The file modification time (in milliseconds) for the cached template.
	/// </summary>
	protected internal long lastModified = 0;

	/// <summary> The next time the file modification time will be checked (in
	/// milliseconds).
	/// </summary>
	protected internal long nextCheck = 0;

	/// <summary>  Name of the resource
	/// </summary>
	protected internal System.String name;

	/// <summary>  Character encoding of this resource
	/// </summary>
	//UPGRADE_NOTE: The initialization of  'encoding' was moved to method 'InitBlock'. 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="jlca1005"'
	protected internal System.String encoding;

	///
	/// <summary>  Resource might require ancillary storage of some kind
	/// </summary>
	protected internal System.Object data = null;

	///
	/// <summary>  Default constructor
	/// </summary>
	public Resource() {
	    InitBlock();
	}


	/// <summary> Perform any subsequent processing that might need
	/// to be done by a resource. In the case of a template
	/// the actual parsing of the input stream needs to be
	/// performed.
	/// </summary>
	public abstract bool Process();

	public virtual bool IsSourceModified() {
	    return resourceLoader.isSourceModified(this);
	}

	/// <summary> Set the modification check interval.
	/// </summary>
	/// <param name="interval">The interval (in minutes).
	///
	/// </param>

	/// <summary> Is it time to check to see if the resource
	/// source has been updated?
	/// </summary>
	public virtual bool RequiresChecking() {
	    /*
	    *  short circuit this if modificationCheckInterval == 0
	    *  as this means "don't check"
	    */

	    if (modificationCheckInterval <= 0) {
		return false;
	    }

	    /*
	    *  see if we need to check now
	    */

	    return ((System.DateTime.Now.Ticks - 621355968000000000) / 10000 >= nextCheck);
	}

	/// <summary> 'Touch' this template and thereby resetting
	/// the nextCheck field.
	/// </summary>
	public virtual void  Touch() {
	    nextCheck = (System.DateTime.Now.Ticks - 621355968000000000) / 10000 + (MILLIS_PER_SECOND * modificationCheckInterval);
	}

	/// <summary> Set the name of this resource, for example
	/// test.vm.
	/// </summary>

	/// <summary> Get the name of this template.
	/// </summary>

	/// <summary>  set the encoding of this resource
	/// for example, "ISO-8859-1"
	/// </summary>

	/// <summary>  get the encoding of this resource
	/// for example, "ISO-8859-1"
	/// </summary>


	/// <summary> Return the lastModifed time of this
	/// template.
	/// </summary>

	/// <summary> Set the last modified time for this
	/// template.
	/// </summary>

	/// <summary> Return the template loader that pulled
	/// in the template stream
	/// </summary>

	/// <summary> Set the template loader for this template. Set
	/// when the Runtime determines where this template
	/// came from the list of possible sources.
	/// </summary>

	///
	/// <summary> Set arbitrary data object that might be used
	/// by the resource.
	/// </summary>

	/// <summary> Get arbitrary data object that might be used
	/// by the resource.
	/// </summary>
    }
}
