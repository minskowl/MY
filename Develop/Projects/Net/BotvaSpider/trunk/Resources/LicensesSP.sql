
                



PRINT N'                 START CREATE PROCEDURES 	               '

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


																

	IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='License_Insert')
	BEGIN
			DROP PROCEDURE [dbo].[License_Insert]
	END
	GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 08.11.2009 21:32:07
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE License_Insert(@Count [int],@CreationDate [datetime],@LicenseID [int],@ProductID [int],@PublicKey [uniqueidentifier],@UserID [int],@Version [varchar],@WmTransferID [int])
AS
BEGIN
	 INSERT [Licenses]
           ([Count],[CreationDate],[LicenseID],[ProductID],[PublicKey],[UserID],[Version],[WmTransferID])
     VALUES
           (@Count,@CreationDate,@LicenseID,@ProductID,@PublicKey,@UserID,@Version,@WmTransferID)
END
GO

	IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='License_GetAll')
	BEGIN
			DROP PROCEDURE [dbo].[License_GetAll]
	END
	GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 08.11.2009 21:32:07
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE License_GetAll
AS
BEGIN
	SELECT [Count],[CreationDate],[LicenseID],[ProductID],[PublicKey],[UserID],[Version],[WmTransferID] 
	FROM [Licenses]
END
GO

	
																																																																															
			IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='License_Update')
	BEGIN
			DROP PROCEDURE [dbo].[License_Update]
	END
	GO
	-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 08.11.2009 21:32:07
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE License_Update(@LicenseID [int],@PublicKey [uniqueidentifier],@UserID [int],@ProductID [int],@CreationDate [datetime],@WmTransferID [int],@Count [int],@Version [varchar])
	AS
	BEGIN
		UPDATE [Licenses]
		SET 
			[LicenseID]=@LicenseID,[PublicKey]=@PublicKey,[UserID]=@UserID,[ProductID]=@ProductID,[CreationDate]=@CreationDate,[WmTransferID]=@WmTransferID,[Count]=@Count,[Version]=@Version
		WHERE [LicenseID]=@LicenseID        
	END
	GO
			IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='License_Delete')
	BEGIN
			DROP PROCEDURE [dbo].[License_Delete]
	END
	GO
	-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 08.11.2009 21:32:07
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE License_Delete(@LicenseID [int])
	AS
	BEGIN
		DELETE FROM [Licenses]
		WHERE [LicenseID]=@LicenseID        
	END
	GO
			IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='License_GetByID')
	BEGIN
			DROP PROCEDURE [dbo].[License_GetByID]
	END
	GO
	-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 08.11.2009 21:32:07
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE License_GetByID(@LicenseID [int])
	AS
	BEGIN
		SELECT [Count],[CreationDate],[LicenseID],[ProductID],[PublicKey],[UserID],[Version],[WmTransferID] 
		FROM [Licenses]
		WHERE [LicenseID]=@LicenseID  
	END
	GO

-- ========================FOREIGN KEY GENERATION =====================

			IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='License_GetByProductID')
	BEGIN
			DROP PROCEDURE [dbo].[License_GetByProductID]
	END
	GO
	-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 08.11.2009 21:32:07
	-- Description:	Generated CodeRocket. Select by ForeygnKey FK_Licenses_Peoducts
	-- =============================================

	CREATE PROCEDURE License_GetByProductID( @ProductID [int])
	AS
	BEGIN
		SELECT [Count],[CreationDate],[LicenseID],[ProductID],[PublicKey],[UserID],[Version],[WmTransferID] 
		FROM [Licenses]
		WHERE  [ProductID]=@ProductID 
	END
	GO
				IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='License_GetByUserID')
	BEGIN
			DROP PROCEDURE [dbo].[License_GetByUserID]
	END
	GO
	-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 08.11.2009 21:32:07
	-- Description:	Generated CodeRocket. Select by ForeygnKey FK_Licenses_Users
	-- =============================================

	CREATE PROCEDURE License_GetByUserID( @UserID [int])
	AS
	BEGIN
		SELECT [Count],[CreationDate],[LicenseID],[ProductID],[PublicKey],[UserID],[Version],[WmTransferID] 
		FROM [Licenses]
		WHERE  [UserID]=@UserID 
	END
	GO
				IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='License_GetByWmTransferID')
	BEGIN
			DROP PROCEDURE [dbo].[License_GetByWmTransferID]
	END
	GO
	-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 08.11.2009 21:32:07
	-- Description:	Generated CodeRocket. Select by ForeygnKey FK_Licenses_WM.Transfers
	-- =============================================

	CREATE PROCEDURE License_GetByWmTransferID( @WmTransferID [int])
	AS
	BEGIN
		SELECT [Count],[CreationDate],[LicenseID],[ProductID],[PublicKey],[UserID],[Version],[WmTransferID] 
		FROM [Licenses]
		WHERE  [WmTransferID]=@WmTransferID 
	END
	GO
		SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
PRINT N'                 END CREATE PROCEDURES 	               '