﻿CREATE PROCEDURE _FileLinks_GetByPublicID	(	@PublicID UNIQUEIDENTIFIER	)
AS
SELECT * FROM [FileLinks] WHERE [PublicID]=@PublicID


