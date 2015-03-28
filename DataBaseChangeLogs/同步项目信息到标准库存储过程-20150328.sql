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
INSERT INTO XM_BaseInfo.[dbo].[XM_XMBJXX]
           ([BJID]  --未知id,newid
           ,[XMBH]  --项目编号
           ,[XMMC]  --项目名称
           ,[XMSD]  --项目属地
           ,[XMDZ]  --项目地址
           ,[JSDW]  --建设单位
           ,[JSDWDZ] --建设单位地址
           ,[JSDWFR] --建设单位法人
           ,[JSDWXZ] --建设单位性质
           ,[JSWDXDZM] --建设单位(未知)
           ,[GCLB]     --工程类别
           ,[JGLX]     --什么类型
           ,[ZFTZ]     --政府投资
           ,[ZCTZ]     --什么投资 
           ,[WSTZ]    --外商投资
           ,[DKTZ]    --贷款总额
           ,[QTTZ]    --其他投资
           ,[ZTZE]    --总投资额
           ,[YDMJ]    --未知
           ,[JZMJ]    --未知
           ,[DS]      --未知
           ,[DSCS]     --未知
           ,[DXCS]      --未知
           ,[ZDGD]       --未知
           ,[ZDKD]    --未知
           ,[CD]        --未知
           ,[KD]        --未知
           ,[JHKGRQ]    --计划开工日期
           ,[JHJGRQ]    --计划竣工日期
           ,[FBFS]      --发布方式
           ,[JSXZ]      --建设性质
           ,[XMJSNR]    --项目建设内容
           ,[JSGCYDXKZ] --建设工地用户许可证
           ,[JSGCGHXKZ] --建设工程规划许可证
           ,[LXJB]      --未知
           ,[LXWJ]      --未知
           ,[LXWH]      --未知
           ,[PZDW]      --批准单位
           ,[PZRQ]      --批准日期
           ,[BJBH]      --报建编号
           ,[BJRQ]      --报建日期
           ,[LXPZMJ]    --未知
           ,[LXPZGM]    --未知
           ,[SFSW]      --未知
           ,[BZ]        --备注
           ,[BH]        --编号
           ,[CreateTime]  --创建日期
           ,[Ftime])      --创建日期
	SELECT TOP 1 
	newid(),                     
 	a.FId,                        
 	a.ProjectName,                
 	@xmsd,                     
 	a.Address,                     
 	a.jsdw,                       
 	a.JSDWDZ,                     
 	a.JSDWFR,                     
 	null,                   
 	C.DWXZ,                       
 	A.ProjectType,                
 	NULL,                         
 	NULL,                         
 	NULL,                         
 	NULL,                         
 	NULL,                         
 	NULL,                         
 	A.Investment,                 
 	NULL,                         
 	NULL,                         
 	NULL AS DS,                   
 	NULL,                         
 	NULL,                         
 	NULL,                         
 	NULL,                         
 	NULL,                         
 	NULL,                         
 	A.StartDate,                  
 	A.EndDate,                    
 	NULL AS FBFS,                 
   NULL AS JSXZ,                 
 	A.ConstrContent AS XMJSNR,    
 	A.JSYDXKZ,                    
 	A.JSGCXKZ,                    
 	NULL,                         
 	A.ConstrBasis,                
 	NULL,                         
 	NULL AS PZDW,                 
 	NULL AS PZRQ,                 
 	NULL AS BJBH,                 
 	NULL AS BJRQ,                 
 	NULL AS LXPZMJ,               
 	NULL AS LXPZGM,               
 	NULL AS SFSW,                 
 	NULL AS BZ,                   
 	NULL AS BH,                   
 	GETDATE(),                    
 	GETDATE()                     

	FROM TC_Prj_Info A 
	LEFT JOIN CF_SYS_DIC B ON A.CONSTRTYPE=B.FNUMBER
	LEFT JOIN TC_JSDW_USER C ON a.FJSDWID  = c.FID
	WHERE A.FID=@FID;
END
