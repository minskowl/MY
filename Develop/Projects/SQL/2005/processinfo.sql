
DECLARE @spid smallint
DECLARE @sql nvarchar(1000)
DECLARE @EventType nvarchar(100)  
DECLARE @Parameters int 
DECLARE @EventInfo nvarchar(4000)  

CREATE TABLE #ProcessInfo
(
	[Process ID] [smallint] NOT NULL,
	[IsSystemProcess] [int] NOT NULL,
	[User] [nchar](128)  NOT NULL,
	[Database] [nvarchar](128)  NOT NULL,
	[Status] [nchar](30)  NOT NULL,
	[Open Transactions] [smallint] NOT NULL,
	[Command] [nchar](16)  NOT NULL,
	[Application] [nchar](128)  NOT NULL,
	[Wait Time] [bigint] NOT NULL,
	[Wait Type] [nvarchar](32)  NOT NULL,
	[Wait Resource] [nvarchar](256)  NOT NULL,
	[CPU] [int] NOT NULL,
	[Physical IO] [bigint] NOT NULL,
	[Memory Usage] [int] NOT NULL,
	[Login Time] [datetime] NOT NULL,
	[Last Batch] [datetime] NOT NULL,
	[Host] [nchar](128)  NOT NULL,
	[Net Library] [nchar](12)  NOT NULL,
	--[Net Address] [nchar](12)  NOT NULL,
	[Blocked By] [smallint] NOT NULL,
	[Blocking] [int] NOT NULL,
	[Execution Context ID] [smallint] NOT NULL,
	EventType nvarchar(100)  NULL,
	Parameters int NULL,
	EventInfo nvarchar(4000) NULL  
) 
create table #temp
(
EventType nvarchar(100),  
Parameters int ,
EventInfo nvarchar(4000)  
)
insert into  #ProcessInfo
(
	[Process ID] ,
	[IsSystemProcess] ,
	[User],
	[Database],
	[Status]  ,
	[Open Transactions] ,
	[Command] ,
	[Application],
	[Wait Time] ,
	[Wait Type],
	[Wait Resource],
	[CPU] ,
	[Physical IO] ,
	[Memory Usage] ,
	[Login Time] ,
	[Last Batch] ,
	[Host],
	[Net Library],
	--[Net Address],
	[Blocked By] ,
	[Blocking] ,
	[Execution Context ID]
)
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
   --[Net Address]   = p.net_address, 
   [Blocked By]    = p.blocked, 
   [Blocking]      = 0, 
   [Execution Context ID] = p.ecid
from master.dbo.sysprocesses p, master.sys.dm_exec_sessions s 
with (NOLOCK) 
where p.spid = s.session_id  
order by p.spid 



DECLARE proces CURSOR FOR 
select  [Process ID] from  #ProcessInfo 

		
OPEN proces
		
FETCH NEXT FROM proces INTO @spid
		
WHILE @@FETCH_STATUS = 0
BEGIN
    select	@sql=N'DBCC INPUTBUFFER(' + convert(varchar, @spid) + ') WITH NO_INFOMSGS '
    INSERT #temp 
    EXEC(@sql)

    select TOP 1 @EventType=EventType ,@Parameters=Parameters  ,@EventInfo=EventInfo 
    from #temp 

    delete from #temp 
    update #ProcessInfo
    set
       EventType=@EventType ,
       Parameters=@Parameters  ,
       EventInfo=@EventInfo 
    where [Process ID] =  @spid
    FETCH NEXT FROM proces INTO @spid
END

CLOSE proces
DEALLOCATE proces

select * from #ProcessInfo

drop TABLE #ProcessInfo
drop table #temp

