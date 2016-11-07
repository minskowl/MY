
namespace Savchin.Data.MSSQL
{
    /// <summary>
    /// sql error codes.
    /// </summary> 
    internal enum ErrorsCode : int
    {
        /// <summary>
        /// NoErrors?
        /// </summary>
        NoErrors = 0,

        /// <summary>
        /// String or binary data would be truncated
        /// </summary>
        TruncateData = 8152,

        /// <summary>
        /// Cannot insert duplicate key row in object '%.*ls' with unique index '%.*ls'.
        /// </summary>     
        InsertDuplicatedKey = 2601,

        /// <summary>
        /// Violation of %ls constraint '%.*ls'. Cannot insert duplicate key in object '%.*ls'.
        /// </summary> 
        ConstraintInsertDuplicatedKey = 2627,

        /// <summary>
        /// %ls statement conflicted with %ls %ls constraint '%.*ls'. The conflict occurred in database '%.*ls', table '%.*ls'%ls%.*ls%ls.
        /// </summary> 
        ConstraintStatementConflicted = 547,

        /// <summary>
        /// The statement has been terminated.
        /// </summary> 
        StatementTerminated = 3621,

        /// <summary>
        /// Cannot insert the value NULL into column '%.*ls', table '%.*ls'; column does not allow nulls. %ls fails.
        /// </summary> 
        CannotInsertDBNull = 515,




        /// <summary>
        /// StoredProcedureNotFind
        /// </summary>
        StoredProcedureNotFind = 2812,
        /// <summary>
        /// @i_nLanguageId is not a parameter for procedure mrs_ProjectLanguageListSelectOne.
        /// </summary>
        StoredProcedureNotFindParrameter = 8145,
        /// <summary>
        ///  Procedure 'mrs_CodificatorInsert' expects parameter '@i_nProjectId', which was not supplied
        /// </summary>
        StoredProcedureExpectParrameter = 201,

        /// <summary>
        /// UserDefined ValidateException
        /// </summary>
        UserDefinedValidateException = 50000,

        /// <summary>
        /// Input parameters cannot be NULL
        /// </summary> 
        InputParameterDBNull = 50001,


    }
}