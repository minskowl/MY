CREATE TABLE [dbo].[Keywords]
(
[KeywordID] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
[KeywordTypeID] [smallint] NOT NULL,
[CreationDate] [datetime] NOT NULL,
[KeywordStatusID] [tinyint] NOT NULL
) ON [PRIMARY]


