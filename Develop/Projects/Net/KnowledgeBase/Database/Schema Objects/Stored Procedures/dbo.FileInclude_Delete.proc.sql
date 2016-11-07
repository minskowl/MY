-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 28.11.2008 13:31:23
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE FileInclude_Delete(@FileIncludeID [uniqueidentifier])
	AS
	BEGIN
		DELETE FROM [FileIncludes]
		WHERE [FileIncludeID]=@FileIncludeID        
	END


