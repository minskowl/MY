USE [msdb]
GO
/****** Object:  StoredProcedure [dbo].[CreateBackupDiff]    Script Date: 03/16/2009 12:15:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[CreateBackupDiff] 
	@DbName sysname
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	DECLARE @Name nvarchar(100);
	DECLARE @Sql nvarchar(4000);
	SELECT @Name = @DbName+N'_' + CONVERT( nvarchar ,GETDATE(),112 );
	
	SELECT @Sql	='BACKUP DATABASE ['+@DbName+'] TO  [' + dbo.GetDeviceNameCurrent(@DbName) 
	+ '] WITH  DIFFERENTIAL , NOFORMAT, NOINIT,  NAME = N''' + @Name + ''', SKIP, REWIND, NOUNLOAD,  STATS = 10';
	EXEC(@Sql);
		
END
