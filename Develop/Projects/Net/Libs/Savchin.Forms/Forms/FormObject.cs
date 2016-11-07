using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Savchin.Forms
{
    public partial class FormObject : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormObject"/> class.
        /// </summary>
        public FormObject()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Shows the object.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="title">The title.</param>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public static FormObject ShowObject(IWin32Window window, string title, object o)
        {
            var formObject = new FormObject
                                 {
                                     propertyGrid1 = { SelectedObject = o ,
                                     },
                                     Text = title
                                 };
            if (window == null)
                formObject.Show();
            else
                formObject.Show(window);

            return formObject;
        }

        /// <summary>
        /// Shows the object.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public static FormObject ShowObject(string title, object o)
        {
            return ShowObject(null, title, o);
        }

        /// <summary>
        /// Shows the dialog object.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public static DialogResult ShowDialogObject(string title, object o)
        {
            return ShowDialogObject(null, title, o);
        }

        /// <summary>
        /// Shows the dialog object.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="title">The title.</param>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public static DialogResult ShowDialogObject(IWin32Window window, string title, object o)
        {
            var formObject = new FormObject
                                 {
                                     propertyGrid1 = { SelectedObject = o },
                                     Text = title
                                 };
            return window == null ? formObject.ShowDialog() : formObject.ShowDialog(window);
        }
        /// <summary>
        /// Shows the objects.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="o">The o.</param>
        /// <returns></returns>
        public static FormObject ShowObjects(string title, object[] o)
        {
            var formObject = new FormObject
                                 {
                                     propertyGrid1 = { SelectedObjects = o },
                                     Text = title,
                                     buttonCopy = { Visible = false }
                                 };
            formObject.Show();
            return formObject;
        }

        /// <summary>
        /// Handles the Click event of the buttonCopy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void buttonCopy_Click(object sender, EventArgs e)
        {
            if (propertyGrid1.SelectedObject != null)
            {
                Clipboard.SetText(propertyGrid1.SelectedObject.ToString());
            }

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}