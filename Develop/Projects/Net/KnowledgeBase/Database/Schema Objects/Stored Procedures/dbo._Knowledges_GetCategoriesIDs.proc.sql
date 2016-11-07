CREATE PROCEDURE _Knowledges_GetCategoriesIDs	(	@KnowledgeID int 	)

AS
	SELECT [CategoryID]
	FROM [KnowledgeCategory] 
	WHERE [KnowledgeID]=@KnowledgeID


