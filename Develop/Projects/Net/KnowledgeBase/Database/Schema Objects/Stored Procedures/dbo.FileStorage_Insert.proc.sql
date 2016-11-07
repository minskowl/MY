-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 21.02.2009 18:00:05
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE FileStorage_Insert(@FileStorageID [smallint],@Name [nvarchar](50),@Path [nvarchar](50),@SettingsID [tinyint])
AS
BEGIN
	 INSERT [FileStorages]
           ([FileStorageID],[Name],[Path],[SettingsID])
     VALUES
           (@FileStorageID,@Name,@Path,@SettingsID)
END


