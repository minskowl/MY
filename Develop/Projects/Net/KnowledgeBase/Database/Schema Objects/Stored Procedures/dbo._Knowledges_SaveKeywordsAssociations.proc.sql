CREATE PROCEDURE _Knowledges_SaveKeywordsAssociations	(	@KnowledgeID int	)

AS
SET NOCOUNT ON
DECLARE @tempExists INT;

EXEC _ExistsTempTable '#Int32',@tempExists OUTPUT;

IF @tempExists=0
BEGIN
 PRINT '#Int32 not exists'
 DELETE  
 FROM [KnowledgeKeywords] 
 WHERE [KnowledgeID]=@KnowledgeID 
 RETURN;
END
PRINT '#Int32 exists'
BEGIN TRAN

DELETE [KnowledgeKeywords] 
FROM [KnowledgeKeywords] 
WHERE [KnowledgeID]=@KnowledgeID  AND [KeywordID] NOT IN (SELECT Value FROM #Int32 )
IF @@ERROR<>0
BEGIN
   ROLLBACK TRAN
   DROP TABLE #Int32
   RETURN;
END

INSERT INTO [KnowledgeKeywords] ([KnowledgeID],[KeywordID])
SELECT @KnowledgeID, Value
FROM #Int32 
WHERE Value NOT IN( SELECT [KeywordID] FROM [KnowledgeKeywords] WHERE [KnowledgeID]=@KnowledgeID)

IF @@ERROR<>0
BEGIN
   ROLLBACK TRAN
   DROP TABLE #Int32
   RETURN;
END

COMMIT TRAN 

DROP TABLE #Int32
SET NOCOUNT OFF


