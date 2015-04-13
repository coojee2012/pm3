USE [dbCenter]
GO

INSERT INTO [dbo].[TC_PrjItem_Emp_Lock]
           ([FId]
           ,[FPrjId]
           ,[FPrjItemId]
           ,[FAppId]
           ,[FEntId]
           ,[FIdCard]
           ,[FHumanName]
           ,[IsLock]
           ,[SelectedCount]
           ,[Remark],lockType)
	SELECT distinct [FId]
           ,[FPrjId]
           ,[FPrjItemId]
           ,[FAppId]
           ,[FEntId]
           ,[FIdCard]
		   ,fhumanname
		   ,b.state,1,'',0
	 from [TC_PrjItem_Emp] a ,[rylock].[dbo].[RY_LOCK] b
    where a.[FIdCard] = b.sfzh


INSERT INTO [dbo].[TC_PrjItem_Emp_Lock]
           ([FId]
           ,[FPrjId]
           ,[FPrjItemId]
           ,[FAppId]
           ,[FEntId]
           ,[FIdCard]
           ,[FHumanName]
		   ,FEntName 
           ,[IsLock]
           ,[SelectedCount]
		   ,FCreateTime,FTime
           ,lockType)
   select lockid,xmbm,null,KEYVALUE,ENTERID,SFZH,rymc,ENTERNAME,state,1,SDKSRQ,SDJSRQ,0
     from [rylock].[dbo].[RY_LOCK] a
    where not exists(select 1 from [TC_PrjItem_Emp_Lock] b where a.sfzh = b.[FIdCard])


