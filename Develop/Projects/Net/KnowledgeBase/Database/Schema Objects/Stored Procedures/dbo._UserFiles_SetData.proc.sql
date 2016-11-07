CREATE PROCEDURE _UserFiles_SetData	(	@UserFileID INT, @Data image 	)
AS
UPDATE dbo.UserFiles
SET Data=@Data
WHERE dbo.UserFiles.UserFileID=@UserFileID


