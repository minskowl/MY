using System;
using System.Collections;

namespace NVelocity.Runtime.Resource {

    /// <summary> Default implementation of the resource cache for the default
    /// ResourceManager.
    /// *
    /// </summary>
    /// <author> <a href="mailto:geirm@apache.org">Geir Magnusson Jr.</a>
    /// </author>
    /// <author> <a href="mailto:dlr@finemaltcoding.com">Daniel Rall</a>
    /// </author>
    /// <version> $Id: ResourceCacheImpl.cs,v 1.4 2003/10/27 13:54:11 corts Exp $
    ///
    /// </version>
    public class ResourceCacheImpl : ResourceCache {

	/// <summary>
	/// Cache storage, assumed to be thread-safe.
	/// </summary>
	//UPGRADE_NOTE: The initialization of  'cache' was moved to method 'InitBlock'. 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="jlca1005"'
	protected internal Hashtable cache = new System.Collections.Hashtable();

	/// <summary>
	/// Runtime services, generally initialized by the
	/// <code>initialize()</code> method.
	/// </summary>
	protected internal RuntimeServices rsvc = null;

	public ResourceCacheImpl() {}

	public virtual void  initialize(RuntimeServices rs) {
	    rsvc = rs;

	    rsvc.info("ResourceCache : initialized. (" + this.GetType() + ")");
	}

	public virtual Resource get
	    (System.Object key) {
	    return (Resource) cache[key];
	}

	public virtual Resource put(System.Object key, Resource value_Renamed) {
	    Object o = cache[key];
	    cache[key] = value_Renamed;
	    return (Resource) o;
	}

	public virtual Resource remove
	    (System.Object key) {
	    Object o = cache[key];
	    cache.Remove(key);
	    return (Resource) o;
	}

	public virtual IEnumerator enumerateKeys() {
	    return cache.Keys.GetEnumerator();
	}
    }
}
