-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE KnowledgeCategoryAdd
	@CategoryID , 
	@KnowledgeID , 

AS
BEGIN
	 INSERT [KnowledgeCategory]
           (
$columnList
			)
     VALUES
           (
		@CategoryID, 
		@KnowledgeID, 
			)
END
GO