-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 31.10.2008 23:28:15
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE Keyword_Delete(@KeywordID [int])
	AS
	BEGIN
		DELETE FROM [Keywords]
		WHERE [KeywordID]=@KeywordID        
	END


