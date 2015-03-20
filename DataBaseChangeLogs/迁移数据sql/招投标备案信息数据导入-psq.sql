--1、导入招标项目信息
--先处理jihuajungong_time,jihuajungong_time，first_time，lasttime，tongyi_time，tianbao_time,'0000-00-00'的数据处理为'1900-01-01'
update bjxx_psq set jihuakaigong_time= '1900-01-01' where (jihuakaigong_time = '0000-00-00' or jihuakaigong_time is null or jihuakaigong_time = 'NULL')
update bjxx_psq set jihuajungong_time= '1900-01-01' where (jihuajungong_time = '0000-00-00' or jihuajungong_time is null or jihuajungong_time = 'NULL')
update bjxx_psq set tianbao_time= '1900-01-01' where (tianbao_time = '0000-00-00'  or tianbao_time is null or tianbao_time = 'NULL')
update bjxx_psq set first_time= '1900-01-01' where (first_time = '0000-00-00' or first_time is null or first_time = 'NULL')
update bjxx_psq set lasttime= '1900-01-01' where (lasttime = '0000-00-00' or lasttime is null or lasttime = 'NULL')
update bjxx_psq set tongyi_time= '1900-01-01' where (tongyi_time = '0000-00-00' or tongyi_time is null or tongyi_time = 'NULL')
--处理金额为'NULL'的数据为0
update bjxx_psq set touzizhong_e= 0 where  touzizhong_e= 'NULL'
update bjxx_psq set zhengfutouzi= 0 where  zhengfutouzi= 'NULL'
update bjxx_psq set zichouzijing= 0 where  zichouzijing= 'NULL'
update bjxx_psq set waizi= 0 where  waizi= 'NULL'
update bjxx_psq set jianzhumianji= 0 where  jianzhumianji= 'NULL'
--导入施工许可证相关项目表
INSERT INTO dbCenter.dbo.TC_Prj_Info
SELECT
newid() FId         --项目id
,a.jianshedawei  JSDW         --建设单位名称
,''  JSDWDM         --建设单位组织机构代码(为空)
,''  JSDWDZ         --建设单位地址  (为空)
,a.lianxiren   Contacts         --建设单位联系人
,a.tel_no Mobile         --建设单位联系电话
,isnull(a.project_name,'')+'-招标备案导入' ProjectName         --项目名称加上'[招标备案导入项目]'进行项目区分
,'51' as Province --项目属地-省
,shenpidanweishudi_shi as City --项目属地-市州   (导入后根据地区名称统一处理)
,shenpidanweishudi_xian as County --项目属地-区县 (导入后根据县区名称统一处理)
,'' as ProjectType         --为空项目类别:2000101--房屋建筑;2000102--市政基础;2000103--其他
,isnull(a.place,'')+'-ps'+a.baojian_no Address         --项目地址 把原来的备案编号存入以区分项目
,a.lixianwenhao ProjectNumber         --立项文号
,NULL ProjectLevel         --立项级别
,NULL ProjectTime         --项目时间
,NEWID() ProjectNo         --项目编号  之前没有流程数据，项目编号new一个guid
,NULL IsForeign         --是否涉外
,NULL JSYDXKZ         --建设用地许可证号
,NULL JSGCXKZ         --工程规划许可证号
,a.jianzhumianji Area         --建设总面积（m2）
,NULL Investment         --投资规模(万元)
,NULL ConstrType         --建设性质：2000501--新建；2000502--改建 --给水	200010201 --排水	200010202  --燃气	200010203  --热力	200010204 --道路	200010205--桥隧	200010206
--风景园林	200010207 --环境园林	200010208  --公共交通	200010209 --其他	200010210
,null as ProjectUse         --工程用途：200010201--给水；200010205--道路
,null as  StartDate         --实际开工日期
,null as  EndDate         --实际竣工日期
,a.tianbao_time RegisterTime         --记录登记时间
,NULL  ConstrBasis         --建设依据
,NULL ConstrContent         --建设内容
,null as  FJSDWID --建设单位外键
,null as  AddressDept    --地址外键
,a.guimo ConstrScale--建设规模
,NULL LandType  --用地性质 --新建	2000501  --改建	2000502 --扩建	2000503 --重建	2000504  --迁建	2000505--恢复	2000506--其他	2000507
,a.fa_ren JSDWFR         --建设单位法人
FROM  bjxx_psq as A

--把在招投标备案表中存在的项目但在基本项目表中不存在的，插入到基本项目信息表中  (一共45条)
INSERT INTO dbCenter.dbo.TC_Prj_Info
SELECT
newid() FId         --项目id
,''  JSDW         --建设单位名称
,''  JSDWDM         --建设单位组织机构代码(为空)
,''  JSDWDZ         --建设单位地址  (为空)
,null   Contacts         --建设单位联系人
,null Mobile         --建设单位联系电话
,isnull(a.project_name,'')+'-招标备案导入' ProjectName         --项目名称加上'[招标备案导入项目]'进行项目区分
,'51' as Province --项目属地-省
,null as City --项目属地-市州   (导入后根据地区名称统一处理)
,null as County --项目属地-区县 (导入后根据县区名称统一处理)
,'' as ProjectType         --为空项目类别:2000101--房屋建筑;2000102--市政基础;2000103--其他
,'招标文件备案中无法关联的项目'+'-ps'+a.baojian_no as  Address         --项目地址 把原来的备案编号存入以区分项目
,null ProjectNumber         --立项文号
,NULL ProjectLevel         --立项级别
,NULL ProjectTime         --项目时间
,NEWID() ProjectNo         --项目编号  之前没有流程数据，项目编号new一个guid
,NULL IsForeign         --是否涉外
,NULL JSYDXKZ         --建设用地许可证号
,NULL JSGCXKZ         --工程规划许可证号
,null Area         --建设总面积（m2）
,NULL Investment         --投资规模(万元)
,NULL ConstrType         --建设性质：2000501--新建；2000502--改建 --给水	200010201 --排水	200010202  --燃气	200010203  --热力	200010204 --道路	200010205--桥隧	200010206
--风景园林	200010207 --环境园林	200010208  --公共交通	200010209 --其他	200010210
,null as ProjectUse         --工程用途：200010201--给水；200010205--道路
,null as  StartDate         --实际开工日期
,null as  EndDate         --实际竣工日期
,null RegisterTime         --记录登记时间
,NULL  ConstrBasis         --建设依据
,NULL ConstrContent         --建设内容
,null as  FJSDWID --建设单位外键
,null as  AddressDept    --地址外键
,null ConstrScale--建设规模
,NULL LandType  --用地性质 --新建	2000501  --改建	2000502 --扩建	2000503 --重建	2000504  --迁建	2000505--恢复	2000506--其他	2000507
,null JSDWFR         --建设单位法人
from  zhaobiaobeian_psq a
where a.baojian_no 
not in (select distinct   SUBSTRING(Address,CHARINDEX('-',Address)+3,13) from TC_Prj_Info
where Address like '%-ps%'
and  ProjectName like '%-招标备案导入')



--把在中标结果备案表中存在的项目但在基本项目表中不存在的，插入到基本项目信息表中  (一共1条)
INSERT INTO dbCenter.dbo.TC_Prj_Info
SELECT
newid() FId         --项目id
,''  JSDW         --建设单位名称
,''  JSDWDM         --建设单位组织机构代码(为空)
,''  JSDWDZ         --建设单位地址  (为空)
,null   Contacts         --建设单位联系人
,null Mobile         --建设单位联系电话
,isnull(a.G_project_name,'')+'-招标备案导入' ProjectName         --项目名称加上'[招标备案导入项目]'进行项目区分
,'51' as Province --项目属地-省
,null as City --项目属地-市州   (导入后根据地区名称统一处理)
,null as County --项目属地-区县 (导入后根据县区名称统一处理)
,'' as ProjectType         --为空项目类别:2000101--房屋建筑;2000102--市政基础;2000103--其他
,'招标文件备案中无法关联的项目'+'-ps'+a.baojian_no as  Address         --项目地址 把原来的备案编号存入以区分项目
,null ProjectNumber         --立项文号
,NULL ProjectLevel         --立项级别
,NULL ProjectTime         --项目时间
,NEWID() ProjectNo         --项目编号  之前没有流程数据，项目编号new一个guid
,NULL IsForeign         --是否涉外
,NULL JSYDXKZ         --建设用地许可证号
,NULL JSGCXKZ         --工程规划许可证号
,null Area         --建设总面积（m2）
,NULL Investment         --投资规模(万元)
,NULL ConstrType         --建设性质：2000501--新建；2000502--改建 --给水	200010201 --排水	200010202  --燃气	200010203  --热力	200010204 --道路	200010205--桥隧	200010206
--风景园林	200010207 --环境园林	200010208  --公共交通	200010209 --其他	200010210
,null as ProjectUse         --工程用途：200010201--给水；200010205--道路
,null as  StartDate         --实际开工日期
,null as  EndDate         --实际竣工日期
,null RegisterTime         --记录登记时间
,NULL  ConstrBasis         --建设依据
,NULL ConstrContent         --建设内容
,null as  FJSDWID --建设单位外键
,null as  AddressDept    --地址外键
,null ConstrScale--建设规模
,NULL LandType  --用地性质 --新建	2000501  --改建	2000502 --扩建	2000503 --重建	2000504  --迁建	2000505--恢复	2000506--其他	2000507
,null JSDWFR         --建设单位法人
from  zhongbiaobeian_psq a
where a.baojian_no not in 
(
  select  SUBSTRING(b.Address,CHARINDEX('-',Address)+3,13) from TC_Prj_Info b
  where  b.Address like '%-ps%'
  and b.ProjectName   like '%-招标备案导入'
)
--2、导入单体工程项目信息
INSERT INTO dbCenter.dbo.TC_PrjItem_Info
SELECT 
NEWID() FId         --单体工程id 新生成一个id作为主键
,a.FId FPrjId         --关联TC_Prj_Info.FId
,a.ProjectName ProjectName         --项目名称
,a.Address as  Address         --项目地址
,NULL Area         --建设总面积
,NULL Investment         --投资规模(万元)
,a.JSDW JSDW         --建设单位名称
,a.JSDWFR LegalPerson         --法人代表
,a.JSDWDM JSDWDM         --建设单位组织机构代码
,a.ProjectName PrjItemName         --工程名称
,NULL as  PrjItemType     --工程类别          --工程类别：2000101--房屋建筑;2000102--市政基础;2000103--  其他  --砖木结构	509001--砖混结构	509002--钢筋混凝土结构	509003--钢结构	509004
--框架结构	509005--剪力墙结构	509006--框架-剪力墙结构	509007--框-筒结构	509008--其他	509010
,NULL ConstrType         --结构类型：509001；509002；509005
,null Cost         --工程造价（万元）
,NULL Scale         --工程规模
,NULL PrjItemDesc         --工程描述
,a.FJSDWID as  FJSDWID         --建设单位外键
,a.AddressDept AddressDept         --地址外键
,a.ConstrScale ConstrScale--建设规模
FROM  TC_Prj_Info A
where a.ProjectName  like '%-招标备案导入'

--------------------------------------------------------------------------------------------------------------------------------
--3、导入招标文件备案信息
USE [dbCenter]
GO
--修改招标文件表中的标段名称和项目名称长度
alter table TC_ZBWJ_Record alter column bdbm varchar(500);
alter table TC_ZBWJ_Record alter column ProjectName varchar(500);
go

INSERT INTO [dbo].[TC_ZBWJ_Record]
           ([FId]           ,[FAppId]           ,[FPrjId]           ,[BDId]           ,[CS]           ,[BDBM]           ,[BDMC]
           ,[ProjectName]           ,[FBFS]           ,[ZGYSFS]           ,[ZBZZXS]           ,[DLJG]
           ,[BZR]           ,[SHR]           ,[SDR]           ,[BATime]           ,[FResult]           ,[DLJGId])
select  --a.*
newid() as fid,--招标备案主键
newid() as Fappid,--业务主键
case b.FId when   null  then newid()  else b.FId end as FPrijid,--项目编号
case b.FId when  null  then newid() else  b.FId end as BDid, --标段ID    因目前无标段信息，暂时以项目编号作为标段编号
null as CS,--次数
null as BDBM,--标段编码
a.project_name as BDMC,--标段名称
a.project_name as ProjectName,--项目名称
case a.fabao_way when '公开招标' then '11220902' else '11220901' end as FBFS,--发包方式
case a.zigeshencha when '资格后审' then '11220702' else  '11220701' end as ZGYSFS,--资格预审方式
a.zuzixinshi as ZBZZXS,--招标组织形式
a.dailijigou as DLJG,--代理机构
null as BZR,--编制人
null as SHR,--审核人
null as SDR,--审定人
case a.zhaobiaobei_time when 'NULL' then null else zhaobiaobei_time end as BAtime,--备案时间
null as Fresult,--备案结果
null as DLJGID--代理机构编号
from  zhaobiaobeian_psq as a left join TC_Prj_Info b
on a.baojian_no = SUBSTRING(b.Address,CHARINDEX('-',Address)+3,13)
and b.Address like '%-ps%'
and b.ProjectName   like '%-招标备案导入'
--------------------------------------------------------------------------------------------------------------------------------
--4、导入招标备案业务主表  根据招标备案结果表向业务主表中补充数据.
INSERT INTO dbCenter.dbo.CF_App_List
SELECT  --*
 a.FAppId FId    --业务编码
,newid()  FBaseinfoId    --企业id  (暂时无法找到企业信息,临时new一个guid)
,a.FPrjId FPrjId    --项目编码
,CASE WHEN ISNULL(a.BATime,'')<>'' THEN CONVERT(CHAR(4),YEAR(a.BATime))+'年 ' ELSE '' END +'招投标备案' FName    --业务名称
,11232 FManageTypeId    --业务编码   11232是招标文件备案
,a.BATime FwriteDate    --写入时间
,a.BATime FReportDate    --上报时间
,NULL FIsSign    --是否签字
,0 FState    --业务状态
,NULL FResult    --审批结论
,YEAR(a.BATime) FYear    --年度
,MONTH(a.BATime) FMonth    --月份
,a.FPrjId FLinkId    --外键工程编码
,b.JSDW FBaseName    --建设单位名称
,NULL FUpDeptId    --上报部门地区编码
,NULL FRemark    --暂不考虑
,NULL FIsCheck    --暂不考虑
,NULL FCount    --暂不考虑
,a.BATime FTime    --最后更新时间,业务暂时没有上报，没走流程
,NULL FIsDeleted    --是否删除
,a.BATime FCreateTime    --创建时间
,1 FReportCount    --暂不考虑
,NULL FToBaseinfoId    --暂不考虑
,NULL FAppDate    --暂不考虑
,NULL FLinkAppId    --暂不考虑
,NULL FBarCode    --暂不考虑
,NULL FCreateUser    --暂不考虑
,NULL FgfTime    --暂不考虑
from  [TC_ZBWJ_Record] a left join TC_Prj_Info b
on a.FPrjId = b.FId
where  b.Address like '%-ps%'
and b.ProjectName   like '%-招标备案导入'
--------------------------------------------------------------------------------------------------------------------------
--5、导入中标结果信息
--修改中标文件备案文件表中的标段名称和项目名称长度
alter table TC_ZBJG_Record alter column projectname varchar(500);
alter table TC_ZBJG_Record alter column BDMC varchar(500);
alter table TC_ZBJG_Record alter column zhaobr varchar(500);
alter table TC_ZBJG_Record alter column zhongbr varchar(500);
--处理日期数据0000-00-00为'1900-01-01'
update zhongbiaobeian_psq set  kaibiao_date = '1900-01-01' where kaibiao_date = '0000-00-00' or kaibiao_date is null or kaibiao_date = 'NULL'
go
--------------------------------------------------------------------------------------------------------------------------
USE [dbCenter]
GO
--------------------------------------------------------------------------------------------------------------------------
INSERT INTO [dbo].[TC_ZBJG_Record]
           ([FId]           ,[FPrjId]           ,[FAppId]           ,[BDId]           ,[CS]
           ,[GCBM]           ,[BDBM]           ,[ZTBBM]           ,[ProjectName]           ,[BDMC]
           ,[ZBDLDW]           ,[ZHAOBR]           ,[KBSJ]           ,[QYBM]           ,[ZHONGBR]
           ,[KGRQ]           ,[JGRQ]           ,[HTQDDD]           ,[TZFFSJ]           ,[ZBJG]
           ,[ZBYY]           ,[FResult]           ,[QYId])
 select  --a.*
 newid() Fid,--主键
 b.FId  Fprjid,--项目编号
 newid() Fappid,--业务主键（new一个guid）
 B.FId BDid,--标段ID
 null CS,--次数
 b.FId GCBM,--以项目编号作为工程编码
 b.FId BDBM,--以项目编号作为标段编码
 null ZTBBM,--招投标编码
 a.G_project_name ProjectName,--项目名称
 a.G_project_name ProjectName,--标段名称
 null ZBDLDW,--代理机构
 b.JSDW ZHAOBR,--招标人
 CONVERT(DATE,a.kaibiao_date)   KBSJ,--开标时间
 b.FJSDWID QYBM,--企业编码
 a.G_zhongbiaoren ZHONGBR,--中标人
 CONVERT(DATE,b.StartDate) KGRQ,--开工日期
 CONVERT(DATE,b.EndDate)   JGRQ,--竣工日期
 null HTQDDD,--合同签订地点
 null tzffsj,--通知发放时间
 null ZBJG,--中标结果
 null ZBYY,--中标原因
 a.zhong_leader Fresult,--备案结果
 b.FJSDWID QYid--企业编号
 from  zhongbiaobeian_psq a left join TC_Prj_Info b
on a.baojian_no = SUBSTRING(b.Address,CHARINDEX('-',Address)+3,13)
and b.Address like '%-ps%'
and b.ProjectName   like '%-招标备案导入'

--------------------------------------------------------------------------------------------------------------------------
--6、导入中标结果业务主表  根据[TC_ZBJG_Record]进行反向导入
--导入中标结果业务主表
INSERT INTO dbCenter.dbo.CF_App_List
SELECT 
 a.FAppId as  FId,    --业务编码
 newid() FBaseinfoId, --企业id 中标结果备案表中没有企业编号，新生成一个GUID
 a.FPrjId  FPrjId,--项目编码     中标结果备案表中没有项目编号，新生成一个GUID
 CONVERT(char(4),year(convert(datetime,a.KBSJ)))+'年 '+'中标结果备案' FName    --业务名称
,11235 FManageTypeId    --业务编码(中标结果备案)  业务编码是11235
,a.KBSJ   FwriteDate    --写入时间
,a.KBSJ FReportDate    --上报时间 (暂时以同意时间为准)
,NULL FIsSign    --是否签字
,0 FState    --业务状态
,NULL FResult    --审批结论
,YEAR(convert(datetime,isnull(a.KBSJ,''))) FYear    --年度
,MONTH(convert(datetime,isnull(a.KBSJ,''))) FMonth    --月份
,SUBSTRING(b.Address,CHARINDEX('-',Address)+3,13) FLinkId    --外键工程编码(暂时以报建id为工程外键id）
,a.ZHAOBR FBaseName    --建设单位名称
,NULL FUpDeptId    --上报部门地区编码
,NULL FRemark    --暂不考虑
,NULL FIsCheck    --暂不考虑
,NULL FCount    --暂不考虑
,a.KBSJ FTime    --最后更新时间,业务暂时没有上报，没走流程
,NULL FIsDeleted    --是否删除
, convert(datetime,isnull(a.KBSJ,'')) FCreateTime    --创建时间
,1 FReportCount    --暂不考虑
,NULL FToBaseinfoId    --暂不考虑
,NULL FAppDate    --暂不考虑
,NULL FLinkAppId    --暂不考虑
,NULL FBarCode    --暂不考虑
,NULL FCreateUser    --暂不考虑
,NULL FgfTime    --暂不考虑
from  TC_ZBJG_Record  a 
left join TC_Prj_Info b
on a.FPrjId = b.FId
and b.Address like '%-ps%'
and b.ProjectName   like '%-招标备案导入'
and a.FAppId not in
(select fid from CF_App_List)




--select  *  from  TC_ZBJG_Record  where FAppId = '2a3b3c2e-b2bd-4fb6-8929-00f6fc3da8de'
--select  *  from  CF_Sys_ManageType  where  fname like '%中标结果%'   --查询业务类型编码
--------------------------------------------------------------------------------------------------------------------------







