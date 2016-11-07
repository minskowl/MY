                          
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Category_Insert')
BEGIN
		DROP PROCEDURE [dbo].[Category_Insert]
END
GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 24.05.2008 18:23:30
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
GO
IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Category_GetAll')
BEGIN
		DROP PROCEDURE [dbo].[Category_GetAll]
END
GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 24.05.2008 18:23:30
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE Category_GetAll
AS
BEGIN
	SELECT [CategoryID],[CreationDate],[Name],[ParentCategoryID] 
	FROM [Categories]
END
GO
IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Category_Update')
BEGIN
		DROP PROCEDURE [dbo].[Category_Update]
END
GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 24.05.2008 18:23:30
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
GO
IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Category_Delete')
BEGIN
		DROP PROCEDURE [dbo].[Category_Delete]
END
GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 24.05.2008 18:23:30
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE Category_Delete(@CategoryID [int])
AS
BEGIN
	DELETE FROM [Categories]
	WHERE [CategoryID]=@CategoryID       
END
GO
IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Category_GetByID')
BEGIN
		DROP PROCEDURE [dbo].[Category_GetByID]
END
GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 24.05.2008 18:23:30
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE Category_GetByID(@CategoryID [int])
AS
BEGIN
	SELECT [CategoryID],[CreationDate],[Name],[ParentCategoryID] 
	FROM [Categories]
	WHERE [CategoryID]=@CategoryID 
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO