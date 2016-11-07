-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 24.11.2008 16:15:13
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE UserFile_Update(@UserFileID [int],@UserID [int],@FileName [nvarchar](100),@Size [int])
	AS
	BEGIN
		UPDATE [UserFiles]
		SET 
			[UserID]=@UserID,[FileName]=@FileName,[Size]=@Size
		WHERE [UserFileID]=@UserFileID        
	END


