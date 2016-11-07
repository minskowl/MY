-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 11.11.2008 17:06:48
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE FileLink_Delete(@FileLinkID [int])
	AS
	BEGIN
		DELETE FROM [FileLinks]
		WHERE [FileLinkID]=@FileLinkID        
	END


