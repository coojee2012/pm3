
begin
----------------------------------------------------------------------------------------------------------------------------------------------------
--删除施工许可证相关信息
--业务主表
Delete dbCenter.dbo.CF_App_List where FId in 
(SELECT 
BA.YWBM    --业务编码
FROM JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_YWInfo YW ON YW.YWBM=B.YWBM 
WHERE ISNULL(JS.FID,'')<>'' AND ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
  );

--项目表
delete from  dbCenter.dbo.TC_Prj_Info where FId in (
SELECT --A.*
A.GuidId         --项目id
FROM JKCWFDB_WORK_NJS.DBO.YW_XMInfo AS A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=A.JSDW
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS BA ON BA.YWBM=A.YWBM
WHERE ISNULL(JS.FID,'')<>'' AND ISNULL(A.XMBM,'')<>'' --保证项目编码有值 
  AND A.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--单体工程表
delete from dbCenter.dbo.TC_PrjItem_Info where FId in (
SELECT 
FQ.GuidId
FROM JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
WHERE ISNULL(JS.FID,'')<>'' AND ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--保证金
delete from dbCenter.dbo.TC_SGXKZ_BZJQR where FAppId in (
SELECT 
A.YWBM
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--施工图审查信息
delete from dbCenter.dbo.TC_SGXKZ_SGTSC where FId in (
SELECT 
A.ID         --主键
FROM JKCWFDB_WORK_NJS.dbo.YW_SGTZJJSZL A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--合同办理表
delete from dbCenter.dbo.TC_SGXKZ_ZBJGBL where FId in (
SELECT 
B.GuidID       --主键
FROM JKCWFDB_WORK_NJS.dbo.YW_ZBJG_KCDW KC
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=KC.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--招投标信息表
delete from dbCenter.dbo.TC_SGXKZ_ZBJG where FId in (
SELECT 
B.GuidID         --主键
FROM JKCWFDB_WORK_NJS.dbo.YW_ZBJG_KCDW KC
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_ZBJG_SJDW SJ ON SJ.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_ZBJG_JLDW JL ON JL.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_ZBJG_SGDW SG ON SG.YWBM=KC.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--建设工程规划许可证表
delete from dbCenter.dbo.TC_SGXKZ_JSGCGHXKZ where FId in (
SELECT 
A.ID          --主键
FROM JKCWFDB_WORK_NJS.dbo.YW_GCGHXKZ A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--建设用地规划许可证
delete from dbCenter.dbo.TC_SGXKZ_JSYDGHXKZ where FId in (
SELECT 
A.ID         --主键
FROM JKCWFDB_WORK_NJS.dbo.YW_YDGHXKZ A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--选址意见书
delete from dbCenter.dbo.TC_SGXKZ_Location where FId in (
SELECT 
A.ID         --主键
FROM JKCWFDB_WORK_NJS.dbo.YW_XZYJS A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--施工许可证初次办理信息表
delete from  dbCenter.dbo.TC_SGXKZ_PrjInfo where FId in (
SELECT
BA.WYBM  --主键
FROM JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
WHERE ISNULL(JS.FID,'')<>'' AND ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--附件材料
delete from dbCenter.dbo.TC_SGXKZ_File where FId in (
SELECT 
A.ID
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);


--监理附件材料
delete from dbCenter.dbo.TC_SGXKZ_File where FId in (
SELECT 
A.ID
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL_JL A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);


--施工附件材料
delete from dbCenter.dbo.TC_SGXKZ_File where FId in (
SELECT 
A.ID
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL_SG A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--设计附件材料
delete from dbCenter.dbo.TC_SGXKZ_File where FId in (
SELECT 
A.ID
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL_SJ A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);


--勘察附件材料
delete from dbCenter.dbo.TC_SGXKZ_File where FId in (
SELECT 
A.ID 
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL_KC A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--单项工程参建单位表
delete from dbCenter.dbo.TC_PrjItem_Ent where FAppId in (
SELECT 
A.YWBM
FROM JKCWFDB_WORK_NJS.dbo.YW_SGZCBDWInfo A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);

--单项工程参建人员表
delete from dbCenter.dbo.TC_PrjItem_Emp where FAppId in (
SELECT 
A.YWBM  --业务主键
FROM JKCWFDB_WORK_NJS.dbo.YW_SGZCBDW_RYXX A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
);
end 

/**delete end **/
-----------------------------------------------------------------------------------------------------------------------------------------------------
--导入施工许可证业务主表

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
	BA.YWBM FId    --业务编码
	,B.JSDW FBaseinfoId    --企业id
	,B.GuidId FPrjId    --项目编码
	,CASE WHEN ISNULL(YW.CreateTime,'')<>'' THEN CONVERT(CHAR(4),YEAR(YW.CreateTime))+'年 ' ELSE '' END +'施工许可证管理初次办理' FName    --业务名称
	,11223 FManageTypeId    --业务编码
	,YW.CreateTime FwriteDate    --写入时间
	,BA.SBSJ FReportDate    --上报时间
	,NULL FIsSign    --是否签字
	,0 FState    --业务状态
	,NULL FResult    --审批结论
	,YEAR(YW.CreateTime) FYear    --年度
	,MONTH(YW.CreateTime) FMonth    --月份
	,FQ.FQBM FLinkId    --外键工程编码
	,JS.FCompany FBaseName    --建设单位名称
	,isnull(b.SDC,'51') FUpDeptId    --上报部门地区编码
	,NULL FRemark    --暂不考虑
	,NULL FIsCheck    --暂不考虑
	,NULL FCount    --暂不考虑
	,YW.CreateTime FTime    --最后更新时间,业务暂时没有上报，没走流程
	,NULL FIsDeleted    --是否删除
	,YW.CreateTime FCreateTime    --创建时间
	,1 FReportCount    --暂不考虑
	,NULL FToBaseinfoId    --暂不考虑
	,NULL FAppDate    --暂不考虑
	,NULL FLinkAppId    --暂不考虑
	,NULL FBarCode    --暂不考虑
	,NULL FCreateUser    --暂不考虑
	,NULL FgfTime    --暂不考虑
	FROM JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS BA
	LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
	LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
	LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
	LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_YWInfo YW ON YW.YWBM=B.YWBM 
	WHERE ISNULL(JS.FID,'')<>'' AND ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
	  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
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
					   when 277 then '11225' when 278 then '11225' when 279 then '11225' end as FManageTypeId,  --类型
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
--导入施工许可证相关项目表
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
,CASE WHEN ISNULL(BA.GCLB,'0')='01' OR ISNULL(BA.GCLB,'0')='02' OR ISNULL(BA.GCLB,'0')='03' OR
ISNULL(BA.GCLB,'0')='04' OR ISNULL(BA.GCLB,'0')='05' OR ISNULL(BA.GCLB,'0')='06' OR
ISNULL(BA.GCLB,'0')='07' OR ISNULL(BA.GCLB,'0')='08' OR ISNULL(BA.GCLB,'0')='09' OR ISNULL(BA.GCLB,'0')='10' THEN '2000101'
WHEN ISNULL(BA.GCLB,'0')='11' OR ISNULL(BA.GCLB,'0')='12' OR ISNULL(BA.GCLB,'0')='13' OR
ISNULL(BA.GCLB,'0')='14' OR ISNULL(BA.GCLB,'0')='15' OR ISNULL(BA.GCLB,'0')='16' OR
ISNULL(BA.GCLB,'0')='17' OR ISNULL(BA.GCLB,'0')='18' THEN '2000102'
WHEN ISNULL(BA.GCLB,'0')='19' THEN '2000103'
ELSE NULL END ProjectType         --项目类别:2000101--房屋建筑;2000102--市政基础;2000103--其他
,A.XMDZ Address         --项目地址
,NULL ProjectNumber         --立项文号
,NULL ProjectLevel         --立项级别
,NULL ProjectTime         --项目时间
,A.XMBH ProjectNo         --项目编号
,NULL IsForeign         --是否涉外
,NULL JSYDXKZ         --建设用地许可证号
,NULL JSGCXKZ         --工程规划许可证号
,NULL Area         --建设总面积（m2）
,NULL Investment         --投资规模(万元)
,NULL ConstrType         --建设性质：2000501--新建；2000502--改建
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
,CASE WHEN ISNULL(BA.GCLB,'0')='01' OR ISNULL(BA.GCLB,'0')='02' OR ISNULL(BA.GCLB,'0')='03' OR
ISNULL(BA.GCLB,'0')='04' OR ISNULL(BA.GCLB,'0')='05' OR ISNULL(BA.GCLB,'0')='06' OR
ISNULL(BA.GCLB,'0')='07' OR ISNULL(BA.GCLB,'0')='08' OR ISNULL(BA.GCLB,'0')='09' OR ISNULL(BA.GCLB,'0')='10' THEN '200010113'
WHEN ISNULL(BA.GCLB,'0')='11' OR ISNULL(BA.GCLB,'0')='12' OR ISNULL(BA.GCLB,'0')='13' THEN '200010209'
WHEN ISNULL(BA.GCLB,'0')='14' OR ISNULL(BA.GCLB,'0')='15' THEN '200010210' 
WHEN ISNULL(BA.GCLB,'0')='16' THEN '200010204'
WHEN ISNULL(BA.GCLB,'0')='17' THEN '200010201'
WHEN ISNULL(BA.GCLB,'0')='18' THEN '200010206'
WHEN ISNULL(BA.GCLB,'0')='19' THEN '2000103'
ELSE NULL END ProjectUse         --工程用途：200010201--给水；200010205--道路
,BA.SJKGRQ StartDate         --实际开工日期
,BA.SJJGRQ EndDate         --实际竣工日期
,NULL RegisterTime         --记录登记时间
,NULL  ConstrBasis         --建设依据
,''  ConstrContent         --建设内容
,A.JSDW FJSDWID --建设单位外键
,isnull(A.XMSZD,'51') AddressDept    --地址外键
,CONVERT(VARCHAR(20),BA.JSGM) + BA.JSGM_DW + ', ' + BA.JSGMQT ConstrScale--建设规模
,NULL LandType  --用地性质
--新建	2000501
--改建	2000502
--扩建	2000503
--重建	2000504
--迁建	2000505
--恢复	2000506
--其他	2000507
,A.JSDWFR JSDWFR         --建设单位法人
FROM JKCWFDB_WORK_NJS.DBO.YW_XMInfo AS A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=A.JSDW
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS BA ON BA.YWBM=A.YWBM
WHERE ISNULL(JS.FID,'')<>'' AND ISNULL(A.XMBM,'')<>'' --保证项目编码有值 
  AND A.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

-----------------------------------------------------------------------------------------------------------------------------------------------------
--导入施工许可证相关的单体工程表
INSERT INTO dbCenter.dbo.TC_PrjItem_Info
SELECT 
FQ.GuidId FId         --单体工程id
,B.GuidId FPrjId         --关联TC_Prj_Info.FId
,B.XMMC ProjectName         --项目名称
,B.XMDZ Address         --项目地址
,NULL Area         --建设总面积
,NULL Investment         --投资规模(万元)
,JS.FCompany JSDW         --建设单位名称
,JS.FRXM LegalPerson         --法人代表
,JS.ZZJGDM JSDWDM         --建设单位组织机构代码
,FQ.Name PrjItemName         --工程名称
,CASE WHEN ISNULL(BA.GCLB,'0')='01' OR ISNULL(BA.GCLB,'0')='02' OR ISNULL(BA.GCLB,'0')='03' OR
ISNULL(BA.GCLB,'0')='04' OR ISNULL(BA.GCLB,'0')='05' OR ISNULL(BA.GCLB,'0')='06' OR
ISNULL(BA.GCLB,'0')='07' OR ISNULL(BA.GCLB,'0')='08' OR ISNULL(BA.GCLB,'0')='09' OR ISNULL(BA.GCLB,'0')='10' THEN '2000101'
WHEN ISNULL(BA.GCLB,'0')='11' OR ISNULL(BA.GCLB,'0')='12' OR ISNULL(BA.GCLB,'0')='13' OR
ISNULL(BA.GCLB,'0')='14' OR ISNULL(BA.GCLB,'0')='15' OR ISNULL(BA.GCLB,'0')='16' OR
ISNULL(BA.GCLB,'0')='17' OR ISNULL(BA.GCLB,'0')='18' THEN '2000102'
WHEN ISNULL(BA.GCLB,'0')='19' THEN '2000103'
ELSE NULL END PrjItemType --工程类别          --工程类别：2000101--房屋建筑;2000102--市政基础;2000103--其他
--砖木结构	509001
--砖混结构	509002
--钢筋混凝土结构	509003
--钢结构	509004
--框架结构	509005
--剪力墙结构	509006
--框架-剪力墙结构	509007
--框-筒结构	509008
--其他	509010
,NULL ConstrType         --结构类型：509001；509002；509005
,BA.HTJG Cost         --工程造价（万元）
,NULL Scale         --工程规模
,NULL PrjItemDesc         --工程描述
,JS.FID FJSDWID         --建设单位外键
,isnull(B.XMSZD,'51') AddressDept         --地址外键
,CONVERT(VARCHAR(20),BA.JSGM) + BA.JSGM_DW + ', ' + BA.JSGMQT ConstrScale--建设规模
FROM JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
WHERE ISNULL(JS.FID,'')<>'' AND ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


-----------------------------------------------------------------------------------------------------------------------------------------------------
--保证金1
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
A.ID FId         --主键
,A.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,B.GuidId FPrjId --项目外键
,'特大城市市政基础设施配套费（包含原收取的市政建设配套费、城市燃气配套费、自来水配套费）' JFXM     --缴费项目
,'1' JFXMBM     --缴费项目编码
,A.JE1 Money     --金额（万元）
,A.JFSJ1 JFSJ     --缴费时间
,A.SKJBR1 SKJBR     --收款经办人
,A.SKDW1 SKDW     --收款单位签章
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

--保证金2
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
newid() FId         --主键
,A.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,B.GuidId FPrjId --项目外键
,'工程定额测定费' JFXM     --缴费项目
,'2' JFXMBM     --缴费项目编码
,A.JE2 Money     --金额（万元）
,A.JFSJ2 JFSJ     --缴费时间
,A.SKJBR2 SKJBR     --收款经办人
,A.SKDW2 SKDW     --收款单位签章
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

--保证金3
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
newid() FId         --主键
,A.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,B.GuidId FPrjId --项目外键
,'文物勘探发掘费' JFXM     --缴费项目
,'3' JFXMBM     --缴费项目编码
,A.JE3 Money     --金额（万元）
,A.JFSJ3 JFSJ     --缴费时间
,A.SKJBR3 SKJBR     --收款经办人
,A.SKDW3 SKDW     --收款单位签章
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--保证金4
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
newid() FId         --主键
,A.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,B.GuidId FPrjId --项目外键
,'建筑工程质量监督费' JFXM     --缴费项目
,'4' JFXMBM     --缴费项目编码
,A.JE4 Money     --金额（万元）
,A.JFSJ4 JFSJ     --缴费时间
,A.SKJBR4 SKJBR     --收款经办人
,A.SKDW4 SKDW     --收款单位签章
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--保证金5
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
newid() FId         --主键
,A.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,B.GuidId FPrjId --项目外键
,'新建项目白蚁防治费' JFXM     --缴费项目
,'5' JFXMBM     --缴费项目编码
,A.JE5 Money     --金额（万元）
,A.JFSJ5 JFSJ     --缴费时间
,A.SKJBR5 SKJBR     --收款经办人
,A.SKDW5 SKDW     --收款单位签章
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--保证金6
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
newid() FId         --主键
,A.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,B.GuidId FPrjId --项目外键
,'新型墙体材料专项基金' JFXM     --缴费项目
,'6' JFXMBM     --缴费项目编码
,A.JE6 Money     --金额（万元）
,A.JFSJ6 JFSJ     --缴费时间
,A.SKJBR6 SKJBR     --收款经办人
,A.SKDW6 SKDW     --收款单位签章
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--保证金7
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
newid() FId         --主键
,A.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,B.GuidId FPrjId --项目外键
,'散装水泥费' JFXM     --缴费项目
,'7' JFXMBM     --缴费项目编码
,A.JE7 Money     --金额（万元）
,A.JFSJ7 JFSJ     --缴费时间
,A.SKJBR7 SKJBR     --收款经办人
,A.SKDW7 SKDW     --收款单位签章
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--保证金8
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
newid() FId         --主键
,A.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,B.GuidId FPrjId --项目外键
,'异地绿化费' JFXM     --缴费项目
,'8' JFXMBM     --缴费项目编码
,A.JE8 Money     --金额（万元）
,A.JFSJ8 JFSJ     --缴费时间
,A.SKJBR8 SKJBR     --收款经办人
,A.SKDW8 SKDW     --收款单位签章
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--保证金9
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
newid() FId         --主键
,A.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,B.GuidId FPrjId --项目外键
,'防空地下室异地建设费' JFXM     --缴费项目
,'9' JFXMBM     --缴费项目编码
,A.JE9 Money     --金额（万元）
,A.JFSJ9 JFSJ     --缴费时间
,A.SKJBR9 SKJBR     --收款经办人
,A.SKDW9 SKDW     --收款单位签章
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--保证金10
INSERT INTO dbCenter.dbo.TC_SGXKZ_BZJQR
SELECT 
newid() FId         --主键
,A.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,B.GuidId FPrjId --项目外键
,'农民工工资支付保证金担保情况' JFXM     --缴费项目
,'10' JFXMBM     --缴费项目编码
,A.JE10 Money     --金额（万元）
,A.JFSJ10 JFSJ     --缴费时间
,A.SKJBR10 SKJBR     --收款经办人
,A.SKDW10 SKDW     --收款单位签章
FROM JKCWFDB_WORK_NJS.dbo.YW_BZJQRB A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

-----------------------------------------------------------------------------------------------------------------------------------------------------
--施工许可证相关施工图审查信息
INSERT INTO dbCenter.dbo.TC_SGXKZ_SGTSC
SELECT 
A.ID FId         --主键
,A.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,A.SCHGSBH SGTSCHGSBH     --施工图审查合格书编号
,B.XMBH ProjectNo --项目编号
,A.STJG SGTSCJGMC --施工图审查机构名称
,NULL SGTSCZZJGDM     --施工图审查机构组织机构代码
,A.FZRQ SCWCRQ     --审查完成日期
,CONVERT(VARCHAR(20),BA.JSGM) + BA.JSGM_DW + ', ' + BA.JSGMQT ConstrScale--建设规模
,NULL KCDWMC --勘察单位名称
,NULL KCDWZZJGDM     --勘察单位组织机构代码
,NULL SJDWMC --设计单位名称
,NULL SJDWZZJGDM --设计单位组织机构代码
,NULL YCSCSFTG --一次审查是否通过
,NULL YCSCWFTS --一次审查时违反强条数
,NULL YCSCWFTM --一次审查时违反的强条条目
,NULL BL
,NULL YL
,NULL SGTSCJGId
,NULL KCDWId
,NULL SJDWId
FROM JKCWFDB_WORK_NJS.dbo.YW_SGTZJJSZL A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

-----------------------------------------------------------------------------------------------------------------------------------------------------
--招投标相关
--合同办理表
INSERT INTO dbCenter.dbo.TC_SGXKZ_ZBJGBL
SELECT 
B.GuidID FId         --主键
,BA.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,NULL BL     
,NULL YL
FROM JKCWFDB_WORK_NJS.dbo.YW_ZBJG_KCDW KC
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=KC.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--招投标信息表
INSERT INTO dbCenter.dbo.TC_SGXKZ_ZBJG
SELECT 
B.GuidID FId         --主键
,BA.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,B.XMMC ProjectName     --项目名称
,FQ.Name PrjItemName --工程名称
,B.XMJSDW JSDW     --建设单位
,KC.ID KCId --勘察

,CASE WHEN ISNULL(KC.ZBFS,'0')='公开招标' THEN '11220601'
      WHEN ISNULL(KC.ZBFS,'0')='邀请招标' THEN '11220602'
 ELSE NULL END KCZBFS --招标方式

,CASE WHEN ISNULL(KC.ZZFS,'0')='委拖招标' THEN '11221001'
      WHEN ISNULL(KC.ZZFS,'0')='自行招标' THEN '11221002'
 ELSE NULL END KCZZFS --组织方式

,KC.ZBDW KCZBDW --中标单位
,KC.ZBQYZZZSH KCZBQYZZZSH --中标企业资质证书号
,KC.ZBJ KCZBJ --中标价
,KC.ZBGQ KCZBGQ --中标工期
,KC.HTKGRQ KCHTKGTime --合同开工日期
,KC.HTJGRQ KCHTJGTime --合同竣工日期
,KC.HTQDRQ KCHTQDTime --合同签订日期
,KC.HTBASJ KCHTBATime --合同备案日期
,KC.HTBABH KCHTBABH --合同备案编号
,KC.XMJL KCXMJL --项目经理
,SJ.ID SJId --设计

,CASE WHEN ISNULL(SJ.ZBFS,'0')='公开招标' THEN '11220601'
      WHEN ISNULL(SJ.ZBFS,'0')='邀请招标' THEN '11220602'
 ELSE NULL END SJZBFS --招标方式

,CASE WHEN ISNULL(SJ.ZZFS,'0')='委拖招标' THEN '11221001'
      WHEN ISNULL(SJ.ZZFS,'0')='自行招标' THEN '11221002'
 ELSE NULL END SJZZFS --组织方式

,SJ.ZBDW SJZBDW --中标单位
,SJ.ZBQYZZZSH SJZBQYZZZSH --中标企业资质证书号
,NULL SJZLBZ --质量标准
,SJ.ZBJ SJZBJ --中标价
,SJ.ZBGQ SJZBGQ --中标工期
,SJ.HTKGRQ SJHTKGTime --合同开工日期
,SJ.HTJGRQ SJHTJGTime --合同竣工日期
,SJ.HTQDRQ SJHTQDTime --合同签订日期
,SJ.HTBASJ SJHTBATime --合同备案日期
,SJ.HTBABH SJHTBABH --合同备案编号
,NULL SJHTBAJG --合同备案机关
,JL.ID JLId --监理

,CASE WHEN ISNULL(JL.ZBFS,'0')='公开招标' THEN '11220601'
      WHEN ISNULL(JL.ZBFS,'0')='邀请招标' THEN '11220602'
 ELSE NULL END JLZBFS --招标方式

,CASE WHEN ISNULL(JL.ZZFS,'0')='委拖招标' THEN '11221001'
      WHEN ISNULL(JL.ZZFS,'0')='自行招标' THEN '11221002'
 ELSE NULL END JLZZFS --组织方式

,JL.ZBDW JLZBDW --中标单位
,JL.ZBQYZZZSH JLZBQYZZZSH --中标企业资质证书号
,NULL JLZBQYZZDJ --中标企业资质等级
,NULL JLZLBZ --质量标准
,JL.ZBJ JLZBJ --中标价
,NULL JLZBJDX --大写
,JL.ZBGQ JLZBGQ --中标工期
,JL.JHKGRQ JLHTKGTime --合同开工日期
,JL.JHJGRQ JLHTJGTime --合同竣工日期
,JL.HTQDRQ JLHTQDTime --合同签订日期
,JL.HTBASJ JLHTBATime --合同备案日期
,JL.HTBABH JLHTBABH --合同备案编号
,NULL JLHTBAJG --合同备案机关
,NULL JLGCS --监理工程师

,SG.ID SGId --施工

,CASE WHEN ISNULL(SG.ZBFS,'0')='公开招标' THEN '11220601'
      WHEN ISNULL(SG.ZBFS,'0')='邀请招标' THEN '11220602'
 ELSE NULL END SGZBFS --招标方式

,CASE WHEN ISNULL(SG.ZZFS,'0')='委拖招标' THEN '11221001'
      WHEN ISNULL(SG.ZZFS,'0')='自行招标' THEN '11221002'
 ELSE NULL END SGZZFS --组织方式

,SG.ZBDW SGZBDW --中标单位
,SG.ZBQYZZZSH SGZBQYZZZSH --中标企业资质证书号
,NULL SGZBQYZZDJ --中标企业资质等级
,NULL SGZLBZ --质量标准
,NULL SGZLDJ --质量等级
,SG.ZBJ SGZBJ --中标价
,NULL SGZBJDX --大写
,SG.ZBGQ SGZBGQ --中标工期
,SG.HTKGRQ SGHTKGTime --合同开工日期
,SG.HTJGRQ SGHTJGTime --合同竣工日期
,NULL SGZBFW --招标范围
,NULL SGJZBM --建筑面积
,NULL SGSZSNYL --散装水泥用量
,SG.XMJLXM SGXMJL --项目经理姓名
,NULL SGXMJLZS --项目经理证书号码

,SG.HTQDSJ SGHTQDTime --合同签订日期
,SG.BASJ SGHTBATime --合同备案日期
,SG.HTBABH SGHTBABH --合同备案编号
,NULL SGHTBAJG --合同备案机关
,B.XMBH ProjectNo --项目编号
,NULL ZBTZSBH --中标通知书编号
,CONVERT(VARCHAR(20),BA.JSGM) + BA.JSGM_DW + ', ' + BA.JSGMQT ConstrScale--建设规模
,NULL Area     --总面积
,NULL ZBDLDWMC     --招标代理单位名称
,NULL ZBDLDWZZJGDM --招标代理单位组织机构代码
,NULL JLZBLX --招标类型
,NULL JLZBDWZZJGDM --中标单位组织机构代码
,NULL JLZBRQ --中标日期
,NULL JLGCSZJLX --总监理工程师证件类型
,NULL JLGCSZJHM --总监理工程师证件号码
,NULL SGZBLX --招标类型
,NULL SGZBDWZZJGDM --中标单位组织机构代码
,NULL SGZBRQ --中标日期
,NULL SGXMJLZJLX --项目经理证件类型
,NULL SGXMJLZJHM --项目经理证件号码
,NULL BL
,NULL YL
FROM JKCWFDB_WORK_NJS.dbo.YW_ZBJG_KCDW KC
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_ZBJG_SJDW SJ ON SJ.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_ZBJG_JLDW JL ON JL.YWBM=KC.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_ZBJG_SGDW SG ON SG.YWBM=KC.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));
-----------------------------------------------------------------------------------------------------------------------------------------------------

--建设工程规划许可证
INSERT INTO dbCenter.dbo.TC_SGXKZ_JSGCGHXKZ
SELECT 
A.ID FId         --主键
,A.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,B.XMMC ProjectName     --项目名称
,B.XMJSDW JSDW     --建设单位
,BA.JSDD Address --建设地址
,NULL Area     --面积
,CONVERT(VARCHAR(20),BA.JSGM) + BA.JSGM_DW + ', ' + BA.JSGMQT ConstrScale--建设规模
,BA.KDGD Span --跨度(高度)
,NULL Others     --其他
,A.FZRQ CreateTime --发证日期
,A.FZJG HFJG --核发机关
,A.XKZSBH GCGHXKZBH --工程规划许可证书编号
,NULL BL
,NULL YL
FROM JKCWFDB_WORK_NJS.dbo.YW_GCGHXKZ A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

-----------------------------------------------------------------------------------------------------------------------------------------------------

--建设用地规划许可证
INSERT INTO dbCenter.dbo.TC_SGXKZ_JSYDGHXKZ
SELECT 
A.ID FId         --主键
,A.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,B.XMMC ProjectName     --项目名称
,B.XMJSDW JSDW     --建设单位
,BA.JSDD Address --建设地址
,A.YDMJ Area     --用地面积
,CONVERT(VARCHAR(20),BA.JSGM) + BA.JSGM_DW + ', ' + BA.JSGMQT ConstrScale--建设规模
,NULL Others     --其他
,A.YDXZ YDXZ --用地性质
,A.FZRQ CreateTime --发证日期
,A.FZDW HFJG --核发机关
,A.YDXKZBH YDGHXKZBH --用地规划许可证编号
,NULL BL
,NULL YL
FROM JKCWFDB_WORK_NJS.dbo.YW_YDGHXKZ A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo BA ON BA.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));
-----------------------------------------------------------------------------------------------------------------------------------------------------
--选址意见书
INSERT INTO dbCenter.dbo.TC_SGXKZ_Location
SELECT 
A.ID FId         --主键
,A.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,B.XMMC ProjectName     --项目名称
,B.XMJSDW JSDW     --建设单位
,A.XMNXWZ LocationAddress     --项目拟选位置
,A.NYDMJ Area     --拟用地面积
,A.NJSGM Scale     --拟建设规模
,A.JSXMYJ ProjectBasis     --建设项目依据
,A.FZRQ CreateTime --发证日期
,A.HFJG HFJG --核发机关
,A.ZSBH XZYJSZSBH --选址意见书证书编号
,NULL BL
,NULL YL
,NULL YDPZSX
FROM JKCWFDB_WORK_NJS.dbo.YW_XZYJS A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

-----------------------------------------------------------------------------------------------------------------------------------------------------
--施工许可证初次办理信息表
INSERT INTO dbCenter.dbo.TC_SGXKZ_PrjInfo
SELECT
BA.WYBM FId  --主键
,BA.YWBM FAppId  --业务外键
,FQ.GuidId FPrjItemId --工程外键
,JS.FCompany JSDW  --建设单位
,NULL JProvince --省
,NULL JCity --市
,NULL JCounty --县
,isnull(BA.JSDWSDX,'51') JSDWAddressDept --建设单位属地
,BA.JSDWDZ JSDWDZ --建设单位地址
,CASE WHEN ISNULL(BA.SYZXZ,'0')='有限责任' THEN '11221201'
      WHEN ISNULL(BA.SYZXZ,'0')='集体' THEN '11221202'
      WHEN ISNULL(BA.SYZXZ,'0')='股份合作' THEN '11221203'
      WHEN ISNULL(BA.SYZXZ,'0')='国有' THEN '11221204'
      WHEN ISNULL(BA.SYZXZ,'0')='股份有限' THEN '11221205'
      WHEN ISNULL(BA.SYZXZ,'0')='私有' THEN '11221206'
      WHEN ISNULL(BA.SYZXZ,'0')='其他内资' THEN '11221207'
      WHEN ISNULL(BA.SYZXZ,'0')='内地和港澳台合资' THEN '11221208'
      WHEN ISNULL(BA.SYZXZ,'0')='联营' THEN '11221209'
      WHEN ISNULL(BA.SYZXZ,'0')='港、澳、台独资' THEN '112212010'
      WHEN ISNULL(BA.SYZXZ,'0')='港澳台投资股份有限（公司）' THEN '112212011'
      WHEN ISNULL(BA.SYZXZ,'0')='其他港澳台投资' THEN '112212012'
      WHEN ISNULL(BA.SYZXZ,'0')='中外合资' THEN '112212013'
      WHEN ISNULL(BA.SYZXZ,'0')='国外投资股份有限（公司）' THEN '112212015'
      WHEN ISNULL(BA.SYZXZ,'0')='其他国外投资' THEN '112212016'
      WHEN ISNULL(BA.SYZXZ,'0')='其他' THEN '112212099'
ELSE NULL END JSDWXZ  --所有制性质
,BA.SQDWFDDBR FDDBR --法定代表人
,JS.FRSJ FRDH --法人电话
,BA.LZR LZR  --领证人
,BA.YDDH LXDH --联系电话
,BA.JSDWJSFZR JSFZR --建设单位技术负责人
,BA.JSDWJSFZRZC JSFZRZC --建设单位技术负责人职称
,BA.JSDWJSFZRDH JSFZRDH --建设单位技术负责人电话
,B.XMBM PrjId --项目外键
,FQ.FQBM PrjItemId --工程外键
,B.XMMC ProjectName --项目名称
,FQ.Name PrjItemName --工程名称
,CASE WHEN ISNULL(BA.GCLB,'0')='01' OR ISNULL(BA.GCLB,'0')='02' OR ISNULL(BA.GCLB,'0')='03' OR
ISNULL(BA.GCLB,'0')='04' OR ISNULL(BA.GCLB,'0')='05' OR ISNULL(BA.GCLB,'0')='06' OR
ISNULL(BA.GCLB,'0')='07' OR ISNULL(BA.GCLB,'0')='08' OR ISNULL(BA.GCLB,'0')='09' OR ISNULL(BA.GCLB,'0')='10' THEN '2000101'
WHEN ISNULL(BA.GCLB,'0')='11' OR ISNULL(BA.GCLB,'0')='12' OR ISNULL(BA.GCLB,'0')='13' OR
ISNULL(BA.GCLB,'0')='14' OR ISNULL(BA.GCLB,'0')='15' OR ISNULL(BA.GCLB,'0')='16' OR
ISNULL(BA.GCLB,'0')='17' OR ISNULL(BA.GCLB,'0')='18' THEN '2000102'
WHEN ISNULL(BA.GCLB,'0')='19' THEN '2000103'
ELSE NULL END PrjItemType --工程类别 
,BA.BJSJ ProjectTime --报建时间
,BA.SBSJ ReportTime --申报时间
,NULL PProvince --省
,NULL PCity --市
,NULL PCounty --县
,isnull(B.SDC,'51') PrjAddressDept --工程属地
,BA.JSDD Address --建设地址
,CONVERT(VARCHAR(20),BA.JSGM) + BA.JSGM_DW + ', ' + BA.JSGMQT ConstrScale--建设规模
,NULL ConstrType --结构类型
,BA.HTJG Price --合同价格
,11221128 Currency --币种
,BA.HTKGRQ StartDate --合同开工日期
,BA.HTJGRQ EndDate --合同竣工工日期
,NULL FResult --审核结果
,NULL Remark --备注
,NULL ProjectFile --立项文件
,B.XMBH ProjectNo --项目编码
,NULL ProjectLevel --立项级别
,NULL Cost --总投资（万元）
,NULL Area --总面积（m2）
,NULL BuildType --建筑性质
,CASE WHEN ISNULL(BA.GCLB,'0')='01' OR ISNULL(BA.GCLB,'0')='02' OR ISNULL(BA.GCLB,'0')='03' OR
ISNULL(BA.GCLB,'0')='04' OR ISNULL(BA.GCLB,'0')='05' OR ISNULL(BA.GCLB,'0')='06' OR
ISNULL(BA.GCLB,'0')='07' OR ISNULL(BA.GCLB,'0')='08' OR ISNULL(BA.GCLB,'0')='09' OR ISNULL(BA.GCLB,'0')='10' THEN '200010113'
WHEN ISNULL(BA.GCLB,'0')='11' OR ISNULL(BA.GCLB,'0')='12' OR ISNULL(BA.GCLB,'0')='13' THEN '200010209'
WHEN ISNULL(BA.GCLB,'0')='14' OR ISNULL(BA.GCLB,'0')='15' THEN '200010210' 
WHEN ISNULL(BA.GCLB,'0')='16' THEN '200010204'
WHEN ISNULL(BA.GCLB,'0')='17' THEN '200010201'
WHEN ISNULL(BA.GCLB,'0')='18' THEN '200010206'
WHEN ISNULL(BA.GCLB,'0')='19' THEN '2000103'
ELSE NULL END ProjectUse --工程用途 
,NULL ProjectNumber --立项文号
,null SGXKZBH --施工许可证编号
,NULL FZJG  --
,NULl FZTime
,null DZZT
FROM JKCWFDB_WORK_NJS.DBO.YW_SGXKZInfo AS BA
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQINFO AS FQ ON FQ.YWBM=BA.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMINFO AS B ON BA.YWBM=B.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.xm_JSDW_USER AS JS ON JS.FID=B.JSDW 
WHERE ISNULL(JS.FID,'')<>'' AND ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND BA.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

-----------------------------------------------------------------------------------------------------------------------------------------------------
--附件材料
--施工附件材料
INSERT INTO dbCenter.dbo.TC_SGXKZ_File
SELECT 
A.ID FId         --主键
,A.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,A.CLBH FLinkId     --外键
,A.CLMC FileName --材料名称
,A.SCRQ ReportTime --上传时间
,A.FilePath FilePath     --地址
,NULL FileType     --材料类型
,NULL Size --大小
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--监理附件材料
INSERT INTO dbCenter.dbo.TC_SGXKZ_File
SELECT 
A.ID FId         --主键
,A.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,A.CLBH FLinkId     --外键
,A.CLMC FileName --材料名称
,A.SCRQ ReportTime --上传时间
,A.FilePath FilePath     --地址
,NULL FileType     --材料类型
,NULL Size --大小
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL_JL A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--施工附件材料
INSERT INTO dbCenter.dbo.TC_SGXKZ_File
SELECT 
A.ID FId         --主键
,A.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,A.CLBH FLinkId     --外键
,A.CLMC FileName --材料名称
,A.SCRQ ReportTime --上传时间
,A.FilePath FilePath     --地址
,NULL FileType     --材料类型
,NULL Size --大小
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL_SG A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

--设计附件材料
INSERT INTO dbCenter.dbo.TC_SGXKZ_File
SELECT 
A.ID FId         --主键
,A.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,A.CLBH FLinkId     --外键
,A.CLMC FileName --材料名称
,A.SCRQ ReportTime --上传时间
,A.FilePath FilePath     --地址
,NULL FileType     --材料类型
,NULL Size --大小
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL_SJ A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--勘察附件材料
INSERT INTO dbCenter.dbo.TC_SGXKZ_File
SELECT 
A.ID FId         --主键
,A.YWBM FAppId  --业务主键
,FQ.GuidId FPrjItemId --单项工程外键
,A.CLBH FLinkId     --外键
,A.CLMC FileName --材料名称
,A.SCRQ ReportTime --上传时间
,A.FilePath FilePath     --地址
,NULL FileType     --材料类型
,NULL Size --大小
FROM JKCWFDB_WORK_NJS.dbo.YW_WJ_ZM_CL_KC A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

-----------------------------------------------------------------------------------------------------------------------------------------------------

--总包单位
INSERT INTO dbCenter.dbo.TC_PrjItem_Ent
SELECT 
A.ID FId         --主键
,B.GuidId FPrjId --项目外键
,FQ.GuidId FPrjItemId --单项工程外键
,NULL FProcId
,A.YWBM FAppId  --业务主键
,A.QYID QYId     --企业id
,A.DWMC FName     --单位名称
,2 FEntType   --2.施工总包；3.专业承包；4.劳务分包；5.勘察；6.设计；7.监理
,A.ZZJGDM FOrgCode     --组织机构代码
,A.DWDZ FAddress     --单位地址
,NULL ZZDJ --资质等级
,NULL ZZZSH
,NULL YYZZH
,A.FDDBR FLegalPerson     --法定代表人
,A.LXDH FTel     --联系电话
,A.LXR FLinkMan     --联系人
,A.YDDH FMobile     --移动电话
,A.ZXZZ mZXZZ     --主项资质
,A.ZXZZ_Z oZXZZ     --增项资质
,NULL FCreateTime
,NULL FTime
,ISNULL(A.BZ,'')  Remark     --备注
FROM JKCWFDB_WORK_NJS.dbo.YW_SGZCBDWInfo A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));



--专包单位
INSERT INTO dbCenter.dbo.TC_PrjItem_Ent
SELECT 
A.ID FId         --主键
,B.GuidId FPrjId --项目外键
,FQ.GuidId FPrjItemId --单项工程外键
,NULL FProcId
,A.YWBM FAppId  --业务主键
,A.QYID QYId     --企业id
,A.DWMC FName     --单位名称
,3 FEntType   --2.施工总包；3.专业承包；4.劳务分包；5.勘察；6.设计；7.监理
,A.ZZJGDM FOrgCode     --组织机构代码
,A.DWDZ FAddress     --单位地址
,NULL ZZDJ --资质等级
,NULL ZZZSH
,NULL YYZZH
,A.FDDBR FLegalPerson     --法定代表人
,A.LXDH FTel     --联系电话
,A.LXR FLinkMan     --联系人
,A.YDDH FMobile     --移动电话
,A.ZXZZ mZXZZ     --主项资质
,A.ZXZZ_Z oZXZZ     --增项资质     
,NULL FCreateTime                  
,NULL FTime                        
,ISNULL(A.BZ,'')  Remark     --备注
FROM JKCWFDB_WORK_NJS.dbo.YW_ZYCBDW A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

--劳务
INSERT INTO dbCenter.dbo.TC_PrjItem_Ent
SELECT 
A.ID FId         --主键
,B.GuidId FPrjId --项目外键
,FQ.GuidId FPrjItemId --单项工程外键
,NULL FProcId
,A.YWBM FAppId  --业务主键
,A.QYID QYId     --企业id
,A.DWMC FName     --单位名称
,4 FEntType   --2.施工总包；3.专业承包；4.劳务分包；5.勘察；6.设计；7.监理
,A.ZZJGDM FOrgCode     --组织机构代码
,A.DWDZ FAddress     --单位地址
,NULL ZZDJ --资质等级
,NULL ZZZSH
,NULL YYZZH
,A.FDDBR FLegalPerson     --法定代表人
,A.LXDH FTel     --联系电话
,A.LXR FLinkMan     --联系人
,A.YDDH FMobile     --移动电话
,A.ZXZZ mZXZZ     --主项资质
,NULL oZXZZ     --增项资质     
,NULL FCreateTime                  
,NULL FTime                        
,ISNULL(A.BZ,'')  Remark     --备注
FROM JKCWFDB_WORK_NJS.dbo.YW_LWFBDW A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));



--勘察
INSERT INTO dbCenter.dbo.TC_PrjItem_Ent
SELECT 
A.ID FId         --主键
,B.GuidId FPrjId --项目外键
,FQ.GuidId FPrjItemId --单项工程外键
,NULL FProcId
,A.YWBM FAppId  --业务主键
,A.QYID QYId     --企业id
,A.DWMC FName     --单位名称
,5 FEntType   --2.施工总包；3.专业承包；4.劳务分包；5.勘察；6.设计；7.监理
,A.ZZJGDM FOrgCode     --组织机构代码
,A.DWDZ FAddress     --单位地址
,NULL ZZDJ --资质等级
,NULL ZZZSH
,NULL YYZZH
,A.FDDBR FLegalPerson     --法定代表人
,A.LXDH FTel     --联系电话
,A.LXR FLinkMan     --联系人
,A.YDDH FMobile     --移动电话
,A.ZXZZ mZXZZ     --主项资质
,NULL oZXZZ     --增项资质     
,NULL FCreateTime                  
,NULL FTime                        
,ISNULL(A.BZ,'')  Remark     --备注
FROM JKCWFDB_WORK_NJS.dbo.YW_KCDW A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--设计
INSERT INTO dbCenter.dbo.TC_PrjItem_Ent
SELECT 
A.ID FId         --主键
,B.GuidId FPrjId --项目外键
,FQ.GuidId FPrjItemId --单项工程外键
,NULL FProcId
,A.YWBM FAppId  --业务主键
,A.QYID QYId     --企业id
,A.DWMC FName     --单位名称
,6 FEntType   --2.施工总包；3.专业承包；4.劳务分包；5.勘察；6.设计；7.监理
,A.ZZJGDM FOrgCode     --组织机构代码
,A.DWDZ FAddress     --单位地址
,NULL ZZDJ --资质等级
,NULL ZZZSH
,NULL YYZZH
,A.FDDBR FLegalPerson     --法定代表人
,A.LXDH FTel     --联系电话
,A.LXR FLinkMan     --联系人
,A.YDDH FMobile     --移动电话
,A.ZXZZ mZXZZ     --主项资质
,NULL oZXZZ     --增项资质     
,NULL FCreateTime                  
,NULL FTime                        
,ISNULL(A.BZ,'')  Remark     --备注
FROM JKCWFDB_WORK_NJS.dbo.YW_SJDW A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));



--监理
INSERT INTO dbCenter.dbo.TC_PrjItem_Ent
SELECT 
A.ID FId         --主键
,B.GuidId FPrjId --项目外键
,FQ.GuidId FPrjItemId --单项工程外键
,NULL FProcId
,A.YWBM FAppId  --业务主键
,A.QYID QYId     --企业id
,A.DWMC FName     --单位名称
,7 FEntType   --2.施工总包；3.专业承包；4.劳务分包；5.勘察；6.设计；7.监理
,A.ZZJGDM FOrgCode     --组织机构代码
,A.DWDZ FAddress     --单位地址
,A.ZZDJ ZZDJ --资质等级
,A.ZGZSH ZZZSH
,A.ZGZH YYZZH
,A.FDDBR FLegalPerson     --法定代表人
,A.LXDH FTel     --联系电话
,A.LXR FLinkMan     --联系人
,A.YDDH FMobile     --移动电话
,A.ZXZZ mZXZZ     --主项资质
,NULL oZXZZ     --增项资质     
,NULL FCreateTime                  
,NULL FTime                        
,ISNULL(A.BZ,'')  Remark     --备注
FROM JKCWFDB_WORK_NJS.dbo.YW_JLDW A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));



--总包单位人员
INSERT INTO dbCenter.dbo.TC_PrjItem_Emp
SELECT 
A.ID FId         --主键
,B.GuidId FPrjId --项目外键
,FQ.GuidId FPrjItemId --单项工程外键
,A.YWBM FAppId  --业务主键
,A.PID FEntId --外键
,A.XM FHumanName --姓名
,CASE WHEN ISNULL(A.XB,'0')='男' THEN '1'
      WHEN ISNULL(A.XB,'0')='女' THEN '2'
ELSE NULL END FSex --性别
,NULL FPhoto --照片
,NULL FBirthDay --生日
,NULL ZJLX --证件类型
,CASE WHEN ISNULL(A.ZGXL,'0')='博士后' THEN '107000'
      WHEN ISNULL(A.ZGXL,'0')='博士' THEN '107001'
      WHEN ISNULL(A.ZGXL,'0')='硕士' THEN '107002'
      WHEN ISNULL(A.ZGXL,'0')='本科' THEN '107003'
      WHEN ISNULL(A.ZGXL,'0')='大专' THEN '107004'
      WHEN ISNULL(A.ZGXL,'0')='中专' THEN '107005'
      WHEN ISNULL(A.ZGXL,'0')='高中及以下' THEN '107006'
      WHEN ISNULL(A.ZGXL,'0')='无' THEN '107007'
ELSE NULL END ZGXL --最高学历
,A.YDDH FMobile     --移动电话
,A.LXDH FTel     --联系电话
,CASE WHEN ISNULL(A.RYLX,'0')='项目经理' THEN '11220201'
      WHEN ISNULL(A.RYLX,'0')='项目技术负责人' THEN '11220202'
      WHEN ISNULL(A.RYLX,'0')='安全负责人' THEN '11220203'
      WHEN ISNULL(A.RYLX,'0')='施工员' THEN '11220204'
      WHEN ISNULL(A.RYLX,'0')='质量员' THEN '11220205'
      WHEN ISNULL(A.RYLX,'0')='专职安全员' THEN '11220206'
      WHEN ISNULL(A.RYLX,'0')='材料员' THEN '11220207'
      WHEN ISNULL(A.RYLX,'0')='预算员' THEN '11220208'
      WHEN ISNULL(A.RYLX,'0')='总监理工程师' THEN '11220209'
      WHEN ISNULL(A.RYLX,'0')='专业监理工程师' THEN '11220210'
      WHEN ISNULL(A.RYLX,'0')='监理员' THEN '11220211'
      WHEN ISNULL(A.RYLX,'0')='其他' THEN '11220212'
ELSE NULL END EmpType --人员类型
,A.SFZH FIdCard     --身份证号
,NULL XMZW
,NULL ZJHM
,NULL FEntName
,A.ZW ZW     --职务
,CASE WHEN ISNULL(A.ZC,'0')='教授级高工' THEN '5081'
      WHEN ISNULL(A.ZC,'0')='工程师' THEN '5082'
      WHEN ISNULL(A.ZC,'0')='高级工程师' THEN '5083'
      WHEN ISNULL(A.ZC,'0')='助理工程师' THEN '5084'
      WHEN ISNULL(A.ZC,'0')='其他' THEN '5085'
ELSE NULL END ZC --职称
,A.ZY ZY     --专业
,A.ZSBH ZSBH     --证书编号
,A.DJ DJ     --等级
,A.ZCZSBH ZCBH     --注册编号
,A.ZCZY ZCZY     --注册专业
,A.FZRQ ZCRQ     --注册日期
,NULL FCreateTime
,NULL FTime
,NULL Remark
,A.RYID FEmpId --人员外键
,null Pid
,null FlinkId
,null FentType 
FROM JKCWFDB_WORK_NJS.dbo.YW_SGZCBDW_RYXX A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--专业承包人员
INSERT INTO dbCenter.dbo.TC_PrjItem_Emp
SELECT 
A.ID FId         --主键
,B.GuidId FPrjId --项目外键
,FQ.GuidId FPrjItemId --单项工程外键
,A.YWBM FAppId  --业务主键
,A.PID FEntId --企业外键
,A.XM FHumanName --姓名
,CASE WHEN ISNULL(A.XB,'0')='男' THEN '1'
      WHEN ISNULL(A.XB,'0')='女' THEN '2'
ELSE NULL END FSex --性别
,NULL FPhoto --照片
,NULL FBirthDay --生日
,NULL ZJLX --证件类型
,CASE WHEN ISNULL(A.ZGXL,'0')='博士后' THEN '107000'
      WHEN ISNULL(A.ZGXL,'0')='博士' THEN '107001'
      WHEN ISNULL(A.ZGXL,'0')='硕士' THEN '107002'
      WHEN ISNULL(A.ZGXL,'0')='本科' THEN '107003'
      WHEN ISNULL(A.ZGXL,'0')='大专' THEN '107004'
      WHEN ISNULL(A.ZGXL,'0')='中专' THEN '107005'
      WHEN ISNULL(A.ZGXL,'0')='高中及以下' THEN '107006'
      WHEN ISNULL(A.ZGXL,'0')='无' THEN '107007'
ELSE NULL END ZGXL --最高学历
,A.YDDH FMobile     --移动电话
,A.LXDH FTel     --联系电话
,CASE WHEN ISNULL(A.RYLX,'0')='项目经理' THEN '11220201'
      WHEN ISNULL(A.RYLX,'0')='项目技术负责人' THEN '11220202'
      WHEN ISNULL(A.RYLX,'0')='安全负责人' THEN '11220203'
      WHEN ISNULL(A.RYLX,'0')='施工员' THEN '11220204'
      WHEN ISNULL(A.RYLX,'0')='质量员' THEN '11220205'
      WHEN ISNULL(A.RYLX,'0')='专职安全员' THEN '11220206'
      WHEN ISNULL(A.RYLX,'0')='材料员' THEN '11220207'
      WHEN ISNULL(A.RYLX,'0')='预算员' THEN '11220208'
      WHEN ISNULL(A.RYLX,'0')='总监理工程师' THEN '11220209'
      WHEN ISNULL(A.RYLX,'0')='专业监理工程师' THEN '11220210'
      WHEN ISNULL(A.RYLX,'0')='监理员' THEN '11220211'
      WHEN ISNULL(A.RYLX,'0')='其他' THEN '11220212'
ELSE NULL END EmpType --人员类型
,A.SFZH FIdCard     --身份证号
,NULL XMZW
,NULL ZJHM
,NULL FEntName
,A.ZW ZW     --职务
,CASE WHEN ISNULL(A.ZC,'0')='教授级高工' THEN '5081'
      WHEN ISNULL(A.ZC,'0')='工程师' THEN '5082'
      WHEN ISNULL(A.ZC,'0')='高级工程师' THEN '5083'
      WHEN ISNULL(A.ZC,'0')='助理工程师' THEN '5084'
      WHEN ISNULL(A.ZC,'0')='其他' THEN '5085'
ELSE NULL END ZC --职称
,A.ZY ZY     --专业
,A.ZSBH ZSBH     --证书编号
,A.DJ DJ     --等级
,A.ZCZSBH ZCBH     --注册编号
,A.ZCZY ZCZY     --注册专业
,A.FZRQ ZCRQ     --注册日期
,NULL FCreateTime
,NULL FTime
,NULL Remark
,A.RYID FEmpId --人员外键
,null Pid
,null FlinkId
,null FentType 
FROM JKCWFDB_WORK_NJS.dbo.YW_ZYCBDW_RYXX A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));

--劳务分包人员
INSERT INTO dbCenter.dbo.TC_PrjItem_Emp
SELECT 
A.ID FId         --主键
,B.GuidId FPrjId --项目外键
,FQ.GuidId FPrjItemId --单项工程外键
,A.YWBM FAppId  --业务主键
,A.PID FEntId --企业外键
,A.XM FHumanName --姓名
,CASE WHEN ISNULL(A.XB,'0')='男' THEN '1'
      WHEN ISNULL(A.XB,'0')='女' THEN '2'
ELSE NULL END FSex --性别
,NULL FPhoto --照片
,NULL FBirthDay --生日
,NULL ZJLX --证件类型
,CASE WHEN ISNULL(A.ZGXL,'0')='博士后' THEN '107000'
      WHEN ISNULL(A.ZGXL,'0')='博士' THEN '107001'
      WHEN ISNULL(A.ZGXL,'0')='硕士' THEN '107002'
      WHEN ISNULL(A.ZGXL,'0')='本科' THEN '107003'
      WHEN ISNULL(A.ZGXL,'0')='大专' THEN '107004'
      WHEN ISNULL(A.ZGXL,'0')='中专' THEN '107005'
      WHEN ISNULL(A.ZGXL,'0')='高中及以下' THEN '107006'
      WHEN ISNULL(A.ZGXL,'0')='无' THEN '107007'
ELSE NULL END ZGXL --最高学历
,A.YDDH FMobile     --移动电话
,A.LXDH FTel     --联系电话
,CASE WHEN ISNULL(A.RYLX,'0')='项目经理' THEN '11220201'
      WHEN ISNULL(A.RYLX,'0')='项目技术负责人' THEN '11220202'
      WHEN ISNULL(A.RYLX,'0')='安全负责人' THEN '11220203'
      WHEN ISNULL(A.RYLX,'0')='施工员' THEN '11220204'
      WHEN ISNULL(A.RYLX,'0')='质量员' THEN '11220205'
      WHEN ISNULL(A.RYLX,'0')='专职安全员' THEN '11220206'
      WHEN ISNULL(A.RYLX,'0')='材料员' THEN '11220207'
      WHEN ISNULL(A.RYLX,'0')='预算员' THEN '11220208'
      WHEN ISNULL(A.RYLX,'0')='总监理工程师' THEN '11220209'
      WHEN ISNULL(A.RYLX,'0')='专业监理工程师' THEN '11220210'
      WHEN ISNULL(A.RYLX,'0')='监理员' THEN '11220211'
      WHEN ISNULL(A.RYLX,'0')='其他' THEN '11220212'
ELSE NULL END EmpType --人员类型
,A.SFZH FIdCard     --身份证号
,NULL XMZW
,NULL ZJHM
,NULL FEntName
,A.ZW ZW     --职务
,CASE WHEN ISNULL(A.ZC,'0')='教授级高工' THEN '5081'
      WHEN ISNULL(A.ZC,'0')='工程师' THEN '5082'
      WHEN ISNULL(A.ZC,'0')='高级工程师' THEN '5083'
      WHEN ISNULL(A.ZC,'0')='助理工程师' THEN '5084'
      WHEN ISNULL(A.ZC,'0')='其他' THEN '5085'
ELSE NULL END ZC --职称
,A.ZY ZY     --专业
,A.ZSBH ZSBH     --证书编号
,A.DJ DJ     --等级
,A.ZCZSBH ZCBH     --注册编号
,A.ZCZY ZCZY     --注册专业
,A.FZRQ ZCRQ     --注册日期
,NULL FCreateTime
,NULL FTime
,NULL Remark
,A.RYID FEmpId --人员外键
,null Pid
,null FlinkId
,null FentType 
FROM JKCWFDB_WORK_NJS.dbo.YW_LWFBDW_RYXX A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279));


--勘察人员
INSERT INTO dbCenter.dbo.TC_PrjItem_Emp
SELECT 
A.ID FId         --主键
,B.GuidId FPrjId --项目外键
,FQ.GuidId FPrjItemId --单项工程外键
,A.YWBM FAppId  --业务主键
,A.PID FEntId --企业外键
,A.XM FHumanName --姓名
,CASE WHEN ISNULL(A.XB,'0')='男' THEN '1'
      WHEN ISNULL(A.XB,'0')='女' THEN '2'
ELSE NULL END FSex --性别
,NULL FPhoto --照片
,NULL FBirthDay --生日
,NULL ZJLX --证件类型
,CASE WHEN ISNULL(A.ZGXL,'0')='博士后' THEN '107000'
      WHEN ISNULL(A.ZGXL,'0')='博士' THEN '107001'
      WHEN ISNULL(A.ZGXL,'0')='硕士' THEN '107002'
      WHEN ISNULL(A.ZGXL,'0')='本科' THEN '107003'
      WHEN ISNULL(A.ZGXL,'0')='大专' THEN '107004'
      WHEN ISNULL(A.ZGXL,'0')='中专' THEN '107005'
      WHEN ISNULL(A.ZGXL,'0')='高中及以下' THEN '107006'
      WHEN ISNULL(A.ZGXL,'0')='无' THEN '107007'
ELSE NULL END ZGXL --最高学历
,A.YDDH FMobile     --移动电话
,A.LXDH FTel     --联系电话
,CASE WHEN ISNULL(A.RYLX,'0')='项目经理' THEN '11220201'
      WHEN ISNULL(A.RYLX,'0')='项目技术负责人' THEN '11220202'
      WHEN ISNULL(A.RYLX,'0')='安全负责人' THEN '11220203'
      WHEN ISNULL(A.RYLX,'0')='施工员' THEN '11220204'
      WHEN ISNULL(A.RYLX,'0')='质量员' THEN '11220205'
      WHEN ISNULL(A.RYLX,'0')='专职安全员' THEN '11220206'
      WHEN ISNULL(A.RYLX,'0')='材料员' THEN '11220207'
      WHEN ISNULL(A.RYLX,'0')='预算员' THEN '11220208'
      WHEN ISNULL(A.RYLX,'0')='总监理工程师' THEN '11220209'
      WHEN ISNULL(A.RYLX,'0')='专业监理工程师' THEN '11220210'
      WHEN ISNULL(A.RYLX,'0')='监理员' THEN '11220211'
      WHEN ISNULL(A.RYLX,'0')='其他' THEN '11220212'
ELSE NULL END EmpType --人员类型
,A.SFZH FIdCard     --身份证号
,NULL XMZW
,NULL ZJHM
,NULL FEntName
,A.ZW ZW     --职务
,CASE WHEN ISNULL(A.ZC,'0')='教授级高工' THEN '5081'
      WHEN ISNULL(A.ZC,'0')='工程师' THEN '5082'
      WHEN ISNULL(A.ZC,'0')='高级工程师' THEN '5083'
      WHEN ISNULL(A.ZC,'0')='助理工程师' THEN '5084'
      WHEN ISNULL(A.ZC,'0')='其他' THEN '5085'
ELSE NULL END ZC --职称
,A.ZY ZY     --专业
,A.ZSBH ZSBH     --证书编号
,A.DJ DJ     --等级
,A.ZCZSBH ZCBH     --注册编号
,A.ZCZY ZCZY     --注册专业
,A.FZRQ ZCRQ     --注册日期
,NULL FCreateTime
,NULL FTime
,NULL Remark
,A.RYID FEmpId --人员外键
,null Pid
,null FlinkId
,null FentType 
FROM JKCWFDB_WORK_NJS.dbo.YW_KCDW_RYXX A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
  and not exists(select 1 from dbCenter.dbo.TC_PrjItem_Emp cc where a.id = cc.fid)


--设计人员
INSERT INTO dbCenter.dbo.TC_PrjItem_Emp
SELECT 
A.ID FId         --主键
,B.GuidId FPrjId --项目外键
,FQ.GuidId FPrjItemId --单项工程外键
,A.YWBM FAppId  --业务主键
,A.PID FEntId --企业外键
,A.XM FHumanName --姓名
,CASE WHEN ISNULL(A.XB,'0')='男' THEN '1'
      WHEN ISNULL(A.XB,'0')='女' THEN '2'
ELSE NULL END FSex --性别
,NULL FPhoto --照片
,NULL FBirthDay --生日
,NULL ZJLX --证件类型
,CASE WHEN ISNULL(A.ZGXL,'0')='博士后' THEN '107000'
      WHEN ISNULL(A.ZGXL,'0')='博士' THEN '107001'
      WHEN ISNULL(A.ZGXL,'0')='硕士' THEN '107002'
      WHEN ISNULL(A.ZGXL,'0')='本科' THEN '107003'
      WHEN ISNULL(A.ZGXL,'0')='大专' THEN '107004'
      WHEN ISNULL(A.ZGXL,'0')='中专' THEN '107005'
      WHEN ISNULL(A.ZGXL,'0')='高中及以下' THEN '107006'
      WHEN ISNULL(A.ZGXL,'0')='无' THEN '107007'
ELSE NULL END ZGXL --最高学历
,A.YDDH FMobile     --移动电话
,A.LXDH FTel     --联系电话
,CASE WHEN ISNULL(A.RYLX,'0')='项目经理' THEN '11220201'
      WHEN ISNULL(A.RYLX,'0')='项目技术负责人' THEN '11220202'
      WHEN ISNULL(A.RYLX,'0')='安全负责人' THEN '11220203'
      WHEN ISNULL(A.RYLX,'0')='施工员' THEN '11220204'
      WHEN ISNULL(A.RYLX,'0')='质量员' THEN '11220205'
      WHEN ISNULL(A.RYLX,'0')='专职安全员' THEN '11220206'
      WHEN ISNULL(A.RYLX,'0')='材料员' THEN '11220207'
      WHEN ISNULL(A.RYLX,'0')='预算员' THEN '11220208'
      WHEN ISNULL(A.RYLX,'0')='总监理工程师' THEN '11220209'
      WHEN ISNULL(A.RYLX,'0')='专业监理工程师' THEN '11220210'
      WHEN ISNULL(A.RYLX,'0')='监理员' THEN '11220211'
      WHEN ISNULL(A.RYLX,'0')='其他' THEN '11220212'
ELSE NULL END EmpType --人员类型
,A.SFZH FIdCard     --身份证号
,NULL XMZW
,NULL ZJHM
,NULL FEntName
,A.ZW ZW     --职务
,CASE WHEN ISNULL(A.ZC,'0')='教授级高工' THEN '5081'
      WHEN ISNULL(A.ZC,'0')='工程师' THEN '5082'
      WHEN ISNULL(A.ZC,'0')='高级工程师' THEN '5083'
      WHEN ISNULL(A.ZC,'0')='助理工程师' THEN '5084'
      WHEN ISNULL(A.ZC,'0')='其他' THEN '5085'
ELSE NULL END ZC --职称
,A.ZY ZY     --专业
,A.ZSBH ZSBH     --证书编号
,A.DJ DJ     --等级
,A.ZCZSBH ZCBH     --注册编号
,A.ZCZY ZCZY     --注册专业
,A.FZRQ ZCRQ     --注册日期
,NULL FCreateTime
,NULL FTime
,NULL Remark
,A.RYID FEmpId --人员外键
,null Pid
,null FlinkId
,null FentType 
FROM JKCWFDB_WORK_NJS.dbo.YW_SJDW_RYXX A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
  and not exists(select 1 from dbCenter.dbo.TC_PrjItem_Emp cc where a.id = cc.fid)


--监理人员
INSERT INTO dbCenter.dbo.TC_PrjItem_Emp
SELECT 
A.ID FId         --主键
,B.GuidId FPrjId --项目外键
,FQ.GuidId FPrjItemId --单项工程外键
,A.YWBM FAppId  --业务主键
,A.PID FEntId --企业外键
,A.XM FHumanName --姓名
,CASE WHEN ISNULL(A.XB,'0')='男' THEN '1'
      WHEN ISNULL(A.XB,'0')='女' THEN '2'
ELSE NULL END FSex --性别
,NULL FPhoto --照片
,NULL FBirthDay --生日
,NULL ZJLX --证件类型
,CASE WHEN ISNULL(A.ZGXL,'0')='博士后' THEN '107000'
      WHEN ISNULL(A.ZGXL,'0')='博士' THEN '107001'
      WHEN ISNULL(A.ZGXL,'0')='硕士' THEN '107002'
      WHEN ISNULL(A.ZGXL,'0')='本科' THEN '107003'
      WHEN ISNULL(A.ZGXL,'0')='大专' THEN '107004'
      WHEN ISNULL(A.ZGXL,'0')='中专' THEN '107005'
      WHEN ISNULL(A.ZGXL,'0')='高中及以下' THEN '107006'
      WHEN ISNULL(A.ZGXL,'0')='无' THEN '107007'
ELSE NULL END ZGXL --最高学历
,A.YDDH FMobile     --移动电话
,A.LXDH FTel     --联系电话
,CASE WHEN ISNULL(A.RYLX,'0')='项目经理' THEN '11220201'
      WHEN ISNULL(A.RYLX,'0')='项目技术负责人' THEN '11220202'
      WHEN ISNULL(A.RYLX,'0')='安全负责人' THEN '11220203'
      WHEN ISNULL(A.RYLX,'0')='施工员' THEN '11220204'
      WHEN ISNULL(A.RYLX,'0')='质量员' THEN '11220205'
      WHEN ISNULL(A.RYLX,'0')='专职安全员' THEN '11220206'
      WHEN ISNULL(A.RYLX,'0')='材料员' THEN '11220207'
      WHEN ISNULL(A.RYLX,'0')='预算员' THEN '11220208'
      WHEN ISNULL(A.RYLX,'0')='总监理工程师' THEN '11220209'
      WHEN ISNULL(A.RYLX,'0')='专业监理工程师' THEN '11220210'
      WHEN ISNULL(A.RYLX,'0')='监理员' THEN '11220211'
      WHEN ISNULL(A.RYLX,'0')='其他' THEN '11220212'
ELSE NULL END EmpType --人员类型
,A.SFZH FIdCard     --身份证号
,NULL XMZW
,NULL ZJHM
,NULL FEntName
,A.ZW ZW     --职务
,CASE WHEN ISNULL(A.ZC,'0')='教授级高工' THEN '5081'
      WHEN ISNULL(A.ZC,'0')='工程师' THEN '5082'
      WHEN ISNULL(A.ZC,'0')='高级工程师' THEN '5083'
      WHEN ISNULL(A.ZC,'0')='助理工程师' THEN '5084'
      WHEN ISNULL(A.ZC,'0')='其他' THEN '5085'
ELSE NULL END ZC --职称
,A.ZY ZY     --专业
,A.ZSBH ZSBH     --证书编号
,A.DJ DJ     --等级
,A.ZCZSBH ZCBH     --注册编号
,A.ZCZY ZCZY     --注册专业
,A.FZRQ ZCRQ     --注册日期
,NULL FCreateTime
,NULL FTime
,NULL Remark
,A.RYID FEmpId --人员外键
,null Pid
,null FlinkId
,null FentType 
FROM JKCWFDB_WORK_NJS.dbo.YW_JLDW_RYXX A
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMFQInfo FQ ON FQ.YWBM=A.YWBM
LEFT JOIN JKCWFDB_WORK_NJS.DBO.YW_XMInfo B ON B.YWBM=A.YWBM
WHERE ISNULL(FQ.FQBM,'')<>'' AND ISNULL(FQ.XMBM,'')<>'' --保证项目编码和分期编码都有值 
  AND B.JSDW <>'AA18F961-B45D-4391-B31C-C59B7C25B80C'  --排除建设单位测试帐号
  AND A.YWBM in (select distinct C05.ProcessKeyValue from JKCWFDB_WORK_NJS.DBO.C06 Left Join JKCWFDB_WORK_NJS.DBO.C05 on C05.ProjectID=C06.ProjectID
                 where C06.ActionState in (0,1,2) and C05.ProcessTimeB>='2014-01-01'and C06.actionid in (257,258,259,269,270,271,277,278,279))
  and not exists(select 1 from dbCenter.dbo.TC_PrjItem_Emp cc where a.id = cc.fid)
-----------------------------------------------------------------------------------------------------------------------------------------------------
--施工许可证人员锁定需要增加导入



--导入最终招标备案的过程数据
--CF_App_ProcessInstance   备案主流程表
--CF_App_ProcessRecord     备案子流程表
if not exists (select 1
            from  sysobjects
           where  id = object_id('dbo._App_ProcessInstance_SGXK')
            and   type = 'U')
begin 
    SELECT * into _App_ProcessInstance_SGXK from CF_App_ProcessInstance where 1=2  --只取表结构
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
	          a.FReportDate,1,'8801',1,1,'管理部门审核',1,2,1
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