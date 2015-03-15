USE [dbCenter]
GO

/****** Object:  Table [dbo].[TC_Prj_XCTKJL_File]    Script Date: 2015/3/11 22:47:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TC_Prj_XCTKJL_File](
	[FId] [varchar](36) NOT NULL,
	[FAppId] [varchar](36) NULL,
	[FLinkId] [varchar](36) NULL,
	[FFileName] [varchar](200) NULL,
	[FNum] [int] NULL,
	[FReportor] [varchar](50) NULL,
	[FFilePath] [varchar](300) NULL,
 CONSTRAINT [PK_TC_Prj_XCTKJL_File] PRIMARY KEY CLUSTERED 
(
	[FId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


