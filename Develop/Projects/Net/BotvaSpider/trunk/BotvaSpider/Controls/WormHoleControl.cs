using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BotvaSpider.Data;

namespace BotvaSpider.Controls
{
    public partial class WormHoleControl : UserControl
    {
        private List<byte> sequnces;



        /// <summary>
        /// Initializes a new instance of the <see cref="WormHoleControl"/> class.
        /// </summary>
        public WormHoleControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;
            ShowTotals();
            sequnces = ObjectProvider.Instance.GetWormHoles();
            pageControl1.Pages = sequnces.Count;
        }


        #region Buttons click
        private void buttonSave_Click(object sender, EventArgs e)
        {
            var results = holelMap.GetResult();
            if (results.Count != 1)
            {
                MessageBox.Show(this, "Червоточинка одна.", "Ошибка");
                return;
            }
            var hole = (byte)results[0];
            ObjectProvider.Instance.SaveWormHole(hole);
            sequnces.Add(hole);
            pageControl1.Pages = sequnces.Count;
            holelMap.Clear();
            ShowTotals();
            MessageBox.Show(this, "Сохранено.");
        }



        #endregion
        private void checkBoxSlideMode_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSlideMode.Checked)
            {
                pageControl1.Visible = false;
                ShowTotals();
            }
            else
            {
                pageControl1.Visible = true;
                ShowCurrentSlide();
            }
        }

        private void ShowCurrentSlide()
        {
            ShowHole(sequnces[pageControl1.CurrentPage]);
        }

        private void pageControl1_CurrentPageChanged(object sender, EventArgs e)
        {
            ShowCurrentSlide();
        }

        private void ShowTotals()
        {
            holeMapResult.Show(ObjectProvider.Instance.GetTopWormHole());
        }


        /// <summary>
        /// Shows the hole.
        /// </summary>
        /// <param name="hole">The hole.</param>
        private void ShowHole(byte hole)
        {
            holeMapResult.Show(new Dictionary<int, int> { { hole, 1 } });
        }




    }
}
