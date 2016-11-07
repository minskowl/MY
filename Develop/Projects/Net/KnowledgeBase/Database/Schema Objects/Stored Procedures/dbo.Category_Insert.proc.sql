-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 20.10.2008 23:15:46
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE Category_Insert(@CreationDate [datetime],@Name [nvarchar](50),@ParentCategoryID [int], @CategoryID [int] OUTPUT)
AS
BEGIN
	 INSERT [Categories]
           ([CreationDate],[Name],[ParentCategoryID])
     VALUES
           (@CreationDate,@Name,@ParentCategoryID)
	SELECT @CategoryID=@@IDENTITY
END


