
            



PRINT N'                 START CREATE PROCEDURES 	               '

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


									

	IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Product_Insert')
	BEGIN
			DROP PROCEDURE [dbo].[Product_Insert]
	END
	GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 08.11.2009 9:38:22
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE Product_Insert(@CreationDate [datetime],@Name [nvarchar](100),@PrivateKey [varbinary],@PublicKey [varbinary], @ProductID [int] OUTPUT)
AS
BEGIN
	 INSERT [Products]
           ([CreationDate],[Name],[PrivateKey],[PublicKey])
     VALUES
           (@CreationDate,@Name,@PrivateKey,@PublicKey)
	SELECT @ProductID=@@IDENTITY
END
GO

	IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Product_GetAll')
	BEGIN
			DROP PROCEDURE [dbo].[Product_GetAll]
	END
	GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 08.11.2009 9:38:22
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE Product_GetAll
AS
BEGIN
	SELECT [CreationDate],[Name],[PrivateKey],[ProductID],[PublicKey] 
	FROM [Products]
END
GO

	
																																																		
			IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Product_Update')
	BEGIN
			DROP PROCEDURE [dbo].[Product_Update]
	END
	GO
	-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 08.11.2009 9:38:22
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE Product_Update(@ProductID [int],@Name [nvarchar](100),@PublicKey [varbinary],@PrivateKey [varbinary],@CreationDate [datetime])
	AS
	BEGIN
		UPDATE [Products]
		SET 
			[Name]=@Name,[PublicKey]=@PublicKey,[PrivateKey]=@PrivateKey,[CreationDate]=@CreationDate
		WHERE [ProductID]=@ProductID        
	END
	GO
			IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Product_Delete')
	BEGIN
			DROP PROCEDURE [dbo].[Product_Delete]
	END
	GO
	-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 08.11.2009 9:38:22
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE Product_Delete(@ProductID [int])
	AS
	BEGIN
		DELETE FROM [Products]
		WHERE [ProductID]=@ProductID        
	END
	GO
			IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Product_GetByID')
	BEGIN
			DROP PROCEDURE [dbo].[Product_GetByID]
	END
	GO
	-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 08.11.2009 9:38:22
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE Product_GetByID(@ProductID [int])
	AS
	BEGIN
		SELECT [CreationDate],[Name],[PrivateKey],[ProductID],[PublicKey] 
		FROM [Products]
		WHERE [ProductID]=@ProductID  
	END
	GO

-- ========================FOREIGN KEY GENERATION =====================

	SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
PRINT N'                 END CREATE PROCEDURES 	               '