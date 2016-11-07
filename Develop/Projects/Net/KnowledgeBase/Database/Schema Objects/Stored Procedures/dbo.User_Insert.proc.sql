-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 19.10.2008 16:28:47
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE User_Insert(@CreationDate [datetime],@Email [nvarchar](150),@FirstName [nvarchar](50),@LastName [nvarchar](50),@Login [nvarchar](50),@Password [nvarchar](50),@RootPermissionID [smallint],@SecurityAnswer [nvarchar](50),@SecurityQuestion [nvarchar](50), @UserID [int] OUTPUT)
AS
BEGIN
	 INSERT [Users]
           ([CreationDate],[Email],[FirstName],[LastName],[Login],[Password],[RootPermissionID],[SecurityAnswer],[SecurityQuestion])
     VALUES
           (@CreationDate,@Email,@FirstName,@LastName,@Login,@Password,@RootPermissionID,@SecurityAnswer,@SecurityQuestion)
	SELECT @UserID=@@IDENTITY
END


