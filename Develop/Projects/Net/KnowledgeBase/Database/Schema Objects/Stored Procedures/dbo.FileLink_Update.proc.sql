-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 11.11.2008 17:06:48
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE FileLink_Update(@FileLinkID [int],@FileStorageID [smallint],@Path [nvarchar](250),@PublicID [uniqueidentifier])
	AS
	BEGIN
		UPDATE [FileLinks]
		SET 
			[FileStorageID]=@FileStorageID,[Path]=@Path,[PublicID]=@PublicID
		WHERE [FileLinkID]=@FileLinkID        
	END


