using System;
using System.Reflection;

namespace Savchin.Core
{
    public static class Reflected
    {
        static object Get(this object src, string strName, BindingFlags type)
        {
            var bStatic = (src is Type);
            var t = bStatic ? (Type)src : src.GetType();
            var bindingFlags = BindingFlags.NonPublic | BindingFlags.Public |
                                        BindingFlags.DeclaredOnly |
                                        (bStatic ? BindingFlags.Static : BindingFlags.Instance) |
                                        type;
            object target = (bStatic ? null : src);

            return t.InvokeMember(strName, bindingFlags, null, target, null);
        }

        static object Set(this object src, string strName, BindingFlags type,object value)
        {
            var bStatic = (src is Type);
            var t = bStatic ? (Type)src : src.GetType();
            var bindingFlags = BindingFlags.NonPublic | BindingFlags.Public |
                                        BindingFlags.DeclaredOnly |
                                        (bStatic ? BindingFlags.Static : BindingFlags.Instance) |
                                        type;
            object target = (bStatic ? null : src);

            return t.InvokeMember(strName, bindingFlags, null, target, new object[] { value });
        }

    
        static T Get<T>(this object src, string strName, BindingFlags type)
        {
            return (T)Get(src, strName, type);
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src">The SRC.</param>
        /// <param name="strName">Name of the STR.</param>
        /// <returns></returns>
        public static T GetProperty<T>(this object src, string strName)
        {
            return (Get<T>(src, strName, BindingFlags.GetProperty));
        }

        /// <summary>
        /// Gets the field.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src">The SRC.</param>
        /// <param name="strName">Name of the STR.</param>
        /// <returns></returns>
        public static T GetField<T>(this object src, string strName)
        {
            return (Get<T>(src, strName, BindingFlags.GetField));
        }



        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <param name="strName">Name of the STR.</param>
        /// <returns></returns>
        public static object GetProperty(this object src, string strName)
        {
            return (Get(src, strName, BindingFlags.GetProperty));
        }

        /// <summary>
        /// Gets the field.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <param name="strName">Name of the STR.</param>
        /// <returns></returns>
        public static object GetField(this object src, string strName)
        {
            return (Get(src, strName, BindingFlags.GetField));
        }

        /// <summary>
        /// Sets the property.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <param name="strName">Name of the STR.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static object SetProperty(this object src, string strName,object value)
        {
            return Set(src, strName, BindingFlags.SetProperty,value);
        }

        /// <summary>
        /// Gets the field.
        /// </summary>
        /// <param name="src">The SRC.</param>
        /// <param name="strName">Name of the STR.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static object SetField(this object src, string strName, object value)
        {
            return Set(src, strName, BindingFlags.SetField, value);
        }
    }
}