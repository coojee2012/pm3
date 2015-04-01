--删除质监数据信息
-----------------------------------------------------------------------------------------------------------------
--业务主表
  Delete dbCenter.dbo.CF_App_List where FId in 
(SELECT 
BA.YWBM    --业务编码
FROM JKCWFDB_WORK_NJS.DBO.YW_ZLJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (391,392))
  );

--项目表
delete from  dbCenter.dbo.TC_Prj_Info where FId in (
SELECT --A.*
A.GuidId         --项目id
FROM JKCWFDB_WORK_NJS.DBO.YW_XMInfo AS A
WHERE A.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (391,392))
);

--单体工程表
delete from dbCenter.dbo.TC_PrjItem_Info where FId in (
SELECT 
FQ.GuidId
FROM JKCWFDB_WORK_NJS.DBO.YW_ZLJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (391,392))
);

--质监备案表
delete from dbCenter.dbo.TC_QA_Record where FId in (
SELECT
a.GuidID
from JKCWFDB_WORK_NJS.dbo.YW_ZLJDBAinfo a
left join JKCWFDB_WORK_NJS.dbo.YW_XMInfo b on a.YWBM = b.YWBM
WHERE  b.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
   AND a.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01' and C06.actionid in (391,392))
);

/*****   end  *****/
-----------------------------------------------------------------------------------------------------------------

--导入质监相关的项目表
INSERT INTO dbCenter.dbo.TC_Prj_Info
SELECT --A.*
A.GuidId FId         --项目id
,A.XMJSDW  JSDW         --建设单位名称
,JS.ZZJGDM  JSDWDM         --建设单位组织机构代码
,JS.DWDZ  JSDWDZ         --建设单位地址
,JS.LXR   Contacts         --建设单位联系人
,JS.LXDH Mobile         --建设单位联系电话
,A.XMMC ProjectName         --项目名称
,CASE WHEN A.XMSZD LIKE '51%' THEN LEFT(A.XMSZD,2) ELSE NULL END Province         --项目属地-省
,CASE WHEN A.XMSZD LIKE '51%' AND LEN(A.XMSZD)>=4 THEN LEFT(A.XMSZD,4) ELSE NULL END City         --项目属地-市州
,CASE WHEN A.XMSZD LIKE '51%' AND LEN(A.XMSZD)>=6 THEN LEFT(A.XMSZD,6) ELSE NULL END County         --项目属地-区县
,CASE WHEN ISNULL(BA.GCXZ,'0')='6' THEN '2000101'
WHEN ISNULL(BA.GCXZ,'0')='5' THEN '2000102'
WHEN ISNULL(BA.GCXZ,'0')='7' THEN '2000103'
ELSE '2000103' END ProjectType         --项目类别:2000101--房屋建筑;2000102--市政基础;2000103--其他
,A.XMDZ Address         --项目地址
,BA.LXWH ProjectNumber         --立项文号
,CASE WHEN ISNULL(LX.LXJB,'0')='1' THEN '11220401'
WHEN ISNULL(LX.LXJB,'0')='2' THEN '11220402'
WHEN ISNULL(LX.LXJB,'0')='3' THEN '11220403'
WHEN ISNULL(LX.LXJB,'0')='4' THEN '112204034'
ELSE '112204035' END ProjectLevel         --立项级别
,BA.LXSJ ProjectTime         --项目时间
,A.XMBH ProjectNo         --项目编号
,NULL IsForeign         --是否涉外
,NULL JSYDXKZ         --建设用地许可证号
,NULL JSGCXKZ         --工程规划许可证号
,JZ.ZMJ Area         --建设总面积（m2）
,NULL Investment         --投资规模(万元)
,CASE WHEN ISNULL(LX.JZXZ,'0')='新建' THEN '30503'
WHEN ISNULL(LX.JZXZ,'0')='改建' THEN '30501'
WHEN ISNULL(LX.JZXZ,'0')='扩建' THEN '30502'
ELSE NULL END ConstrType         --建设性质：2000501--新建；2000502--改建
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
,CASE WHEN ISNULL(SG.GCLB,'0')='01' OR ISNULL(SG.GCLB,'0')='02' OR ISNULL(SG.GCLB,'0')='03' OR
ISNULL(SG.GCLB,'0')='04' OR ISNULL(SG.GCLB,'0')='05' OR ISNULL(SG.GCLB,'0')='06' OR
ISNULL(SG.GCLB,'0')='07' OR ISNULL(SG.GCLB,'0')='08' OR ISNULL(SG.GCLB,'0')='09' OR ISNULL(SG.GCLB,'0')='10' THEN '200010113'
WHEN ISNULL(SG.GCLB,'0')='11' OR ISNULL(SG.GCLB,'0')='12' OR ISNULL(SG.GCLB,'0')='13' THEN '200010209'
WHEN ISNULL(SG.GCLB,'0')='14' OR ISNULL(SG.GCLB,'0')='15' THEN '200010210' 
WHEN ISNULL(SG.GCLB,'0')='16' THEN '200010204'
WHEN ISNULL(SG.GCLB,'0')='17' THEN '200010201'
WHEN ISNULL(SG.GCLB,'0')='18' THEN '200010206'
WHEN ISNULL(SG.GCLB,'0')='19' THEN '2000103'
ELSE NULL END ProjectUse         --工程用途：200010201--给水；200010205--道路
,SG.SJKGRQ StartDate         --实际开工日期
,SG.SJJGRQ EndDate         --实际竣工日期
,NULL RegisterTime         --记录登记时间
,NULL  ConstrBasis         --建设依据
,NULL ConstrContent         --建设内容
,A.JSDW FJSDWID --建设单位外键
,A.XMSZD AddressDept    --地址外键
,CASE WHEN ISNULL(SG.JSGM,0)<>0 THEN '面积' + CONVERT(VARCHAR(20),SG.JSGM) + '（m2）'
WHEN ISNULL(SG.KDGD,0)<>0 THEN '跨度（高度）' + CONVERT(VARCHAR(20),SG.KDGD) + '（m）'
ELSE NULL END ConstrScale--建设规模
,NULL LandType  --用地性质
--新建	2000501
--改建	2000502
--扩建	2000503
--重建	2000504
--迁建	2000505
--恢复	2000506
--其他	2000507
,JS.FRXM JSDWFR         --建设单位法人
FROM JKCWFDB_WORK_NJS.DBO.YW_XMInfo AS A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=A.JSDW
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_ZLJDBAinfo AS BA ON BA.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMLXInfo  AS LX ON LX.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS SG ON SG.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_JSZBInfo AS JZ ON JZ.YWBM=A.YWBM
WHERE A.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
AND ISNULL(A.XMMC,'') <>'' 
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (391,392));

-----------------------------------------------------------------------------------------------------------------
--导入质监单体工程表
INSERT INTO dbCenter.dbo.TC_PrjItem_Info
SELECT 
FQ.GuidId FId         --单体工程id
,B.GuidId FPrjId         --关联TC_Prj_Info.FId
,B.XMMC ProjectName         --项目名称
,B.XMDZ Address         --项目地址
,JZ.ZMJ Area         --建设总面积
,SG.HTJG Investment         --投资规模(万元)
,B.XMJSDW JSDW         --建设单位名称
,JS.FRXM LegalPerson         --法人代表
,JS.ZZJGDM JSDWDM         --建设单位组织机构代码
,isnull(FQ.Name,'补项目名称') PrjItemName         --工程名称
,CASE WHEN ISNULL(BA.GCXZ,'0')='6' THEN '2000101'
WHEN ISNULL(BA.GCXZ,'0')='5' THEN '2000102'
WHEN ISNULL(BA.GCXZ,'0')='7' THEN '2000103'
ELSE '2000103' END PrjItemType --工程类别          --工程类别：2000101--房屋建筑;2000102--市政基础;2000103--其他
--砖木结构	509001
--砖混结构	509002
--钢筋混凝土结构	509003
--钢结构	509004
--框架结构	509005
--剪力墙结构	509006
--框架-剪力墙结构	509007
--框-筒结构	509008
--其他	509010
,CASE WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='砖木结构' THEN '509001' 
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='砖混结构' THEN '509002'
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='钢筋混凝土结构' THEN '509003'
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='钢结构' THEN '509004'
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='框架结构' THEN '509005'
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='剪力墙结构' THEN '509006' 
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='框架-剪力墙结构' THEN '509007' 
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='框-筒结构' THEN '509008' 
ELSE '509010' END ConstrType         --结构类型：509001；509002；509005
,SG.HTJG Cost         --工程造价（万元）
,NULL Scale         --工程规模
,NULL PrjItemDesc         --工程描述
,JS.FID FJSDWID         --建设单位外键
,B.XMSZD AddressDept         --地址外键
,CASE WHEN ISNULL(SG.JSGM,0)<>0 THEN '面积' + CONVERT(VARCHAR(20),SG.JSGM) + '（m2）'
WHEN ISNULL(SG.KDGD,0)<>0 THEN '跨度（高度）' + CONVERT(VARCHAR(20),SG.KDGD) + '（m）'
ELSE NULL END ConstrScale--建设规模
FROM JKCWFDB_WORK_NJS.DBO.YW_ZLJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMLXInfo  AS LX ON LX.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS SG ON SG.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_JSZBInfo AS JZ ON JZ.YWBM=BA.YWBM
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
AND ISNULL(B.XMMC,'') <>'' 
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (391,392));
-----------------------------------------------------------------------------------------------------------------
--导入质监业务主表
INSERT INTO dbCenter.dbo.CF_App_List
SELECT 
BA.YWBM FId    --业务编码
,B.JSDW FBaseinfoId    --企业id
,B.GuidId FPrjId    --项目编码
,CONVERT(VARCHAR(20),YEAR(YW.CreateTime)) + '年 质量监督备案' FName    --业务名称
,11221 FManageTypeId    --业务编码
,YW.CreateTime FwriteDate    --写入时间
,BA.LXSJ FReportDate    --上报时间
,NULL FIsSign    --是否签字
,0 FState    --业务状态
,NULL FResult    --审批结论
,YEAR(BA.LXSJ) FYear    --年度
,MONTH(BA.LXSJ) FMonth    --月份
,FQ.GuidId FLinkId    --外键工程编码
,JS.FCompany FBaseName    --建设单位名称
,NULL FUpDeptId    --上报部门地区编码
,NULL FRemark    --暂不考虑
,NULL FIsCheck    --暂不考虑
,NULL FCount    --暂不考虑
,YW.CreateTime FTime    --最后更新时间,业务暂时没有上报，没走流程
,0 FIsDeleted    --是否删除
,YW.CreateTime FCreateTime    --创建时间
,1 FReportCount    --暂不考虑
,NULL FToBaseinfoId    --暂不考虑
,NULL FAppDate    --暂不考虑
,NULL FLinkAppId    --暂不考虑
,NULL FBarCode    --暂不考虑
,NULL FCreateUser    --暂不考虑
,NULL FgfTime    --暂不考虑
FROM JKCWFDB_WORK_NJS.DBO.YW_ZLJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_YWInfo YW ON YW.YWBM=B.YWBM 
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
AND ISNULL(B.XMMC,'') <>'' 
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (391,392));



-----------------------------------------------------------------------------------------------------------------
--导入质监备案数据信息
insert into dbCenter.dbo.TC_QA_Record
(FId, FAppId, FPrjId, FPrjItemId, FJSDWID, RecordNo, ProjectName, JSDW, LegalPerson, PrjItemName,
 Province, city, county, AddressDept, Address, PrjItemType, Area, ConstrType, ProjectNumber, RegisterTime,
 Contracts, Mobile, SGDWId, SGDW, SGDWDZ, SGDWDH, SGDWFR, SGDWZS, XMJL, jldwId,
 JLDW, JLDWDZ, JLDWFR, JLDWDH, JLZS, XMZJ, sjdwId, SJDW, SJDWDZ, SJDWFR,
 SJDWDH, SJDWZS, JZS, JGS, kcdwId, CCDW, CCDWDZ, CCDWDH, CCDWFR, CCDWZS,
 YTGCS, Remark)
select 
a.GuidID FId,                    --主键
a.YWBM  FAppId,                  --业务外键
b.GuidID FPrjId,                   --项目编码
c.GuidID FPrjItemId,               --工程外键 
b.jsdw FJSDWID,                  --建设单位id
a.BAH RecordNo,                  --备案号
b.xmmc ProjectName,              --项目名称
b.xmjsdw JSDW,                   --建设单位名称
b.jsdwfr LegalPerson,            --建设单位法人
c.name PrjItemName,              --工程名称
b.xmszd Province,                --工程属地
null city,
null county,
null AddressDept,
b.XMDZ Address,                  --工程地点
CASE WHEN ISNULL(a.GCXZ,'0')='6' THEN '2000101'
WHEN ISNULL(a.GCXZ,'0')='5' THEN '2000102'
WHEN ISNULL(a.GCXZ,'0')='7' THEN '2000103'
ELSE '2000103' END PrjItemType,              --工程性质
d.ZMJ  Area,                     --建筑面积（m2）

CASE WHEN ISNULL(cast(d.cssj as varchar(500)), '0')='砖木结构'  THEN '509001'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='砖混结构' THEN '509002'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='钢筋混凝土结构' THEN '509003'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='钢结构' THEN '509004'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='框架结构' THEN '509005'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='剪力墙结构' THEN '509006' 
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='框架-剪力墙结构' THEN '509007' 
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='框-筒结构' THEN '509008' 
ELSE '509010' END ConstrType,    --结构类型
           
a.lxwh ProjectNumber,            --立项号
a.lxsj RegisterTime,             --立项时间
a.lxr Contracts,                 --联系人
a.lxdh Mobile,                   --联系电话

f.qyid SGDWId,                   --施工单位id
f.dwmc SGDW,                     --施工单位
f.DWDZ SGDWDZ,                   --施工单位地址
f.LXDH SGDWDH,                   --联系电话
f.FDDBR SGDWFR,                  --施工单位法人
f.ZXZZ SGDWZS,                   --资质证书号
f.XMFZR XMJL,                    --项目经理

g.qyid jldwId,                   --监理单位id
g.DWMC JLDW,                     --监理单位
g.dwdz JLDWDZ,                   --监理单位地址
g.FDDBR JLDWFR,                  --监理单位法人
g.LXDH JLDWDH,                   --联系电话
g.ZXZZ JLZS,                     --证书编号
g.XMZJ XMZJ,                     --项目经理

h.qyid sjdwId,                   --设计单位id
h.DWMC SJDW,                     --设计单位
h.DWDZ SJDWDZ,                   --设计单位地址
h.FDDBR SJDWFR,                  --设计单位法人
h.LXDH SJDWDH,                   --联系电话
h.ZXZZ SJDWZS,                   --证书编号
h.LXR JZS,                       --项目设计注册建筑师
h.ZZJGDM JGS,                    --项目设计注册结构师

i.QYID kcdwId,                   --勘察单位id
i.DWMC CCDW,                     --勘察单位
i.DWDZ CCDWDZ,                   --勘察单位地址
i.LXDH CCDWDH,                   --联系电话
i.FDDBR CCDWFR,                  --勘察单位法人
i.ZXZZ CCDWZS,                   --证书编号
i.LXR YTGCS,                     --注册岩土工程师

a.bz Remark                      --备注

from JKCWFDB_WORK_NJS.dbo.YW_ZLJDBAinfo a
left join JKCWFDB_WORK_NJS.dbo.YW_XMInfo b on a.YWBM = b.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_XMFQInfo c on a.YWBM = c.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_JSZBInfo d on a.YWBM = d.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_SGZCBDWInfo f on a.YWBM = f.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_JLDW g on a.YWBM = g.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_SJDW h on a.YWBM = h.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_KCDW i on a.YWBM = i.YWBM
WHERE  b.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
   AND ISNULL(b.xmmc,'') <>'' 
   AND a.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01' and C06.actionid in (391,392))
-----------------------------------------------------------------------------------------------------------------



--删除安监相关信息
--业务主表
Delete dbCenter.dbo.CF_App_List where FId in 
(SELECT 
BA.YWBM    --业务编码
FROM JKCWFDB_WORK_NJS.DBO.YW_AQJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (413,414))
  );

--项目表
delete from  dbCenter.dbo.TC_Prj_Info where FId in (
SELECT --A.*
A.GuidId         --项目id
FROM JKCWFDB_WORK_NJS.DBO.YW_XMInfo AS A
WHERE A.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (413,414))
);

--单体工程表
delete from dbCenter.dbo.TC_PrjItem_Info where FId in (
SELECT 
FQ.GuidId
FROM JKCWFDB_WORK_NJS.DBO.YW_AQJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (413,414))
);

--安监备案表
delete from dbCenter.dbo.TC_AJBA_Record where FId in (
SELECT
a.GuidID
from JKCWFDB_WORK_NJS.dbo.YW_AQJDBAinfo a
left join JKCWFDB_WORK_NJS.dbo.YW_XMInfo b on a.YWBM = b.YWBM
WHERE  b.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
   AND a.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01' and C06.actionid in (413,414))
);

-----------------------------------------------------------------------------------------------------------------
--导入安监项目表
INSERT INTO dbCenter.dbo.TC_Prj_Info
SELECT --A.*
A.GuidId FId         --项目id
,A.XMJSDW  JSDW         --建设单位名称
,JS.ZZJGDM  JSDWDM         --建设单位组织机构代码
,JS.DWDZ  JSDWDZ         --建设单位地址
,JS.LXR   Contacts         --建设单位联系人
,JS.LXDH Mobile         --建设单位联系电话
,A.XMMC ProjectName         --项目名称
,CASE WHEN A.XMSZD LIKE '51%' THEN LEFT(A.XMSZD,2) ELSE NULL END Province         --项目属地-省
,CASE WHEN A.XMSZD LIKE '51%' AND LEN(A.XMSZD)>=4 THEN LEFT(A.XMSZD,4) ELSE NULL END City         --项目属地-市州
,CASE WHEN A.XMSZD LIKE '51%' AND LEN(A.XMSZD)>=6 THEN LEFT(A.XMSZD,6) ELSE NULL END County         --项目属地-区县
,CASE WHEN ISNULL(LX.XMLX,'0')='1' THEN '2000101'
WHEN ISNULL(LX.XMLX,'0')='2' THEN '2000102'
WHEN ISNULL(LX.XMLX,'0')='3' THEN '2000103'
ELSE '2000103' END ProjectType         --项目类别:2000101--房屋建筑;2000102--市政基础;2000103--其他
,A.XMDZ Address         --项目地址
,BA.LXWH ProjectNumber         --立项文号
,CASE WHEN ISNULL(LX.LXJB,'0')='1' THEN '11220401'
WHEN ISNULL(LX.LXJB,'0')='2' THEN '11220402'
WHEN ISNULL(LX.LXJB,'0')='3' THEN '11220403'
WHEN ISNULL(LX.LXJB,'0')='4' THEN '112204034'
ELSE '112204035' END ProjectLevel         --立项级别
,BA.LXSJ ProjectTime         --项目时间
,A.XMBH ProjectNo         --项目编号
,NULL IsForeign         --是否涉外
,NULL JSYDXKZ         --建设用地许可证号
,NULL JSGCXKZ         --工程规划许可证号
,NULL Area         --建设总面积（m2）
,BA.GCZZJ Investment         --投资规模(万元)
,CASE WHEN ISNULL(LX.JZXZ,'0')='新建' THEN '30503'
WHEN ISNULL(LX.JZXZ,'0')='改建' THEN '30501'
WHEN ISNULL(LX.JZXZ,'0')='扩建' THEN '30502'
ELSE NULL END ConstrType         --建设性质：2000501--新建；2000502--改建
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
,CASE WHEN ISNULL(SG.GCLB,'0')='01' OR ISNULL(SG.GCLB,'0')='02' OR ISNULL(SG.GCLB,'0')='03' OR
ISNULL(SG.GCLB,'0')='04' OR ISNULL(SG.GCLB,'0')='05' OR ISNULL(SG.GCLB,'0')='06' OR
ISNULL(SG.GCLB,'0')='07' OR ISNULL(SG.GCLB,'0')='08' OR ISNULL(SG.GCLB,'0')='09' OR ISNULL(SG.GCLB,'0')='10' THEN '200010113'
WHEN ISNULL(SG.GCLB,'0')='11' OR ISNULL(SG.GCLB,'0')='12' OR ISNULL(SG.GCLB,'0')='13' THEN '200010209'
WHEN ISNULL(SG.GCLB,'0')='14' OR ISNULL(SG.GCLB,'0')='15' THEN '200010210' 
WHEN ISNULL(SG.GCLB,'0')='16' THEN '200010204'
WHEN ISNULL(SG.GCLB,'0')='17' THEN '200010201'
WHEN ISNULL(SG.GCLB,'0')='18' THEN '200010206'
WHEN ISNULL(SG.GCLB,'0')='19' THEN '2000103'
ELSE NULL END ProjectUse         --工程用途：200010201--给水；200010205--道路
,SG.SJKGRQ StartDate         --实际开工日期
,SG.SJJGRQ EndDate         --实际竣工日期
,NULL RegisterTime         --记录登记时间
,NULL  ConstrBasis         --建设依据
,NULL ConstrContent         --建设内容
,A.JSDW FJSDWID --建设单位外键
,A.XMSZD AddressDept    --地址外键
,CASE WHEN ISNULL(SG.JSGM,0)<>0 THEN '面积' + CONVERT(VARCHAR(20),SG.JSGM) + '（m2）'
WHEN ISNULL(SG.KDGD,0)<>0 THEN '跨度（高度）' + CONVERT(VARCHAR(20),SG.KDGD) + '（m）'
ELSE NULL END ConstrScale--建设规模
,NULL LandType  --用地性质
--新建	2000501
--改建	2000502
--扩建	2000503
--重建	2000504
--迁建	2000505
--恢复	2000506
--其他	2000507
,JS.FRXM JSDWFR         --建设单位法人
FROM JKCWFDB_WORK_NJS.DBO.YW_XMInfo AS A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=A.JSDW
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_AQJDBAinfo AS BA ON BA.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMLXInfo  AS LX ON LX.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS SG ON SG.YWBM=A.YWBM
WHERE A.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
AND ISNULL(A.XMMC,'') <>'' 
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (413,414));
-----------------------------------------------------------------------------------------------------------------
--导入安监单体工程表
INSERT INTO dbCenter.dbo.TC_PrjItem_Info
SELECT 
FQ.GuidId FId         --单体工程id
,B.GuidId FPrjId         --关联TC_Prj_Info.FId
,B.XMMC ProjectName         --项目名称
,B.XMDZ Address         --项目地址
,JZ.ZMJ Area         --建设总面积
,SG.HTJG Investment         --投资规模(万元)
,B.XMJSDW JSDW         --建设单位名称
,JS.FRXM LegalPerson         --法人代表
,JS.ZZJGDM JSDWDM         --建设单位组织机构代码
,isnull(FQ.Name ,'补工程名称')  as PrjItemName      --工程名称
,CASE WHEN ISNULL(LX.XMLX,'0')='1' THEN '2000101'
WHEN ISNULL(LX.XMLX,'0')='2' THEN '2000102'
WHEN ISNULL(LX.XMLX,'0')='3' THEN '2000103'
ELSE '2000103' END PrjItemType --工程类别          --工程类别：2000101--房屋建筑;2000102--市政基础;2000103--其他
--砖木结构	509001
--砖混结构	509002
--钢筋混凝土结构	509003
--钢结构	509004
--框架结构	509005
--剪力墙结构	509006
--框架-剪力墙结构	509007
--框-筒结构	509008
--其他	509010
,CASE WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='砖木结构' THEN '509001' 
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='砖混结构' THEN '509002'
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='钢筋混凝土结构' THEN '509003'
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='钢结构' THEN '509004'
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='框架结构' THEN '509005'
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='剪力墙结构' THEN '509006' 
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='框架-剪力墙结构' THEN '509007' 
WHEN ISNULL(cast(JZ.cssj as VARCHAR(100)),'0')='框-筒结构' THEN '509008' 
ELSE '509010' END ConstrType         --结构类型：509001；509002；509005
,BA.GCZZJ Cost         --工程造价（万元）
,NULL Scale         --工程规模
,NULL PrjItemDesc         --工程描述
,JS.FID FJSDWID         --建设单位外键
,B.XMSZD AddressDept         --地址外键
,CASE WHEN ISNULL(SG.JSGM,0)<>0 THEN '面积' + CONVERT(VARCHAR(20),SG.JSGM) + '（m2）'
WHEN ISNULL(SG.KDGD,0)<>0 THEN '跨度（高度）' + CONVERT(VARCHAR(20),SG.KDGD) + '（m）'
ELSE NULL END ConstrScale--建设规模
FROM JKCWFDB_WORK_NJS.DBO.YW_AQJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMLXInfo  AS LX ON LX.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS SG ON SG.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_JSZBInfo AS JZ ON JZ.YWBM=BA.YWBM
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
AND ISNULL(B.XMMC,'') <>'' 
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (413,414));
-----------------------------------------------------------------------------------------------------------------
--导入安监业务主表
INSERT INTO dbCenter.dbo.CF_App_List
SELECT 
BA.YWBM FId    --业务编码
,B.JSDW FBaseinfoId    --企业id
,B.GuidId FPrjId    --项目编码
,CONVERT(VARCHAR(20),YEAR(YW.CreateTime)) + '年 安全监督备案' FName    --业务名称
,11222 FManageTypeId    --业务编码
,YW.CreateTime FwriteDate    --写入时间
,BA.LXSJ FReportDate    --上报时间
,NULL FIsSign    --是否签字
,0 FState    --业务状态
,NULL FResult    --审批结论
,YEAR(BA.LXSJ) FYear    --年度
,MONTH(BA.LXSJ) FMonth    --月份
,FQ.GuidId FLinkId    --外键工程编码
,JS.FCompany FBaseName    --建设单位名称
,NULL FUpDeptId    --上报部门地区编码
,NULL FRemark    --暂不考虑
,NULL FIsCheck    --暂不考虑
,NULL FCount    --暂不考虑
,YW.CreateTime FTime    --最后更新时间,业务暂时没有上报，没走流程
,0 FIsDeleted    --是否删除
,YW.CreateTime FCreateTime    --创建时间
,1 FReportCount    --暂不考虑
,NULL FToBaseinfoId    --暂不考虑
,NULL FAppDate    --暂不考虑
,NULL FLinkAppId    --暂不考虑
,NULL FBarCode    --暂不考虑
,NULL FCreateUser    --暂不考虑
,NULL FgfTime    --暂不考虑
FROM JKCWFDB_WORK_NJS.DBO.YW_AQJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_YWInfo YW ON YW.YWBM=B.YWBM 
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
AND ISNULL(B.XMMC,'') <>'' 
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (413,414));
  
  -----------------------------------------------------------------------------------------------------------------
--导入安监备案数据信息表
insert into dbCenter.dbo.TC_AJBA_Record              --为什么这里又要用dbCenter_sc.呢？？？
(FId,FAppId,FPrjId,FPrjItemId,FJSDWID,ProjectName,JSDW,LegalPerson,PrjItemName,
 Province,City,County,Address,AddressDept,Area,ConstrType,Floor,Cost,PlanLimit,RecordNo,ProjectNo,ProjectTime,Contracts,
 Mobile,SGId,SGDW,SGDWDH,SGDWFR,SGDWZZZSH,SGDWXMJL,JLId,JLDW,JLDWDH,JLDWFR,Remark)
select 
a.GuidID FId,                    --主键
a.YWBM  FAppId,                  --业务外键
b.GuidID FPrjId,                 --工程编码
c.GuidID FPrjItemId,             --工程外键
b.jsdw FJSDWID,                  --建设单位id
b.XMMC ProjectName,              --项目名称
b.XMJSDW JSDW,                   --建设单位名称
b.JSDWFR LegalPerson,            --建设单位法人
c.name PrjItemName,              --工程名称
'' Province,                --项目施工地
'' City,
'' County,
b.xmdz Address,                   --工程地点
b.xmszd AddressDept,             
d.zmj Area,                      --建筑面积(㎡)

CASE WHEN ISNULL(cast(d.cssj as varchar(500)), '0')='砖木结构'  THEN '509001'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='砖混结构' THEN '509002'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='钢筋混凝土结构' THEN '509003'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='钢结构' THEN '509004'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='框架结构' THEN '509005'
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='剪力墙结构' THEN '509006' 
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='框架-剪力墙结构' THEN '509007' 
WHEN ISNULL(cast(d.cssj as varchar(500)),'0')='框-筒结构' THEN '509008' 
ELSE '509010' END ConstrType,    --结构类型

a.jzcs Floor,                    --建筑层数（层）
a.gczzj Cost,                    --工程总造价（万元）
a.jhsgqx PlanLimit,              --计划施工期限
a.djbah RecordNo,                --登记备案号
a.lxwh ProjectNo,                --立项文号
a.lxsj ProjectTime,              --立项日期
a.lxr Contracts,                 --联系人
a.lxdh Mobile,                   --联系电话
e.qyid SGId,                   --施工单位id
e.dwmc SGDW,                     --施工单位名称
e.lxdh SGDWDH,                   --施工单位电话
e.fddbr SGDWFR,                  --施工单位法人
e.zxzz SGDWZZZSH,                --资质证书号
e.xmfzr SGDWXMJL,                --项目经理
f.QYID JLId,                   --监理单位id
f.DWMC JLDW,                     --监理单位
f.LXDH JLDWDH,                   --联系电话
f.FDDBR JLDWFR,                  --监理单位法人
a.BZ Remark                      --备注
from JKCWFDB_WORK_NJS.dbo.YW_AQJDBAinfo a
left join JKCWFDB_WORK_NJS.dbo.YW_XMInfo b on a.YWBM = b.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_XMFQInfo c on a.YWBM = c.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_JSZBInfo d on a.YWBM = d.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_SGZCBDWInfo e on a.ywbm = e.YWBM
left join JKCWFDB_WORK_NJS.dbo.YW_JLDW f on a.YWBM = f.YWBM
WHERE  b.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
AND ISNULL(b.XMMC,'') <>'' 
   AND a.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01' and C06.actionid in (413,414));    
-----------------------------------------------------------------------------------------------------------------




--导入最终招标备案的过程数据
--CF_App_ProcessInstance   备案主流程表
--CF_App_ProcessRecord     备案子流程表

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
BA.YWBM FId    --业务编码
,B.JSDW FBaseinfoId    --企业id
,B.GuidId FPrjId    --项目编码
,CONVERT(VARCHAR(20),YEAR(YW.CreateTime)) + '年 质量监督备案' FName    --业务名称
,11221 FManageTypeId    --业务编码
,YW.CreateTime FwriteDate    --写入时间
,BA.LXSJ FReportDate    --上报时间
,NULL FIsSign    --是否签字
,0 FState    --业务状态
,NULL FResult    --审批结论
,YEAR(BA.LXSJ) FYear    --年度
,MONTH(BA.LXSJ) FMonth    --月份
,FQ.GuidId FLinkId    --外键工程编码
,JS.FCompany FBaseName    --建设单位名称
,NULL FUpDeptId    --上报部门地区编码
,NULL FRemark    --暂不考虑
,NULL FIsCheck    --暂不考虑
,NULL FCount    --暂不考虑
,YW.CreateTime FTime    --最后更新时间,业务暂时没有上报，没走流程
,0 FIsDeleted    --是否删除
,YW.CreateTime FCreateTime    --创建时间
,1 FReportCount    --暂不考虑
,NULL FToBaseinfoId    --暂不考虑
,NULL FAppDate    --暂不考虑
,NULL FLinkAppId    --暂不考虑
,NULL FBarCode    --暂不考虑
,NULL FCreateUser    --暂不考虑
,NULL FgfTime    --暂不考虑
FROM JKCWFDB_WORK_NJS.DBO.YW_ZLJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_YWInfo YW ON YW.YWBM=B.YWBM 
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
AND ISNULL(B.XMMC,'') <>'' 
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (391,392));


if not exists (select 1
            from  sysobjects
           where  id = object_id('dbo._App_ProcessInstance_QA')
            and   type = 'U')
begin 
    SELECT * into _App_ProcessInstance_QA from CF_App_ProcessInstance where 1=2  --只取表结构
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
	          a.FReportDate,1,'8801',1,1,'管理部门审核',1,2,1
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
BA.YWBM FId    --业务编码
,B.JSDW FBaseinfoId    --企业id
,B.GuidId FPrjId    --项目编码
,CONVERT(VARCHAR(20),YEAR(YW.CreateTime)) + '年 安全监督备案' FName    --业务名称
,11222 FManageTypeId    --业务编码
,YW.CreateTime FwriteDate    --写入时间
,BA.LXSJ FReportDate    --上报时间
,NULL FIsSign    --是否签字
,0 FState    --业务状态
,NULL FResult    --审批结论
,YEAR(BA.LXSJ) FYear    --年度
,MONTH(BA.LXSJ) FMonth    --月份
,FQ.GuidId FLinkId    --外键工程编码
,JS.FCompany FBaseName    --建设单位名称
,NULL FUpDeptId    --上报部门地区编码
,NULL FRemark    --暂不考虑
,NULL FIsCheck    --暂不考虑
,NULL FCount    --暂不考虑
,YW.CreateTime FTime    --最后更新时间,业务暂时没有上报，没走流程
,0 FIsDeleted    --是否删除
,YW.CreateTime FCreateTime    --创建时间
,1 FReportCount    --暂不考虑
,NULL FToBaseinfoId    --暂不考虑
,NULL FAppDate    --暂不考虑
,NULL FLinkAppId    --暂不考虑
,NULL FBarCode    --暂不考虑
,NULL FCreateUser    --暂不考虑
,NULL FgfTime    --暂不考虑
FROM JKCWFDB_WORK_NJS.DBO.YW_AQJDBAinfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_YWInfo YW ON YW.YWBM=B.YWBM 
WHERE B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
AND ISNULL(B.XMMC,'') <>'' 
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS .DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (413,414));

if not exists (select 1
            from  sysobjects
           where  id = object_id('dbo._App_ProcessInstance_AJBA')
            and   type = 'U')
begin 
    SELECT * into _App_ProcessInstance_AJBA from CF_App_ProcessInstance where 1=2  --只取表结构
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
	          a.FReportDate,1,'8801',1,1,'管理部门审核',1,2,1
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