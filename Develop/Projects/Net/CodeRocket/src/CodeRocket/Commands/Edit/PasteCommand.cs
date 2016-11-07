namespace CodeRocket.Commands.Edit
{
    class PasteCommand : EditorCommand
    {
        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            AciveEditor.Paste();
        }
    }
}
