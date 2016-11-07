-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 09.05.2008 20:02:36
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE _User_GetByLoginByPassword(@Login [nvarchar](50),@Password [nvarchar](50))
AS
BEGIN
	SELECT * FROM [Users]
	WHERE [Login]=@Login AND [Password]=@Password 
END


