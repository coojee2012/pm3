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
INSERT INTO XM_BaseInfo.[dbo].[XM_XMJBXX]
           ([XMBH]   --��Ŀ���
           ,[XMMC]   --��Ŀ����
           ,[XMSD]   --��Ŀ����
           ,[XMDZ]   --��Ŀ��ַ
           ,[JSDW]   --���赥λ
           ,[JSDWDZ] --���赥λ��ַ
           ,[XMLX]   --��Ŀ����
           ,[XMZLX]  --����ģʽ
           ,[JSXZ]   --��������
           ,[JSMS]   --����ģʽ
           ,[XMZTZ]  --��Ŀ��Ͷ��
           ,[JSGM]   --�����ģ
           ,[JSNR]   --��������
           ,[BH]     --���  
           ,[JSDWZZJFDM]  --���赥λ��֯��������
           ,[JSDWFR]      --���赥λ����
           ,[JSDWFRDH]    --���赥λ���˵绰
           ,[JSDWJSFZR]   --���赥λ����������
           ,[JSDWJSFZRZC]  --���赥λ����������ְ��
           ,[JSDWJSFZRDH]  --���赥λ���������˵绰
           ,[SFSW]         --�Ƿ�����
           ,[CreateTime]   --����ʱ��
           ,[Ftime]        --����ʱ��
           ,[YDXZ]         --�õ�����
           ,[IsZHCJ])      --�Ƿ������ɹ�
	SELECT TOP 1 	           
 	a.FId,                        
 	a.ProjectName,                
 	@xmsd,                     
 	a.Address,                     
 	a.jsdw,                       
 	a.JSDWDZ,
	a.ProjectType,
	null,
	null as jsxz,
	null as jsms,
	a.Investment,
	a.ConstrScale,
	a.ConstrContent,
	a.ProjectNo,
	a.JSDWDM,
	a.JSDWFR,                  
 	null JSDWFRDH,
	null JSDWJSFZR,
	null as JSDWJSFZRZC,
	null as JSDWJSFZRDH,
	a.IsForeign,	           
 	GETDATE(),                    
 	GETDATE(),
	a.LandType,
	null   
	FROM TC_Prj_Info A 
	LEFT JOIN CF_SYS_DIC B ON A.CONSTRTYPE=B.FNUMBER
	LEFT JOIN TC_JSDW_USER C ON a.FJSDWID  = c.FID
	WHERE A.FID=@FID;
END

