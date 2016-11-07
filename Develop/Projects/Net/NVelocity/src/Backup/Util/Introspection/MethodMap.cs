using System;
using System.Collections;

namespace NVelocity.Util.Introspection {

    public class MethodMap {
	public MethodMap() {}

	/// <summary> Keep track of all methods with the same name.
	/// </summary>
	//UPGRADE_NOTE: The initialization of  'methodByNameMap' was moved to method 'InitBlock'. 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="jlca1005"'
	internal Hashtable methodByNameMap = new System.Collections.Hashtable();

	/// <summary> Add a method to a list of methods by name.
	/// For a particular class we are keeping track
	/// of all the methods with the same name.
	/// </summary>
	public virtual void add
	    (System.Reflection.MethodInfo method) {
	    System.String methodName = method.Name;

	    IList l = (IList) methodByNameMap[methodName];

	    if (l == null) {
		l = new ArrayList();
		methodByNameMap[methodName] = l;
	    }

	    l.Add(method);

	    return ;
	}

	/// <summary> Return a list of methods with the same name.
	/// </summary>
	/// <param name="String">key
	/// </param>
	/// <returns>List list of methods
	///
	/// </returns>
	public virtual IList get
	    (System.String key) {
	    return (IList) methodByNameMap[key];
	}

	/// <summary>  <p>
	/// Find a method.  Attempts to find the
	/// most appropriate method using the
	/// sense of 'specificity'.
	/// </p>
	///
	/// <p>
	/// This turns out to be a relatively rare case
	/// where this is needed - however, functionality
	/// like this is needed.  This may not be the
	/// optimum approach, but it works.
	/// </p>
	/// </summary>
	/// <param name="String">name of method
	/// </param>
	/// <param name="Object[]">params
	/// </param>
	/// <returns>Method
	///
	/// </returns>
	public virtual System.Reflection.MethodInfo find(System.String methodName, System.Object[] params_Renamed) {
	    IList methodList = (IList) methodByNameMap[methodName];

	    if (methodList == null) {
		return null;
	    }

	    System.Type[] parameterTypes = null;
	    System.Reflection.MethodInfo method = null;

	    int numMethods = methodList.Count;

	    int bestDistance = - 2;
	    System.Reflection.MethodInfo bestMethod = null;
	    Twonk bestTwonk = null;
	    bool ambiguous = false;

	    for (int i = 0; i < numMethods; i++) {
		method = (System.Reflection.MethodInfo) methodList[i];
		parameterTypes = GetMethodParameterTypes(method);

		/*
		* The methods we are trying to compare must
		* the same number of arguments.
		*/

		if (parameterTypes.Length == params_Renamed.Length) {
		    /*
		    *  use the calling parameters as the baseline
		    *  and calculate the 'distance' from the parameters
		    *  to the method args.  This will be useful when
		    *  determining specificity
		    */

		    Twonk twonk = calcDistance(params_Renamed, parameterTypes);

		    if (twonk != null) {
			/*
			*  if we don't have anything yet, take it
			*/

			if (bestTwonk == null) {
			    bestTwonk = twonk;
			    bestMethod = method;
			} else {
			    /*
			    * now see which is more specific, this current
			    * versus what we think of as the best candidate
			    */

			    int val = twonk.moreSpecific(bestTwonk);

			    //System.out.println("Val = " + val + " for " + method + " vs " + bestMethod );

			    if (val == 0) {
				/*
				* this means that the parameters 'crossed'
				* therefore, it's ambiguous because one is as 
				* good as the other
				*/
				ambiguous = true;
			    } else if (val == 1) {
				/*
				*  the current method is clearly more
				*  specific than the current best, so
				*  we take the current we are testing
				*  and clear the ambiguity flag
				*/
				ambiguous = false;
				bestTwonk = twonk;
				bestMethod = method;
			    }
			}
		    }

		}
	    }

	    /*
	    *  if ambiguous is true, it means we couldn't decide
	    *  so inform the caller...
	    */

	    if (ambiguous) {
		throw new AmbiguousException();
	    }

	    return bestMethod;
	}

	/// <summary>  Calculates the distance, expressed as a vector of inheritance
	/// steps, between the calling args and the method args.
	/// There still is an issue re interfaces...
	/// </summary>
	private Twonk calcDistance(System.Object[] set
				       , System.Type[] base_Renamed) {
	    if (set.Length != base_Renamed.Length)
		return null;

	    Twonk twonk = new Twonk(set.Length);

	    int distance = 0;

	    for (int i = 0; i < set.Length; i++) {
		/*
		* can I get from here to there?
		*/

		System.Type setclass = set
					   [i].GetType();

		if (!base_Renamed[i].IsAssignableFrom(set
						      [i].GetType()))
		    return null;

		/*
		* ok, I can.  How many steps?
		*/

		System.Type c = setclass;

		while (c != null) {
		    /*
		    * is this a valid step?
		    */

		    if (!base_Renamed[i].IsAssignableFrom(c)) {
			/*
			*  it stopped being assignable - therefore we are looking at
			*  an interface as our target, so move back one step
			*  from the distance as the stop wasn't valid
			*/
			break;
		    }

		    if (base_Renamed[i].Equals(c)) {
			/*
			*  we are equal, so no need to move forward
			*/

			break;
		    }

		    c = c.BaseType;
		    twonk.distance++;
		    twonk.vec[i]++;
		}
	    }

	    return twonk;
	}

	private static System.Type[] GetMethodParameterTypes(System.Reflection.MethodInfo method) {
	    System.Reflection.ParameterInfo[] parms = method.GetParameters();
	    Type[] types  = new Type[parms.Length];

	    for (Int32 i = 0; i<parms.Length; i++) {
		types[i] = parms[i].ParameterType;
	    }

	    return types;
	}


    }
}
