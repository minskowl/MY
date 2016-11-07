CREATE PROCEDURE _Categories_GetTree

AS
	SELECT ISNULL([ParentCategoryID],0),[CategoryID] 
	FROM [Categories] 
	ORDER BY 1,2


