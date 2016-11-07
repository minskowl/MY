-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 28.11.2008 13:31:23
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE FileInclude_GetAll
AS
BEGIN
	SELECT [FileIncludeID],[FileName],[KnowledgeID],[Size] 
	FROM [FileIncludes]
END


