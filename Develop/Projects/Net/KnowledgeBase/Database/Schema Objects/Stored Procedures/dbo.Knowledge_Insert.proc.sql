-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 04.11.2008 22:13:55
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE Knowledge_Insert(@CategoryID [int],@CreationDate [datetime],@CreatorID [int],@KnowledgeStatusID [tinyint],@KnowledgeTypeID [smallint],@ModificationDate [datetime],@ModificatorID [int],@Summary [ntext],@Title [nvarchar](150), @KnowledgeID [int] OUTPUT)
AS
BEGIN
	 INSERT [Knowledges]
           ([CategoryID],[CreationDate],[CreatorID],[KnowledgeStatusID],[KnowledgeTypeID],[ModificationDate],[ModificatorID],[Summary],[Title])
     VALUES
           (@CategoryID,@CreationDate,@CreatorID,@KnowledgeStatusID,@KnowledgeTypeID,@ModificationDate,@ModificatorID,@Summary,@Title)
	SELECT @KnowledgeID=@@IDENTITY
END


