#region Version & Copyright
/* 
 * $Id: ObjectParameter.cs 26244 2008-01-10 12:08:33Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

namespace Savchin.Web.UI
{

    /// <summary>
    /// ObjectParameter. Provide parameter from page\control property
    /// </summary>
    public class ObjectParameter : Parameter
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectParameter"/> class.
        /// </summary>
        public ObjectParameter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectParameter"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public ObjectParameter(string name, object value)
            : base(name)
        {
            PropertyName = value.ToString();

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectParameter"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        public ObjectParameter(string name, TypeCode type, object value)
            : base(name, type)
        {
            PropertyName = value.ToString();
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectParameter"/> class.
        /// </summary>
        /// <param name="original">The original.</param>
        protected ObjectParameter(ObjectParameter original)
            : base(original)
        {
            PropertyName = original.PropertyName;
        }
        #endregion

        /// <summary>
        /// Updates and returns the value of the <see cref="T:System.Web.UI.WebControls.Parameter"></see> object.
        /// </summary>
        /// <param name="context">The current <see cref="T:System.Web.HttpContext"></see> of the request.</param>
        /// <param name="control">The <see cref="T:System.Web.UI.Control"></see> the parameter is bound to. If the parameter is not bound to a control, the control parameter is ignored.</param>
        /// <returns>
        /// An object that represents the updated and current value of the parameter.
        /// </returns>
        protected override object Evaluate(HttpContext context, Control control)
        {
            PropertyInfo info;
            info = control.Parent.GetType().GetProperty(PropertyName);
            if (info != null)
            {
                return info.GetValue(control.Parent, null);
            }
            info = control.Page.GetType().GetProperty(PropertyName);
            if (info != null)
            {
                return info.GetValue(control.Page, null);
            }
            return null;
        }

        /// <summary>
        /// Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.Parameter"></see> instance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.UI.WebControls.Parameter"></see> that is an exact duplicate of the current one.
        /// </returns>
        protected override Parameter Clone()
        {
            return new ObjectParameter(this);
        }


        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public string PropertyName
        {
            get
            {
                object obj = ViewState["PropertyName"];

                if (obj == null)
                    return string.Empty;
                return (string)obj;
            }

            set
            {
                if (PropertyName != value)
                {
                    ViewState["PropertyName"] = value;
                    OnParameterChanged();
                }
            }

        }



    }
}
