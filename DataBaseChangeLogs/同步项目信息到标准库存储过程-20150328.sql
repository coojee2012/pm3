USE [dbCenter]
GO
/****** Object:  StoredProcedure [dbo].[SP_GC_TO_BZK]    Script Date: 2015/3/28 11:10:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--同步项目信息到标准库
alter PROC [dbo].[SP_GCPRJ_TO_BZK]
(
	@FID VARCHAR(50) --项目编号
)
AS
IF NOT EXISTS(SELECT TOP 1* FROM XM_BaseInfo.[dbo].[XM_XMJBXX] WHERE [XMBH]=@FID)
BEGIN
--先判断项目所属地，如果不是县级就判断是否是市级，不是市级再判断是否是省级
declare  @xmsd int;
declare  @province int,@city int,@county int
select @province = Province,@city = City,@county = County from  TC_Prj_Info  where  FId = @FID;
if(isnull(@county,'') != '')
begin
set @xmsd = @county;
end
else if(isnull(@city,'') != '')
begin
   set @xmsd = @city;
end 
else if(ISNULL(@province,'') != '')
begin
   set @xmsd = @province;
end
---------------------------------------------------
INSERT INTO XM_BaseInfo.[dbo].[XM_XMJBXX]
           ([XMBH]   --项目编号
           ,[XMMC]   --项目名称
           ,[XMSD]   --项目属地
           ,[XMDZ]   --项目地址
           ,[JSDW]   --建设单位
           ,[JSDWDZ] --建设单位地址
           ,[XMLX]   --项目类型
           ,[XMZLX]  --建设模式
           ,[JSXZ]   --建设性质
           ,[JSMS]   --建设模式
           ,[XMZTZ]  --项目总投资
           ,[JSGM]   --建设规模
           ,[JSNR]   --建设内容
           ,[BH]     --编号  
           ,[JSDWZZJFDM]  --建设单位组织机构代码
           ,[JSDWFR]      --建设单位法人
           ,[JSDWFRDH]    --建设单位法人电话
           ,[JSDWJSFZR]   --建设单位技术负责人
           ,[JSDWJSFZRZC]  --建设单位技术负责人职称
           ,[JSDWJSFZRDH]  --建设单位技术负责人电话
           ,[SFSW]         --是否涉外
           ,[CreateTime]   --创建时间
           ,[Ftime]        --更新时间
           ,[YDXZ]         --用地性质
           ,[IsZHCJ])      --是否政府采购
	SELECT TOP 1 	           
 	a.FId,                        
 	a.ProjectName,                
 	@xmsd,                     
 	a.Address,                     
 	a.jsdw,                       
 	a.JSDWDZ,
	a.ProjectType,
	null,
	null as jsxz,
	null as jsms,
	a.Investment,
	a.ConstrScale,
	a.ConstrContent,
	a.ProjectNo,
	a.JSDWDM,
	a.JSDWFR,                  
 	null JSDWFRDH,
	null JSDWJSFZR,
	null as JSDWJSFZRZC,
	null as JSDWJSFZRDH,
	a.IsForeign,	           
 	GETDATE(),                    
 	GETDATE(),
	a.LandType,
	null   
	FROM TC_Prj_Info A 
	LEFT JOIN CF_SYS_DIC B ON A.CONSTRTYPE=B.FNUMBER
	LEFT JOIN TC_JSDW_USER C ON a.FJSDWID  = c.FID
	WHERE A.FID=@FID;
END

