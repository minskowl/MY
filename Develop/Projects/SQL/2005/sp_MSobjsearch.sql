set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

/*
** Copyright Microsoft, Inc. 1994 - 2000
** All Rights Reserved.
*/
-- =============================================
-- sp_MSobjsearch (for 8.0 servers)
--
-- PARAMETERS
-- =============================================
-- @searchkey       default NULL
-- @dbname          default current db = db_name(), valid DB name
--                     or * (ALL)
-- @objecttype      default 1 (user table), can be valid objtype
--                     or 4096 (ALL), see remarks
-- @hitlimit		default 100 rows, 0 is all results
-- @casesensitive   default 0, only valid when server is case sensitive
-- @status          default 0 = no status, 1 = send percentage
--                     progress status back based on database/step
-- @extpropname	    default NULL
-- @extpropvalue    default NULL
--
-- REMARKS
-- =============================================
-- @objecttype	
--		user table       = 1	from @dbname..sysobjects
--		system table     = 2	from @dbname..sysobjects
--		view             = 4	from @dbname..sysobjects
--		sp               = 8	from @dbname..sysobjects
--		rf(repl sp)      = 16	from @dbname..sysobjects
--		xp               = 32	from @dbname..sysobjects
-- 		trigger          = 64	from @dbname..sysobjects
--		UDF              = 128  from @dbname..sysobjects
--     		DRI Constraints  = 256  from @dbname..sysobjects
-- 		log              = 512  from @dbname..sysobjects
--		column           = 1024 from @dbname..syscolumns
--		index            = 2048	from @dbname..sysindexes
--		all              = 4096	
-- =============================================
--
-- RETURN VALUES
-- =============================================
-- 0 = success
-- 1 = parameter error
-- 2 = resultset truncated
-- =============================================
ALTER procedure [dbo].[sp_MSobjsearch] --'t%', '*', 4096
@searchkey as nvarchar(4000) = NULL,
@dbname as sysname = NULL,
@objecttype as int = 1,
@hitlimit as int = 100,
@casesensitive as tinyint = 0,
@status as tinyint = 0,
@extpropname as sysname = NULL,
@extpropvalue as nvarchar(4000) = NULL

as

-- =============================================
-- create temp result set
-- =============================================
create table #objsearch(
dbname	sysname COLLATE database_default not null,
owner	sysname COLLATE database_default not null,
objname	sysname	COLLATE database_default not null,
objtype	nvarchar(25) COLLATE database_default not null,
objtab	sysname COLLATE database_default null,
extpropname sysname COLLATE database_default null,
extpropvalue sql_variant null)

-- =============================================
-- create covering index
-- =============================================
create index #ind_objsearch on #objsearch(dbname, owner, objname, objtype)

-- =============================================
-- required connection settings setting
-- =============================================
set nocount on

-- =============================================
-- declare variables
-- =============================================
declare @cnt integer
declare @stmt nvarchar(4000)
declare @strtype nvarchar(100)
declare @dbcount integer
declare @i integer
declare @typepointer as integer
declare @objtype as integer
declare @quotedbname as nvarchar(256)
declare @quotedbname2 as nvarchar(256)
declare @beginupper as nvarchar(6)
declare @endupper as nvarchar(1)

declare @extprop as bit

-- =============================================
-- initialize extended property search variables
-- =============================================
if (@extpropname is NULL) and (@extpropvalue is NULL)
	select @extprop = 0
else
	select @extprop = 1

if (@extpropname is NULL) and (@extpropvalue is not NULL) select @extpropname = '%'

if (@extpropname is not NULL) and (@extpropvalue is NULL) select @extpropvalue = '%'

-- =============================================
-- initialize variables
-- =============================================
select @cnt = 0, @stmt = '', @strtype = '''U''', @dbcount = 0, @i = 0, @beginupper = '', @endupper = ''

select @searchkey = quotename(@searchkey, '''')
select @extpropname = quotename(@extpropname, '''')
select @extpropvalue = quotename(@extpropvalue, '''')

if @objtype = 4095 select @objtype = 4096

-- =============================================
-- search key is a mandatory parameter
-- =============================================
if (@searchkey is null)
begin
	raiserror ('No search key provided, search procedure aborted', 16, 1, @dbname)
	return 1
end

-- =============================================
-- default database is the current database from which executed
-- =============================================
if (@dbname is null) select @dbname = db_name()
-- =============================================
-- verify if database name exists
-- =============================================
if (@dbname <> '*')
begin
	if not exists (select * from master..sysdatabases where name = @dbname and has_dbaccess(@dbname) = 1)
	begin
		raiserror ('Database %s does not exist, search procedure aborted', 16, 1, @dbname)
		return 1
	end
end

-- =============================================
-- verify case sensitivety if needed
-- =============================================
-- we need to modify @searchkey to include all upper/lower letters in the string
if (@casesensitive = 0)
begin
    select @searchkey = upper(@searchkey)
    select @extpropname = upper(@extpropname)
    select @extpropvalue = upper(@extpropvalue)

    select @beginupper = 'upper('
    select @endupper = ')'
end

-- =============================================
-- indicate progress ?
-- =============================================
if (@status = 1)
	select @dbcount = (select count(*) from master.dbo.sysdatabases where has_dbaccess(name) = 1)

-- =============================================
-- if @dbname = '*'
-- =============================================
if (@dbname = '*')
	select @stmt = 'declare dbcursor cursor forward_only read_only for select name from master.dbo.sysdatabases where has_dbaccess(name) = 1 order by name'
else
	begin
		select @dbname = quotename(@dbname, '''')
		select @stmt = 'declare dbcursor cursor forward_only read_only for select name from master.dbo.sysdatabases where has_dbaccess(name) = 1 and name = N'+ @dbname + ' order by name'
	end

exec (@stmt)
if @@error <> 0
	begin
		raiserror ('Error creating cursor for databases, search procedure aborted', 16, 1, @dbname)
		return 1
	end

open dbcursor
-- ====================================
-- loop for each database in dbcursor
-- ====================================
fetch next from dbcursor into @dbname
while (@@fetch_status <> -1)
begin
	select @quotedbname = quotename(@dbname)
	select @quotedbname2 = quotename(@dbname, '''')
	select @typepointer = 1

	-- =============================================
	-- loop to match @objecttype with each typepointer (1, 2, 4, ...)
	-- =============================================
	while (@objecttype >= @typepointer)
	begin
		if (@@fetch_status <> -2)
		begin
		
			select @objtype = @objecttype&@typepointer
			-- =============================================
			-- query sysobjects
			-- =============================================
			if (@objtype in (1, 2, 4, 8, 16, 32, 64, 128, 256, 512, 4096))
			begin
				-- =============================================
				-- set @strobj to indicate search type
				-- =============================================
				if (@objtype = 1) select @strtype = '''U'''
				else if (@objtype = 2) select @strtype = '''S'''
				else if (@objtype = 4) select @strtype = '''V'''
				else if (@objtype = 8) select @strtype = '''P'''
				else if (@objtype = 16) select @strtype = '''RF'''
				else if (@objtype = 32) select @strtype = '''X'''
				else if (@objtype = 64) select @strtype = '''TR'''
				else if (@objtype = 128) select @strtype = '''TF'',''IF'',''FN'''
				else if (@objtype = 256) select @strtype = '''C'',''D'',''F'',''PK'',''UQ'''
				else if (@objtype = 512) select @strtype = '''L'''
	
				if (@objtype = 4096)
				    if (@extprop = 0)
					select @stmt = 'use ' + @quotedbname + ' insert into #objsearch select dbname = N' + @quotedbname2 + ', user_name(o.uid), o.name, o.xtype, object_name(o.parent_obj), NULL, NULL from ' + @quotedbname + '.dbo.sysobjects o where ' + @beginupper + 'o.name' + @endupper +' like N' + @searchkey
				    else
					select @stmt = 'use ' + @quotedbname + ' insert into #objsearch select dbname = N' + @quotedbname2 + ', user_name(o.uid), o.name, o.xtype, object_name(o.parent_obj), p.name, p.value from ' + @quotedbname + '.dbo.sysobjects o, '+ @quotedbname + '.dbo.sysproperties p where o.id = p.id and ' + @beginupper + 'o.name' + @endupper +' like N' + @searchkey + ' and ' + @beginupper + 'p.name' + @endupper +' like N' + @extpropname + ' and ' + @beginupper + 'cast(ISNULL(p.value, N'''') as nvarchar(4000))' + @endupper +' like N' + @extpropvalue + ' and p.type = 3'
					
				else 	
				    if (@extprop = 0)
					select @stmt = 'use ' + @quotedbname + ' insert into #objsearch select dbname = N' + @quotedbname2 + ', user_name(o.uid), o.name, o.xtype, object_name(o.parent_obj), NULL, NULL from ' + @quotedbname + '.dbo.sysobjects o where o.xtype in (' + @strtype + ') and ' + @beginupper + 'o.name' + @endupper +' like N' + @searchkey
				    else	
					select @stmt = 'use ' + @quotedbname + ' insert into #objsearch select dbname = N' + @quotedbname2 + ', user_name(o.uid), o.name, o.xtype, object_name(o.parent_obj), p.name, p.value from ' + @quotedbname + '.dbo.sysobjects o, '+ @quotedbname + '.dbo.sysproperties p where o.id = p.id and o.xtype in (' + @strtype + ') and ' + @beginupper + 'o.name' + @endupper +' like N' + @searchkey + ' and ' + @beginupper + 'p.name' + @endupper +' like N' + @extpropname + ' and ' + @beginupper + 'cast(ISNULL(p.value, N'''') as nvarchar(4000))' + @endupper +' like N' + @extpropvalue + ' and p.type = 3'
				
				exec (@stmt)

				if @@error <> 0
					begin
						raiserror ('Error inserting objects from %s into #objsearch, search procedure aborted', 16, 1, @dbname)
						return 1
					end

				select @cnt = @cnt + @@rowcount
			end
			
			if (@hitlimit > 0 and @cnt >= @hitlimit) goto returnresults
	
			-- =============================================
			-- query syscolumns
			-- =============================================
			if (@objtype in (1024, 4096))
			begin
				-- because paremeters for store proc and UDF are also stored in syscolumns table, the following query filter them out(by checking name start with '@' and name = '')
				if (@extprop = 0)
				   select @stmt = 'use ' + @quotedbname + ' insert into #objsearch select dbname = N' + @quotedbname2 + ', user_name(o.uid), c.name, ''COL'', o.name, NULL, NULL from ' + @quotedbname + '.dbo.syscolumns c, ' + @quotedbname + '.dbo.sysobjects o where c.id = o.id and ' + @beginupper + 'c.name' + @endupper +' like N' + @searchkey + ' and c.name not like ''@%''' + ' and c.name <> '''''
				else
				   select @stmt = 'use ' + @quotedbname + ' insert into #objsearch select dbname = N' + @quotedbname2 + ', user_name(o.uid), c.name, ''COL'', o.name, p.name, p.value from ' + @quotedbname + '.dbo.syscolumns c, ' + @quotedbname + '.dbo.sysobjects o, '+ @quotedbname + '.dbo.sysproperties p where c.id = o.id and o.id = p.id and c.colid = p.smallid and ' + @beginupper + 'c.name' + @endupper +' like N' + @searchkey + ' and c.name not like ''@%''' + ' and c.name <> ''''' + ' and ' + @beginupper + 'p.name' + @endupper +' like N' + @extpropname + ' and ' + @beginupper + 'cast(ISNULL(p.value, N'''') as nvarchar(4000))' + @endupper +' like N' + @extpropvalue + ' and p.type = 4'	

				exec  (@stmt)
				if @@error <> 0
					begin
						raiserror ('Error inserting objects from %s into #objsearch, search procedure aborted', 16, 1, @dbname)
						return 1
					end

				select @cnt = @cnt + @@rowcount
			end

			if (@hitlimit > 0 and @cnt >= @hitlimit) goto returnresults
	
			-- =============================================
			-- query sysindexes
			-- =============================================
			if (@objtype in (2048, 4096))
			begin
				-- because statistics and 'fake'index are also stored in sysindexes table, the following query filter them out (by checking status&0x0040 - statistics, and indid not in (0, 255)- fake index)
				if (@extprop = 0)
				   select @stmt = 'use ' + @quotedbname + ' insert into #objsearch select dbname = N' + @quotedbname2 + ', user_name(o.uid), i.name, ''I'', o.name, NULL, NULL from ' + @quotedbname + '.dbo.sysindexes i, ' + @quotedbname + '.dbo.sysobjects o where i.id = o.id and (i.indid not in (0, 255)) and (i.status&(32 + 64 + 2048 + 4096) = 0) and ' + @beginupper + 'i.name' + @endupper +' like N' + @searchkey
				else
				   select @stmt = 'use ' + @quotedbname + ' insert into #objsearch select dbname = N' + @quotedbname2 + ', user_name(o.uid), i.name, ''I'', o.name, p.name, p.value from ' + @quotedbname + '.dbo.sysindexes i, ' + @quotedbname + '.dbo.sysobjects o, '+ @quotedbname + '.dbo.sysproperties p where i.id = o.id and i.id = p.id and i.indid = p.smallid and (i.indid not in (0, 255)) and (i.status&(32 + 64 + 2048 + 4096) = 0) and ' + @beginupper + 'i.name' + @endupper +' like N' + @searchkey  + ' and ' + @beginupper + 'p.name' + @endupper +' like N' + @extpropname + ' and ' + @beginupper + 'cast(ISNULL(p.value, N'''') as nvarchar(4000))' + @endupper +' like N' + @extpropvalue + ' and p.type = 6'	
				
				exec  (@stmt)
				if @@error <> 0
					begin
						raiserror ('Error inserting objects from %s into #objsearch, search procedure aborted', 16, 1, @dbname)
						return 1
					end

				select @cnt = @cnt + @@rowcount
			end

			if (@hitlimit > 0 and @cnt >= @hitlimit) goto returnresults
	
			-- =============================================
			-- move on to match next datatype
			-- =============================================
			select @typepointer = @typepointer*2

		end -- if (@@fetch_status <> -2)
		
	end -- while (@objecttype >= @typepointer)

	fetch next from dbcursor into @dbname
	
	-- =============================================
	-- report progress as (step X of Y)
	-- =============================================
	if (@status = 1)
	begin
		select @i = @i + 1
		select 'step' = @i, 'steps' = @dbcount
	end
end

-- =============================================
-- return result set
-- =============================================
returnresults:

deallocate dbcursor

-- =============================================
-- enforce hitlimit
-- =============================================
set rowcount @hitlimit

if (@extprop = 0)
	select dbname, owner, objname, objtype, ISNULL(objtab, '') as objtab from #objsearch order by dbname, owner, objname, objtype
else
	select dbname, owner, objname, objtype, ISNULL(objtab, '') as objtab, extpropname, extpropvalue from #objsearch order by dbname, owner, objname, objtype

set rowcount 0

-- =============================================
-- return status
-- =============================================
if (@cnt > @hitlimit)
	return 2 -- resultset truncated
else
	return 0 -- resultset within limits
-- =============================================
-- end sp_MSobjsearch
-- =============================================


