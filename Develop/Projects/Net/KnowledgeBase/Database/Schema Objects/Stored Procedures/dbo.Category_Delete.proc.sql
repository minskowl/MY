-- =============================================
	-- Author:		Savchin Inc. 
	-- Create date: 20.10.2008 23:15:46
	-- Description:	Generated CodeRocket
	-- =============================================
	CREATE PROCEDURE Category_Delete(@CategoryID [int])
	AS
	BEGIN
		DELETE FROM [Categories]
		WHERE [CategoryID]=@CategoryID        
	END


