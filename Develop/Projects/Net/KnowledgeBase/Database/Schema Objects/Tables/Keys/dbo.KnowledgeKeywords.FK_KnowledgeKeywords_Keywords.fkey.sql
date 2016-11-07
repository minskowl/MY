ALTER TABLE [dbo].[KnowledgeKeywords] ADD
CONSTRAINT [FK_KnowledgeKeywords_Keywords] FOREIGN KEY ([KeywordID]) REFERENCES [dbo].[Keywords] ([KeywordID])


