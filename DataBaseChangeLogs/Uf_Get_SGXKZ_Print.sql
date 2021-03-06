USE [dbCenter]
GO
create table GD_SGXKZ_ZSPrint
(FAppId varchar(50) null,
 fprjid varchar(50) null,
 FPrjItemId varchar(50) null,
 PrjAddressDept varchar(50) null,
 SGXKZBH varchar(50) null,
 jsdw varchar(200) null,
 Prjitemname varchar(300) null,
 ProjectNumber varchar(100) null,
 Address varchar(500) null,
 ConstrScale varchar(500) null,
 Price varchar(18) null,
 KCDWid varchar(50) null,
 SJDWid varchar(50) null,
 SGDWid varchar(50) null,
 JLDWid varchar(50) null,
 KCDW varchar(200) null,
 SJDW varchar(200) null,
 SGDW varchar(200) null,
 JLDW varchar(200) null,
 KCFZR varchar(50) null,
 SJFZR varchar(50) null,
 SGFZR varchar(50) null,
 ZJLG  varchar(50) null, 
 startdate datetime null,
 enddate   datetime null,
 Remark varchar(4000) null,
 sqsj datetime     null,
 fzjg varchar(100) null,
 fzrq datetime     null ,
 JSDWXZ varchar(50) null, 
 JSDWDZ varchar(200) null, 
 JSFZR  varchar(50) null, 
 Currency varchar(50) null,
 JSFZRZC  varchar(50) null,
 JSFZRDH  varchar(50) null,
 YDPZSX varchar(50) null,YDGHXKZBH varchar(50) null,YDHFJG varchar(200) null,YDTime datetime null,
 GCGHXKZBH varchar(50) null,GCHFJG varchar(200) null,GCTime datetime null,
 SGTJ varchar(50) null,ZBTZSBH varchar(50) null,HTBH varchar(50) null,
 SGTSCHGSBH varchar(50) null,jlHTBH varchar(50) null,ZLBABH varchar(50) null,
 AQBABH varchar(50) null,ISDBS varchar(20) null,JF varchar(20) null,YF varchar(20) null,
 CNS varchar(1000) null,QTZL  varchar(3000) null
)

/****** Object:  UserDefinedFunction [dbo].[Uf_Get_SGXKZ_Print]    Script Date: 2015/5/15 11:42:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER function [dbo].[Uf_Get_SGXKZ_Print] (@FAppId varchar(50))
RETURNS @t table
(FAppId varchar(50) null,
 fprjid varchar(50) null,
 FPrjItemId varchar(50) null,
 PrjAddressDept varchar(50) null,
 SGXKZBH varchar(50) null,
 jsdw varchar(200) null,
 Prjitemname varchar(300) null,
 ProjectNumber varchar(100) null,
 Address varchar(500) null,
 ConstrScale varchar(500) null,
 Price varchar(18) null,
 KCDWid varchar(50) null,
 SJDWid varchar(50) null,
 SGDWid varchar(50) null,
 JLDWid varchar(50) null,
 KCDW varchar(200) null,
 SJDW varchar(200) null,
 SGDW varchar(200) null,
 JLDW varchar(200) null,
 KCFZR varchar(50) null,
 SJFZR varchar(50) null,
 SGFZR varchar(50) null,
 ZJLG  varchar(50) null, 
 startdate datetime null,
 enddate   datetime null,
 Remark varchar(4000) null,
 sqsj datetime     null,
 fzjg varchar(100) null,
 fzrq datetime     null ,
 JSDWXZ varchar(50) null, 
 JSDWDZ varchar(200) null, 
 JSFZR  varchar(50) null, 
 Currency varchar(50) null,
 JSFZRZC  varchar(50) null,
 JSFZRDH  varchar(50) null,
 YDPZSX varchar(50) null,YDGHXKZBH varchar(50) null,YDHFJG varchar(200) null,YDTime datetime null,
 GCGHXKZBH varchar(50) null,GCHFJG varchar(200) null,GCTime datetime null,
 SGTJ varchar(50) null,ZBTZSBH varchar(50) null,HTBH varchar(50) null,
 SGTSCHGSBH varchar(50) null,jlHTBH varchar(50) null,ZLBABH varchar(50) null,
 AQBABH varchar(50) null,ISDBS varchar(20) null,JF varchar(20) null,YF varchar(20) null,
 CNS varchar(1000) null,QTZL  varchar(3000) null
)
as 
begin
   if (@FAppId = '1')
   begin
	   insert into @t(FAppId,fprjid,FPrjItemId,jsdw,Prjitemname,Address,ConstrScale,Price,StartDate,endDate,Remark
					  ,JSDWXZ,JSDWDZ,JSFZR,Currency,JSFZRZC,JSFZRDH,PrjAddressDept,sqsj,fzjg,fzrq,SGXKZBH
					  )
	   select   FAppId,PrjId
			   ,FPrjItemId 
			   ,[JSDW]
			   ,[PrjItemName]
			   ,[Address]
			   ,[ConstrScale]
			   ,[Price]
			   ,StartDate,endDate
			   ,Remark
			   ,JSDWXZ
			   ,JSDWDZ
			   ,JSFZR
			   ,Currency
			   ,JSFZRZC
			   ,JSFZRDH
			   ,'01'+PrjAddressDept
			   ,ReportTime,fzjg,fztime,SGXKZBH
		  from GD_TC_SGXKZ_PrjInfo
		 where DZZT = 1
	end
	else if (@FAppId = '2')
	begin
	   insert into @t(FAppId,fprjid,FPrjItemId,jsdw,Prjitemname,Address,ConstrScale,Price,StartDate,endDate,Remark
					  ,JSDWXZ,JSDWDZ,JSFZR,Currency,JSFZRZC,JSFZRDH,PrjAddressDept,sqsj,fzjg,fzrq,SGXKZBH
					  )
	   select   FAppId,PrjId
			   ,FPrjItemId 
			   ,[JSDW]
			   ,[PrjItemName]
			   ,[Address]
			   ,[ConstrScale]
			   ,[Price]
			   ,StartDate,endDate
			   ,Remark
			   ,JSDWXZ
			   ,JSDWDZ
			   ,JSFZR
			   ,Currency
			   ,JSFZRZC
			   ,JSFZRDH
			   ,'01'+PrjAddressDept
			   ,ReportTime,fzjg,fztime,SGXKZBH
		  from GD_TC_SGXKZ_PrjInfo
		 where DZZT = 2																				
	end
	else
	begin
	   insert into @t(FAppId,fprjid,FPrjItemId,jsdw,Prjitemname,Address,ConstrScale,Price,StartDate,endDate,Remark
					  ,JSDWXZ,JSDWDZ,JSFZR,Currency,JSFZRZC,JSFZRDH,PrjAddressDept,sqsj,fzjg,fzrq,SGXKZBH
					  )
	   select   FAppId,PrjId
			   ,FPrjItemId 
			   ,[JSDW]
			   ,[PrjItemName]
			   ,[Address]
			   ,[ConstrScale]
			   ,[Price]
			   ,StartDate,endDate
			   ,Remark
			   ,JSDWXZ
			   ,JSDWDZ
			   ,JSFZR
			   ,Currency
			   ,JSFZRZC
			   ,JSFZRDH
			   ,'01'+PrjAddressDept
			   ,ReportTime,fzjg,fztime,SGXKZBH
		  from GD_TC_SGXKZ_PrjInfo
		 where FAppId = @FAppId
	end
 /*
    update @t set ProjectNumber = b.ProjectNumber
      from @t a,TC_Prj_Info b
     where fprjid = b.fid*/
   --勘察  
   update @t set KCDW = b.FName,kcdwid = b.qyid,kcfzr = b.flinkman
     from @t a,TC_PrjItem_Ent b
    where a.FPrjItemId = b.FPrjItemId
      and b.FEntType = '5'

   --设计
   update @t set SJDW = b.FName,sjdwid = b.qyid,kcfzr = b.flinkman
     from @t a,TC_PrjItem_Ent b
    where a.FPrjItemId = b.FPrjItemId
      and b.FEntType = '6'

   --监理
   update @t set JLDW = b.FName,jldwid = b.qyid,kcfzr = b.flinkman
     from @t a,TC_PrjItem_Ent b
    where a.FPrjItemId = b.FPrjItemId
      and b.FEntType = '7'    
      
   --施工
   update @t set SGDW = b.FName,sgdwid = b.qyid,kcfzr = b.flinkman
     from @t a,TC_PrjItem_Ent b
    where a.FPrjItemId = b.FPrjItemId
      and b.FEntType = '2'  
    
	--全部取负责人
   --勘察负责人  
   update @t set KCFZR = b.FHumanName
     from @t a,TC_PrjItem_Emp b
    where a.FPrjItemId = b.FPrjItemId
      and b.EmpType like '11220201%'
      and a.kcdwid  = b.Fentid

   --设计负责人
   update @t set SJFZR = b.FHumanName
     from @t a,TC_PrjItem_Emp b
    where a.FPrjItemId = b.FPrjItemId
      and b.EmpType like '11220201%'
      and a.sjdwid  = b.Fentid
      
   --监理负责人
   update @t set ZJLG = b.FHumanName
     from @t a,TC_PrjItem_Emp b
    where a.FPrjItemId = b.FPrjItemId 
      and b.EmpType like '11220201%'
      and a.jldwid  = b.Fentid        
      
   --施工负责人
   update @t set SGFZR = b.FHumanName
     from @t a,TC_PrjItem_Emp b
    where a.FPrjItemId = b.FPrjItemId
      and b.EmpType like '11220201%' 
      and a.sgdwid  = b.Fentid
 /*   
   update @t set ZBTZSBH = b.ZBTZSBH
     from @t a,TC_SGXKZ_ZBJG b
    where a.FAppId = b.FAppId
                    
   update @t set JSDWXZ = b.fname
     from @t a,CF_Sys_Dic b
    where a.JSDWXZ = b.FNumber
      and b.FParentid='112212' and b.FName<>'全部'

   update @t set YDPZSX = b.YDPZSX   --用地批准手续
     from @t a,TC_SGXKZ_Location b
    where a.FAppId = b.FAppId
        
    update @t set YDGHXKZBH = b.YDGHXKZBH,YDHFJG = b.HFJG,YDtime = b.CreateTime  --
      from @t a,TC_SGXKZ_JSYDGHXKZ b
     where a.FAppId = b.FAppId

    update @t set GCGHXKZBH = b.GCGHXKZBH,GCHFJG = b.HFJG,GCTime = b.CreateTime   --
      from @t a,TC_SGXKZ_JSGCGHXKZ b
     where a.FAppId = b.FAppId
     
    update @t set SGTJ = b.SGTJ  --施工条件
      from @t a,TC_SGXKZ_QTZL b
     where a.FAppId = b.FAppId     
                        

    update @t set HTBH = b.HTBH  --施工合同
      from @t a,TC_SGXKZ_HTBA b
     where a.FAppId = b.FAppId 
                             
    update @t set SGTSCHGSBH = b.SGTSCHGSBH  --施工图设计文件审查合格证明
      from @t a,TC_SGXKZ_SGTSC b
     where a.FAppId = b.FAppId 

    update @t set jlHTBH = b.HTBH  --合同编号
      from @t a,TC_SGXKZ_HTBA b
     where a.FAppId = b.FAppId 
       and b.htlb = '511002'
 
     update @t set ZLBABH = b.ZLBABH,AQBABH = b.AQBABH  --质量、安全备案
      from @t a,TC_SGXKZ_JDSX b
     where a.FAppId = b.FAppId 
 
      update @t set ISDBS = case b.ISDBS when 1 then '办理' else '没有办理' end ,
                    JF = b.JF,YF = b.YF  --资金保函或证明 是否办理保证书 甲方、乙方
      from @t a,TC_SGXKZ_ZJBH b
     where a.FAppId = b.FAppId 
                   
      update @t set CNS = b.CNS,QTZL = b.QTZL  --无拖欠工程款情形的承诺书 其他资料
      from @t a,TC_SGXKZ_QTZL b
     where a.FAppId = b.FAppId  
     */
                     
return 
end
