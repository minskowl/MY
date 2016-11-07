using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Savchin.Threading.Tasks;

namespace Savchin.Threading
{
    /// <summary>
    /// Class for wrapping part of method code into an object
    /// that will be executed on Managed IOCP based ThreadPool.
    /// By default async code blocks uses AppDomain wide Managed
    /// IOCP based ThreadPool. If required developers can specify
    /// a different instance of Managed IOCP ThreadPool
    /// </summary>
    public class async
    {
        #region Static

        private static ThreadPool defaultThreadPool = new ThreadPool(1, 1);
        /// <summary>
        /// Gets the default thread pool.
        /// </summary>
        /// <value>The default thread pool.</value>
        public static ThreadPool DefaultThreadPool
        {
            get { return defaultThreadPool; }
        }

        static async()
        {
            AppDomain.CurrentDomain.DomainUnload += delegate { Close(); };
        }

        /// <summary>
        /// Initializes the AppDomain wide ManagedIOCP ThreadPool
        /// used by async object execution
        /// </summary>
        public static void Open()
        {
            lock (typeof(async))
            {
                if (defaultThreadPool == null)
                {
                    defaultThreadPool = new ThreadPool(1, 1);
                }
            }
        }

        /// <summary>
        /// Closes the AppDomain wide ManagedIOCP ThreadPool
        /// used by async object execution
        /// </summary>
        public static void Close()
        {
            lock (typeof(async))
            {
                if (defaultThreadPool != null)
                {
                    defaultThreadPool.Close();
                    defaultThreadPool = null;
                }
            }
        }

        #endregion

        #region Public Properties

        protected AsyncDelegate _ad;
        protected List<async> _dependentCodeBlockList = new List<async>();
        protected volatile int _dependentCount;
        protected AsyncCodeBlockExecutionCompleteCallback _executionCompleteCallback;
        private volatile bool _executionCompleted;
        protected SynchronizationContext _synchronizationContext;
        protected object _syncObject = new object();
        protected object _targetObject;
        private Type _targetType;
        protected ITask _task;
        protected ThreadPool threadPool;


        protected bool _waitable;
        /// <summary>
        /// Identifies whether the calling code wait on this
        /// async object until the code wrapped by it executed.
        /// true: Can wait, false: Cannot wait.
        /// For async objects this property always returns 'false'
        /// </summary>
        public bool Waitable
        {
            get { return _waitable; }
        }
        /// <summary>
        /// Gets a value indicating whether [execution completed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [execution completed]; otherwise, <c>false</c>.
        /// </value>
        public bool ExecutionCompleted
        {
            get { return _executionCompleted; }
        }

        /// <summary>
        /// Gets the SynchronizationContext associated with this instance of the
        /// async class
        /// </summary>
        public SynchronizationContext SynchronizationContext
        {
            get { return _synchronizationContext; }
        }

        #endregion

        #region Public Constructor(s)

        /// <summary>
        /// Creates an instance of async class, that executes
        /// the wrapped code block on the default AppDomain wide
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        public async(AsyncDelegate ad)
        {
            Initialize(ad, null, null, null);
        }

        /// <summary>
        /// Creates an instance of async class, that executes
        /// the wrapped code block on the default AppDomain wide
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="executionCompleteCallback">
        /// Delegate handler that will be called when the execution of the
        /// code block wrapped by this instance is completed. Dependent
        /// async objects will be scheduled for execution after the
        /// completion callback has executed
        /// </param>
        public async(AsyncDelegate ad, AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback)
        {
            Initialize(ad, null, null, executionCompleteCallback);
        }

        /// <summary>
        /// Creates an instance of async class, that executes
        /// the wrapped code block on the developer supplied
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="tp">Managed IOCP based ThreadPool object</param>
        public async(AsyncDelegate ad, ThreadPool tp)
        {
            Initialize(ad, tp, null, null);
        }

        /// <summary>
        /// Creates an instance of async class, that executes
        /// the wrapped code block on the developer supplied
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="tp">Managed IOCP based ThreadPool object</param>
        /// <param name="executionCompleteCallback">
        /// Delegate handler that will be called when the execution of the
        /// code block wrapped by this instance is completed. Dependent
        /// async objects will be scheduled for execution after the
        /// completion callback has executed
        /// </param>
        public async(AsyncDelegate ad, ThreadPool tp,
                     AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback)
        {
            Initialize(ad, tp, null, executionCompleteCallback);
        }

        /// <summary>
        /// Creates an instance of async class, that executes
        /// the wrapped code block on the default AppDomain wide
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="dependentOnAsync">
        /// async object on which the current instance of async 
        /// depends on. The code wrapped by the current instance 
        /// of async object will be executed after the code wrapped 
        /// by dependentOnAsync object has completed execution
        /// </param>
        public async(AsyncDelegate ad, async dependentOnAsync)
        {
            Initialize(ad, null, new[] { dependentOnAsync }, null);
        }

        /// <summary>
        /// Creates an instance of async class, that executes
        /// the wrapped code block on the default AppDomain wide
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="dependentOnAsync">
        /// async object on which the current instance of async 
        /// depends on. The code wrapped by the current instance 
        /// of async object will be executed after the code wrapped 
        /// by dependentOnAsync object has completed execution
        /// </param>
        /// <param name="executionCompleteCallback">
        /// Delegate handler that will be called when the execution of the
        /// code block wrapped by this instance is completed. Dependent
        /// async objects will be scheduled for execution after the
        /// completion callback has executed
        /// </param>
        public async(AsyncDelegate ad, async dependentOnAsync,
                     AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback)
        {
            Initialize(ad, null, new[] { dependentOnAsync }, executionCompleteCallback);
        }

        /// <summary>
        /// Creates an instance of async class, that executes
        /// the wrapped code block on the developer supplied
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="tp">Managed IOCP based ThreadPool object</param>
        /// <param name="dependentOnAsync">
        /// async object on which the current instance of async 
        /// depends on. The code wrapped by the current instance 
        /// of async object will be executed after the code wrapped 
        /// by dependentOnAsync object has completed execution
        /// </param>
        public async(AsyncDelegate ad, ThreadPool tp, async dependentOnAsync)
        {
            Initialize(ad, tp, new[] { dependentOnAsync }, null);
        }

        /// <summary>
        /// Creates an instance of async class, that executes
        /// the wrapped code block on the developer supplied
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="tp">Managed IOCP based ThreadPool object</param>
        /// <param name="dependentOnAsync">
        /// async object on which the current instance of async 
        /// depends on. The code wrapped by the current instance 
        /// of async object will be executed after the code wrapped 
        /// by dependentOnAsync object has completed execution
        /// </param>
        /// <param name="executionCompleteCallback">
        /// Delegate handler that will be called when the execution of the
        /// code block wrapped by this instance is completed. Dependent
        /// async objects will be scheduled for execution after the
        /// completion callback has executed
        /// </param>
        public async(AsyncDelegate ad, ThreadPool tp, async dependentOnAsync,
                     AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback)
        {
            Initialize(ad, tp, new[] { dependentOnAsync }, executionCompleteCallback);
        }

        /// <summary>
        /// Creates an instance of async class, that executes
        /// the wrapped code block on the developer supplied
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">
        /// Anonymous delegate wrapping the code block to execute
        /// </param>
        /// <param name="tp">Managed IOCP based ThreadPool object</param>
        /// <param name="dependentOnAsync">
        /// async object array on which the current instance of async 
        /// depends on. The code wrapped by the current instance 
        /// of async object will be executed after the code wrapped 
        /// by dependentOnAsync object array has completed execution
        /// </param>
        public async(AsyncDelegate ad, ThreadPool tp, async[] arrDependentOnAsync)
        {
            Initialize(ad, tp, arrDependentOnAsync, null);
        }

        /// <summary>
        /// Creates an instance of async class, that executes
        /// the wrapped code block on the developer supplied
        /// Managed IOCP based ThreadPool
        /// </summary>
        /// <param name="ad">Anonymous delegate wrapping the code block to execute</param>
        /// <param name="tp">Managed IOCP based ThreadPool object</param>
        /// <param name="arrDependentOnAsync">The arr dependent on async.</param>
        /// <param name="executionCompleteCallback">Delegate handler that will be called when the execution of the
        /// code block wrapped by this instance is completed. Dependent
        /// async objects will be scheduled for execution after the
        /// completion callback has executed</param>
        public async(AsyncDelegate ad, ThreadPool tp, async[] arrDependentOnAsync,
                     AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback)
        {
            Initialize(ad, tp, arrDependentOnAsync, executionCompleteCallback);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the value of any local variable used within
        /// the code wrapped by this async object
        /// </summary>
        /// <param name="name">Name of the local variable</param>
        /// <returns>Value of the local variable</returns>
        public object GetObject(string name)
        {
            object obj = _targetType.InvokeMember(name, BindingFlags.GetField, null, _targetObject, null);
            return obj;
        }

        /// <summary>
        /// Executes the given AsyncDelegate in the SynchronizationContext associated with this
        /// instance of async class
        /// </summary>
        /// <param name="ad">AsyncDelegate object</param>
        public void ExecuteInSychronizationContext(AsyncDelegate ad)
        {
            if (_synchronizationContext != null)
            {
                _synchronizationContext.Send(
                    Delegate.CreateDelegate(typeof(SendOrPostCallback), ad.Method) as SendOrPostCallback,
                    null);
            }
            else
            {
                throw new InvalidOperationException(
                    "SynchronizationContext object is not available to execute the supplied code");
            }
        }

        #endregion

        #region Public Virtual Methods

        /// <summary>
        /// Returns the Exception object if the execution of the code 
        /// wrapped by this async object threw any exception
        /// </summary>
        public virtual Exception CodeException
        {
            get
            {
                Exception ex = null;
                var adt = _task as AsyncDelegateTask;
                if (adt != null)
                {
                    if (adt.CodeException != null)
                        ex = adt.CodeException;
                }
                return ex;
            }
        }

        /// <summary>
        /// Calling code cannot wait for code execution completion
        /// that is wrapped by async objects
        /// </summary>
        /// <param name="msWaitTime"></param>
        /// <returns></returns>
        public virtual bool Wait(int msWaitTime)
        {
            return false;
        }

        #endregion


        #region Protected Methods

        protected virtual void Initialize(AsyncDelegate ad, ThreadPool tp, async[] arrDependentOnAsync,
                                          AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback)
        {
            Initialize(ad, tp, arrDependentOnAsync, executionCompleteCallback, false);
        }


        protected void Initialize(AsyncDelegate ad, ThreadPool tp, async[] arrDependentOnAsync,
                                  AsyncCodeBlockExecutionCompleteCallback executionCompleteCallback, bool waitable)
        {
            if (ad == null) return;

            _waitable = waitable;
            _ad = ad;
            threadPool = tp;
            _targetObject = ad.Target;
            _targetType = ad.Method.DeclaringType;
            if (_waitable)
            {
                _task = new WaitableAsyncDelegateTask(ad) { TaskCompleted = MarkCompleted };
     
            }
            else
            {
                _task = new AsyncDelegateTask(ad) { TaskCompleted = MarkCompleted };
 
            }
            _executionCompleteCallback = executionCompleteCallback;
            bool dispatchForExecution = true;
            if (arrDependentOnAsync != null)
            {
                lock (_syncObject)
                {
                    foreach (async asyncObj in arrDependentOnAsync)
                    {
                        if (asyncObj.AddToDependencyCodeBlockList(this)) _dependentCount++;
                    }
                    if (_dependentCount == 0)
                        dispatchForExecution = true;
                    else
                        dispatchForExecution = false;
                }
            }
            // Store the current SynchronizationContext
            //
            _synchronizationContext = SynchronizationContext.Current;
            if (dispatchForExecution)
            {
                Execute(ad, _task, tp);
            }
        }

        #endregion

        #region Private Methods

        private bool AddToDependencyCodeBlockList(async asyncObj)
        {
            bool added = false;
            lock (_dependentCodeBlockList)
            {
                if (_executionCompleted == false)
                {
                    _dependentCodeBlockList.Add(asyncObj);
                    added = true;
                }
            }
            return added;
        }

        private void MarkCompleted()
        {
            // Execute the execution completion callback
            //
            if (_executionCompleteCallback != null) _executionCompleteCallback(this);

            // Schedule execution of dependent async objects
            //
            lock (_dependentCodeBlockList)
            {
                _executionCompleted = true;
                // Dispatch all dependent async code blocks for
                // execution
                //
                if (_dependentCodeBlockList.Count > 0)
                {
                    foreach (async asyncObj in _dependentCodeBlockList)
                    {
                        asyncObj.ExecuteSelf();
                    }
                    // Release our references to the dependent async objects
                    //
                    _dependentCodeBlockList.Clear();
                }
            }
        }
        private void ExecuteSelf()
        {
            lock (_syncObject)
            {
                if ((_dependentCount > 0) && (--_dependentCount == 0))
                {
                    Execute(_ad, _task, threadPool);
                }
            }
        }

        private static void Execute(AsyncDelegate ad, ITask task, ThreadPool tp)
        {
            if (defaultThreadPool != null)
            {
                if (tp == null)
                {
                    defaultThreadPool.Dispatch(task);
                }
                else
                {
                    tp.Dispatch(task);
                }
            }
            else
            {
                throw new ApplicationException("Thread Pool used by AsynchronousCodeBlock class is closed. " +
                                               "Cannot execute any more asynchronous code blocks on default Thread pool. Please open the " +
                                               "Thread Pool used by AsynchronousCodeBlock class or supply your own Thread Pool object for " +
                                               "asynchronous code block");
            }
        }
        #endregion






        /*
         * Used for serialization and deserialization of async code blocks
         * 
        private static AssemblyBuilder _asmBuilder = null;
        private static ModuleBuilder _modBuilder = null;
        private Dictionary<string, Sonic.Net.ThreadPool> _threadPoolTable = new Dictionary<string,ThreadPool>();
        private Dictionary<string, Type> _asyncTypeTable = new Dictionary<string,Type>();
        private Dictionary<Type, MethodInfo> _asyncTypeMethodTable = new Dictionary<Type, MethodInfo>();
        private Dictionary<Type, Dictionary<string, object>> _asyncTypeObjectTable = new Dictionary<Type, Dictionary<string, object>>();


        /*
         * Used for deserializing async code blocks
         * 
        static async()
        {
            AssemblyName asmName = new AssemblyName("ACBAssembly");
            _asmBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                asmName, AssemblyBuilderAccess.Run);
            _modBuilder = _asmBuilder.DefineDynamicModule("ACBModule", true);
        }
         /*
         * Used for deserialization of async code blocks
         * 
        public async(SerializableCodeBlock scb)
        {
            lock (_asyncTypeTable)
            {
                if (_asyncTypeTable.ContainsKey(scb.TypeName) == false)
                {
                    // Fill in our async object with the data from SCB
                    //
                    // Create the type
                    //
                    TypeBuilder typBuilder = _modBuilder.DefineType(scb.TypeName, TypeAttributes.Class | TypeAttributes.Public);
                    if (scb.InstanceID != 0)
                    {
                        foreach (FieldValue fv in scb.FieldValueList)
                        {
                            // Add fields to the type
                            //
                            typBuilder.DefineField(fv.Name, fv.Value.GetType(), FieldAttributes.Public);
                        }
                    }
                    // Add the method to our type
                    //
                    MethodBuilder methBuilder = typBuilder.DefineMethod(scb.MethodName,
                        (scb.InstanceID == 0) ? MethodAttributes.Static | MethodAttributes.Public :
                        MethodAttributes.Public);
                    ILGenerator ilg = methBuilder.GetILGenerator();
                    ilg.EmitWriteLine(scb.MethodName);
                    // Add the type to our type table
                    //
                    Type asyncType = typBuilder.CreateType();
                    _asyncTypeTable[scb.TypeName] = asyncType;
                    // Add the async method to our type method table
                    //
                    MethodInfo asyncMethInfo = asyncType.GetMethod(scb.MethodName);
                    _asyncTypeMethodTable[asyncType] = asyncMethInfo;
                    // Add the IL to our method
                    //
                    // Get the token for our method
                    //
                    MethodToken methToken = methBuilder.GetToken();
                    // Get the pointer to the method body.
                    GCHandle hmem = GCHandle.Alloc((Object)scb.MethodIL, GCHandleType.Pinned);
                    IntPtr addr = hmem.AddrOfPinnedObject();
                    int cbSize = scb.MethodIL.Length;
                    // Swap the old method body with the new body.
                    MethodRental.SwapMethodBody(
                                    asyncType,
                                    methToken.Token,
                                    addr,
                                    cbSize,
                                    MethodRental.JitOnDemand);
                    AsyncDelegate asyncDlg = Delegate.CreateDelegate(
                        typeof(AsyncDelegate), asyncMethInfo) as 
                        AsyncDelegate;
                    asyncDlg();
                }                
            }
        }

                #region Public Static Conversions

        /*
         * More research is required in the area of serializing and deserializing
         * async code blocks. Here is the status...
         * 
         * Pending creation of MSIL for the anonymous method body.
         * MethodBody.GetILAsByteArray() is only giving the raw IL Opcodes.
         * Opcodes for code size, maxstacksize and local variable initialization
         * are missing. Also even if I was able to serialize and deserialize the
         * IL of the method body somehow, I need to figure out a way to add any 
         * references to the assemblies used by method body, while executing the
         * deserialized code block
         * 
        public static explicit operator SerializableCodeBlock(async objAsync)
        {
            // Create a SerializableCodeBlock object from async object data
            //
            SerializableCodeBlock scb = new SerializableCodeBlock();
            if (objAsync._targetType != null)
                scb.TypeName = objAsync._targetType.FullName;
            else
                scb.TypeName = "T" + Guid.NewGuid().ToString().Replace("-", "");
            if (objAsync._targetObject != null)
            {
                scb.InstanceID = objAsync._targetObject.GetHashCode();
                FieldInfo[] arrFI = objAsync._targetType.GetFields();
                FieldValue[] arrFV = new FieldValue[arrFI.Length];
                int i = 0;
                foreach (FieldInfo fi in arrFI)
                {
                    object val = objAsync._targetType.InvokeMember(fi.Name, BindingFlags.GetField, null, objAsync._targetObject, null);
                    arrFV[i] = new FieldValue();
                    arrFV[i].Name = fi.Name;
                    arrFV[i].Value = val;
                    i++;
                }
                scb.FieldValueList = arrFV;
            }
            scb.MethodName = objAsync._ad.Method.Name;
            if (objAsync.threadPool != null) scb.ThreadPoolID = objAsync.threadPool.GetHashCode().ToString();
            MethodBody mb = objAsync._ad.Method.GetMethodBody();
            byte[] il = mb.GetILAsByteArray();
            List<byte> codeSizeList = new List<byte>();
            codeSizeList.Add(0x03);
            codeSizeList.Add(0x30);
            codeSizeList.Add(0x0A);
            codeSizeList.Add(0x00);
            codeSizeList.Add((byte)il.Length);                // code size
            codeSizeList.Add(0x00);
            codeSizeList.Add(0x00);
            codeSizeList.Add(0x00);
            codeSizeList.Add(0x00);
            codeSizeList.Add(0x00);
            codeSizeList.Add(0x00);
            codeSizeList.Add(0x00);
            codeSizeList.AddRange(il);
            scb.MethodIL = codeSizeList.ToArray();
            return scb;
        }
  
  
         * 
         */
    }
}