ALTER TABLE [dbo].[Knowledges] ADD
CONSTRAINT [FK_Knowledges_KnowledgeStatuses] FOREIGN KEY ([KnowledgeStatusID]) REFERENCES [dbo].[KnowledgeStatuses] ([KnowledgeStatusID])


