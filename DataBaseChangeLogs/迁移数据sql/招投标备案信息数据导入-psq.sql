--1�������б���Ŀ��Ϣ
--�ȴ���jihuajungong_time,jihuajungong_time��first_time��lasttime��tongyi_time��tianbao_time,'0000-00-00'�����ݴ���Ϊ'1900-01-01'
update bjxx_psq set jihuakaigong_time= '1900-01-01' where (jihuakaigong_time = '0000-00-00' or jihuakaigong_time is null or jihuakaigong_time = 'NULL')
update bjxx_psq set jihuajungong_time= '1900-01-01' where (jihuajungong_time = '0000-00-00' or jihuajungong_time is null or jihuajungong_time = 'NULL')
update bjxx_psq set tianbao_time= '1900-01-01' where (tianbao_time = '0000-00-00'  or tianbao_time is null or tianbao_time = 'NULL')
update bjxx_psq set first_time= '1900-01-01' where (first_time = '0000-00-00' or first_time is null or first_time = 'NULL')
update bjxx_psq set lasttime= '1900-01-01' where (lasttime = '0000-00-00' or lasttime is null or lasttime = 'NULL')
update bjxx_psq set tongyi_time= '1900-01-01' where (tongyi_time = '0000-00-00' or tongyi_time is null or tongyi_time = 'NULL')
--������Ϊ'NULL'������Ϊ0
update bjxx_psq set touzizhong_e= 0 where  touzizhong_e= 'NULL'
update bjxx_psq set zhengfutouzi= 0 where  zhengfutouzi= 'NULL'
update bjxx_psq set zichouzijing= 0 where  zichouzijing= 'NULL'
update bjxx_psq set waizi= 0 where  waizi= 'NULL'
update bjxx_psq set jianzhumianji= 0 where  jianzhumianji= 'NULL'
--����ʩ�����֤�����Ŀ��
INSERT INTO dbCenter.dbo.TC_Prj_Info
SELECT
newid() FId         --��Ŀid
,a.jianshedawei  JSDW         --���赥λ����
,''  JSDWDM         --���赥λ��֯��������(Ϊ��)
,''  JSDWDZ         --���赥λ��ַ  (Ϊ��)
,a.lianxiren   Contacts         --���赥λ��ϵ��
,a.tel_no Mobile         --���赥λ��ϵ�绰
,isnull(a.project_name,'')+'-�б걸������' ProjectName         --��Ŀ���Ƽ���'[�б걸��������Ŀ]'������Ŀ����
,'51' as Province --��Ŀ����-ʡ
,shenpidanweishudi_shi as City --��Ŀ����-����   (�������ݵ�������ͳһ����)
,shenpidanweishudi_xian as County --��Ŀ����-���� (����������������ͳһ����)
,'' as ProjectType         --Ϊ����Ŀ���:2000101--���ݽ���;2000102--��������;2000103--����
,isnull(a.place,'')+'-ps'+a.baojian_no Address         --��Ŀ��ַ ��ԭ���ı�����Ŵ�����������Ŀ
,a.lixianwenhao ProjectNumber         --�����ĺ�
,NULL ProjectLevel         --�����
,NULL ProjectTime         --��Ŀʱ��
,NEWID() ProjectNo         --��Ŀ���  ֮ǰû���������ݣ���Ŀ���newһ��guid
,NULL IsForeign         --�Ƿ�����
,NULL JSYDXKZ         --�����õ����֤��
,NULL JSGCXKZ         --���̹滮���֤��
,a.jianzhumianji Area         --�����������m2��
,NULL Investment         --Ͷ�ʹ�ģ(��Ԫ)
,NULL ConstrType         --�������ʣ�2000501--�½���2000502--�Ľ� --��ˮ	200010201 --��ˮ	200010202  --ȼ��	200010203  --����	200010204 --��·	200010205--����	200010206
--�羰԰��	200010207 --����԰��	200010208  --������ͨ	200010209 --����	200010210
,null as ProjectUse         --������;��200010201--��ˮ��200010205--��·
,null as  StartDate         --ʵ�ʿ�������
,null as  EndDate         --ʵ�ʿ�������
,a.tianbao_time RegisterTime         --��¼�Ǽ�ʱ��
,NULL  ConstrBasis         --��������
,NULL ConstrContent         --��������
,null as  FJSDWID --���赥λ���
,null as  AddressDept    --��ַ���
,a.guimo ConstrScale--�����ģ
,NULL LandType  --�õ����� --�½�	2000501  --�Ľ�	2000502 --����	2000503 --�ؽ�	2000504  --Ǩ��	2000505--�ָ�	2000506--����	2000507
,a.fa_ren JSDWFR         --���赥λ����
FROM  bjxx_psq as A

--������Ͷ�걸�����д��ڵ���Ŀ���ڻ�����Ŀ���в����ڵģ����뵽������Ŀ��Ϣ����  (һ��45��)
INSERT INTO dbCenter.dbo.TC_Prj_Info
SELECT
newid() FId         --��Ŀid
,''  JSDW         --���赥λ����
,''  JSDWDM         --���赥λ��֯��������(Ϊ��)
,''  JSDWDZ         --���赥λ��ַ  (Ϊ��)
,null   Contacts         --���赥λ��ϵ��
,null Mobile         --���赥λ��ϵ�绰
,isnull(a.project_name,'')+'-�б걸������' ProjectName         --��Ŀ���Ƽ���'[�б걸��������Ŀ]'������Ŀ����
,'51' as Province --��Ŀ����-ʡ
,null as City --��Ŀ����-����   (�������ݵ�������ͳһ����)
,null as County --��Ŀ����-���� (����������������ͳһ����)
,'' as ProjectType         --Ϊ����Ŀ���:2000101--���ݽ���;2000102--��������;2000103--����
,'�б��ļ��������޷���������Ŀ'+'-ps'+a.baojian_no as  Address         --��Ŀ��ַ ��ԭ���ı�����Ŵ�����������Ŀ
,null ProjectNumber         --�����ĺ�
,NULL ProjectLevel         --�����
,NULL ProjectTime         --��Ŀʱ��
,NEWID() ProjectNo         --��Ŀ���  ֮ǰû���������ݣ���Ŀ���newһ��guid
,NULL IsForeign         --�Ƿ�����
,NULL JSYDXKZ         --�����õ����֤��
,NULL JSGCXKZ         --���̹滮���֤��
,null Area         --�����������m2��
,NULL Investment         --Ͷ�ʹ�ģ(��Ԫ)
,NULL ConstrType         --�������ʣ�2000501--�½���2000502--�Ľ� --��ˮ	200010201 --��ˮ	200010202  --ȼ��	200010203  --����	200010204 --��·	200010205--����	200010206
--�羰԰��	200010207 --����԰��	200010208  --������ͨ	200010209 --����	200010210
,null as ProjectUse         --������;��200010201--��ˮ��200010205--��·
,null as  StartDate         --ʵ�ʿ�������
,null as  EndDate         --ʵ�ʿ�������
,null RegisterTime         --��¼�Ǽ�ʱ��
,NULL  ConstrBasis         --��������
,NULL ConstrContent         --��������
,null as  FJSDWID --���赥λ���
,null as  AddressDept    --��ַ���
,null ConstrScale--�����ģ
,NULL LandType  --�õ����� --�½�	2000501  --�Ľ�	2000502 --����	2000503 --�ؽ�	2000504  --Ǩ��	2000505--�ָ�	2000506--����	2000507
,null JSDWFR         --���赥λ����
from  zhaobiaobeian_psq a
where a.baojian_no 
not in (select distinct   SUBSTRING(Address,CHARINDEX('-',Address)+3,13) from TC_Prj_Info
where Address like '%-ps%'
and  ProjectName like '%-�б걸������')



--�����б����������д��ڵ���Ŀ���ڻ�����Ŀ���в����ڵģ����뵽������Ŀ��Ϣ����  (һ��1��)
INSERT INTO dbCenter.dbo.TC_Prj_Info
SELECT
newid() FId         --��Ŀid
,''  JSDW         --���赥λ����
,''  JSDWDM         --���赥λ��֯��������(Ϊ��)
,''  JSDWDZ         --���赥λ��ַ  (Ϊ��)
,null   Contacts         --���赥λ��ϵ��
,null Mobile         --���赥λ��ϵ�绰
,isnull(a.G_project_name,'')+'-�б걸������' ProjectName         --��Ŀ���Ƽ���'[�б걸��������Ŀ]'������Ŀ����
,'51' as Province --��Ŀ����-ʡ
,null as City --��Ŀ����-����   (�������ݵ�������ͳһ����)
,null as County --��Ŀ����-���� (����������������ͳһ����)
,'' as ProjectType         --Ϊ����Ŀ���:2000101--���ݽ���;2000102--��������;2000103--����
,'�б��ļ��������޷���������Ŀ'+'-ps'+a.baojian_no as  Address         --��Ŀ��ַ ��ԭ���ı�����Ŵ�����������Ŀ
,null ProjectNumber         --�����ĺ�
,NULL ProjectLevel         --�����
,NULL ProjectTime         --��Ŀʱ��
,NEWID() ProjectNo         --��Ŀ���  ֮ǰû���������ݣ���Ŀ���newһ��guid
,NULL IsForeign         --�Ƿ�����
,NULL JSYDXKZ         --�����õ����֤��
,NULL JSGCXKZ         --���̹滮���֤��
,null Area         --�����������m2��
,NULL Investment         --Ͷ�ʹ�ģ(��Ԫ)
,NULL ConstrType         --�������ʣ�2000501--�½���2000502--�Ľ� --��ˮ	200010201 --��ˮ	200010202  --ȼ��	200010203  --����	200010204 --��·	200010205--����	200010206
--�羰԰��	200010207 --����԰��	200010208  --������ͨ	200010209 --����	200010210
,null as ProjectUse         --������;��200010201--��ˮ��200010205--��·
,null as  StartDate         --ʵ�ʿ�������
,null as  EndDate         --ʵ�ʿ�������
,null RegisterTime         --��¼�Ǽ�ʱ��
,NULL  ConstrBasis         --��������
,NULL ConstrContent         --��������
,null as  FJSDWID --���赥λ���
,null as  AddressDept    --��ַ���
,null ConstrScale--�����ģ
,NULL LandType  --�õ����� --�½�	2000501  --�Ľ�	2000502 --����	2000503 --�ؽ�	2000504  --Ǩ��	2000505--�ָ�	2000506--����	2000507
,null JSDWFR         --���赥λ����
from  zhongbiaobeian_psq a
where a.baojian_no not in 
(
  select  SUBSTRING(b.Address,CHARINDEX('-',Address)+3,13) from TC_Prj_Info b
  where  b.Address like '%-ps%'
  and b.ProjectName   like '%-�б걸������'
)
--2�����뵥�幤����Ŀ��Ϣ
INSERT INTO dbCenter.dbo.TC_PrjItem_Info
SELECT 
NEWID() FId         --���幤��id ������һ��id��Ϊ����
,a.FId FPrjId         --����TC_Prj_Info.FId
,a.ProjectName ProjectName         --��Ŀ����
,a.Address as  Address         --��Ŀ��ַ
,NULL Area         --���������
,NULL Investment         --Ͷ�ʹ�ģ(��Ԫ)
,a.JSDW JSDW         --���赥λ����
,a.JSDWFR LegalPerson         --���˴���
,a.JSDWDM JSDWDM         --���赥λ��֯��������
,a.ProjectName PrjItemName         --��������
,NULL as  PrjItemType     --�������          --�������2000101--���ݽ���;2000102--��������;2000103--  ����  --שľ�ṹ	509001--ש��ṹ	509002--�ֽ�������ṹ	509003--�ֽṹ	509004
--��ܽṹ	509005--����ǽ�ṹ	509006--���-����ǽ�ṹ	509007--��-Ͳ�ṹ	509008--����	509010
,NULL ConstrType         --�ṹ���ͣ�509001��509002��509005
,null Cost         --������ۣ���Ԫ��
,NULL Scale         --���̹�ģ
,NULL PrjItemDesc         --��������
,a.FJSDWID as  FJSDWID         --���赥λ���
,a.AddressDept AddressDept         --��ַ���
,a.ConstrScale ConstrScale--�����ģ
FROM  TC_Prj_Info A
where a.ProjectName  like '%-�б걸������'

--------------------------------------------------------------------------------------------------------------------------------
--3�������б��ļ�������Ϣ
USE [dbCenter]
GO
--�޸��б��ļ����еı�����ƺ���Ŀ���Ƴ���
alter table TC_ZBWJ_Record alter column bdbm varchar(500);
alter table TC_ZBWJ_Record alter column ProjectName varchar(500);
go

INSERT INTO [dbo].[TC_ZBWJ_Record]
           ([FId]           ,[FAppId]           ,[FPrjId]           ,[BDId]           ,[CS]           ,[BDBM]           ,[BDMC]
           ,[ProjectName]           ,[FBFS]           ,[ZGYSFS]           ,[ZBZZXS]           ,[DLJG]
           ,[BZR]           ,[SHR]           ,[SDR]           ,[BATime]           ,[FResult]           ,[DLJGId])
select  --a.*
newid() as fid,--�б걸������
newid() as Fappid,--ҵ������
case b.FId when   null  then newid()  else b.FId end as FPrijid,--��Ŀ���
case b.FId when  null  then newid() else  b.FId end as BDid, --���ID    ��Ŀǰ�ޱ����Ϣ����ʱ����Ŀ�����Ϊ��α��
null as CS,--����
null as BDBM,--��α���
a.project_name as BDMC,--�������
a.project_name as ProjectName,--��Ŀ����
case a.fabao_way when '�����б�' then '11220902' else '11220901' end as FBFS,--������ʽ
case a.zigeshencha when '�ʸ����' then '11220702' else  '11220701' end as ZGYSFS,--�ʸ�Ԥ��ʽ
a.zuzixinshi as ZBZZXS,--�б���֯��ʽ
a.dailijigou as DLJG,--�������
null as BZR,--������
null as SHR,--�����
null as SDR,--����
case a.zhaobiaobei_time when 'NULL' then null else zhaobiaobei_time end as BAtime,--����ʱ��
null as Fresult,--�������
null as DLJGID--����������
from  zhaobiaobeian_psq as a left join TC_Prj_Info b
on a.baojian_no = SUBSTRING(b.Address,CHARINDEX('-',Address)+3,13)
and b.Address like '%-ps%'
and b.ProjectName   like '%-�б걸������'
--------------------------------------------------------------------------------------------------------------------------------
--4�������б걸��ҵ������  �����б걸���������ҵ�������в�������.
INSERT INTO dbCenter.dbo.CF_App_List
SELECT  --*
 a.FAppId FId    --ҵ�����
,newid()  FBaseinfoId    --��ҵid  (��ʱ�޷��ҵ���ҵ��Ϣ,��ʱnewһ��guid)
,a.FPrjId FPrjId    --��Ŀ����
,CASE WHEN ISNULL(a.BATime,'')<>'' THEN CONVERT(CHAR(4),YEAR(a.BATime))+'�� ' ELSE '' END +'��Ͷ�걸��' FName    --ҵ������
,11232 FManageTypeId    --ҵ�����   11232���б��ļ�����
,a.BATime FwriteDate    --д��ʱ��
,a.BATime FReportDate    --�ϱ�ʱ��
,NULL FIsSign    --�Ƿ�ǩ��
,0 FState    --ҵ��״̬
,NULL FResult    --��������
,YEAR(a.BATime) FYear    --���
,MONTH(a.BATime) FMonth    --�·�
,a.FPrjId FLinkId    --������̱���
,b.JSDW FBaseName    --���赥λ����
,NULL FUpDeptId    --�ϱ����ŵ�������
,NULL FRemark    --�ݲ�����
,NULL FIsCheck    --�ݲ�����
,NULL FCount    --�ݲ�����
,a.BATime FTime    --������ʱ��,ҵ����ʱû���ϱ���û������
,NULL FIsDeleted    --�Ƿ�ɾ��
,a.BATime FCreateTime    --����ʱ��
,1 FReportCount    --�ݲ�����
,NULL FToBaseinfoId    --�ݲ�����
,NULL FAppDate    --�ݲ�����
,NULL FLinkAppId    --�ݲ�����
,NULL FBarCode    --�ݲ�����
,NULL FCreateUser    --�ݲ�����
,NULL FgfTime    --�ݲ�����
from  [TC_ZBWJ_Record] a left join TC_Prj_Info b
on a.FPrjId = b.FId
where  b.Address like '%-ps%'
and b.ProjectName   like '%-�б걸������'
--------------------------------------------------------------------------------------------------------------------------
--5�������б�����Ϣ
--�޸��б��ļ������ļ����еı�����ƺ���Ŀ���Ƴ���
alter table TC_ZBJG_Record alter column projectname varchar(500);
alter table TC_ZBJG_Record alter column BDMC varchar(500);
alter table TC_ZBJG_Record alter column zhaobr varchar(500);
alter table TC_ZBJG_Record alter column zhongbr varchar(500);
--������������0000-00-00Ϊ'1900-01-01'
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
 newid() Fid,--����
 b.FId  Fprjid,--��Ŀ���
 newid() Fappid,--ҵ��������newһ��guid��
 B.FId BDid,--���ID
 null CS,--����
 b.FId GCBM,--����Ŀ�����Ϊ���̱���
 b.FId BDBM,--����Ŀ�����Ϊ��α���
 null ZTBBM,--��Ͷ�����
 a.G_project_name ProjectName,--��Ŀ����
 a.G_project_name ProjectName,--�������
 null ZBDLDW,--�������
 b.JSDW ZHAOBR,--�б���
 CONVERT(DATE,a.kaibiao_date)   KBSJ,--����ʱ��
 b.FJSDWID QYBM,--��ҵ����
 a.G_zhongbiaoren ZHONGBR,--�б���
 CONVERT(DATE,b.StartDate) KGRQ,--��������
 CONVERT(DATE,b.EndDate)   JGRQ,--��������
 null HTQDDD,--��ͬǩ���ص�
 null tzffsj,--֪ͨ����ʱ��
 null ZBJG,--�б���
 null ZBYY,--�б�ԭ��
 a.zhong_leader Fresult,--�������
 b.FJSDWID QYid--��ҵ���
 from  zhongbiaobeian_psq a left join TC_Prj_Info b
on a.baojian_no = SUBSTRING(b.Address,CHARINDEX('-',Address)+3,13)
and b.Address like '%-ps%'
and b.ProjectName   like '%-�б걸������'

--------------------------------------------------------------------------------------------------------------------------
--6�������б���ҵ������  ����[TC_ZBJG_Record]���з�����
--�����б���ҵ������
INSERT INTO dbCenter.dbo.CF_App_List
SELECT 
 a.FAppId as  FId,    --ҵ�����
 newid() FBaseinfoId, --��ҵid �б�����������û����ҵ��ţ�������һ��GUID
 a.FPrjId  FPrjId,--��Ŀ����     �б�����������û����Ŀ��ţ�������һ��GUID
 CONVERT(char(4),year(convert(datetime,a.KBSJ)))+'�� '+'�б�������' FName    --ҵ������
,11235 FManageTypeId    --ҵ�����(�б�������)  ҵ�������11235
,a.KBSJ   FwriteDate    --д��ʱ��
,a.KBSJ FReportDate    --�ϱ�ʱ�� (��ʱ��ͬ��ʱ��Ϊ׼)
,NULL FIsSign    --�Ƿ�ǩ��
,0 FState    --ҵ��״̬
,NULL FResult    --��������
,YEAR(convert(datetime,isnull(a.KBSJ,''))) FYear    --���
,MONTH(convert(datetime,isnull(a.KBSJ,''))) FMonth    --�·�
,SUBSTRING(b.Address,CHARINDEX('-',Address)+3,13) FLinkId    --������̱���(��ʱ�Ա���idΪ�������id��
,a.ZHAOBR FBaseName    --���赥λ����
,NULL FUpDeptId    --�ϱ����ŵ�������
,NULL FRemark    --�ݲ�����
,NULL FIsCheck    --�ݲ�����
,NULL FCount    --�ݲ�����
,a.KBSJ FTime    --������ʱ��,ҵ����ʱû���ϱ���û������
,NULL FIsDeleted    --�Ƿ�ɾ��
, convert(datetime,isnull(a.KBSJ,'')) FCreateTime    --����ʱ��
,1 FReportCount    --�ݲ�����
,NULL FToBaseinfoId    --�ݲ�����
,NULL FAppDate    --�ݲ�����
,NULL FLinkAppId    --�ݲ�����
,NULL FBarCode    --�ݲ�����
,NULL FCreateUser    --�ݲ�����
,NULL FgfTime    --�ݲ�����
from  TC_ZBJG_Record  a 
left join TC_Prj_Info b
on a.FPrjId = b.FId
and b.Address like '%-ps%'
and b.ProjectName   like '%-�б걸������'
and a.FAppId not in
(select fid from CF_App_List)




--select  *  from  TC_ZBJG_Record  where FAppId = '2a3b3c2e-b2bd-4fb6-8929-00f6fc3da8de'
--select  *  from  CF_Sys_ManageType  where  fname like '%�б���%'   --��ѯҵ�����ͱ���
--------------------------------------------------------------------------------------------------------------------------







