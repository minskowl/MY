using System;
using System.Windows.Forms;
using WatiN.Core;

namespace FlatSearcher.Controls
{
    public partial class BrowserControl : UserControl
    {
        private IE _watinBrowser;
        public IE WatinBrowser
        {
            get { return _watinBrowser ?? (_watinBrowser = new IE(webBrowser.ActiveXInstance, false)); }
        }

        public BrowserControl()
        {
            InitializeComponent();
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(textBox1.Text);
        }

        public void Navigate(string url)
        {
            textBox1.Text = url;
            webBrowser.Navigate(textBox1.Text);
        }

        private void buttonGoBack_Click(object sender, EventArgs e)
        {
            if (webBrowser.CanGoBack)
                webBrowser.GoBack();
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            if (webBrowser.CanGoForward)
                webBrowser.GoForward();
        }
    }
}
