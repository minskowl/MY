using System;
using MethodInvocationException = NVelocity.Exception.MethodInvocationException;
using InternalContextAdapter = NVelocity.Context.InternalContextAdapter;

namespace NVelocity.Runtime.Parser.Node {

    /// <summary> Abstract class that is used to execute an arbitrary
    /// method that is in introspected. This is the superclass
    /// for the GetExecutor and PropertyExecutor.
    /// </summary>
    public abstract class AbstractExecutor {
	protected internal RuntimeServices rsvc = null;

	/// <summary> Method to be executed.
	/// </summary>
	protected internal System.Reflection.MethodInfo method = null;
	protected internal System.Reflection.PropertyInfo property = null;

	/// <summary> Execute method against context.
	/// </summary>
	public abstract System.Object execute(System.Object o, InternalContextAdapter context);

	/// <summary> Tell whether the executor is alive by looking
	/// at the value of the method.
	/// </summary>
	public virtual bool isAlive() {
	    return (method != null || property != null);
	}
    }
}
