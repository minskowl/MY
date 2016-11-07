-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 09.05.2008 20:02:36
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE _User_GetByLogin( @Login [nvarchar](50))
AS
	SELECT * FROM [Users]
	WHERE [Login]=@Login


