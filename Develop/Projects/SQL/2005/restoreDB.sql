SET NOCOUNT ON
USE master

DECLARE @dbname nvarchar(100)
DECLARE @sql nvarchar(1000)
DECLARE @spid smallint
DECLARE @dbid  smallint
DECLARE @backupfile nvarchar(100)
DECLARE @destinationpath nvarchar(100)
DECLARE @DataLogicalName nvarchar(100)
DECLARE @LogLogicalName nvarchar(100)
DECLARE @Login nvarchar(10)
DECLARE @Password nvarchar(10)
DECLARE @res int

set @backupfile = N'D:\Projects\Deploy\db_v2.0.0.38.bak' 
set @destinationpath  = N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\'

set @dbname=N'WAM'
set @Login = N'wam'
set @Password = N'gjG67$tu4_dg'

SELECT  @dbid =COALESCE(dbid,0) FROM   master..sysdatabases   WHERE name = @dbname


if (@dbid >0)
begin   -- есть ли на не открытые соединения 
	if EXISTS (select spid from master..sysprocesses where dbid=@dbid )
	begin   -- убиваем соединения
		DECLARE proces CURSOR FOR 
		select spid  from master..sysprocesses where dbid=@dbid;
		
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
	
	
	end
end


if (@dbid >0)
begin
	exec sp_dboption  @dbname,'single user','TRUE'
end
  else
begin
  select @sql =N'CREATE DATABASE [' + @dbname + ']
ON  PRIMARY  
( NAME = N''' + @dbname + '_Dat'',
    FILENAME = N'''+ @destinationpath + @dbname + '_Dat.mdf'',
    SIZE = 10,
    MAXSIZE = 50,
    FILEGROWTH = 5 )
LOG ON
( NAME = N''' + @dbname + '_Log'',
    FILENAME = N'''+ @destinationpath + @dbname + '_Log.ldf'',
    SIZE = 5MB,
    MAXSIZE = 25MB,
    FILEGROWTH = 5MB )';
  print @sql;
  EXECUTE (@sql);
  
end

CREATE TABLE #FILELIST
(
      LogicalName	nvarchar(128),
      PhysicalName	nvarchar(260),
      Type	char(1),
      FileGroupName	nvarchar(128),
      Size	numeric(20,0),
      MaxSize	numeric(20,0),
      FileID	bigint,
      CreateLSN	numeric(25,0),
      DropLSN	numeric(25,0) NULL,
      UniqueID	uniqueidentifier,
      ReadOnlyLSN	numeric(25,0) NULL,
      ReadWriteLSN	numeric(25,0) NULL,
      BackupSizeInBytes	bigint,
      SourceBlockSize	int,
      FileGroupID	int,
      LogGroupGUID	uniqueidentifier NULL,
      DifferentialBaseLSN	numeric(25,0) NULL,
      DifferentialBaseGUID	uniqueidentifier,
      IsReadOnly	bit,
      IsPresent	bit
)


select @sql =N'RESTORE FILELISTONLY FROM DISK = ''' + @backupfile  + ''''
print  @sql
INSERT INTO #FILELIST EXEC(@sql)

select @DataLogicalName=LogicalName from  #FILELIST where [Type]='D'
select @LogLogicalName =LogicalName from  #FILELIST where [Type]='L'

--   RESTORE DATABASE [WAM] FROM  DISK = N'D:\Projects\Deploy\db_v2.0.0.36.bak' WITH  FILE = 1,  MOVE N'WAMNew' TO N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\WAM.mdf',  MOVE N'WAMNew_log' TO N'C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\WAMNew_log.ldf',  NOUNLOAD,  REPLACE,  STATS = 10
select @sql =N'
RESTORE DATABASE ' + @dbname + ' from DISK = N''' + @backupfile  + '''
with move N''' + @DataLogicalName + ''' to N'''+ @destinationpath + @dbname + '_Data.MDF'',
move N''' + @LogLogicalName +  ''' to N''' + @destinationpath + @dbname + '_Log.LDF'', replace'

print  @sql
EXEC(@sql)

DROP TABLE #FILELIST
 

if (@dbid >0)
begin
	exec sp_dboption  @dbname,'single user','FALSE'
end

   
BEGIN TRY

      exec sp_addlogin @loginame =  @Login  ,  @passwd =  @Password  ,  @defdb =  @dbname
END TRY
BEGIN CATCH
END CATCH;

print 'add alias'
select @sql =N'USE ' + @dbname
exec(@sql)
exec ..sp_addalias  @loginame = @Login , @name_in_db = @Login

SET NOCOUNT OFF

print 'OK'
