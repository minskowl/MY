-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 21.02.2009 18:00:05
	-- Description:	Generated CodeRocket. Select by ForeygnKey FK_FileStorages_FileStorages
	-- =============================================

	CREATE PROCEDURE FileStorage_GetBySettingsID( @SettingsID [tinyint])
	AS
	BEGIN
		SELECT [FileStorageID],[Name],[Path],[SettingsID] 
		FROM [FileStorages]
		WHERE  [SettingsID]=@SettingsID 
	END


