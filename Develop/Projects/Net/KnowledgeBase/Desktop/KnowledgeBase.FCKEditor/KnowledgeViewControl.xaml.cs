using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KnowledgeBase.Core;

namespace KnowledgeBase.FCKEditor
{
    /// <summary>
    /// Interaction logic for KnowledgeViewControl.xaml
    /// </summary>
    public partial class KnowledgeViewControl : IKnowledgeView
    {
        private KnowledgeView _knowledge;

        /// <summary>
        /// Gets or sets the knowledge ID.
        /// </summary>
        /// <value>The knowledge ID.</value>
        public KnowledgeView Knowledge
        {
            get { return _knowledge; }
            set
            {
                if (_knowledge != value)
                {
                    _knowledge = value;
                    KnowledgeChanged();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KnowledgeViewControl"/> class.
        /// </summary>
        public KnowledgeViewControl()
        {
            InitializeComponent();
        }


        private void KnowledgeChanged()
        {
            var myBrowser = new System.Windows.Forms.WebBrowser();

            browserHost.Child = myBrowser;


            var filePath = _knowledge.GetContentLocalPath();
            if (filePath != null)
            {
                myBrowser.Navigate(filePath);
            }
            else
            {
                myBrowser.DocumentText = _knowledge.GetKnowledgeHtml();
            }
        }

    }
}
