SELECT o.NAME FROM sys.objects o 
INNER JOIN sys.columns c ON c.OBJECT_ID = o.OBJECT_ID 
WHERE c.NAME ='ProgramID'