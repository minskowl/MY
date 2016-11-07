-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 04.11.2008 22:13:55
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE Knowledge_Delete(@KnowledgeID [int])
	AS
	BEGIN
		DELETE FROM [Knowledges]
		WHERE [KnowledgeID]=@KnowledgeID        
	END


