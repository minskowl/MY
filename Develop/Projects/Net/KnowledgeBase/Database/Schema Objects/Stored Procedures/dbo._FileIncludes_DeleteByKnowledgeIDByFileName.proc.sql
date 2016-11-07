CREATE PROCEDURE _FileIncludes_DeleteByKnowledgeIDByFileName	(
	@KnowledgeID int ,
	@FileName VARCHAR(200)
	)

AS
DELETE FROM dbo.FileIncludes
WHERE KnowledgeID=@KnowledgeID AND FileName=@FileName


