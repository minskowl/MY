using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace Savchin.Wpf.Validation
{
    /// <summary>
    /// A validation rule that makes use of the business objects IDataErrorInfo interface is applied at the group level
    /// </summary>
    public class RowDataInfoValidationRule : ValidationRule
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
            var group = (BindingGroup)value;

            var errorBuilder = new StringBuilder();
            foreach (var item in group.Items.OfType<IDataErrorInfo>())
            {
                var error = item.Error;
                if (!string.IsNullOrEmpty(error))
                {
                    errorBuilder.Append((errorBuilder.Length != 0 ? ", " : "") + error);
                }
            }

            return errorBuilder.Length > 0 ? 
                new ValidationResult(false, errorBuilder.ToString()) : ValidationResult.ValidResult;
        }
    }

}
