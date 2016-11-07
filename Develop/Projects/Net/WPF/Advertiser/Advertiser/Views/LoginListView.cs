using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using Advertiser.Controllers;
using Advertiser.Controls;
using Advertiser.Core;
using Advertiser.Entities;
using Savchin.Core;
using Savchin.Wpf.Input;
using WatiN.Core;
using WatiN.Core.Interfaces;

namespace Advertiser.Views
{
    public class LoginListView : EntityListView<Login>
    {
        private readonly ILogWriter _logWriter;
        private readonly LoginControl _control = new LoginControl();
        public NameValuePair[] Sites { get; set; }


        public override object ActiveView
        {
            get { return _control; }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="LoginListView"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public LoginListView(List<Login> source, ILogWriter logWriter)
            : base("Аккаунты", source)
        {
            _logWriter = logWriter;
            Sites = EnumHelper.GetData(typeof(Site));

            ContextMenu.Add(new MenuItem
            {
                Header = "Войти на сайт",
                Command = new DelegateCommand(DoLogon, HasSelection)
            });

            ContextMenu.Add(new MenuItem
            {
                Header = "Очистить объявы",
                Command = new DelegateCommand(DoClear, HasSelection)
            });

            ContextMenu.Add(new MenuItem
            {
                Header = "Поднять объявы",
                Command = new DelegateCommand(DoUp, HasSelection)
            });
        }


        protected override bool CanSave()
        {
            return base.CanSave() && SelectedItem != null &&
                !string.IsNullOrWhiteSpace(SelectedItem.User) &&
                !string.IsNullOrWhiteSpace(SelectedItem.Password);
        }

        private void DoUp()
        {
            ProcessInController(e =>
                                    {
                                        e.DoUp();
                                        e.DoLogout();
                                    });
        }

        private void DoClear()
        {
            ProcessInController(e =>
                                    {
                                        e.Clear();
                                        e.DoLogout();
                                    });
        }

        private bool HasSelection()
        {
            return SelectedItem != null;
        }

        private void DoLogon()
        {
            ProcessInController(closeBrowser: false);
        }

        private void ProcessInController(Action<IAdvController> action = null, bool closeBrowser = true)
        {
            var login = SelectedItem;
            if (login == null) return;

            Publisher.DoAsync(delegate
            {
                var controller = AdvControllerBase.CreateController(login);
                if (controller == null) return;

                using (var br = new IE { AutoClose = closeBrowser })
                using (controller)
                {
                    controller.Browser = br;
                    controller.LogWriter = _logWriter;
                    controller.DoLogin(login);
                    if (action != null)
                        action(controller);
                }
            });
        }





    }
}
