CREATE TABLE [dbo].[Users]
(
[UserID] [int] NOT NULL IDENTITY(1, 1),
[Login] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
[Password] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
[FirstName] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
[LastName] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
[Email] [nvarchar] (150) COLLATE Latin1_General_CI_AS NOT NULL,
[SecurityQuestion] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
[SecurityAnswer] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL,
[RootPermissionID] [smallint] NULL,
[IsUserAdmin] [bit] NOT NULL,
[IsSystem] [bit] NOT NULL,
[CreationDate] [datetime] NOT NULL
) ON [PRIMARY]


