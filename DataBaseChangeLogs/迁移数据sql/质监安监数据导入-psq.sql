--ɾ���ʼ�������Ϣ
-----------------------------------------------------------------------------------------------------------------
--ҵ������
  Delete dbCenter.dbo.CF_App_List where FId in 
(SELECT 
BA.YWBM    --ҵ�����
FROM JKCWFDB_WORK_NJS.DBO.YW_ZLJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (391,392))
  );

--��Ŀ��
delete from  dbCenter.dbo.TC_Prj_Info where FId in (
SELECT --A.*
A.GuidId         --��Ŀid
FROM JKCWFDB_WORK_NJS.DBO.YW_XMInfo AS A
WHERE A.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (391,392))
);

--���幤�̱�
delete from dbCenter.dbo.TC_PrjItem_Info where FId in (
SELECT 
FQ.GuidId
FROM JKCWFDB_WORK_NJS.DBO.YW_ZLJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (391,392))
);

--�ʼ౸����
delete from dbCenter.dbo.TC_QA_Record where FId in (
SELECT
a.GuidID
from JKCWFDB_WORK_NJS.dbo.YW_ZLJDBAinfo a
left join JKCWFDB_WORK_NJS.dbo.YW_XMInfo b on a.YWBM = b.YWBM
WHERE  b.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
   AND a.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01' and C06.actionid in (391,392))
);

/*****   end  *****/
-----------------------------------------------------------------------------------------------------------------

--�����ʼ���ص���Ŀ��
INSERT INTO dbCenter.dbo.TC_Prj_Info
SELECT --A.*
A.GuidId FId         --��Ŀid
,A.XMJSDW  JSDW         --���赥λ����
,JS.ZZJGDM  JSDWDM         --���赥λ��֯��������
,JS.DWDZ  JSDWDZ         --���赥λ��ַ
,JS.LXR   Contacts         --���赥λ��ϵ��
,JS.LXDH Mobile         --���赥λ��ϵ�绰
,A.XMMC ProjectName         --��Ŀ����
,CASE WHEN A.XMSZD LIKE '51%' THEN LEFT(A.XMSZD,2) ELSE NULL END Province         --��Ŀ����-ʡ
,CASE WHEN A.XMSZD LIKE '51%' AND LEN(A.XMSZD)>=4 THEN LEFT(A.XMSZD,4) ELSE NULL END City         --��Ŀ����-����
,CASE WHEN A.XMSZD LIKE '51%' AND LEN(A.XMSZD)>=6 THEN LEFT(A.XMSZD,6) ELSE NULL END County         --��Ŀ����-����
,CASE WHEN ISNULL(BA.GCXZ,'0')='6' THEN '2000101'
WHEN ISNULL(BA.GCXZ,'0')='5' THEN '2000102'
WHEN ISNULL(BA.GCXZ,'0')='7' THEN '2000103'
ELSE '2000103' END ProjectType         --��Ŀ���:2000101--���ݽ���;2000102--��������;2000103--����
,A.XMDZ Address         --��Ŀ��ַ
,BA.LXWH ProjectNumber         --�����ĺ�
,CASE WHEN ISNULL(LX.LXJB,'0')='1' THEN '11220401'
WHEN ISNULL(LX.LXJB,'0')='2' THEN '11220402'
WHEN ISNULL(LX.LXJB,'0')='3' THEN '11220403'
WHEN ISNULL(LX.LXJB,'0')='4' THEN '112204034'
ELSE '112204035' END ProjectLevel         --�����
,BA.LXSJ ProjectTime         --��Ŀʱ��
,A.XMBH ProjectNo         --��Ŀ���
,NULL IsForeign         --�Ƿ�����
,NULL JSYDXKZ         --�����õ����֤��
,NULL JSGCXKZ         --���̹滮���֤��
,JZ.ZMJ Area         --�����������m2��
,NULL Investment         --Ͷ�ʹ�ģ(��Ԫ)
,CASE WHEN ISNULL(LX.JZXZ,'0')='�½�' THEN '30503'
WHEN ISNULL(LX.JZXZ,'0')='�Ľ�' THEN '30501'
WHEN ISNULL(LX.JZXZ,'0')='����' THEN '30502'
ELSE NULL END ConstrType         --�������ʣ�2000501--�½���2000502--�Ľ�
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
,CASE WHEN ISNULL(SG.GCLB,'0')='01' OR ISNULL(SG.GCLB,'0')='02' OR ISNULL(SG.GCLB,'0')='03' OR
ISNULL(SG.GCLB,'0')='04' OR ISNULL(SG.GCLB,'0')='05' OR ISNULL(SG.GCLB,'0')='06' OR
ISNULL(SG.GCLB,'0')='07' OR ISNULL(SG.GCLB,'0')='08' OR ISNULL(SG.GCLB,'0')='09' OR ISNULL(SG.GCLB,'0')='10' THEN '200010113'
WHEN ISNULL(SG.GCLB,'0')='11' OR ISNULL(SG.GCLB,'0')='12' OR ISNULL(SG.GCLB,'0')='13' THEN '200010209'
WHEN ISNULL(SG.GCLB,'0')='14' OR ISNULL(SG.GCLB,'0')='15' THEN '200010210' 
WHEN ISNULL(SG.GCLB,'0')='16' THEN '200010204'
WHEN ISNULL(SG.GCLB,'0')='17' THEN '200010201'
WHEN ISNULL(SG.GCLB,'0')='18' THEN '200010206'
WHEN ISNULL(SG.GCLB,'0')='19' THEN '2000103'
ELSE NULL END ProjectUse         --������;��200010201--��ˮ��200010205--��·
,SG.SJKGRQ StartDate         --ʵ�ʿ�������
,SG.SJJGRQ EndDate         --ʵ�ʿ�������
,NULL RegisterTime         --��¼�Ǽ�ʱ��
,NULL  ConstrBasis         --��������
,NULL ConstrContent         --��������
,A.JSDW FJSDWID --���赥λ���
,A.XMSZD AddressDept    --��ַ���
,CASE WHEN ISNULL(SG.JSGM,0)<>0 THEN '���' + CONVERT(VARCHAR(20),SG.JSGM) + '��m2��'
WHEN ISNULL(SG.KDGD,0)<>0 THEN '��ȣ��߶ȣ�' + CONVERT(VARCHAR(20),SG.KDGD) + '��m��'
ELSE NULL END ConstrScale--�����ģ
,NULL LandType  --�õ�����
--�½�	2000501
--�Ľ�	2000502
--����	2000503
--�ؽ�	2000504
--Ǩ��	2000505
--�ָ�	2000506
--����	2000507
,JS.FRXM JSDWFR         --���赥λ����
FROM JKCWFDB_WORK_NJS.DBO.YW_XMInfo AS A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=A.JSDW
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_ZLJDBAinfo AS BA ON BA.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMLXInfo  AS LX ON LX.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS SG ON SG.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_JSZBInfo AS JZ ON JZ.YWBM=A.YWBM
WHERE A.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
AND ISNULL(A.XMMC,'') <>'' 
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (391,392));

-----------------------------------------------------------------------------------------------------------------
--�����ʼ൥�幤�̱�
INSERT INTO dbCenter.dbo.TC_PrjItem_Info
SELECT 
FQ.GuidId FId         --���幤��id
,B.GuidId FPrjId         --����TC_Prj_Info.FId
,B.XMMC ProjectName         --��Ŀ����
,B.XMDZ Address         --��Ŀ��ַ
,JZ.ZMJ Area         --���������
,SG.HTJG Investment         --Ͷ�ʹ�ģ(��Ԫ)
,B.XMJSDW JSDW         --���赥λ����
,JS.FRXM LegalPerson         --���˴���
,JS.ZZJGDM JSDWDM         --���赥λ��֯��������
,isnull(FQ.Name,'����Ŀ����') PrjItemName         --��������
,CASE WHEN ISNULL(BA.GCXZ,'0')='6' THEN '2000101'
WHEN ISNULL(BA.GCXZ,'0')='5' THEN '2000102'
WHEN ISNULL(BA.GCXZ,'0')='7' THEN '2000103'
ELSE '2000103' END PrjItemType --�������          --�������2000101--���ݽ���;2000102--��������;2000103--����
--שľ�ṹ	509001
--ש��ṹ	509002
--�ֽ�������ṹ	509003
--�ֽṹ	509004
--��ܽṹ	509005
--����ǽ�ṹ	509006
--���-����ǽ�ṹ	509007
--��-Ͳ�ṹ	509008
--����	509010
,CASE WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='שľ�ṹ' THEN '509001' 
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='ש��ṹ' THEN '509002'
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='�ֽ�������ṹ' THEN '509003'
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='�ֽṹ' THEN '509004'
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='��ܽṹ' THEN '509005'
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='����ǽ�ṹ' THEN '509006' 
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='���-����ǽ�ṹ' THEN '509007' 
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='��-Ͳ�ṹ' THEN '509008' 
ELSE '509010' END ConstrType         --�ṹ���ͣ�509001��509002��509005
,SG.HTJG Cost         --������ۣ���Ԫ��
,NULL Scale         --���̹�ģ
,NULL PrjItemDesc         --��������
,JS.FID FJSDWID         --���赥λ���
,B.XMSZD AddressDept         --��ַ���
,CASE WHEN ISNULL(SG.JSGM,0)<>0 THEN '���' + CONVERT(VARCHAR(20),SG.JSGM) + '��m2��'
WHEN ISNULL(SG.KDGD,0)<>0 THEN '��ȣ��߶ȣ�' + CONVERT(VARCHAR(20),SG.KDGD) + '��m��'
ELSE NULL END ConstrScale--�����ģ
FROM JKCWFDB_WORK_NJS.DBO.YW_ZLJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMLXInfo  AS LX ON LX.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS SG ON SG.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_JSZBInfo AS JZ ON JZ.YWBM=BA.YWBM
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
AND ISNULL(B.XMMC,'') <>'' 
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (391,392));
-----------------------------------------------------------------------------------------------------------------
--�����ʼ�ҵ������
INSERT INTO dbCenter.dbo.CF_App_List
SELECT 
BA.YWBM FId    --ҵ�����
,B.JSDW FBaseinfoId    --��ҵid
,B.GuidId FPrjId    --��Ŀ����
,CONVERT(VARCHAR(20),YEAR(YW.CreateTime)) + '�� �����ල����' FName    --ҵ������
,11221 FManageTypeId    --ҵ�����
,YW.CreateTime FwriteDate    --д��ʱ��
,BA.LXSJ FReportDate    --�ϱ�ʱ��
,NULL FIsSign    --�Ƿ�ǩ��
,0 FState    --ҵ��״̬
,NULL FResult    --��������
,YEAR(BA.LXSJ) FYear    --���
,MONTH(BA.LXSJ) FMonth    --�·�
,FQ.GuidId FLinkId    --������̱���
,JS.FCompany FBaseName    --���赥λ����
,NULL FUpDeptId    --�ϱ����ŵ�������
,NULL FRemark    --�ݲ�����
,NULL FIsCheck    --�ݲ�����
,NULL FCount    --�ݲ�����
,YW.CreateTime FTime    --������ʱ��,ҵ����ʱû���ϱ���û������
,0 FIsDeleted    --�Ƿ�ɾ��
,YW.CreateTime FCreateTime    --����ʱ��
,1 FReportCount    --�ݲ�����
,NULL FToBaseinfoId    --�ݲ�����
,NULL FAppDate    --�ݲ�����
,NULL FLinkAppId    --�ݲ�����
,NULL FBarCode    --�ݲ�����
,NULL FCreateUser    --�ݲ�����
,NULL FgfTime    --�ݲ�����
FROM JKCWFDB_WORK_NJS.DBO.YW_ZLJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_YWInfo YW ON YW.YWBM=B.YWBM 
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
AND ISNULL(B.XMMC,'') <>'' 
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (391,392));



-----------------------------------------------------------------------------------------------------------------
--�����ʼ౸��������Ϣ
insert into dbCenter.dbo.TC_QA_Record
(FId, FAppId, FPrjId, FPrjItemId, FJSDWID, RecordNo, ProjectName, JSDW, LegalPerson, PrjItemName,
 Province, city, county, AddressDept, Address, PrjItemType, Area, ConstrType, ProjectNumber, RegisterTime,
 Contracts, Mobile, SGDWId, SGDW, SGDWDZ, SGDWDH, SGDWFR, SGDWZS, XMJL, jldwId,
 JLDW, JLDWDZ, JLDWFR, JLDWDH, JLZS, XMZJ, sjdwId, SJDW, SJDWDZ, SJDWFR,
 SJDWDH, SJDWZS, JZS, JGS, kcdwId, CCDW, CCDWDZ, CCDWDH, CCDWFR, CCDWZS,
 YTGCS, Remark)
select 
a.GuidID FId,                    --����
a.YWBM  FAppId,                  --ҵ�����
b.GuidID FPrjId,                   --��Ŀ����
c.GuidID FPrjItemId,               --������� 
b.jsdw FJSDWID,                  --���赥λid
a.BAH RecordNo,                  --������
b.xmmc ProjectName,              --��Ŀ����
b.xmjsdw JSDW,                   --���赥λ����
b.jsdwfr LegalPerson,            --���赥λ����
c.name PrjItemName,              --��������
b.xmszd Province,                --��������
null city,
null county,
null AddressDept,
b.XMDZ Address,                  --���̵ص�
CASE WHEN ISNULL(a.GCXZ,'0')='6' THEN '2000101'
WHEN ISNULL(a.GCXZ,'0')='5' THEN '2000102'
WHEN ISNULL(a.GCXZ,'0')='7' THEN '2000103'
ELSE '2000103' END PrjItemType,              --��������
d.ZMJ  Area,                     --���������m2��

CASE WHEN ISNULL(cast(d.cssj as varchar(500)), '0')='שľ�ṹ'  THEN '509001'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='ש��ṹ' THEN '509002'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='�ֽ�������ṹ' THEN '509003'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='�ֽṹ' THEN '509004'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='��ܽṹ' THEN '509005'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='����ǽ�ṹ' THEN '509006' 
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='���-����ǽ�ṹ' THEN '509007' 
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='��-Ͳ�ṹ' THEN '509008' 
ELSE '509010' END ConstrType,    --�ṹ����
           
a.lxwh ProjectNumber,            --�����
a.lxsj RegisterTime,             --����ʱ��
a.lxr Contracts,                 --��ϵ��
a.lxdh Mobile,                   --��ϵ�绰

f.qyid SGDWId,                   --ʩ����λid
f.dwmc SGDW,                     --ʩ����λ
f.DWDZ SGDWDZ,                   --ʩ����λ��ַ
f.LXDH SGDWDH,                   --��ϵ�绰
f.FDDBR SGDWFR,                  --ʩ����λ����
f.ZXZZ SGDWZS,                   --����֤���
f.XMFZR XMJL,                    --��Ŀ����

g.qyid jldwId,                   --����λid
g.DWMC JLDW,                     --����λ
g.dwdz JLDWDZ,                   --����λ��ַ
g.FDDBR JLDWFR,                  --����λ����
g.LXDH JLDWDH,                   --��ϵ�绰
g.ZXZZ JLZS,                     --֤����
g.XMZJ XMZJ,                     --��Ŀ����

h.qyid sjdwId,                   --��Ƶ�λid
h.DWMC SJDW,                     --��Ƶ�λ
h.DWDZ SJDWDZ,                   --��Ƶ�λ��ַ
h.FDDBR SJDWFR,                  --��Ƶ�λ����
h.LXDH SJDWDH,                   --��ϵ�绰
h.ZXZZ SJDWZS,                   --֤����
h.LXR JZS,                       --��Ŀ���ע�Ὠ��ʦ
h.ZZJGDM JGS,                    --��Ŀ���ע��ṹʦ

i.QYID kcdwId,                   --���쵥λid
i.DWMC CCDW,                     --���쵥λ
i.DWDZ CCDWDZ,                   --���쵥λ��ַ
i.LXDH CCDWDH,                   --��ϵ�绰
i.FDDBR CCDWFR,                  --���쵥λ����
i.ZXZZ CCDWZS,                   --֤����
i.LXR YTGCS,                     --ע����������ʦ

a.bz Remark                      --��ע

from JKCWFDB_WORK_NJS.dbo.YW_ZLJDBAinfo a
left join JKCWFDB_WORK_NJS.dbo.YW_XMInfo b on a.YWBM = b.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_XMFQInfo c on a.YWBM = c.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_JSZBInfo d on a.YWBM = d.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_SGZCBDWInfo f on a.YWBM = f.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_JLDW g on a.YWBM = g.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_SJDW h on a.YWBM = h.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_KCDW i on a.YWBM = i.YWBM
WHERE  b.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
   AND ISNULL(b.xmmc,'') <>'' 
   AND a.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01' and C06.actionid in (391,392))
-----------------------------------------------------------------------------------------------------------------



--ɾ�����������Ϣ
--ҵ������
Delete dbCenter.dbo.CF_App_List where FId in 
(SELECT 
BA.YWBM    --ҵ�����
FROM JKCWFDB_WORK_NJS.DBO.YW_AQJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (413,414))
  );

--��Ŀ��
delete from  dbCenter.dbo.TC_Prj_Info where FId in (
SELECT --A.*
A.GuidId         --��Ŀid
FROM JKCWFDB_WORK_NJS.DBO.YW_XMInfo AS A
WHERE A.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (413,414))
);

--���幤�̱�
delete from dbCenter.dbo.TC_PrjItem_Info where FId in (
SELECT 
FQ.GuidId
FROM JKCWFDB_WORK_NJS.DBO.YW_AQJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (413,414))
);

--���౸����
delete from dbCenter.dbo.TC_AJBA_Record where FId in (
SELECT
a.GuidID
from JKCWFDB_WORK_NJS.dbo.YW_AQJDBAinfo a
left join JKCWFDB_WORK_NJS.dbo.YW_XMInfo b on a.YWBM = b.YWBM
WHERE  b.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
   AND a.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01' and C06.actionid in (413,414))
);

-----------------------------------------------------------------------------------------------------------------
--���밲����Ŀ��
INSERT INTO dbCenter.dbo.TC_Prj_Info
SELECT --A.*
A.GuidId FId         --��Ŀid
,A.XMJSDW  JSDW         --���赥λ����
,JS.ZZJGDM  JSDWDM         --���赥λ��֯��������
,JS.DWDZ  JSDWDZ         --���赥λ��ַ
,JS.LXR   Contacts         --���赥λ��ϵ��
,JS.LXDH Mobile         --���赥λ��ϵ�绰
,A.XMMC ProjectName         --��Ŀ����
,CASE WHEN A.XMSZD LIKE '51%' THEN LEFT(A.XMSZD,2) ELSE NULL END Province         --��Ŀ����-ʡ
,CASE WHEN A.XMSZD LIKE '51%' AND LEN(A.XMSZD)>=4 THEN LEFT(A.XMSZD,4) ELSE NULL END City         --��Ŀ����-����
,CASE WHEN A.XMSZD LIKE '51%' AND LEN(A.XMSZD)>=6 THEN LEFT(A.XMSZD,6) ELSE NULL END County         --��Ŀ����-����
,CASE WHEN ISNULL(LX.XMLX,'0')='1' THEN '2000101'
WHEN ISNULL(LX.XMLX,'0')='2' THEN '2000102'
WHEN ISNULL(LX.XMLX,'0')='3' THEN '2000103'
ELSE '2000103' END ProjectType         --��Ŀ���:2000101--���ݽ���;2000102--��������;2000103--����
,A.XMDZ Address         --��Ŀ��ַ
,BA.LXWH ProjectNumber         --�����ĺ�
,CASE WHEN ISNULL(LX.LXJB,'0')='1' THEN '11220401'
WHEN ISNULL(LX.LXJB,'0')='2' THEN '11220402'
WHEN ISNULL(LX.LXJB,'0')='3' THEN '11220403'
WHEN ISNULL(LX.LXJB,'0')='4' THEN '112204034'
ELSE '112204035' END ProjectLevel         --�����
,BA.LXSJ ProjectTime         --��Ŀʱ��
,A.XMBH ProjectNo         --��Ŀ���
,NULL IsForeign         --�Ƿ�����
,NULL JSYDXKZ         --�����õ����֤��
,NULL JSGCXKZ         --���̹滮���֤��
,NULL Area         --�����������m2��
,BA.GCZZJ Investment         --Ͷ�ʹ�ģ(��Ԫ)
,CASE WHEN ISNULL(LX.JZXZ,'0')='�½�' THEN '30503'
WHEN ISNULL(LX.JZXZ,'0')='�Ľ�' THEN '30501'
WHEN ISNULL(LX.JZXZ,'0')='����' THEN '30502'
ELSE NULL END ConstrType         --�������ʣ�2000501--�½���2000502--�Ľ�
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
,CASE WHEN ISNULL(SG.GCLB,'0')='01' OR ISNULL(SG.GCLB,'0')='02' OR ISNULL(SG.GCLB,'0')='03' OR
ISNULL(SG.GCLB,'0')='04' OR ISNULL(SG.GCLB,'0')='05' OR ISNULL(SG.GCLB,'0')='06' OR
ISNULL(SG.GCLB,'0')='07' OR ISNULL(SG.GCLB,'0')='08' OR ISNULL(SG.GCLB,'0')='09' OR ISNULL(SG.GCLB,'0')='10' THEN '200010113'
WHEN ISNULL(SG.GCLB,'0')='11' OR ISNULL(SG.GCLB,'0')='12' OR ISNULL(SG.GCLB,'0')='13' THEN '200010209'
WHEN ISNULL(SG.GCLB,'0')='14' OR ISNULL(SG.GCLB,'0')='15' THEN '200010210' 
WHEN ISNULL(SG.GCLB,'0')='16' THEN '200010204'
WHEN ISNULL(SG.GCLB,'0')='17' THEN '200010201'
WHEN ISNULL(SG.GCLB,'0')='18' THEN '200010206'
WHEN ISNULL(SG.GCLB,'0')='19' THEN '2000103'
ELSE NULL END ProjectUse         --������;��200010201--��ˮ��200010205--��·
,SG.SJKGRQ StartDate         --ʵ�ʿ�������
,SG.SJJGRQ EndDate         --ʵ�ʿ�������
,NULL RegisterTime         --��¼�Ǽ�ʱ��
,NULL  ConstrBasis         --��������
,NULL ConstrContent         --��������
,A.JSDW FJSDWID --���赥λ���
,A.XMSZD AddressDept    --��ַ���
,CASE WHEN ISNULL(SG.JSGM,0)<>0 THEN '���' + CONVERT(VARCHAR(20),SG.JSGM) + '��m2��'
WHEN ISNULL(SG.KDGD,0)<>0 THEN '��ȣ��߶ȣ�' + CONVERT(VARCHAR(20),SG.KDGD) + '��m��'
ELSE NULL END ConstrScale--�����ģ
,NULL LandType  --�õ�����
--�½�	2000501
--�Ľ�	2000502
--����	2000503
--�ؽ�	2000504
--Ǩ��	2000505
--�ָ�	2000506
--����	2000507
,JS.FRXM JSDWFR         --���赥λ����
FROM JKCWFDB_WORK_NJS.DBO.YW_XMInfo AS A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=A.JSDW
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_AQJDBAinfo AS BA ON BA.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMLXInfo  AS LX ON LX.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS SG ON SG.YWBM=A.YWBM
WHERE A.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
AND ISNULL(A.XMMC,'') <>'' 
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (413,414));
-----------------------------------------------------------------------------------------------------------------
--���밲�൥�幤�̱�
INSERT INTO dbCenter.dbo.TC_PrjItem_Info
SELECT 
FQ.GuidId FId         --���幤��id
,B.GuidId FPrjId         --����TC_Prj_Info.FId
,B.XMMC ProjectName         --��Ŀ����
,B.XMDZ Address         --��Ŀ��ַ
,JZ.ZMJ Area         --���������
,SG.HTJG Investment         --Ͷ�ʹ�ģ(��Ԫ)
,B.XMJSDW JSDW         --���赥λ����
,JS.FRXM LegalPerson         --���˴���
,JS.ZZJGDM JSDWDM         --���赥λ��֯��������
,isnull(FQ.Name ,'����������')  as PrjItemName      --��������
,CASE WHEN ISNULL(LX.XMLX,'0')='1' THEN '2000101'
WHEN ISNULL(LX.XMLX,'0')='2' THEN '2000102'
WHEN ISNULL(LX.XMLX,'0')='3' THEN '2000103'
ELSE '2000103' END PrjItemType --�������          --�������2000101--���ݽ���;2000102--��������;2000103--����
--שľ�ṹ	509001
--ש��ṹ	509002
--�ֽ�������ṹ	509003
--�ֽṹ	509004
--��ܽṹ	509005
--����ǽ�ṹ	509006
--���-����ǽ�ṹ	509007
--��-Ͳ�ṹ	509008
--����	509010
,CASE WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='שľ�ṹ' THEN '509001' 
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='ש��ṹ' THEN '509002'
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='�ֽ�������ṹ' THEN '509003'
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='�ֽṹ' THEN '509004'
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='��ܽṹ' THEN '509005'
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='����ǽ�ṹ' THEN '509006' 
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='���-����ǽ�ṹ' THEN '509007' 
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='��-Ͳ�ṹ' THEN '509008' 
ELSE '509010' END ConstrType         --�ṹ���ͣ�509001��509002��509005
,BA.GCZZJ Cost         --������ۣ���Ԫ��
,NULL Scale         --���̹�ģ
,NULL PrjItemDesc         --��������
,JS.FID FJSDWID         --���赥λ���
,B.XMSZD AddressDept         --��ַ���
,CASE WHEN ISNULL(SG.JSGM,0)<>0 THEN '���' + CONVERT(VARCHAR(20),SG.JSGM) + '��m2��'
WHEN ISNULL(SG.KDGD,0)<>0 THEN '��ȣ��߶ȣ�' + CONVERT(VARCHAR(20),SG.KDGD) + '��m��'
ELSE NULL END ConstrScale--�����ģ
FROM JKCWFDB_WORK_NJS.DBO.YW_AQJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMLXInfo  AS LX ON LX.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS SG ON SG.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_JSZBInfo AS JZ ON JZ.YWBM=BA.YWBM
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
AND ISNULL(B.XMMC,'') <>'' 
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (413,414));
-----------------------------------------------------------------------------------------------------------------
--���밲��ҵ������
INSERT INTO dbCenter.dbo.CF_App_List
SELECT 
BA.YWBM FId    --ҵ�����
,B.JSDW FBaseinfoId    --��ҵid
,B.GuidId FPrjId    --��Ŀ����
,CONVERT(VARCHAR(20),YEAR(YW.CreateTime)) + '�� ��ȫ�ල����' FName    --ҵ������
,11222 FManageTypeId    --ҵ�����
,YW.CreateTime FwriteDate    --д��ʱ��
,BA.LXSJ FReportDate    --�ϱ�ʱ��
,NULL FIsSign    --�Ƿ�ǩ��
,0 FState    --ҵ��״̬
,NULL FResult    --��������
,YEAR(BA.LXSJ) FYear    --���
,MONTH(BA.LXSJ) FMonth    --�·�
,FQ.GuidId FLinkId    --������̱���
,JS.FCompany FBaseName    --���赥λ����
,NULL FUpDeptId    --�ϱ����ŵ�������
,NULL FRemark    --�ݲ�����
,NULL FIsCheck    --�ݲ�����
,NULL FCount    --�ݲ�����
,YW.CreateTime FTime    --������ʱ��,ҵ����ʱû���ϱ���û������
,0 FIsDeleted    --�Ƿ�ɾ��
,YW.CreateTime FCreateTime    --����ʱ��
,1 FReportCount    --�ݲ�����
,NULL FToBaseinfoId    --�ݲ�����
,NULL FAppDate    --�ݲ�����
,NULL FLinkAppId    --�ݲ�����
,NULL FBarCode    --�ݲ�����
,NULL FCreateUser    --�ݲ�����
,NULL FgfTime    --�ݲ�����
FROM JKCWFDB_WORK_NJS.DBO.YW_AQJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_YWInfo YW ON YW.YWBM=B.YWBM 
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
AND ISNULL(B.XMMC,'') <>'' 
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (413,414));
  
  -----------------------------------------------------------------------------------------------------------------
--���밲�౸��������Ϣ��
insert into dbCenter.dbo.TC_AJBA_Record              --Ϊʲô������Ҫ��dbCenter_sc.�أ�����
(FId,FAppId,FPrjId,FPrjItemId,FJSDWID,ProjectName,JSDW,LegalPerson,PrjItemName,
 Province,City,County,Address,AddressDept,Area,ConstrType,Floor,Cost,PlanLimit,RecordNo,ProjectNo,ProjectTime,Contracts,
 Mobile,SGId,SGDW,SGDWDH,SGDWFR,SGDWZZZSH,SGDWXMJL,JLId,JLDW,JLDWDH,JLDWFR,Remark)
select 
a.GuidID FId,                    --����
a.YWBM  FAppId,                  --ҵ�����
b.GuidID FPrjId,                 --���̱���
c.GuidID FPrjItemId,             --�������
b.jsdw FJSDWID,                  --���赥λid
b.XMMC ProjectName,              --��Ŀ����
b.XMJSDW JSDW,                   --���赥λ����
b.JSDWFR LegalPerson,            --���赥λ����
c.name PrjItemName,              --��������
'' Province,                --��Ŀʩ����
'' City,
'' County,
b.xmdz Address,                   --���̵ص�
b.xmszd AddressDept,             
d.zmj Area,                      --�������(�O)

CASE WHEN ISNULL(cast(d.cssj as varchar(500)), '0')='שľ�ṹ'  THEN '509001'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='ש��ṹ' THEN '509002'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='�ֽ�������ṹ' THEN '509003'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='�ֽṹ' THEN '509004'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='��ܽṹ' THEN '509005'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='����ǽ�ṹ' THEN '509006' 
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='���-����ǽ�ṹ' THEN '509007' 
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='��-Ͳ�ṹ' THEN '509008' 
ELSE '509010' END ConstrType,    --�ṹ����

a.jzcs Floor,                    --�����������㣩
a.gczzj Cost,                    --��������ۣ���Ԫ��
a.jhsgqx PlanLimit,              --�ƻ�ʩ������
a.djbah RecordNo,                --�ǼǱ�����
a.lxwh ProjectNo,                --�����ĺ�
a.lxsj ProjectTime,              --��������
a.lxr Contracts,                 --��ϵ��
a.lxdh Mobile,                   --��ϵ�绰
e.qyid SGId,                   --ʩ����λid
e.dwmc SGDW,                     --ʩ����λ����
e.lxdh SGDWDH,                   --ʩ����λ�绰
e.fddbr SGDWFR,                  --ʩ����λ����
e.zxzz SGDWZZZSH,                --����֤���
e.xmfzr SGDWXMJL,                --��Ŀ����
f.QYID JLId,                   --����λid
f.DWMC JLDW,                     --����λ
f.LXDH JLDWDH,                   --��ϵ�绰
f.FDDBR JLDWFR,                  --����λ����
a.BZ Remark                      --��ע
from JKCWFDB_WORK_NJS.dbo.YW_AQJDBAinfo a
left join JKCWFDB_WORK_NJS.dbo.YW_XMInfo b on a.YWBM = b.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_XMFQInfo c on a.YWBM = c.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_JSZBInfo d on a.YWBM = d.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_SGZCBDWInfo e on a.ywbm = e.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_JLDW f on a.YWBM = f.YWBM
WHERE  b.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
AND ISNULL(b.XMMC,'') <>'' 
   AND a.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01' and C06.actionid in (413,414));    
-----------------------------------------------------------------------------------------------------------------




--���������б걸���Ĺ�������
--CF_App_ProcessInstance   ���������̱�
--CF_App_ProcessRecord     ���������̱�

if not exists (select 1
            from  sysobjects
           where  id = object_id('dbo._App_List_QA')
            and   type = 'U')
begin
    select * into _App_List_QA from dbCenter.dbo.CF_App_List where 1=2
end
else
begin
   delete _App_List_QA
end 

INSERT INTO dbCenter.dbo._App_List_QA
SELECT 
BA.YWBM FId    --ҵ�����
,B.JSDW FBaseinfoId    --��ҵid
,B.GuidId FPrjId    --��Ŀ����
,CONVERT(VARCHAR(20),YEAR(YW.CreateTime)) + '�� �����ල����' FName    --ҵ������
,11221 FManageTypeId    --ҵ�����
,YW.CreateTime FwriteDate    --д��ʱ��
,BA.LXSJ FReportDate    --�ϱ�ʱ��
,NULL FIsSign    --�Ƿ�ǩ��
,0 FState    --ҵ��״̬
,NULL FResult    --��������
,YEAR(BA.LXSJ) FYear    --���
,MONTH(BA.LXSJ) FMonth    --�·�
,FQ.GuidId FLinkId    --������̱���
,JS.FCompany FBaseName    --���赥λ����
,NULL FUpDeptId    --�ϱ����ŵ�������
,NULL FRemark    --�ݲ�����
,NULL FIsCheck    --�ݲ�����
,NULL FCount    --�ݲ�����
,YW.CreateTime FTime    --������ʱ��,ҵ����ʱû���ϱ���û������
,0 FIsDeleted    --�Ƿ�ɾ��
,YW.CreateTime FCreateTime    --����ʱ��
,1 FReportCount    --�ݲ�����
,NULL FToBaseinfoId    --�ݲ�����
,NULL FAppDate    --�ݲ�����
,NULL FLinkAppId    --�ݲ�����
,NULL FBarCode    --�ݲ�����
,NULL FCreateUser    --�ݲ�����
,NULL FgfTime    --�ݲ�����
FROM JKCWFDB_WORK_NJS.DBO.YW_ZLJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_YWInfo YW ON YW.YWBM=B.YWBM 
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
AND ISNULL(B.XMMC,'') <>'' 
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (391,392));


if not exists (select 1
            from  sysobjects
           where  id = object_id('dbo._App_ProcessInstance_QA')
            and   type = 'U')
begin 
    SELECT * into _App_ProcessInstance_QA from CF_App_ProcessInstance where 1=2  --ֻȡ��ṹ
    alter table _App_ProcessInstance_QA alter column FentName varchar(200) null       

	   insert into _App_ProcessInstance_QA
	   (fid,FisDeleted,Ftime,FCreateTime,FBaseInfoID,FEntName,FEmpId,FLinkId,FState,FIsPrime,
			  FIsTemp,FListId,FTypeId,FLevelId,FProcessId,FManageDeptId,FManageTypeId,--FSubFlowId,
			  FResult,Fyear,FMonth,FSubmitDate,FReportDate,FCurStepID,FroleId,FBeginRoleId,FDefineDay,
			  FAppState,FSystemId,FIsNew,FSeeState,fseetime,FPlanTime,FFactTime,FBarCode)
       select newid(),0,a.FwriteDate,a.FwriteDate,a.FBaseinfoId,a.FBaseName,a.FLinkId as FEmpId,a.Fid,1,0,
	          0,'19301','1930100','1930100','927fade8-5741-41d9-9131-0f891816325a',isnull(a.fupdeptid,'51'),a.FManageTypeId,
			  '',year(a.FwriteDate),month(a.FwriteDate),a.FReportDate,a.FReportDate,isnull(a.fupdeptid,'51'),'903','903',1,1,'1122',0,null,null,null,null,null
	     from _App_List_QA a
		where  not exists(select 1 from CF_App_ProcessInstance b where a.FId = b.FLinkId)
end 

  alter table CF_App_ProcessInstance alter column Fentname varchar(200) null

  insert into CF_App_ProcessInstance
             (fid,FisDeleted,Ftime,FCreateTime,FBaseInfoID,FEntName,FEmpId,FLinkId,FState,FIsPrime,
			  FIsTemp,FListId,FTypeId,FLevelId,FProcessId,FManageDeptId,FManageTypeId,--FSubFlowId,
			  FResult,Fyear,FMonth,FSubmitDate,FReportDate,FCurStepID,FroleId,FBeginRoleId,FDefineDay,
			  FAppState,FSystemId,FIsNew,FSeeState,fseetime,FPlanTime,FFactTime,FBarCode)
       select fid,FisDeleted,Ftime,FCreateTime,FBaseInfoID,FEntName,FEmpId,FLinkId,FState,FIsPrime,
			  FIsTemp,FListId,FTypeId,FLevelId,FProcessId,FManageDeptId,FManageTypeId,--FSubFlowId,
			  FResult,Fyear,FMonth,FSubmitDate,FReportDate,FCurStepID,FroleId,FBeginRoleId,FDefineDay,
			  FAppState,FSystemId,FIsNew,FSeeState,fseetime,FPlanTime,FFactTime,FBarCode
         from _App_ProcessInstance_QA a
		where not exists(select 1 from CF_App_ProcessInstance b where a.FId = b.fid)


if not exists (select 1
            from  sysobjects
           where  id = object_id('dbo._App_ProcessRecord_QA')
            and   type = 'U')
begin

  select * into _App_ProcessRecord_QA from CF_App_ProcessRecord where 1=2

  insert into _App_ProcessRecord_QA
              (fid,FTime,FIsDeleted,FProcessInstanceID,FLinkId,FSubFlowId,FMeasure,Fresult,FManageDeptId,
			  FReportTime,FDefineDay,FRoleId,FLevel,FOrder,FRoleDesc,FTypeId,FIsQuali,FIsPrint )
	   select newid(),a.FSubmitDate,0,a.fid,a.FLinkId,a.FSubFlowId,0,null,isnull(FManageDeptId,'51'),
	          a.FReportDate,1,'8801',1,1,'���������',1,2,1
	     from _App_ProcessInstance_QA a
		where  not exists(select 1 from CF_App_ProcessRecord b where a.FId = b.FProcessInstanceID)


end 

  insert into CF_App_ProcessRecord
              (fid,FTime,FIsDeleted,FProcessInstanceID,FLinkId,FSubFlowId,FMeasure,Fresult,FManageDeptId,
			  FReportTime,FDefineDay,FRoleId,FLevel,FOrder,FRoleDesc,FTypeId,FIsQuali,FIsPrint )
       select fid,FTime,FIsDeleted,FProcessInstanceID,FLinkId,FSubFlowId,FMeasure,Fresult,FManageDeptId,
			  FReportTime,FDefineDay,FRoleId,FLevel,FOrder,FRoleDesc,FTypeId,FIsQuali,FIsPrint
		 from _App_ProcessRecord_QA a
	    where not exists(select 1 from CF_App_ProcessRecord b where a.FId = b.fid)



if not exists (select 1
            from  sysobjects
           where  id = object_id('dbo._App_List_AJBA')
            and   type = 'U')
begin
    select * into _App_List_AJBA from dbCenter.dbo.CF_App_List where 1=2
end
else
begin
   delete _App_List_AJBA
end 

INSERT INTO dbCenter.dbo._App_List_AJBA
SELECT 
BA.YWBM FId    --ҵ�����
,B.JSDW FBaseinfoId    --��ҵid
,B.GuidId FPrjId    --��Ŀ����
,CONVERT(VARCHAR(20),YEAR(YW.CreateTime)) + '�� ��ȫ�ල����' FName    --ҵ������
,11222 FManageTypeId    --ҵ�����
,YW.CreateTime FwriteDate    --д��ʱ��
,BA.LXSJ FReportDate    --�ϱ�ʱ��
,NULL FIsSign    --�Ƿ�ǩ��
,0 FState    --ҵ��״̬
,NULL FResult    --��������
,YEAR(BA.LXSJ) FYear    --���
,MONTH(BA.LXSJ) FMonth    --�·�
,FQ.GuidId FLinkId    --������̱���
,JS.FCompany FBaseName    --���赥λ����
,NULL FUpDeptId    --�ϱ����ŵ�������
,NULL FRemark    --�ݲ�����
,NULL FIsCheck    --�ݲ�����
,NULL FCount    --�ݲ�����
,YW.CreateTime FTime    --������ʱ��,ҵ����ʱû���ϱ���û������
,0 FIsDeleted    --�Ƿ�ɾ��
,YW.CreateTime FCreateTime    --����ʱ��
,1 FReportCount    --�ݲ�����
,NULL FToBaseinfoId    --�ݲ�����
,NULL FAppDate    --�ݲ�����
,NULL FLinkAppId    --�ݲ�����
,NULL FBarCode    --�ݲ�����
,NULL FCreateUser    --�ݲ�����
,NULL FgfTime    --�ݲ�����
FROM JKCWFDB_WORK_NJS.DBO.YW_AQJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_YWInfo YW ON YW.YWBM=B.YWBM 
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
AND ISNULL(B.XMMC,'') <>'' 
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (413,414));

if not exists (select 1
            from  sysobjects
           where  id = object_id('dbo._App_ProcessInstance_AJBA')
            and   type = 'U')
begin 
    SELECT * into _App_ProcessInstance_AJBA from CF_App_ProcessInstance where 1=2  --ֻȡ��ṹ
    alter table _App_ProcessInstance_AJBA alter column FentName varchar(200) null       

	   insert into _App_ProcessInstance_AJBA
	   (fid,FisDeleted,Ftime,FCreateTime,FBaseInfoID,FEntName,FEmpId,FLinkId,FState,FIsPrime,
			  FIsTemp,FListId,FTypeId,FLevelId,FProcessId,FManageDeptId,FManageTypeId,--FSubFlowId,
			  FResult,Fyear,FMonth,FSubmitDate,FReportDate,FCurStepID,FroleId,FBeginRoleId,FDefineDay,
			  FAppState,FSystemId,FIsNew,FSeeState,fseetime,FPlanTime,FFactTime,FBarCode)
       select newid(),0,a.FwriteDate,a.FwriteDate,a.FBaseinfoId,a.FBaseName,a.FLinkId as FEmpId,a.Fid,1,0,
	          0,'19301','1930100','1930100','8940a75c-b8a5-4de2-be85-15412132e0f1',isnull(a.fupdeptid,'51'),a.FManageTypeId,
			  '',year(a.FwriteDate),month(a.FwriteDate),a.FReportDate,a.FReportDate,isnull(a.fupdeptid,'51'),'8801','8801',1,1,'1122',0,null,null,null,null,null
	     from _App_List_AJBA a
		where  not exists(select 1 from CF_App_ProcessInstance b where a.FId = b.FLinkId)
end 

  insert into CF_App_ProcessInstance
             (fid,FisDeleted,Ftime,FCreateTime,FBaseInfoID,FEntName,FEmpId,FLinkId,FState,FIsPrime,
			  FIsTemp,FListId,FTypeId,FLevelId,FProcessId,FManageDeptId,FManageTypeId,--FSubFlowId,
			  FResult,Fyear,FMonth,FSubmitDate,FReportDate,FCurStepID,FroleId,FBeginRoleId,FDefineDay,
			  FAppState,FSystemId,FIsNew,FSeeState,fseetime,FPlanTime,FFactTime,FBarCode)
       select fid,FisDeleted,Ftime,FCreateTime,FBaseInfoID,FEntName,FEmpId,FLinkId,FState,FIsPrime,
			  FIsTemp,FListId,FTypeId,FLevelId,FProcessId,FManageDeptId,FManageTypeId,--FSubFlowId,
			  FResult,Fyear,FMonth,FSubmitDate,FReportDate,FCurStepID,FroleId,FBeginRoleId,FDefineDay,
			  FAppState,FSystemId,FIsNew,FSeeState,fseetime,FPlanTime,FFactTime,FBarCode
         from _App_ProcessInstance_AJBA a
		where not exists(select 1 from CF_App_ProcessInstance b where a.FId = b.fid)


if not exists (select 1
            from  sysobjects
           where  id = object_id('dbo._App_ProcessRecord_AJBA')
            and   type = 'U')
begin

  select * into _App_ProcessRecord_AJBA from CF_App_ProcessRecord where 1=2

  insert into _App_ProcessRecord_AJBA
              (fid,FTime,FIsDeleted,FProcessInstanceID,FLinkId,FSubFlowId,FMeasure,Fresult,FManageDeptId,
			  FReportTime,FDefineDay,FRoleId,FLevel,FOrder,FRoleDesc,FTypeId,FIsQuali,FIsPrint )
	   select newid(),a.FSubmitDate,0,a.fid,a.FLinkId,a.FSubFlowId,0,null,isnull(FManageDeptId,'51'),
	          a.FReportDate,1,'8801',1,1,'���������',1,2,1
	     from _App_ProcessInstance_AJBA a
		where  not exists(select 1 from CF_App_ProcessRecord b where a.FId = b.FProcessInstanceID)
end 

  insert into CF_App_ProcessRecord
              (fid,FTime,FIsDeleted,FProcessInstanceID,FLinkId,FSubFlowId,FMeasure,Fresult,FManageDeptId,
			  FReportTime,FDefineDay,FRoleId,FLevel,FOrder,FRoleDesc,FTypeId,FIsQuali,FIsPrint )
       select fid,FTime,FIsDeleted,FProcessInstanceID,FLinkId,FSubFlowId,FMeasure,Fresult,FManageDeptId,
			  FReportTime,FDefineDay,FRoleId,FLevel,FOrder,FRoleDesc,FTypeId,FIsQuali,FIsPrint
		 from _App_ProcessRecord_AJBA a
	    where not exists(select 1 from CF_App_ProcessRecord b where a.FId = b.fid)