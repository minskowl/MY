namespace BotvaSpider.Automation
{
    public enum Action
    {
        Login,
        Logout,
        Error,
        Rest,
        Sleep,
        Awake
    }

    public class Event
    {
        public static readonly Event Awake = new Event {Action = Action.Awake};
        public static readonly Event Login = new Event { Action = Action.Login };
        public static readonly Event Error = new Event { Action = Action.Error };
        public static readonly Event Sleep = new Event { Action = Action.Sleep };
        public static readonly Event Logout = new Event { Action = Action.Logout };
        public Action Action { get; set; }
        public object Parameter { get; set; }
    }
}