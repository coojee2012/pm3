
begin
----------------------------------------------------------------------------------------------------------------------------------------------------
--ɾ��ʩ�����֤�����Ϣ
--ҵ������
Delete dbCenter.dbo.CF_App_List where FId in 
(SELECT 
BA.YWBM    --ҵ�����
FROM JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_YWInfo YW ON YW.YWBM=B.YWBM 
WHERE ISNULL(JS.FID,'')<>'' AND ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
  );

--��Ŀ��
delete from  dbCenter.dbo.TC_Prj_Info where FId in (
SELECT --A.*
A.GuidId         --��Ŀid
FROM JKCWFDB_WORK_NJS.DBO.YW_XMInfo AS A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=A.JSDW
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS BA ON BA.YWBM=A.YWBM
WHERE ISNULL(JS.FID,'')<>'' AND ISNULL(A.XMBM,'')<>'' --��֤��Ŀ������ֵ 
  AND A.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--���幤�̱�
delete from dbCenter.dbo.TC_PrjItem_Info where FId in (
SELECT 
FQ.GuidId
FROM JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
WHERE ISNULL(JS.FID,'')<>'' AND ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--��֤��
delete from dbCenter.dbo.TC_SGXKZ_BZJQR where FAppId in (
SELECT 
A.YWBM
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--ʩ��ͼ�����Ϣ
delete from dbCenter.dbo.TC_SGXKZ_SGTSC where FId in (
SELECT 
A.ID         --����
FROM JKCWFDB_WORK_NJS.dbo.YW_SGTZJJSZL A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--��ͬ�����
delete from dbCenter.dbo.TC_SGXKZ_ZBJGBL where FId in (
SELECT 
B.GuidID       --����
FROM JKCWFDB_WORK_NJS.dbo.YW_ZBJG_KCDW KC
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=KC.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--��Ͷ����Ϣ��
delete from dbCenter.dbo.TC_SGXKZ_ZBJG where FId in (
SELECT 
B.GuidID         --����
FROM JKCWFDB_WORK_NJS.dbo.YW_ZBJG_KCDW KC
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_ZBJG_SJDW SJ ON SJ.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_ZBJG_JLDW JL ON JL.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_ZBJG_SGDW SG ON SG.YWBM=KC.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--���蹤�̹滮���֤��
delete from dbCenter.dbo.TC_SGXKZ_JSGCGHXKZ where FId in (
SELECT 
A.ID          --����
FROM JKCWFDB_WORK_NJS.dbo.YW_GCGHXKZ A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--�����õع滮���֤
delete from dbCenter.dbo.TC_SGXKZ_JSYDGHXKZ where FId in (
SELECT 
A.ID         --����
FROM JKCWFDB_WORK_NJS.dbo.YW_YDGHXKZ A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--ѡַ�����
delete from dbCenter.dbo.TC_SGXKZ_Location where FId in (
SELECT 
A.ID         --����
FROM JKCWFDB_WORK_NJS.dbo.YW_XZYJS A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--ʩ�����֤���ΰ�����Ϣ��
delete from  dbCenter.dbo.TC_SGXKZ_PrjInfo where FId in (
SELECT
BA.WYBM  --����
FROM JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
WHERE ISNULL(JS.FID,'')<>'' AND ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--��������
delete from dbCenter.dbo.TC_SGXKZ_File where FId in (
SELECT 
A.ID
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);


--����������
delete from dbCenter.dbo.TC_SGXKZ_File where FId in (
SELECT 
A.ID
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL_JL A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);


--ʩ����������
delete from dbCenter.dbo.TC_SGXKZ_File where FId in (
SELECT 
A.ID
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL_SG A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--��Ƹ�������
delete from dbCenter.dbo.TC_SGXKZ_File where FId in (
SELECT 
A.ID
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL_SJ A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);


--���츽������
delete from dbCenter.dbo.TC_SGXKZ_File where FId in (
SELECT 
A.ID 
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL_KC A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--����̲ν���λ��
delete from dbCenter.dbo.TC_PrjItem_Ent where FAppId in (
SELECT 
A.YWBM
FROM JKCWFDB_WORK_NJS.dbo.YW_SGZCBDWInfo A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--����̲ν���Ա��
delete from dbCenter.dbo.TC_PrjItem_Emp where FAppId in (
SELECT 
A.YWBM  --ҵ������
FROM JKCWFDB_WORK_NJS.dbo.YW_SGZCBDW_RYXX A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);
end 

/**delete end **/
-----------------------------------------------------------------------------------------------------------------------------------------------------
--����ʩ�����֤ҵ������

if not exists (select 1
            from  sysobjects
           where  id = object_id('dbo._App_List_SGXK')
            and   type = 'U')
begin
    select * into _App_List_SGXK from dbCenter.dbo.CF_App_List where 1=2
end
else
begin
   delete _App_List_SGXK
end 
	INSERT INTO dbCenter.dbo._App_List_SGXK
	SELECT 
	BA.YWBM FId    --ҵ�����
	,B.JSDW FBaseinfoId    --��ҵid
	,B.GuidId FPrjId    --��Ŀ����
	,CASE WHEN ISNULL(YW.CreateTime,'')<>'' THEN CONVERT(CHAR(4),YEAR(YW.CreateTime))+'�� ' ELSE '' END +'ʩ�����֤������ΰ���' FName    --ҵ������
	,11223 FManageTypeId    --ҵ�����
	,YW.CreateTime FwriteDate    --д��ʱ��
	,BA.SBSJ FReportDate    --�ϱ�ʱ��
	,NULL FIsSign    --�Ƿ�ǩ��
	,0 FState    --ҵ��״̬
	,NULL FResult    --��������
	,YEAR(YW.CreateTime) FYear    --���
	,MONTH(YW.CreateTime) FMonth    --�·�
	,FQ.FQBM FLinkId    --������̱���
	,JS.FCompany FBaseName    --���赥λ����
	,isnull(b.SDC,'51') FUpDeptId    --�ϱ����ŵ�������
	,NULL FRemark    --�ݲ�����
	,NULL FIsCheck    --�ݲ�����
	,NULL FCount    --�ݲ�����
	,YW.CreateTime FTime    --������ʱ��,ҵ����ʱû���ϱ���û������
	,NULL FIsDeleted    --�Ƿ�ɾ��
	,YW.CreateTime FCreateTime    --����ʱ��
	,1 FReportCount    --�ݲ�����
	,NULL FToBaseinfoId    --�ݲ�����
	,NULL FAppDate    --�ݲ�����
	,NULL FLinkAppId    --�ݲ�����
	,NULL FBarCode    --�ݲ�����
	,NULL FCreateUser    --�ݲ�����
	,NULL FgfTime    --�ݲ�����
	FROM JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS BA
	LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
	LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
	LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
	LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_YWInfo YW ON YW.YWBM=B.YWBM 
	WHERE ISNULL(JS.FID,'')<>'' AND ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
	  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
	  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
					 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

     update [CF_App_List]
          set FManageTypeId = b.FManageTypeId,
              Fresult  = b.FResul,
			  fstate   = b.fstate
    from [CF_App_List] a,
	(select ba.ywbm ,c.actionid,
       case c.actionid when 257 then '11223' when 258 then '11223' when 259 then '11223'
	                   when 269 then '11224' when 270 then '11224' when 271 then '11224'
					   when 277 then '11225' when 278 then '11225' when 279 then '11225' end as FManageTypeId,  --����
       case c.actionid when 257 then 0 when 258 then 0 when 259 then 0
	                   when 269 then 0 when 270 then 0 when 271 then 0
					   when 277 then 0 when 278 then 0 when 279 then 0                   end as FMeasure,          --
       1  as fstate,         --
       null as FResul	   		--   	   
 from JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA,JKCWFDB_WORK_NJS.DBO.c05 b ,JKCWFDB_WORK_NJS.DBO.c06 c
where ba.ywbm = b.ProcessKeyValue 
  and b.ProjectID=c.ProjectID
  and c.ActionState in (0,1,2)
  and b.ProcessTimeB>='2014-01-01'
  and c.actionid in (257,258,259,269,270,271,277,278,279)
    ) b 
  where a.fid = b.ywbm 

  
  INSERT INTO dbCenter.dbo.CF_App_List
       select * 
	     from dbCenter.dbo._App_List_SGXK a
        where not exists(select 1 from dbCenter.dbo.CF_App_List b where a.FId = b.Fid)

-----------------------------------------------------------------------------------------------------------------------------------------------------
--����ʩ�����֤�����Ŀ��
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
,CASE WHEN ISNULL(BA.GCLB,'0')='01' OR ISNULL(BA.GCLB,'0')='02' OR ISNULL(BA.GCLB,'0')='03' OR
ISNULL(BA.GCLB,'0')='04' OR ISNULL(BA.GCLB,'0')='05' OR ISNULL(BA.GCLB,'0')='06' OR
ISNULL(BA.GCLB,'0')='07' OR ISNULL(BA.GCLB,'0')='08' OR ISNULL(BA.GCLB,'0')='09' OR ISNULL(BA.GCLB,'0')='10' THEN '2000101'
WHEN ISNULL(BA.GCLB,'0')='11' OR ISNULL(BA.GCLB,'0')='12' OR ISNULL(BA.GCLB,'0')='13' OR
ISNULL(BA.GCLB,'0')='14' OR ISNULL(BA.GCLB,'0')='15' OR ISNULL(BA.GCLB,'0')='16' OR
ISNULL(BA.GCLB,'0')='17' OR ISNULL(BA.GCLB,'0')='18' THEN '2000102'
WHEN ISNULL(BA.GCLB,'0')='19' THEN '2000103'
ELSE NULL END ProjectType         --��Ŀ���:2000101--���ݽ���;2000102--��������;2000103--����
,A.XMDZ Address         --��Ŀ��ַ
,NULL ProjectNumber         --�����ĺ�
,NULL ProjectLevel         --�����
,NULL ProjectTime         --��Ŀʱ��
,A.XMBH ProjectNo         --��Ŀ���
,NULL IsForeign         --�Ƿ�����
,NULL JSYDXKZ         --�����õ����֤��
,NULL JSGCXKZ         --���̹滮���֤��
,NULL Area         --�����������m2��
,NULL Investment         --Ͷ�ʹ�ģ(��Ԫ)
,NULL ConstrType         --�������ʣ�2000501--�½���2000502--�Ľ�
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
,CASE WHEN ISNULL(BA.GCLB,'0')='01' OR ISNULL(BA.GCLB,'0')='02' OR ISNULL(BA.GCLB,'0')='03' OR
ISNULL(BA.GCLB,'0')='04' OR ISNULL(BA.GCLB,'0')='05' OR ISNULL(BA.GCLB,'0')='06' OR
ISNULL(BA.GCLB,'0')='07' OR ISNULL(BA.GCLB,'0')='08' OR ISNULL(BA.GCLB,'0')='09' OR ISNULL(BA.GCLB,'0')='10' THEN '200010113'
WHEN ISNULL(BA.GCLB,'0')='11' OR ISNULL(BA.GCLB,'0')='12' OR ISNULL(BA.GCLB,'0')='13' THEN '200010209'
WHEN ISNULL(BA.GCLB,'0')='14' OR ISNULL(BA.GCLB,'0')='15' THEN '200010210' 
WHEN ISNULL(BA.GCLB,'0')='16' THEN '200010204'
WHEN ISNULL(BA.GCLB,'0')='17' THEN '200010201'
WHEN ISNULL(BA.GCLB,'0')='18' THEN '200010206'
WHEN ISNULL(BA.GCLB,'0')='19' THEN '2000103'
ELSE NULL END ProjectUse         --������;��200010201--��ˮ��200010205--��·
,BA.SJKGRQ StartDate         --ʵ�ʿ�������
,BA.SJJGRQ EndDate         --ʵ�ʿ�������
,NULL RegisterTime         --��¼�Ǽ�ʱ��
,NULL  ConstrBasis         --��������
,''  ConstrContent         --��������
,A.JSDW FJSDWID --���赥λ���
,isnull(A.XMSZD,'51') AddressDept    --��ַ���
,CONVERT(VARCHAR(20),BA.JSGM) + BA.JSGM_DW + ', ' + BA.JSGMQT ConstrScale--�����ģ
,NULL LandType  --�õ�����
--�½�	2000501
--�Ľ�	2000502
--����	2000503
--�ؽ�	2000504
--Ǩ��	2000505
--�ָ�	2000506
--����	2000507
,A.JSDWFR JSDWFR         --���赥λ����
FROM JKCWFDB_WORK_NJS.DBO.YW_XMInfo AS A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=A.JSDW
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS BA ON BA.YWBM=A.YWBM
WHERE ISNULL(JS.FID,'')<>'' AND ISNULL(A.XMBM,'')<>'' --��֤��Ŀ������ֵ 
  AND A.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

-----------------------------------------------------------------------------------------------------------------------------------------------------
--����ʩ�����֤��صĵ��幤�̱�
INSERT INTO dbCenter.dbo.TC_PrjItem_Info
SELECT 
FQ.GuidId FId         --���幤��id
,B.GuidId FPrjId         --����TC_Prj_Info.FId
,B.XMMC ProjectName         --��Ŀ����
,B.XMDZ Address         --��Ŀ��ַ
,NULL Area         --���������
,NULL Investment         --Ͷ�ʹ�ģ(��Ԫ)
,JS.FCompany JSDW         --���赥λ����
,JS.FRXM LegalPerson         --���˴���
,JS.ZZJGDM JSDWDM         --���赥λ��֯��������
,FQ.Name PrjItemName         --��������
,CASE WHEN ISNULL(BA.GCLB,'0')='01' OR ISNULL(BA.GCLB,'0')='02' OR ISNULL(BA.GCLB,'0')='03' OR
ISNULL(BA.GCLB,'0')='04' OR ISNULL(BA.GCLB,'0')='05' OR ISNULL(BA.GCLB,'0')='06' OR
ISNULL(BA.GCLB,'0')='07' OR ISNULL(BA.GCLB,'0')='08' OR ISNULL(BA.GCLB,'0')='09' OR ISNULL(BA.GCLB,'0')='10' THEN '2000101'
WHEN ISNULL(BA.GCLB,'0')='11' OR ISNULL(BA.GCLB,'0')='12' OR ISNULL(BA.GCLB,'0')='13' OR
ISNULL(BA.GCLB,'0')='14' OR ISNULL(BA.GCLB,'0')='15' OR ISNULL(BA.GCLB,'0')='16' OR
ISNULL(BA.GCLB,'0')='17' OR ISNULL(BA.GCLB,'0')='18' THEN '2000102'
WHEN ISNULL(BA.GCLB,'0')='19' THEN '2000103'
ELSE NULL END PrjItemType --�������          --�������2000101--���ݽ���;2000102--��������;2000103--����
--שľ�ṹ	509001
--ש��ṹ	509002
--�ֽ�������ṹ	509003
--�ֽṹ	509004
--��ܽṹ	509005
--����ǽ�ṹ	509006
--���-����ǽ�ṹ	509007
--��-Ͳ�ṹ	509008
--����	509010
,NULL ConstrType         --�ṹ���ͣ�509001��509002��509005
,BA.HTJG Cost         --������ۣ���Ԫ��
,NULL Scale         --���̹�ģ
,NULL PrjItemDesc         --��������
,JS.FID FJSDWID         --���赥λ���
,isnull(B.XMSZD,'51') AddressDept         --��ַ���
,CONVERT(VARCHAR(20),BA.JSGM) + BA.JSGM_DW + ', ' + BA.JSGMQT ConstrScale--�����ģ
FROM JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
WHERE ISNULL(JS.FID,'')<>'' AND ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


-----------------------------------------------------------------------------------------------------------------------------------------------------
--��֤��1
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
A.ID FId         --����
,A.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,B.GuidId FPrjId --��Ŀ���
,'�ش��������������ʩ���׷ѣ�����ԭ��ȡ�������������׷ѡ�����ȼ�����׷ѡ�����ˮ���׷ѣ�' JFXM     --�ɷ���Ŀ
,'1' JFXMBM     --�ɷ���Ŀ����
,A.JE1 Money     --����Ԫ��
,A.JFSJ1 JFSJ     --�ɷ�ʱ��
,A.SKJBR1 SKJBR     --�տ����
,A.SKDW1 SKDW     --�տλǩ��
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

--��֤��2
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
newid() FId         --����
,A.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,B.GuidId FPrjId --��Ŀ���
,'���̶���ⶨ��' JFXM     --�ɷ���Ŀ
,'2' JFXMBM     --�ɷ���Ŀ����
,A.JE2 Money     --����Ԫ��
,A.JFSJ2 JFSJ     --�ɷ�ʱ��
,A.SKJBR2 SKJBR     --�տ����
,A.SKDW2 SKDW     --�տλǩ��
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

--��֤��3
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
newid() FId         --����
,A.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,B.GuidId FPrjId --��Ŀ���
,'���￱̽�����' JFXM     --�ɷ���Ŀ
,'3' JFXMBM     --�ɷ���Ŀ����
,A.JE3 Money     --����Ԫ��
,A.JFSJ3 JFSJ     --�ɷ�ʱ��
,A.SKJBR3 SKJBR     --�տ����
,A.SKDW3 SKDW     --�տλǩ��
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--��֤��4
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
newid() FId         --����
,A.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,B.GuidId FPrjId --��Ŀ���
,'�������������ල��' JFXM     --�ɷ���Ŀ
,'4' JFXMBM     --�ɷ���Ŀ����
,A.JE4 Money     --����Ԫ��
,A.JFSJ4 JFSJ     --�ɷ�ʱ��
,A.SKJBR4 SKJBR     --�տ����
,A.SKDW4 SKDW     --�տλǩ��
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--��֤��5
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
newid() FId         --����
,A.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,B.GuidId FPrjId --��Ŀ���
,'�½���Ŀ���Ϸ��η�' JFXM     --�ɷ���Ŀ
,'5' JFXMBM     --�ɷ���Ŀ����
,A.JE5 Money     --����Ԫ��
,A.JFSJ5 JFSJ     --�ɷ�ʱ��
,A.SKJBR5 SKJBR     --�տ����
,A.SKDW5 SKDW     --�տλǩ��
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--��֤��6
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
newid() FId         --����
,A.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,B.GuidId FPrjId --��Ŀ���
,'����ǽ�����ר�����' JFXM     --�ɷ���Ŀ
,'6' JFXMBM     --�ɷ���Ŀ����
,A.JE6 Money     --����Ԫ��
,A.JFSJ6 JFSJ     --�ɷ�ʱ��
,A.SKJBR6 SKJBR     --�տ����
,A.SKDW6 SKDW     --�տλǩ��
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--��֤��7
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
newid() FId         --����
,A.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,B.GuidId FPrjId --��Ŀ���
,'ɢװˮ���' JFXM     --�ɷ���Ŀ
,'7' JFXMBM     --�ɷ���Ŀ����
,A.JE7 Money     --����Ԫ��
,A.JFSJ7 JFSJ     --�ɷ�ʱ��
,A.SKJBR7 SKJBR     --�տ����
,A.SKDW7 SKDW     --�տλǩ��
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--��֤��8
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
newid() FId         --����
,A.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,B.GuidId FPrjId --��Ŀ���
,'����̻���' JFXM     --�ɷ���Ŀ
,'8' JFXMBM     --�ɷ���Ŀ����
,A.JE8 Money     --����Ԫ��
,A.JFSJ8 JFSJ     --�ɷ�ʱ��
,A.SKJBR8 SKJBR     --�տ����
,A.SKDW8 SKDW     --�տλǩ��
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--��֤��9
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
newid() FId         --����
,A.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,B.GuidId FPrjId --��Ŀ���
,'���յ�������ؽ����' JFXM     --�ɷ���Ŀ
,'9' JFXMBM     --�ɷ���Ŀ����
,A.JE9 Money     --����Ԫ��
,A.JFSJ9 JFSJ     --�ɷ�ʱ��
,A.SKJBR9 SKJBR     --�տ����
,A.SKDW9 SKDW     --�տλǩ��
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--��֤��10
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
newid() FId         --����
,A.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,B.GuidId FPrjId --��Ŀ���
,'ũ�񹤹���֧����֤�𵣱����' JFXM     --�ɷ���Ŀ
,'10' JFXMBM     --�ɷ���Ŀ����
,A.JE10 Money     --����Ԫ��
,A.JFSJ10 JFSJ     --�ɷ�ʱ��
,A.SKJBR10 SKJBR     --�տ����
,A.SKDW10 SKDW     --�տλǩ��
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

-----------------------------------------------------------------------------------------------------------------------------------------------------
--ʩ�����֤���ʩ��ͼ�����Ϣ
INSERT INTO dbCenter.dbo.TC_SGXKZ_SGTSC
SELECT 
A.ID FId         --����
,A.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,A.SCHGSBH SGTSCHGSBH     --ʩ��ͼ���ϸ�����
,B.XMBH ProjectNo --��Ŀ���
,A.STJG SGTSCJGMC --ʩ��ͼ����������
,NULL SGTSCZZJGDM     --ʩ��ͼ��������֯��������
,A.FZRQ SCWCRQ     --����������
,CONVERT(VARCHAR(20),BA.JSGM) + BA.JSGM_DW + ', ' + BA.JSGMQT ConstrScale--�����ģ
,NULL KCDWMC --���쵥λ����
,NULL KCDWZZJGDM     --���쵥λ��֯��������
,NULL SJDWMC --��Ƶ�λ����
,NULL SJDWZZJGDM --��Ƶ�λ��֯��������
,NULL YCSCSFTG --һ������Ƿ�ͨ��
,NULL YCSCWFTS --һ�����ʱΥ��ǿ����
,NULL YCSCWFTM --һ�����ʱΥ����ǿ����Ŀ
,NULL BL
,NULL YL
,NULL SGTSCJGId
,NULL KCDWId
,NULL SJDWId
FROM JKCWFDB_WORK_NJS.dbo.YW_SGTZJJSZL A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

-----------------------------------------------------------------------------------------------------------------------------------------------------
--��Ͷ�����
--��ͬ�����
INSERT INTO dbCenter.dbo.TC_SGXKZ_ZBJGBL
SELECT 
B.GuidID FId         --����
,BA.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,NULL BL     
,NULL YL
FROM JKCWFDB_WORK_NJS.dbo.YW_ZBJG_KCDW KC
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=KC.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--��Ͷ����Ϣ��
INSERT INTO dbCenter.dbo.TC_SGXKZ_ZBJG
SELECT 
B.GuidID FId         --����
,BA.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,B.XMMC ProjectName     --��Ŀ����
,FQ.Name PrjItemName --��������
,B.XMJSDW JSDW     --���赥λ
,KC.ID KCId --����

,CASE WHEN ISNULL(KC.ZBFS,'0')='�����б�' THEN '11220601'
      WHEN ISNULL(KC.ZBFS,'0')='�����б�' THEN '11220602'
 ELSE NULL END KCZBFS --�б귽ʽ

,CASE WHEN ISNULL(KC.ZZFS,'0')='ί���б�' THEN '11221001'
      WHEN ISNULL(KC.ZZFS,'0')='�����б�' THEN '11221002'
 ELSE NULL END KCZZFS --��֯��ʽ

,KC.ZBDW KCZBDW --�б굥λ
,KC.ZBQYZZZSH KCZBQYZZZSH --�б���ҵ����֤���
,KC.ZBJ KCZBJ --�б��
,KC.ZBGQ KCZBGQ --�б깤��
,KC.HTKGRQ KCHTKGTime --��ͬ��������
,KC.HTJGRQ KCHTJGTime --��ͬ��������
,KC.HTQDRQ KCHTQDTime --��ͬǩ������
,KC.HTBASJ KCHTBATime --��ͬ��������
,KC.HTBABH KCHTBABH --��ͬ�������
,KC.XMJL KCXMJL --��Ŀ����
,SJ.ID SJId --���

,CASE WHEN ISNULL(SJ.ZBFS,'0')='�����б�' THEN '11220601'
      WHEN ISNULL(SJ.ZBFS,'0')='�����б�' THEN '11220602'
 ELSE NULL END SJZBFS --�б귽ʽ

,CASE WHEN ISNULL(SJ.ZZFS,'0')='ί���б�' THEN '11221001'
      WHEN ISNULL(SJ.ZZFS,'0')='�����б�' THEN '11221002'
 ELSE NULL END SJZZFS --��֯��ʽ

,SJ.ZBDW SJZBDW --�б굥λ
,SJ.ZBQYZZZSH SJZBQYZZZSH --�б���ҵ����֤���
,NULL SJZLBZ --������׼
,SJ.ZBJ SJZBJ --�б��
,SJ.ZBGQ SJZBGQ --�б깤��
,SJ.HTKGRQ SJHTKGTime --��ͬ��������
,SJ.HTJGRQ SJHTJGTime --��ͬ��������
,SJ.HTQDRQ SJHTQDTime --��ͬǩ������
,SJ.HTBASJ SJHTBATime --��ͬ��������
,SJ.HTBABH SJHTBABH --��ͬ�������
,NULL SJHTBAJG --��ͬ��������
,JL.ID JLId --����

,CASE WHEN ISNULL(JL.ZBFS,'0')='�����б�' THEN '11220601'
      WHEN ISNULL(JL.ZBFS,'0')='�����б�' THEN '11220602'
 ELSE NULL END JLZBFS --�б귽ʽ

,CASE WHEN ISNULL(JL.ZZFS,'0')='ί���б�' THEN '11221001'
      WHEN ISNULL(JL.ZZFS,'0')='�����б�' THEN '11221002'
 ELSE NULL END JLZZFS --��֯��ʽ

,JL.ZBDW JLZBDW --�б굥λ
,JL.ZBQYZZZSH JLZBQYZZZSH --�б���ҵ����֤���
,NULL JLZBQYZZDJ --�б���ҵ���ʵȼ�
,NULL JLZLBZ --������׼
,JL.ZBJ JLZBJ --�б��
,NULL JLZBJDX --��д
,JL.ZBGQ JLZBGQ --�б깤��
,JL.JHKGRQ JLHTKGTime --��ͬ��������
,JL.JHJGRQ JLHTJGTime --��ͬ��������
,JL.HTQDRQ JLHTQDTime --��ͬǩ������
,JL.HTBASJ JLHTBATime --��ͬ��������
,JL.HTBABH JLHTBABH --��ͬ�������
,NULL JLHTBAJG --��ͬ��������
,NULL JLGCS --������ʦ

,SG.ID SGId --ʩ��

,CASE WHEN ISNULL(SG.ZBFS,'0')='�����б�' THEN '11220601'
      WHEN ISNULL(SG.ZBFS,'0')='�����б�' THEN '11220602'
 ELSE NULL END SGZBFS --�б귽ʽ

,CASE WHEN ISNULL(SG.ZZFS,'0')='ί���б�' THEN '11221001'
      WHEN ISNULL(SG.ZZFS,'0')='�����б�' THEN '11221002'
 ELSE NULL END SGZZFS --��֯��ʽ

,SG.ZBDW SGZBDW --�б굥λ
,SG.ZBQYZZZSH SGZBQYZZZSH --�б���ҵ����֤���
,NULL SGZBQYZZDJ --�б���ҵ���ʵȼ�
,NULL SGZLBZ --������׼
,NULL SGZLDJ --�����ȼ�
,SG.ZBJ SGZBJ --�б��
,NULL SGZBJDX --��д
,SG.ZBGQ SGZBGQ --�б깤��
,SG.HTKGRQ SGHTKGTime --��ͬ��������
,SG.HTJGRQ SGHTJGTime --��ͬ��������
,NULL SGZBFW --�б귶Χ
,NULL SGJZBM --�������
,NULL SGSZSNYL --ɢװˮ������
,SG.XMJLXM SGXMJL --��Ŀ��������
,NULL SGXMJLZS --��Ŀ����֤�����

,SG.HTQDSJ SGHTQDTime --��ͬǩ������
,SG.BASJ SGHTBATime --��ͬ��������
,SG.HTBABH SGHTBABH --��ͬ�������
,NULL SGHTBAJG --��ͬ��������
,B.XMBH ProjectNo --��Ŀ���
,NULL ZBTZSBH --�б�֪ͨ����
,CONVERT(VARCHAR(20),BA.JSGM) + BA.JSGM_DW + ', ' + BA.JSGMQT ConstrScale--�����ģ
,NULL Area     --�����
,NULL ZBDLDWMC     --�б����λ����
,NULL ZBDLDWZZJGDM --�б����λ��֯��������
,NULL JLZBLX --�б�����
,NULL JLZBDWZZJGDM --�б굥λ��֯��������
,NULL JLZBRQ --�б�����
,NULL JLGCSZJLX --�ܼ�����ʦ֤������
,NULL JLGCSZJHM --�ܼ�����ʦ֤������
,NULL SGZBLX --�б�����
,NULL SGZBDWZZJGDM --�б굥λ��֯��������
,NULL SGZBRQ --�б�����
,NULL SGXMJLZJLX --��Ŀ����֤������
,NULL SGXMJLZJHM --��Ŀ����֤������
,NULL BL
,NULL YL
FROM JKCWFDB_WORK_NJS.dbo.YW_ZBJG_KCDW KC
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_ZBJG_SJDW SJ ON SJ.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_ZBJG_JLDW JL ON JL.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_ZBJG_SGDW SG ON SG.YWBM=KC.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));
-----------------------------------------------------------------------------------------------------------------------------------------------------

--���蹤�̹滮���֤
INSERT INTO dbCenter.dbo.TC_SGXKZ_JSGCGHXKZ
SELECT 
A.ID FId         --����
,A.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,B.XMMC ProjectName     --��Ŀ����
,B.XMJSDW JSDW     --���赥λ
,BA.JSDD Address --�����ַ
,NULL Area     --���
,CONVERT(VARCHAR(20),BA.JSGM) + BA.JSGM_DW + ', ' + BA.JSGMQT ConstrScale--�����ģ
,BA.KDGD Span --���(�߶�)
,NULL Others     --����
,A.FZRQ CreateTime --��֤����
,A.FZJG HFJG --�˷�����
,A.XKZSBH GCGHXKZBH --���̹滮���֤����
,NULL BL
,NULL YL
FROM JKCWFDB_WORK_NJS.dbo.YW_GCGHXKZ A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

-----------------------------------------------------------------------------------------------------------------------------------------------------

--�����õع滮���֤
INSERT INTO dbCenter.dbo.TC_SGXKZ_JSYDGHXKZ
SELECT 
A.ID FId         --����
,A.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,B.XMMC ProjectName     --��Ŀ����
,B.XMJSDW JSDW     --���赥λ
,BA.JSDD Address --�����ַ
,A.YDMJ Area     --�õ����
,CONVERT(VARCHAR(20),BA.JSGM) + BA.JSGM_DW + ', ' + BA.JSGMQT ConstrScale--�����ģ
,NULL Others     --����
,A.YDXZ YDXZ --�õ�����
,A.FZRQ CreateTime --��֤����
,A.FZDW HFJG --�˷�����
,A.YDXKZBH YDGHXKZBH --�õع滮���֤���
,NULL BL
,NULL YL
FROM JKCWFDB_WORK_NJS.dbo.YW_YDGHXKZ A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));
-----------------------------------------------------------------------------------------------------------------------------------------------------
--ѡַ�����
INSERT INTO dbCenter.dbo.TC_SGXKZ_Location
SELECT 
A.ID FId         --����
,A.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,B.XMMC ProjectName     --��Ŀ����
,B.XMJSDW JSDW     --���赥λ
,A.XMNXWZ LocationAddress     --��Ŀ��ѡλ��
,A.NYDMJ Area     --���õ����
,A.NJSGM Scale     --�⽨���ģ
,A.JSXMYJ ProjectBasis     --������Ŀ����
,A.FZRQ CreateTime --��֤����
,A.HFJG HFJG --�˷�����
,A.ZSBH XZYJSZSBH --ѡַ�����֤����
,NULL BL
,NULL YL
,NULL YDPZSX
FROM JKCWFDB_WORK_NJS.dbo.YW_XZYJS A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

-----------------------------------------------------------------------------------------------------------------------------------------------------
--ʩ�����֤���ΰ�����Ϣ��
INSERT INTO dbCenter.dbo.TC_SGXKZ_PrjInfo
SELECT
BA.WYBM FId  --����
,BA.YWBM FAppId  --ҵ�����
,FQ.GuidId FPrjItemId --�������
,JS.FCompany JSDW  --���赥λ
,NULL JProvince --ʡ
,NULL JCity --��
,NULL JCounty --��
,isnull(BA.JSDWSDX,'51') JSDWAddressDept --���赥λ����
,BA.JSDWDZ JSDWDZ --���赥λ��ַ
,CASE WHEN ISNULL(BA.SYZXZ,'0')='��������' THEN '11221201'
      WHEN ISNULL(BA.SYZXZ,'0')='����' THEN '11221202'
      WHEN ISNULL(BA.SYZXZ,'0')='�ɷݺ���' THEN '11221203'
      WHEN ISNULL(BA.SYZXZ,'0')='����' THEN '11221204'
      WHEN ISNULL(BA.SYZXZ,'0')='�ɷ�����' THEN '11221205'
      WHEN ISNULL(BA.SYZXZ,'0')='˽��' THEN '11221206'
      WHEN ISNULL(BA.SYZXZ,'0')='��������' THEN '11221207'
      WHEN ISNULL(BA.SYZXZ,'0')='�ڵغ͸۰�̨����' THEN '11221208'
      WHEN ISNULL(BA.SYZXZ,'0')='��Ӫ' THEN '11221209'
      WHEN ISNULL(BA.SYZXZ,'0')='�ۡ��ġ�̨����' THEN '112212010'
      WHEN ISNULL(BA.SYZXZ,'0')='�۰�̨Ͷ�ʹɷ����ޣ���˾��' THEN '112212011'
      WHEN ISNULL(BA.SYZXZ,'0')='�����۰�̨Ͷ��' THEN '112212012'
      WHEN ISNULL(BA.SYZXZ,'0')='�������' THEN '112212013'
      WHEN ISNULL(BA.SYZXZ,'0')='����Ͷ�ʹɷ����ޣ���˾��' THEN '112212015'
      WHEN ISNULL(BA.SYZXZ,'0')='��������Ͷ��' THEN '112212016'
      WHEN ISNULL(BA.SYZXZ,'0')='����' THEN '112212099'
ELSE NULL END JSDWXZ  --����������
,BA.SQDWFDDBR FDDBR --����������
,JS.FRSJ FRDH --���˵绰
,BA.LZR LZR  --��֤��
,BA.YDDH LXDH --��ϵ�绰
,BA.JSDWJSFZR JSFZR --���赥λ����������
,BA.JSDWJSFZRZC JSFZRZC --���赥λ����������ְ��
,BA.JSDWJSFZRDH JSFZRDH --���赥λ���������˵绰
,B.XMBM PrjId --��Ŀ���
,FQ.FQBM PrjItemId --�������
,B.XMMC ProjectName --��Ŀ����
,FQ.Name PrjItemName --��������
,CASE WHEN ISNULL(BA.GCLB,'0')='01' OR ISNULL(BA.GCLB,'0')='02' OR ISNULL(BA.GCLB,'0')='03' OR
ISNULL(BA.GCLB,'0')='04' OR ISNULL(BA.GCLB,'0')='05' OR ISNULL(BA.GCLB,'0')='06' OR
ISNULL(BA.GCLB,'0')='07' OR ISNULL(BA.GCLB,'0')='08' OR ISNULL(BA.GCLB,'0')='09' OR ISNULL(BA.GCLB,'0')='10' THEN '2000101'
WHEN ISNULL(BA.GCLB,'0')='11' OR ISNULL(BA.GCLB,'0')='12' OR ISNULL(BA.GCLB,'0')='13' OR
ISNULL(BA.GCLB,'0')='14' OR ISNULL(BA.GCLB,'0')='15' OR ISNULL(BA.GCLB,'0')='16' OR
ISNULL(BA.GCLB,'0')='17' OR ISNULL(BA.GCLB,'0')='18' THEN '2000102'
WHEN ISNULL(BA.GCLB,'0')='19' THEN '2000103'
ELSE NULL END PrjItemType --������� 
,BA.BJSJ ProjectTime --����ʱ��
,BA.SBSJ ReportTime --�걨ʱ��
,NULL PProvince --ʡ
,NULL PCity --��
,NULL PCounty --��
,isnull(B.SDC,'51') PrjAddressDept --��������
,BA.JSDD Address --�����ַ
,CONVERT(VARCHAR(20),BA.JSGM) + BA.JSGM_DW + ', ' + BA.JSGMQT ConstrScale--�����ģ
,NULL ConstrType --�ṹ����
,BA.HTJG Price --��ͬ�۸�
,11221128 Currency --����
,BA.HTKGRQ StartDate --��ͬ��������
,BA.HTJGRQ EndDate --��ͬ����������
,NULL FResult --��˽��
,NULL Remark --��ע
,NULL ProjectFile --�����ļ�
,B.XMBH ProjectNo --��Ŀ����
,NULL ProjectLevel --�����
,NULL Cost --��Ͷ�ʣ���Ԫ��
,NULL Area --�������m2��
,NULL BuildType --��������
,CASE WHEN ISNULL(BA.GCLB,'0')='01' OR ISNULL(BA.GCLB,'0')='02' OR ISNULL(BA.GCLB,'0')='03' OR
ISNULL(BA.GCLB,'0')='04' OR ISNULL(BA.GCLB,'0')='05' OR ISNULL(BA.GCLB,'0')='06' OR
ISNULL(BA.GCLB,'0')='07' OR ISNULL(BA.GCLB,'0')='08' OR ISNULL(BA.GCLB,'0')='09' OR ISNULL(BA.GCLB,'0')='10' THEN '200010113'
WHEN ISNULL(BA.GCLB,'0')='11' OR ISNULL(BA.GCLB,'0')='12' OR ISNULL(BA.GCLB,'0')='13' THEN '200010209'
WHEN ISNULL(BA.GCLB,'0')='14' OR ISNULL(BA.GCLB,'0')='15' THEN '200010210' 
WHEN ISNULL(BA.GCLB,'0')='16' THEN '200010204'
WHEN ISNULL(BA.GCLB,'0')='17' THEN '200010201'
WHEN ISNULL(BA.GCLB,'0')='18' THEN '200010206'
WHEN ISNULL(BA.GCLB,'0')='19' THEN '2000103'
ELSE NULL END ProjectUse --������; 
,NULL ProjectNumber --�����ĺ�
,null SGXKZBH --ʩ�����֤���
,NULL FZJG  --
,NULl FZTime
,null DZZT
FROM JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
WHERE ISNULL(JS.FID,'')<>'' AND ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

-----------------------------------------------------------------------------------------------------------------------------------------------------
--��������
--ʩ����������
INSERT INTO dbCenter.dbo.TC_SGXKZ_File
SELECT 
A.ID FId         --����
,A.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,A.CLBH FLinkId     --���
,A.CLMC FileName --��������
,A.SCRQ ReportTime --�ϴ�ʱ��
,A.FilePath FilePath     --��ַ
,NULL FileType     --��������
,NULL Size --��С
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--����������
INSERT INTO dbCenter.dbo.TC_SGXKZ_File
SELECT 
A.ID FId         --����
,A.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,A.CLBH FLinkId     --���
,A.CLMC FileName --��������
,A.SCRQ ReportTime --�ϴ�ʱ��
,A.FilePath FilePath     --��ַ
,NULL FileType     --��������
,NULL Size --��С
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL_JL A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--ʩ����������
INSERT INTO dbCenter.dbo.TC_SGXKZ_File
SELECT 
A.ID FId         --����
,A.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,A.CLBH FLinkId     --���
,A.CLMC FileName --��������
,A.SCRQ ReportTime --�ϴ�ʱ��
,A.FilePath FilePath     --��ַ
,NULL FileType     --��������
,NULL Size --��С
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL_SG A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

--��Ƹ�������
INSERT INTO dbCenter.dbo.TC_SGXKZ_File
SELECT 
A.ID FId         --����
,A.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,A.CLBH FLinkId     --���
,A.CLMC FileName --��������
,A.SCRQ ReportTime --�ϴ�ʱ��
,A.FilePath FilePath     --��ַ
,NULL FileType     --��������
,NULL Size --��С
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL_SJ A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--���츽������
INSERT INTO dbCenter.dbo.TC_SGXKZ_File
SELECT 
A.ID FId         --����
,A.YWBM FAppId  --ҵ������
,FQ.GuidId FPrjItemId --��������
,A.CLBH FLinkId     --���
,A.CLMC FileName --��������
,A.SCRQ ReportTime --�ϴ�ʱ��
,A.FilePath FilePath     --��ַ
,NULL FileType     --��������
,NULL Size --��С
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL_KC A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

-----------------------------------------------------------------------------------------------------------------------------------------------------

--�ܰ���λ
INSERT INTO dbCenter.dbo.TC_PrjItem_Ent
SELECT 
A.ID FId         --����
,B.GuidId FPrjId --��Ŀ���
,FQ.GuidId FPrjItemId --��������
,NULL FProcId
,A.YWBM FAppId  --ҵ������
,A.QYID QYId     --��ҵid
,A.DWMC FName     --��λ����
,2 FEntType   --2.ʩ���ܰ���3.רҵ�а���4.����ְ���5.���죻6.��ƣ�7.����
,A.ZZJGDM FOrgCode     --��֯��������
,A.DWDZ FAddress     --��λ��ַ
,NULL ZZDJ --���ʵȼ�
,NULL ZZZSH
,NULL YYZZH
,A.FDDBR FLegalPerson     --����������
,A.LXDH FTel     --��ϵ�绰
,A.LXR FLinkMan     --��ϵ��
,A.YDDH FMobile     --�ƶ��绰
,A.ZXZZ mZXZZ     --��������
,A.ZXZZ_Z oZXZZ     --��������
,NULL FCreateTime
,NULL FTime
,ISNULL(A.BZ,'')  Remark     --��ע
FROM JKCWFDB_WORK_NJS.dbo.YW_SGZCBDWInfo A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));



--ר����λ
INSERT INTO dbCenter.dbo.TC_PrjItem_Ent
SELECT 
A.ID FId         --����
,B.GuidId FPrjId --��Ŀ���
,FQ.GuidId FPrjItemId --��������
,NULL FProcId
,A.YWBM FAppId  --ҵ������
,A.QYID QYId     --��ҵid
,A.DWMC FName     --��λ����
,3 FEntType   --2.ʩ���ܰ���3.רҵ�а���4.����ְ���5.���죻6.��ƣ�7.����
,A.ZZJGDM FOrgCode     --��֯��������
,A.DWDZ FAddress     --��λ��ַ
,NULL ZZDJ --���ʵȼ�
,NULL ZZZSH
,NULL YYZZH
,A.FDDBR FLegalPerson     --����������
,A.LXDH FTel     --��ϵ�绰
,A.LXR FLinkMan     --��ϵ��
,A.YDDH FMobile     --�ƶ��绰
,A.ZXZZ mZXZZ     --��������
,A.ZXZZ_Z oZXZZ     --��������     
,NULL FCreateTime                  
,NULL FTime                        
,ISNULL(A.BZ,'')  Remark     --��ע
FROM JKCWFDB_WORK_NJS.dbo.YW_ZYCBDW A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

--����
INSERT INTO dbCenter.dbo.TC_PrjItem_Ent
SELECT 
A.ID FId         --����
,B.GuidId FPrjId --��Ŀ���
,FQ.GuidId FPrjItemId --��������
,NULL FProcId
,A.YWBM FAppId  --ҵ������
,A.QYID QYId     --��ҵid
,A.DWMC FName     --��λ����
,4 FEntType   --2.ʩ���ܰ���3.רҵ�а���4.����ְ���5.���죻6.��ƣ�7.����
,A.ZZJGDM FOrgCode     --��֯��������
,A.DWDZ FAddress     --��λ��ַ
,NULL ZZDJ --���ʵȼ�
,NULL ZZZSH
,NULL YYZZH
,A.FDDBR FLegalPerson     --����������
,A.LXDH FTel     --��ϵ�绰
,A.LXR FLinkMan     --��ϵ��
,A.YDDH FMobile     --�ƶ��绰
,A.ZXZZ mZXZZ     --��������
,NULL oZXZZ     --��������     
,NULL FCreateTime                  
,NULL FTime                        
,ISNULL(A.BZ,'')  Remark     --��ע
FROM JKCWFDB_WORK_NJS.dbo.YW_LWFBDW A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));



--����
INSERT INTO dbCenter.dbo.TC_PrjItem_Ent
SELECT 
A.ID FId         --����
,B.GuidId FPrjId --��Ŀ���
,FQ.GuidId FPrjItemId --��������
,NULL FProcId
,A.YWBM FAppId  --ҵ������
,A.QYID QYId     --��ҵid
,A.DWMC FName     --��λ����
,5 FEntType   --2.ʩ���ܰ���3.רҵ�а���4.����ְ���5.���죻6.��ƣ�7.����
,A.ZZJGDM FOrgCode     --��֯��������
,A.DWDZ FAddress     --��λ��ַ
,NULL ZZDJ --���ʵȼ�
,NULL ZZZSH
,NULL YYZZH
,A.FDDBR FLegalPerson     --����������
,A.LXDH FTel     --��ϵ�绰
,A.LXR FLinkMan     --��ϵ��
,A.YDDH FMobile     --�ƶ��绰
,A.ZXZZ mZXZZ     --��������
,NULL oZXZZ     --��������     
,NULL FCreateTime                  
,NULL FTime                        
,ISNULL(A.BZ,'')  Remark     --��ע
FROM JKCWFDB_WORK_NJS.dbo.YW_KCDW A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--���
INSERT INTO dbCenter.dbo.TC_PrjItem_Ent
SELECT 
A.ID FId         --����
,B.GuidId FPrjId --��Ŀ���
,FQ.GuidId FPrjItemId --��������
,NULL FProcId
,A.YWBM FAppId  --ҵ������
,A.QYID QYId     --��ҵid
,A.DWMC FName     --��λ����
,6 FEntType   --2.ʩ���ܰ���3.רҵ�а���4.����ְ���5.���죻6.��ƣ�7.����
,A.ZZJGDM FOrgCode     --��֯��������
,A.DWDZ FAddress     --��λ��ַ
,NULL ZZDJ --���ʵȼ�
,NULL ZZZSH
,NULL YYZZH
,A.FDDBR FLegalPerson     --����������
,A.LXDH FTel     --��ϵ�绰
,A.LXR FLinkMan     --��ϵ��
,A.YDDH FMobile     --�ƶ��绰
,A.ZXZZ mZXZZ     --��������
,NULL oZXZZ     --��������     
,NULL FCreateTime                  
,NULL FTime                        
,ISNULL(A.BZ,'')  Remark     --��ע
FROM JKCWFDB_WORK_NJS.dbo.YW_SJDW A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));



--����
INSERT INTO dbCenter.dbo.TC_PrjItem_Ent
SELECT 
A.ID FId         --����
,B.GuidId FPrjId --��Ŀ���
,FQ.GuidId FPrjItemId --��������
,NULL FProcId
,A.YWBM FAppId  --ҵ������
,A.QYID QYId     --��ҵid
,A.DWMC FName     --��λ����
,7 FEntType   --2.ʩ���ܰ���3.רҵ�а���4.����ְ���5.���죻6.��ƣ�7.����
,A.ZZJGDM FOrgCode     --��֯��������
,A.DWDZ FAddress     --��λ��ַ
,A.ZZDJ ZZDJ --���ʵȼ�
,A.ZGZSH ZZZSH
,A.ZGZH YYZZH
,A.FDDBR FLegalPerson     --����������
,A.LXDH FTel     --��ϵ�绰
,A.LXR FLinkMan     --��ϵ��
,A.YDDH FMobile     --�ƶ��绰
,A.ZXZZ mZXZZ     --��������
,NULL oZXZZ     --��������     
,NULL FCreateTime                  
,NULL FTime                        
,ISNULL(A.BZ,'')  Remark     --��ע
FROM JKCWFDB_WORK_NJS.dbo.YW_JLDW A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));



--�ܰ���λ��Ա
INSERT INTO dbCenter.dbo.TC_PrjItem_Emp
SELECT 
A.ID FId         --����
,B.GuidId FPrjId --��Ŀ���
,FQ.GuidId FPrjItemId --��������
,A.YWBM FAppId  --ҵ������
,A.PID FEntId --���
,A.XM FHumanName --����
,CASE WHEN ISNULL(A.XB,'0')='��' THEN '1'
      WHEN ISNULL(A.XB,'0')='Ů' THEN '2'
ELSE NULL END FSex --�Ա�
,NULL FPhoto --��Ƭ
,NULL FBirthDay --����
,NULL ZJLX --֤������
,CASE WHEN ISNULL(A.ZGXL,'0')='��ʿ��' THEN '107000'
      WHEN ISNULL(A.ZGXL,'0')='��ʿ' THEN '107001'
      WHEN ISNULL(A.ZGXL,'0')='˶ʿ' THEN '107002'
      WHEN ISNULL(A.ZGXL,'0')='����' THEN '107003'
      WHEN ISNULL(A.ZGXL,'0')='��ר' THEN '107004'
      WHEN ISNULL(A.ZGXL,'0')='��ר' THEN '107005'
      WHEN ISNULL(A.ZGXL,'0')='���м�����' THEN '107006'
      WHEN ISNULL(A.ZGXL,'0')='��' THEN '107007'
ELSE NULL END ZGXL --���ѧ��
,A.YDDH FMobile     --�ƶ��绰
,A.LXDH FTel     --��ϵ�绰
,CASE WHEN ISNULL(A.RYLX,'0')='��Ŀ����' THEN '11220201'
      WHEN ISNULL(A.RYLX,'0')='��Ŀ����������' THEN '11220202'
      WHEN ISNULL(A.RYLX,'0')='��ȫ������' THEN '11220203'
      WHEN ISNULL(A.RYLX,'0')='ʩ��Ա' THEN '11220204'
      WHEN ISNULL(A.RYLX,'0')='����Ա' THEN '11220205'
      WHEN ISNULL(A.RYLX,'0')='רְ��ȫԱ' THEN '11220206'
      WHEN ISNULL(A.RYLX,'0')='����Ա' THEN '11220207'
      WHEN ISNULL(A.RYLX,'0')='Ԥ��Ա' THEN '11220208'
      WHEN ISNULL(A.RYLX,'0')='�ܼ�����ʦ' THEN '11220209'
      WHEN ISNULL(A.RYLX,'0')='רҵ������ʦ' THEN '11220210'
      WHEN ISNULL(A.RYLX,'0')='����Ա' THEN '11220211'
      WHEN ISNULL(A.RYLX,'0')='����' THEN '11220212'
ELSE NULL END EmpType --��Ա����
,A.SFZH FIdCard     --���֤��
,NULL XMZW
,NULL ZJHM
,NULL FEntName
,A.ZW ZW     --ְ��
,CASE WHEN ISNULL(A.ZC,'0')='���ڼ��߹�' THEN '5081'
      WHEN ISNULL(A.ZC,'0')='����ʦ' THEN '5082'
      WHEN ISNULL(A.ZC,'0')='�߼�����ʦ' THEN '5083'
      WHEN ISNULL(A.ZC,'0')='������ʦ' THEN '5084'
      WHEN ISNULL(A.ZC,'0')='����' THEN '5085'
ELSE NULL END ZC --ְ��
,A.ZY ZY     --רҵ
,A.ZSBH ZSBH     --֤����
,A.DJ DJ     --�ȼ�
,A.ZCZSBH ZCBH     --ע����
,A.ZCZY ZCZY     --ע��רҵ
,A.FZRQ ZCRQ     --ע������
,NULL FCreateTime
,NULL FTime
,NULL Remark
,A.RYID FEmpId --��Ա���
,null Pid
,null FlinkId
,null FentType 
FROM JKCWFDB_WORK_NJS.dbo.YW_SGZCBDW_RYXX A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--רҵ�а���Ա
INSERT INTO dbCenter.dbo.TC_PrjItem_Emp
SELECT 
A.ID FId         --����
,B.GuidId FPrjId --��Ŀ���
,FQ.GuidId FPrjItemId --��������
,A.YWBM FAppId  --ҵ������
,A.PID FEntId --��ҵ���
,A.XM FHumanName --����
,CASE WHEN ISNULL(A.XB,'0')='��' THEN '1'
      WHEN ISNULL(A.XB,'0')='Ů' THEN '2'
ELSE NULL END FSex --�Ա�
,NULL FPhoto --��Ƭ
,NULL FBirthDay --����
,NULL ZJLX --֤������
,CASE WHEN ISNULL(A.ZGXL,'0')='��ʿ��' THEN '107000'
      WHEN ISNULL(A.ZGXL,'0')='��ʿ' THEN '107001'
      WHEN ISNULL(A.ZGXL,'0')='˶ʿ' THEN '107002'
      WHEN ISNULL(A.ZGXL,'0')='����' THEN '107003'
      WHEN ISNULL(A.ZGXL,'0')='��ר' THEN '107004'
      WHEN ISNULL(A.ZGXL,'0')='��ר' THEN '107005'
      WHEN ISNULL(A.ZGXL,'0')='���м�����' THEN '107006'
      WHEN ISNULL(A.ZGXL,'0')='��' THEN '107007'
ELSE NULL END ZGXL --���ѧ��
,A.YDDH FMobile     --�ƶ��绰
,A.LXDH FTel     --��ϵ�绰
,CASE WHEN ISNULL(A.RYLX,'0')='��Ŀ����' THEN '11220201'
      WHEN ISNULL(A.RYLX,'0')='��Ŀ����������' THEN '11220202'
      WHEN ISNULL(A.RYLX,'0')='��ȫ������' THEN '11220203'
      WHEN ISNULL(A.RYLX,'0')='ʩ��Ա' THEN '11220204'
      WHEN ISNULL(A.RYLX,'0')='����Ա' THEN '11220205'
      WHEN ISNULL(A.RYLX,'0')='רְ��ȫԱ' THEN '11220206'
      WHEN ISNULL(A.RYLX,'0')='����Ա' THEN '11220207'
      WHEN ISNULL(A.RYLX,'0')='Ԥ��Ա' THEN '11220208'
      WHEN ISNULL(A.RYLX,'0')='�ܼ�����ʦ' THEN '11220209'
      WHEN ISNULL(A.RYLX,'0')='רҵ������ʦ' THEN '11220210'
      WHEN ISNULL(A.RYLX,'0')='����Ա' THEN '11220211'
      WHEN ISNULL(A.RYLX,'0')='����' THEN '11220212'
ELSE NULL END EmpType --��Ա����
,A.SFZH FIdCard     --���֤��
,NULL XMZW
,NULL ZJHM
,NULL FEntName
,A.ZW ZW     --ְ��
,CASE WHEN ISNULL(A.ZC,'0')='���ڼ��߹�' THEN '5081'
      WHEN ISNULL(A.ZC,'0')='����ʦ' THEN '5082'
      WHEN ISNULL(A.ZC,'0')='�߼�����ʦ' THEN '5083'
      WHEN ISNULL(A.ZC,'0')='������ʦ' THEN '5084'
      WHEN ISNULL(A.ZC,'0')='����' THEN '5085'
ELSE NULL END ZC --ְ��
,A.ZY ZY     --רҵ
,A.ZSBH ZSBH     --֤����
,A.DJ DJ     --�ȼ�
,A.ZCZSBH ZCBH     --ע����
,A.ZCZY ZCZY     --ע��רҵ
,A.FZRQ ZCRQ     --ע������
,NULL FCreateTime
,NULL FTime
,NULL Remark
,A.RYID FEmpId --��Ա���
,null Pid
,null FlinkId
,null FentType 
FROM JKCWFDB_WORK_NJS.dbo.YW_ZYCBDW_RYXX A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

--����ְ���Ա
INSERT INTO dbCenter.dbo.TC_PrjItem_Emp
SELECT 
A.ID FId         --����
,B.GuidId FPrjId --��Ŀ���
,FQ.GuidId FPrjItemId --��������
,A.YWBM FAppId  --ҵ������
,A.PID FEntId --��ҵ���
,A.XM FHumanName --����
,CASE WHEN ISNULL(A.XB,'0')='��' THEN '1'
      WHEN ISNULL(A.XB,'0')='Ů' THEN '2'
ELSE NULL END FSex --�Ա�
,NULL FPhoto --��Ƭ
,NULL FBirthDay --����
,NULL ZJLX --֤������
,CASE WHEN ISNULL(A.ZGXL,'0')='��ʿ��' THEN '107000'
      WHEN ISNULL(A.ZGXL,'0')='��ʿ' THEN '107001'
      WHEN ISNULL(A.ZGXL,'0')='˶ʿ' THEN '107002'
      WHEN ISNULL(A.ZGXL,'0')='����' THEN '107003'
      WHEN ISNULL(A.ZGXL,'0')='��ר' THEN '107004'
      WHEN ISNULL(A.ZGXL,'0')='��ר' THEN '107005'
      WHEN ISNULL(A.ZGXL,'0')='���м�����' THEN '107006'
      WHEN ISNULL(A.ZGXL,'0')='��' THEN '107007'
ELSE NULL END ZGXL --���ѧ��
,A.YDDH FMobile     --�ƶ��绰
,A.LXDH FTel     --��ϵ�绰
,CASE WHEN ISNULL(A.RYLX,'0')='��Ŀ����' THEN '11220201'
      WHEN ISNULL(A.RYLX,'0')='��Ŀ����������' THEN '11220202'
      WHEN ISNULL(A.RYLX,'0')='��ȫ������' THEN '11220203'
      WHEN ISNULL(A.RYLX,'0')='ʩ��Ա' THEN '11220204'
      WHEN ISNULL(A.RYLX,'0')='����Ա' THEN '11220205'
      WHEN ISNULL(A.RYLX,'0')='רְ��ȫԱ' THEN '11220206'
      WHEN ISNULL(A.RYLX,'0')='����Ա' THEN '11220207'
      WHEN ISNULL(A.RYLX,'0')='Ԥ��Ա' THEN '11220208'
      WHEN ISNULL(A.RYLX,'0')='�ܼ�����ʦ' THEN '11220209'
      WHEN ISNULL(A.RYLX,'0')='רҵ������ʦ' THEN '11220210'
      WHEN ISNULL(A.RYLX,'0')='����Ա' THEN '11220211'
      WHEN ISNULL(A.RYLX,'0')='����' THEN '11220212'
ELSE NULL END EmpType --��Ա����
,A.SFZH FIdCard     --���֤��
,NULL XMZW
,NULL ZJHM
,NULL FEntName
,A.ZW ZW     --ְ��
,CASE WHEN ISNULL(A.ZC,'0')='���ڼ��߹�' THEN '5081'
      WHEN ISNULL(A.ZC,'0')='����ʦ' THEN '5082'
      WHEN ISNULL(A.ZC,'0')='�߼�����ʦ' THEN '5083'
      WHEN ISNULL(A.ZC,'0')='������ʦ' THEN '5084'
      WHEN ISNULL(A.ZC,'0')='����' THEN '5085'
ELSE NULL END ZC --ְ��
,A.ZY ZY     --רҵ
,A.ZSBH ZSBH     --֤����
,A.DJ DJ     --�ȼ�
,A.ZCZSBH ZCBH     --ע����
,A.ZCZY ZCZY     --ע��רҵ
,A.FZRQ ZCRQ     --ע������
,NULL FCreateTime
,NULL FTime
,NULL Remark
,A.RYID FEmpId --��Ա���
,null Pid
,null FlinkId
,null FentType 
FROM JKCWFDB_WORK_NJS.dbo.YW_LWFBDW_RYXX A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--������Ա
INSERT INTO dbCenter.dbo.TC_PrjItem_Emp
SELECT 
A.ID FId         --����
,B.GuidId FPrjId --��Ŀ���
,FQ.GuidId FPrjItemId --��������
,A.YWBM FAppId  --ҵ������
,A.PID FEntId --��ҵ���
,A.XM FHumanName --����
,CASE WHEN ISNULL(A.XB,'0')='��' THEN '1'
      WHEN ISNULL(A.XB,'0')='Ů' THEN '2'
ELSE NULL END FSex --�Ա�
,NULL FPhoto --��Ƭ
,NULL FBirthDay --����
,NULL ZJLX --֤������
,CASE WHEN ISNULL(A.ZGXL,'0')='��ʿ��' THEN '107000'
      WHEN ISNULL(A.ZGXL,'0')='��ʿ' THEN '107001'
      WHEN ISNULL(A.ZGXL,'0')='˶ʿ' THEN '107002'
      WHEN ISNULL(A.ZGXL,'0')='����' THEN '107003'
      WHEN ISNULL(A.ZGXL,'0')='��ר' THEN '107004'
      WHEN ISNULL(A.ZGXL,'0')='��ר' THEN '107005'
      WHEN ISNULL(A.ZGXL,'0')='���м�����' THEN '107006'
      WHEN ISNULL(A.ZGXL,'0')='��' THEN '107007'
ELSE NULL END ZGXL --���ѧ��
,A.YDDH FMobile     --�ƶ��绰
,A.LXDH FTel     --��ϵ�绰
,CASE WHEN ISNULL(A.RYLX,'0')='��Ŀ����' THEN '11220201'
      WHEN ISNULL(A.RYLX,'0')='��Ŀ����������' THEN '11220202'
      WHEN ISNULL(A.RYLX,'0')='��ȫ������' THEN '11220203'
      WHEN ISNULL(A.RYLX,'0')='ʩ��Ա' THEN '11220204'
      WHEN ISNULL(A.RYLX,'0')='����Ա' THEN '11220205'
      WHEN ISNULL(A.RYLX,'0')='רְ��ȫԱ' THEN '11220206'
      WHEN ISNULL(A.RYLX,'0')='����Ա' THEN '11220207'
      WHEN ISNULL(A.RYLX,'0')='Ԥ��Ա' THEN '11220208'
      WHEN ISNULL(A.RYLX,'0')='�ܼ�����ʦ' THEN '11220209'
      WHEN ISNULL(A.RYLX,'0')='רҵ������ʦ' THEN '11220210'
      WHEN ISNULL(A.RYLX,'0')='����Ա' THEN '11220211'
      WHEN ISNULL(A.RYLX,'0')='����' THEN '11220212'
ELSE NULL END EmpType --��Ա����
,A.SFZH FIdCard     --���֤��
,NULL XMZW
,NULL ZJHM
,NULL FEntName
,A.ZW ZW     --ְ��
,CASE WHEN ISNULL(A.ZC,'0')='���ڼ��߹�' THEN '5081'
      WHEN ISNULL(A.ZC,'0')='����ʦ' THEN '5082'
      WHEN ISNULL(A.ZC,'0')='�߼�����ʦ' THEN '5083'
      WHEN ISNULL(A.ZC,'0')='������ʦ' THEN '5084'
      WHEN ISNULL(A.ZC,'0')='����' THEN '5085'
ELSE NULL END ZC --ְ��
,A.ZY ZY     --רҵ
,A.ZSBH ZSBH     --֤����
,A.DJ DJ     --�ȼ�
,A.ZCZSBH ZCBH     --ע����
,A.ZCZY ZCZY     --ע��רҵ
,A.FZRQ ZCRQ     --ע������
,NULL FCreateTime
,NULL FTime
,NULL Remark
,A.RYID FEmpId --��Ա���
,null Pid
,null FlinkId
,null FentType 
FROM JKCWFDB_WORK_NJS.dbo.YW_KCDW_RYXX A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
  and not exists(select 1 from dbCenter.dbo.TC_PrjItem_Emp cc where a.id = cc.fid)


--�����Ա
INSERT INTO dbCenter.dbo.TC_PrjItem_Emp
SELECT 
A.ID FId         --����
,B.GuidId FPrjId --��Ŀ���
,FQ.GuidId FPrjItemId --��������
,A.YWBM FAppId  --ҵ������
,A.PID FEntId --��ҵ���
,A.XM FHumanName --����
,CASE WHEN ISNULL(A.XB,'0')='��' THEN '1'
      WHEN ISNULL(A.XB,'0')='Ů' THEN '2'
ELSE NULL END FSex --�Ա�
,NULL FPhoto --��Ƭ
,NULL FBirthDay --����
,NULL ZJLX --֤������
,CASE WHEN ISNULL(A.ZGXL,'0')='��ʿ��' THEN '107000'
      WHEN ISNULL(A.ZGXL,'0')='��ʿ' THEN '107001'
      WHEN ISNULL(A.ZGXL,'0')='˶ʿ' THEN '107002'
      WHEN ISNULL(A.ZGXL,'0')='����' THEN '107003'
      WHEN ISNULL(A.ZGXL,'0')='��ר' THEN '107004'
      WHEN ISNULL(A.ZGXL,'0')='��ר' THEN '107005'
      WHEN ISNULL(A.ZGXL,'0')='���м�����' THEN '107006'
      WHEN ISNULL(A.ZGXL,'0')='��' THEN '107007'
ELSE NULL END ZGXL --���ѧ��
,A.YDDH FMobile     --�ƶ��绰
,A.LXDH FTel     --��ϵ�绰
,CASE WHEN ISNULL(A.RYLX,'0')='��Ŀ����' THEN '11220201'
      WHEN ISNULL(A.RYLX,'0')='��Ŀ����������' THEN '11220202'
      WHEN ISNULL(A.RYLX,'0')='��ȫ������' THEN '11220203'
      WHEN ISNULL(A.RYLX,'0')='ʩ��Ա' THEN '11220204'
      WHEN ISNULL(A.RYLX,'0')='����Ա' THEN '11220205'
      WHEN ISNULL(A.RYLX,'0')='רְ��ȫԱ' THEN '11220206'
      WHEN ISNULL(A.RYLX,'0')='����Ա' THEN '11220207'
      WHEN ISNULL(A.RYLX,'0')='Ԥ��Ա' THEN '11220208'
      WHEN ISNULL(A.RYLX,'0')='�ܼ�����ʦ' THEN '11220209'
      WHEN ISNULL(A.RYLX,'0')='רҵ������ʦ' THEN '11220210'
      WHEN ISNULL(A.RYLX,'0')='����Ա' THEN '11220211'
      WHEN ISNULL(A.RYLX,'0')='����' THEN '11220212'
ELSE NULL END EmpType --��Ա����
,A.SFZH FIdCard     --���֤��
,NULL XMZW
,NULL ZJHM
,NULL FEntName
,A.ZW ZW     --ְ��
,CASE WHEN ISNULL(A.ZC,'0')='���ڼ��߹�' THEN '5081'
      WHEN ISNULL(A.ZC,'0')='����ʦ' THEN '5082'
      WHEN ISNULL(A.ZC,'0')='�߼�����ʦ' THEN '5083'
      WHEN ISNULL(A.ZC,'0')='������ʦ' THEN '5084'
      WHEN ISNULL(A.ZC,'0')='����' THEN '5085'
ELSE NULL END ZC --ְ��
,A.ZY ZY     --רҵ
,A.ZSBH ZSBH     --֤����
,A.DJ DJ     --�ȼ�
,A.ZCZSBH ZCBH     --ע����
,A.ZCZY ZCZY     --ע��רҵ
,A.FZRQ ZCRQ     --ע������
,NULL FCreateTime
,NULL FTime
,NULL Remark
,A.RYID FEmpId --��Ա���
,null Pid
,null FlinkId
,null FentType 
FROM JKCWFDB_WORK_NJS.dbo.YW_SJDW_RYXX A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
  and not exists(select 1 from dbCenter.dbo.TC_PrjItem_Emp cc where a.id = cc.fid)


--������Ա
INSERT INTO dbCenter.dbo.TC_PrjItem_Emp
SELECT 
A.ID FId         --����
,B.GuidId FPrjId --��Ŀ���
,FQ.GuidId FPrjItemId --��������
,A.YWBM FAppId  --ҵ������
,A.PID FEntId --��ҵ���
,A.XM FHumanName --����
,CASE WHEN ISNULL(A.XB,'0')='��' THEN '1'
      WHEN ISNULL(A.XB,'0')='Ů' THEN '2'
ELSE NULL END FSex --�Ա�
,NULL FPhoto --��Ƭ
,NULL FBirthDay --����
,NULL ZJLX --֤������
,CASE WHEN ISNULL(A.ZGXL,'0')='��ʿ��' THEN '107000'
      WHEN ISNULL(A.ZGXL,'0')='��ʿ' THEN '107001'
      WHEN ISNULL(A.ZGXL,'0')='˶ʿ' THEN '107002'
      WHEN ISNULL(A.ZGXL,'0')='����' THEN '107003'
      WHEN ISNULL(A.ZGXL,'0')='��ר' THEN '107004'
      WHEN ISNULL(A.ZGXL,'0')='��ר' THEN '107005'
      WHEN ISNULL(A.ZGXL,'0')='���м�����' THEN '107006'
      WHEN ISNULL(A.ZGXL,'0')='��' THEN '107007'
ELSE NULL END ZGXL --���ѧ��
,A.YDDH FMobile     --�ƶ��绰
,A.LXDH FTel     --��ϵ�绰
,CASE WHEN ISNULL(A.RYLX,'0')='��Ŀ����' THEN '11220201'
      WHEN ISNULL(A.RYLX,'0')='��Ŀ����������' THEN '11220202'
      WHEN ISNULL(A.RYLX,'0')='��ȫ������' THEN '11220203'
      WHEN ISNULL(A.RYLX,'0')='ʩ��Ա' THEN '11220204'
      WHEN ISNULL(A.RYLX,'0')='����Ա' THEN '11220205'
      WHEN ISNULL(A.RYLX,'0')='רְ��ȫԱ' THEN '11220206'
      WHEN ISNULL(A.RYLX,'0')='����Ա' THEN '11220207'
      WHEN ISNULL(A.RYLX,'0')='Ԥ��Ա' THEN '11220208'
      WHEN ISNULL(A.RYLX,'0')='�ܼ�����ʦ' THEN '11220209'
      WHEN ISNULL(A.RYLX,'0')='רҵ������ʦ' THEN '11220210'
      WHEN ISNULL(A.RYLX,'0')='����Ա' THEN '11220211'
      WHEN ISNULL(A.RYLX,'0')='����' THEN '11220212'
ELSE NULL END EmpType --��Ա����
,A.SFZH FIdCard     --���֤��
,NULL XMZW
,NULL ZJHM
,NULL FEntName
,A.ZW ZW     --ְ��
,CASE WHEN ISNULL(A.ZC,'0')='���ڼ��߹�' THEN '5081'
      WHEN ISNULL(A.ZC,'0')='����ʦ' THEN '5082'
      WHEN ISNULL(A.ZC,'0')='�߼�����ʦ' THEN '5083'
      WHEN ISNULL(A.ZC,'0')='������ʦ' THEN '5084'
      WHEN ISNULL(A.ZC,'0')='����' THEN '5085'
ELSE NULL END ZC --ְ��
,A.ZY ZY     --רҵ
,A.ZSBH ZSBH     --֤����
,A.DJ DJ     --�ȼ�
,A.ZCZSBH ZCBH     --ע����
,A.ZCZY ZCZY     --ע��רҵ
,A.FZRQ ZCRQ     --ע������
,NULL FCreateTime
,NULL FTime
,NULL Remark
,A.RYID FEmpId --��Ա���
,null Pid
,null FlinkId
,null FentType 
FROM JKCWFDB_WORK_NJS.dbo.YW_JLDW_RYXX A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --��֤��Ŀ����ͷ��ڱ��붼��ֵ 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --�ų����赥λ�����ʺ�
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
  and not exists(select 1 from dbCenter.dbo.TC_PrjItem_Emp cc where a.id = cc.fid)
-----------------------------------------------------------------------------------------------------------------------------------------------------
--ʩ�����֤��Ա������Ҫ���ӵ���



--���������б걸���Ĺ�������
--CF_App_ProcessInstance   ���������̱�
--CF_App_ProcessRecord     ���������̱�
if not exists (select 1
            from  sysobjects
           where  id = object_id('dbo._App_ProcessInstance_SGXK')
            and   type = 'U')
begin 
    SELECT * into _App_ProcessInstance_SGXK from CF_App_ProcessInstance where 1=2  --ֻȡ��ṹ
    alter table _App_ProcessInstance_SGXK alter column FentName varchar(200) null       

	   insert into _App_ProcessInstance_SGXK
	   (fid,FisDeleted,Ftime,FCreateTime,FBaseInfoID,FEntName,FEmpId,FLinkId,FState,FIsPrime,
			  FIsTemp,FListId,FTypeId,FLevelId,FProcessId,FManageDeptId,FManageTypeId,--FSubFlowId,
			  FResult,Fyear,FMonth,FSubmitDate,FReportDate,FCurStepID,FroleId,FBeginRoleId,FDefineDay,
			  FAppState,FSystemId,FIsNew,FSeeState,fseetime,FPlanTime,FFactTime,FBarCode)
       select newid(),0,a.FwriteDate,a.FwriteDate,a.FBaseinfoId,a.FBaseName,a.FLinkId as FEmpId,a.Fid,1,0,
	          0,'19301','1930100','1930100','72aac054-972a-473a-b03d-d878f622dc3f',isnull(a.fupdeptid,'51'),a.FManageTypeId,
			  '',year(a.FwriteDate),month(a.FwriteDate),a.FReportDate,a.FReportDate,isnull(a.fupdeptid,'51'),'8801','8801',1,1,'1122',0,null,null,null,null,null
	     from _App_List_SGXK a
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
         from _App_ProcessInstance_SGXK a
		where not exists(select 1 from CF_App_ProcessInstance b where a.FId = b.fid)


if not exists (select 1
            from  sysobjects
           where  id = object_id('dbo._App_ProcessRecord_SGXK')
            and   type = 'U')
begin

  select * into _App_ProcessRecord_SGXK from CF_App_ProcessRecord where 1=2

  insert into _App_ProcessRecord_SGXK
              (fid,FTime,FIsDeleted,FProcessInstanceID,FLinkId,FSubFlowId,FMeasure,Fresult,FManageDeptId,
			  FReportTime,FDefineDay,FRoleId,FLevel,FOrder,FRoleDesc,FTypeId,FIsQuali,FIsPrint )
	   select newid(),a.FSubmitDate,0,a.fid,a.FLinkId,a.FSubFlowId,0,null,isnull(FManageDeptId,'51'),
	          a.FReportDate,1,'8801',1,1,'���������',1,2,1
	     from _App_ProcessInstance_SGXK a
		where  not exists(select 1 from CF_App_ProcessRecord b where a.FId = b.FProcessInstanceID)


end 

  insert into CF_App_ProcessRecord
              (fid,FTime,FIsDeleted,FProcessInstanceID,FLinkId,FSubFlowId,FMeasure,Fresult,FManageDeptId,
			  FReportTime,FDefineDay,FRoleId,FLevel,FOrder,FRoleDesc,FTypeId,FIsQuali,FIsPrint )
       select fid,FTime,FIsDeleted,FProcessInstanceID,FLinkId,FSubFlowId,FMeasure,Fresult,FManageDeptId,
			  FReportTime,FDefineDay,FRoleId,FLevel,FOrder,FRoleDesc,FTypeId,FIsQuali,FIsPrint
		 from _App_ProcessRecord_SGXK a
	    where not exists(select 1 from CF_App_ProcessRecord b where a.FId = b.fid)