-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 31.10.2008 23:28:15
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE Keyword_Update(@KeywordID [int],@KeywordStatusID [tinyint],@KeywordTypeID [smallint],@Name [nvarchar](50))
	AS
	BEGIN
		UPDATE [Keywords]
		SET 
			[KeywordStatusID]=@KeywordStatusID,[KeywordTypeID]=@KeywordTypeID,[Name]=@Name
		WHERE [KeywordID]=@KeywordID        
	END


