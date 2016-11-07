ALTER TABLE [dbo].[KnowledgeKeywords] ADD
CONSTRAINT [FK_KnowledgeKeywords_Knowledges] FOREIGN KEY ([KnowledgeID]) REFERENCES [dbo].[Knowledges] ([KnowledgeID]) ON DELETE CASCADE


