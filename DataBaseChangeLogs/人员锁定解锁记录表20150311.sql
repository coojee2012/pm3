USE [dbCenter]
GO

/****** Object:  Table [dbo].[TC_PrjItem_Emp_UnLock]    Script Date: 2015/4/12 22:16:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TC_PrjItem_Emp_UnLock](
	[FId] [varchar](36) NOT NULL,
	[FLinkId] [varchar](36) NOT NULL,
	[FJSR] [varchar](20) NULL,
	[FJSRID] [varchar](36) NULL,
	[FJSTime] [datetime] NULL,
	[FJSYY] [varchar](200) NULL,
 CONSTRAINT [PK_TC_PrjItem_Emp_UnLock] PRIMARY KEY CLUSTERED 
(
	[FId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

