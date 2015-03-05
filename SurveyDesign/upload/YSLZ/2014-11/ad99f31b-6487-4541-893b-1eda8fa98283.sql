use ZJB_dbStandard
go
if object_id('TBProjectInfo') is not null
drop table TBProjectInfo
go
---������Ŀ������Ϣ����
create table TBProjectInfo
(
	ID uniqueidentifier primary key not null,
	PrjNum varchar(20),
	PrjName varchar(200),
	PrjTypeNum char(2),
	BuildCorpName varchar(200),
	BuildCorpCode varchar(15),
	ProvinceNum int,
	CityNum int,
	CountyNum int,
	PhaseName int,
	PrjApprovalNum varchar(200),
	PrjApprovalLevelNum char(3),
	BuldPlanNum varchar(100),
	ProjectPlanNum varchar(100),
	AllInvest decimal(15,4),
	AllArea decimal(15,4),
	PrjPropertyNum char(3),
	PrjFunctionNum char(3),
	TenderNum varchar(50),
	TenderTypeNum char(3),
	CensorNum varchar(50),
	BuilderLicenceNum varchar(50),
	PrjSize varchar(500),
	ContractMoney decimal,
	EconCorpName varchar(200),--�б�����������
	EconCorpCode varchar(15),
	KcEconCorpName varchar(200),--���쵥λ���� 
	KcEconCorpCode varchar(15),
	DesignCorpName varchar(200),
	DesignCorpCode varchar(15),
	CensorCorpName varchar(200),
	CensorCorpCode varchar(15),
	ConsCorpName varchar(200),
	ConsCorpCode varchar(15),
	SuperCorpName varchar(200),
	SuperCorpCode varchar(15),
	QCCorpName varchar(200),
	QCCorpCode varchar(15),
	ConstructorName varchar(50),
	CIDCardTypeNum char(1),
	ConstructorIDCard varchar(30),
	SupervisionName varchar(50),
	SIDCardTypeNum char(1),
	SupervisionIDCard varchar(30),
	PrjFinishNum varchar(50),
	BDate datetime,
	EDate datetime,
	FactAllCost decimal(15,4),
	FactAllArea decimal(14,4),
	FactPrjSize varchar(500),
	CreateDate datetime
)	
go
if object_id('TBProjectDesignEconUserInfo') is not null
drop table TBProjectDesignEconUserInfo
go
--������ƴ�ҵ��Ա��ϸ��
create table TBProjectDesignEconUserInfo
(
   ID	uniqueidentifier primary key not null,
   PrjNum varchar(20),
   CensorNum varchar(50),
   CorpName varchar(200),
   CorpCode varchar(15),
   UserName varchar(50),
   IDCardTypeNum char(1),
   IDCard varchar(30),
   SpecialtyTypNum int,
   PrjDuty varchar(50)
)
go
if object_id('TBProjectBuilderUserInfo')is not null
drop table TBProjectBuilderUserInfo
go
--ʩ����ȫ��ҵ��Ա��ϸ��
create table TBProjectBuilderUserInfo
(
	 ID	uniqueidentifier primary key not null,
	 PrjNum varchar(20),
	 BuilderLicenceNum varchar(50),
	 CorpName varchar(200),
	 CorpCode varchar(15),
	 SafetyCerID varchar(30),
	 UserName varchar(50),
	 IDCardTypeNum  char(1),
	 IDCard varchar(30),--���֤����Ϊ18λ
	 CertID varchar(30),
	 UserType int, --1��Ҫ������2��Ŀ������3��ȫԱ
)

--������  ��Ŀ������Ϣ��
alter table XM_BaseInfo.dbo.XM_XMJBXX add 
JSDWZZJFDM varchar(200),
JSDWFR varchar(100),
JSDWFRDH varchar(100),
JSDWJSFZR varchar(100),
JSDWJSFZRZC varchar(100),
JSDWJSFZRDH varchar(100)
go
--ʩ��ͼ�����Ϣ��������
alter table XM_BaseInfo.dbo.XM_SGTSCXX add
ZYDMJ float,
DZJBLD float,
KZSFLD float,
DS   int,
RJL float,
JZMD float,
LDL float,
DSZMJ float,
DXZMJ float,
YTGCDJ varchar(20),
CDDJ varchar(20),
DJDJ varchar(20),
YTGCLB varchar(20),
TCW int
go
--ʩ��ͼ�����Ϣ(����)  ��δ�ҵ�
--alter table XM_BaseInfo.dbo.XM_SGTSCSZXX add
--DZJBLD float,
--KZSFLD float,
--YTGCDJ varchar(20),
--CDDJ varchar(20),
--DJDJ varchar(20),
--YTGCLB varchar(20)
go
--��׼���е�豸��Ϣ
alter table XM_BaseInfo.dbo.GC_JQSBXX add
SBLX int,
ZZCJ varchar(100),
CCRQ datetime,
CCBH varchar(50),
ZZXKZ varchar(50),
SBGZSJ datetime,
QZL float,
QZDW varchar(50)
go
insert into TBProjectInfo(ID,PrjName,BuildCorpName,AllInvest,PrjPropertyNum,PrjSize,pkid,LastUpdateDate)
select newid(), XMMC,JSDW,XMZTZ,JSXZ,JSGM,XMBH,getdate() from XM_BaseInfo.dbo.XM_XMJBXX
GO

TRUNCATE TABLE TBProjectInfo
INSERT INTO TBProjectInfo
SELECT NEWID(),SUBSTRING(A.XMBH,1,20),A.XMMC,A.XMLX,A.JSDW,'��֯�ṹ����','��Ŀ����ʡ','��Ŀ������','��Ŀ������',
'��Ϣ��������',C.LXWH,C.LXJB,C.JSGCYDXKZ,C.JSGCGHXKZ,C.ZTZE,C.YDMJ,C.JSXZ,'������;',
D.ZBTZSBH,E.ZBFS,'ʩ��ͼ�ϸ�����',F.XKZBH,C.LXPZGM,'���','�б�����������','�б���������֯��������',
'���쵥λ����','���쵥λ��֯��������','��Ƶ�λ����','��Ƶ�λ��֯��������','ʩ��ͼ����������',
'ʩ��ͼ��������֯��������','ʩ����λ����','ʩ����λ��֯��������','����λ����','����λ��֯��������',
'��������������','������������֯��������','��Ŀ��������','��Ŀ����֤������','��Ŀ����֤������',
'�ܼ�����ʦ����','�ܼ�����ʦ֤������','�ܼ�����ʦ֤������',G.BAH,G.SJKGRQ,G.JGYSRQ,G.GCZJ,
G.JZMJ,'ʵ�ʽ����ģ','��¼�Ǽ�ʱ��',A.XMBH,getdate()
FROM XM_BaseInfo.dbo.XM_XMJBXX A LEFT JOIN XM_BaseInfo.dbo.GC_DWGCXX B
ON A.XMBH = B.XMBH 
LEFT JOIN XM_BaseInfo.dbo.XM_XMBJXX C
ON A.XMBH= C.XMBH
LEFT JOIN XM_BaseInfo.dbo.XM_ZBJGXX D
ON A.XMBH= D.XMBH
LEFT JOIN XM_BaseInfo.dbo.XM_ZBJBXX E
ON A.XMBH= E.XMBH
LEFT JOIN XM_BaseInfo.dbo.GC_SGXKZ F
ON A.XMBH= F.XMBH
LEFT JOIN XM_BaseInfo.dbo.XM_JGBAXX G
ON A.XMBH= G.XMBH
go
if object_id('TBTenderInfo') is not null
drop table TBTenderInfo
go
-- ��Ͷ����Ϣ
create table TBTenderInfo
(
   FID	varchar(50) primary key not null,
   TenderNum varchar(50),
   PrjNum varchar(20),
   TenderClassNum char(3),
   TenderTypeNum char(3),
   TenderResultDate datetime,
   TenderMoney decimal(15,4),
   PrjSize varchar(500),
   Area decimal(15,2),
   AgencyCorpName varchar(200),
   AgencyCorpCode varchar(15),
   TenderCorpName varchar(200),
   TenderCorpCode varchar(15),
   ConstructorName varchar(50),
   IDCardTypeNum char(1),
   ConstructorIDCard varchar(30),
   CreateDate datetime,
   pkid varchar(50),
   LastUpdateDate datetime
)
go
if object_id('TBContractRecordManage') is not null
drop table TBContractRecordManage
go
-- ��ͬ������Ϣ
create table TBContractRecordManage
(
	FID varchar(50) primary key not null,
	RecordNum varchar(50),
	PrjNum varchar(20),
	ContractNum varchar(50),
	ContractTypeNum char(3),
	ContractMoney decimal(15,4),
	PrjSize varchar(500),
	ContractDate datetime,
	PropietorCorpName varchar(200),
	PropietorCorpCode varchar(15),
	ContractorCorpName varchar(200),
	ContractorCorpCode varchar(15),
	UnionCorpName varchar(200),
	UnionCorpCode varchar(15),
	CreateDate datetime,
	pkid varchar(50),
    LastUpdateDate datetime
)
go
if object_id('TBProjectCensorInfo') is not null
drop table TBProjectCensorInfo
go
-- ��ͬ������Ϣ
create table TBProjectCensorInfo
(
	FID varchar(50) primary key not null,
	CensorNum varchar(50),
	PrjNum varchar(20),
	CensorCorpName varchar(200),
	CensorCorpCode varchar(15),
	CensorPersonName varchar(30),
	CIDCardTypeNum char(1),
	CensorPersonIDCard varchar(50),
	CensorEDate datetime,
	PrjSize varchar(500),
	CorpName varchar(200),
	CorpCode varchar(15),
	TradePersonName varchar(50),
	TIDCardTypeNum char(1),
	TradePersonIDCard varchar(30),
	SpecialtyTypNum int,
	PrjDuty varchar(50),
	OneCensorIsPass int,
	OneCensorWfqtCount int,
	OneCensorWfqtContent varchar(500),
	CreateDate datetime,
	pkid varchar(50),
    LastUpdateDate datetime
)
go
if object_id('TBBuilderLicenceManage') is not null
drop table TBBuilderLicenceManage
go
-- ��ͬ������Ϣ
create table TBBuilderLicenceManage
(
	FID varchar(50) primary key not null,
	BuilderLicenceNum varchar(50),
	PrjNum varchar(20),
	BuldPlanNum varchar(100),
	ProjectPlanNum varchar(100),
	CensorNum varchar(50),
	ContractMoney decimal(15,4),
	Area decimal(15,2),
	PrjSize varchar(500),
	IssueCertDate datetime,
	EconCorpName varchar(200),
	EconCorpCode varchar(15),
	DesignCorpName varchar(200),
	DesignCorpCode varchar(15),
	ConsCorpName varchar(200),
	ConsCorpCode varchar(15),
	SafetyCerID varchar(30),
	SuperCorpName varchar(200),
	SuperCorpCode varchar(15),
	ConstructorName varchar(50),
	CIDCardTypeNum char(1),
	ConstructorIDCard varchar(30),
	SupervisionName varchar(50),
	SIDCardTypeNum char(1),
	SupervisionIDCard varchar(30),
	UserName varchar(50),
	IDCardTypeNum char(1),
	IDCard varchar(30),
	CertID varchar(30),
	UserType int,
	CreateDate datetime,
	pkid varchar(50),
    LastUpdateDate datetime
)
go
if object_id('TBProjectFinishManage') is not null
drop table TBProjectFinishManage
go
-- ��ͬ������Ϣ
create table TBProjectFinishManage
(
	FID varchar(50) primary key not null,
	PrjFinishNum varchar(50),
	PrjNum varchar(20),
	BuilderLicenceNum varchar(50),
	QCCorpName varchar(200),
	QCCorpCode varchar(15),
	FactCost decimal(15,4),
	FactArea decimal(15,2),
	FactSize varchar(500),
	PrjStructureTypeNum char(3),
	BDate datetime,
	EDate datetime,
	Mark varchar(500),
	CreateDate datetime,
	pkid varchar(50),
    LastUpdateDate datetime
)
go--��Ͷ����Ϣ
insert into TBTenderInfo
select  '', --�б�֪ͨ����
		a.YWBM, --��Ŀ���
		'', ---�б�����
		a.ZBFS,  --�б귽ʽ
		'',  --�б�����
		a.ZBJ,  --�б���
		'',   --�����ģ
		'',   --���
		'',   --�б����λ����
		'',  --�б����λ��֯��������
		a.ZBDW,  --�б굥λ����
		'',  --�б굥λ��֯��������
		XMJL,  --��Ŀ����/�ܼ�����ʦ����
		'',  --��Ŀ����/�ܼ�����ʦ֤������
		'',  --��Ŀ����/�ܼ�����ʦ֤������
		''  --��¼�Ǽ�ʱ��
		from YW_ZBJG_KCDW a
go
insert into TBTenderInfo
select  '', --�б�֪ͨ����
		a.YWBM, --��Ŀ���
		'', ---�б�����
		a.ZBFS,  --�б귽ʽ
		'',  --�б�����
		a.ZBJ,  --�б���
		'',   --�����ģ
		'',   --���
		'',   --�б����λ����
		'',  --�б����λ��֯��������
		a.ZBDW,  --�б굥λ����
		'',  --�б굥λ��֯��������
		'',  --��Ŀ����/�ܼ�����ʦ����
		'',  --��Ŀ����/�ܼ�����ʦ֤������
		'',  --��Ŀ����/�ܼ�����ʦ֤������
		''  --��¼�Ǽ�ʱ��
		from YW_ZBJG_SJDW a
go
insert into TBTenderInfo
select  '', --�б�֪ͨ����
		a.YWBM, --��Ŀ���
		'', ---�б�����
		a.ZBFS,  --�б귽ʽ
		'',  --�б�����
		a.ZBJ,  --�б���
		'',   --�����ģ
		'',   --���
		'',   --�б����λ����
		'',  --�б����λ��֯��������
		a.ZBDW,  --�б굥λ����
		'',  --�б굥λ��֯��������
		'',  --��Ŀ����/�ܼ�����ʦ����
		'',  --��Ŀ����/�ܼ�����ʦ֤������
		'',  --��Ŀ����/�ܼ�����ʦ֤������
		''  --��¼�Ǽ�ʱ��
		from YW_ZBJG_JLDW a
go
insert into TBTenderInfo
select  '', --�б�֪ͨ����
		a.YWBM, --��Ŀ���
		'', ---�б�����
		a.ZBFS,  --�б귽ʽ
		'',  --�б�����
		a.ZBJ,  --�б���
		'',   --�����ģ
		a.JZMJ,   --���
		'',   --�б����λ����
		'',  --�б����λ��֯��������
		a.ZBDW,  --�б굥λ����
		'',  --�б굥λ��֯��������
		'',  --��Ŀ����/�ܼ�����ʦ����
		'',  --��Ŀ����/�ܼ�����ʦ֤������
		'',  --��Ŀ����/�ܼ�����ʦ֤������
		''  --��¼�Ǽ�ʱ��
		from YW_ZBJG_SGDW a
go
--ʩ��ͼ�����Ϣ
insert into TBProjectCensorInfo
select  a.SCHGSBH,--ʩ��ͼ���ϸ����ţ�
		a.YWBM,--��Ŀ���
		a.STJG, --ʩ��ͼ����������
		'', --ʩ��ͼ��������֯��������
		'', --����������
		'', --�����ģ
		'', --���쵥λ����
		'', --���쵥λ��֯��������
		'', --��Ƶ�λ����
		'', --��Ƶ�λ��֯��������
		'', --һ������Ƿ�ͨ��
		'', --һ�����ʱΥ��ǿ����
		'', --һ�����ʱΥ����ǿ����Ŀ
		'' --��¼�Ǽ�����
		from YW_SGTZJJSZL a
