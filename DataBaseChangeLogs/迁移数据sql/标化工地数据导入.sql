--׼�����������
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
   and C06.actionid in (308,309,310);--δ�ϱ� --���Ӽ� --�����


--����껯������ص���Ŀ��Ϣ
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
	 b.fqbm     FId         --��Ŀid
	,isnull(b.JSDW,'')    JSDW         --���赥λ�����赥λ��֯���������
	,null       JSDWDM         --���赥λ��֯��������
	,c.txdz     JSDWDZ         --���赥λ��ַ
	,c.LXR   Contacts         --���赥λ��ϵ��
	,c.LXDH Mobile         --���赥λ��ϵ�绰
	,b.gcmc ProjectName         --��Ŀ����
	,isnull(b.s,'51') Province  --��Ŀ����-ʡ
	,b.SQ City                  --��Ŀ����-����
	,b.qx County         --��Ŀ����-����
	,'2000103'  ProjectType         --��Ŀ���:2000101--���ݽ���;2000102--��������;2000103--����
	,b.JSDD  Address         --��Ŀ��ַ
	,null ProjectNumber         --�����ĺ�
	,'11220401' as  ProjectLevel  --�����
	,B.kgrq ProjectTime         --��Ŀʱ��
	,null ProjectNo         --��Ŀ���
	,b.JW IsForeign         --�Ƿ�����
	,NULL JSYDXKZ         --�����õ����֤��
	,NULL JSGCXKZ         --���̹滮���֤��
	,null Area            --�����������m2��
	,b.TZGM Investment         --Ͷ�ʹ�ģ(��Ԫ)
	,'30503' as ConstrType     --�������ʣ�2000501--�½���2000502--�Ľ�
	--��ˮ	200010201
	--��ˮ	200010202
	--ȼ��	200010203
	--����	200010204
	--��·	200010205
	--����	200010206
	--�羰԰��	200010207
	--����԰��	200010208
	--������ͨ	200010209
	--����	200010210
	,null ProjectUse             --������;��200010201--��ˮ��200010205--��·
	,b.KGRQ StartDate            --ʵ�ʿ�������
	,b.JGYSRQ EndDate            --ʵ�ʿ�������
	,c.YBSJ RegisterTime         --��¼�Ǽ�ʱ��
	,NULL  ConstrBasis           --��������
	,NULL ConstrContent          --��������
	,c.SBDWID FJSDWID            --���赥λ���
	,null AddressDept         --��ַ���
	,NULL ConstrScale--�����ģ
	,NULL LandType  --�õ�����
	--�½�	2000501
	--�Ľ�	2000502
	--����	2000503
	--�ؽ�	2000504
	--Ǩ��	2000505
	--�ָ�	2000506
	--����	2000507
	,null JSDWFR         --���赥λ����
  FROM (select max(bhid) as bhid,fqbm from   JKCWFDB_WORK_NJS.DBO.BH_YWINFO group by fqbm) A,
       JKCWFDB_WORK_NJS.DBO.BH_GCINFO B,
	   JKCWFDB_WORK_NJS.DBO.bh_ybsqb  c
 WHERE A.fqbm = B.FQBM AND
       a.bhid = c.bhid and
	   not exists(select 1 from dbcenter.dbo.TC_Prj_Info bb where b.fqbm = bb.fid) and
	   A.BHID in (select  processkeyvalue from _BZGD_s);
   
-----------------------------------------------------------------------------------------------------------------
--����껯���ر���������Ϣ

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
			a.BHID  FId,                     --����
			a.BHID  FAppId,                  --ҵ�����
			b.fqbm  FPrjId,                  --��Ŀ����
			newid()   FPrjItemId,            --������� 
			b.GCMC ProjectName,              --��Ŀ����
			b.s,SQ,QX,                       --ʡ���С���
			b.JW IsForeign,                  --
			b.JSDD  Address,                 --�����ַ
			b.JZLX         ,                 --��������
			b.DTJZMJ Area,                   --�������
			b.TZGM Investment,
			b.ISBZ WPZSGD,                   --��־�Խ���
			null SGDWId,                     --ʩ����λ,
			b.SGDW as sgdw,null as sgdwdz,
			null sgdwdh,
			null sgdwfr,
			null as jsdwid,                  --���赥λ
			b.JSDW JSDW,                     
			null as jsdwdz,
			null as jsdwdh,
			null as jldwid,                  --����λ��
			b.JLDW,
			null as jldwdz,null as jldwdh,
			b.SGXKZBH,                      --ʩ�����֤���
			b.QYZZZSBH   as sgdwzs,
			b.AQSCXKZBH  as SGDWAQSCXKZ,
			b.AQSCYJPJSCBH as SGSCYJPJSC,
			b.XMJLAQSCNLKHZSBH as XMJLAQSCLLKHZS,
			b.SCNLKHZSBH as AQYAQSCLLKH,    --��ȫԱ��ȫ������������֤����,
			d.KGTJSCQK,                     --��������������
			b.MQGCJD,null as GCZJ,b.JGLX,        --Ŀǰ���ȣ�������ۡ��������       
			b.xmjl, b.GCAQPJDJ,KGRQ,JGYSRQ,  --��Ŀ����
			b.ZDZLSG,b.ZDAQSG,
			c.SBDWID,c.YBDW,c.YBSJ,c.lxr,c.LXDH,c.TXDZ,c.YZBM,  --�걨��λ
			null as JDBM,d.LXR,d.LXDH,            --�ල��λ
			d.GZJH,
			d.GCJLDWYJ,              
			null as JLDWMC,
			d.JLTXYJSJ,            --����λ
			null as JSDWYJ,d.JSDWMC,d.JSTXYJSJ,   --���赥λ
			e.ZGBMSCYJ,e.ZGBMSCBM,e.ZGBMSCRQ, --�����������ܲ��Ż��й��������
			c.bz Remark                      --��ע
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
			a.BHID  FId,                     --����
			a.BHID  FAppId,                  --ҵ�����
			b.JSDW JSDW,                     
			b.s,SQ,QX,                       --ʡ���С���
			b.sd,                            --����
			null as jsdwdz,
			null as jsdwxz,null,null,null,null,
			null as jsfzr,null as jsfzrzc,null as jsfzrdh,
			b.fqbm  FPrjId,                  --��Ŀ����
			null   FPrjItemId,               --������� 
			b.GCMC ProjectName,              --��Ŀ����
			null as prjitemname,null prjitemtype,
			b.KGRQ as ProjectTime,
			c.YBSJ,
			b.s,SQ,QX,                       --ʡ���С���
			b.sd,                            --����
			b.JSDD  Address,                 --�����ַ
			b.TZGM,                          --Ͷ�ʹ�ģ
			b.JZLX  as ConstrType,            --��������
			null as price,null,
			b.KGRQ,b.JGYSRQ,                 --�������������ڣ�
			e.ZGBMSCJL,
			c.bz Remark                      --��ע
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
	 a.bhid FId    --ҵ�����
	,c.SBDWID FBaseinfoId    --��ҵid
	,B.fqbm FPrjId    --��Ŀ����
	,CONVERT(VARCHAR(20),YEAR(c.YBSJ)) + '�� ��׼�����ر���' FName    --ҵ������
	,11236  FManageTypeId    --ҵ�����
	,c.YBSJ FwriteDate    --д��ʱ��
	,c.YBSJ FReportDate    --�ϱ�ʱ��
	,NULL FIsSign    --�Ƿ�ǩ��
	,0 FState        --ҵ��״̬
	,NULL FResult    --��������
	,YEAR(c.YBSJ) FYear    --���
	,MONTH(c.YBSJ) FMonth    --�·�
	,B.fqbm   FLinkId    --������̱���
	,b.JSDW   FBaseName    --���赥λ����
	,b.sd  FUpDeptId    --�ϱ����ŵ�������
	,NULL FRemark    --�ݲ�����
	,NULL FIsCheck    --�ݲ�����
	,NULL FCount    --�ݲ�����
	,c.YBSJ FTime    --������ʱ��,ҵ����ʱû���ϱ���û������
	,0 FIsDeleted    --�Ƿ�ɾ��
	,c.YBSJ FCreateTime    --����ʱ��
	,1 FReportCount    --�ݲ�����
	,NULL FToBaseinfoId    --�ݲ�����
	,NULL FAppDate    --�ݲ�����
	,NULL FLinkAppId    --�ݲ�����
	,NULL FBarCode    --�ݲ�����
	,NULL FCreateUser    --�ݲ�����
	,NULL FgfTime    --�ݲ�����
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


  --����ҵ������
	INSERT INTO dbCenter.dbo.CF_App_List
	SELECT * FROM _App_List_BZHD a
	  where not exists (select 1 from dbCenter.dbo.CF_App_List f where a.fid = f.fid) 


if not exists (select 1
            from  sysobjects
           where  id = object_id('dbo._App_ProcessInstance_BZGD')
            and   type = 'U')
begin 
    SELECT * into _App_ProcessInstance_BZGD from dbCenter.dbo.CF_App_ProcessInstance where 1=2  --ֻȡ��ṹ
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
	          a.FReportDate,1,'8801',1,1,'���������',1,2,1
	     from _App_ProcessInstance_BZGD a
	--	where  not exists(select 1 from dbCenter.dbo.CF_App_ProcessRecord b where a.FId = b.FProcessInstanceID)

  insert into dbCenter.dbo.CF_App_ProcessRecord
              (fid,FTime,FIsDeleted,FProcessInstanceID,FLinkId,FSubFlowId,FMeasure,Fresult,FManageDeptId,
			  FReportTime,FDefineDay,FRoleId,FLevel,FOrder,FRoleDesc,FTypeId,FIsQuali,FIsPrint )
       select fid,FTime,FIsDeleted,FProcessInstanceID,FLinkId,FSubFlowId,FMeasure,Fresult,FManageDeptId,
			  FReportTime,FDefineDay,FRoleId,FLevel,FOrder,FRoleDesc,FTypeId,FIsQuali,FIsPrint
		 from _App_ProcessRecord_BZGD a
	    where not exists(select 1 from dbCenter.dbo.CF_App_ProcessRecord b where a.FId = b.fid)