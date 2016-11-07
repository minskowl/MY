CREATE FUNCTION dbo.GetUniuqueFileIncludeName
	(
	@KnowledgeID int = 5,
	@FileName NVARCHAR(200)
	)
RETURNS NVARCHAR(200)
AS
	BEGIN
		DECLARE @RES NVARCHAR(200)
		DECLARE @CNT INT
		SET @RES=@FileName;
		SET @CNT=0;
		
		WHILE EXISTS(SELECT * FROM dbo.FileIncludes WHERE KnowledgeID=@KnowledgeID AND FileName=@RES)
		BEGIN
		  SELECT @CNT=@CNT+1
		  SELECT @RES=@FileName + CAST( @CNT AS NVARCHAR);
		END
		
		RETURN @RES;
	END


