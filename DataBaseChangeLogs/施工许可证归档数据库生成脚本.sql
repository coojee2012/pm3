/**
施工许可证归档数据库生成脚本
*/
if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.GD_TC_SGXKZ_PrjInfo')
            and   type = 'U')
   drop table dbo.GD_TC_SGXKZ_PrjInfo
go

/*==============================================================*/
/* Table: GD_TC_SGXKZ_PrjInfo     项目信息归档库                                 */
/*==============================================================*/
create table dbo.GD_TC_SGXKZ_PrjInfo (
   FId                  varchar(36)          collate Chinese_PRC_CI_AS not null,
   FAppId               varchar(36)          collate Chinese_PRC_CI_AS not null,
   FPrjItemId           varchar(36)          collate Chinese_PRC_CI_AS not null,
   JSDW                 varchar(200)         collate Chinese_PRC_CI_AS null,
   JProvince            int                  null,
   JCity                int                  null,
   JCounty              int                  null,
   JSDWAddressDept      varchar(50)          collate Chinese_PRC_CI_AS null,
   JSDWDZ               varchar(200)         collate Chinese_PRC_CI_AS null,
   JSDWXZ               varchar(50)          collate Chinese_PRC_CI_AS null,
   FDDBR                varchar(50)          collate Chinese_PRC_CI_AS null,
   FRDH                 varchar(50)          collate Chinese_PRC_CI_AS null,
   LZR                  varchar(50)          collate Chinese_PRC_CI_AS null,
   LXDH                 varchar(50)          collate Chinese_PRC_CI_AS null,
   JSFZR                varchar(50)          collate Chinese_PRC_CI_AS null,
   JSFZRZC              varchar(50)          collate Chinese_PRC_CI_AS null,
   JSFZRDH              varchar(50)          collate Chinese_PRC_CI_AS null,
   PrjId                varchar(36)          collate Chinese_PRC_CI_AS null,
   PrjItemId            varchar(36)          collate Chinese_PRC_CI_AS null,
   ProjectName          varchar(200)         collate Chinese_PRC_CI_AS null,
   PrjItemName          varchar(200)         collate Chinese_PRC_CI_AS null,
   PrjItemType          varchar(50)          collate Chinese_PRC_CI_AS null,
   ProjectTime          date                 null,
   ReportTime           date                 null,
   PProvince            int                  null,
   PCity                int                  null,
   PCounty              int                  null,
   PrjAddressDept       varchar(50)          collate Chinese_PRC_CI_AS null,
   Address              varchar(500)         collate Chinese_PRC_CI_AS null,
   ConstrScale          varchar(500)         collate Chinese_PRC_CI_AS null,
   ConstrType           varchar(50)          collate Chinese_PRC_CI_AS null,
   Price                varchar(50)          collate Chinese_PRC_CI_AS null,
   Currency             varchar(50)          collate Chinese_PRC_CI_AS null,
   StartDate            date                 null,
   EndDate              date                 null,
   FResult              varchar(20)          collate Chinese_PRC_CI_AS null,
   Remark               text                 collate Chinese_PRC_CI_AS null,
   ProjectFile          varchar(200)         collate Chinese_PRC_CI_AS null,
   ProjectNo            varchar(50)          collate Chinese_PRC_CI_AS null,
   ProjectLevel         varchar(20)          collate Chinese_PRC_CI_AS null,
   Cost                 decimal(15,4)        null,
   Area                 decimal(15,4)        null,
   BuildType            varchar(20)          collate Chinese_PRC_CI_AS null,
   ProjectUse           varchar(20)          collate Chinese_PRC_CI_AS null,
   ProjectNumber        varchar(200)         collate Chinese_PRC_CI_AS null,
   SGXKZBH              varchar(100)         collate Chinese_PRC_CI_AS null,
   FZJG                 varchar(100)         collate Chinese_PRC_CI_AS null,
   FZTime               date                 null,
   DZZT                 int                  null,
   constraint PK__GD_TC_SGXKZ__C1BEAA4218427513 primary key (FId)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   '工程简要说明表GD_TC_SGXKZ_PrjInfo',
   'user', 'dbo', 'table', 'GD_TC_SGXKZ_PrjInfo'
go
------------------------------------------------------------------

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.GD_TC_PrjItem_Ent')
            and   type = 'U')
   drop table dbo.GD_TC_PrjItem_Ent
go

/*==============================================================*/
/* Table: GD_TC_PrjItem_Ent                                     */
/*==============================================================*/
create table dbo.GD_TC_PrjItem_Ent (
   FId                  varchar(36)          collate Chinese_PRC_CI_AS not null,
   FPrjId               varchar(36)          collate Chinese_PRC_CI_AS null,
   FPrjItemId           varchar(36)          collate Chinese_PRC_CI_AS null,
   FProcId              varchar(36)          collate Chinese_PRC_CI_AS null,
   FAppId               varchar(36)          collate Chinese_PRC_CI_AS null,
   QYID                 varchar(60)          collate Chinese_PRC_CI_AS null,
   FName                varchar(200)         collate Chinese_PRC_CI_AS null,
   FEntType             int                  null,
   FOrgCode             varchar(50)          collate Chinese_PRC_CI_AS null,
   FAddress             varchar(500)         collate Chinese_PRC_CI_AS null,
   ZZDJ                 varchar(100)         collate Chinese_PRC_CI_AS null,
   ZZZSH                varchar(60)          collate Chinese_PRC_CI_AS null,
   YYZZH                varchar(50)          collate Chinese_PRC_CI_AS null,
   FLegalPerson         varchar(50)          collate Chinese_PRC_CI_AS null,
   FTel                 varchar(50)          collate Chinese_PRC_CI_AS null,
   FLinkMan             varchar(50)          collate Chinese_PRC_CI_AS null,
   FMobile              varchar(50)          collate Chinese_PRC_CI_AS null,
   mZXZZ                varchar(500)         collate Chinese_PRC_CI_AS null,
   oZXZZ                text                 collate Chinese_PRC_CI_AS null,
   FCreateTime          datetime             null,
   FTime                datetime             null,
   Remark               text                 collate Chinese_PRC_CI_AS null,
   constraint PK__GD_TC_PrjIt__C1BEAA425398450B primary key (FId)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   '项目参与企业归档TC_PrjItem_Ent',
   'user', 'dbo', 'table', 'GD_TC_PrjItem_Ent'
go
---------------------------------------------------------
if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.GD_TC_PrjItem_Emp')
            and   type = 'U')
   drop table dbo.GD_TC_PrjItem_Emp
go

/*==============================================================*/
/* Table: GD_TC_PrjItem_Emp                                     */
/*==============================================================*/
create table dbo.GD_TC_PrjItem_Emp (
   FId                  varchar(36)          collate Chinese_PRC_CI_AS not null,
   FPrjId               varchar(36)          collate Chinese_PRC_CI_AS null,
   FPrjItemId           varchar(36)          collate Chinese_PRC_CI_AS null,
   FAppId               varchar(36)          collate Chinese_PRC_CI_AS null,
   FEntId               varchar(60)          collate Chinese_PRC_CI_AS null,
   FHumanName           varchar(50)          collate Chinese_PRC_CI_AS null,
   FSex                 varchar(10)          collate Chinese_PRC_CI_AS null,
   FPhoto               image                null,
   FBirthDay            date                 null,
   ZJLX                 varchar(20)          collate Chinese_PRC_CI_AS null,
   ZGXL                 varchar(20)          collate Chinese_PRC_CI_AS null,
   FMobile              varchar(50)          collate Chinese_PRC_CI_AS null,
   FTel                 varchar(50)          collate Chinese_PRC_CI_AS null,
   EmpType              int                  null,
   FIdCard              varchar(50)          collate Chinese_PRC_CI_AS null,
   XMZW                 char(2)              collate Chinese_PRC_CI_AS null,
   ZJHM                 varchar(50)          collate Chinese_PRC_CI_AS null,
   FEntName             varchar(200)         collate Chinese_PRC_CI_AS null,
   ZW                   varchar(50)          collate Chinese_PRC_CI_AS null,
   ZC                   varchar(20)          collate Chinese_PRC_CI_AS null,
   ZY                   varchar(50)          collate Chinese_PRC_CI_AS null,
   ZSBH                 varchar(50)          collate Chinese_PRC_CI_AS null,
   DJ                   varchar(50)          collate Chinese_PRC_CI_AS null,
   ZCBH                 varchar(50)          collate Chinese_PRC_CI_AS null,
   ZCZY                 varchar(50)          collate Chinese_PRC_CI_AS null,
   ZCRQ                 datetime             null,
   FCreateTime          datetime             null,
   FTime                datetime             null,
   Remark               text                 collate Chinese_PRC_CI_AS null,
   FEmpId               varchar(60)          collate Chinese_PRC_CI_AS null,
   PId                  varchar(50)          collate Chinese_PRC_CI_AS null,
   FLinkId              varchar(50)          collate Chinese_PRC_CI_AS null,
   constraint PK__GD_TC_PrjIt__C1BEAA425768D5EF primary key (FId)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   '项目参与人员归档GD_TC_PrjItem_Emp',
   'user', 'dbo', 'table', 'GD_TC_PrjItem_Emp'
go
-----------------------------------------------------------------------------
if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.GD_TC_SGXKZ_Location')
            and   type = 'U')
   drop table dbo.GD_TC_SGXKZ_Location
go

/*==============================================================*/
/* Table: GD_TC_SGXKZ_Location                                  */
/*==============================================================*/
create table dbo.GD_TC_SGXKZ_Location (
   FId                  varchar(36)          collate Chinese_PRC_CI_AS not null,
   FAppId               varchar(36)          collate Chinese_PRC_CI_AS not null,
   FprjItemId           varchar(36)          collate Chinese_PRC_CI_AS not null,
   ProjectName          varchar(200)         collate Chinese_PRC_CI_AS null,
   JSDW                 varchar(200)         collate Chinese_PRC_CI_AS null,
   LocationAddress      varchar(200)         collate Chinese_PRC_CI_AS null,
   Area                 decimal(15,2)        null,
   Scale                varchar(500)         collate Chinese_PRC_CI_AS null,
   ProjectBasis         text                 collate Chinese_PRC_CI_AS null,
   CreateTime           date                 null,
   HFJG                 varchar(200)         collate Chinese_PRC_CI_AS null,
   XZYJSZSBH            varchar(50)          collate Chinese_PRC_CI_AS null,
   BL                   varchar(10)          collate Chinese_PRC_CI_AS null,
   YL                   varchar(1000)        collate Chinese_PRC_CI_AS null,
   YDPZSX               varchar(1000)        collate Chinese_PRC_CI_AS null,
   constraint PK__GD_TC_SGXKZ__C1BEAA421C1305F7 primary key (FId)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   '选址意见书归档GD_TC_SGXKZ_Location',
   'user', 'dbo', 'table', 'GD_TC_SGXKZ_Location'
go
----------------------------------------------------
if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.GD_TC_SGXKZ_File')
            and   type = 'U')
   drop table dbo.GD_TC_SGXKZ_File
go

/*==============================================================*/
/* Table: GD_TC_SGXKZ_File                                      */
/*==============================================================*/
create table dbo.GD_TC_SGXKZ_File (
   FId                  varchar(36)          collate Chinese_PRC_CI_AS not null,
   FAppId               varchar(36)          collate Chinese_PRC_CI_AS not null,
   FprjItemId           varchar(36)          collate Chinese_PRC_CI_AS not null,
   FLinkId              varchar(36)          collate Chinese_PRC_CI_AS not null,
   FileName             varchar(1000)        collate Chinese_PRC_CI_AS null,
   ReportTime           date                 null,
   FilePath             varchar(200)         collate Chinese_PRC_CI_AS null,
   FileType             varchar(80)          collate Chinese_PRC_CI_AS null,
   Size                 float                null,
   constraint PK__GD_TC_SGXKZ__C1BEAA423DC8FF7D primary key (FId)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   'TC_SGXKZ_File',
   'user', 'dbo', 'table', 'GD_TC_SGXKZ_File'
go

-----------------------------------------------------
if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.GD_TC_SGXKZ_JSYDGHXKZ')
            and   type = 'U')
   drop table dbo.GD_TC_SGXKZ_JSYDGHXKZ
go

/*==============================================================*/
/* Table: GD_TC_SGXKZ_JSYDGHXKZ                                 */
/*==============================================================*/
create table dbo.GD_TC_SGXKZ_JSYDGHXKZ (
   FId                  varchar(36)          collate Chinese_PRC_CI_AS not null,
   FAppId               varchar(36)          collate Chinese_PRC_CI_AS not null,
   FprjItemId           varchar(36)          collate Chinese_PRC_CI_AS not null,
   ProjectName          varchar(200)         collate Chinese_PRC_CI_AS null,
   JSDW                 varchar(200)         collate Chinese_PRC_CI_AS null,
   Address              varchar(500)         collate Chinese_PRC_CI_AS null,
   Area                 decimal(15,2)        null,
   ConstrScale          varchar(500)         collate Chinese_PRC_CI_AS null,
   Others               varchar(50)          collate Chinese_PRC_CI_AS null,
   YDXZ                 text                 collate Chinese_PRC_CI_AS null,
   CreateTime           date                 null,
   HFJG                 varchar(200)         collate Chinese_PRC_CI_AS null,
   YDGHXKZBH            varchar(50)          collate Chinese_PRC_CI_AS null,
   BL                   varchar(10)          collate Chinese_PRC_CI_AS null,
   YL                   varchar(1000)        collate Chinese_PRC_CI_AS null,
   constraint PK__GD_TC_SGXKZ__C1BEAA42304F08CE primary key (FId)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   '建设用地规划许可证归档GD_TC_SGXKZ_JSYDGHXKZ',
   'user', 'dbo', 'table', 'GD_TC_SGXKZ_JSYDGHXKZ'
go
--------------------------------------------------------------------------
if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.GD_TC_SGXKZ_JSGCGHXKZ')
            and   type = 'U')
   drop table dbo.GD_TC_SGXKZ_JSGCGHXKZ
go

/*==============================================================*/
/* Table: GD_TC_SGXKZ_JSGCGHXKZ                                 */
/*==============================================================*/
create table dbo.GD_TC_SGXKZ_JSGCGHXKZ (
   FId                  varchar(36)          collate Chinese_PRC_CI_AS not null,
   FAppId               varchar(36)          collate Chinese_PRC_CI_AS not null,
   FprjItemId           varchar(36)          collate Chinese_PRC_CI_AS not null,
   ProjectName          varchar(200)         collate Chinese_PRC_CI_AS null,
   JSDW                 varchar(200)         collate Chinese_PRC_CI_AS null,
   Address              varchar(500)         collate Chinese_PRC_CI_AS null,
   Area                 decimal(15,2)        null,
   ConstrScale          varchar(500)         collate Chinese_PRC_CI_AS null,
   Span                 decimal(15,2)        null,
   Others               varchar(50)          collate Chinese_PRC_CI_AS null,
   CreateTime           date                 null,
   HFJG                 varchar(200)         collate Chinese_PRC_CI_AS null,
   GCGHXKZBH            varchar(100)         collate Chinese_PRC_CI_AS null,
   BL                   varchar(10)          collate Chinese_PRC_CI_AS null,
   YL                   varchar(1000)        collate Chinese_PRC_CI_AS null,
   constraint PK__GD_TC_SGXKZ__C1BEAA42341F99B2 primary key (FId)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   '建设工程规划许可证归档GD_TC_SGXKZ_JSGCGHXKZ',
   'user', 'dbo', 'table', 'GD_TC_SGXKZ_JSGCGHXKZ'
go
---------------------------------------------------------

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.GD_TC_SGXKZ_ZBJG')
            and   type = 'U')
   drop table dbo.GD_TC_SGXKZ_ZBJG
go

/*==============================================================*/
/* Table: GD_TC_SGXKZ_ZBJG                                      */
/*==============================================================*/
create table dbo.GD_TC_SGXKZ_ZBJG (
   FId                  varchar(36)          collate Chinese_PRC_CI_AS not null,
   FAppId               varchar(36)          collate Chinese_PRC_CI_AS not null,
   FprjItemId           varchar(36)          collate Chinese_PRC_CI_AS not null,
   ProjectName          varchar(200)         collate Chinese_PRC_CI_AS null,
   PrjItemName          varchar(200)         collate Chinese_PRC_CI_AS null,
   JSDW                 varchar(200)         collate Chinese_PRC_CI_AS null,
   KCId                 varchar(36)          collate Chinese_PRC_CI_AS null,
   KCZBFS               varchar(20)          collate Chinese_PRC_CI_AS null,
   KCZZFS               varchar(20)          collate Chinese_PRC_CI_AS null,
   KCZBDW               varchar(200)         collate Chinese_PRC_CI_AS null,
   KCZBQYZZZSH          varchar(50)          collate Chinese_PRC_CI_AS null,
   KCZBJ                decimal(15,4)        null,
   KCZBGQ               varchar(50)          collate Chinese_PRC_CI_AS null,
   KCHTKGTime           date                 null,
   KCHTJGTime           date                 null,
   KCHTQDTime           date                 null,
   KCHTBATime           date                 null,
   KCHTBABH             varchar(50)          collate Chinese_PRC_CI_AS null,
   KCXMJL               varchar(50)          collate Chinese_PRC_CI_AS null,
   SJId                 varchar(36)          collate Chinese_PRC_CI_AS null,
   SJZBFS               varchar(20)          collate Chinese_PRC_CI_AS null,
   SJZZFS               varchar(20)          collate Chinese_PRC_CI_AS null,
   SJZBDW               varchar(200)         collate Chinese_PRC_CI_AS null,
   SJZBQYZZZSH          varchar(50)          collate Chinese_PRC_CI_AS null,
   SJZLBZ               varchar(200)         collate Chinese_PRC_CI_AS null,
   SJZBJ                decimal(15,4)        null,
   SJZBGQ               varchar(50)          collate Chinese_PRC_CI_AS null,
   SJHTKGTime           date                 null,
   SJHTJGTime           date                 null,
   SJHTQDTime           date                 null,
   SJHTBATime           date                 null,
   SJHTBABH             varchar(50)          collate Chinese_PRC_CI_AS null,
   SJHTBAJG             varchar(200)         collate Chinese_PRC_CI_AS null,
   JLId                 varchar(36)          collate Chinese_PRC_CI_AS not null,
   JLZBFS               varchar(20)          collate Chinese_PRC_CI_AS null,
   JLZZFS               varchar(20)          collate Chinese_PRC_CI_AS null,
   JLZBDW               varchar(200)         collate Chinese_PRC_CI_AS null,
   JLZBQYZZZSH          varchar(50)          collate Chinese_PRC_CI_AS null,
   JLZBQYZZDJ           varchar(50)          collate Chinese_PRC_CI_AS null,
   JLZLBZ               varchar(200)         collate Chinese_PRC_CI_AS null,
   JLZBJ                decimal(15,4)        null,
   JLZBJDX              varchar(50)          collate Chinese_PRC_CI_AS null,
   JLZBGQ               varchar(50)          collate Chinese_PRC_CI_AS null,
   JLHTKGTime           date                 null,
   JLHTJGTime           date                 null,
   JLHTQDTime           date                 null,
   JLHTBATime           date                 null,
   JLHTBABH             varchar(50)          collate Chinese_PRC_CI_AS null,
   JLHTBAJG             varchar(200)         collate Chinese_PRC_CI_AS null,
   JLGCS                varchar(50)          collate Chinese_PRC_CI_AS null,
   SGId                 varchar(36)          collate Chinese_PRC_CI_AS not null,
   SGZBFS               varchar(20)          collate Chinese_PRC_CI_AS null,
   SGZZFS               varchar(20)          collate Chinese_PRC_CI_AS null,
   SGZBDW               varchar(200)         collate Chinese_PRC_CI_AS null,
   SGZBQYZZZSH          varchar(50)          collate Chinese_PRC_CI_AS null,
   SGZBQYZZDJ           varchar(50)          collate Chinese_PRC_CI_AS null,
   SGZLBZ               varchar(200)         collate Chinese_PRC_CI_AS null,
   SGZLDJ               varchar(50)          collate Chinese_PRC_CI_AS null,
   SGZBJ                decimal(15,4)        null,
   SGZBJDX              varchar(50)          collate Chinese_PRC_CI_AS null,
   SGZBGQ               varchar(50)          collate Chinese_PRC_CI_AS null,
   SGHTKGTime           date                 null,
   SGHTJGTime           date                 null,
   SGZBFW               text                 collate Chinese_PRC_CI_AS null,
   SGJZBM               decimal(15,2)        null,
   SGSZSNYL             varchar(50)          collate Chinese_PRC_CI_AS null,
   SGXMJL               varchar(50)          collate Chinese_PRC_CI_AS null,
   SGXMJLZS             varchar(50)          collate Chinese_PRC_CI_AS null,
   SGHTQDTime           date                 null,
   SGHTBATime           date                 null,
   SGHTBABH             varchar(50)          collate Chinese_PRC_CI_AS null,
   SGHTBAJG             varchar(200)         collate Chinese_PRC_CI_AS null,
   ProjectNo            varchar(200)         collate Chinese_PRC_CI_AS null,
   ZBTZSBH              varchar(50)          collate Chinese_PRC_CI_AS null,
   ConstrScale          varchar(500)         collate Chinese_PRC_CI_AS null,
   Area                 decimal(15,4)        null,
   ZBDLDWMC             varchar(200)         collate Chinese_PRC_CI_AS null,
   ZBDLDWZZJGDM         varchar(50)          collate Chinese_PRC_CI_AS null,
   JLZBLX               varchar(20)          collate Chinese_PRC_CI_AS null,
   JLZBDWZZJGDM         varchar(50)          collate Chinese_PRC_CI_AS null,
   JLZBRQ               date                 null,
   JLGCSZJLX            varchar(20)          collate Chinese_PRC_CI_AS null,
   JLGCSZJHM            varchar(30)          collate Chinese_PRC_CI_AS null,
   SGZBLX               varchar(20)          collate Chinese_PRC_CI_AS null,
   SGZBDWZZJGDM         varchar(20)          collate Chinese_PRC_CI_AS null,
   SGZBRQ               date                 null,
   SGXMJLZJLX           varchar(20)          collate Chinese_PRC_CI_AS null,
   SGXMJLZJHM           varchar(20)          collate Chinese_PRC_CI_AS null,
   BL                   varchar(10)          collate Chinese_PRC_CI_AS null,
   YL                   varchar(1000)        collate Chinese_PRC_CI_AS null,
   constraint PK__GD_TC_SGXKZ__C1BEAA426E4C3B47 primary key (FId)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   '招投标信息TC_SGXKZ_ZBJG',
   'user', 'dbo', 'table', 'GD_TC_SGXKZ_ZBJG'
go
-----------------------------------------------------------------------------------
if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.GD_TC_SGXKZ_HTBA')
            and   type = 'U')
   drop table dbo.GD_TC_SGXKZ_HTBA
go

/*==============================================================*/
/* Table: GD_TC_SGXKZ_HTBA                                      */
/*==============================================================*/
create table dbo.GD_TC_SGXKZ_HTBA (
   FId                  varchar(36)          collate Chinese_PRC_CI_AS not null,
   FAppId               varchar(36)          collate Chinese_PRC_CI_AS not null,
   FprjItemId           varchar(36)          collate Chinese_PRC_CI_AS not null,
   HTBABH               varchar(50)          collate Chinese_PRC_CI_AS null,
   ProjectNo            varchar(30)          collate Chinese_PRC_CI_AS null,
   HTBH                 varchar(50)          collate Chinese_PRC_CI_AS null,
   HTJE                 decimal(15,4)        null,
   ConstrScale          varchar(500)         collate Chinese_PRC_CI_AS null,
   HTQDTime             date                 null,
   FBDWMC               varchar(200)         collate Chinese_PRC_CI_AS null,
   FBDWZZJGDM           varchar(15)          collate Chinese_PRC_CI_AS null,
   CBDWMC               varchar(200)         collate Chinese_PRC_CI_AS null,
   CBDWZZJGDM           varchar(15)          collate Chinese_PRC_CI_AS null,
   LHTCBDWMC            varchar(200)         collate Chinese_PRC_CI_AS null,
   LHTCBDWZZJGDM        varchar(15)          collate Chinese_PRC_CI_AS null,
   HTLB                 varchar(20)          collate Chinese_PRC_CI_AS null,
   BL                   varchar(10)          collate Chinese_PRC_CI_AS null,
   YL                   varchar(1000)        collate Chinese_PRC_CI_AS null,
   FBDWId               varchar(50)          collate Chinese_PRC_CI_AS null,
   CBDWId               varchar(50)          collate Chinese_PRC_CI_AS null,
   LHTDWId              varchar(50)          collate Chinese_PRC_CI_AS null,
   constraint PK__GD_TC_SGXKZ__C1BEAA4252050254 primary key (FId)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   'TC_SGXKZ_HTBA',
   'user', 'dbo', 'table', 'GD_TC_SGXKZ_HTBA'
go
-------------------------------------------------------------------------------

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.GD_TC_SGXKZ_SGTSC')
            and   type = 'U')
   drop table dbo.GD_TC_SGXKZ_SGTSC
go

/*==============================================================*/
/* Table: GD_TC_SGXKZ_SGTSC                                     */
/*==============================================================*/
create table dbo.GD_TC_SGXKZ_SGTSC (
   FId                  varchar(36)          collate Chinese_PRC_CI_AS not null,
   FAppId               varchar(36)          collate Chinese_PRC_CI_AS not null,
   FprjItemId           varchar(36)          collate Chinese_PRC_CI_AS not null,
   SGTSCHGSBH           varchar(50)          collate Chinese_PRC_CI_AS null,
   ProjectNo            varchar(50)          collate Chinese_PRC_CI_AS null,
   SGTSCJGMC            varchar(200)         collate Chinese_PRC_CI_AS null,
   SGTSCZZJGDM          varchar(15)          collate Chinese_PRC_CI_AS null,
   SCWCRQ               date                 null,
   ConstrScale          varchar(500)         collate Chinese_PRC_CI_AS null,
   KCDWMC               varchar(200)         collate Chinese_PRC_CI_AS null,
   KCDWZZJGDM           varchar(15)          collate Chinese_PRC_CI_AS null,
   SJDWMC               varchar(200)         collate Chinese_PRC_CI_AS null,
   SJDWZZJGDM           varchar(15)          collate Chinese_PRC_CI_AS null,
   YCSCSFTG             int                  null,
   YCSCWFTS             int                  null,
   YCSCWFTM             varchar(500)         collate Chinese_PRC_CI_AS null,
   BL                   varchar(10)          collate Chinese_PRC_CI_AS null,
   YL                   varchar(1000)        collate Chinese_PRC_CI_AS null,
   SGTSCJGId            varchar(50)          collate Chinese_PRC_CI_AS null,
   KCDWId               varchar(50)          collate Chinese_PRC_CI_AS null,
   SJDWId               varchar(50)          collate Chinese_PRC_CI_AS null,
   constraint PK__GD_TC_SGXKZ__C1BEAA424E347170 primary key (FId)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   'TC_SGXKZ_SGTSC',
   'user', 'dbo', 'table', 'GD_TC_SGXKZ_SGTSC'
go
------------------------------------------------------------------------------


if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.GD_TC_SGXKZ_JDSX')
            and   type = 'U')
   drop table dbo.GD_TC_SGXKZ_JDSX
go

/*==============================================================*/
/* Table: GD_TC_SGXKZ_JDSX                                      */
/*==============================================================*/
create table dbo.GD_TC_SGXKZ_JDSX (
   FId                  varchar(36)          collate Chinese_PRC_CI_AS not null,
   FPrjItemId           varchar(36)          collate Chinese_PRC_CI_AS not null,
   FAppId               varchar(36)          collate Chinese_PRC_CI_AS not null,
   ProjectName          varchar(200)         collate Chinese_PRC_CI_AS null,
   PrjItemName          varchar(200)         collate Chinese_PRC_CI_AS null,
   JSDW                 varchar(200)         collate Chinese_PRC_CI_AS null,
   ZLBABH               varchar(50)          collate Chinese_PRC_CI_AS null,
   ZLBAJG               varchar(50)          collate Chinese_PRC_CI_AS null,
   ZLBATime             date                 null,
   AQBABH               varchar(50)          collate Chinese_PRC_CI_AS null,
   AQBAJG               varchar(50)          collate Chinese_PRC_CI_AS null,
   AQBATime             date                 null,
   BL                   varchar(10)          collate Chinese_PRC_CI_AS null,
   YL                   varchar(1000)        collate Chinese_PRC_CI_AS null,
   constraint PK__GD_TC_SGXKZ__C1BEAA4251BAE991 primary key (FId)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   '质量安全监督手续TC_SGXKZ_JDSX',
   'user', 'dbo', 'table', 'GD_TC_SGXKZ_JDSX'
go
---------------------------------------------------------------------------

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.GD_TC_SGXKZ_ZJBH')
            and   type = 'U')
   drop table dbo.GD_TC_SGXKZ_ZJBH
go

/*==============================================================*/
/* Table: GD_TC_SGXKZ_ZJBH                                      */
/*==============================================================*/
create table dbo.GD_TC_SGXKZ_ZJBH (
   FId                  varchar(36)          collate Chinese_PRC_CI_AS not null,
   FPrjItemId           varchar(36)          collate Chinese_PRC_CI_AS not null,
   FAppId               varchar(36)          collate Chinese_PRC_CI_AS not null,
   ProjectName          varchar(200)         collate Chinese_PRC_CI_AS null,
   PrjItemName          varchar(200)         collate Chinese_PRC_CI_AS null,
   JSDW                 varchar(200)         collate Chinese_PRC_CI_AS null,
   ISDBS                int                  null,
   ZJBH                 varchar(1000)        collate Chinese_PRC_CI_AS null,
   JF                   varchar(100)         collate Chinese_PRC_CI_AS null,
   YF                   varchar(100)         collate Chinese_PRC_CI_AS null,
   constraint PK__GD_TC_SGXKZ__C1BEAA42558B7A75 primary key (FId)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   'TC_SGXKZ_ZJBH',
   'user', 'dbo', 'table', 'GD_TC_SGXKZ_ZJBH'
go
---------------------------------------------------------------------------
if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.GD_TC_SGXKZ_QTZL')
            and   type = 'U')
   drop table dbo.GD_TC_SGXKZ_QTZL
go

/*==============================================================*/
/* Table: GD_TC_SGXKZ_QTZL                                      */
/*==============================================================*/
create table dbo.GD_TC_SGXKZ_QTZL (
   FId                  varchar(36)          collate Chinese_PRC_CI_AS not null,
   FAppId               varchar(36)          collate Chinese_PRC_CI_AS not null,
   FprjItemId           varchar(36)          collate Chinese_PRC_CI_AS not null,
   ProjectName          varchar(200)         collate Chinese_PRC_CI_AS null,
   PrjItemName          varchar(200)         collate Chinese_PRC_CI_AS null,
   JSDW                 varchar(200)         collate Chinese_PRC_CI_AS null,
   QTZL                 text                 collate Chinese_PRC_CI_AS null,
   SGTJ                 varchar(1000)        collate Chinese_PRC_CI_AS null,
   CNS                  varchar(1000)        collate Chinese_PRC_CI_AS null,
   constraint PK__GD_TC_SGXKZ__C1BEAA4210A1534B primary key (FId)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   '其他资料TC_SGXKZ_QTZL',
   'user', 'dbo', 'table', 'GD_TC_SGXKZ_QTZL'
go
-----------------------------------------------------------------------

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.GD_TC_SGXKZ_BZJQR')
            and   type = 'U')
   drop table dbo.GD_TC_SGXKZ_BZJQR
go

/*==============================================================*/
/* Table: GD_TC_SGXKZ_BZJQR                                     */
/*==============================================================*/
create table dbo.GD_TC_SGXKZ_BZJQR (
   FId                  varchar(36)          collate Chinese_PRC_CI_AS not null,
   FAppId               varchar(36)          collate Chinese_PRC_CI_AS not null,
   FPrjItemId           varchar(36)          collate Chinese_PRC_CI_AS null,
   FPrjId               varchar(36)          collate Chinese_PRC_CI_AS null,
   JFXM                 varchar(200)         collate Chinese_PRC_CI_AS null,
   JFXMBM               varchar(50)          collate Chinese_PRC_CI_AS null,
   Money                decimal(15,2)        null,
   JFSJ                 date                 null,
   SKJBR                varchar(30)          collate Chinese_PRC_CI_AS null,
   SKDW                 varchar(100)         collate Chinese_PRC_CI_AS null,
   constraint PK__GD_TC_SGXKZ__C1BEAA4255D59338 primary key (FId)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   '保证金确认TC_SGXKZ_BZJQR',
   'user', 'dbo', 'table', 'GD_TC_SGXKZ_BZJQR'
go







