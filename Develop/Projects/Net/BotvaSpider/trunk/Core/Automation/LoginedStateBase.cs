using System;
using BotvaSpider.Configuration;
using BotvaSpider.Core;
using BotvaSpider.Data;
using BotvaSpider.Gears;

namespace BotvaSpider.Automation
{
    /// <summary>
    /// LoginedStateBase
    /// </summary>
    public class LoginedStateBase : MachineStateBase
    {
        protected readonly UserImporter importer = new UserImporter();
        private CowsSearcher cowsSearcher;


        /// <summary>
        /// Initializes a new instance of the <see cref="FightLoginedState"/> class.
        /// </summary>
        /// <param name="automaton">The automaton.</param>
        public LoginedStateBase(MachineBase automaton)
            : base(automaton)
        {
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>The state.</value>
        public override MachineState State
        {
            get { return MachineState.Loggined; }
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        public override void Login()
        {

        }
        /// <summary>
        /// Logins this instance.
        /// </summary>
        /// <returns></returns>
        public override bool DoTraning()
        {
            return (AppCore.AccountantSettings.InvestmentEnabled) ?
                     automaton.Coach.DoTraning() : false;
        }
        /// <summary>
        /// Sleeps the specified span.
        /// </summary>
        /// <param name="sleepTo">The sleep to.</param>
        /// <param name="canDisguise">if set to <c>true</c> [can disguise].</param>
        public override void Sleep(DateTime sleepTo, bool canDisguise)
        {
            if (canDisguise) automaton.Player.PrepareForAction(PlayerAction.Sleep);

            Log.Debug("Уснули до " + sleepTo);

            //if (AppCore.AccountantSettings.InvestmentEnabled &&
            //    AppCore.AccountantSettings.NormalStrategy.IsSet(InvestmentStrategy.BuySpecifiedStuff) &&
            //    AppCore.AccountantSettings.NormalStrategy.HasAction)
            //{
            //    automaton.Accountant.MakeNormalInvestment(sleepTo);
            //}
            if (DateTime.Now >= sleepTo) return;

            if (AppCore.GameSettings.BotvaSettings.RefreshInfo)
            {
                UpdateUsersInfo(sleepTo);
            }

            if (DateTime.Now >= sleepTo) return;
            if (AppCore.GameSettings.BotvaSettings.UseCowSearch)
            {
                DoSearchNewCows(sleepTo);
            }
            if (DateTime.Now < sleepTo) base.Sleep(sleepTo, canDisguise);
        }

        /// <summary>
        /// Updates the player status.
        /// </summary>
        public override void UpdatePlayerStatus()
        {
        tryAgain:
            automaton.Player.Update();

            ClearWardrobe(automaton.Player.Wardrobe);

            if (AppCore.GameSettings.BotvaSettings.AutoTreat &&
                automaton.Player.Health < automaton.Player.MaxHealth - 300)
            {
                automaton.Player.TryTreat();
            }

            Log.Debug("Обновили состояние игрока", automaton.Player.ToString(), automaton.Player.Clone());


            if (automaton.Player.Health <= 25)
            {
                Sleep(new TimeSpan(0, 10, 0), true);
                if (automaton.CancelingAction) return;
                Controller.GoTo(automaton.Controller.UrlAccount);
                goto tryAgain;
            }

        }
        private void ClearWardrobe(Wardrobe wardrobe)
        {
            if (wardrobe.EmptySlotsCount >= AppCore.BotvaSettings.Wardrobe.MinEmptySlots) return;

            var smallTicketAction = AppCore.MinerSettings.GetTicketAction(Ticket.Small);
            var bigTicketAction = AppCore.MinerSettings.GetTicketAction(Ticket.Big);

            var bigTicketUrl =automaton.Controller.ItemBuilder.GetItemSmallImageUrl(Ticket.Big);
            var smallTicketUrl = automaton.Controller.ItemBuilder.GetItemSmallImageUrl(Ticket.Small);
            //Use tickets
            if (smallTicketAction.ActionType == TicketActionType.Use)
                UseTicket(true);

            if (bigTicketAction.ActionType == TicketActionType.Use)
                UseTicket(false);

            //Move tickets to bag
            automaton.Controller.ClearWardrobe(delegate(string url)
                                                   {
                                                       if (smallTicketAction.ActionType != TicketActionType.Use)
                                                       {
                                                           return string.Compare(url, smallTicketUrl, true) == 0;
                                                       }
                                                       if (bigTicketAction.ActionType != TicketActionType.Use)
                                                       {
                                                           return string.Compare(url, bigTicketUrl, true) == 0;
                                                       }
                                                       return false;
                                                   });
            //Sell tickets
            if (smallTicketAction.ActionType == TicketActionType.Sale)
                automaton.Controller.Sell(Ticket.Small, Price.Crystals(smallTicketAction.Price));

            if (bigTicketAction.ActionType == TicketActionType.Sale)
                automaton.Controller.Sell(Ticket.Big, Price.Crystals(bigTicketAction.Price));





            automaton.Player.UpdateItems();

            if (!automaton.Player.WardrobeHasEmtySlots)
            {
                AppCore.LogFights.Suggestion("Освободите одевалку.",
                                             "При полной одевалке бот не ходит в дозоры и неинвестирует в шмот.");

            }
        }

        private void UseTicket(bool isSmallTicket)
        {
            if (!automaton.Miner.CanVisitGlade) return;

            var ticketsCount = automaton.Player.Wardrobe.GetItemCount(isSmallTicket ? Ticket.Small : Ticket.Big);
            if (ticketsCount == 0) return;

            Log.DebugFormat("Используем {0} билетов", ticketsCount);

            for (var i = 0; i < ticketsCount; i++)
            {
                automaton.Miner.VisitGlade(isSmallTicket);
                if (!automaton.Miner.CanVisitGlade) return;
            }


        }

        private void UpdateUsersInfo(DateTime sleepTo)
        {
            var users = ObjectProvider.Instance.GetUsersForUpdate();
            foreach (var user in users)
            {
                if (Controller.ShowUserInfo(user.Name))
                {
                    user.UpdateFormUserPage(Browser);
                    ObjectProvider.Instance.UpdateUser(user);
                    ObjectProvider.Instance.UserSaveSkills(user);
                }
                else
                {
                    ObjectProvider.Instance.DeleteUser(user);
                }
                if (DateTime.Now >= sleepTo) return;
            }
        }

        private void DoSearchNewCows(DateTime sleepTo)
        {
            if (cowsSearcher == null) cowsSearcher = new CowsSearcher(Controller, importer);
            cowsSearcher.StopSearch = (() => DateTime.Now >= sleepTo);
            cowsSearcher.StartSearch();

        }

        /// <summary>
        /// Searches the crystal.
        /// </summary>
        public override void SearchCrystal()
        {


            automaton.Player.PrepareForAction(PlayerAction.SearchMine);
            var sleepTime = automaton.Miner.StartSearchCristal();

            if (sleepTime.HasValue)
                Sleep(sleepTime.Value, false);

            automaton.Miner.GetCrystal();
        }




    }
}