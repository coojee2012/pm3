USE [dbCenter]
GO

/****** Object:  Table [dbo].[TC_AJBA_QZSB_CZRY]    Script Date: 2015/3/14 15:06:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TC_AJBA_QZSB_CZRY](
	[ID] [nvarchar](50) NOT NULL,
	[FAppID] [nvarchar](50) NULL,
	[FPrjItemID] [nvarchar](50) NULL,
	[FLinkID] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[Trades] [nvarchar](50) NULL,
	[CZZH] [nvarchar](50) NULL,
 CONSTRAINT [PK_TC_AJBA_QZSB_CZZY] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

