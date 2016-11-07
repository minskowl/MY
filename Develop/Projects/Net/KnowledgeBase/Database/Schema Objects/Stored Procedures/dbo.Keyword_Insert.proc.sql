-- =============================================
-- Author:		Savchin Inc. 
-- Create date: 31.10.2008 23:28:15
-- Description:	Generated CodeRocket
-- =============================================
CREATE PROCEDURE Keyword_Insert(@KeywordStatusID [tinyint],@KeywordTypeID [smallint],@Name [nvarchar](50), @KeywordID [int] OUTPUT)
AS
BEGIN
	 INSERT [Keywords]
           ([KeywordStatusID],[KeywordTypeID],[Name])
     VALUES
           (@KeywordStatusID,@KeywordTypeID,@Name)
	SELECT @KeywordID=@@IDENTITY
END


