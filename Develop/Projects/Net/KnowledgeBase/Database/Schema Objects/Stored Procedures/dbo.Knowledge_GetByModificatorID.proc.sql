-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 04.11.2008 22:13:55
	-- Description:	Generated CodeRocket. Select by ForeygnKey FK_Knowledges_Modificator
	-- =============================================

	CREATE PROCEDURE Knowledge_GetByModificatorID( @ModificatorID [int])
	AS
	BEGIN
		SELECT [CategoryID],[CreationDate],[CreatorID],[KnowledgeID],[KnowledgeStatusID],[KnowledgeTypeID],[ModificationDate],[ModificatorID],[PublicID],[Summary],[Title] 
		FROM [Knowledges]
		WHERE  [ModificatorID]=@ModificatorID 
	END


