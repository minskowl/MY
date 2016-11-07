using System;
using FileTools.Controls;
using FileTools.Core;

namespace FileTools.Commands
{
    public class LockFileCommand : BaseCommand
    {
        public LockFileCommand(ILog log):base(log)
        {
        
        }

        #region Overrides of Command

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            using (var f = new FormLockFile())
                f.ShowDialog();
        }

        #endregion
    }
}