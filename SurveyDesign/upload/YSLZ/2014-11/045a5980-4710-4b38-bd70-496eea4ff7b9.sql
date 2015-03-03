--导入施工许可信息
--insert INTO 
--(
--FID
--,BuilderLicenceNum--施工许可证编号
--,PrjNum--项目编号
--,BuldPlanNum--建设用地规划许可证编号
--,ProjectPlanNum--建设工程规划许可证编号
--,CensorNum--施工图审查合格书编号
--,ContractMoney--合同金额(万元)
--,Area--面积（平方米）
--,PrjSize--建设规模
--,IssueCertDate--发证日期
--,EconCorpName--勘察单位名称
--,EconCorpCode--勘察单位组织机构代码
--,DesignCorpName--设计单位名称
--,DesignCorpCode--设计单位组织机构代码
--,ConsCorpName--施工单位名称
--,ConsCorpCode--施工单位组织机构代码
--,SafetyCerID--施工单位安全生产许可证编号
--,SuperCorpName--监理单位名称
--,SuperCorpCode--监理单位组织机构代码
--,ConstructorName--项目经理姓名
--,CIDCardTypeNum--项目经理证件类型
--,ConstructorIDCard--项目经理证件号码
--,SupervisionName--总监理工程师姓名
--,SIDCardTypeNum--总监理工程师证件类型
--,SupervisionIDCard--总监理工程师证件号码
--,CreateDate--记录登记时间
--,pkid
--,LastUpdateDate	
--)
SELECT --NEWID() FID,
C.XKZBH	BuilderLicenceNum--施工许可证编号
,B.XMBM 	PrjNum--项目编号
,D.ZSBH BuldPlanNum--建设用地规划许可证编号
,E.ZSBH	ProjectPlanNum--建设工程规划许可证编号
,null	CensorNum--施工图审查合格书编号
,C.HTJG	ContractMoney--合同金额(万元)
,C.JSGM	Area--面积（平方米）
,C.JSGM	PrjSize--建设规模
,C.FZRQ	IssueCertDate--发证日期
,F.DWMC	EconCorpName--勘察单位名称
,F.ZZJGDM	EconCorpCode--勘察单位组织机构代码
,H.DWMC 	DesignCorpName--设计单位名称
,H.ZZJGDM 	DesignCorpCode--设计单位组织机构代码
,J.DWMC	ConsCorpName--施工单位名称
,J.ZZJGDM 	ConsCorpCode--施工单位组织机构代码
,(SELECT TOP 1 FCERTINO FROM dbCenterSC.dbo.CF_Ent_SafetyCerti WHERE FBaseInfoId=J.QYID )	SafetyCerID--施工单位安全生产许可证编号
,L.DWMC 	SuperCorpName--监理单位名称
,L.ZZJGDM 	SuperCorpCode--监理单位组织机构代码
,M.XM	ConstructorName--项目经理姓名
,CASE WHEN ISNULL(M.XM,'')<>'' THEN 1 ELSE NULL END	CIDCardTypeNum--项目经理证件类型
,M.SFZH	ConstructorIDCard--项目经理证件号码
,N.XM 	SupervisionName--总监理工程师姓名
,CASE WHEN ISNULL(N.XM,'')<>'' THEN 1 ELSE NULL END	SIDCardTypeNum--总监理工程师证件类型
,N.SFZH SupervisionIDCard--总监理工程师证件号码
,C.FZRQ	CreateDate--记录登记时间
,B.XMBM pkid
,GETDATE() LastUpdateDate	
--INTO TBBuilderLicenceManage
FROM JKCWFDB_WORK_NJS.DBO.C05 AS C05
,JKCWFDB_WORK_NJS.DBO.C06 AS C06
,JKCWFDB_WORK_NJS.DBO.YW_YWINFO AS A
,JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ
,JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_YDGHXKZINFO D ON D.YWBM=(SELECT TOP 1 XM.YWBM FROM JKCWFDB_WORK_NJS.DBO.YW_XMINFO XM,JKCWFDB_WORK_NJS.DBO.YW_YDGHXKZINFO YD WHERE XM.YWBM=YD.YWBM AND XM.XMBM=B.XMBM AND ISNULL(YD.ZSBH,'')<>'')
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_JSGCGHXKINFO E ON E.YWBM=(SELECT TOP 1 XM.YWBM FROM JKCWFDB_WORK_NJS.DBO.YW_XMINFO XM,JKCWFDB_WORK_NJS.DBO.YW_JSGCGHXKINFO YD WHERE XM.YWBM=YD.YWBM AND XM.XMBM=B.XMBM AND ISNULL(YD.ZSBH,'')<>'')
--LEFT JOIN ZJB_dbStandard.dbo.TBProjectInfo TP ON B.XMBM=TP.pkid 
,JKCWFDB_WORK_NJS.DBO.YW_SGXKZINFO AS C
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_KCDW F ON C.YWBM=F.YWBM
--LEFT JOIN dbCenterSC.dbo.CF_Ent_BaseInfo G ON G.FId=F.QYID 
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SJDW H ON C.YWBM=H.YWBM
--LEFT JOIN dbCenterSC.dbo.CF_Ent_BaseInfo I ON I.FId=H.QYID 
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGZCBDWInfo J ON C.YWBM=J.YWBM
--LEFT JOIN dbCenterSC.dbo.CF_Ent_BaseInfo K ON K.FId=J.QYID 
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_JLDW L ON C.YWBM=L.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGZCBDW_RYXX M ON C.YWBM=M.YWBM AND M.RYLX IN ('项目经理','项目负责人')
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_JLDW_RYXX N ON C.YWBM=N.YWBM AND N.RYLX='项目总监' 
,JKCWFDB_WORK_NJS.DBO.XM_XMINFO AS XM 
WHERE C05.PROCESSKEYVALUE=A.YWBM AND C05.PROJECTID=C06.PROJECTID AND C06.ACTIONID IN (279,271,259)
  AND ISNULL(A.DZZT,0) IN (1,2) AND A.YWBM=B.YWBM AND A.YWBM=C.YWBM AND ISNULL(A.YWLX,'') <> ''
  AND B.XMBM=XM.XMBM 
  AND FQ.YWBM=A.YWBM 
AND ISNULL(A.ZXDEL,0)<>1 