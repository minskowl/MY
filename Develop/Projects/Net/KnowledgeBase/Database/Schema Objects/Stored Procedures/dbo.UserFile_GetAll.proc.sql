-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 24.11.2008 16:15:13
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE UserFile_GetAll
AS
BEGIN
	SELECT [FileName],[Size],[UserFileID],[UserID] 
	FROM [UserFiles]
END


