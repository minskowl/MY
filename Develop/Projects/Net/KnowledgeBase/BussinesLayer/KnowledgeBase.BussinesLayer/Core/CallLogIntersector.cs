using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;

namespace KnowledgeBase.BussinesLayer.Core
{
    public class CallLogIntersector : StandardInterceptor
    {
        /// <summary>
        /// Intercepts the specified invocation.
        /// </summary>
        /// <param name="invocation">The invocation.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public override object Intercept(IInvocation invocation, params object[] args)
        {

            return base.Intercept(invocation, args);
        }
    }
}
