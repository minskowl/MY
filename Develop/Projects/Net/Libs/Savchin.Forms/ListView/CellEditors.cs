
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Savchin.Forms.ListView
{
    /// <summary>
    /// This editor shows and auto completes values from the given listview column.
    /// </summary>
    public class AutoCompleteCellEditor : ComboBox
    {
        public AutoCompleteCellEditor(ObjectListView lv, OLVColumn column)
        {
            this.DropDownStyle = ComboBoxStyle.DropDown;

            Dictionary<String, bool> alreadySeen = new Dictionary<string, bool>();
            for (int i = 0; i < Math.Min(lv.GetItemCount(), 1000); i++) {
                String str = column.GetStringValue(lv.GetModelObject(i));
                if (!alreadySeen.ContainsKey(str)) {
                    this.Items.Add(str);
                    alreadySeen[str] = true;
                }
            }

            this.Sorted = true;
            this.AutoCompleteSource = AutoCompleteSource.ListItems;
            this.AutoCompleteMode = AutoCompleteMode.Append;
        }
    }

    //-----------------------------------------------------------------------
    // Cell editors
    // These classes are simple cell editors that make it easier to get and set
    // the value that the control is showing.
    // In many cases, you can intercept the CellEditStarting event to 
    // change the characteristics of the editor. For example, changing
    // the acceptable range for a numeric editor or changing the strings
    // that respresent true and false values for a boolean editor.
    
    /// <summary>
    /// This editor simply shows and edits integer values.
    /// </summary>
    [Browsable(false)]
    internal class IntUpDown : NumericUpDown
    {
        public IntUpDown()
        {
            this.DecimalPlaces = 0;
            this.Minimum = -9999999;
            this.Maximum = 9999999;
        }

        new public int Value
        {
            get { return Decimal.ToInt32(base.Value); }
            set { base.Value = new Decimal(value); }
        }
    }

    /// <summary>
    /// This editor simply shows and edits unsigned integer values.
    /// </summary>
    internal class UintUpDown : NumericUpDown
    {
        public UintUpDown()
        {
            this.DecimalPlaces = 0;
            this.Minimum = 0;
            this.Maximum = 9999999;
        }

        new public uint Value
        {
            get { return Decimal.ToUInt32(base.Value); }
            set { base.Value = new Decimal(value); }
        }
    }

    /// <summary>
    /// This editor simply shows and edits boolean values.
    /// </summary>
    /// <remarks>You can intercept the CellEditStarting event if you want
    /// to change the characteristics of the editor. For example, by changing
    /// the labels to "No" and "Yes". The false value must come first.</remarks>
    internal class BooleanCellEditor : ComboBox
    {
        public BooleanCellEditor()
        {
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Items.AddRange(new String[] { "False", "True" }); // needs to be localizable
        }

        public bool Value
        {
            get { return this.SelectedIndex == 1; }
            set {
                if (value)
                    this.SelectedIndex = 1;
                else
                    this.SelectedIndex = 0;
            }
        }
    }

    /// <summary>
    /// This editor simply shows and edits floating point values.
    /// </summary>
    /// <remarks>You can intercept the CellEditStarting event if you want
    /// to change the characteristics of the editor. For example, by increasing
    /// the number of decimal places.</remarks>
    internal class FloatCellEditor : NumericUpDown
    {
        public FloatCellEditor()
        {
            this.DecimalPlaces = 2;
            this.Minimum = -9999999;
            this.Maximum = 9999999;
        }

        new public double Value
        {
            get { return Convert.ToDouble(base.Value); }
            set { base.Value = Convert.ToDecimal(value); }
        }
    }
}
