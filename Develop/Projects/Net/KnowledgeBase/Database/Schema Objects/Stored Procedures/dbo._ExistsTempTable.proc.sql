CREATE PROCEDURE _ExistsTempTable
	(
	@name sysname ,
	@exists int OUTPUT
	)

AS
SELECT @exists=0
SELECT TOP 1  @exists=1 FROM tempdb.sys.objects WHERE [type]='U' AND [name] LIKE @name+'%'


