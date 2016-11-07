using System.Windows.Forms;
using BotvaSpider.Controls;
using BotvaSpider.Core;
using Savchin.Forms.Core.Commands;

namespace BotvaSpider.Tools.Commands
{
    class AddStaffListCommand : Command
    {
        private readonly IControllerSource _sourceController;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClearStaffListCommand"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public AddStaffListCommand(IControllerSource controller)
        {
            this._sourceController = controller;
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void Execute(object parameter, object target)
        {
            using(var form= new FormStaffLists())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                _sourceController.Controller.AddStaffList(form.ListType, form.Users);
            }
        }
    }
}