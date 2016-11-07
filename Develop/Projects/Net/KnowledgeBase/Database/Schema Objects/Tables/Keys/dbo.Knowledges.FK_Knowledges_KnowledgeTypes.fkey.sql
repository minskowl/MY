ALTER TABLE [dbo].[Knowledges] ADD
CONSTRAINT [FK_Knowledges_KnowledgeTypes] FOREIGN KEY ([KnowledgeTypeID]) REFERENCES [dbo].[KnowledgeTypes] ([KnowledgeTypeID])


