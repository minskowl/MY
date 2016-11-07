using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Savchin.Wpf.Validation
{
    /// <summary>
    ///  A validation rule that makes use of the business objects IDataErrorInfo interface is applied at the property level
    /// </summary>
    public class CellDataInfoValidationRule : ValidationRule
    {
        /// <summary>
        /// When overridden in a derived class, performs validation checks on a value.
        /// </summary>
        /// <param name="value">The value from the binding target to check.</param>
        /// <param name="cultureInfo">The culture to use in this rule.</param>
        /// <returns>
        /// A <see cref="T:System.Windows.Controls.ValidationResult"/> object.
        /// </returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // obtain the bound business object
            var expression = value as BindingExpression;
            if (expression == null) return ValidationResult.ValidResult;

            var info = expression.DataItem as IDataErrorInfo;
            if (info == null) return ValidationResult.ValidResult;


            // obtain any errors relating to this bound property
            var error = info[expression.ParentBinding.Path.Path];

            return !string.IsNullOrEmpty(error) ?
                new ValidationResult(false, error) : ValidationResult.ValidResult;
        }
    }
}
