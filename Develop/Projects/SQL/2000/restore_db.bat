echo off
rem Copy to local
copy  \\orion\mrs_backup\mrs_backup.dmp  c:\mrs_backup.dmp /Y

rem restore db
osql.exe -U sa -P qwertyuiop -S (local) -d master -i restore_db.sql -o restore_db.log -n

rem Install error codes
osql.exe -U sa -P qwertyuiop -S (local) -d master -i \\orion\mrs_backup\errors.sql -o errors.log -n

del c:\mrs_backup.dmp

echo on
osql.exe -U sa -P qwertyuiop -S (local) -d master -Q "SELECT  CAST([SettingName] as varchar(10)) , CAST([SettingValue] as int) FROM [MRS_UNIT].[dbo].[GlobalSettings] WHERE [SettingId]=1" 

pause