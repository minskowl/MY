CREATE TABLE [dbo].[UserFiles]
(
[UserFileID] [int] NOT NULL IDENTITY(1, 1),
[UserID] [int] NOT NULL,
[FileName] [nvarchar] (100) COLLATE Cyrillic_General_CI_AS NOT NULL,
[Data] [image] NULL,
[Size] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


