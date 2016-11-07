CREATE PROCEDURE _UserFiles_GetData	(	@UserFileID int 	)
AS
SELECT Data 
FROM dbo.UserFiles 
WHERE dbo.UserFiles.UserFileID=@UserFileID


