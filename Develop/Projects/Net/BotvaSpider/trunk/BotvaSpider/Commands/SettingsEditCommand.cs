using BotvaSpider.Controls.Configuration;
using Savchin.Forms.Core.Commands;

namespace BotvaSpider.Commands
{
    class SettingsEditCommand : Command
    {
        #region Overrides of Command

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <param name="target">The target.</param>
        public override void Execute(object parameter, object target)
        {
            FormSettings.EditSettings();
          
        }

        #endregion
    }
}
