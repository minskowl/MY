CREATE PROCEDURE _Knowledges_GetShortInfoByCategoryID	(	@CategoryID int	)

AS
SELECT 	*
FROM KnowledgeInfo  
WHERE KnowledgeInfo.CategoryID=@CategoryID


