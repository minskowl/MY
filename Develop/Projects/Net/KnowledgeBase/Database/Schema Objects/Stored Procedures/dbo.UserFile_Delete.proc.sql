-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 24.11.2008 16:15:13
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE UserFile_Delete(@UserFileID [int])
	AS
	BEGIN
		DELETE FROM [UserFiles]
		WHERE [UserFileID]=@UserFileID        
	END


