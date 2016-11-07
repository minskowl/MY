using System.Text.RegularExpressions;

namespace Savchin.Data.MSSQL
{
    internal abstract class ErrorMessageParser
    {
        protected static RegexOptions RegexOptionsDefault = RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline;
        internal abstract Regex CannotInsertNULLParser { get;}
        internal abstract Regex ConstraintStatementConflictedParser { get;}
    }

    internal class MSSQL2005ErrorMessageParser : ErrorMessageParser
    {
        private Regex constraintStatementConflictedParser = new Regex(
@"The\\s
(?\'OPERATION\'.*)?(?:\\sstatement\\sconflicted\\swith\\sthe\\s)\r\n
(?\'TYPE\'.*)?(?:\\sconstraint\\s"")\r\n
(?\'NAME\'.*)?(?:""\\.)", RegexOptionsDefault);

        private Regex cannotInsertNULLParser = new Regex(@"
Cannot\sinsert\sthe\svalue\sNULL\sinto\scolumn\s'
(?'COLUMN'.*)?(?:',\stable\s')
(?'NAME'.*)?(?:;\scolumn\sdoes\snot\sallow\snulls\.)
", RegexOptionsDefault);

        /// <summary>
        /// Gets the cannot insert NULL parser.
        /// </summary>
        /// <value>The cannot insert NULL parser.</value>
        internal override Regex CannotInsertNULLParser
        {
            get { return cannotInsertNULLParser; }
        }

        /// <summary>
        /// Gets the constraint statement conflicted parser.
        /// </summary>
        /// <value>The constraint statement conflicted parser.</value>
        internal override Regex ConstraintStatementConflictedParser
        {
            get { return constraintStatementConflictedParser; }
        }
    }

    
    
    internal class MSSQL2000ErrorMessageParser : ErrorMessageParser
    {
        private Regex constraintStatementConflictedParser = new Regex(@"
(?'OPERATION'.*)?(?:\sstatement\sconflicted\swith\s)
(?'TYPE'.*)?(?:\sconstraint\s')
(?'NAME'.*)?(?:'\.\sThe\sconflict\soccurred\sin\sdatabase\s')
(?'DBNAME'.*)?(?:',\stable\s')
(?'TBLNAME'.*)?(?:')
(
((?:,\scolumn\s')(?'CLMNAME'.*)?(?:'.))
    |
(\.)
)
", RegexOptionsDefault);

        private Regex cannotInsertNULLParser = new Regex(@"
Cannot\sinsert\sthe\svalue\sNULL\sinto\scolumn\s'
(?'COLUMN'.*)?(?:',\stable\s')
(?'NAME'.*)?(?:;\scolumn\sdoes\snot\sallow\snulls\.)
", RegexOptionsDefault);

        /// <summary>
        /// Gets the cannot insert NULL parser.
        /// </summary>
        /// <value>The cannot insert NULL parser.</value>
        internal override Regex CannotInsertNULLParser
        {
            get { return cannotInsertNULLParser; }
        }

        /// <summary>
        /// Gets the constraint statement conflicted parser.
        /// </summary>
        /// <value>The constraint statement conflicted parser.</value>
        internal override Regex ConstraintStatementConflictedParser
        {
            get { return constraintStatementConflictedParser; }
        }

    }
}
