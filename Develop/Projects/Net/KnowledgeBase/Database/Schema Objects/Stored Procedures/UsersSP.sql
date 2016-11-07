                                                        
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='User_Insert')
BEGIN
		DROP PROCEDURE [dbo].[User_Insert]
END
GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 09.05.2008 10:56:23
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE User_Insert(@CreationDate [datetime],@Email [nvarchar](150),@FirstName [nvarchar](50),@LastName [nvarchar](50),@Login [nvarchar](50),@Password [nvarchar](50),@SecurityAnswer [nvarchar](50),@SecurityQuestion [nvarchar](50), @UserID [int] OUTPUT)
AS
BEGIN
	 INSERT [Users]
           ([CreationDate],[Email],[FirstName],[LastName],[Login],[Password],[SecurityAnswer],[SecurityQuestion])
     VALUES
           (@CreationDate,@Email,@FirstName,@LastName,@Login,@Password,@SecurityAnswer,@SecurityQuestion)
	SELECT @UserID=@@IDENTITY
END
GO
IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='User_GetAll')
BEGIN
		DROP PROCEDURE [dbo].[User_GetAll]
END
GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 09.05.2008 10:56:23
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE User_GetAll
AS
BEGIN
	SELECT [CreationDate],[Email],[FirstName],[LastName],[Login],[Password],[SecurityAnswer],[SecurityQuestion],[UserID] 
	FROM [Users]
END
GO
IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='User_Update')
BEGIN
		DROP PROCEDURE [dbo].[User_Update]
END
GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 09.05.2008 10:56:23
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE User_Update(@CreationDate [datetime],@Email [nvarchar](150),@FirstName [nvarchar](50),@LastName [nvarchar](50),@Login [nvarchar](50),@Password [nvarchar](50),@SecurityAnswer [nvarchar](50),@SecurityQuestion [nvarchar](50),@UserID [int])
AS
BEGIN
	UPDATE [Users]
	SET 
		[CreationDate]=@CreationDate,[Email]=@Email,[FirstName]=@FirstName,[LastName]=@LastName,[Login]=@Login,[Password]=@Password,[SecurityAnswer]=@SecurityAnswer,[SecurityQuestion]=@SecurityQuestion
	WHERE [UserID]=@UserID       
END
GO
IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='User_Delete')
BEGIN
		DROP PROCEDURE [dbo].[User_Delete]
END
GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 09.05.2008 10:56:23
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE User_Delete(@UserID [int])
AS
BEGIN
	DELETE FROM [Users]
	WHERE [UserID]=@UserID       
END
GO
IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='User_GetByID')
BEGIN
		DROP PROCEDURE [dbo].[User_GetByID]
END
GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 09.05.2008 10:56:23
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE User_GetByID(@UserID [int])
AS
BEGIN
	SELECT [CreationDate],[Email],[FirstName],[LastName],[Login],[Password],[SecurityAnswer],[SecurityQuestion],[UserID] 
	FROM [Users]
	WHERE [UserID]=@UserID 
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO