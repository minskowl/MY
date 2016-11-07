using System;
using System.Drawing;
using System.Windows.Forms;
using Savchin.Drawing;


namespace Savchin.Forms
{
    public partial class ColorPicker : UserControl
    {


        public event EventHandler ValueChanged;

        private bool IgnoreEvent = false;

        #region Properties
        Color rgbValue;
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public Color Value
        {
            get
            {
                return rgbValue;

            }
            set
            {
                rgbValue = value;
                resultPanel.BackColor = rgbValue;
                if (mode == ColorSheme.RGB)
                {
                    trackBar1.Value = rgbValue.R;
                    textBox1.Text = rgbValue.R.ToString();

                    trackBar2.Value = rgbValue.G;
                    textBox2.Text = rgbValue.G.ToString();

                    trackBar3.Value = rgbValue.B;
                    textBox3.Text = rgbValue.B.ToString();
                }
                else
                {
                    HSBColor color = HSBColor.FromColor(rgbValue);
                    trackBar1.Value = color.H;
                    textBox1.Text = color.H.ToString();

                    trackBar2.Value = color.S;
                    textBox2.Text = color.S.ToString();

                    trackBar3.Value = color.B;
                    textBox3.Text = color.B.ToString();
                }
            }
        }


        ColorSheme mode;
        public ColorSheme Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;

                if (mode == ColorSheme.RGB)
                {
                    label1.Text = "R";
                    label2.Text = "G";
                    label3.Text = "B";
                    Value = rgbValue;
                }
                else
                {
                    label1.Text = "H";
                    label2.Text = "S";
                    label3.Text = "B";
                    Value = rgbValue;
                }
            }
        }




        #endregion

        public ColorPicker()
        {
            InitializeComponent();
        }

        private int GetSaveInteger(string source)
        {
            try
            {
                if (string.IsNullOrEmpty(source))
                    return 0;

                return int.Parse(source);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Called when [rgbValue changed].
        /// </summary>
        protected virtual void OnValueChanged()
        {
            if (mode == ColorSheme.HSB)
            {
                rgbValue = HSBColor.FromHSB(trackBar1.Value, trackBar2.Value, trackBar3.Value);
            }
            else
            {
                rgbValue = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
            }
            resultPanel.BackColor = rgbValue;

            if (ValueChanged != null)
                ValueChanged(this, new EventArgs());
        }





        private void trackScroll(TextBox box, TrackBar bar)
        {
            IgnoreEvent = true;
            box.Text = bar.Value.ToString();
            OnValueChanged();
        }
        private void TextChanged(TextBox box, TrackBar bar)
        {
            if (IgnoreEvent)
            {
                IgnoreEvent = false;
                return;
            }
            bar.Value = GetSaveInteger(box.Text);
            OnValueChanged();
        }



        #region Scroll
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            trackScroll(textBox1, trackBar1);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            trackScroll(textBox2, trackBar2);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            trackScroll(textBox3, trackBar3);
        }
        #endregion

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextChanged(textBox1, trackBar1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TextChanged(textBox2, trackBar2);
        }

        /// <summary>
        /// Handles the TextChanged event of the textBox3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            TextChanged(textBox3, trackBar3);
        }


    }
}