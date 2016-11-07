-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE KeywordAdd
	@KeywordID , 
	@Name , 

AS
BEGIN
	 INSERT [Keywords]
           (
$columnList
			)
     VALUES
           (
		@KeywordID, 
		@Name, 
			)
END
GO