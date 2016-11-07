using System.Windows.Forms;
using BotvaSpider.Controls;
using BotvaSpider.Core;
using BotvaSpider.Tools.Core;
using Savchin.Forms.Core.Commands;
using Savchin.Threading;

namespace BotvaSpider.Tools.Commands
{
    class ClearStaffListCommand : Command
    {
        private readonly IControllerSource _sourceController;
        private readonly IObjectViewer _view;
        /// <summary>
        /// Initializes a new instance of the <see cref="ClearStaffListCommand"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public ClearStaffListCommand(IControllerSource controller, IObjectViewer view)
        {
            this._sourceController = controller;
            _view = view;
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public override void Execute(object parameter, object target)
        {
            StaffListType type;
            using (var form = new FormStaffLists())
            {
                if (form.ShowDialog() != DialogResult.OK) return;
                type = form.ListType;

            }
            _view.ShowStatus("Начинае очищать список");
            var controller = _sourceController.Controller;
            new async(() => controller.ClearStaffList(type, ShowStatus));

        }

        private void ShowStatus(string message, int percentage)
        {
            _view.ShowStatus(message);
        }
    }
}