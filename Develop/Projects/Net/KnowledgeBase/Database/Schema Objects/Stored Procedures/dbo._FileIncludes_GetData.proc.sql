CREATE PROCEDURE _FileIncludes_GetData	(	@FileIncludeID UNIQUEIDENTIFIER 	)
AS
SELECT Data 
FROM dbo.FileIncludes
WHERE dbo.FileIncludes.FileIncludeID=@FileIncludeID


