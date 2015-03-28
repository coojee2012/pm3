USE [dbCenter]
GO
/****** Object:  StoredProcedure [dbo].[SP_GC_TO_BZK]    Script Date: 2015/3/28 11:10:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--ͬ����Ŀ��Ϣ����׼��
alter PROC [dbo].[SP_GCPRJ_TO_BZK]
(
	@FID VARCHAR(50) --��Ŀ���
)
AS
IF NOT EXISTS(SELECT TOP 1* FROM XM_BaseInfo.[dbo].[XM_XMJBXX] WHERE [XMBH]=@FID)
BEGIN
--���ж���Ŀ�����أ���������ؼ����ж��Ƿ����м��������м����ж��Ƿ���ʡ��
declare  @xmsd int;
declare  @province int,@city int,@county int
select @province = Province,@city = City,@county = County from  TC_Prj_Info  where  FId = @FID;
if(isnull(@county,'') != '')
begin
set @xmsd = @county;
end
else if(isnull(@city,'') != '')
begin
   set @xmsd = @city;
end 
else if(ISNULL(@province,'') != '')
begin
   set @xmsd = @province;
end
---------------------------------------------------
INSERT INTO XM_BaseInfo.[dbo].[XM_XMBJXX]
           ([BJID]  --δ֪id,newid
           ,[XMBH]  --��Ŀ���
           ,[XMMC]  --��Ŀ����
           ,[XMSD]  --��Ŀ����
           ,[XMDZ]  --��Ŀ��ַ
           ,[JSDW]  --���赥λ
           ,[JSDWDZ] --���赥λ��ַ
           ,[JSDWFR] --���赥λ����
           ,[JSDWXZ] --���赥λ����
           ,[JSWDXDZM] --���赥λ(δ֪)
           ,[GCLB]     --�������
           ,[JGLX]     --ʲô����
           ,[ZFTZ]     --����Ͷ��
           ,[ZCTZ]     --ʲôͶ�� 
           ,[WSTZ]    --����Ͷ��
           ,[DKTZ]    --�����ܶ�
           ,[QTTZ]    --����Ͷ��
           ,[ZTZE]    --��Ͷ�ʶ�
           ,[YDMJ]    --δ֪
           ,[JZMJ]    --δ֪
           ,[DS]      --δ֪
           ,[DSCS]     --δ֪
           ,[DXCS]      --δ֪
           ,[ZDGD]       --δ֪
           ,[ZDKD]    --δ֪
           ,[CD]        --δ֪
           ,[KD]        --δ֪
           ,[JHKGRQ]    --�ƻ���������
           ,[JHJGRQ]    --�ƻ���������
           ,[FBFS]      --������ʽ
           ,[JSXZ]      --��������
           ,[XMJSNR]    --��Ŀ��������
           ,[JSGCYDXKZ] --���蹤���û����֤
           ,[JSGCGHXKZ] --���蹤�̹滮���֤
           ,[LXJB]      --δ֪
           ,[LXWJ]      --δ֪
           ,[LXWH]      --δ֪
           ,[PZDW]      --��׼��λ
           ,[PZRQ]      --��׼����
           ,[BJBH]      --�������
           ,[BJRQ]      --��������
           ,[LXPZMJ]    --δ֪
           ,[LXPZGM]    --δ֪
           ,[SFSW]      --δ֪
           ,[BZ]        --��ע
           ,[BH]        --���
           ,[CreateTime]  --��������
           ,[Ftime])      --��������
	SELECT TOP 1 
	newid(),                     
 	a.FId,                        
 	a.ProjectName,                
 	@xmsd,                     
 	a.Address,                     
 	a.jsdw,                       
 	a.JSDWDZ,                     
 	a.JSDWFR,                     
 	null,                   
 	C.DWXZ,                       
 	A.ProjectType,                
 	NULL,                         
 	NULL,                         
 	NULL,                         
 	NULL,                         
 	NULL,                         
 	NULL,                         
 	A.Investment,                 
 	NULL,                         
 	NULL,                         
 	NULL AS DS,                   
 	NULL,                         
 	NULL,                         
 	NULL,                         
 	NULL,                         
 	NULL,                         
 	NULL,                         
 	A.StartDate,                  
 	A.EndDate,                    
 	NULL AS FBFS,                 
   NULL AS JSXZ,                 
 	A.ConstrContent AS XMJSNR,    
 	A.JSYDXKZ,                    
 	A.JSGCXKZ,                    
 	NULL,                         
 	A.ConstrBasis,                
 	NULL,                         
 	NULL AS PZDW,                 
 	NULL AS PZRQ,                 
 	NULL AS BJBH,                 
 	NULL AS BJRQ,                 
 	NULL AS LXPZMJ,               
 	NULL AS LXPZGM,               
 	NULL AS SFSW,                 
 	NULL AS BZ,                   
 	NULL AS BH,                   
 	GETDATE(),                    
 	GETDATE()                     

	FROM TC_Prj_Info A 
	LEFT JOIN CF_SYS_DIC B ON A.CONSTRTYPE=B.FNUMBER
	LEFT JOIN TC_JSDW_USER C ON a.FJSDWID  = c.FID
	WHERE A.FID=@FID;
END
