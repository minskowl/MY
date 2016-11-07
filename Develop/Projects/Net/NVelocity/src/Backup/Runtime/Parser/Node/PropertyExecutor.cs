using System;
using Introspector = NVelocity.Util.Introspection.Introspector;
using MethodInvocationException = NVelocity.Exception.MethodInvocationException;
using InternalContextAdapter = NVelocity.Context.InternalContextAdapter;
using NVelocity.App.Events;

namespace NVelocity.Runtime.Parser.Node {

    /// <summary>
    /// Returned the value of object property when executed.
    /// </summary>
    public class PropertyExecutor:AbstractExecutor {
	private String propertyUsed = null;

	public PropertyExecutor(RuntimeServices r, System.Type clazz, System.String propertyName) {
	    rsvc = r;

	    discover(clazz, propertyName);
	}

	protected internal virtual void discover(System.Type clazz, System.String propertyName) {
	    /*
	    *  this is gross and linear, but it keeps it straightforward.
	    */

	    try {
		Introspector introspector = rsvc.Introspector;
		propertyUsed = propertyName;
		property = introspector.getProperty(clazz, propertyUsed);
		if (property != null) {
		    return ;
		}

		/*
		*  now the convenience, flip the 1st character
		*/
		propertyUsed = propertyName.Substring(0,1).ToUpper() + propertyName.Substring(1);
		property = introspector.getProperty(clazz, propertyUsed);
		if (property != null) {
		    return ;
		}

		propertyUsed = propertyName.Substring(0,1).ToLower() + propertyName.Substring(1);
		property = introspector.getProperty(clazz, propertyUsed);
		if (property != null) {
		    return ;
		}

		// check for a method that takes no arguments
		propertyUsed = propertyName;
		method = introspector.getMethod(clazz, propertyUsed, new Object[0]);
		if (method != null) {
		    return;
		}

		// check for a method that takes no arguments, flipping 1st character
		propertyUsed = propertyName.Substring(0,1).ToUpper() + propertyName.Substring(1);
		method = introspector.getMethod(clazz, propertyUsed, new Object[0]);
		if (method != null) {
		    return;
		}

		propertyUsed = propertyName.Substring(0,1).ToLower() + propertyName.Substring(1);
		method = introspector.getMethod(clazz, propertyUsed, new Object[0]);
		if (method != null) {
		    return;
		}
	    } catch (System.Exception e) {
		rsvc.error("PROGRAMMER ERROR : PropertyExector() : " + e);
	    }
	}

	/// <summary> Execute method against context.
	/// </summary>
	public override System.Object execute(System.Object o, InternalContextAdapter context) {
	    if (property == null && method == null)
		return null;

	    try {
		if (property != null) {
		    return property.GetValue(o, null);
		} else {
		    return method.Invoke(o, new Object[0]);
		}
	    } catch (System.Reflection.TargetInvocationException ite) {
		EventCartridge ec = context.EventCartridge;

		/*
		*  if we have an event cartridge, see if it wants to veto
		*  also, let non-Exception Throwables go...
		*/

		if (ec != null && ite.GetBaseException() is System.Exception) {
		    try {
			return ec.methodException(o.GetType(), propertyUsed, (System.Exception) ite.GetBaseException());
		    } catch (System.Exception e) {
			throw new MethodInvocationException("Invocation of property '" + propertyUsed + "'" + " in  " + o.GetType() + " threw exception " + ite.GetBaseException().GetType() + " : " + ite.GetBaseException().Message, ite.GetBaseException(), propertyUsed);
		    }
		} else {
		    /*
		    * no event cartridge to override. Just throw
		    */

		    throw new MethodInvocationException("Invocation of property '" + propertyUsed + "'" + " in  " + o.GetType() + " threw exception " + ite.GetBaseException().GetType() + " : " + ite.GetBaseException().Message, ite.GetBaseException(), propertyUsed);
		}
	    } catch (System.ArgumentException iae) {
		return null;
	    }
	}
    }
}
