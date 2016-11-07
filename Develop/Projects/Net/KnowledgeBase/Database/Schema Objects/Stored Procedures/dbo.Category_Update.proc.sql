-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 20.10.2008 23:15:46
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE Category_Update(@CategoryID [int],@CreationDate [datetime],@Name [nvarchar](50),@ParentCategoryID [int])
	AS
	BEGIN
		UPDATE [Categories]
		SET 
			[CreationDate]=@CreationDate,[Name]=@Name,[ParentCategoryID]=@ParentCategoryID
		WHERE [CategoryID]=@CategoryID        
	END


