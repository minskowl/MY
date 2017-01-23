using System.Collections;
using System.Collections.Generic;
using Savchin.Application;

namespace Savchin.Development
{
    /// <summary>
    /// SingleThreadProvider
    /// </summary>
    public class SingleThreadProvider : BaseObjectProvider, IApplicationObjectsProvider
    {
     
        readonly SimpleSession _session = new SimpleSession();
        readonly IDictionary _context = new Dictionary<object,object>();
        readonly IDictionary _applicationState= new Dictionary<object, object>();



        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <value>The session.</value>
        public override ISession Session
        {
            get { return _session; }
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        public IDictionary Context
        {
            get { return _context; }
        }

        /// <summary>
        /// Gets the state of the application.
        /// </summary>
        /// <value>The state of the application.</value>
        public IDictionary ApplicationState
        {
            get { return _applicationState; }
        }

       
    }
}