--����֤��ȼ����ֵ 920

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