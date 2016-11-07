     USE [master]
      GO
      CREATE DATABASE [KnowledgeBase] ON
      ( FILENAME = N'${folder}KnowledgeBase.mdf' ),
      ( FILENAME = N'${folder}KnowledgeBase_log.LDF' )
      FOR ATTACH
      GO
  
      IF EXISTS(select name from master.sys.databases sd where name = N'KnowledgeBase' )
      BEGIN
         PRINT 'DB CREATED'
	  	 if not exists (select name from master.sys.databases sd where name = N'KnowledgeBase' and SUSER_SNAME(sd.owner_sid) = SUSER_SNAME() ) 
			EXEC [KnowledgeBase].dbo.sp_changedbowner @loginame=N'BUILTIN\Administrators', @map=false
		 
		  USE [KnowledgeBase]
		  DROP USER [kbUser]
		  CREATE USER [kbUser] FOR LOGIN [kbUser]
		  EXEC sp_addrolemember N'db_owner', N'kbUser'
		 
		  PRINT 'SUCESS'
		  
	  END
      GO     