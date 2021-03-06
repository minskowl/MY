USE [msdb]
GO
/****** Object:  StoredProcedure [dbo].[CreateBackupFull]    Script Date: 03/16/2009 12:13:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Dmitry,Savchin>
-- Create date: <Create Date,,>
-- Description:	<Kill all connection on DataBase>
-- =============================================
CREATE PROCEDURE [dbo].[UnlockDb] 
	@DbName sysname
AS
BEGIN
	
	DECLARE @SQL varchar(max)
	SET @SQL = ''
	
	SELECT @SQL = @SQL + 'Kill ' + Convert(varchar, SPId) + ';'
	FROM MASTER..SysProcesses
	WHERE DBId = DB_ID(@DbName) AND SPId <> @@SPId
	
	-- SELECT @SQL 
	EXEC(@SQL)
END
