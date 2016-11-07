using KnowledgeBase.Core;
using Telerik.Windows.Documents.FormatProviders.Html;

namespace KnowledgeBase.TelerikEditor
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

        public KnowledgeViewControl()
        {
            InitializeComponent();
        }

        private void KnowledgeChanged()
        {
            //Title = _knowledge.Title;
            var provider = new HtmlFormatProvider();
            boxEditor.Document = provider.Import(_knowledge.GetKnowledgeHtml());

        }
    }
}
