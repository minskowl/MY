using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.AddIn;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.SharpDevelop.Editor.CodeCompletion;
using Savchin.CodeEditor.CodeCompletion;

namespace Savchin.CodeEditor
{
    public class CodeEditorControl : TextEditor, ITextEditor
    {


        CompletionWindow completionWindow;
        AvalonEditDocumentAdapter document;
        FileName _fileName = new FileName(@"D:\MY\Develop\Projects\Net\WPF\CodeEditor\Demo\Resources\Test.cs");

        public event KeyEventHandler KeyPress
        {
            add { TextArea.PreviewKeyDown += value; }
            remove { TextArea.PreviewKeyDown -= value; }
        }

        public event EventHandler SelectionChanged
        {
            add { TextArea.SelectionChanged += value; }
            remove { TextArea.SelectionChanged -= value; }
        }

        public CodeEditorControl()
        {
            TextArea.DefaultInputHandler.CommandBindings.Add(new CommandBinding(CustomCommands.CtrlSpaceCompletion, OnCodeCompletion));

            this.Caret = new CaretAdapter(TextArea.Caret);
           


            OnDocumentChanged(EventArgs.Empty);
        }
        protected override void OnDocumentChanged(EventArgs e)
        {
            base.OnDocumentChanged(e);
            if (Document != null)
                document = new AvalonEditDocumentAdapter(this.Document, this);
            else
                document = null;
        }
        void OnCodeCompletion(object sender, ExecutedRoutedEventArgs e)
        {
            if (completionWindow != null)
                completionWindow.Close();

            CtrlSpace(this);
        }

        public bool CtrlSpace(ITextEditor editor)
        {
            NRefactoryCtrlSpaceCompletionItemProvider provider = new NRefactoryCtrlSpaceCompletionItemProvider(LanguageProperties.CSharp);
            provider.AllowCompleteExistingExpression = true;
            // on Ctrl+Space, include items (e.g. types / extension methods) from all namespaces, regardless of imports
            provider.ShowItemsFromAllNamespaces = true;
            provider.ShowCompletion(editor);
            return true;
        }


         IDocument ITextEditor.Document
        {
            get { return document; }
        }

        public ITextEditorCaret Caret { get; private set; }

        public FileName FileName
        {
            get { return _fileName; }
        }

        public ICompletionListWindow ShowCompletionWindow(ICompletionItemList data)
        {
            if (data == null || !data.Items.Any())
                return null;
            SharpDevelopCompletionWindow window = new SharpDevelopCompletionWindow(this, TextArea, data);
            ShowCompletionWindow(window);
            return window;
        }

        internal void ShowCompletionWindow(SharpDevelopCompletionWindow window)
        {
            CloseExistingCompletionWindow();
            completionWindow = window;
            window.Closed += delegate
            {
                completionWindow = null;
            };
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(
                delegate
                {
                    if (completionWindow == window)
                    {
                        window.Show();
                    }
                }
            ));
        }
        void CloseExistingCompletionWindow()
        {
            if (completionWindow != null)
            {
                completionWindow.Close();
            }
        }
		
        public IEnumerable<ICompletionItem> GetSnippets()
        {
            //CodeSnippetGroup g = SnippetManager.Instance.FindGroup(Path.GetExtension(this.FileName));
            //if (g != null)
            //{
            //    return g.Snippets.Select(s => s.CreateCompletionItem(this));
            //}
            //else
            //{
            //    return base.GetSnippets();
            //}
            return new ICompletionItem[0];
        }
    }
}
