ALTER TABLE [dbo].[FileIncludes] ADD
CONSTRAINT [FK_FileIncludes_FileIncludes] FOREIGN KEY ([KnowledgeID]) REFERENCES [dbo].[Knowledges] ([KnowledgeID])


