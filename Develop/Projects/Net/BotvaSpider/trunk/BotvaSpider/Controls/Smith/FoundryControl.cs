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
using BotvaSpider.Core;
using BotvaSpider.Data;

namespace BotvaSpider.Controls
{
    public partial class FoundryControl : UserControl
    {
        List<List<int>> seqence;

        public FoundryControl()
        {
            InitializeComponent();
            seqence = ObjectProvider.Instance.GetMines();
            pageControl1.Pages = seqence.Count;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.UserControl.Load"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode) ShowTotals();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var results = controlMap.GetResult();
            if (results.Count != 7)
            {
                MessageBox.Show(this, "Мин должно быть 7.", "Ошибка");
                return;
            }

            ObjectProvider.Instance.SaveMineMap(results);
            seqence.Add(results);
            pageControl1.Pages = seqence.Count;

            controlMap.Clear();
            ShowTotals();
            MessageBox.Show(this, "Сохранено.");
        }
        private void ShowTotals()
        {
            controlResult.Show(ObjectProvider.Instance.GetMineMapPositionsTop());
        }

        private void pageControl1_CurrentPageChanged(object sender, EventArgs e)
        {
            ShowCurrentSequnece();
        }
        private void ShowCurrentSequnece()
        {
            controlResult.Show(seqence[pageControl1.CurrentPage]);
        }

        private void checkBoxSlideMode_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxSlideMode.Checked)
            {
                pageControl1.Visible = false;
                ShowTotals();
            }
            else
            {
                pageControl1.Visible = true;
                ShowCurrentSequnece();
            }
        }

    }
}
