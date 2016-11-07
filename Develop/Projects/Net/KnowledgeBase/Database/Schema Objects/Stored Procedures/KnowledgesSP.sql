                                                  
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Knowledge_Insert')
BEGIN
		DROP PROCEDURE [dbo].[Knowledge_Insert]
END
GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 25.05.2008 16:07:33
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE Knowledge_Insert(@CreationDate [datetime],@CreatorID [int],@KnowledgeTypeID [smallint],@ModificationDate [datetime],@ModificatorID [int],@Summary [text],@Title [nvarchar](150), @KnowledgeID [int] OUTPUT)
AS
BEGIN
	 INSERT [Knowledges]
           ([CreationDate],[CreatorID],[KnowledgeTypeID],[ModificationDate],[ModificatorID],[Summary],[Title])
     VALUES
           (@CreationDate,@CreatorID,@KnowledgeTypeID,@ModificationDate,@ModificatorID,@Summary,@Title)
	SELECT @KnowledgeID=@@IDENTITY
END
GO
IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Knowledge_GetAll')
BEGIN
		DROP PROCEDURE [dbo].[Knowledge_GetAll]
END
GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 25.05.2008 16:07:33
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE Knowledge_GetAll
AS
BEGIN
	SELECT [CreationDate],[CreatorID],[KnowledgeID],[KnowledgeTypeID],[ModificationDate],[ModificatorID],[Summary],[Title] 
	FROM [Knowledges]
END
GO
IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Knowledge_Update')
BEGIN
		DROP PROCEDURE [dbo].[Knowledge_Update]
END
GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 25.05.2008 16:07:33
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE Knowledge_Update(@CreationDate [datetime],@CreatorID [int],@KnowledgeID [int],@KnowledgeTypeID [smallint],@ModificationDate [datetime],@ModificatorID [int],@Summary [text],@Title [nvarchar](150))
AS
BEGIN
	UPDATE [Knowledges]
	SET 
		[CreationDate]=@CreationDate,[CreatorID]=@CreatorID,[KnowledgeTypeID]=@KnowledgeTypeID,[ModificationDate]=@ModificationDate,[ModificatorID]=@ModificatorID,[Summary]=@Summary,[Title]=@Title
	WHERE [KnowledgeID]=@KnowledgeID       
END
GO
IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Knowledge_Delete')
BEGIN
		DROP PROCEDURE [dbo].[Knowledge_Delete]
END
GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 25.05.2008 16:07:33
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE Knowledge_Delete(@KnowledgeID [int])
AS
BEGIN
	DELETE FROM [Knowledges]
	WHERE [KnowledgeID]=@KnowledgeID       
END
GO
IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Knowledge_GetByID')
BEGIN
		DROP PROCEDURE [dbo].[Knowledge_GetByID]
END
GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 25.05.2008 16:07:33
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE Knowledge_GetByID(@KnowledgeID [int])
AS
BEGIN
	SELECT [CreationDate],[CreatorID],[KnowledgeID],[KnowledgeTypeID],[ModificationDate],[ModificatorID],[Summary],[Title] 
	FROM [Knowledges]
	WHERE [KnowledgeID]=@KnowledgeID 
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO