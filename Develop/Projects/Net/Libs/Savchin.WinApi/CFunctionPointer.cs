
using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

// csc /nologo /t:library /out:CFunctionPointer.Net.dll CFunctionPointer.cs
namespace Savchin.WinApi
{
    /// <summary>
    ///CFunctionPointer 
    /// </summary>
    public abstract class CFunctionPointer
    {
        #region ---- public stuff -----
        /// <summary>
        /// Invokes the specified args.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public object Invoke(params object[] args)
        {
            ArrayList handles = new ArrayList(); // GCHandle(s) to free
            object[] margs = new object[args.Length];
            try
            {
                // ---- Marshalling ----
                if (args.Length != officiousSignature.arg.Length)
                    throw new ArgumentException("incorrect number of arguments");
                for (int i = 0; i < args.Length; i++)
                {
                    if (!officialSignature.arg[i].IsInstanceOfType(args[i]))
                        throw new ArgumentException("argument " + i + " is not of the correct type");

                    if (officiousSignature.arg[i].IsInstanceOfType(args[i]))
                        margs[i] = args[i];
                    else if (args[i] is IConvertible && typeof(IConvertible).IsAssignableFrom(officiousSignature.arg[i]))
                        margs[i] = Convert.ChangeType(args[i], officiousSignature.arg[i]);
                    else if (officiousSignature.arg[i].Equals(typeof(IntPtr)) && args[i] == null)
                        margs[i] = IntPtr.Zero;
                    else if (officialSignature.arg[i].IsArray)
                    {
                        handles.Add(GCHandle.Alloc(args[i], GCHandleType.Pinned));
                        margs[i] = Marshal.UnsafeAddrOfPinnedArrayElement(args[i] as Array, 0);
                    }
                    else if (args[i] is string && officiousSignature.arg[i].Equals(typeof(IntPtr)))
                        margs[i] = Marshal.StringToHGlobalAnsi(args[i] as string);
                    else
                        throw new ArgumentException("argument " + i + " incorrect");
                }

                // ---- Call it ! ----
                object o = InvokeImpl(margs);

                // ---- Unmarshalling ----
                return o;
            }
            finally
            {
                // ---- free marshalling handle ----
                foreach (GCHandle gch in handles)
                    gch.Free();

                for (int i = 0; i < margs.Length; i++)
                {
                    if (typeof(string).IsAssignableFrom(officialSignature.arg[i]))
                    {
                        IntPtr p = (IntPtr)margs[i];
                        if (p != IntPtr.Zero)
                            Marshal.FreeCoTaskMem(p);
                    }
                }
            }
        }

        // TODO marshal setting and calling convention		
        /** <summary> 
         * call the big create with CallingConvention.Cdecl
         * </summary>
         */
        public static CFunctionPointer Create(IntPtr fct, Type ret, params Type[] args)
        {
            return Create(fct, ret, CallingConvention.Cdecl, args);
        }




        /// <summary>
        /// Create a '.NET function' with th given signature and corresponding
        /// </summary>
        /// <param name="fct">The FCT.</param>
        /// <param name="ret">The ret.</param>
        /// <param name="call">The call.</param>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public static CFunctionPointer Create(IntPtr fct, Type ret, CallingConvention call, params Type[] args)
        {
            if (fct == IntPtr.Zero)
                throw new NullReferenceException("Function Pointer null");

            Signature sign = new Signature(ret, call, args);
            Signature realSign = sign.Marshalled();
            Type t = classCache[realSign] as Type;
            if (t == null)
                classCache[realSign] = t = CreateSubclass(realSign);

            ConstructorInfo ctor = t.GetConstructor(new Type[0]);
            CFunctionPointer p = (CFunctionPointer)ctor.Invoke(new object[0]);
            p.functionPtr = fct;
            p.officialSignature = sign;
            p.officiousSignature = realSign;
            return p;
        }

        public IntPtr Function { get { return functionPtr; } }
        public Type Return { get { return officialSignature.ret; } }
        public Type[] Arguments
        {
            get
            {
                Type[] ret = new Type[officialSignature.arg.Length];
                for (int i = 0; i < ret.Length; i++)
                    ret[i] = officialSignature.arg[i];
                return ret;
            }
        }
        #endregion

        #region ---- private stuff ----
        protected IntPtr functionPtr; // the function to call
        protected Signature officialSignature;
        protected Signature officiousSignature;
        protected CFunctionPointer() // only me can instantiate !!!
        {
        }


        static Hashtable classCache = new Hashtable(); // cache[Signature] = Type
        protected class Signature
        {
            public Type ret;
            public Type[] arg;
            public CallingConvention call;
            public Signature(Type _ret, CallingConvention _call, Type[] _arg)
            {
                ret = _ret;
                call = _call;
                arg = _arg;
            }
            protected Signature() { }

            public Signature Marshalled()
            {
                Signature _ret = new Signature();

                // checking return
                /*if(ret.Equals(typeof(string)))  // TODO
                    _ret.ret = typeof(IntPtr);
                else */
                if (this.ret.IsValueType)
                    _ret.ret = ret;
                else
                    throw new ArgumentException("only string or value type return are supported");

                _ret.call = call;

                _ret.arg = new Type[arg.Length];
                for (int i = 0; i < arg.Length; i++)
                {
                    Type t = arg[i];
                    if (t.IsValueType)
                        _ret.arg[i] = t;
                    else if (t.Equals(typeof(string)) /*|| t.Equals(typeof(StringBuilder))*/) // TODO
                        _ret.arg[i] = typeof(IntPtr);
                    else if (t.IsArray && t.GetElementType().IsValueType)
                        _ret.arg[i] = typeof(IntPtr);
                    else
                        throw new ArgumentException("only string or value type or array of value type are supported arguments");
                }
                return _ret;
            }

            #region ------ overriden --------
            public override int GetHashCode()
            {
                int hc = ret.GetHashCode();
                for (int i = 0; i < arg.Length; i++)
                    hc ^= arg[i].GetHashCode();
                return hc;
            }
            public override bool Equals(object o)
            {
                if (o is Signature)
                {
                    Signature ci = (Signature)o;
                    if (!ret.Equals(ci.ret))
                        return false;
                    if (arg.Length != ci.arg.Length)
                        return false;
                    for (int i = 0; i < arg.Length; i++)
                        if (!arg[i].Equals(ci.arg[i]))
                            return false;
                    return true;
                }
                return false;
            }
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(ret);
                sb.Append(' ');
                sb.Append(call);
                sb.Append('(');
                for (int i = 0; i < arg.Length; i++)
                {
                    if (i != 0)
                        sb.Append(", ");
                    sb.Append(arg[i]);
                }
                sb.Append(')');
                return sb.ToString();
            }
            #endregion
        }

        // this method is dynamically emitted ...
        // call it with marshalled arguments
        protected abstract object InvokeImpl(object[] args);

        static Type CreateSubclass(Signature sign)
        {
            // generate all the wrapping ...
            AppDomain ad = Thread.GetDomain();
            AssemblyName asmn = new AssemblyName();
            asmn.Name = "CFctPtr-" + Guid.NewGuid();
            AssemblyBuilder asmb = ad.DefineDynamicAssembly(asmn,
                                                            AssemblyBuilderAccess.Run);
            ModuleBuilder modb = asmb.DefineDynamicModule("MainMod");
            TypeBuilder typb = modb.DefineType("CFctPtrImpl",
                                               TypeAttributes.Public,
                                               typeof(CFunctionPointer));
            MethodBuilder impl = typb.DefineMethod("InvokeImpl_Emited",
                                                   MethodAttributes.Virtual | MethodAttributes.Family,
                                                   typeof(object),
                                                   new Type[] { typeof(object[]) });
            // the real thing ...
            EmitImpl(impl, sign);

            MethodInfo declar = typeof(CFunctionPointer).GetMethod("InvokeImpl", BindingFlags.NonPublic | BindingFlags.Instance);
            typb.DefineMethodOverride(impl, declar);
            return typb.CreateType();
        }
        static void EmitImpl(MethodBuilder mbuilder, Signature sign)
        {
            ILGenerator ilgen = mbuilder.GetILGenerator();

            // push the arguments
            for (int i = 0; i < sign.arg.Length; i++)
            {
                ilgen.Emit(OpCodes.Ldarg_1); // push the array argument
                ilgen.Emit(OpCodes.Ldc_I4, i); // push an index
                ilgen.Emit(OpCodes.Ldelem_Ref); // fetch the object at index
                // convert it
                if (sign.arg[i].IsValueType)
                {
                    ilgen.Emit(OpCodes.Unbox, sign.arg[i]); // unbox it
                    ilgen.Emit(OpCodes.Ldobj, sign.arg[i]); // replace the ref by the val
                }
                else // bug, isn't it ? (no managed stuff here ...)
                    throw new FormatException("call C function with argument " + i + " managed");
            }

            // load the function address
            ilgen.Emit(OpCodes.Ldarg_0); // this
            FieldInfo pfctinf = typeof(CFunctionPointer).GetField("functionPtr", BindingFlags.NonPublic | BindingFlags.Instance);
            ilgen.Emit(OpCodes.Ldfld, pfctinf);

            // call the function
            ilgen.EmitCalli(OpCodes.Calli,
                            sign.call,
                            sign.ret,
                            sign.arg);

            // ensure something is returned
            if (sign.ret == typeof(void))
                ilgen.Emit(OpCodes.Ldnull);
            else if (sign.ret.IsValueType) // obviously ...
                ilgen.Emit(OpCodes.Box, sign.ret);
            else
                throw new FormatException("expecting managed return type from C function");

            // return
            ilgen.Emit(OpCodes.Ret);
        }
        #endregion
    }
}
