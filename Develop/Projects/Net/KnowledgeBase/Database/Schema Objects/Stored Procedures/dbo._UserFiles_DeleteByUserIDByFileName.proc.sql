CREATE PROCEDURE _UserFiles_DeleteByUserIDByFileName	(
	@UserID int ,
	@FileName VARCHAR(200)
	)

AS
DELETE FROM dbo.UserFiles
WHERE UserID=@UserID AND FileName=@FileName


