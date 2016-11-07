-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 11.11.2008 17:06:48
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE FileLink_GetByID(@FileLinkID [int])
	AS
	BEGIN
		SELECT [FileLinkID],[FileStorageID],[Path],[PublicID] 
		FROM [FileLinks]
		WHERE [FileLinkID]=@FileLinkID  
	END


