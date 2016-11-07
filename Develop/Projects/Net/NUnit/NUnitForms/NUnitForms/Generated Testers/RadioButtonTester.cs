
using System.Windows.Forms;

namespace NUnit.Extensions.Forms
{
    public partial class RadioButtonTester : ControlTester<RadioButton, RadioButtonTester>
    {
        ///<summary>
        /// Gets the Checked property of the underlying radio button.
        ///</summary>
        public bool Checked
        {
            get { return Properties.Checked; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RadioButtonTester"/> class.
        /// </summary>
        public RadioButtonTester()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RadioButtonTester"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="form">The form.</param>
        public RadioButtonTester(string name, Form form) : base(name, form)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RadioButtonTester"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="formName">Name of the form.</param>
        public RadioButtonTester(string name, string formName) : base(name, formName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RadioButtonTester"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public RadioButtonTester(string name) : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RadioButtonTester"/> class.
        /// </summary>
        /// <param name="tester">The tester.</param>
        /// <param name="index">The index.</param>
        public RadioButtonTester(RadioButtonTester tester, int index) : base(tester, index)
        {
        }
    }
}