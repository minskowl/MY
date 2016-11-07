ALTER TABLE [dbo].[UserFiles] ADD CONSTRAINT [IX_UserFiles_UniqueName] UNIQUE NONCLUSTERED  ([UserID], [FileName]) ON [PRIMARY]


