CREATE PROCEDURE _Keywords_SetStatus
	(
	@KeywordID int ,
	@KeywordStatusID tinyint
	)

AS
UPDATE [Keywords] 
SET KeywordStatusID=@KeywordStatusID 
WHERE KeywordID=@KeywordID


