-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 31.10.2008 23:28:15
	-- Description:	Generated CodeRocket. Select by ForeygnKey FK_Keywords_KeywordStatuses
	-- =============================================

	CREATE PROCEDURE Keyword_GetByKeywordStatusID( @KeywordStatusID [tinyint])
	AS
	BEGIN
		SELECT [CreationDate],[KeywordID],[KeywordStatusID],[KeywordTypeID],[Name] 
		FROM [Keywords]
		WHERE  [KeywordStatusID]=@KeywordStatusID 
	END


