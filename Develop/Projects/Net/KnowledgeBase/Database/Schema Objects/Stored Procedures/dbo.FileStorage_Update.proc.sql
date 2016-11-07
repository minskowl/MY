-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 21.02.2009 18:00:05
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE FileStorage_Update(@FileStorageID [smallint],@SettingsID [tinyint],@Name [nvarchar](50),@Path [nvarchar](50))
	AS
	BEGIN
		UPDATE [FileStorages]
		SET 
			[FileStorageID]=@FileStorageID,[SettingsID]=@SettingsID,[Name]=@Name,[Path]=@Path
		WHERE [FileStorageID]=@FileStorageID AND [SettingsID]=@SettingsID        
	END


