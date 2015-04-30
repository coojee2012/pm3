
/*==============================================================*/
/* Table: CF_App_PrjFileList                                    */
/*==============================================================*/
create table dbo.CF_App_PrjFileList (
   FId                  char(36)             collate Chinese_PRC_CI_AS not null,
   FLinkId              char(36)             null,
   FTime                datetime             null,
   FCreateTime          datetime             null,
   FIsDeleted           bit                  null constraint DF_CF_App_PrjFileList_FIsDeleted default (0),
   FManageId            char(36)             collate Chinese_PRC_CI_AS null,
   FManageType          int                  null,
   FFileName            varchar(200)         collate Chinese_PRC_CI_AS null,
   FFileAmount          int                  null,
   FRemark              varchar(200)         collate Chinese_PRC_CI_AS null,
   FOrder               int                  null,
   FFileID              char(36)             collate Chinese_PRC_CI_AS null,
   FIsMust              int                  null,
   FIsPrjType           varchar(200)         collate Chinese_PRC_CI_AS null,
   FType                int                  null,
   FMType               varchar(200)         collate Chinese_PRC_CI_AS null,
   constraint CF_App_PrjFileList_FId_pk primary key (FId)
         on "PRIMARY"
)
on "PRIMARY"
go


if exists (select 1
          from sysobjects
          where id = object_id('dbo.Trigger_Insert_QAFileList')
          and type = 'TR')
   drop trigger dbo.Trigger_Insert_QAFileList
go


create trigger dbo.Trigger_Insert_QAFileList on dbo.TC_QA_Record for insert as
begin
   insert into CF_App_PrjFileList
           ([FId]
           ,[FLinkId]
           ,[FTime]
           ,[FCreateTime]
           ,[FIsDeleted]
           ,[FManageId]
           ,[FManageType]
           ,[FFileName]
           ,[FFileAmount]
           ,[FRemark]
           ,[FOrder]
           ,[FFileID]
           ,[FIsMust]
           ,[FIsPrjType]
           ,[FType]
           ,[FMType])
   select   a.FId
           ,b.FAppId
           ,a.FTime
           ,a.FCreateTime
           ,a.FIsDeleted
           ,a.FManageId
           ,a.FManageType
           ,a.FFileName
           ,a.FFileAmount
           ,a.FRemark
           ,a.FOrder
           ,a.FFileID
           ,a.FIsMust
           ,a.FIsPrjType
           ,a.FType
           ,a.FMType
      from CF_Sys_PrjList a,inserted b 
     where a.FManageType = b.PrjItemType
end
go


if exists (select 1
          from sysobjects
          where id = object_id('dbo.Trigger_Insert_AQFileList')
          and type = 'TR')
   drop trigger dbo.Trigger_Insert_AQFileList
go


create trigger dbo.Trigger_Insert_AQFileList on dbo.TC_AJBA_Record for insert as
begin
   insert into CF_App_PrjFileList
           ([FId]
           ,[FLinkId]
           ,[FTime]
           ,[FCreateTime]
           ,[FIsDeleted]
           ,[FManageId]
           ,[FManageType]
           ,[FFileName]
           ,[FFileAmount]
           ,[FRemark]
           ,[FOrder]
           ,[FFileID]
           ,[FIsMust]
           ,[FIsPrjType]
           ,[FType]
           ,[FMType])
   select  a.FId
           ,b.FAppId
           ,a.FTime
           ,a.FCreateTime
           ,a.FIsDeleted
           ,a.FManageId
           ,a.FManageType
           ,a.FFileName
           ,a.FFileAmount
           ,a.FRemark
           ,a.FOrder
           ,a.FFileID
           ,a.FIsMust
           ,a.FIsPrjType
           ,a.FType
           ,a.FMType
      from CF_Sys_PrjList a,inserted b 
     where a.FManageType = 11222
end
go
