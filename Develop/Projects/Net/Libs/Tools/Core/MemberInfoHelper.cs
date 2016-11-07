using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Savchin.Core
{
    /// <summary>
    /// MemberInfoHelper
    /// </summary>
    public static class MemberInfoHelper
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public static object GetValue(this MemberInfo member, object o)
        {
            return (member is PropertyInfo) ?
                ((PropertyInfo)member).GetValue(o, null) :
                ((FieldInfo)member).GetValue(o);
        }
    }
}
