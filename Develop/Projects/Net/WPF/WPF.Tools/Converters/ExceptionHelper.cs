using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Savchin.Wpf.Converters
{
    public class ExceptionHelper
    {
        private static readonly IDictionary<Assembly, XDocument> _exceptionInfos = (IDictionary<Assembly, XDocument>)new Dictionary<Assembly, XDocument>();
        private static readonly object _exceptionInfosLock = new object();
        private const string _typeAttributeName = "type";
        private readonly Type _forType;

        static ExceptionHelper()
        {
        }

        public ExceptionHelper(Type forType)
        {
            ArgumentHelperExtensions.AssertNotNull<Type>(forType, "forType");
            this._forType = forType;
        }

        [DebuggerHidden]
        public Exception Resolve(string exceptionKey, params object[] messageArgs)
        {
            return this.Resolve(exceptionKey, (object[])null, (Exception)null, messageArgs);
        }

        [DebuggerHidden]
        public Exception Resolve(string exceptionKey, Exception innerException, params object[] messageArgs)
        {
            return this.Resolve(exceptionKey, (object[])null, innerException, messageArgs);
        }

        [DebuggerHidden]
        public Exception Resolve(string exceptionKey, object[] constructorArgs, Exception innerException)
        {
            return this.Resolve(exceptionKey, constructorArgs, innerException, (object[])null);
        }

        [DebuggerHidden]
        public Exception Resolve(string exceptionKey, object[] constructorArgs, params object[] messageArgs)
        {
            return this.Resolve(exceptionKey, constructorArgs, (Exception)null, messageArgs);
        }

        [DebuggerHidden]
        public Exception Resolve(string exceptionKey, object[] constructorArgs, Exception innerException, params object[] messageArgs)
        {
            ArgumentHelperExtensions.AssertNotNull<string>(exceptionKey, "exceptionKey");
            XElement xelement = Enumerable.FirstOrDefault<XElement>(Enumerable.Select(Enumerable.Where(Enumerable.SelectMany(ExceptionHelper.GetExceptionInfo(this._forType.Assembly).Element((XName)"exceptionHelper").Elements((XName)"exceptionGroup"), (Func<XElement, IEnumerable<XElement>>)(exceptionGroup => exceptionGroup.Elements((XName)"exception")), (exceptionGroup, exception) => new
            {
                exceptionGroup = exceptionGroup,
                exception = exception
            }), param0 =>
            {
                if (string.Equals(param0.exceptionGroup.Attribute((XName)"type").Value, this._forType.FullName, StringComparison.Ordinal))
                    return string.Equals(param0.exception.Attribute((XName)"key").Value, exceptionKey, StringComparison.Ordinal);
                else
                    return false;
            }), param0 => param0.exception));
            if (xelement == null)
            {
                throw new InvalidOperationException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "The exception details for key '{0}' could not be found at /exceptionHelper/exceptionGroup[@type'{1}']/exception[@key='{2}'].", (object)exceptionKey, (object)this._forType, (object)exceptionKey));
            }
            else
            {
                XAttribute xattribute = xelement.Attribute((XName)"type");
                if (xattribute == null)
                {
                    throw new InvalidOperationException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "The '{0}' attribute could not be found for exception with key '{1}'", new object[2]
          {
            (object) "type",
            (object) exceptionKey
          }));
                }
                else
                {
                    Type type = Type.GetType(xattribute.Value);
                    if (type == null)
                        throw new InvalidOperationException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "Type '{0}' could not be loaded for exception with key '{1}'", new object[2]
            {
              (object) xattribute.Value,
              (object) exceptionKey
            }));
                    else if (!typeof(Exception).IsAssignableFrom(type))
                    {
                        throw new InvalidOperationException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "Type '{0}' for exception with key '{1}' does not inherit from '{2}'", (object)type.FullName, (object)exceptionKey, (object)typeof(Exception).FullName));
                    }
                    else
                    {
                        string format = xelement.Value.Trim();
                        if (messageArgs != null && messageArgs.Length > 0)
                            format = string.Format((IFormatProvider)CultureInfo.InvariantCulture, format, messageArgs);
                        List<object> list = new List<object>();
                        list.Add((object)format);
                        if (constructorArgs != null)
                            list.AddRange((IEnumerable<object>)constructorArgs);
                        if (innerException != null)
                            list.Add((object)innerException);
                        object[] args = list.ToArray();
                        BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
                        ConstructorInfo constructorInfo = (ConstructorInfo)null;
                        try
                        {
                            object state;
                            constructorInfo = (ConstructorInfo)Type.DefaultBinder.BindToMethod(bindingAttr, (MethodBase[])type.GetConstructors(bindingAttr), ref args, (ParameterModifier[])null, (CultureInfo)null, (string[])null, out state);
                        }
                        catch (MissingMethodException ex)
                        {
                        }
                        if (constructorInfo != null)
                            return (Exception)constructorInfo.Invoke(args);
                        throw new InvalidOperationException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "An appropriate constructor could not be found for exception type '{0}, for exception with key '{1}'", new object[2]
            {
              (object) type.FullName,
              (object) exceptionKey
            }));
                    }
                }
            }
        }

        [DebuggerHidden]
        public void ResolveAndThrowIf(bool condition, string exceptionKey, params object[] messageArgs)
        {
            if (condition)
                throw this.Resolve(exceptionKey, messageArgs);
        }

        [DebuggerHidden]
        public void ResolveAndThrowIf(bool condition, string exceptionKey, Exception innerException, params object[] messageArgs)
        {
            if (condition)
                throw this.Resolve(exceptionKey, innerException, messageArgs);
        }

        [DebuggerHidden]
        public void ResolveAndThrowIf(bool condition, string exceptionKey, object[] constructorArgs, Exception innerException)
        {
            if (condition)
                throw this.Resolve(exceptionKey, constructorArgs, innerException);
        }

        [DebuggerHidden]
        public void ResolveAndThrowIf(bool condition, string exceptionKey, object[] constructorArgs, params object[] messageArgs)
        {
            if (condition)
                throw this.Resolve(exceptionKey, constructorArgs, messageArgs);
        }

        [DebuggerHidden]
        public void ResolveAndThrowIf(bool condition, string exceptionKey, object[] constructorArgs, Exception innerException, params object[] messageArgs)
        {
            if (condition)
                throw this.Resolve(exceptionKey, constructorArgs, innerException, messageArgs);
        }

        [DebuggerHidden]
        private static XDocument GetExceptionInfo(Assembly assembly)
        {
            XDocument xdocument = (XDocument)null;
            lock (ExceptionHelper._exceptionInfosLock)
            {
                if (ExceptionHelper._exceptionInfos.ContainsKey(assembly))
                {
                    xdocument = ExceptionHelper._exceptionInfos[assembly];
                }
                else
                {
                    string local_2 = new AssemblyName(assembly.FullName).Name + ".Properties.ExceptionHelper.xml";
                    using (Stream resource_1 = assembly.GetManifestResourceStream(local_2))
                    {
                        if (resource_1 == null)
                        {
                            throw new InvalidOperationException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "XML resource file '{0}' could not be found in assembly '{1}'.", new object[2]
              {
                (object) local_2,
                (object) assembly.FullName
              }));
                        }
                        else
                        {
                            using (StreamReader resource_0 = new StreamReader(resource_1))
                                xdocument = XDocument.Load((TextReader)resource_0);
                        }
                    }
                    ExceptionHelper._exceptionInfos[assembly] = xdocument;
                }
            }
            return xdocument;
        }
    }
}
