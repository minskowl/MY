-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Dmitry Savchin>
-- Create date: <Create Date,,>
-- Description:	<Create Backup Device>
-- =============================================
CREATE PROCEDURE CreateBackupDevice
	@DbName nvarchar(100), 
	@BasePath nvarchar(100)
AS
BEGIN
	DECLARE @Name nvarchar(1000);
	DECLARE @PhName nvarchar(1000);
	DECLARE @Sql nvarchar(1000);
	DECLARE @month int; 

	SELECT @month =DATEPART( month, DATEADD(day,1,GETDATE()));
	SELECT @Name= @DbName + '_DEVICE_' + CAST( @month as nvarchar)
	SELECT @PhName=@BasePath + @Name

	IF NOT EXISTS(select * from sys.backup_devices WHERE [name]=@Name)
	BEGIN
		EXEC master.dbo.sp_addumpdevice  @devtype = N'disk', @logicalname = @Name, @physicalname = @PhName
	END

	SELECT @month = @month - 2
	IF @month<1
	BEGIN
		SELECT @month = @month + 12
	END
	SELECT @Name='BackOfficeEmpty_DEVICE_' + CAST( @month as nvarchar)
	IF EXISTS(select * from sys.backup_devices WHERE [name]=@Name)
	BEGIN
		EXEC master.dbo.sp_dropdevice @logicalname = @Name
	END
END
GO
