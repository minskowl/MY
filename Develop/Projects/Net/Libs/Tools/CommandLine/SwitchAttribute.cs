using System;

namespace Savchin.CommandLine
{
    /// <summary>
    /// SwitchAttribute
    /// </summary>
    [AttributeUsageAttribute(AttributeTargets.Property,AllowMultiple = false)]
    public class SwitchAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the names.
        /// </summary>
        /// <value>The names.</value>
        public string[] Names { get; set; }
        /// <summary>
        /// Gets or sets the help.
        /// </summary>
        /// <value>The help.</value>
        public string Help { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SwitchAttribute"/> is required.
        /// </summary>
        /// <value><c>true</c> if required; otherwise, <c>false</c>.</value>
        public bool Required { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchAttribute"/> class.
        /// </summary>
        /// <param name="names">The names.</param>
        /// <param name="help">The help.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        public SwitchAttribute(string[] names, string help, bool required)
        {
            Names = names;
            Help = help;
            Required = required;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="help">The help.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        public SwitchAttribute(string name, string help, bool required)
        {
            Names = new [] { name };
            Help = help;
            Required = required;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="help">The help.</param>
        public SwitchAttribute(string name, string help)
        {
            Names = new string[] { name };
            Help = help;
        }


    }
}
