/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     04/03/2018 15:38:03                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('APPLICATION') and o.name = 'FK_APPLICAT_RELATIONS_WEBSERVE')
alter table APPLICATION
   drop constraint FK_APPLICAT_RELATIONS_WEBSERVE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IISAPPLICATION') and o.name = 'FK_IISAPPLI_RELATIONS_APPLICAT')
alter table IISAPPLICATION
   drop constraint FK_IISAPPLI_RELATIONS_APPLICAT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IISAPPLICATION') and o.name = 'FK_IISAPPLI_RELATIONS_IISWEBSI')
alter table IISAPPLICATION
   drop constraint FK_IISAPPLI_RELATIONS_IISWEBSI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IISAPPLICATIONSOAPSERVICE') and o.name = 'FK_IISAPPLI_RELATIONS_SOAPSERV')
alter table IISAPPLICATIONSOAPSERVICE
   drop constraint FK_IISAPPLI_RELATIONS_SOAPSERV
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('IISAPPLICATIONSOAPSERVICE') and o.name = 'FK_IISAPPLI_RELATIONS_IISAPPLI')
alter table IISAPPLICATIONSOAPSERVICE
   drop constraint FK_IISAPPLI_RELATIONS_IISAPPLI
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('LOGENTRY') and o.name = 'FK_LOGENTRY_RELATIONS_IISAPPLI')
alter table LOGENTRY
   drop constraint FK_LOGENTRY_RELATIONS_IISAPPLI
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
           where  id    = object_id('IISAPPLICATION')
            and   name  = 'RELATIONSHIP_10_FK'
            and   indid > 0
            and   indid < 255)
   drop index IISAPPLICATION.RELATIONSHIP_10_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('IISAPPLICATION')
            and   name  = 'RELATIONSHIP_7_FK'
            and   indid > 0
            and   indid < 255)
   drop index IISAPPLICATION.RELATIONSHIP_7_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('IISAPPLICATION')
            and   type = 'U')
   drop table IISAPPLICATION
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('IISAPPLICATIONSOAPSERVICE')
            and   name  = 'RELATIONSHIP_13_FK'
            and   indid > 0
            and   indid < 255)
   drop index IISAPPLICATIONSOAPSERVICE.RELATIONSHIP_13_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('IISAPPLICATIONSOAPSERVICE')
            and   name  = 'RELATIONSHIP_12_FK'
            and   indid > 0
            and   indid < 255)
   drop index IISAPPLICATIONSOAPSERVICE.RELATIONSHIP_12_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('IISAPPLICATIONSOAPSERVICE')
            and   type = 'U')
   drop table IISAPPLICATIONSOAPSERVICE
go

if exists (select 1
            from  sysobjects
           where  id = object_id('IISWEBSITE')
            and   type = 'U')
   drop table IISWEBSITE
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('LOGENTRY')
            and   name  = 'RELATIONSHIP_9_FK'
            and   indid > 0
            and   indid < 255)
   drop index LOGENTRY.RELATIONSHIP_9_FK
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
   CREATIONDATE         datetime             null,
   ENDDATE              datetime             null,
   constraint PK_APPLICATION primary key nonclustered (IDAPPLICATION)
)
go

if exists (select 1 from  sys.extended_properties
           where major_id = object_id('APPLICATION') and minor_id = 0)
begin 
   declare @CurrentUser sysname 
select @CurrentUser = user_name() 
execute sp_dropextendedproperty 'MS_Description',  
   'user', @CurrentUser, 'table', 'APPLICATION' 
 
end 


select @CurrentUser = user_name() 
execute sp_addextendedproperty 'MS_Description',  
   'Representa uma aplicação. Equivalente a um sistema no GASC
   ', 
   'user', @CurrentUser, 'table', 'APPLICATION'
go

/*==============================================================*/
/* Index: RELATIONSHIP_2_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_2_FK on APPLICATION (
IDWEBSERVER ASC
)
go

/*==============================================================*/
/* Table: IISAPPLICATION                                        */
/*==============================================================*/
create table IISAPPLICATION (
   IDIISAPPLICATION     int                  identity,
   IDIISWEBSITE         int                  null,
   IDAPPLICATION        int                  null,
   APPLOGPATH           varchar(256)         null,
   PHYSICALPATH         varchar(1024)        null,
   LOGICALPATH          varchar(1024)        null,
   APPPOLLNAME          varchar(1024)        null,
   IISLOGPATH           varchar(256)         null,
   CREATIONDATE         datetime             null,
   ENDDATE              datetime             null,
   constraint PK_IISAPPLICATION primary key nonclustered (IDIISAPPLICATION)
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_7_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_7_FK on IISAPPLICATION (
IDIISWEBSITE ASC
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_10_FK                                    */
/*==============================================================*/
create index RELATIONSHIP_10_FK on IISAPPLICATION (
IDAPPLICATION ASC
)
go

/*==============================================================*/
/* Table: IISAPPLICATIONSOAPSERVICE                             */
/*==============================================================*/
create table IISAPPLICATIONSOAPSERVICE (
   IDIISAPPLICATIONSOAPSERVICE int                  identity,
   IDIISAPPLICATION     int                  not null,
   IDSOAPSERVICE        int                  not null,
   constraint PK_IISAPPLICATIONSOAPSERVICE primary key nonclustered (IDIISAPPLICATIONSOAPSERVICE)
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_12_FK                                    */
/*==============================================================*/
create index RELATIONSHIP_12_FK on IISAPPLICATIONSOAPSERVICE (
IDSOAPSERVICE ASC
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_13_FK                                    */
/*==============================================================*/
create index RELATIONSHIP_13_FK on IISAPPLICATIONSOAPSERVICE (
IDIISAPPLICATION ASC
)
go

/*==============================================================*/
/* Table: IISWEBSITE                                            */
/*==============================================================*/
create table IISWEBSITE (
   IDIISWEBSITE         int                  identity,
   NAMEWEBSITE          varchar(99)          not null,
   APPPOLLNAME          varchar(256)         null,
   ADRESSWEBSITE        varchar(256)         null,
   IISLOGPATH           varchar(256)         null,
   ALIASIISWEBSITE      varchar(256)         null,
   CREATIONDATE         datetime             null,
   ENDDATE              datetime             null,
   constraint PK_IISWEBSITE primary key nonclustered (IDIISWEBSITE)
)
go

/*==============================================================*/
/* Table: LOGENTRY                                              */
/*==============================================================*/
create table LOGENTRY (
   IDLOGENTRY           int                  identity,
   LOGTYPE              smallint             null,
   IDIISAPPLICATION     int                  null,
   URLPATH              varchar(256)         null,
   METHOD               varchar(2000)        null,
   MESSAGE              varchar(2000)        null,
   HASH                 varchar(255)         null,
   APPLOGID             int                  not null,
   CREATIONDATE         datetime             null,
   ENDDATE              datetime             null,
   constraint PK_LOGENTRY primary key nonclustered (IDLOGENTRY)
)
go

/*==============================================================*/
/* Index: RELATIONSHIP_9_FK                                     */
/*==============================================================*/
create index RELATIONSHIP_9_FK on LOGENTRY (
IDIISAPPLICATION ASC
)
go

/*==============================================================*/
/* Table: SOAPENDPOINT                                          */
/*==============================================================*/
create table SOAPENDPOINT (
   IDSOAPENDPOINT       int                  identity,
   URL                  varchar(2000)        null,
   IDSOAPSERVICE        int                  null,
   CREATIONDATE         datetime             null,
   ENDDATE              datetime             null,
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
   CREATIONDATE         datetime             null,
   ENDDATE              datetime             null,
   constraint PK_SOAPSERVICE primary key nonclustered (IDSOAPSERVICE)
)
go

/*==============================================================*/
/* Table: WEBSERVER                                             */
/*==============================================================*/
create table WEBSERVER (
   IDWEBSERVER          int                  identity,
   NAMEWEBSERVER        varchar(255)         null,
   CREATIONDATE         datetime             null,
   ENDDATE              datetime             null,
   constraint PK_WEBSERVER primary key nonclustered (IDWEBSERVER)
)
go

alter table APPLICATION
   add constraint FK_APPLICAT_RELATIONS_WEBSERVE foreign key (IDWEBSERVER)
      references WEBSERVER (IDWEBSERVER)
go

alter table IISAPPLICATION
   add constraint FK_IISAPPLI_RELATIONS_APPLICAT foreign key (IDAPPLICATION)
      references APPLICATION (IDAPPLICATION)
go

alter table IISAPPLICATION
   add constraint FK_IISAPPLI_RELATIONS_IISWEBSI foreign key (IDIISWEBSITE)
      references IISWEBSITE (IDIISWEBSITE)
go

alter table IISAPPLICATIONSOAPSERVICE
   add constraint FK_IISAPPLI_RELATIONS_SOAPSERV foreign key (IDSOAPSERVICE)
      references SOAPSERVICE (IDSOAPSERVICE)
go

alter table IISAPPLICATIONSOAPSERVICE
   add constraint FK_IISAPPLI_RELATIONS_IISAPPLI foreign key (IDIISAPPLICATION)
      references IISAPPLICATION (IDIISAPPLICATION)
go

alter table LOGENTRY
   add constraint FK_LOGENTRY_RELATIONS_IISAPPLI foreign key (IDIISAPPLICATION)
      references IISAPPLICATION (IDIISAPPLICATION)
go

alter table SOAPENDPOINT
   add constraint FK_SOAPENDP_RELATIONS_SOAPSERV foreign key (IDSOAPSERVICE)
      references SOAPSERVICE (IDSOAPSERVICE)
go

