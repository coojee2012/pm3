USE [dbCenter]
GO

/****** Object:  View [dbo].[V_SGXKZ_YW]    Script Date: 2015/4/20 20:56:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[V_SGXKZ_YW] AS 
select FAppId,Address,PrjAddressDept,ProjectName,PrjItemName,JSDW,a.PrjItemType,ISNULL(b.SGXKZBB, 0) as SGXKZBB,ISNULL(b.FPublish, 0) as FPublish
,'初次办理' as YWLX
from  TC_SGXKZ_PrjInfo a
left join TC_SGXKZ_PrjState b
on a.PrjItemId = b.FPrjItemId
union ALL
select b.FAppId,Address,PrjAddressDept,ProjectName,PrjItemName,JSDW,a.PrjItemType,0 as SGXKZBB, 0 as FPublish
,'延期办理' as YWLX
from  TC_SGXKZ_PrjInfo a,TC_SGXKZ_YQSQ b
where b.FPrjInfoId = a.FId
union ALL
select FAppId,Address,PrjAddressDept,ProjectName,PrjItemName,JSDW,PrjItemType,0 as SGXKZBB, 0 as FPublish
,'变更办理' as YWLX
from  TC_SGXKZ_BGPrjInfo

GO


