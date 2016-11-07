-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 04.11.2008 22:13:55
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE Knowledge_Update(@CategoryID [int],@CreationDate [datetime],@CreatorID [int],@KnowledgeID [int],@KnowledgeStatusID [tinyint],@KnowledgeTypeID [smallint],@ModificationDate [datetime],@ModificatorID [int],@Summary [ntext],@Title [nvarchar](150))
	AS
	BEGIN
		UPDATE [Knowledges]
		SET 
			[CategoryID]=@CategoryID,[CreationDate]=@CreationDate,[CreatorID]=@CreatorID,[KnowledgeStatusID]=@KnowledgeStatusID,[KnowledgeTypeID]=@KnowledgeTypeID,[ModificationDate]=@ModificationDate,[ModificatorID]=@ModificatorID,[Summary]=@Summary,[Title]=@Title
		WHERE [KnowledgeID]=@KnowledgeID        
	END


