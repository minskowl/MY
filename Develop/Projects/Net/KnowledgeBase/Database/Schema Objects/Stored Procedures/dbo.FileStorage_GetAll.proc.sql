-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 21.02.2009 18:00:05
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE FileStorage_GetAll
AS
BEGIN
	SELECT [FileStorageID],[Name],[Path],[SettingsID] 
	FROM [FileStorages]
END


