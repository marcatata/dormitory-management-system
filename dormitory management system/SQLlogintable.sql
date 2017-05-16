CREATE TABLE [dbo].[RegLogUser](
    [LoginID] [int] IDENTITY(1,1) NOT NULL,
    [UserName] [varchar](50) NULL,
    [Password] [varchar](50) NULL,
    [LogType] [bit] NULL)