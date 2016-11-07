-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 28.11.2008 13:31:23
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE FileInclude_Insert(@FileIncludeID [uniqueidentifier],@FileName [nvarchar](100),@KnowledgeID [int],@Size [int])
AS
BEGIN
	 INSERT [FileIncludes]
           ([FileIncludeID],[FileName],[KnowledgeID],[Size])
     VALUES
           (@FileIncludeID,@FileName,@KnowledgeID,@Size)
END


