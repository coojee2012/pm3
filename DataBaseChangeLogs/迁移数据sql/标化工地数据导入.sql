--准备导入的数据
 if not exists (select 1
            from  sysobjects
           where  id = object_id('dbo._BZGD_s')
            and   type = 'U')
begin
  select ProcessKeyValue,0000 as actionid into _BZGD_s from JKCWFDB_WORK_NJS.DBO.C05 where 1=2
end
else
begin
  delete _BZGD_s
end

insert _BZGD_s(ProcessKeyValue,actionid)
select distinct C05.ProcessKeyValue,actionid
  from JKCWFDB_WORK_NJS .DBO.C06 Left Join 
	   JKCWFDB_WORK_NJS.DBO.C05 
	on C05.ProjectID=C06.ProjectID
 where C06.ActionState in (0,1,2) 
   and C05.ProcessTimeB>='2014-01-01'
   and C06.actionid in (308,309,310);--未上报 --待接件 --待审核


--导入标化工地相关的项目信息
INSERT INTO dbCenter.dbo.TC_Prj_Info
           ([FId]
           ,[JSDW]
           ,[JSDWDM]
           ,[JSDWDZ]
           ,[Contacts]
           ,[Mobile]
           ,[ProjectName]
           ,[Province]
           ,[City]
           ,[County]
           ,[ProjectType]
           ,[Address]
           ,[ProjectNumber]
           ,[ProjectLevel]
           ,[ProjectTime]
           ,[ProjectNo]
           ,[IsForeign]
           ,[JSYDXKZ]
           ,[JSGCXKZ]
           ,[Area]
           ,[Investment]
           ,[ConstrType]
           ,[ProjectUse]
           ,[StartDate]
           ,[EndDate]
           ,[RegisterTime]
           ,[ConstrBasis]
           ,[ConstrContent]
           ,[FJSDWID]
           ,[AddressDept]
           ,[ConstrScale]
           ,[LandType]
           ,[JSDWFR])
SELECT --A.*
	 b.fqbm     FId         --项目id
	,isnull(b.JSDW,'')    JSDW         --建设单位名建设单位组织机构代码称
	,null       JSDWDM         --建设单位组织机构代码
	,c.txdz     JSDWDZ         --建设单位地址
	,c.LXR   Contacts         --建设单位联系人
	,c.LXDH Mobile         --建设单位联系电话
	,b.gcmc ProjectName         --项目名称
	,isnull(b.s,'51') Province  --项目属地-省
	,b.SQ City                  --项目属地-市州
	,b.qx County         --项目属地-区县
	,'2000103'  ProjectType         --项目类别:2000101--房屋建筑;2000102--市政基础;2000103--其他
	,b.JSDD  Address         --项目地址
	,null ProjectNumber         --立项文号
	,'11220401' as  ProjectLevel  --立项级别
	,B.kgrq ProjectTime         --项目时间
	,null ProjectNo         --项目编号
	,b.JW IsForeign         --是否涉外
	,NULL JSYDXKZ         --建设用地许可证号
	,NULL JSGCXKZ         --工程规划许可证号
	,null Area            --建设总面积（m2）
	,b.TZGM Investment         --投资规模(万元)
	,'30503' as ConstrType     --建设性质：2000501--新建；2000502--改建
	--给水	200010201
	--排水	200010202
	--燃气	200010203
	--热力	200010204
	--道路	200010205
	--桥隧	200010206
	--风景园林	200010207
	--环境园林	200010208
	--公共交通	200010209
	--其他	200010210
	,null ProjectUse             --工程用途：200010201--给水；200010205--道路
	,b.KGRQ StartDate            --实际开工日期
	,b.JGYSRQ EndDate            --实际竣工日期
	,c.YBSJ RegisterTime         --记录登记时间
	,NULL  ConstrBasis           --建设依据
	,NULL ConstrContent          --建设内容
	,c.SBDWID FJSDWID            --建设单位外键
	,null AddressDept         --地址外键
	,NULL ConstrScale--建设规模
	,NULL LandType  --用地性质
	--新建	2000501
	--改建	2000502
	--扩建	2000503
	--重建	2000504
	--迁建	2000505
	--恢复	2000506
	--其他	2000507
	,null JSDWFR         --建设单位法人
  FROM (select max(bhid) as bhid,fqbm from   JKCWFDB_WORK_NJS.DBO.BH_YWINFO group by fqbm) A,
       JKCWFDB_WORK_NJS.DBO.BH_GCINFO B,
	   JKCWFDB_WORK_NJS.DBO.bh_ybsqb  c
 WHERE A.fqbm = B.FQBM AND
       a.bhid = c.bhid and
	   not exists(select 1 from dbcenter.dbo.TC_Prj_Info bb where b.fqbm = bb.fid) and
	   A.BHID in (select  processkeyvalue from _BZGD_s);
   
-----------------------------------------------------------------------------------------------------------------
--导入标化工地备案数据信息

 if not exists (select 1
            from  sysobjects
           where  id = object_id('dbo._BZGD_Record')
            and   type = 'U')
begin
	alter TABLE  dbcenter.DBO.tc_bzgd_record alter column projectType [varchar](1000) NULL
	alter TABLE  dbcenter.DBO.tc_bzgd_record alter column KGTJSHQK    [varchar](1000) NULL
	alter TABLE  dbcenter.DBO.tc_bzgd_record alter column GCJLDWYJ  [varchar](1000) NULL
	alter TABLE  dbcenter.DBO.tc_bzgd_record alter column JDBM    [varchar](100) NULL
	alter TABLE  dbcenter.DBO.tc_bzgd_record alter column JLDWMC    [varchar](100) null
	alter TABLE  dbcenter.DBO.tc_bzgd_record alter column JSDWMC    [varchar](100) NULL
	alter TABLE  dbcenter.DBO.tc_bzgd_record alter column XZZGBMYJ    [varchar](1000) NULL
	
	select * into _BZGD_Record from dbcenter.DBO.TC_BZGD_Record where 1=2
end

insert into dbo._BZGD_Record
           ([FId]
           ,[FAppId]
           ,[FPrjId]
           ,[FPrjItemId]
           ,[ProjectName]
           ,[Province]
           ,[City]
           ,[County]
           ,[IsForeign]
           ,[Address]
           ,[ProjectType]
           ,[Area]
           ,[Investment]
           ,[WPZSGD]
           ,[SGDWId]
           ,[SGDW]
           ,[SGDWDZ]
           ,[SGDWDH]
           ,[SGDWFR]
           ,[JSDWID]
           ,[JSDW]
           ,[JSDWDZ]
           ,[JSDWDH]
           ,[JLDWId]
           ,[JLDW]
           ,[JLDWDZ]
           ,[JLDWDH]
           ,[SGXKZBH]
           ,[SGDWZS]
           ,[SGDWAQSCXKZ]
           ,[SGSCYJPJSC]
           ,[XMJLAQSCLLKHZS]
           ,[AQYAQSCLLKH]
           ,[KGTJSHQK]
           ,[MQGCJD]
           ,[GCZJ]
           ,[ConstrType]
           ,[XMJL]
           ,[GCAQDJ]
           ,[StartDate]
           ,[EndDate]
           ,[ZDZLSG]
           ,[ZDAQSG]
           ,[SBDWID]
           ,[SBDWMC]
           ,[SBSJ]
           ,[FLinkMan]
           ,[FTel]
           ,[FAddres]
           ,[FPost]
           ,[JDBM]
           ,[JDLinkMan]
           ,[LJDTel]
           ,[SJAQWMSGGDJH]
           ,[GCJLDWYJ]
           ,[JLDWMC]
           ,[JLDWTBYJSJ]
           ,[JSDWYJ]
           ,[JSDWMC]
           ,[JSDWSJ]
           ,[XZZGBMYJ]
           ,[XZZGBMMC]
           ,[XZZGBMSJ]
           ,[ReMark])
select 
			a.BHID  FId,                     --主键
			a.BHID  FAppId,                  --业务外键
			b.fqbm  FPrjId,                  --项目编码
			newid()   FPrjItemId,            --工程外键 
			b.GCMC ProjectName,              --项目名称
			b.s,SQ,QX,                       --省、市、县
			b.JW IsForeign,                  --
			b.JSDD  Address,                 --建设地址
			b.JZLX         ,                 --工程类型
			b.DTJZMJ Area,                   --建设面积
			b.TZGM Investment,
			b.ISBZ WPZSGD,                   --标志性建筑
			null SGDWId,                     --施工单位,
			b.SGDW as sgdw,null as sgdwdz,
			null sgdwdh,
			null sgdwfr,
			null as jsdwid,                  --建设单位
			b.JSDW JSDW,                     
			null as jsdwdz,
			null as jsdwdh,
			null as jldwid,                  --监理单位，
			b.JLDW,
			null as jldwdz,null as jldwdh,
			b.SGXKZBH,                      --施工许可证编号
			b.QYZZZSBH   as sgdwzs,
			b.AQSCXKZBH  as SGDWAQSCXKZ,
			b.AQSCYJPJSCBH as SGSCYJPJSC,
			b.XMJLAQSCNLKHZSBH as XMJLAQSCLLKHZS,
			b.SCNLKHZSBH as AQYAQSCLLKH,    --安全员安全生产能力考核证书编号,
			d.KGTJSCQK,                     --开工条件审查情况
			b.MQGCJD,null as GCZJ,b.JGLX,        --目前进度，工程造价、工程类别       
			b.xmjl, b.GCAQPJDJ,KGRQ,JGYSRQ,  --项目经理，
			b.ZDZLSG,b.ZDAQSG,
			c.SBDWID,c.YBDW,c.YBSJ,c.lxr,c.LXDH,c.TXDZ,c.YZBM,  --申报单位
			null as JDBM,d.LXR,d.LXDH,            --监督单位
			d.GZJH,
			d.GCJLDWYJ,              
			null as JLDWMC,
			d.JLTXYJSJ,            --监理单位
			null as JSDWYJ,d.JSDWMC,d.JSTXYJSJ,   --建设单位
			e.ZGBMSCYJ,e.ZGBMSCBM,e.ZGBMSCRQ, --建设行政主管部门或有关厅局意见
			c.bz Remark                      --备注
	  from  JKCWFDB_WORK_NJS.DBO.BH_YWINFO A,
		    JKCWFDB_WORK_NJS.DBO.BH_GCINFO B,
		    JKCWFDB_WORK_NJS.DBO.bh_ybsqb  c,
		    JKCWFDB_WORK_NJS.DBO.bh_ybqtxx d,
		    JKCWFDB_WORK_NJS.DBO.v_gcyb_yj e
      WHERE a.fqbm = B.FQBM and
            a.bhid = c.bhid and
			a.bhid = d.bhid and
			a.bhid = e.bhid and
			not exists (select 1 from _BZGD_Record f where a.bhid = f.fid) and 
	        a.BHID in (select  processkeyvalue from _BZGD_s);

insert into dbCenter.dbo.TC_BZGD_Record
     select * from _BZGD_Record a where not exists (select 1 from dbCenter.dbo.TC_BZGD_Record f where a.fid = f.fid)


 if not exists (select 1
            from  sysobjects
           where  id = object_id('dbo._BZGD_PrjInfo')
            and   type = 'U')
begin
  select * into _BZGD_PrjInfo from dbcenter.DBO.TC_BZGD_PrjInfo where 1=2
end

insert into dbo._BZGD_PrjInfo
           ([FId]
           ,[FAppId]
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
           ,[Remark])
select 
			a.BHID  FId,                     --主键
			a.BHID  FAppId,                  --业务外键
			b.JSDW JSDW,                     
			b.s,SQ,QX,                       --省、市、县
			b.sd,                            --属地
			null as jsdwdz,
			null as jsdwxz,null,null,null,null,
			null as jsfzr,null as jsfzrzc,null as jsfzrdh,
			b.fqbm  FPrjId,                  --项目编码
			null   FPrjItemId,               --工程外键 
			b.GCMC ProjectName,              --项目名称
			null as prjitemname,null prjitemtype,
			b.KGRQ as ProjectTime,
			c.YBSJ,
			b.s,SQ,QX,                       --省、市、县
			b.sd,                            --属地
			b.JSDD  Address,                 --建设地址
			b.TZGM,                          --投资规模
			b.JZLX  as ConstrType,            --工程类型
			null as price,null,
			b.KGRQ,b.JGYSRQ,                 --开工、竣工日期，
			e.ZGBMSCJL,
			c.bz Remark                      --备注
	  from  JKCWFDB_WORK_NJS.DBO.BH_YWINFO A,
		    JKCWFDB_WORK_NJS.DBO.BH_GCINFO B,
		    JKCWFDB_WORK_NJS.DBO.bh_ybsqb  c,
		    JKCWFDB_WORK_NJS.DBO.bh_ybqtxx d,
		    JKCWFDB_WORK_NJS.DBO.v_gcyb_yj e
      WHERE a.fqbm = B.FQBM and
            a.bhid = c.bhid and
			a.bhid = d.bhid and
			a.bhid = e.bhid and
			not exists (select 1 from _BZGD_PrjInfo f where a.bhid = f.fid) and 
	        a.BHID in (select  processkeyvalue from _BZGD_s);

insert into dbCenter.dbo.TC_BZGD_PrjInfo
     select * from _BZGD_PrjInfo a where not exists (select 1 from dbCenter.dbo.TC_BZGD_PrjInfo f where a.fid = f.fid)


if not exists (select 1
            from  sysobjects
           where  id = object_id('dbo._App_List_BZHD')
            and   type = 'U')
begin
    select * into _App_List_BZHD from dbCenter.dbo.CF_App_List where 1=2
end
else
begin
   delete _App_List_BZHD
end 

	INSERT INTO _App_List_BZHD
	SELECT 
	 a.bhid FId    --业务编码
	,c.SBDWID FBaseinfoId    --企业id
	,B.fqbm FPrjId    --项目编码
	,CONVERT(VARCHAR(20),YEAR(c.YBSJ)) + '年 标准化工地备案' FName    --业务名称
	,11236  FManageTypeId    --业务编码
	,c.YBSJ FwriteDate    --写入时间
	,c.YBSJ FReportDate    --上报时间
	,NULL FIsSign    --是否签字
	,0 FState        --业务状态
	,NULL FResult    --审批结论
	,YEAR(c.YBSJ) FYear    --年度
	,MONTH(c.YBSJ) FMonth    --月份
	,B.fqbm   FLinkId    --外键工程编码
	,b.JSDW   FBaseName    --建设单位名称
	,b.sd  FUpDeptId    --上报部门地区编码
	,NULL FRemark    --暂不考虑
	,NULL FIsCheck    --暂不考虑
	,NULL FCount    --暂不考虑
	,c.YBSJ FTime    --最后更新时间,业务暂时没有上报，没走流程
	,0 FIsDeleted    --是否删除
	,c.YBSJ FCreateTime    --创建时间
	,1 FReportCount    --暂不考虑
	,NULL FToBaseinfoId    --暂不考虑
	,NULL FAppDate    --暂不考虑
	,NULL FLinkAppId    --暂不考虑
	,NULL FBarCode    --暂不考虑
	,NULL FCreateUser    --暂不考虑
	,NULL FgfTime    --暂不考虑
	  from  JKCWFDB_WORK_NJS.DBO.BH_YWINFO A,
		    JKCWFDB_WORK_NJS.DBO.BH_GCINFO B,
		    JKCWFDB_WORK_NJS.DBO.bh_ybsqb  c,
		    JKCWFDB_WORK_NJS.DBO.bh_ybqtxx d,
		    JKCWFDB_WORK_NJS.DBO.v_gcyb_yj e
      WHERE a.fqbm = B.FQBM and
            a.bhid = c.bhid and
			a.bhid = d.bhid and
			a.bhid = e.bhid and
			not exists (select 1 from _App_List_BZHD f where a.bhid = f.fid) and 
	        a.BHID in (select  processkeyvalue from _BZGD_s);


  --导入业务主表
	INSERT INTO dbCenter.dbo.CF_App_List
	SELECT * FROM _App_List_BZHD a
	  where not exists (select 1 from dbCenter.dbo.CF_App_List f where a.fid = f.fid) 


if not exists (select 1
            from  sysobjects
           where  id = object_id('dbo._App_ProcessInstance_BZGD')
            and   type = 'U')
begin 
    SELECT * into _App_ProcessInstance_BZGD from dbCenter.dbo.CF_App_ProcessInstance where 1=2  --只取表结构
    alter table _App_ProcessInstance_BZGD alter column FentName varchar(200) null       
end
else
begin
    delete _App_ProcessInstance_BZGD
end
	   insert into _App_ProcessInstance_BZGD
	   (fid,FisDeleted,Ftime,FCreateTime,FBaseInfoID,FEntName,FEmpId,FLinkId,FState,FIsPrime,
			  FIsTemp,FListId,FTypeId,FLevelId,FProcessId,FManageDeptId,FManageTypeId,--FSubFlowId,
			  FResult,Fyear,FMonth,FSubmitDate,FReportDate,FCurStepID,FroleId,FBeginRoleId,FDefineDay,
			  FAppState,FSystemId,FIsNew,FSeeState,fseetime,FPlanTime,FFactTime,FBarCode)
       select newid(),0,a.FwriteDate,a.FwriteDate,a.FBaseinfoId,a.FBaseName,a.FLinkId as FEmpId,a.Fid,1,0,
	          0,'19301','1930100','1930100','8940a75c-b8a5-4de2-be85-15412132e0f1',isnull(a.fupdeptid,'51'),a.FManageTypeId,
			  '',year(a.FwriteDate),month(a.FwriteDate),a.FReportDate,a.FReportDate,isnull(a.fupdeptid,'51'),'8801','8801',1,1,'1122',0,null,null,null,null,null
	     from _App_List_BZHD a
	--	where  not exists(select 1 from dbCenter.dbo.CF_App_ProcessInstance b where a.FId = b.FLinkId)


  insert into dbCenter.dbo.CF_App_ProcessInstance
             (fid,FisDeleted,Ftime,FCreateTime,FBaseInfoID,FEntName,FEmpId,FLinkId,FState,FIsPrime,
			  FIsTemp,FListId,FTypeId,FLevelId,FProcessId,FManageDeptId,FManageTypeId,--FSubFlowId,
			  FResult,Fyear,FMonth,FSubmitDate,FReportDate,FCurStepID,FroleId,FBeginRoleId,FDefineDay,
			  FAppState,FSystemId,FIsNew,FSeeState,fseetime,FPlanTime,FFactTime,FBarCode)
       select fid,FisDeleted,Ftime,FCreateTime,FBaseInfoID,FEntName,FEmpId,FLinkId,FState,FIsPrime,
			  FIsTemp,FListId,FTypeId,FLevelId,FProcessId,FManageDeptId,FManageTypeId,--FSubFlowId,
			  FResult,Fyear,FMonth,FSubmitDate,FReportDate,FCurStepID,FroleId,FBeginRoleId,FDefineDay,
			  FAppState,FSystemId,FIsNew,FSeeState,fseetime,FPlanTime,FFactTime,FBarCode
         from _App_ProcessInstance_BZGD a
		where not exists(select 1 from dbCenter.dbo.CF_App_ProcessInstance b where a.FId = b.fid)


if not exists (select 1
            from  sysobjects
           where  id = object_id('dbo._App_ProcessRecord_BZGD')
            and   type = 'U')
begin
  select * into _App_ProcessRecord_BZGD from dbCenter.dbo.CF_App_ProcessRecord where 1=2
end
else
begin
  delete _App_ProcessRecord_BZGD
end

  insert into _App_ProcessRecord_BZGD
              (fid,FTime,FIsDeleted,FProcessInstanceID,FLinkId,FSubFlowId,FMeasure,Fresult,FManageDeptId,
			  FReportTime,FDefineDay,FRoleId,FLevel,FOrder,FRoleDesc,FTypeId,FIsQuali,FIsPrint )
	   select newid(),a.FSubmitDate,0,a.fid,a.FLinkId,a.FSubFlowId,0,null,isnull(FManageDeptId,'51'),
	          a.FReportDate,1,'8801',1,1,'管理部门审核',1,2,1
	     from _App_ProcessInstance_BZGD a
	--	where  not exists(select 1 from dbCenter.dbo.CF_App_ProcessRecord b where a.FId = b.FProcessInstanceID)

  insert into dbCenter.dbo.CF_App_ProcessRecord
              (fid,FTime,FIsDeleted,FProcessInstanceID,FLinkId,FSubFlowId,FMeasure,Fresult,FManageDeptId,
			  FReportTime,FDefineDay,FRoleId,FLevel,FOrder,FRoleDesc,FTypeId,FIsQuali,FIsPrint )
       select fid,FTime,FIsDeleted,FProcessInstanceID,FLinkId,FSubFlowId,FMeasure,Fresult,FManageDeptId,
			  FReportTime,FDefineDay,FRoleId,FLevel,FOrder,FRoleDesc,FTypeId,FIsQuali,FIsPrint
		 from _App_ProcessRecord_BZGD a
	    where not exists(select 1 from dbCenter.dbo.CF_App_ProcessRecord b where a.FId = b.fid)