CREATE PROCEDURE _Knowledges_GetKeywordsAssociations	(	@KnowledgeID int	)
AS
SELECT [KeywordID]
FROM [KnowledgeKeywords] 
WHERE [KnowledgeKeywords].[KnowledgeID]=@KnowledgeID;


