using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Savchin.Core;

namespace Savchin.Forms.Helpers
{
    /// <summary>
    /// EnumData
    /// </summary>
    public class EnumData
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public Enum Value { get; private set; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumData"/> class.
        /// </summary>
        /// <param name="value">The columb.</param>
        public EnumData(Enum value)
        {
            Value = value;
            Text = value.GetDescription();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            return Text;
        }
    }

    /// <summary>
    /// GuiEnumHelper
    /// </summary>
    public static class GuiEnumHelper
    {
        /// <summary>
        /// Setups the column.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="column">The column.</param>
        /// <param name="type">The type.</param>
        public static void Setup(this DataGridViewComboBoxColumn column, Type type)
        {
            Setup(column, EnumHelper.GetValues(type));
        }

        /// <summary>
        /// Setups the column.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="values">The values.</param>
        public static void Setup(this DataGridViewComboBoxColumn column, IEnumerable<Enum> values)
        {
            column.DisplayMember = "Text";
            column.ValueMember = "Value";

            var data = values.Select(e => new EnumData(e)).OrderBy(e => e.Text).ToArray();
            column.DataSource = data;

        }

        /// <summary>
        /// Setups the list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="type">The type.</param>
        /// <param name="exclude">The exclude.</param>
        public static void Setup(this CheckedListBox list, Type type, IEnumerable<Enum> exclude)
        {
            Setup(list, EnumHelper.GetValues(type).Except(exclude));
        }


        /// <summary>
        /// Setups the list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="values">The values.</param>
        public static void Setup(this CheckedListBox list, IEnumerable<Enum> values)
        {
            list.Items.AddRange(values.Select(e => new EnumData(e)).ToArray());
        }

        /// <summary>
        /// Setups the combo.
        /// </summary>
        /// <param name="combo">The combo.</param>
        /// <param name="type">The type.</param>
        public static void Setup(this ComboBox combo, Type type)
        {
            var values = Enum.GetValues(type);
            foreach (Enum value in values)
            {
                combo.Items.Add(new EnumData(value));
            }
        }
        /// <summary>
        /// Setups the specified combo.
        /// </summary>
        /// <param name="combo">The combo.</param>
        /// <param name="type">The type.</param>
        /// <param name="except">The except.</param>
        public static void Setup(this ComboBox combo, Type type, IEnumerable<Enum> except)
        {
            var values = EnumHelper.GetValuesArray(type).Except(except);
            foreach (Enum value in values)
            {
                combo.Items.Add(new EnumData(value));
            }
        }
        /// <summary>
        /// Setups the specified combo.
        /// </summary>
        /// <param name="combo">The combo.</param>
        /// <param name="type">The type.</param>
        /// <param name="except">The except.</param>
        public static void Setup(this ComboBox combo, Type type, Enum except)
        {
            Setup(combo, type, new[] {except});
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="combo">The combo.</param>
        /// <param name="value">The value.</param>
        public static void SetValue(this ComboBox combo, Enum value)
        {
            foreach (EnumData item in combo.Items)
            {
                if (value.Equals(item.Value))
                {
                    combo.SelectedItem = item;
                    return;
                }
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="combo">The combo.</param>
        /// <returns></returns>
        public static Enum GetValue(this ComboBox combo)
        {
            return ((EnumData) combo.SelectedItem).Value;
        }
    }
}