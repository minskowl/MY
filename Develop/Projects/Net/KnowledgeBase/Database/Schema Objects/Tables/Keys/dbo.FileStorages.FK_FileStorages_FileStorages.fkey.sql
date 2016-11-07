ALTER TABLE [dbo].[FileStorages] ADD
CONSTRAINT [FK_FileStorages_FileStorages] FOREIGN KEY ([SettingsID]) REFERENCES [dbo].[Settings] ([SettingsID])


