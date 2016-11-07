-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 21.02.2009 18:00:05
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE FileStorage_GetByID(@FileStorageID [smallint],@SettingsID [tinyint])
	AS
	BEGIN
		SELECT [FileStorageID],[Name],[Path],[SettingsID] 
		FROM [FileStorages]
		WHERE [FileStorageID]=@FileStorageID AND [SettingsID]=@SettingsID  
	END


