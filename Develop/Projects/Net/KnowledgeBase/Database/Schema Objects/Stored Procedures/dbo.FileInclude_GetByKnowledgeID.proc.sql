-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 28.11.2008 13:31:23
	-- Description:	Generated CodeRocket. Select by ForeygnKey FK_FileIncludes_FileIncludes
	-- =============================================

	CREATE PROCEDURE FileInclude_GetByKnowledgeID( @KnowledgeID [int])
	AS
	BEGIN
		SELECT [FileIncludeID],[FileName],[KnowledgeID],[Size] 
		FROM [FileIncludes]
		WHERE  [KnowledgeID]=@KnowledgeID 
	END


