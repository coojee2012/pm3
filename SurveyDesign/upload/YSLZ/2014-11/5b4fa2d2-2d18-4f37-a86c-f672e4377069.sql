--����ʩ�������Ϣ
--insert INTO 
--(
--FID
--,BuilderLicenceNum--ʩ�����֤���
--,PrjNum--��Ŀ���
--,BuldPlanNum--�����õع滮���֤���
--,ProjectPlanNum--���蹤�̹滮���֤���
--,CensorNum--ʩ��ͼ���ϸ�����
--,ContractMoney--��ͬ���(��Ԫ)
--,Area--�����ƽ���ף�
--,PrjSize--�����ģ
--,IssueCertDate--��֤����
--,EconCorpName--���쵥λ����
--,EconCorpCode--���쵥λ��֯��������
--,DesignCorpName--��Ƶ�λ����
--,DesignCorpCode--��Ƶ�λ��֯��������
--,ConsCorpName--ʩ����λ����
--,ConsCorpCode--ʩ����λ��֯��������
--,SafetyCerID--ʩ����λ��ȫ�������֤���
--,SuperCorpName--����λ����
--,SuperCorpCode--����λ��֯��������
--,ConstructorName--��Ŀ��������
--,CIDCardTypeNum--��Ŀ����֤������
--,ConstructorIDCard--��Ŀ����֤������
--,SupervisionName--�ܼ�����ʦ����
--,SIDCardTypeNum--�ܼ�����ʦ֤������
--,SupervisionIDCard--�ܼ�����ʦ֤������
--,CreateDate--��¼�Ǽ�ʱ��
--,pkid
--,LastUpdateDate	
--)
SELECT --NEWID() FID,
C.XKZBH	BuilderLicenceNum--ʩ�����֤���
,B.XMBM 	PrjNum--��Ŀ���
,D.ZSBH BuldPlanNum--�����õع滮���֤���
,E.ZSBH	ProjectPlanNum--���蹤�̹滮���֤���
,null	CensorNum--ʩ��ͼ���ϸ�����
,C.HTJG	ContractMoney--��ͬ���(��Ԫ)
,C.JSGM	Area--�����ƽ���ף�
,C.JSGM	PrjSize--�����ģ
,C.FZRQ	IssueCertDate--��֤����
,F.DWMC	EconCorpName--���쵥λ����
,F.ZZJGDM	EconCorpCode--���쵥λ��֯��������
,H.DWMC 	DesignCorpName--��Ƶ�λ����
,H.ZZJGDM 	DesignCorpCode--��Ƶ�λ��֯��������
,J.DWMC	ConsCorpName--ʩ����λ����
,J.ZZJGDM 	ConsCorpCode--ʩ����λ��֯��������
,(SELECT TOP 1 FCERTINO FROM dbCenterSC.dbo.CF_Ent_SafetyCerti WHERE FBaseInfoId=J.QYID )	SafetyCerID--ʩ����λ��ȫ�������֤���
,L.DWMC 	SuperCorpName--����λ����
,L.ZZJGDM 	SuperCorpCode--����λ��֯��������
,M.XM	ConstructorName--��Ŀ��������
,CASE WHEN ISNULL(M.XM,'')<>'' THEN 1 ELSE NULL END	CIDCardTypeNum--��Ŀ����֤������
,M.SFZH	ConstructorIDCard--��Ŀ����֤������
,N.XM 	SupervisionName--�ܼ�����ʦ����
,CASE WHEN ISNULL(N.XM,'')<>'' THEN 1 ELSE NULL END	SIDCardTypeNum--�ܼ�����ʦ֤������
,N.SFZH SupervisionIDCard--�ܼ�����ʦ֤������
,C.FZRQ	CreateDate--��¼�Ǽ�ʱ��
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
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGZCBDW_RYXX M ON C.YWBM=M.YWBM AND M.RYLX IN ('��Ŀ����','��Ŀ������')
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_JLDW_RYXX N ON C.YWBM=N.YWBM AND N.RYLX='��Ŀ�ܼ�' 
,JKCWFDB_WORK_NJS.DBO.XM_XMINFO AS XM 
WHERE C05.PROCESSKEYVALUE=A.YWBM AND C05.PROJECTID=C06.PROJECTID AND C06.ACTIONID IN (279,271,259)
  AND ISNULL(A.DZZT,0) IN (1,2) AND A.YWBM=B.YWBM AND A.YWBM=C.YWBM AND ISNULL(A.YWLX,'') <> ''
  AND B.XMBM=XM.XMBM 
  AND FQ.YWBM=A.YWBM 
AND ISNULL(A.ZXDEL,0)<>1 