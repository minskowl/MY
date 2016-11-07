using Savchin.CodeEditor.CodeComplition;

namespace Savchin.CodeEditor.CodeCompletion
{
    public interface ICompletionListWindow : ICompletionWindow
    {
        /// <summary>
        /// Gets/Sets the currently selected item.
        /// </summary>
        ICompletionItem SelectedItem { get; set; }
    }
}
