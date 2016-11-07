using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FlatSearcher.Core;
using MyCustomWebBrowser.Core;
using Savchin.Forms;

namespace FlatSearcher.Controls
{
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var aresess = SearchContext.Current.Data.Adresses;
            listAddresees.DataSource = aresess;
            listAddresees.DisplayMember = "Key";
            foreach (var adr in aresess)
            {
                listAddresees.SetItemChecked(listAddresees.Items.IndexOf(adr), adr.Visible);
            }

        }

        private void listAddresees_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var adr = (Address)listAddresees.Items[e.Index];
            adr.Visible = e.NewValue == CheckState.Checked;
            adr.Manual = true;
        }

        private void listAddresees_DoubleClick(object sender, EventArgs e)
        {
            var adr = (Address)listAddresees.SelectedItem;
            if (adr == null) return;

            using (var frm = new FormText())
            {
                frm.Value = adr.Key;
                if (frm.ShowDialog() == DialogResult.OK)
                    adr.Key = frm.Value;
            }


        }
    }
}
