USE [msdb]
GO
/****** Object:  StoredProcedure [dbo].[CreateBackupFull]    Script Date: 03/16/2009 12:13:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[CreateBackupFull] 
	@DbName sysname
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Name nvarchar(200);
	DECLARE @Sql nvarchar(2000);
			
	SELECT @Name = @DbName+N'_' + CONVERT( nvarchar ,GETDATE(),112 );
	SELECT @Sql	='BACKUP DATABASE ['+ @DbName +'] TO  [' + dbo.GetDeviceNameCurrent(@DbName) +
	 '] WITH NOFORMAT, NOINIT,   NAME = N''' + @Name + ''', SKIP, REWIND, NOUNLOAD,  STATS = 10';
	EXEC(@Sql);	
END
