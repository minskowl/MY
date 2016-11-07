USE [msdb]
GO
/****** Object:  Job [CREATE_BACKUP_DEVICE]    Script Date: 07/23/2007 08:22:09 ******/
BEGIN TRANSACTION
DECLARE @ReturnCode INT
DECLARE @Job_Name  nvarchar(100)
SELECT @Job_Name='KnowledgeBase_CREATE_BACKUP_DEVICE'

IF EXISTS(SELECT *  FROM [msdb].[dbo].[sysjobs] WHERE [name]= @Job_Name)
BEGIN 
	EXEC sp_delete_job   @job_name =@Job_Name ;
END

SELECT @ReturnCode = 0
/****** Object:  JobCategory [Database Maintenance]    Script Date: 07/23/2007 08:22:09 ******/
IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N'Database Maintenance' AND category_class=1)
BEGIN
EXEC @ReturnCode = msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'Database Maintenance'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

END

DECLARE @jobId BINARY(16)
EXEC @ReturnCode =  msdb.dbo.sp_add_job 
		@job_name=@Job_Name, 
		@enabled=1, 
		@notify_level_eventlog=2, 
		@notify_level_email=0, 
		@notify_level_netsend=0, 
		@notify_level_page=0, 
		@delete_level=0, 
		@description=N'CREATE BACKUP DEVICE EVERY MONTH', 
		@category_name=N'Database Maintenance', 
		@owner_login_name=N'sa', @job_id = @jobId OUTPUT
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [Create backup]    Script Date: 07/23/2007 08:22:09 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'Create backup', 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=1, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'
DECLARE @Name nvarchar(100);
DECLARE @PhName nvarchar(100);
DECLARE @Sql nvarchar(1000);
DECLARE @month int; 

SELECT @month =DATEPART( month, DATEADD(day,1,GETDATE()));
SELECT @Name=''KnowledgeBase_DEVICE_'' + CAST( @month as nvarchar)
SELECT @PhName=N''F:\Backups\'' + @Name

IF NOT EXISTS(select * from sys.backup_devices WHERE [name]=@Name)
BEGIN
	EXEC master.dbo.sp_addumpdevice  @devtype = N''disk'', @logicalname = @Name, @physicalname = @PhName
END

SELECT @month = @month - 2
IF @month<1
BEGIN
	SELECT @month = @month + 12
END
SELECT @Name=''KnowledgeBase_DEVICE_'' + CAST( @month as nvarchar)
IF EXISTS(select * from sys.backup_devices WHERE [name]=@Name)
BEGIN
	EXEC master.dbo.sp_dropdevice @logicalname = @Name
END
', 
		@database_name=N'KnowledgeBase', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'Schedule', 
		@enabled=1, 
		@freq_type=32, 
		@freq_interval=8, 
		@freq_subday_type=1, 
		@freq_subday_interval=0, 
		@freq_relative_interval=16, 
		@freq_recurrence_factor=1, 
		@active_start_date=20070110, 
		@active_end_date=99991231, 
		@active_start_time=1000, 
		@active_end_time=235959
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
	IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:
