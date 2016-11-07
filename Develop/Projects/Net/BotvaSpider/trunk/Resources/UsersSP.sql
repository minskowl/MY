
                      



PRINT N'                 START CREATE PROCEDURES 	               '

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
-- Create date: 08.11.2009 10:08:00
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE User_Insert(@CreationDate [datetime],@Email [nvarchar](150),@FirstName [nvarchar](50),@IsAdmin [bit],@LastName [nvarchar](50),@Login [nvarchar](50),@Password [nvarchar](50),@SecurityAnswer [nvarchar](50),@SecurityQuestion [nvarchar](50), @UserID [int] OUTPUT)
AS
BEGIN
	 INSERT [Users]
           ([CreationDate],[Email],[FirstName],[IsAdmin],[LastName],[Login],[Password],[SecurityAnswer],[SecurityQuestion])
     VALUES
           (@CreationDate,@Email,@FirstName,@IsAdmin,@LastName,@Login,@Password,@SecurityAnswer,@SecurityQuestion)
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
-- Create date: 08.11.2009 10:08:00
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE User_GetAll
AS
BEGIN
	SELECT [CreationDate],[Email],[FirstName],[IsAdmin],[LastName],[Login],[Password],[SecurityAnswer],[SecurityQuestion],[UserID] 
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
	-- Create date: 08.11.2009 10:08:00
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE User_Update(@UserID [int],@Login [nvarchar](50),@Password [nvarchar](50),@FirstName [nvarchar](50),@LastName [nvarchar](50),@Email [nvarchar](150),@SecurityQuestion [nvarchar](50),@SecurityAnswer [nvarchar](50),@IsAdmin [bit],@CreationDate [datetime])
	AS
	BEGIN
		UPDATE [Users]
		SET 
			[Login]=@Login,[Password]=@Password,[FirstName]=@FirstName,[LastName]=@LastName,[Email]=@Email,[SecurityQuestion]=@SecurityQuestion,[SecurityAnswer]=@SecurityAnswer,[IsAdmin]=@IsAdmin,[CreationDate]=@CreationDate
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
	-- Create date: 08.11.2009 10:08:00
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
	-- Create date: 08.11.2009 10:08:00
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE User_GetByID(@UserID [int])
	AS
	BEGIN
		SELECT [CreationDate],[Email],[FirstName],[IsAdmin],[LastName],[Login],[Password],[SecurityAnswer],[SecurityQuestion],[UserID] 
		FROM [Users]
		WHERE [UserID]=@UserID  
	END
	GO

-- ========================FOREIGN KEY GENERATION =====================

	SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
PRINT N'                 END CREATE PROCEDURES 	               '