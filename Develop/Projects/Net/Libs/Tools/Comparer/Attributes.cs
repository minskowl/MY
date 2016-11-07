using System;

namespace Savchin.Comparer
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,AllowMultiple=false, Inherited = false)]
    public class CompareAttribute : Attribute
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class IgnoreCompareAttribute : Attribute
    {

    }


    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class CompareKeyAttribute : Attribute
    {
        public string KeyPropertyName { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="CompareKeyAttribute"/> class.
        /// </summary>
        /// <param name="keyPropertyName">Name of the key property.</param>
        public CompareKeyAttribute(string keyPropertyName)
        {
            KeyPropertyName = keyPropertyName;
        }


    }
}
