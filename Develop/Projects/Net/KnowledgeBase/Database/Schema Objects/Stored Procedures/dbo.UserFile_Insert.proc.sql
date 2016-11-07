-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 24.11.2008 16:15:13
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE UserFile_Insert(@FileName [nvarchar](100),@Size [int],@UserID [int], @UserFileID [int] OUTPUT)
AS
BEGIN
	 INSERT [UserFiles]
           ([FileName],[Size],[UserID])
     VALUES
           (@FileName,@Size,@UserID)
	SELECT @UserFileID=@@IDENTITY
END


