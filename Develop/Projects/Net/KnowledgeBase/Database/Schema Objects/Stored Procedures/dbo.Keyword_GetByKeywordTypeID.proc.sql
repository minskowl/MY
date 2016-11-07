-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 31.10.2008 23:28:15
	-- Description:	Generated CodeRocket. Select by ForeygnKey FK_Keywords_KeywordTypes
	-- =============================================

	CREATE PROCEDURE Keyword_GetByKeywordTypeID( @KeywordTypeID [smallint])
	AS
	BEGIN
		SELECT [CreationDate],[KeywordID],[KeywordStatusID],[KeywordTypeID],[Name] 
		FROM [Keywords]
		WHERE  [KeywordTypeID]=@KeywordTypeID 
	END


