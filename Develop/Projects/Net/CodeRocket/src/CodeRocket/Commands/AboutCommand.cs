using System;
using CodeRocket.Controls;
using Savchin.Core;
using Savchin.Forms.Core.Commands;


namespace CodeRocket.Commands
{

    /// <summary>
    /// AboutCommand
    /// </summary>
    class AboutCommand : SimpleCommand
    {

        #region Overrides of Command

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public override void Execute()
        {
            using (var about = new FormAbout())
            {
                about.ShowDialog();
            }
        }

        #endregion
    }
}
