CREATE TABLE [dbo].[ActionLog]
(
[Type] [nvarchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL,
[Method] [nvarchar] (50) COLLATE Cyrillic_General_CI_AS NOT NULL,
[SeverityID] [smallint] NOT NULL,
[Date] [datetime] NOT NULL
) ON [PRIMARY]


