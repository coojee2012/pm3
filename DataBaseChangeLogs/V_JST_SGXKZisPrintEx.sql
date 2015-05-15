USE [JKCWFDB_WORK_NJS]
GO

/****** Object:  View [dbo].[V_JST_SGXKZisPrintEx]    Script Date: 2015/5/15 15:58:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





ALTER view [dbo].[V_JST_SGXKZisPrintEx]
AS
select fq.FQBM AS P_Code, SGXK.XKZBH AS gcbh,fq.name AS gcmc, XM.XMDZ AS gcjsdz, XM.XMJSDW AS jsdw, SGXK.JSGM, SGXK.HTJG, '' AS SJDW, 
                      ZCBDW.DWMC AS sgzbdw,'' AS jldw, SGXK.HTKGRQ AS kgsj, SGXK.HTJGRQ AS jgsj, convert(nvarchar(4000),SGXK.BZ) bz, SGXK.SBSJ AS sqsj, SGXK.FZDW AS fzjg, 
                     cast(convert(varchar(10),ISNULL(SGXK.FZRQ, GETDATE()),121) as datetime)AS FZRQ,
       substring(('01'+cast(SGXK.SLDW as varchar)+'000000'),1,8) as SLDW
	   ,null kcdw,null kcfzr,null sgfzr,null zjlg,null jsdwxz,null jsdwdz
from ZY_XMFQInfo as fq
join ZY_XMInfo as xm on fq.xmbm=xm.xmbm
join ZY_SGXKZInfo as sgxk on sgxk.fqbm=fq.fqbm
--left join zy_jldw as jl on jl.fqbm=fq.fqbm
--left join zy_sjdw as sj on sj.fqbm=fq.fqbm
left join ZY_SGZCBDWInfo as zcbdw on zcbdw.fqbm=fq.fqbm
WHERE    FQ.FQBM IN 
(
    SELECT B.FQBM   FROM  YW_YWInfo AS A,YW_XMFQINFO AS B
    WHERE   A.YWBM=B.YWBM AND A.DZZT = 2
)
union
select fprjitemid,ProjectNumber,Prjitemname,address,jsdw,constrscale,price,sjdw,sgdw,jldw,startdate,enddate,remark,sqsj,fzjg,fzrq,PrjAddressDept
       ,kcdw,kcfzr,sgfzr,zjlg,jsdwxz,jsdwdz
  from dbcenter.dbo.GD_SGXKZ_ZSPrint a
 where exists(select 1 from dbcenter.dbo.TC_SGXKZ_PrjInfo b where a.FAppId = b.FAppId and b.DZZT = 2)


 go

 

ALTER view [dbo].[V_JST_SGXKZnotPrintEx]
as 
SELECT FQ.FQBM AS P_Code, SGXK.XKZBH AS gcbh, FQ.Name AS gcmc, 
      XM.XMDZ AS gcjsdz, XM.XMJSDW AS jsdw, CONVERT(varchar(50), SGXK.JSGM) 
      jsgm, CONVERT(varchar(50), SGXK.HTJG) htjg, '' AS SJDW, 
      ZCBDW.DWMC AS sgzbdw, '' AS jldw, SGXK.HTKGRQ AS kgsj, 
      SGXK.HTJGRQ AS jgsj, CONVERT(nvarchar(4000), SGXK.BZ) bz, SGXK.SBSJ AS sqsj, 
      SGXK.FZDW AS fzjg, CAST(CONVERT(varchar(10), ISNULL(SGXK.FZRQ, GETDATE()), 
      121) AS datetime) AS FZRQ, SUBSTRING('01' + CAST(SGXK.SLDW AS varchar) 
      + '000000', 1, 8) AS SLDW, NULL kcdw, NULL kcfzr, NULL sgfzr, NULL zjlg, null as sjfzr,NULL 
      jsdwxz, NULL jsdwdz, SGXK.XKZBH AS SGXKZBH, 1 as isold,NULL AS ZYCB,NULL AS LWCB,null as ZJLGCS,null as ZYJLGCS,
      NULL AS XMFZRzyzg
FROM dbo.ZY_XMInfo AS XM LEFT OUTER JOIN
      dbo.ZY_XMFQInfo AS FQ ON XM.XMBM = FQ.XMBM LEFT OUTER JOIN
      dbo.ZY_SGXKZInfo AS SGXK ON FQ.FQBM = SGXK.FQBM LEFT OUTER JOIN
      dbo.ZY_SGZCBDWInfo AS ZCBDW ON ZCBDW.FQBM = SGXK.FQBM
WHERE (FQ.FQBM IN
          (SELECT B.FQBM
         FROM dbo.YW_YWInfo AS A INNER JOIN
               dbo.YW_XMFQInfo AS B ON A.YWBM = B.YWBM
         WHERE (A.DZZT = 1)))
UNION
SELECT fprjitemid, SGXKZBH as ProjectNumber, Prjitemname, address, jsdw, constrscale, price, sjdw, 
      sgdw, jldw, startdate, enddate, remark, sqsj, fzjg, fzrq, PrjAddressDept, kcdw, kcfzr, 
      sgfzr, zjlg, sjfzr,jsdwxz, jsdwdz, SGXKZBH, 0 as isold,'','','','',''
  from dbcenter.dbo.GD_SGXKZ_ZSPrint a
 where exists(select 1 from dbcenter.dbo.TC_SGXKZ_PrjInfo b where a.FAppId = b.FAppId and b.DZZT = 1)
go




GO


