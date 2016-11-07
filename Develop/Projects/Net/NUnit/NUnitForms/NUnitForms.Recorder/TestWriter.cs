

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NUnit.Extensions.Forms.Recorder
{
    ///<summary>
    /// This class creates a C# unit test for a form 
    /// based on events that happen on the form.
    ///</summary>
    public class TestWriter
    {
        private readonly Listener listener;
        private readonly List<Action> actions = new List<Action>();
        private readonly ArrayList definitions = new ArrayList();
        private string test = "";
        public EventHandler TestChanged;

        /// <summary>
        /// Gets the forms exclude listening.
        /// </summary>
        /// <value>The forms exclude listening.</value>
        public List<Type> FormsExcludeListening
        {
            get { return listener.FormsExcludeListening; }
        }
        /// <summary>
        /// Initialize a <c>TestWriter</c> for a form.
        /// </summary>
        /// <param name="form">
        /// The <see cref="Form"/> whose event are followed and recorded in a test.
        /// </param>
        /// <exception cref="ArgumentException">
        /// This exception is thrown if <paramref name="form"/> is not effective.
        /// </exception>
        public TestWriter(Form form)
        {
            if (form == null)
                throw new ArgumentNullException("form");

            listener = new Listener();
            listener.ListenTo(form);
            listener.Event += new EventHappened(eventHappened);

        }

        /// <summary>
        /// Gets or sets the text representation of this test.
        /// </summary>
        /// <value>
        /// This test. Not effective values are set as empty strings.
        /// </value>
        public string Test
        {
            get { return test; }
            set
            {
                if (value != null)
                {
                    test = value;
                }
                else
                {
                    test = string.Empty;
                }
                FireTestChanged();
            }
        }

        /// <summary>
        /// Add a definition to the list of definitions.
        /// </summary>
        /// <param name="definition">
        /// The definition which will be added.
        /// </param>
        /// <exception cref="ArgumentException">
        /// This exception is thrown if <paramref name="definition"/> is not effective.
        /// </exception>
        public void AddDefinition(Definition definition)
        {
            if (definition != null)
            {
                definitions.Add(definition);
                UpdateTest();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Add an action to the list of actions.
        /// </summary>
        /// <param name="action">
        /// The action which will be added.
        /// </param>
        /// /// <exception cref="ArgumentException">
        /// This exception is thrown if <paramref name="action"/> is not effective.
        /// </exception>
        public void AddAction(Action action)
        {
            if (action != null)
            {
                actions.Add(action);
                UpdateTest();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        protected void FireTestChanged()
        {
            if (TestChanged != null)
            {
                TestChanged(this, new EventArgs());
            }
        }

        private void eventHappened(Type testerType, object sender, Action action)
        {
            action.Definition = FindOrCreateVariableNameForDefinition(sender, testerType);
            actions.Add(action);

            UpdateTest();
        }

        private static Definition GetNewDefinition(object control, Type testerType)
        {
            Definition newDefinition;
            try
            {
                newDefinition = new Definition(control, GetName(control, null), testerType, null);
            }
            catch (AmbiguousNameException)
            {
                Form form = ((Control)control).FindForm(); //TODO: fix this! not always a control :(
                newDefinition = new Definition(control, GetName(control, form), testerType, form.Name);
            }
            return newDefinition;
        }

        private static void EnsureNameIsGood(Definition definition, object control, Type testerType)
        {
            Definition goodName = GetNewDefinition(control, testerType);
            definition.Name = goodName.Name;
            if (goodName.FormName != null)
            {
                definition.FormName = goodName.FormName;
            }
        }

        private Definition FindOrCreateVariableNameForDefinition(object control, Type testerType)
        {
            foreach (Definition definition in definitions)
            {
                if (definition.Control == control && definition.TesterType == testerType)
                {
                    EnsureNameIsGood(definition, control, testerType);
                    return definition;
                }
            }

            Definition newDefinition = GetNewDefinition(control, testerType);

            definitions.Add(newDefinition);

            return newDefinition;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="control"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        /// <remarks>
        /// won't yet return unique name for two different tester types
        /// on the same actual control.  
        /// </remarks>
        private static string GetName(object control, Form form)
        {
            return ControlHelper.GetUniqueShortName(form, control).Replace('.', '_');
        }

        /// <summary>
        /// Update a unit test method : all actions and definitions are updated. The updated unit
        /// test is available at <see cref="Test"/>.
        /// </summary>
        private void UpdateTest()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[Test]");
            WriteLine(sb);
            sb.Append("public void Test()" + Environment.NewLine + "{");
            WriteLine(sb);
            WriteLine(sb);

            foreach (Definition definition in definitions)
            {
                WriteDefinition(definition, sb);
            }

            WriteLine(sb);
            //should allow multiple or plugin style approach to test writing

            //should allow Plugin approach to processors of actions.
            ICollection<Action> processedActions = actions;

            processedActions = new EnterSelectTextCollapsingProcessor().Process(processedActions);
            processedActions = new EnterTextCollapsingProcessor().Process(processedActions);

            foreach (Action action in processedActions)
            {
                WriteAction(action.ToString(), sb);
            }

            WriteLine(sb);
            sb.Append("}");

            Test = sb.ToString();
        }

        /// <summary>
        /// Append a definition to a string buffer.
        /// </summary>
        /// <param name="definition">
        /// The definition to append to a string buffer.
        /// </param>
        /// <param name="sb">
        /// The string buffer.
        /// </param>
        /// <exception cref="ArgumentException">
        /// This exception is thrown if <paramref name="sb"/> or <paramref name="definition"/> is not effective.
        /// </exception>
        private static void WriteDefinition(Definition definition, StringBuilder sb)
        {
            if (definition != null && sb != null)
            {
                WriteTab(sb);
                sb.Append(definition.ToString());
                WriteLine(sb);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Write an action to a string buffer.
        /// </summary>
        /// <param name="action">
        /// The action to append to the string buffer.
        /// </param>
        /// <param name="sb">
        /// The string buffer.
        /// </param>
        /// <exception cref="ArgumentException">
        /// This exception is thrown if <paramref name="sb"/> or <paramref name="action"/> is not effective.
        /// </exception>
        private static void WriteAction(string action, StringBuilder sb)
        {
            if (sb != null && action != null)
            {
                WriteTab(sb);
                sb.Append(action);
                WriteLine(sb);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Append a new line at the end of a string buffer.
        /// </summary>
        /// <param name="sb">
        /// The buffer to append a new line to.
        /// </param>
        /// <exception cref="ArgumentException">
        /// This exception is thrown if <paramref name="sb"/> is not effective.
        /// </exception>
        private static void WriteLine(StringBuilder sb)
        {
            if (sb != null)
            {
                sb.Append(Environment.NewLine);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Append a tab at the end of a string buffer.
        /// </summary>
        /// <param name="sb">
        /// The buffer to append a tab to.
        /// </param>
        /// <exception cref="ArgumentException">
        /// This exception is thrown if <paramref name="sb"/> is not effective.
        /// </exception>
        private static void WriteTab(StringBuilder sb)
        {
            if (sb != null)
            {
                sb.Append("\t");
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}