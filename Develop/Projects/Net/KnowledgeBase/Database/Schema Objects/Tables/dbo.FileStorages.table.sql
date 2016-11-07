CREATE TABLE [dbo].[FileStorages]
(
[FileStorageID] [smallint] NOT NULL,
[SettingsID] [tinyint] NOT NULL,
[Name] [nvarchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL,
[Path] [nvarchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL
) ON [PRIMARY]


