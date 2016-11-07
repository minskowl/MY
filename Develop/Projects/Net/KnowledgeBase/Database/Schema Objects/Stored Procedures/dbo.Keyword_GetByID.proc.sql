-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 31.10.2008 23:28:15
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE Keyword_GetByID(@KeywordID [int])
	AS
	BEGIN
		SELECT [CreationDate],[KeywordID],[KeywordStatusID],[KeywordTypeID],[Name] 
		FROM [Keywords]
		WHERE [KeywordID]=@KeywordID  
	END


