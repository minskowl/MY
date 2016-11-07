using System;
using Introspector = NVelocity.Util.Introspection.Introspector;
using MethodInvocationException = NVelocity.Exception.MethodInvocationException;
using NVelocity.Runtime;

namespace NVelocity.Runtime.Parser.Node {

    /// <summary>  Handles discovery and valuation of a
    /// boolean object property, of the
    /// form public boolean is<property> when executed.
    ///
    /// We do this separately as to preserve the current
    /// quasi-broken semantics of get<as is property>
    /// get< flip 1st char> get("property") and now followed
    /// by is<Property>
    /// </summary>
    public class BooleanPropertyExecutor:PropertyExecutor {

	public BooleanPropertyExecutor(RuntimeServices r, System.Type clazz, System.String propertyName):base(r, clazz, propertyName) {}

	protected internal override void  discover(System.Type clazz, System.String propertyName) {
	    try {
		Introspector introspector = rsvc.Introspector;
		property = introspector.getProperty(clazz, propertyName);
		if (property != null) {
		    if (property.PropertyType.Equals(typeof(Boolean))) {
			return ;
		    }
		}

		/*
		*  now the convenience, flip the 1st character
		*/
		propertyName = propertyName.Substring(0,1).ToUpper() + propertyName.Substring(1);
		property = introspector.getProperty(clazz, propertyName);
		if (property != null)
		    if (property.PropertyType.Equals(typeof(Boolean))) {
			return ;
		    }

		property = null;
	    } catch (System.Exception e) {
		rsvc.error("PROGRAMMER ERROR : BooleanPropertyExector() : " + e);
	    }
	}
    }
}
