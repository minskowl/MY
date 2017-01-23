//using System;
//using System.Collections;
//using System.Threading;
//using Savchin.Application;

//namespace Savchin.Development
//{
//    /// <summary>
//    /// MultiThreadProvider
//    /// </summary>
//    public class MultiThreadProvider : BaseObjectProvider, IApplicationObjectsProvider
//    {
//        readonly SimpleSession _session = new SimpleSession();
//        readonly IDictionary _applicationState = new Hashtable();
//        private readonly LocalDataStoreSlot _threadLocalSlot = Thread.AllocateDataSlot();

//        /// <summary>
//        /// Gets the session.
//        /// </summary>
//        /// <value>The session.</value>
//        public override ISession  Session
//        {
//            get { return _session; }
//        }

//        /// <summary>
//        /// Gets the context.
//        /// </summary>
//        /// <value>The context.</value>
//        public IDictionary Context
//        {
//            get
//            {
//                var context = (Hashtable)Thread.GetData(_threadLocalSlot);
//                if (context != null) return context;

//                context = new Hashtable();
//                Thread.SetData(_threadLocalSlot, context);

//                return context;
//            }
//        }

//        /// <summary>
//        /// Gets the state of the application.
//        /// </summary>
//        /// <value>The state of the application.</value>
//        public IDictionary ApplicationState
//        {
//            get { return _applicationState; }
//        }
//    }
//}
