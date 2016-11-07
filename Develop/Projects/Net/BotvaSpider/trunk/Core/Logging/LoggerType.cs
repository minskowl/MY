using System.ComponentModel;

namespace BotvaSpider.Core
{
    /// <summary>
    /// LoggerType
    /// </summary>
    public enum LoggerType
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("Все")]
        All = 0,
        /// <summary>
        /// 
        /// </summary>
        [Description("Бодалка")]
        Fights = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("Шахта")]
        Mine = 2,
        /// <summary>
        /// 
        /// </summary>
        [Description("Бухгалтерия")]
        Accountant = 3,
        /// <summary>
        /// 
        /// </summary>
        [Description("Вывод")]
        Output = 4,
        /// <summary>
        /// 
        /// </summary>
        [Description("Системные")]
        System = 5
    }
}