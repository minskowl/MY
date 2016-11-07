using System.Web.UI.WebControls;
using Savchin.Text;



namespace Savchin.Web.UI
{
    public class EmailValidator : RegularExpressionValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailValidator"/> class.
        /// </summary>
        public EmailValidator()
        {
            InitControl();
        }
        /// <summary>
        /// Inits the control.
        /// </summary>
        private void InitControl()
        {
            Text = "*";
            ValidationExpression = RegularExpressions.Email;
            ErrorMessage = "Enter e-mail in correct format";
        }


    }
}
