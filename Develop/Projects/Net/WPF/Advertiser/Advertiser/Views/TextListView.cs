using System;
using System.Collections.Generic;
using System.Linq;
using Advertiser.Controls;

namespace Advertiser.Views
{
    public class StringView
    {
        public StringView(string title)
        {
            Text = title;
        }

        public string Title
        {
            get { return Text; }
        }
        public string Text { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Text;
        }
    }


    public class TextListView : ItemListView<StringView>
    {
        private static readonly TextControl _view = new TextControl();

       
        public override object ActiveView
        {
            get { return _view; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextListView"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="source">The source.</param>
        public TextListView(string name, List<string> source)
            : base(name, source.Select(e => new StringView(e)).ToList())
        {

        }


        protected override void OnAddItem()
        {
            SelectedItem = new StringView(String.Empty);
        }

        protected override bool CanSave()
        {
            return base.CanSave() && SelectedItem != null && !string.IsNullOrWhiteSpace(SelectedItem.Text);
        }

        protected override void OnSaveItem()
        {
            base.OnSaveItem();

            if (!Items.Contains(SelectedItem))
                Items.Add(SelectedItem);
        }
    }
}
