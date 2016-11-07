DECLARE @dbName nvarchar(200)
SET @dbName='WAM'
DECLARE @spid smallint
DECLARE @sql nvarchar(1000)

 select 
   [Process ID]    = p.spid, 
   [IsSystemProcess] = case when s.is_user_process = 1 then 0 else 1 end, 
    [User]           = p.loginame ,   
   [Database]        = ISNULL(db_name(p.dbid),N'')  , 
   [Status]          = p.status, 
   [Open Transactions] = p.open_tran, 
   [Command]       = p.cmd, 
   [Application]   = p.program_name, 
   [Wait Time]     = p.waittime, 
   [Wait Type]     = case when p.waittype = 0 
                     then N'' 
                     else p.lastwaittype 
                     end, 
   [Wait Resource] = case when p.waittype = 0 
                     then N'' 
                     else p.waitresource 
                     end, 
   [CPU]           = p.cpu, 
   [Physical IO]   = p.physical_io, 
   [Memory Usage]  = p.memusage, 
   [Login Time]    = p.login_time, 
   [Last Batch]    = p.last_batch, 
   [Host]          = p.hostname, 
   [Net Library]   = p.net_library, 
   [Net Address]   = p.net_address, 
   [Blocked By]    = p.blocked, 
   [Blocking]      = 0, 
   [Execution Context ID] = p.ecid
into #locks
from master.dbo.sysprocesses p, master.sys.dm_exec_sessions s 
with (NOLOCK) 
where p.spid = s.session_id  
 order by p.spid 





DECLARE proces CURSOR FOR 
select  [Process ID] from  #locks where [Database]= @dbName and  [Status]='sleeping'

		
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

--DBCC INPUTBUFFER(52)