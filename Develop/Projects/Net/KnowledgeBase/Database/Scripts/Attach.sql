-- =============================================
-- Script Template
-- =============================================
USE [master]
GO
CREATE DATABASE [KnowledgeBase] ON 
( FILENAME = N'u:\KnowledgeBase.mdf' ),
( FILENAME = N'u:\KnowledgeBase_log.LDF' )
 FOR ATTACH
GO
if not exists (select name from master.sys.databases sd where name = N'KnowledgeBase' and SUSER_SNAME(sd.owner_sid) = SUSER_SNAME() ) EXEC [KnowledgeBase].dbo.sp_changedbowner @loginame=N'BUILTIN\Administrators', @map=false
GO
USE [KnowledgeBase]
GO
DROP USER [kbUser]
GO
CREATE USER [kbUser] FOR LOGIN [kbUser]
GO
EXEC sp_addrolemember N'db_owner', N'kbUser'
GO
