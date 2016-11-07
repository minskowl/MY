using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BotvaSpider.Configuration;
using BotvaSpider.Data;
using BotvaSpider.Gears;

namespace BotvaSpider.Controls
{

    public partial class CoulombSelector : UserControl
    {
        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether [small size].
        /// </summary>
        /// <value><c>true</c> if [small size]; otherwise, <c>false</c>.</value>
        public bool SmallSize
        {
            get;
            set;
        }

        [Browsable(false)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public Coulomb SelectedCoulomb
        {
            get
            {
                return listCoulomb.SelectedCoulomb;
            }
            set
            {
                listCoulomb.SelectedCoulomb = value;
            }
        }
        /// <summary>
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The text associated with this control.
        /// </returns>
        [Browsable(true)]
        public override string Text
        {
            get
            {
                return groupBox1.Text;
            }
            set
            {
                groupBox1.Text = value;
            }
        }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CoulombSelector"/> class.
        /// </summary>
        public CoulombSelector()
        {
            InitializeComponent();

            if (!DesignMode) listCoulomb.Fill();
        }


        /// <summary>
        /// Handles the SelectedValueChanged event of the listCoulomb control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void listCoulomb_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void SetPicture()
        {
            var folder = Path.Combine(AppSettings.ApplicatioPath, @"Resources\Coulombs");
            var fileName = string.Format("Coulomb_{0}{1}.jpg", (int)SelectedCoulomb, SmallSize ? "s" : string.Empty);

            folder = Path.Combine(folder, fileName);
            try
            {
                pictureBox.Image = Image.FromFile(folder);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listCoulomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPicture();
        }
    }
}
