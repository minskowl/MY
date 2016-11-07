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
    /// <summary>Implementation of a command-line parsing class.  Is capable of
    /// having switches registered with it directly or can examine a registered
    /// class for any properties with the appropriate attributes appended to
    /// them.</summary>
    public class CommandLineParser
    {
        #region Private Variables

        readonly Regex regexUnhadledSwitches = new Regex(@"(\s|^)(?<match>(-{1,2}|/)(.+?))(?=(\s|$))", RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private readonly string m_commandLine = "";
        private string m_workingString = "";
        private string m_applicationName = "";
        private string[] m_splitParameters = null;

        private readonly Dictionary<string, SwitchRecord> switches = new Dictionary<string, SwitchRecord>();
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <value>The name of the application.</value>
        public string ApplicationName
        {
            get { return m_applicationName; }
        }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public string[] Parameters
        {
            get { return m_splitParameters; }
        }

        /// <summary>
        /// Gets the switches.
        /// </summary>
        /// <value>The switches.</value>
        public SwitchRecord[] Switches
        {
            get
            {
                SwitchRecord[] result = new SwitchRecord[switches.Values.Count];
                switches.Values.CopyTo(result, 0);
                return result;
            }
        }

        /// <summary>
        /// Gets the <see cref="System.Object"/> with the specified name.
        /// </summary>
        /// <value></value>
        public object this[string name]
        {
            get
            {
                if (switches.ContainsKey(name))
                    return switches[name].Value;
                return null;
            }
        }

        /// <summary>This function returns a list of the unhandled switches
        /// that the parser has seen, but not processed.</summary>
        /// <remark>The unhandled switches are not removed from the remainder
        /// of the command-line.</remark>
        public string[] UnhandledSwitches
        {
            get
            {
                MatchCollection matchCollection = regexUnhadledSwitches.Matches(m_workingString);
                List<string> result = new List<string>(matchCollection.Count);
                foreach (Match match in matchCollection)
                {
                    result.Add(match.Groups["match"].Value);
                }

                return result.ToArray();
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds the switch.
        /// </summary>
        /// <param name="switchRecord">The switch record.</param>
        public void AddSwitch(SwitchRecord switchRecord)
        {
            switches.Add(switchRecord.Name, switchRecord);
        }

        /// <summary>
        /// Adds the switch.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        public SwitchRecord AddSwitch(string name, string description)
        {
            SwitchRecord result = new SwitchRecord(name, description);
            switches.Add(name, result);
            return result;
        }

        /// <summary>
        /// Adds the switch.
        /// </summary>
        /// <param name="names">The names.</param>
        /// <param name="description">The description.</param>
        public void AddSwitch(string[] names, string description)
        {
            SwitchRecord rec = new SwitchRecord(names[0], description);
            for (int s = 1; s < names.Length; s++)
                rec.AddAlias(names[s]);
            switches.Add(names[0], rec);
        }

        /// <summary>
        /// Parses this instance.
        /// </summary>
        /// <returns></returns>
        public bool Parse()
        {
            ExtractApplicationName();

            // Remove switches and associated info.
            foreach (SwitchRecord switchRecord in switches.Values)
            {
                ProcessSwitch(switchRecord);
            }

            // Split parameters.
            SplitParameters();

            return true;
        }

        /// <summary>
        /// Internals the value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public object InternalValue(string name)
        {
            if (switches.ContainsKey(name))
                return switches[name].InternalValue;
            return null;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineParser"/> class.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        public CommandLineParser(string commandLine)
        {
            m_commandLine = commandLine;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLineParser"/> class.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        /// <param name="obj">The class for auto attributes.</param>
        public CommandLineParser(string commandLine, object obj)
        {
            m_commandLine = commandLine;

            Type type = obj.GetType();
            foreach (MemberInfo member in type.GetMembers())
            {
                object[] attributes = member.GetCustomAttributes(typeof(CommandLineSwitchAttribute), false);
                if (attributes.Length == 0)
                    continue;

                CommandLineSwitchAttribute switchAttribute = (CommandLineSwitchAttribute)attributes[0];
                

                switches.Add(switchAttribute.Name, 
                             CreateSwitchRecord(obj, member, switchAttribute));


            }

        }

        private SwitchRecord CreateSwitchRecord(object obj, MemberInfo member, CommandLineSwitchAttribute switchAttribute)
        {
            SwitchRecord rec;
            if (member is PropertyInfo)
            {
                PropertyInfo pi = (PropertyInfo)member;

                rec = new SwitchRecord(switchAttribute.Name, 
                                       switchAttribute.Description, 
                                       pi.PropertyType);

                // Map in the Get/Set methods.
                rec.SetMethod = pi.GetSetMethod();
                rec.GetMethod = pi.GetGetMethod();
                rec.PropertyOwner = obj;

            }
            else
            {
                throw new NotImplementedException(member.GetType().FullName);
            }

            foreach (CommandLineAliasAttribute aliasAttrib in member.GetCustomAttributes(typeof(CommandLineAliasAttribute), false))
            {
                rec.AddAlias(aliasAttrib.Alias);
            }
            return rec;
        }

        #endregion

        #region Private Utility Functions
        private void ExtractApplicationName()
        {
            Regex r = new Regex(@"^(?<commandLine>("".+""|(\S)+))(?<remainder>.+)",
                RegexOptions.ExplicitCapture);
            Match m = r.Match(m_commandLine);
            if (m != null && m.Groups["commandLine"] != null)
            {
                m_applicationName = m.Groups["commandLine"].Value;
                m_workingString = m.Groups["remainder"].Value;
            }
        }

        private void SplitParameters()
        {
            // Populate the split parameters array with the remaining parameters.
            // Note that if quotes are used, the quotes are removed.
            // e.g.   one two three "four five six"
            //						0 - one
            //						1 - two
            //						2 - three
            //						3 - four five six
            // (e.g. 3 is not in quotes).
            Regex r = new Regex(@"((\s*(""(?<param>.+?)""|(?<param>\S+))))",
                RegexOptions.ExplicitCapture);
            MatchCollection m = r.Matches(m_workingString);

            if (m != null)
            {
                m_splitParameters = new string[m.Count];
                for (int i = 0; i < m.Count; i++)
                    m_splitParameters[i] = m[i].Groups["param"].Value;
            }
        }



        private void ProcessSwitch(SwitchRecord switchRecord)
        {
            MatchCollection matchCollection = switchRecord.Regex.Matches(m_workingString);

            foreach (Match match in matchCollection)
            {
                string value = null;
                if (match.Groups != null && match.Groups["value"] != null)
                    value = match.Groups["value"].Value;

                if (switchRecord.Type == typeof(bool))
                {
                    bool state = true;
                    // The value string may indicate what value we want.
                    if (match.Groups != null && match.Groups["value"] != null)
                    {
                        switch (value)
                        {
                            case "+": state = true;
                                break;
                            case "-": state = false;
                                break;
                            case "": if (switchRecord.ReadValue != null)
                                    state = !(bool)switchRecord.ReadValue;
                                break;
                            default: break;
                        }
                    }
                    switchRecord.Notify(state);
                    break;
                }
                else if (switchRecord.Type == typeof(string))
                    switchRecord.Notify(value);
                else if (switchRecord.Type == typeof(int))
                    switchRecord.Notify(int.Parse(value));
                else if (switchRecord.Type.IsEnum)
                    switchRecord.Notify(Enum.Parse(switchRecord.Type, value, true));
            }

            m_workingString = switchRecord.Regex.Replace(m_workingString, " ");
        }

        #endregion
    }
}
