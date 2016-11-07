-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 04.11.2008 22:13:55
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE Knowledge_GetByID(@KnowledgeID [int])
	AS
	BEGIN
		SELECT [CategoryID],[CreationDate],[CreatorID],[KnowledgeID],[KnowledgeStatusID],[KnowledgeTypeID],[ModificationDate],[ModificatorID],[PublicID],[Summary],[Title] 
		FROM [Knowledges]
		WHERE [KnowledgeID]=@KnowledgeID  
	END


