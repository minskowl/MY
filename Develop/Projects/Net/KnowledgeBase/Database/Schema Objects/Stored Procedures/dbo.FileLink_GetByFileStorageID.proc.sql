-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 11.11.2008 17:06:48
	-- Description:	Generated CodeRocket. Select by ForeygnKey FK_FileLinks_FileStorages
	-- =============================================

	CREATE PROCEDURE FileLink_GetByFileStorageID( @FileStorageID [smallint])
	AS
	BEGIN
		SELECT [FileLinkID],[FileStorageID],[Path],[PublicID] 
		FROM [FileLinks]
		WHERE  [FileStorageID]=@FileStorageID 
	END


