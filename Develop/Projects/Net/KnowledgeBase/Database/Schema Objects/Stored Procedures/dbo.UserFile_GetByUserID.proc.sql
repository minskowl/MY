-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 24.11.2008 16:15:13
	-- Description:	Generated CodeRocket. Select by ForeygnKey FK_UserFiles_Users
	-- =============================================

	CREATE PROCEDURE UserFile_GetByUserID( @UserID [int])
	AS
	BEGIN
		SELECT [FileName],[Size],[UserFileID],[UserID] 
		FROM [UserFiles]
		WHERE  [UserID]=@UserID 
	END


