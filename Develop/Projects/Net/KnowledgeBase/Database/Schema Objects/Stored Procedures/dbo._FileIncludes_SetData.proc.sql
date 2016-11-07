CREATE PROCEDURE _FileIncludes_SetData	(	@FileIncludeID UNIQUEIDENTIFIER, @Data image 	)
AS
UPDATE dbo.FileIncludes
SET Data=@Data
WHERE dbo.FileIncludes.FileIncludeID=@FileIncludeID


