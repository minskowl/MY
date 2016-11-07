using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Savchin.Validation;

namespace Savchin.Web.UI
{
    [DefaultProperty("ErrorMessage"),
 AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class ProxyValidator : Label, IValidator, IValidationValidator
    {
        private bool isValid = true;
        #region Properies
        /// <summary>
        /// Gets or sets the validation group.
        /// </summary>
        /// <value>The validation group.</value>
        [DefaultValue(""), Description("BaseValidator_ValidationGroup"), Themeable(false), Category("Behavior")]
        public virtual string ValidationGroup
        {
            get
            {
                object obj2 = ViewState["ValidationGroup"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set { ViewState["ValidationGroup"] = value; }
        }

        [DefaultValue(true), Category("Behavior"), Themeable(false), Browsable(false),
 Description("BaseValidator_IsValid"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }

        [Localizable(true), Category("Appearance"), DefaultValue(""), Description("BaseValidator_ErrorMessage")]
        public string ErrorMessage
        {
            get
            {
                object obj2 = ViewState["ErrorMessage"];
                if (obj2 != null)
                {
                    return (string)ViewState["ErrorMessage"];
                }
                return string.Empty;
            }
            set { ViewState["ErrorMessage"] = value; }
        }
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        [DefaultValue(""), Description("PropertyName"), Themeable(false), Category("Behavior")]
        public virtual string PropertyName
        {
            get
            {
                object obj2 = ViewState["PropertyName"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set { ViewState["PropertyName"] = value; }
        } 
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyValidator"/> class.
        /// </summary>
        public ProxyValidator()
        {
            ForeColor = Color.Red;

        }
        /// <summary>
        /// Validates the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void Validate(ValidationException ex)
        {
            string message = ex.GetMessage(PropertyName);
            isValid = string.IsNullOrEmpty(message);
            if (isValid)
            {

                ErrorMessage = string.Empty;
                Text = string.Empty;
            }
            else
            {

                ErrorMessage = message;
                Text = "*";
            }

         
        }

        /// <summary>
        /// Initializes the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        public void Initialize(Type type)
        {
           
        }

        /// <summary>
        /// When implemented by a class, evaluates the condition it checks and updates the <see cref="P:System.Web.UI.IValidator.IsValid"/> property.
        /// </summary>
        public void Validate()
        {
           
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.Validators.Add(this);
        }

    }
}
