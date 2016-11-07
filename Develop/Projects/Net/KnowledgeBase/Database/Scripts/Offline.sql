USE master;

DECLARE @dbName nvarchar(200)
SET @dbName='KnowledgeBase'
DECLARE @spid smallint
DECLARE @sql nvarchar(1000)

 select 
   [Process ID]    = p.spid, 
    [Database]        = ISNULL(db_name(p.dbid),N'')  
 
into #locks
from master.dbo.sysprocesses p, master.sys.dm_exec_sessions s 
with (NOLOCK) 
where p.spid = s.session_id  
 order by p.spid 





DECLARE proces CURSOR FOR 
select  [Process ID] from  #locks where [Database]= @dbName 

		
OPEN proces
		
FETCH NEXT FROM proces INTO @spid
		
WHILE @@FETCH_STATUS = 0
BEGIN
			select	@sql=N'kill ' + +convert(varchar, @spid)
			print @sql
			EXEC(@sql)
			FETCH NEXT FROM proces INTO @spid
END
		
CLOSE proces
DEALLOCATE proces



drop table  #locks
GO
ALTER DATABASE [KnowledgeBase] SET OFFLINE 