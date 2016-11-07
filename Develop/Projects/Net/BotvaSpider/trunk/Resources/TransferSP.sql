
                



PRINT N'                 START CREATE PROCEDURES 	               '

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


													

	IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Transfer_Insert')
	BEGIN
			DROP PROCEDURE [dbo].[Transfer_Insert]
	END
	GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 07.11.2009 22:52:52
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE Transfer_Insert(@Ammount [decimal](18,2),@Date [datetime],@Description [nvarchar](0),@InvoiceId [int],@Purse [varchar],@TransferId [int], @ID [int] OUTPUT)
AS
BEGIN
	 INSERT [WM.Transfers]
           ([Ammount],[Date],[Description],[InvoiceId],[Purse],[TransferId])
     VALUES
           (@Ammount,@Date,@Description,@InvoiceId,@Purse,@TransferId)
	SELECT @ID=@@IDENTITY
END
GO

	IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Transfer_GetAll')
	BEGIN
			DROP PROCEDURE [dbo].[Transfer_GetAll]
	END
	GO
-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 07.11.2009 22:52:52
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE Transfer_GetAll
AS
BEGIN
	SELECT [Ammount],[Date],[Description],[ID],[InvoiceId],[Purse],[TransferId] 
	FROM [WM.Transfers]
END
GO

	
																																																																				
			IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Transfer_Update')
	BEGIN
			DROP PROCEDURE [dbo].[Transfer_Update]
	END
	GO
	-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 07.11.2009 22:52:52
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE Transfer_Update(@ID [int],@InvoiceId [int],@TransferId [int],@Purse [varchar],@Ammount [decimal](18,2),@Description [nvarchar](0),@Date [datetime])
	AS
	BEGIN
		UPDATE [WM.Transfers]
		SET 
			[InvoiceId]=@InvoiceId,[TransferId]=@TransferId,[Purse]=@Purse,[Ammount]=@Ammount,[Description]=@Description,[Date]=@Date
		WHERE [ID]=@ID        
	END
	GO
			IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Transfer_Delete')
	BEGIN
			DROP PROCEDURE [dbo].[Transfer_Delete]
	END
	GO
	-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 07.11.2009 22:52:52
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE Transfer_Delete(@ID [int])
	AS
	BEGIN
		DELETE FROM [WM.Transfers]
		WHERE [ID]=@ID        
	END
	GO
			IF EXISTS(SELECT * FROM sysobjects WHERE xtype='P' AND name='Transfer_GetByID')
	BEGIN
			DROP PROCEDURE [dbo].[Transfer_GetByID]
	END
	GO
	-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 07.11.2009 22:52:52
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE Transfer_GetByID(@ID [int])
	AS
	BEGIN
		SELECT [Ammount],[Date],[Description],[ID],[InvoiceId],[Purse],[TransferId] 
		FROM [WM.Transfers]
		WHERE [ID]=@ID  
	END
	GO

-- ========================FOREIGN KEY GENERATION =====================

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
PRINT N'                 END CREATE PROCEDURES 	               '