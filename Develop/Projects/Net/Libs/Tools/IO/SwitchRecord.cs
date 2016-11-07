#region Version & Copyright
/* 
 * Id 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Savchin.IO
{
    /// <summary>
    /// The SwitchRecord is stored within the parser's collection of registered
    /// switches.  This class is private to the outside world.
    /// </summary>
    public class SwitchRecord
    {
        #region Private Variables
        private string m_name = "";
        private string m_description = "";
        private object m_value = null;
        private readonly Type m_switchType = typeof(bool);
        private readonly List<string> m_Aliases = new List<string>();


        // The following advanced functions allow for callbacks to be
        // made to manipulate the associated data type.
        private MethodInfo m_SetMethod = null;
        private MethodInfo m_GetMethod = null;
        private object m_PropertyOwner = null;
        #endregion

        #region Public Properties
        public object Value
        {
            get
            {
                if (ReadValue != null)
                    return ReadValue;
                else
                    return m_value;
            }
        }

        /// <summary>
        /// Gets the internal value.
        /// </summary>
        /// <value>The internal value.</value>
        public object InternalValue
        {
            get { return m_value; }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public Type Type
        {
            get { return m_switchType; }
        }

        /// <summary>
        /// Gets the aliases.
        /// </summary>
        /// <value>The aliases.</value>
        public string[] Aliases
        {
            get { return m_Aliases.ToArray(); }
        }



        /// <summary>
        /// Sets the set method.
        /// </summary>
        /// <value>The set method.</value>
        public MethodInfo SetMethod
        {
            set { m_SetMethod = value; }
        }

        /// <summary>
        /// Sets the get method.
        /// </summary>
        /// <value>The get method.</value>
        public MethodInfo GetMethod
        {
            set { m_GetMethod = value; }
        }

        /// <summary>
        /// Sets the property owner.
        /// </summary>
        /// <value>The property owner.</value>
        public object PropertyOwner
        {
            set { m_PropertyOwner = value; }
        }

        private Regex regex;
        /// <summary>
        /// Gets the regex.
        /// </summary>
        /// <value>The regex.</value>
        public Regex Regex
        {
            get
            {
                if (regex == null)
                    regex = new Regex(BuildPattern(),
                                      RegexOptions.ExplicitCapture |
                                      RegexOptions.IgnoreCase |
                                      RegexOptions.Compiled);
                return regex;
            }
        }
        public object ReadValue
        {
            get
            {
                object o = null;
                if (m_PropertyOwner != null && m_GetMethod != null)
                    o = m_GetMethod.Invoke(m_PropertyOwner, null);
                return o;
            }
        }

        public string[] Enumerations
        {
            get
            {
                if (m_switchType.IsEnum)
                    return Enum.GetNames(m_switchType);
                else
                    return null;
            }
        }



        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchRecord"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public SwitchRecord(string name, string description)
        {
            Initialize(name, description);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchRecord"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="type">The type.</param>
        public SwitchRecord(string name, string description, Type type)
        {
            if (type == typeof(bool) ||
                  type == typeof(string) ||
                  type == typeof(int) ||
                  type.IsEnum)
            {
                m_switchType = type;
                Initialize(name, description);
            }
            else
                throw new ArgumentException("Currently only Ints, Bool and Strings are supported");
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds the alias.
        /// </summary>
        /// <param name="alias">The alias.</param>
        public void AddAlias(string alias)
        {
            m_Aliases.Add(alias);
        }

        /// <summary>
        /// Notifies the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Notify(object value)
        {
            if (m_PropertyOwner != null && m_SetMethod != null)
            {
                object[] parameters = new object[1];
                parameters[0] = value;
                m_SetMethod.Invoke(m_PropertyOwner, parameters);
            }
            m_value = value;
        }



        #endregion

        #region Private Utility Functions

        /// <summary>
        /// Initializes the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        private void Initialize(string name, string description)
        {
            m_name = name;
            m_description = description;

        }

        private string BuildPattern()
        {
            string matchString = Name;

            if (Aliases != null && Aliases.Length > 0)
                foreach (string s in Aliases)
                    matchString += "|" + s;

            string strPatternStart = @"(\s|^)(?<match>(-{1,2}|/)(";
            string strPatternEnd;  // To be defined below.

            // The common suffix ensures that the switches are followed by
            // a white-space OR the end of the string.  This will stop
            // switches such as /help matching /helpme
            //
            string strCommonSuffix = @"(?=(\s|$))";

            if (Type == typeof(bool))
                strPatternEnd = @")(?<value>(\+|-){0,1}))";
            else if (Type == typeof(string))
                strPatternEnd = @")(?::|\s+))((?:"")(?<value>.+)(?:"")|(?<value>\S+))";
            else if (Type == typeof(int))
                strPatternEnd = @")(?::|\s+))((?<value>(-|\+)[0-9]+)|(?<value>[0-9]+))";
            else if (Type.IsEnum)
            {
                string[] enumNames = Enumerations;
                string e_str = enumNames[0];
                for (int e = 1; e < enumNames.Length; e++)
                    e_str += "|" + enumNames[e];
                strPatternEnd = @")(?::|\s+))(?<value>" + e_str + @")";
            }
            else
                throw new ArgumentException();

            // Set the internal regular expression pattern.
            return strPatternStart + matchString + strPatternEnd + strCommonSuffix;


        }

        #endregion
    }
}
