-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 19.10.2008 16:28:47
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE User_Update(@CreationDate [datetime],@Email [nvarchar](150),@FirstName [nvarchar](50),@LastName [nvarchar](50),@Login [nvarchar](50),@Password [nvarchar](50),@RootPermissionID [smallint],@SecurityAnswer [nvarchar](50),@SecurityQuestion [nvarchar](50),@UserID [int])
	AS
	BEGIN
		UPDATE [Users]
		SET 
			[CreationDate]=@CreationDate,[Email]=@Email,[FirstName]=@FirstName,[LastName]=@LastName,[Login]=@Login,[Password]=@Password,[RootPermissionID]=@RootPermissionID,[SecurityAnswer]=@SecurityAnswer,[SecurityQuestion]=@SecurityQuestion
		WHERE [UserID]=@UserID        
	END


