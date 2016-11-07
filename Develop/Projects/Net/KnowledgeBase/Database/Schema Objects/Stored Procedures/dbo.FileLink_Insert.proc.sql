-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 11.11.2008 17:06:48
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE FileLink_Insert(@FileStorageID [smallint],@Path [nvarchar](250),@PublicID [uniqueidentifier], @FileLinkID [int] OUTPUT)
AS
BEGIN
	 INSERT [FileLinks]
           ([FileStorageID],[Path],[PublicID])
     VALUES
           (@FileStorageID,@Path,@PublicID)
	SELECT @FileLinkID=@@IDENTITY
END


