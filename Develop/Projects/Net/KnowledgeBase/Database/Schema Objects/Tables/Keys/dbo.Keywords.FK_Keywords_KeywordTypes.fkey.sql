ALTER TABLE [dbo].[Keywords] ADD
CONSTRAINT [FK_Keywords_KeywordTypes] FOREIGN KEY ([KeywordTypeID]) REFERENCES [dbo].[KeywordTypes] ([KeywordTypeID])


