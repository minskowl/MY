-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 20.10.2008 23:15:46
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE Category_GetByID(@CategoryID [int])
	AS
	BEGIN
		SELECT [CategoryID],[CreationDate],[Name],[ParentCategoryID] 
		FROM [Categories]
		WHERE [CategoryID]=@CategoryID  
	END


