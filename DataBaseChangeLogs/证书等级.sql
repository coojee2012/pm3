--����֤��ȼ����ֵ 920
if not exists(select 1 from [CF_Sys_DicClass] where [FName]= '֤��ȼ�' and [FNumber] =920 )
INSERT INTO [dbo].[CF_Sys_DicClass]
           ([FID]
           ,[FName]
           ,[FTime]
           ,[FIsDeleted]
           ,[FSystemId]
           ,[FNumber]
           ,[FCNumber]
           ,[FOrder])
     VALUES
           (NEWID()
           ,'֤��ȼ�'
           ,GETDATE()
           ,0
           ,100
           ,920
           ,''
           ,0)
GO


--����֤��ȼ� ����

USE [dbCenter]
GO
if not exists(select 1 from cf_sys_dic where FNumber= '920001')

INSERT INTO [dbo].[CF_Sys_Dic]
           ([FID]
           ,[FName]
           ,[FNumber]
           ,[FCNumber]
           ,[FParentId]
           ,[FClassId]
           ,[FOrder]
           ,[FLevel]
           ,[FIsDeleted]
           ,[FSystemId]
           ,[FRemark]
           ,[FCertiNo]
           ,[FTime])
     VALUES
           (NEWID()
           ,'����'
           ,920001
           ,'920001'
           ,920
           ,null
           ,0
           ,0
           ,0
           ,0
           ,''
           ,null
           ,GETDATE())
GO