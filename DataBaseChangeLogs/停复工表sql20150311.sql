USE [dbCenter]
GO

/****** Object:  Table [dbo].[TC_SGXKZ_TFG]    Script Date: 2015/4/12 22:14:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TC_SGXKZ_TFG](
	[FID] [varchar](36) NOT NULL,
	[FAppId] [varchar](36) NOT NULL,
	[FType] [int] NOT NULL,
	[FTFGRQ] [date] NOT NULL,
	[FYJSJFGRQ] [date] NULL,
	[FCLZT] [int] NOT NULL,
	[FYY] [varchar](200) NULL,
	[FSHR] [varchar](50) NULL,
	[FSHDW] [varchar](50) NULL,
	[FSHRQ] [date] NULL,
 CONSTRAINT [PK_TC_SGXKZ_TFG] PRIMARY KEY CLUSTERED 
(
	[FID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

