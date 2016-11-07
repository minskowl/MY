using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using BotvaSpider.Logging;
using WatiN.Core;

namespace BotvaSpider.Core
{

    /// <summary>
    /// 
    /// </summary>
    public class MailBox
    {
        private Timer timer;
        private int errorCount;
        List<MessageInfo> messages = new List<MessageInfo>();

        public event EventHandler<MessageEventArgs> MessageComming;

        /// <summary>
        /// Gets or sets the log.
        /// </summary>
        /// <value>The log.</value>
        private Logger log;

        private GameController controller;

        public MailBox(Logger log)
        {
            this.log = log;
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            if (timer != null) return;
            errorCount = 0;
            controller = new GameController(AppCore.LogSystem);
            timer = new Timer(OnTimerTick, controller, 1000, 300000);//5 minuntes
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
            if (controller != null)
            {
                controller.Dispose();
                controller = null;
            }
        }
        private void OnTimerTick(Object state)
        {

            FillBox((GameController)state);
        }

        private bool reading = false;
        /// <summary>
        /// Fills the box.
        /// </summary>
        private void FillBox(GameController controller)
        {
            if (reading) return;
            reading = true;
            try
            {
                log.Debug("Читаем почту.");
                controller.GoTo(controller.UrlPost);

                var table = controller.Browser.Table(Find.ByClass("default post_table"));

                foreach (TableRow row in table.TableRows)
                {
                    var cell2 = (row.TableCells[2]);
                    if (cell2.Links.Count <= 0) continue;

                    var message = new MessageInfo();
                    message.Date = DateTime.Parse(row.TableCells[1].Text);
                    var link = cell2.Links[0];
                    if (link.Text.StartsWith("Вы атаковали"))
                        message.EventType = EventType.Fight;
                    if (link.Text.Contains("атаковал вас"))
                        message.EventType = EventType.Attack;
                    message.IsWin = link.ClassName == "text_main_1 fight_win";
                    message.Title = link.Text;
                    message.Severity = message.IsWin ? EventSeverity.Normal : EventSeverity.High;

                    if (messages.Where(e => e.Equals(message)).Count() > 0) return;
                    messages.Add(message);
                    OnMessageComming(new MessageEventArgs(message));
                }
            }
            catch (ThreadAbortException)
            {
                Stop();
            }
            catch (Exception ex)
            {
                log.Error("Ошибка чтения почты.", ex);
                errorCount++;
                if (AppCore.BotvaSettings.AllowDebugger) Debugger.Break();

                if (errorCount >= AppCore.BotvaSettings.MaxDangerousErrors)
                {
                    Stop();
                    AppCore.LogSystem.Error("Модуль чтения почты отключен.", "Произошло слишком много ошибок и модуль был отключен.", ex);
                }
            }
            finally
            {
                reading = false;
            }

        }

        protected virtual void OnMessageComming(MessageEventArgs args)
        {
            if (MessageComming != null)
                MessageComming(this, args);
        }


    }
}
