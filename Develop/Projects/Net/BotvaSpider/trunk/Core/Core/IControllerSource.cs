namespace BotvaSpider.Core
{
    /// <summary>
    /// IControllerSource
    /// </summary>
    public interface IControllerSource
    {
        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <value>The controller.</value>
        GameController Controller { get; }
    }
}
