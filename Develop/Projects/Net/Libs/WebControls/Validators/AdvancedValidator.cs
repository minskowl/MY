using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Savchin.Validation;
using Savchin.Text;


namespace Savchin.Web.UI
{


    [DefaultProperty("ErrorMessage"),
    AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class AdvancedValidator : Label, IValidator, IValidationValidator
    {
        private const string defaultText = "*";

        // Fields
        private bool isValid = true;
        private bool preRenderCalled;
        private bool propertiesChecked = false;
        private bool propertiesValid = true;
        private bool renderUplevel = false;
        //private const string ValidatorFileName = "WebUIValidation.js";
        //private const string ValidatorIncludeScriptKey = "ValidatorIncludeScript";

        //private const string ValidatorStartupScript =
        //    "\r\n<script type=\"text/javascript\">\r\n<!--\r\nvar Page_ValidationActive = false;\r\nif (typeof(ValidatorOnLoad) == \"function\") {\r\n    ValidatorOnLoad();\r\n}\r\n\r\nfunction ValidatorOnSubmit() {\r\n    if (Page_ValidationActive) {\r\n        return ValidatorCommonOnSubmit();\r\n    }\r\n    else {\r\n        return true;\r\n    }\r\n}\r\n// -->\r\n</script>\r\n        ";

        #region Properties
        // Properties
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public override string AssociatedControlID
        {
            get { return base.AssociatedControlID; }
            set
            {
                throw new NotSupportedException("Property_Not_Supported");
            }
        }

        /// <summary>
        /// Gets or sets the control to validate.
        /// </summary>
        /// <value>The control to validate.</value>
        [Category("Behavior"), Themeable(false), DefaultValue(""), IDReferenceProperty,
         Description("BaseValidator_ControlToValidate"), TypeConverter(typeof(ValidatedControlConverter))]
        public string ControlToValidate
        {
            get { return (string)ViewState["ControlToValidate"] ?? string.Empty; }
            set { ViewState["ControlToValidate"] = value; }
        }



        /// <summary>
        /// Gets or sets the display.
        /// </summary>
        /// <value>The display.</value>
        [Description("BaseValidator_Display"), DefaultValue(1), Category("Appearance"), Themeable(false)]
        public ValidatorDisplay Display
        {
            get
            {
                object obj2 = ViewState["Display"];
                if (obj2 != null)
                {
                    return (ValidatorDisplay)obj2;
                }
                return ValidatorDisplay.Static;
            }
            set
            {
                if ((value < ValidatorDisplay.None) || (value > ValidatorDisplay.Dynamic))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                ViewState["Display"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable client script].
        /// </summary>
        /// <value><c>true</c> if [enable client script]; otherwise, <c>false</c>.</value>
        [Category("Behavior"), DefaultValue(true), Description("BaseValidator_EnableClientScript"), Themeable(false)]
        public bool EnableClientScript
        {
            get
            {
                object obj2 = ViewState["EnableClientScript"];
                if (obj2 != null)
                {
                    return (bool)obj2;
                }
                return true;
            }
            set { ViewState["EnableClientScript"] = value; }
        }

        public override bool Enabled
        {
            get { return base.Enabled; }
            set
            {
                base.Enabled = value;
                if (!value)
                {
                    isValid = true;
                }
            }
        }

        [Localizable(true), Category("Appearance"), DefaultValue(""), Description("BaseValidator_ErrorMessage")]
        public string ErrorMessage
        {
            get
            {
                object obj2 = ViewState["ErrorMessage"];
                if (obj2 != null)
                {
                    return (string)obj2;
                }
                return string.Empty;
            }
            set { ViewState["ErrorMessage"] = value; }
        }

        [DefaultValue(typeof(Color), "Red")]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        //public override bool IsReloadable
        //{
        //    get { return true; }
        //}

        [DefaultValue(true), Category("Behavior"), Themeable(false), Browsable(false),
         Description("BaseValidator_IsValid"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }

        protected bool PropertiesValid
        {
            get
            {
                if (!propertiesChecked)
                {
                    propertiesValid = ControlPropertiesValid();
                    propertiesChecked = true;
                }
                return propertiesValid;
            }
        }

        protected bool RenderUplevel
        {
            get { return renderUplevel; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [set focus on error].
        /// </summary>
        /// <value><c>true</c> if [set focus on error]; otherwise, <c>false</c>.</value>
        [Description("BaseValidator_SetFocusOnError"), Category("Behavior"), DefaultValue(false), Themeable(false)]
        public bool SetFocusOnError
        {
            get
            {
                object obj2 = ViewState["SetFocusOnError"];
                return ((obj2 != null) && ((bool)obj2));
            }
            set { ViewState["SetFocusOnError"] = value; }
        }

        [DefaultValue(""), Category("Appearance"), Description("BaseValidator_Text"),
         PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

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

        // Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="TestValidator"/> class.
        /// </summary>
        public AdvancedValidator()
        {

        }
        private string GetPropertyName()
        {
            string result = PropertyName;
            if (!string.IsNullOrEmpty(result))
                return result;


            string controlId = ControlToValidate;
            Control component = this.NamingContainer.FindControl(controlId);
            if (component == null)
                throw new WebException(string.Format("Invalid ControlToValidate '{0}'", controlId));
            if (component is IBindable)
            {
                result = ((IBindable)component).PropertyName;
            }
            if (string.IsNullOrEmpty(result))
                throw new WebException("Need set PropertyName");
            return result;
        }

        /// <summary>
        /// Sets the message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public void Validate(ValidationException ex)
        {
            string message = ex.GetMessage(GetPropertyName());
            isValid = string.IsNullOrEmpty(message);
            if (isValid)
            {

                ErrorMessage = string.Empty;
                Text = string.Empty;
            }
            else
            {

                ErrorMessage = message;
                Text = defaultText;
            }
        }

        /// <summary>
        /// Initializes the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        public void Initialize(Type type)
        {
            string propertyName = GetPropertyName();

            PropertyInfo property = type.GetProperty(propertyName);
            if (property == null)
                throw new WebException(string.Format("Invalid PropertyName '{0}'", propertyName));

            ValidationAttribute[] attr = (ValidationAttribute[])property.GetCustomAttributes(typeof(ValidationAttribute), true);
            CreateValidators(property, attr);
        }








        #region Helpers

        #region Create Inner Validators
        /// <summary>
        /// Creates the validators.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="attribute">The attribute.</param>
        private void CreateValidators(PropertyInfo property, IEnumerable<ValidationAttribute> attribute)
        {
            bool hasRegexValidator = false;
            foreach (ValidationAttribute attr in attribute)
            {
                if (attr is RequiredFieldValidationAttribute)
                {
                    CreateRequiredValidator(property.Name, attr as RequiredFieldValidationAttribute);
                }
                else if (attr is RangeValidationAttribute)
                {
                    CreateRangeValidator(property, (RangeValidationAttribute)attr);
                }
                else if (attr is RegularExpressionValidationAttribute)
                {
                    CreateRegularExpression((RegularExpressionValidationAttribute)attr, property.Name);
                    hasRegexValidator = true;
                }
            }
            if (!hasRegexValidator)
                CreateTypeValidator(property);
        }

        private void CreateTypeValidator(PropertyInfo property)
        {
            if (property.PropertyType.Equals(typeof(string)))
                return;
            string regExpression = null;
            if (property.PropertyType.Equals(typeof(decimal)) ||
                property.PropertyType.Equals(typeof(float)))
            {
                regExpression = RegularExpressions.FloatExpression;
            }
            else if (property.PropertyType.Equals(typeof(int)) ||
                    property.PropertyType.Equals(typeof(short)))
            {
                regExpression = RegularExpressions.IntExpression;
            }
            else
            {
                //throw new NotImplementedException(" CreateTypeValidator " + property.PropertyType.FullName);
            }
            if (!string.IsNullOrEmpty(regExpression))
            {
                RegularExpressionValidator validator = new RegularExpressionValidator();
                validator.ID = "rev" + ControlToValidate;
                validator.ControlToValidate = ControlToValidate;
                validator.ValidationExpression = regExpression;
                validator.Text = defaultText;
                validator.ErrorMessage = "Invalid type of data";
                validator.Display = ValidatorDisplay.Dynamic;
                Controls.Add(validator);
            }

        }

        private void CreateRegularExpression(RegularExpressionValidationAttribute attr, string name)
        {
            RegularExpressionValidator validator = new RegularExpressionValidator();
            validator.ID = "rev" + ControlToValidate;
            validator.ControlToValidate = ControlToValidate;
            validator.ValidationExpression = attr.RegularExpression;
            validator.Text = defaultText;
            validator.ErrorMessage = string.Format(attr.Message, name); ;
            validator.Display = ValidatorDisplay.Dynamic;
            Controls.Add(validator);
        }

        private void CreateRangeValidator(PropertyInfo property, RangeValidationAttribute attr)
        {
            BaseValidator baseValidator;
            bool needCheckMax = attr.MaxValue != null;
            bool needCheckMin = attr.MinValue != null;
            string errorMessage = string.Empty;
            if (property.PropertyType.Equals(typeof(string)))
            {
                


                //if (needCheckMax)
                //{
                //    Control inputField = FindControl(ControlToValidate);
                //    if (inputField != null && inputField is TextBox)
                //    {
                //        ((TextBox)inputField).MaxLength = (int)attr.MaxValue;
                //        if (!needCheckMin)
                //            return;
                //    }
                //}

                RegularExpressionValidator validator = new RegularExpressionValidator();

               
                validator.ValidationExpression = needCheckMax ? 
                    string.Format(".{{{0},{1}}}", attr.MinValue, attr.MaxValue) : 
                    string.Format(".{{{0}}}", attr.MinValue);

                baseValidator = validator;
                errorMessage = attr.Message;
            }
            else
            {
                BaseCompareValidator validator;
                if (needCheckMax && needCheckMin)
                {
                    RangeValidator rangeValidator = new RangeValidator();
                    rangeValidator.MaximumValue = attr.MaxValue.ToString();
                    rangeValidator.MinimumValue = attr.MinValue.ToString();
                    validator = rangeValidator;
                    errorMessage =
                        string.Format(" Field should be more than {0} characters and less than {1} characters.",
                                      attr.MinValue, attr.MaxValue);
                }
                else if (needCheckMin)
                {
                    CompareValidator compareValidator = new CompareValidator();
                    compareValidator.ValueToCompare = attr.MinValue.ToString();
                    compareValidator.Operator = ValidationCompareOperator.GreaterThanEqual;
                    validator = compareValidator;
                    errorMessage = string.Format(" Field should be more than {0} characters. ", attr.MinValue);
                }
                else if (needCheckMax)
                {
                    CompareValidator compareValidator = new CompareValidator();
                    compareValidator.ValueToCompare = attr.MinValue.ToString();
                    compareValidator.Operator = ValidationCompareOperator.LessThanEqual;
                    validator = compareValidator;
                    errorMessage = string.Format(" Field should be less than {0} characters. ", attr.MaxValue);
                }
                else
                {
                    return;
                }

                if (property.PropertyType.Equals(typeof(string)))
                    validator.Type = ValidationDataType.String;
                else if (property.PropertyType.Equals(typeof(DateTime)))
                    validator.Type = ValidationDataType.Date;
                else if (property.PropertyType.Equals(typeof(decimal)))
                    validator.Type = ValidationDataType.Currency;
                else if (property.PropertyType.Equals(typeof(float)) ||
                     property.PropertyType.Equals(typeof(double)))
                    validator.Type = ValidationDataType.Double;
                else
                    validator.Type = ValidationDataType.Integer;



                baseValidator = validator;
            }
            baseValidator.ControlToValidate = ControlToValidate;
            baseValidator.Display = ValidatorDisplay.Dynamic;
            baseValidator.ID = "rv" + baseValidator.ControlToValidate;
            baseValidator.Text = defaultText;
            baseValidator.ErrorMessage = errorMessage;
            Controls.Add(baseValidator);
        }

        private void CreateRequiredValidator(string name, RequiredFieldValidationAttribute attr)
        {
            RequiredFieldValidator validator = new RequiredFieldValidator();
            validator.ID = "rfv" + ControlToValidate;
            validator.ControlToValidate = ControlToValidate;
            validator.Text = defaultText;
            validator.ErrorMessage = string.Format(attr.Message, name);
            validator.Display = ValidatorDisplay.Dynamic;
            Controls.Add(validator);
        }
        #endregion


        #endregion



        /// <summary>
        /// Adds the HTML attributes and styles of a <see cref="T:System.Web.UI.WebControls.Label"/> control to render to the specified output stream.
        /// </summary>
        /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter"/> that represents the output stream to render HTML content on the client.</param>
        /// <exception cref="T:System.Web.HttpException">The control specified in the <see cref="P:System.Web.UI.WebControls.Label.AssociatedControlID"/> property cannot be found.</exception>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            bool flag = !Enabled;
            if (flag)
            {
                Enabled = true;
            }
            try
            {
                if (RenderUplevel)
                {
                    base.EnsureID();
                    string clientID = ClientID;
                    //HtmlTextWriter writer2 = base.EnableLegacyRendering ? writer : null;
                    HtmlTextWriter writer2 = writer;
                    if (ControlToValidate.Length > 0)
                    {
                        AddExpandoAttribute(writer2, clientID, "controltovalidate",
                                            GetControlRenderID(ControlToValidate));
                    }
                    if (SetFocusOnError)
                    {
                        AddExpandoAttribute(writer2, clientID, "focusOnError", "t", false);
                    }
                    if (ErrorMessage.Length > 0)
                    {
                        AddExpandoAttribute(writer2, clientID, "errormessage", ErrorMessage);
                    }
                    ValidatorDisplay enumValue = Display;
                    if (enumValue != ValidatorDisplay.Static)
                    {
                        AddExpandoAttribute(writer2, clientID, "display",
                                            PropertyConverter.EnumToString(typeof(ValidatorDisplay), enumValue), false);
                    }
                    if (!IsValid)
                    {
                        AddExpandoAttribute(writer2, clientID, "isvalid", "False", false);
                    }
                    if (flag)
                    {
                        AddExpandoAttribute(writer2, clientID, "enabled", "False", false);
                    }
                    if (ValidationGroup.Length > 0)
                    {
                        AddExpandoAttribute(writer2, clientID, "validationGroup", ValidationGroup);
                    }
                }
                base.AddAttributesToRender(writer);
            }
            finally
            {
                if (flag)
                {
                    Enabled = false;
                }
            }
        }

        internal void AddExpandoAttribute(HtmlTextWriter writer, string controlId, string attributeName,
                                          string attributeValue)
        {
            AddExpandoAttribute(writer, controlId, attributeName, attributeValue, true);
        }

        internal void AddExpandoAttribute(HtmlTextWriter writer, string controlId, string attributeName,
                                          string attributeValue, bool encode)
        {
            AddExpandoAttribute(this, writer, controlId, attributeName, attributeValue, encode);
        }

        internal static void AddExpandoAttribute(Control control, HtmlTextWriter writer, string controlId,
                                                 string attributeName, string attributeValue, bool encode)
        {
            if (writer != null)
            {
                writer.AddAttribute(attributeName, attributeValue, encode);
            }
            else
            {
                Page page = control.Page;
                //if (!page.IsPartialRenderingSupported)
                //{
                page.ClientScript.RegisterExpandoAttribute(controlId, attributeName, attributeValue, encode);
                //}
                //else
                //{
                //    ValidatorCompatibilityHelper.RegisterExpandoAttribute(control, controlId, attributeName,
                //                                                          attributeValue, encode);
                //}
            }
        }

        protected void CheckControlValidationProperty(string name, string propertyName)
        {
            Control component = NamingContainer.FindControl(name);
            if (component == null)
            {
                throw new ArgumentException(string.Format("Validation control '{0}' not found", name));
            }
            if (GetValidationProperty(component) == null)
            {
                throw new HttpException("Validator_bad_control_type");
            }
        }

        protected virtual bool ControlPropertiesValid()
        {
            string controlToValidate = ControlToValidate;
            if (controlToValidate.Length == 0)
            {
                throw new HttpException("Validator_control_blank");
            }
            CheckControlValidationProperty(controlToValidate, "ControlToValidate");
            return true;
        }

        protected virtual bool DetermineRenderUplevel()
        {
            Page page = Page;
            if ((page == null)
                //|| (page.RequestInternal == null)
                )
            {
                return false;
            }
            return
                ((EnableClientScript && (page.Request.Browser.W3CDomVersion.Major >= 1)) &&
                 (page.Request.Browser.EcmaScriptVersion.CompareTo(new Version(1, 2)) >= 0));
        }

        /// <summary>
        /// Evaluates the is valid.
        /// </summary>
        /// <returns></returns>
        protected virtual bool EvaluateIsValid()
        {
            return string.IsNullOrEmpty(ErrorMessage);
        }

        protected string GetControlRenderID(string name)
        {
            Control control = FindControl(name);
            if (control == null)
            {
                return string.Empty;
            }
            return control.ClientID;
        }

        protected string GetControlValidationValue(string name)
        {
            Control component = NamingContainer.FindControl(name);
            if (component == null)
            {
                return null;
            }
            PropertyDescriptor validationProperty = GetValidationProperty(component);
            if (validationProperty == null)
            {
                return null;
            }
            object obj2 = validationProperty.GetValue(component);
            if (obj2 is ListItem)
            {
                return ((ListItem)obj2).Value;
            }
            if (obj2 != null)
            {
                return obj2.ToString();
            }
            return string.Empty;
        }

        public static PropertyDescriptor GetValidationProperty(object component)
        {
            ValidationPropertyAttribute attribute =
                (ValidationPropertyAttribute)
                TypeDescriptor.GetAttributes(component)[typeof(ValidationPropertyAttribute)];
            if ((attribute != null) && (attribute.Name != null))
            {
                return TypeDescriptor.GetProperties(component, (Attribute[])null)[attribute.Name];
            }
            return null;
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

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            preRenderCalled = true;
            propertiesChecked = false;
            bool propertiesValid = PropertiesValid;
            renderUplevel = DetermineRenderUplevel();
            if (renderUplevel)
            {
                RegisterValidatorCommonScript();
            }
        }

        ///// <summary>
        ///// Raises the <see cref="E:System.Web.UI.Control.Load"/> event.
        ///// </summary>
        ///// <param name="e">The <see cref="T:System.EventArgs"/> object that contains the event data.</param>
        //protected override void OnLoad(EventArgs e)
        //{
        //    RequiredFieldValidator validator = new RequiredFieldValidator();
        //    validator.ID = "rfv" + ControlToValidate;
        //    validator.ControlToValidate = ControlToValidate;
        //    validator.Text = "Requeired";
        //    validator.ErrorMessage = "Requeired";
        //    Controls.Add(validator);

        //    base.OnLoad(e);
        //}

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Unload"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains event data.</param>
        protected override void OnUnload(EventArgs e)
        {
            if (Page != null)
            {
                Page.Validators.Remove(this);
            }
            base.OnUnload(e);
        }

        protected void RegisterValidatorCommonScript()
        {
            //if (!Page.IsPartialRenderingSupported)
            //{
            if (!Page.ClientScript.IsClientScriptBlockRegistered(typeof(BaseValidator), "ValidatorIncludeScript"))
            {
                Page.ClientScript.RegisterClientScriptResource(typeof(BaseValidator), "WebUIValidation.js");
                Page.ClientScript.RegisterStartupScript(typeof(BaseValidator), "ValidatorIncludeScript",
                                                        "\r\n<script type=\"text/javascript\">\r\n<!--\r\nvar Page_ValidationActive = false;\r\nif (typeof(ValidatorOnLoad) == \"function\") {\r\n    ValidatorOnLoad();\r\n}\r\n\r\nfunction ValidatorOnSubmit() {\r\n    if (Page_ValidationActive) {\r\n        return ValidatorCommonOnSubmit();\r\n    }\r\n    else {\r\n        return true;\r\n    }\r\n}\r\n// -->\r\n</script>\r\n        ");
                Page.ClientScript.RegisterOnSubmitStatement(typeof(BaseValidator), "ValidatorOnSubmit",
                                                            "if (typeof(ValidatorOnSubmit) == \"function\" && ValidatorOnSubmit() == false) return false;");
            }
            //}
            //else
            //{
            //    ValidatorCompatibilityHelper.RegisterClientScriptResource(this, typeof (BaseValidator),
            //                                                              "WebUIValidation.js");
            //    ValidatorCompatibilityHelper.RegisterStartupScript(this, typeof (BaseValidator),
            //                                                       "ValidatorIncludeScript",
            //                                                       "\r\n<script type=\"text/javascript\">\r\n<!--\r\nvar Page_ValidationActive = false;\r\nif (typeof(ValidatorOnLoad) == \"function\") {\r\n    ValidatorOnLoad();\r\n}\r\n\r\nfunction ValidatorOnSubmit() {\r\n    if (Page_ValidationActive) {\r\n        return ValidatorCommonOnSubmit();\r\n    }\r\n    else {\r\n        return true;\r\n    }\r\n}\r\n// -->\r\n</script>\r\n        ",
            //                                                       false);
            //    ValidatorCompatibilityHelper.RegisterOnSubmitStatement(this, typeof (BaseValidator), "ValidatorOnSubmit",
            //                                                           "if (typeof(ValidatorOnSubmit) == \"function\" && ValidatorOnSubmit() == false) return false;");
            //}
        }

        protected virtual void RegisterValidatorDeclaration()
        {
            string arrayValue = "document.getElementById(\"" + ClientID + "\")";
            //if (!Page.IsPartialRenderingSupported)
            //{
            Page.ClientScript.RegisterArrayDeclaration("Page_Validators", arrayValue);
            //}
            //else
            //{
            //    ValidatorCompatibilityHelper.RegisterArrayDeclaration(this, "Page_Validators", arrayValue);
            //    ValidatorCompatibilityHelper.RegisterStartupScript(this, typeof (BaseValidator),
            //                                                       ClientID + "_DisposeScript",
            //                                                       string.Format(CultureInfo.InvariantCulture,
            //                                                                     "\r\ndocument.getElementById('{0}').dispose = function() {{\r\n    Array.remove({1}, document.getElementById('{0}'));\r\n}}\r\n",
            //                                                                     new object[]
            //                                                                         {ClientID, "Page_Validators"}),
            //                                                       true);
            //}
        }

        protected override void Render(HtmlTextWriter writer)
        {
            bool flag;
            if (base.DesignMode || (!preRenderCalled && (Page == null)))
            {
                propertiesChecked = true;
                propertiesValid = true;
                renderUplevel = false;
                flag = true;
            }
            else
            {
                flag = Enabled && !IsValid;
            }

            if (!PropertiesValid)
                return;


            bool flag2;
            bool flag3;
            if (Page != null)
            {
                Page.VerifyRenderingInServerForm(this);
            }
            ValidatorDisplay display = Display;
            if (RenderUplevel)
            {
                flag3 = true;
                flag2 = display != ValidatorDisplay.None;
            }
            else
            {
                flag2 = (display != ValidatorDisplay.None) && flag;
                flag3 = flag2;
            }
            if (flag3 && RenderUplevel)
            {
                RegisterValidatorDeclaration();
                if ((display == ValidatorDisplay.None) || (!flag && (display == ValidatorDisplay.Dynamic)))
                {
                    base.Style["display"] = "none";
                }
                else if (!flag)
                {
                    base.Style["visibility"] = "hidden";
                }
            }
            if (flag3)
            {
                RenderBeginTag(writer);
            }
            if (flag2)
            {
                if (Text.Trim().Length > 0)
                {
                    RenderContents(writer);
                }
                //else if (base.HasRenderingData())
                //{
                //    base.RenderContents(writer);
                //}
                else
                {
                    writer.Write(ErrorMessage);
                }

            }
            else if (!RenderUplevel && (display == ValidatorDisplay.Static))
            {
                writer.Write("&nbsp;");
            }
            if (flag3)
            {
                RenderEndTag(writer);
            }

            base.RenderContents(writer);

        }

        public void Validate()
        {
            IsValid = true;
            if (Visible && Enabled)
            {
                propertiesChecked = false;
                if (PropertiesValid)
                {
                    IsValid = EvaluateIsValid();
                    //if ((!IsValid && (Page != null)) && SetFocusOnError)
                    //{
                    //    Page.SetValidatorInvalidControlFocus(ControlToValidate);
                    //}
                }
            }
        }


    }
}