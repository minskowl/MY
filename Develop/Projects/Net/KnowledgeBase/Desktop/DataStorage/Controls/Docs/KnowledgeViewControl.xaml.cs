using KnowledgeBase.Core;

namespace KnowledgeBase.Desktop.Controls.Docs
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
            Title = _knowledge.Title;
            var view = AppCore.GetObject<IKnowledgeView>();
            view.Knowledge = _knowledge;
            Content = view;
        }




    }
}
