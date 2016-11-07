ALTER TABLE [dbo].[Keywords] ADD
CONSTRAINT [FK_Keywords_KeywordStatuses] FOREIGN KEY ([KeywordStatusID]) REFERENCES [dbo].[KeywordStatuses] ([KeywordStatusID])


