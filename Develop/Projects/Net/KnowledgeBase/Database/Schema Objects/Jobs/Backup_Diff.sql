USE [msdb]
GO


/****** Object:  Job [WAMProdDiff]    Script Date: 07/23/2007 07:50:00 ******/
BEGIN TRANSACTION
DECLARE @ReturnCode INT
DECLARE @Job_Name  nvarchar(100)
SELECT @Job_Name='KnowledgeBase_DIFF'

IF EXISTS(SELECT *  FROM [msdb].[dbo].[sysjobs] WHERE [name]= @Job_Name)
BEGIN 
	EXEC sp_delete_job   @job_name =@Job_Name ;
END

SELECT @ReturnCode = 0
/****** Object:  JobCategory [Database Maintenance]    Script Date: 07/23/2007 07:50:00 ******/
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
		@description=N'Create incremental backup.', 
		@category_name=N'Database Maintenance', 
		@owner_login_name=N'sa', @job_id = @jobId OUTPUT
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback


EXEC msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'Create backup', 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=3, 
		@on_fail_action=2, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'
DECLARE @Name nvarchar(100);
DECLARE @DeviceName nvarchar(100);
DECLARE @Sql nvarchar(1000);
		
SELECT @Name = N''KnowledgeBase_'' + CONVERT( nvarchar ,GETDATE(),112 );
SELECT @DeviceName=N''KnowledgeBase_DEVICE_'' + CAST( DATEPART( month, GETDATE()) as nvarchar)	;
SELECT @Sql	=''BACKUP DATABASE [KnowledgeBase] TO  ['' + @DeviceName + ''] WITH  DIFFERENTIAL , NOFORMAT, NOINIT,  NAME = N'''''' + @Name + '''''', SKIP, REWIND, NOUNLOAD,  STATS = 10'';
EXEC(@Sql);
		', 
		@database_name=N'master', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback		


EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'Schedule', 
		@enabled=1, 
		@freq_type=4, 
		@freq_interval=1, 
		@freq_subday_type=1, 
		@freq_subday_interval=0, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=0, 
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

