using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Savchin.Web.UI.Extenders
{
    /// <summary>
    /// CheckBoxListEx
    /// </summary>
    public class CheckBoxListEx : CheckBoxList
    {
        /// <summary>
        /// Gets the selected values.
        /// </summary>
        /// <value>The selected values.</value>
        public List<string> SelectedValues
        {
            get
            {
                List<string> result = new List<string>();
                foreach (ListItem item in Items)
                {
                    if (item.Selected)
                    {
                        result.Add(item.Value);
                    }
                }
                return result;
            }
            set
            {
                foreach (ListItem item in Items)
                {
                    item.Selected = value.Contains(item.Value);
                }
            }
        }
        /// <summary>
        /// Gets or sets the selected long values.
        /// </summary>
        /// <value>The selected long values.</value>
        public List<long> SelectedLongValues
        {
            get
            {
                var result = new List<long>();
                foreach (ListItem item in Items)
                {
                    if (item.Selected)
                    {
                        result.Add(long.Parse(item.Value));
                    }
                }
                return result;
            }
            set
            {
                foreach (ListItem item in Items)
                {
                    item.Selected = value.Contains(long.Parse(item.Value));
                }
            }
        }
        /// <summary>
        /// Gets or sets the selected int values.
        /// </summary>
        /// <value>The selected int values.</value>
        public List<int> SelectedIntValues
        {
            get
            {
                List<int> result = new List<int>();
                foreach (ListItem item in Items)
                {
                    if (item.Selected)
                    {
                        result.Add(int.Parse(item.Value));
                    }
                }
                return result;
            }
            set
            {
                foreach (ListItem item in Items)
                {

                    item.Selected = value.Contains(int.Parse(item.Value));
                }
            }
        }

        /// <summary>
        /// Selects the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Select(string value)
        {
            foreach (ListItem item in Items)
            {
                if (item.Value == value)
                    item.Selected = true;
            }
        }
        /// <summary>
        /// Selects the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Select(int value)
        {
            Select(value.ToString());
        }
        /// <summary>
        /// Selects the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Select(long value)
        {
            Select(value.ToString());
        }

        /// <summary>
        /// Uns the select all.
        /// </summary>
        public void UnSelectAll()
        {
            foreach (ListItem item in Items)
            {
                item.Selected = false;
            }
        }
        /// <summary>
        /// Selects all.
        /// </summary>
        public void SelectAll()
        {
            foreach (ListItem item in Items)
            {
                item.Selected = true;
            }
        }
    }
}