/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     02/03/2018 21:22:34                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('APPLICATION') and o.name = 'FK_APPLICAT_RELATIONS_WEBSERVE')
alter table APPLICATION
   drop constraint FK_APPLICAT_RELATIONS_WEBSERVE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IISWEBSITE') and o.name = 'FK_IISWEBSI_RELATIONS_APPLICAT')
alter table IISWEBSITE
   drop constraint FK_IISWEBSI_RELATIONS_APPLICAT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IISWEBSITESOAPSERVICE') and o.name = 'FK_IISWEBSI_RELATIONS_IISWEBSI')
alter table IISWEBSITESOAPSERVICE
   drop constraint FK_IISWEBSI_RELATIONS_IISWEBSI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IISWEBSITESOAPSERVICE') and o.name = 'FK_IISWEBSI_RELATIONS_SOAPSERV')
alter table IISWEBSITESOAPSERVICE
   drop constraint FK_IISWEBSI_RELATIONS_SOAPSERV
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LOGENTRY') and o.name = 'FK_LOGENTRY_RELATIONS_IISWEBSI')
alter table LOGENTRY
   drop constraint FK_LOGENTRY_RELATIONS_IISWEBSI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SOAPENDPOINT') and o.name = 'FK_SOAPENDP_RELATIONS_SOAPSERV')
alter table SOAPENDPOINT
   drop constraint FK_SOAPENDP_RELATIONS_SOAPSERV
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('APPLICATION')
            and   name  = 'RELATIONSHIP_2_FK'
            and   indid > 0
            and   indid < 255)
   drop index APPLICATION.RELATIONSHIP_2_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('APPLICATION')
            and   type = 'U')
   drop table APPLICATION
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('IISWEBSITE')
            and   name  = 'RELATIONSHIP_1_FK'
            and   indid > 0
            and   indid < 255)
   drop index IISWEBSITE.RELATIONSHIP_1_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('IISWEBSITE')
            and   type = 'U')
   drop table IISWEBSITE
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('IISWEBSITESOAPSERVICE')
            and   name  = 'RELATIONSHIP_5_FK'
            and   indid > 0
            and   indid < 255)
   drop index IISWEBSITESOAPSERVICE.RELATIONSHIP_5_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('IISWEBSITESOAPSERVICE')
            and   name  = 'RELATIONSHIP_4_FK'
            and   indid > 0
            and   indid < 255)
   drop index IISWEBSITESOAPSERVICE.RELATIONSHIP_4_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('IISWEBSITESOAPSERVICE')
            and   type = 'U')
   drop table IISWEBSITESOAPSERVICE
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('LOGENTRY')
            and   name  = 'RELATIONSHIP_7_FK'
            and   indid > 0
            and   indid < 255)
   drop index LOGENTRY.RELATIONSHIP_7_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('LOGENTRY')
            and   type = 'U')
   drop table LOGENTRY
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('SOAPENDPOINT')
            and   name  = 'RELATIONSHIP_6_FK'
            and   indid > 0
            and   indid < 255)
   drop index SOAPENDPOINT.RELATIONSHIP_6_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SOAPENDPOINT')
            and   type = 'U')
   drop table SOAPENDPOINT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SOAPSERVICE')
            and   type = 'U')
   drop table SOAPSERVICE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WEBSERVER')
            and   type = 'U')
   drop table WEBSERVER
go

/*==============================================================*/
/* Table: APPLICATION                                           */
/*==============================================================*/
create table APPLICATION (
   IDAPPLICATION        int                  identity,
   IDWEBSERVER          int                  null,
   NAMEAPPLICATION      varchar(99)          not null,
   INITIALSAPPLICATION  varchar(5)           null,
   constraint PK_APPLICATION primary key nonclustered (IDAPPLICATION)
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_2_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_2_FK on APPLICATION (
IDWEBSERVER ASC
)
go

/*==============================================================*/
/* Table: IISWEBSITE                                            */
/*==============================================================*/
create table IISWEBSITE (
   IDIISWEBSITE         int                  identity,
   IDAPPLICATION        int                  null,
   NAMEWEBSITE          varchar(99)          not null,
   APPLOGPATH           varchar(256)         null,
   constraint PK_IISWEBSITE primary key nonclustered (IDIISWEBSITE)
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_1_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_1_FK on IISWEBSITE (
IDAPPLICATION ASC
)
go

/*==============================================================*/
/* Table: IISWEBSITESOAPSERVICE                                 */
/*==============================================================*/
create table IISWEBSITESOAPSERVICE (
   IISWEBSITESOAPSERVICE int                  not null,
   IDSOAPSERVICE        int                  not null,
   IDIISWEBSITE         int                  not null,
   constraint PK_IISWEBSITESOAPSERVICE primary key nonclustered (IISWEBSITESOAPSERVICE)
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_4_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_4_FK on IISWEBSITESOAPSERVICE (
IDIISWEBSITE ASC
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_5_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_5_FK on IISWEBSITESOAPSERVICE (
IDSOAPSERVICE ASC
)
go

/*==============================================================*/
/* Table: LOGENTRY                                              */
/*==============================================================*/
create table LOGENTRY (
   IDLOGENTRY           int                  not null,
   IDIISWEBSITE         int                  null,
   LOGTYPE              smallint             null,
   URLPATH              varchar(256)         null,
   METHOD               varchar(2000)        null,
   MESSAGE              varchar(2000)        null,
   HASH                 varchar(255)         null,
   APPLOGID             int                  not null,
   constraint PK_LOGENTRY primary key nonclustered (IDLOGENTRY)
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_7_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_7_FK on LOGENTRY (
IDIISWEBSITE ASC
)
go

/*==============================================================*/
/* Table: SOAPENDPOINT                                          */
/*==============================================================*/
create table SOAPENDPOINT (
   IDSOAPENDPOINT       int                  identity,
   URL                  varchar(2000)        null,
   IDSOAPSERVICE        int                  null,
   constraint PK_SOAPENDPOINT primary key nonclustered (IDSOAPENDPOINT)
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_6_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_6_FK on SOAPENDPOINT (
IDSOAPSERVICE ASC
)
go

/*==============================================================*/
/* Table: SOAPSERVICE                                           */
/*==============================================================*/
create table SOAPSERVICE (
   IDSOAPSERVICE        int                  identity,
   NAMESOAPSERVICE      varchar(99)          not null,
   VERSIONNUMBER        float(2)             null,
   constraint PK_SOAPSERVICE primary key nonclustered (IDSOAPSERVICE)
)
go

/*==============================================================*/
/* Table: WEBSERVER                                             */
/*==============================================================*/
create table WEBSERVER (
   IDWEBSERVER          int                  identity,
   NAMEWEBSERVER        varchar(255)         null,
   constraint PK_WEBSERVER primary key nonclustered (IDWEBSERVER)
)
go

alter table APPLICATION
   add constraint FK_APPLICAT_RELATIONS_WEBSERVE foreign key (IDWEBSERVER)
      references WEBSERVER (IDWEBSERVER)
go

alter table IISWEBSITE
   add constraint FK_IISWEBSI_RELATIONS_APPLICAT foreign key (IDAPPLICATION)
      references APPLICATION (IDAPPLICATION)
go

alter table IISWEBSITESOAPSERVICE
   add constraint FK_IISWEBSI_RELATIONS_IISWEBSI foreign key (IDIISWEBSITE)
      references IISWEBSITE (IDIISWEBSITE)
go

alter table IISWEBSITESOAPSERVICE
   add constraint FK_IISWEBSI_RELATIONS_SOAPSERV foreign key (IDSOAPSERVICE)
      references SOAPSERVICE (IDSOAPSERVICE)
go

alter table LOGENTRY
   add constraint FK_LOGENTRY_RELATIONS_IISWEBSI foreign key (IDIISWEBSITE)
      references IISWEBSITE (IDIISWEBSITE)
go

alter table SOAPENDPOINT
   add constraint FK_SOAPENDP_RELATIONS_SOAPSERV foreign key (IDSOAPSERVICE)
      references SOAPSERVICE (IDSOAPSERVICE)
go

