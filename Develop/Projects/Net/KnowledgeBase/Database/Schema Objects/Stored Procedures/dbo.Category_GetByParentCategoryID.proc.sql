-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 20.10.2008 23:15:46
	-- Description:	Generated CodeRocket. Select by ForeygnKey FK_Categories_Categories
	-- =============================================

	CREATE PROCEDURE Category_GetByParentCategoryID( @ParentCategoryID [int])
	AS
	BEGIN
		SELECT [CategoryID],[CreationDate],[Name],[ParentCategoryID] 
		FROM [Categories]
		WHERE  [ParentCategoryID]=@ParentCategoryID 
	END


