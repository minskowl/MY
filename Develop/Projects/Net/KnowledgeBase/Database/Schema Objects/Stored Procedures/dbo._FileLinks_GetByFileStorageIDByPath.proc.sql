CREATE PROCEDURE _FileLinks_GetByFileStorageIDByPath
	(
	@FileStorageID SMALLINT,
	@Path NVARCHAR(1000) 
)
AS
SELECT * FROM [FileLinks]
WHERE [FileStorageID]=@FileStorageID AND [Path]=@Path


