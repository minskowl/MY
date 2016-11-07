#region Version & Copyright
/* 
 * $Id: RegularExpressions.cs 35265 2008-07-17 11:27:52Z svd $ 
 *
 * <:COPYRIGHT_LICENSE:>
 */
#endregion

using System.Text.RegularExpressions;

namespace Savchin.Text
{
    /// <summary>
    /// RegularExpressions
    /// </summary>
    public static class RegularExpressions
    {
        public const string MoneyAmmount = @"[\d]+([\.,]\d{1,2})?";
        public const string Email = @"^\S+@\S+\.(\S*[^\s\d]+\S*)$";
        public const string PersistentUrlPattern = @"^[\w_\-\/]*\{[^\}\{]+\}([\w_\-\/]*(\{[^\}\{]+\})*)*$";
        public const string Token = @"{[^\}\{]+\}";
        public const string TokenParser = @"\{(?<token>[^{].*?[^}])\}";

        public const string IntExpression = @"^(-|\+)?\d+$";
        public const string FloatExpression = @"^(-|\+)?\d+(\.\d{1,5})?$";

        public const string UrlGroups = @"^((?<scheme>[A-Za-z0-9_+.]{1,8}):)?(//)?((?<userinfo>[!-~]+)@)?(?<host>[^/?#:]*)(:(?<port>[0-9]+))?(/(?<path>[^?#]*))?(\?(?<query>[^#]*))?(#(?<fragment>.*))?$";
        public const string Url = @"^(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
        public const string UrlWithoutScheme = @"^([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
        public const string UrlWithoutSchemeGroups = @"^(?<host>[^/?#:]*)(:(?<port>[0-9]+))?(/(?<path>[^?#]*))?(\?(?<query>[^#]*))?(#(?<fragment>.*))?$";
        /// <summary>
        /// Email Regex
        /// </summary>
        public static Regex EmailRegex = new Regex(Email, RegexOptions.Compiled);
        /// <summary>
        /// PersistentUrlPattern Regex
        /// </summary>
        public static Regex PersistentUrlPatternRegex = new Regex(PersistentUrlPattern, RegexOptions.Compiled);

        /// <summary>
        /// TokenRegex
        /// </summary>
        public static Regex TokenRegex = new Regex(Token, RegexOptions.Compiled);

        /// <summary>
        /// TokenParser Regex
        /// </summary>
        public static Regex TokenParserRegex = new Regex(TokenParser, RegexOptions.Compiled);

        /// <summary>
        /// UrlWithoutSchemeRegex
        /// </summary>
        public static Regex UrlWithoutSchemeRegex = new Regex(UrlWithoutScheme, RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        /// <summary>
        /// NonwordCharacterRegex
        /// </summary>
        public static Regex NonwordCharacterRegex = new Regex("\\W", RegexOptions.Compiled);

        /// <summary>
        /// StorageSizeRegex
        /// </summary>
        public static Regex StorageSizeRegex = new Regex(@"^(?<digit>\d+(,\d+)?)\s*(?<key>\wb)?$", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);
    }
}
