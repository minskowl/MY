CREATE TABLE [dbo].[FileLinks]
(
[FileLinkID] [int] NOT NULL IDENTITY(1, 1),
[FileStorageID] [smallint] NOT NULL,
[Path] [nvarchar] (250) COLLATE Cyrillic_General_CI_AS NOT NULL,
[PublicID] [uniqueidentifier] NOT NULL
) ON [PRIMARY]


