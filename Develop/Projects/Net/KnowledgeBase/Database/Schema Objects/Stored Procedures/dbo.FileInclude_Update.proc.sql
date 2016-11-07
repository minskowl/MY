-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 28.11.2008 13:31:23
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE FileInclude_Update(@FileIncludeID [uniqueidentifier],@KnowledgeID [int],@FileName [nvarchar](100),@Size [int])
	AS
	BEGIN
		UPDATE [FileIncludes]
		SET 
			[FileIncludeID]=@FileIncludeID,[KnowledgeID]=@KnowledgeID,[FileName]=@FileName,[Size]=@Size
		WHERE [FileIncludeID]=@FileIncludeID        
	END


