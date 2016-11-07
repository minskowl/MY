using System.Threading;
using BotvaSpider.Core;
using WatiN.Core;

namespace BotvaSpider.Automation
{
    class LogoutedState : MachineStateBase
    {
        private const string messageError = "User Logouted";

        /// <summary>
        /// Initializes a new instance of the <see cref="LogoutedState"/> class.
        /// </summary>
        /// <param name="automaton">The automaton.</param>
        public LogoutedState(MachineBase automaton)
            : base(automaton)
        {
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        public override MachineState State
        {
            get { return MachineState.Logouted; }
        }


        /// <summary>
        /// Logins this instance.
        /// </summary>
        public override void Login()
        {
        startLogin:
            if (Controller.Login())
                eventSink.CastEvent(Event.Login);
            else
                eventSink.CastEvent(Event.Error);

            var newCoolStatus = Controller.HasCoolStatus();
            if (newCoolStatus.HasValue)
            {
                AppCore.AcountSettings.CoolStatus = newCoolStatus.Value;
            }
            else
            {
                var sleepTime = Controller.GetTimerValue("countdown1");
                if (sleepTime.HasValue)
                {
                    var title = Browser.Div(Find.ByClass("title"));
                    Log.Info(string.Format("ждем {0} {1}", title.Exists ? title.Text : string.Empty, sleepTime.Value));
                    Thread.Sleep(sleepTime.Value);
                    goto startLogin;
                }
            }
        }




    }
}