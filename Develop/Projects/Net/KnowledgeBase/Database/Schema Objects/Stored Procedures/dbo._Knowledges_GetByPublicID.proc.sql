CREATE PROCEDURE _Knowledges_GetByPublicID	(	@PublicID uniqueidentifier		)

AS
SELECT * FROM [Knowledges] WHERE PublicID=@PublicID


