--增加证书等级码表值 920

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
           ,'证书等级'
           ,GETDATE()
           ,0
           ,100
           ,920
           ,''
           ,0)
GO


--增加证书等级 子项

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
           ,'其他'
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