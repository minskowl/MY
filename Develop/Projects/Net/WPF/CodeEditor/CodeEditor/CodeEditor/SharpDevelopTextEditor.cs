// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.AddIn;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.SharpDevelop.Editor.CodeCompletion;
using Savchin.CodeEditor.CSharpBinding;
using Savchin.CodeEditor.CodeCompletion;
using Savchin.CodeEditor.Services.Parse;

namespace Savchin.CodeEditor
{
    /// <summary>
    /// The text editor used in SharpDevelop.
    /// Serves both as base class for CodeEditorView and as text editor control
    /// for editors used in other parts of SharpDevelop (e.g. all ConsolePad-based controls)
    /// </summary>
    public class SharpDevelopTextEditor : TextEditor
    {
      
        private CodeCompletionEditorAdapter Adapter;
        private NRefactoryCodeCompletionBinding Binding;
        //static SharpDevelopTextEditor()
        //{
        //    DefaultStyleKeyProperty.OverrideMetadata(typeof(SharpDevelopTextEditor),
        //                                             new FrameworkPropertyMetadata(typeof(SharpDevelopTextEditor)));
        //}

       
        protected readonly CodeEditorOptions options;

        public SharpDevelopTextEditor()
        {
            TextArea.DefaultInputHandler.CommandBindings.Add(new CommandBinding(CustomCommands.CtrlSpaceCompletion, OnCodeCompletion));

           // AvalonEditDisplayBinding.RegisterAddInHighlightingDefinitions();

            //this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Print, OnPrint));
            //this.CommandBindings.Add(new CommandBinding(ApplicationCommands.PrintPreview, OnPrintPreview));

            options = CodeEditorOptions.Instance;
            options.BindToTextEditor(this);
            Adapter = new CodeCompletionEditorAdapter(this);

            TextArea.TextEntering += TextAreaTextEntering;
            TextArea.TextEntered += TextAreaTextEntered;
            Binding= new CSharpCompletionBinding();
            ParserService.RegisterEditor(this);
        }

      
        public virtual string FileName
        {
            get { return GetHashCode() + ".cs"; }
         
        }

        SharpDevelopCompletionWindow completionWindow;
        SharpDevelopInsightWindow insightWindow;

        void CloseExistingCompletionWindow()
        {
            if (completionWindow != null)
            {
                completionWindow.Close();
            }
        }

        void CloseExistingInsightWindow()
        {
            if (insightWindow != null)
            {
                insightWindow.Close();
            }
        }

        public SharpDevelopCompletionWindow ActiveCompletionWindow
        {
            get { return completionWindow; }
        }

        public SharpDevelopInsightWindow ActiveInsightWindow
        {
            get { return insightWindow; }
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

        internal void ShowInsightWindow(SharpDevelopInsightWindow window)
        {
            CloseExistingInsightWindow();
            insightWindow = window;
            window.Closed += delegate
            {
                insightWindow = null;
            };
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(
                delegate
                {
                    if (insightWindow == window)
                    {
                        window.Show();
                    }
                }
            ));
        }
        void OnCodeCompletion(object sender, ExecutedRoutedEventArgs e)
        {
            if (completionWindow != null)
                completionWindow.Close();

            CtrlSpace(Adapter);
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

        void TextAreaTextEntered(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && !e.Handled)
            {
                ILanguageBinding languageBinding = Adapter.Language;
                if (languageBinding != null && languageBinding.FormattingStrategy != null)
                {
                    char c = e.Text[0];
                    // When entering a newline, AvalonEdit might use either "\r\n" or "\n", depending on
                    // what was passed to TextArea.PerformTextInput. We'll normalize this to '\n'
                    // so that formatting strategies don't have to handle both cases.
                    if (c == '\r')
                        c = '\n';
                    languageBinding.FormattingStrategy.FormatLine(Adapter, c);

                    if (c == '\n')
                    {
                        // Immediately parse on enter.
                        // This ensures we have up-to-date CC info about the method boundary when a user
                        // types near the end of a method.
                        ParserService.BeginParse(this.FileName,Adapter.Document.CreateSnapshot());
                    }
                }
            }
        }

        void TextAreaTextEntering(object sender, TextCompositionEventArgs e)
        {
            // don't start new code completion if there is still a completion window open
            if (completionWindow != null)
                return;

            if (e.Handled)
                return;

            // disable all code completion bindings when CC is disabled
            if (!CodeCompletionOptions.EnableCodeCompletion)
                return;

            TextArea textArea = TextArea;
            if (textArea.ActiveInputHandler != textArea.DefaultInputHandler)
                return; // deactivate CC for non-default input handlers

            ITextEditor adapter = Adapter;

            foreach (char c in e.Text)
            {

                {
                    CodeCompletionKeyPressResult result = Binding.HandleKeyPress(adapter, c);
                    if (result == CodeCompletionKeyPressResult.Completed)
                    {
                        if (completionWindow != null)
                        {
                            // a new CompletionWindow was shown, but does not eat the input
                            // tell it to expect the text insertion
                            completionWindow.ExpectInsertionBeforeStart = true;
                        }
                        if (insightWindow != null)
                        {
                            insightWindow.ExpectInsertionBeforeStart = true;
                        }
                        return;
                    }
                    else if (result == CodeCompletionKeyPressResult.CompletedIncludeKeyInCompletion)
                    {
                        if (completionWindow != null)
                        {
                            if (completionWindow.StartOffset == completionWindow.EndOffset)
                            {
                                completionWindow.CloseWhenCaretAtBeginning = true;
                            }
                        }
                        return;
                    }
                    else if (result == CodeCompletionKeyPressResult.EatKey)
                    {
                        e.Handled = true;
                        return;
                    }
                }
            }
        }
    }

    sealed class ZoomLevelToTextFormattingModeConverter : IValueConverter
    {
        public static readonly ZoomLevelToTextFormattingModeConverter Instance = new ZoomLevelToTextFormattingModeConverter();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (((double)value) == 1.0)
                return TextFormattingMode.Display;
            else
                return TextFormattingMode.Ideal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
