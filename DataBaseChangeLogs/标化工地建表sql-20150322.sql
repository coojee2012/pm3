if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.TC_BZGD_PrjInfo')
            and   type = 'U')
   drop table dbo.TC_BZGD_PrjInfo
go

/*==============================================================*/
/* Table: TC_BZGD_PrjInfo                                       */
/*==============================================================*/
create table dbo.TC_BZGD_PrjInfo (
   FId                  varchar(36)          collate Chinese_PRC_CI_AS not null,
   FAppId               varchar(36)          collate Chinese_PRC_CI_AS not null,
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
   constraint PK__TC_BZGD__C1BEAA4218427513 primary key (FId)
         on "PRIMARY"
)
on "PRIMARY"
go


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.TC_BZGD_Record') and o.name = 'FK_TC_BZGD__REFERENCE_TC_PRJ_I')
alter table dbo.TC_BZGD_Record
   drop constraint FK_TC_BZGD__REFERENCE_TC_PRJ_I
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.TC_BZGD_Record')
            and   type = 'U')
   drop table dbo.TC_BZGD_Record
go

/*==============================================================*/
/* Table: TC_BZGD_Record                                        */
/*==============================================================*/
create table dbo.TC_BZGD_Record (
   FId                  varchar(36)          collate Chinese_PRC_CI_AS not null,
   FAppId               varchar(36)          null,
   FPrjId               varchar(36)          collate Chinese_PRC_CI_AS not null,
   FPrjItemId           varchar(36)          collate Chinese_PRC_CI_AS not null,
   ProjectName          varchar(200)         collate Chinese_PRC_CI_AS not null,
   Province             int                  null,
   City                 int                  null,
   County               int                  null,
   IsForeign            bit                  null,
   Address              varchar(200)         collate Chinese_PRC_CI_AS null,
   ProjectType          varchar(20)          collate Chinese_PRC_CI_AS null,
   Area                 decimal(15,2)        null,
   Investment           decimal(15,2)        null,
   WPZSGD               bit                  null,
   SGDWId               varchar(50)          collate Chinese_PRC_CI_AS null,
   SGDW                 varchar(200)         collate Chinese_PRC_CI_AS null,
   SGDWDZ               varchar(200)         collate Chinese_PRC_CI_AS null,
   SGDWDH               varchar(50)          collate Chinese_PRC_CI_AS null,
   SGDWFR               varchar(50)          collate Chinese_PRC_CI_AS null,
   JSDWID               varchar(50)          null,
   JSDW                 varchar(200)         collate Chinese_PRC_CI_AS null,
   JSDWDZ               varchar(200)         null,
   JSDWDH               varchar(200)         null,
   JLDWId               varchar(50)          collate Chinese_PRC_CI_AS null,
   JLDW                 varchar(200)         collate Chinese_PRC_CI_AS null,
   JLDWDZ               varchar(200)         collate Chinese_PRC_CI_AS null,
   JLDWDH               varchar(50)          collate Chinese_PRC_CI_AS null,
   SGXKZBH              varchar(50)          null,
   SGDWZS               varchar(50)          collate Chinese_PRC_CI_AS null,
   SGDWAQSCXKZ          varchar(50)          null,
   SGSCYJPJSC           varchar(50)          null,
   XMJLAQSCLLKHZS       varchar(200)         null,
   AQYAQSCLLKH          varchar(200)         null,
   KGTJSHQK             varchar(500)         null,
   MQGCJD               varchar(200)         null,
   GCZJ                 decimal(10,2)        null,
   ConstrType           varchar(20)          collate Chinese_PRC_CI_AS null,
   XMJL                 varchar(50)          collate Chinese_PRC_CI_AS null,
   GCAQDJ               varchar(50)          null,
   StartDate            date                 null,
   EndDate              date                 null,
   ZDZLSG               bit                  null,
   ZDAQSG               bit                  null,
   SBDWID               varchar(100)         null,
   SBDWMC               varchar(100)         null,
   SBSJ                 datetime             null,
   FLinkMan             Varchar(50)	         null,
   FTel                 Varchar(50)	         null,
   FAddres              Varchar(100)	        null,
   FPost                Varchar(50)	         null,
   JDBM                 varchar(50)          null,
   JDLinkMan            Varchar(50)	         null,
   LJDTel               Varchar(50)	         null,
   SJAQWMSGGDJH         ntext                null,
   GCJLDWYJ             varchar(500)         null,
   JLDWMC               Varchar(50)	         null,
   JLDWTBYJSJ           datetime             null,
   JSDWYJ               varchar(500)         null,
   JSDWMC               Varchar(50)          null,
   JSDWSJ               datetime             null,
   XZZGBMYJ             varchar(500)         null,
   XZZGBMMC             Varchar(100)         null,
   XZZGBMSJ             datetime             null,
   ReMark               varchar(500)         null,
   constraint PK__TC_BZGC_Re__C1BEAA424FC7B427 primary key (FId)
         on "PRIMARY"
)
on "PRIMARY"
go

execute sp_addextendedproperty 'MS_Description', 
   '±£´æ FAppId',
   'user', 'dbo', 'table', 'TC_BZGD_Record', 'column', 'FId'
go

alter table dbo.TC_BZGD_Record
   add constraint FK_TC_BZGD__REFERENCE_TC_PRJ_I foreign key (FPrjId)
      references dbo.TC_Prj_Info (FId)
go
