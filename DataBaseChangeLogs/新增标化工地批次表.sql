USE [dbCenter]
GO

/****** Object:  Table [dbo].[TC_BHGD_Batch]    Script Date: 2015/3/24 16:13:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TC_BHGD_Batch](
	[FId] [varchar](36) NOT NULL,
	[FYear] [nvarchar](50) NOT NULL,
	[FBatchNumber] [nvarchar](50) NOT NULL,
	[FState] [char](2) NOT NULL,
 CONSTRAINT [PK_TC_BHGD_Batch] PRIMARY KEY CLUSTERED 
(
	[FId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TC_BHGD_Batch', @level2type=N'COLUMN',@level2name=N'FId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'年份' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TC_BHGD_Batch', @level2type=N'COLUMN',@level2name=N'FYear'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'批号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TC_BHGD_Batch', @level2type=N'COLUMN',@level2name=N'FBatchNumber'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'TC_BHGD_Batch', @level2type=N'COLUMN',@level2name=N'FState'
GO


