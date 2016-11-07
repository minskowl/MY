CREATE PROCEDURE _FileIncludes_CreateFromUserFile
	(
	@UserFileId int ,
	@FileIncludeID UNIQUEIDENTIFIER, 
	@KnowledgeID INT 
	)
AS
INSERT INTO dbo.FileIncludes (
	FileIncludeID,
	KnowledgeID,
	FileName,
	[Data],
	[Size]
) 
SELECT 
	@FileIncludeID,
	@KnowledgeID,
	dbo.GetUniuqueFileIncludeName(@KnowledgeID,dbo.UserFiles.FileName),
	dbo.UserFiles.Data,
	dbo.UserFiles.[Size]
FROM dbo.UserFiles 
WHERE dbo.UserFiles.UserFileID=@UserFileId


