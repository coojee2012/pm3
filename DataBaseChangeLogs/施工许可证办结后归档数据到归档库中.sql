USE [dbCenter]
GO
/****** Object:  StoredProcedure [dbo].[SP_GD_SGXKZ]    Script Date: 2015/5/16 14:50:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[SP_GD_SGXKZ]
(
	@fprjitemid varchar(50), --工程编号
	@fappid varchar(50)      --办结业务编号
)
AS
--设置打证状态为待打证
UPDATE TC_SGXKZ_PrjInfo  SET DZZT=1  where FAppid = @fappid;
--存在则更新归档库
if exists  (select  1 from  GD_TC_SGXKZ_PrjInfo where FPrjItemId = @fprjitemid)
BEGIN
   --更新工程简要说明表
   UPDATE  GD_TC_SGXKZ_PrjInfo
           SET FAppId = B.FAppId
           ,[FPrjItemId] = B.FPrjItemId
           ,[JSDW] = B.JSDW
           ,[JProvince] = B.JProvince
           ,[JCity] = B.JCity
           ,[JCounty] = B.JCounty
           ,[JSDWAddressDept] =  B.[JSDWAddressDept]
           ,[JSDWDZ]          =  B.[JSDWDZ]         
           ,[JSDWXZ]          =  B.[JSDWXZ]         
           ,[FDDBR]           =  B.[FDDBR]          
           ,[FRDH]            =  B.[FRDH]           
           ,[LZR]             =  B.[LZR]            
           ,[LXDH]            =  B.[LXDH]           
           ,[JSFZR]           =  B.[JSFZR]          
           ,[JSFZRZC]         =  B.[JSFZRZC]        
           ,[JSFZRDH]         =  B.[JSFZRDH]        
           ,[PrjId]           =  B.[PrjId]          
           ,[PrjItemId]       =  B.[PrjItemId]      
           ,[ProjectName]     =  B.[ProjectName]    
           ,[PrjItemName]     =  B.[PrjItemName]    
           ,[PrjItemType]     =  B.[PrjItemType]    
           ,[ProjectTime]     =  B.[ProjectTime]    
           ,[ReportTime]      =  B.[ReportTime]     
           ,[PProvince]       =  B.[PProvince]      
           ,[PCity]           =  B.[PCity]          
           ,[PCounty]         =  B.[PCounty]        
           ,[PrjAddressDept]  =  B.[PrjAddressDept] 
           ,[Address]         =  B.[Address]        
           ,[ConstrScale]     =  B.[ConstrScale]    
           ,[ConstrType]      =  B.[ConstrType]     
           ,[Price]           =  B.[Price]          
           ,[Currency]        =  B.[Currency]       
           ,[StartDate]       =  B.[StartDate]      
           ,[EndDate]         =  B.[EndDate]        
           ,[FResult]         =  B.[FResult]        
           ,[Remark]          =  B.[Remark]         
           ,[ProjectFile]     =  B.[ProjectFile]    
           ,[ProjectNo]       =  B.[ProjectNo]      
           ,[ProjectLevel]    =  B.[ProjectLevel]   
           ,[Cost]            =  B.[Cost]           
           ,[Area]            =  B.[Area]           
           ,[BuildType]       =  B.[BuildType]      
           ,[ProjectUse]      =  B.[ProjectUse]     
           ,[ProjectNumber]   =  B.[ProjectNumber]  
           ,[SGXKZBH]         =  B.[SGXKZBH]        
           ,[FZJG]            =  B.[FZJG]           
           ,[FZTime]          =  B.[FZTime]         
           ,[DZZT]            =  B.[DZZT]           
           ,[SJStartDate]     =  B.[SJStartDate]    
           ,[SJEndDate]       =  B.[SJEndDate]      
           ,[upScale]         =  B.[upScale]        
           ,[DoScale]         =  B.[DoScale]
		   ,jsdwid = b.jsdwid        
    FROM  GD_TC_SGXKZ_PrjInfo A,TC_SGXKZ_PrjInfo B
	WHERE A.FPrjItemId = B.FPrjItemId
	AND B.FPrjItemId = @fprjitemid
	AND B.FAppId =@fappid
--更新参与企业  (先删除参与人员和企业，再插入参与企业和人员)
DELETE FROM  GD_TC_PrjItem_Ent  WHERE  FPrjItemId = @fprjitemid 
DELETE FROM  GD_TC_PrjItem_Emp  WHERE  FPrjItemId = @fprjitemid
--从业务表中插入新的单位和人员
--项目参与企业
INSERT INTO [dbo].[GD_TC_PrjItem_Ent]
           ([FId]
           ,[FPrjId]
           ,[FPrjItemId]
           ,[FProcId]
           ,[FAppId]
           ,[QYID]
           ,[FName]
           ,[FEntType]
           ,[FOrgCode]
           ,[FAddress]
           ,[ZZDJ]
           ,[ZZZSH]
           ,[YYZZH]
           ,[FLegalPerson]
           ,[FTel]
           ,[FLinkMan]
           ,[FMobile]
           ,[mZXZZ]
           ,[oZXZZ]
           ,[FCreateTime]
           ,[FTime]
           ,[Remark])
SELECT   NEWID(),
        [FPrjId]
           ,[FPrjItemId]
           ,[FProcId]
           ,[FAppId]
           ,[QYID]
           ,[FName]
           ,[FEntType]
           ,[FOrgCode]
           ,[FAddress]
           ,[ZZDJ]
           ,[ZZZSH]
           ,[YYZZH]
           ,[FLegalPerson]
           ,[FTel]
           ,[FLinkMan]
           ,[FMobile]
           ,[mZXZZ]
           ,[oZXZZ]
           ,[FCreateTime]
           ,[FTime]
           ,[Remark]
FROM TC_PrjItem_Ent
WHERE FPrjItemId = @fprjitemid
AND FAppId = @fappid
-- 项目参与人员
INSERT INTO [dbo].[GD_TC_PrjItem_Emp]
           ([FId]
           ,[FPrjId]
           ,[FPrjItemId]
           ,[FAppId]
           ,[FEntId]
           ,[FHumanName]
           ,[FSex]
           ,[FPhoto]
           ,[FBirthDay]
           ,[ZJLX]
           ,[ZGXL]
           ,[FMobile]
           ,[FTel]
           ,[EmpType]
           ,[FIdCard]
           ,[XMZW]
           ,[ZJHM]
           ,[FEntName]
           ,[ZW]
           ,[ZC]
           ,[ZY]
           ,[ZSBH]
           ,[DJ]
           ,[ZCBH]
           ,[ZCZY]
           ,[ZCRQ]
           ,[FCreateTime]
           ,[FTime]
           ,[Remark]
           ,[FEmpId]
           ,[PId]
           ,[FLinkId]           --linkid是参与单位的主键
           ,[FEntType])    
SELECT  NEWID(),
        [FPrjId]
           ,[FPrjItemId]
           ,[FAppId]
           ,[FEntId]
           ,[FHumanName]
           ,[FSex]
           ,[FPhoto]
           ,[FBirthDay]
           ,[ZJLX]
           ,[ZGXL]
           ,[FMobile]
           ,[FTel]
           ,[EmpType]
           ,[FIdCard]
           ,[XMZW]
           ,[ZJHM]
           ,[FEntName]
           ,[ZW]
           ,[ZC]
           ,[ZY]
           ,[ZSBH]
           ,[DJ]
           ,[ZCBH]
           ,[ZCZY]
           ,[ZCRQ]
           ,[FCreateTime]
           ,[FTime]
           ,[Remark]
           ,[FEmpId]
           ,[PId]
           ,[FLinkId]
           ,[FEntType]
FROM TC_PrjItem_Emp
WHERE FPrjItemId = @fprjitemid
AND FAppId = @fappid
AND checkstate = '1'   --审核通过人员才归档

--归档工程项目明细表
--先删除历史备案表
delete from  GD_TC_SGXKZ_PrjDetail  where  PrjItemId = @fprjitemid
--再插入
INSERT INTO GD_TC_SGXKZ_PrjDetail
           (
		     [FId]
           ,[FAppId]
           ,[PrjId]
           ,[PrjItemId]
           ,[SgxkzInfoID]
           ,[JSDW]
           ,[AddressDept]
           ,[DetailName]
           ,[Scale]
           ,[UpScale]
           ,[DoScale]
           ,[AbLayerNum]
           ,[UnLayerNum]
           ,[ReMark]
		   )
SELECT   
            NEWID() as fid   
           ,[FAppId]
           ,[PrjId]
           ,[PrjItemId]
           ,[SgxkzInfoID]
           ,[JSDW]
           ,[AddressDept]
           ,[DetailName]
           ,[Scale]
           ,[UpScale]
           ,[DoScale]
           ,[AbLayerNum]
           ,[UnLayerNum]
           ,[ReMark]
FROM  TC_SGXKZ_PrjDetail
WHERE FAPPID = @fappid
      AND PrjItemId = @fprjitemid
--归档保证金确认信息
--先删除归档信息
DELETE FROM  GD_TC_SGXKZ_BZJQR  WHERE FPrjItemId = @fprjitemid
--再导入
INSERT INTO [dbo].[GD_TC_SGXKZ_BZJQR]
           ([FId]
           ,[FAppId]
           ,[FPrjItemId]
           ,[FPrjId]
           ,[JFXM]
           ,[JFXMBM]
           ,[Money]
           ,[JFSJ]
           ,[SKJBR]
           ,[SKDW])
SELECT     NEWID()
           ,[FAppId]
           ,[FPrjItemId]
           ,[FPrjId]
           ,[JFXM]
           ,[JFXMBM]
           ,[Money]
           ,[JFSJ]
           ,[SKJBR]
           ,[SKDW]
FROM TC_SGXKZ_BZJQR
WHERE FAppId = @fappid
AND  FPrjItemId = @fprjitemid
END
ELSE  --不存在则直接插入
BEGIN
--工程简要说明表
INSERT INTO [dbo].[GD_TC_SGXKZ_PrjInfo]
           ([FId]
           ,[FAppId]
           ,[FPrjItemId]
           ,[JSDW]
           ,[JProvince]
           ,[JCity]
           ,[JCounty]
           ,[JSDWAddressDept]
           ,[JSDWDZ]
           ,[JSDWXZ]
           ,[FDDBR]
           ,[FRDH]
           ,[LZR]
           ,[LXDH]
           ,[JSFZR]
           ,[JSFZRZC]
           ,[JSFZRDH]
           ,[PrjId]
           ,[PrjItemId]
           ,[ProjectName]
           ,[PrjItemName]
           ,[PrjItemType]
           ,[ProjectTime]
           ,[ReportTime]
           ,[PProvince]
           ,[PCity]
           ,[PCounty]
           ,[PrjAddressDept]
           ,[Address]
           ,[ConstrScale]
           ,[ConstrType]
           ,[Price]
           ,[Currency]
           ,[StartDate]
           ,[EndDate]
           ,[FResult]
           ,[Remark]
           ,[ProjectFile]
           ,[ProjectNo]
           ,[ProjectLevel]
           ,[Cost]
           ,[Area]
           ,[BuildType]
           ,[ProjectUse]
           ,[ProjectNumber]
           ,[SGXKZBH]
           ,[FZJG]
           ,[FZTime]
           ,[DZZT]
           ,[SJStartDate]
           ,[SJEndDate]
           ,[upScale]
           ,[DoScale]
		   ,jsdwid)
   SELECT  [FId]
           ,[FAppId]
           ,[FPrjItemId]
           ,[JSDW]
           ,[JProvince]
           ,[JCity]
           ,[JCounty]
           ,[JSDWAddressDept]
           ,[JSDWDZ]
           ,[JSDWXZ]
           ,[FDDBR]
           ,[FRDH]
           ,[LZR]
           ,[LXDH]
           ,[JSFZR]
           ,[JSFZRZC]
           ,[JSFZRDH]
           ,[PrjId]
           ,[PrjItemId]
           ,[ProjectName]
           ,[PrjItemName]
           ,[PrjItemType]
           ,[ProjectTime]
           ,[ReportTime]
           ,[PProvince]
           ,[PCity]
           ,[PCounty]
           ,[PrjAddressDept]
           ,[Address]
           ,[ConstrScale]
           ,[ConstrType]
           ,[Price]
           ,[Currency]
           ,[StartDate]
           ,[EndDate]
           ,[FResult]
           ,[Remark]
           ,[ProjectFile]
           ,[ProjectNo]
           ,[ProjectLevel]
           ,[Cost]
           ,[Area]
           ,[BuildType]
           ,[ProjectUse]
           ,[ProjectNumber]
           ,[SGXKZBH]
           ,[FZJG]
           ,[FZTime]
           ,[DZZT]
           ,[SJStartDate]
           ,[SJEndDate]
           ,[upScale]
           ,[DoScale]
		   ,jsdwid
FROM  TC_SGXKZ_PrjInfo
WHERE  FPrjItemId = @fprjitemid
AND FAppId = @fappid
--项目参与企业
INSERT INTO [dbo].[GD_TC_PrjItem_Ent]
           ([FId]
           ,[FPrjId]
           ,[FPrjItemId]
           ,[FProcId]
           ,[FAppId]
           ,[QYID]
           ,[FName]
           ,[FEntType]
           ,[FOrgCode]
           ,[FAddress]
           ,[ZZDJ]
           ,[ZZZSH]
           ,[YYZZH]
           ,[FLegalPerson]
           ,[FTel]
           ,[FLinkMan]
           ,[FMobile]
           ,[mZXZZ]
           ,[oZXZZ]
           ,[FCreateTime]
           ,[FTime]
           ,[Remark])
SELECT   NEWID(),
        [FPrjId]
           ,[FPrjItemId]
           ,[FProcId]
           ,[FAppId]
           ,[QYID]
           ,[FName]
           ,[FEntType]
           ,[FOrgCode]
           ,[FAddress]
           ,[ZZDJ]
           ,[ZZZSH]
           ,[YYZZH]
           ,[FLegalPerson]
           ,[FTel]
           ,[FLinkMan]
           ,[FMobile]
           ,[mZXZZ]
           ,[oZXZZ]
           ,[FCreateTime]
           ,[FTime]
           ,[Remark]
FROM TC_PrjItem_Ent
WHERE FPrjItemId = @fprjitemid
AND FAppId = @fappid
-- 项目参与人员
INSERT INTO [dbo].[GD_TC_PrjItem_Emp]
           ([FId]
           ,[FPrjId]
           ,[FPrjItemId]
           ,[FAppId]
           ,[FEntId]
           ,[FHumanName]
           ,[FSex]
           ,[FPhoto]
           ,[FBirthDay]
           ,[ZJLX]
           ,[ZGXL]
           ,[FMobile]
           ,[FTel]
           ,[EmpType]
           ,[FIdCard]
           ,[XMZW]
           ,[ZJHM]
           ,[FEntName]
           ,[ZW]
           ,[ZC]
           ,[ZY]
           ,[ZSBH]
           ,[DJ]
           ,[ZCBH]
           ,[ZCZY]
           ,[ZCRQ]
           ,[FCreateTime]
           ,[FTime]
           ,[Remark]
           ,[FEmpId]
           ,[PId]
           ,[FLinkId]
           ,[FEntType])    
SELECT  NEWID(),
        [FPrjId]
           ,[FPrjItemId]
           ,[FAppId]
           ,[FEntId]
           ,[FHumanName]
           ,[FSex]
           ,[FPhoto]
           ,[FBirthDay]
           ,[ZJLX]
           ,[ZGXL]
           ,[FMobile]
           ,[FTel]
           ,[EmpType]
           ,[FIdCard]
           ,[XMZW]
           ,[ZJHM]
           ,[FEntName]
           ,[ZW]
           ,[ZC]
           ,[ZY]
           ,[ZSBH]
           ,[DJ]
           ,[ZCBH]
           ,[ZCZY]
           ,[ZCRQ]
           ,[FCreateTime]
           ,[FTime]
           ,[Remark]
           ,[FEmpId]
           ,[PId]
           ,[FLinkId]
           ,[FEntType]
FROM TC_PrjItem_Emp
WHERE FPrjItemId = @fprjitemid
AND FAppId = @fappid
--AND checkstate = '1' --审核通过人员才归档  --初次办理的时候所有人员全部归档
--工程项目明细表
INSERT INTO GD_TC_SGXKZ_PrjDetail
           (
		     [FId]
           ,[FAppId]
           ,[PrjId]
           ,[PrjItemId]
           ,[SgxkzInfoID]
           ,[JSDW]
           ,[AddressDept]
           ,[DetailName]
           ,[Scale]
           ,[UpScale]
           ,[DoScale]
           ,[AbLayerNum]
           ,[UnLayerNum]
           ,[ReMark]
		   )
SELECT   
            NEWID() as fid   
           ,[FAppId]
           ,[PrjId]
           ,[PrjItemId]
           ,[SgxkzInfoID]
           ,[JSDW]
           ,[AddressDept]
           ,[DetailName]
           ,[Scale]
           ,[UpScale]
           ,[DoScale]
           ,[AbLayerNum]
           ,[UnLayerNum]
           ,[ReMark]
FROM  TC_SGXKZ_PrjDetail
WHERE FAPPID = @fappid
      AND PrjItemId = @fprjitemid
--归档保证金确认信息
INSERT INTO [dbo].[GD_TC_SGXKZ_BZJQR]
           ([FId]
           ,[FAppId]
           ,[FPrjItemId]
           ,[FPrjId]
           ,[JFXM]
           ,[JFXMBM]
           ,[Money]
           ,[JFSJ]
           ,[SKJBR]
           ,[SKDW])
SELECT     NEWID()
           ,[FAppId]
           ,[FPrjItemId]
           ,[FPrjId]
           ,[JFXM]
           ,[JFXMBM]
           ,[Money]
           ,[JFSJ]
           ,[SKJBR]
           ,[SKDW]
FROM TC_SGXKZ_BZJQR
WHERE FAppId = @fappid
AND  FPrjItemId = @fprjitemid

--产生打印证书信息
UPDATE GD_TC_SGXKZ_PrjInfo  SET DZZT=1  where FAppid = @fappid
insert into GD_SGXKZ_ZSPrint select * from Uf_Get_SGXKZ_Print(@fappid)

END
