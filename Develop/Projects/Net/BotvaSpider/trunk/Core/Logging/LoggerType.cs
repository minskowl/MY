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
        [Description("���")]
        All = 0,
        /// <summary>
        /// 
        /// </summary>
        [Description("�������")]
        Fights = 1,
        /// <summary>
        /// 
        /// </summary>
        [Description("�����")]
        Mine = 2,
        /// <summary>
        /// 
        /// </summary>
        [Description("�����������")]
        Accountant = 3,
        /// <summary>
        /// 
        /// </summary>
        [Description("�����")]
        Output = 4,
        /// <summary>
        /// 
        /// </summary>
        [Description("���������")]
        System = 5
    }
}