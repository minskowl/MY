using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using KnowledgeBase.Core;
using Savchin.Core;
using Savchin.IO;
using Savchin.Wpf.Core;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.FormatProviders.Html;
using Telerik.Windows.Documents.Layout;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.Proofing;

namespace KnowledgeBase.TelerikEditor
{
    /// <summary>
    /// Interaction logic for RitchEditorControl.xaml
    /// </summary>
    public partial class RitchEditorControl : ISummaryEditor
    {
        #region Properties


        public IEnumerable<NameValuePair<string>> UserFiles { set; private get; }
        public IEnumerable<NameValuePair<string>> ArticleFiles { set; private get; }



        public string Value
        {
            get
            {
                var provider = new HtmlFormatProvider
                {
                    ExportSettings = new HtmlExportSettings
                    {
                        ImageExportMode = ImageExportMode.AutomaticInline,
                        DocumentExportLevel = DocumentExportLevel.Fragment
                    }
                };
                return provider.Export(RadDocument);
            }
            set
            {
                RadDocument = string.IsNullOrEmpty(value) ? new RadDocument() : new HtmlFormatProvider().Import(value);
            }
        }

        private RadDocument RadDocument
        {
            get
            {
                return editor.Document;
            }
            set
            {
                SetupNewDocument(value);
                editor.Document = value;

                //  ExamplesDataContext dataContext = new ExamplesDataContext();

                //this.editor.Document.MailMergeDataSource.ItemsSource = dataContext.Employees;
                //this.editor.Users = dataContext.Users;
            }
        }
        #endregion




        public RitchEditorControl()
        {
            InitializeComponent();

            // This method is used only to work around limitations for using MEF in Examples.
            //ExtensibleUILoader.LoadExtensibleUIComponents(this.editor);

            if (this.IsDesignMode()) return;

            DictionaryCache.SetDictionaries((DocumentSpellChecker)editor.SpellChecker);
            listLanguages.ItemsSource = DictionaryCache.Dictionaries.Keys;
        }



        private void SetupNewDocument(RadDocument document)
        {
            document.LayoutMode = DocumentLayoutMode.Paged;
            document.ParagraphDefaultSpacingAfter = 10;
            document.PageViewMargin = new SizeF(10, 10);
            document.SectionDefaultPageMargin = new Padding(95);
        }

        private bool IsSupportedImageFormat(string extension)
        {
            if (extension != null)
            {
                extension = extension.ToLower();
            }

            return Mime.ImagesTypes.Contains(extension.Substring(1));
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            string statistics = this.editor.Document.GetStatisticsInfo().ToString();
            MessageBox.Show(statistics, "Document Statistics", MessageBoxButton.OK);
        }

        private void ViewDocumentStructure_Click(object sender, RoutedEventArgs e)
        {
            var window = new RadWindow();

            //DocumentModelTreeViewer documentTreeViewer = new DocumentModelTreeViewer();
            //documentTreeViewer.DisplayDocumentLayoutTree(this.editor.Document);

            //window.Content = documentTreeViewer;
            window.Owner = this;

            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Width = 600;
            window.Height = 750;

            window.ShowDialog();
        }

        private void editor_Drop(object sender, DragEventArgs e)
        {
            var droppedFiles = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (droppedFiles == null) return;

            foreach (var droppedFile in droppedFiles)
            {
                string extension = Path.GetExtension(droppedFile);
                if (!IsSupportedImageFormat(extension)) continue;
                var file = new System.IO.FileInfo(droppedFile);
                using (Stream imageStream = file.OpenRead())
                {
                    editor.InsertImage(imageStream, extension);
                }
            }
        }

        private void listLanguagesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var checker = (DocumentSpellChecker)editor.SpellChecker;
            if (e.AddedItems.Count > 0)
                checker.SpellCheckingCulture = (CultureInfo)e.AddedItems[0];
            else
                checker.SpellCheckingCulture = null;
        }
    }
}
