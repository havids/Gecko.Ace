use [Gecko17]
go

/*==============================================================*/
/* Table: PB_DEPARTMENT                                         */
/*==============================================================*/
create table dbo.PB_DEPARTMENT (
   PB_ID                varchar(15)          not null,
   PB_PARENT_ID         varchar(15)          null,
   PB_NAME              varchar(40)          not null,
   PB_PHONE             varchar(40)          null,
   PB_EXT_NUMBER        varchar(20)          null,
   PB_FAX               varchar(40)          null,
   PB_REMARK            varchar(200)         null,
   PB_ORDER_ID          int                  not null,
   constraint PK_PB_DEPARTMENT primary key nonclustered (PB_ID)
)
go

execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'PB_DEPARTMENT'
go

execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', 'dbo', 'table', 'PB_DEPARTMENT', 'column', 'PB_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '�ϼ�����ID',
   'user', 'dbo', 'table', 'PB_DEPARTMENT', 'column', 'PB_PARENT_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'PB_DEPARTMENT', 'column', 'PB_NAME'
go

execute sp_addextendedproperty 'MS_Description', 
   '�绰',
   'user', 'dbo', 'table', 'PB_DEPARTMENT', 'column', 'PB_PHONE'
go

execute sp_addextendedproperty 'MS_Description', 
   '�ֻ�����',
   'user', 'dbo', 'table', 'PB_DEPARTMENT', 'column', 'PB_EXT_NUMBER'
go

execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'PB_DEPARTMENT', 'column', 'PB_FAX'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', 'dbo', 'table', 'PB_DEPARTMENT', 'column', 'PB_REMARK'
go

execute sp_addextendedproperty 'MS_Description', 
   '����ID',
   'user', 'dbo', 'table', 'PB_DEPARTMENT', 'column', 'PB_ORDER_ID'
go

/*==============================================================*/
/* Index: I_PB_DEPARTMENT_ORDER_ID                              */
/*==============================================================*/
create clustered index I_PB_DEPARTMENT_ORDER_ID on dbo.PB_DEPARTMENT (
PB_PARENT_ID ASC,
PB_ORDER_ID ASC
)
go

/*==============================================================*/
/* Table: PB_MODULE                                             */
/*==============================================================*/
create table dbo.PB_MODULE (
   PB_ID                varchar(15)          not null,
   PB_MODULE_TYPE_ID    varchar(15)          not null,
   PB_TAG               varchar(40)          not null,
   PB_NAME              varchar(40)          not null,
   PB_MODULE_URL        varchar(200)         null,
   PB_REMARK            varchar(200)         null,
   PB_DISABLED          int                  not null,
   PB_ORDER_ID          int                  not null,
   constraint PK_PB_MODULE primary key nonclustered (PB_ID)
)
go

execute sp_addextendedproperty 'MS_Description', 
   'ģ��',
   'user', 'dbo', 'table', 'PB_MODULE'
go

execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', 'dbo', 'table', 'PB_MODULE', 'column', 'PB_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   'ģ�����ID',
   'user', 'dbo', 'table', 'PB_MODULE', 'column', 'PB_MODULE_TYPE_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ʾ',
   'user', 'dbo', 'table', 'PB_MODULE', 'column', 'PB_TAG'
go

execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'PB_MODULE', 'column', 'PB_NAME'
go

execute sp_addextendedproperty 'MS_Description', 
   'ģ���ַ',
   'user', 'dbo', 'table', 'PB_MODULE', 'column', 'PB_MODULE_URL'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', 'dbo', 'table', 'PB_MODULE', 'column', 'PB_REMARK'
go

execute sp_addextendedproperty 'MS_Description', 
   '�ѽ���',
   'user', 'dbo', 'table', 'PB_MODULE', 'column', 'PB_DISABLED'
go

execute sp_addextendedproperty 'MS_Description', 
   '����ID',
   'user', 'dbo', 'table', 'PB_MODULE', 'column', 'PB_ORDER_ID'
go

INSERT INTO dbo.PB_MODULE (PB_ID, PB_MODULE_TYPE_ID, PB_TAG, PB_NAME, PB_MODULE_URL, PB_DISABLED, PB_ORDER_ID) VALUES ('0000000000', '0000000000', 'SysCodeMgr', '�������', '../Modules/SysCodeMgr/Default.aspx', 0, 10)
INSERT INTO dbo.PB_MODULE (PB_ID, PB_MODULE_TYPE_ID, PB_TAG, PB_NAME, PB_MODULE_URL, PB_DISABLED, PB_ORDER_ID) VALUES ('0000000001', '0000000000', 'ModuleMgr', 'ģ�����', '../Modules/ModuleMgr/Default.aspx', 0, 20)
INSERT INTO dbo.PB_MODULE (PB_ID, PB_MODULE_TYPE_ID, PB_TAG, PB_NAME, PB_MODULE_URL, PB_DISABLED, PB_ORDER_ID) VALUES ('0000000002', '0000000001', 'RoleMgr', '��ɫ����', '../Modules/RoleMgr/Default.aspx', 0, 10)
INSERT INTO dbo.PB_MODULE (PB_ID, PB_MODULE_TYPE_ID, PB_TAG, PB_NAME, PB_MODULE_URL, PB_DISABLED, PB_ORDER_ID) VALUES ('0000000003', '0000000001', 'DepartmentMgr', '���Ź���', '../Modules/DepartmentMgr/Default.aspx', 0, 20)
INSERT INTO dbo.PB_MODULE (PB_ID, PB_MODULE_TYPE_ID, PB_TAG, PB_NAME, PB_MODULE_URL, PB_DISABLED, PB_ORDER_ID) VALUES ('0000000004', '0000000001', 'StaffMgr', 'ְԱ����', '../Modules/StaffMgr/Default.aspx', 0, 30)
INSERT INTO dbo.PB_MODULE (PB_ID, PB_MODULE_TYPE_ID, PB_TAG, PB_NAME, PB_MODULE_URL, PB_DISABLED, PB_ORDER_ID) VALUES ('0000000005', '0000000002', 'ChangeMyPwd', '�޸�����', '../Modules/ChangeMyPwd/Default.aspx', 0, 10)
--INSERT INTO dbo.PB_MODULE (PB_ID, PB_MODULE_TYPE_ID, PB_TAG, PB_NAME, PB_MODULE_URL, PB_DISABLED, PB_ORDER_ID) VALUES ('0000000006', '0000000003', 'UserMgr', '�û���Ϣ����', '../Modules/UserMgr/Default.aspx', 0, 10)
go

/*==============================================================*/
/* Index: I_PB_MODULE_ORDER_ID                                  */
/*==============================================================*/
create clustered index I_PB_MODULE_ORDER_ID on dbo.PB_MODULE (
PB_MODULE_TYPE_ID ASC,
PB_ORDER_ID ASC
)
go

/*==============================================================*/
/* Index: I_PB_MODULE_TAG                                       */
/*==============================================================*/
create unique index I_PB_MODULE_TAG on dbo.PB_MODULE (
PB_TAG ASC
)
go

/*==============================================================*/
/* Table: PB_MODULE_RIGHT                                       */
/*==============================================================*/
create table dbo.PB_MODULE_RIGHT (
   PB_ID                varchar(15)          not null,
   PB_MODULE_ID         varchar(15)          not null,
   PB_RIGHT_TAG         varchar(40)          not null,
   constraint PK_PB_MODULE_RIGHT primary key nonclustered (PB_ID)
)
go

execute sp_addextendedproperty 'MS_Description', 
   'ģ��Ȩ��',
   'user', 'dbo', 'table', 'PB_MODULE_RIGHT'
go

execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', 'dbo', 'table', 'PB_MODULE_RIGHT', 'column', 'PB_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   'ģ��ID',
   'user', 'dbo', 'table', 'PB_MODULE_RIGHT', 'column', 'PB_MODULE_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   'Ȩ�ޱ�ʾ',
   'user', 'dbo', 'table', 'PB_MODULE_RIGHT', 'column', 'PB_RIGHT_TAG'
go

INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000000', '0000000000', 'rights_browse')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000001', '0000000000', 'rights_add')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000002', '0000000000', 'rights_edit')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000003', '0000000000', 'rights_delete')

INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000004', '0000000001', 'rights_browse')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000005', '0000000001', 'rights_add')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000006', '0000000001', 'rights_edit')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000007', '0000000001', 'rights_delete')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000008', '0000000001', 'rights_move')

INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000009', '0000000002', 'rights_browse')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000010', '0000000002', 'rights_add')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000011', '0000000002', 'rights_edit')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000012', '0000000002', 'rights_delete')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000013', '0000000002', 'rights_move')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000014', '0000000002', 'rights_accredit')

INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000015', '0000000003', 'rights_browse')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000016', '0000000003', 'rights_add')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000017', '0000000003', 'rights_edit')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000018', '0000000003', 'rights_delete')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000019', '0000000003', 'rights_move')

INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000020', '0000000004', 'rights_browse')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000021', '0000000004', 'rights_add')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000022', '0000000004', 'rights_edit')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000023', '0000000004', 'rights_delete')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000024', '0000000004', 'rights_move')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000025', '0000000004', 'rights_accredit')

INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000026', '0000000005', 'rights_browse')

INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000027', '0000000006', 'rights_browse')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000028', '0000000006', 'rights_add')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000029', '0000000006', 'rights_edit')
INSERT INTO dbo.PB_MODULE_RIGHT (PB_ID, PB_MODULE_ID, PB_RIGHT_TAG) VALUES ('0000000030', '0000000006', 'rights_delete')
go

/*==============================================================*/
/* Index: I_PB_MODULE_RIGHT_MODULE_ID                           */
/*==============================================================*/
create unique clustered index I_PB_MODULE_RIGHT_MODULE_ID on dbo.PB_MODULE_RIGHT (
PB_MODULE_ID ASC,
PB_RIGHT_TAG ASC
)
go

/*==============================================================*/
/* Table: PB_MODULE_TYPE                                        */
/*==============================================================*/
create table dbo.PB_MODULE_TYPE (
   PB_ID                varchar(15)          not null,
   PB_PARENT_ID         varchar(15)          null,
   PB_NAME              varchar(40)          not null,
   PB_REMARK            varchar(200)         null,
   PB_ORDER_ID          int                  not null,
   constraint PK_PB_MODULE_TYPE primary key nonclustered (PB_ID)
)
go

execute sp_addextendedproperty 'MS_Description', 
   'ģ�����',
   'user', 'dbo', 'table', 'PB_MODULE_TYPE'
go

execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', 'dbo', 'table', 'PB_MODULE_TYPE', 'column', 'PB_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ģ�����ID',
   'user', 'dbo', 'table', 'PB_MODULE_TYPE', 'column', 'PB_PARENT_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'PB_MODULE_TYPE', 'column', 'PB_NAME'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', 'dbo', 'table', 'PB_MODULE_TYPE', 'column', 'PB_REMARK'
go

execute sp_addextendedproperty 'MS_Description', 
   '����ID',
   'user', 'dbo', 'table', 'PB_MODULE_TYPE', 'column', 'PB_ORDER_ID'
go

--INSERT INTO dbo.PB_MODULE_TYPE (PB_ID, PB_NAME, PB_ORDER_ID) VALUES ('0000000000', 'ϵͳ����', 10)
INSERT INTO dbo.PB_MODULE_TYPE (PB_ID, PB_NAME, PB_ORDER_ID) VALUES ('0000000001', 'Ȩ�޹���', 20)
--INSERT INTO dbo.PB_MODULE_TYPE (PB_ID, PB_NAME, PB_ORDER_ID) VALUES ('0000000002', '������Ϣ����', 30)
--INSERT INTO dbo.PB_MODULE_TYPE (PB_ID, PB_NAME, PB_ORDER_ID) VALUES ('0000000003', 'ǰ̨�û�����', 40)
go

/*==============================================================*/
/* Index: I_PB_MODULE_TYPE_ORDER_ID                             */
/*==============================================================*/
create clustered index I_PB_MODULE_TYPE_ORDER_ID on dbo.PB_MODULE_TYPE (
PB_PARENT_ID ASC,
PB_ORDER_ID ASC
)
go

/*==============================================================*/
/* Table: PB_ROLE                                               */
/*==============================================================*/
create table dbo.PB_ROLE (
   PB_ID                varchar(15)          not null,
   PB_ROLE_TYPE_ID      varchar(15)          not null,
   PB_NAME              varchar(40)          not null,
   PB_REMARK            varchar(200)         null,
   PB_ORDER_ID          int                  not null,
   constraint PK_PB_ROLE primary key nonclustered (PB_ID)
)
go

execute sp_addextendedproperty 'MS_Description', 
   '��ɫ',
   'user', 'dbo', 'table', 'PB_ROLE'
go

execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', 'dbo', 'table', 'PB_ROLE', 'column', 'PB_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ɫ����ID',
   'user', 'dbo', 'table', 'PB_ROLE', 'column', 'PB_ROLE_TYPE_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'PB_ROLE', 'column', 'PB_NAME'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', 'dbo', 'table', 'PB_ROLE', 'column', 'PB_REMARK'
go

execute sp_addextendedproperty 'MS_Description', 
   '����ID',
   'user', 'dbo', 'table', 'PB_ROLE', 'column', 'PB_ORDER_ID'
go

INSERT INTO dbo.PB_ROLE (PB_ID, PB_ROLE_TYPE_ID, PB_NAME, PB_ORDER_ID) VALUES ('0000000000', '0000000000', '��������Ա', 10)
go

/*==============================================================*/
/* Index: I_PB_ROLE_ORDER_ID                                    */
/*==============================================================*/
create clustered index I_PB_ROLE_ORDER_ID on dbo.PB_ROLE (
PB_ROLE_TYPE_ID ASC,
PB_ORDER_ID ASC
)
go

/*==============================================================*/
/* Table: PB_ROLE_MODULE_RIGHT_DENY                             */
/*==============================================================*/
create table dbo.PB_ROLE_MODULE_RIGHT_DENY (
   PB_ROLE_ID           varchar(15)          not null,
   PB_RIGHT_ID          varchar(15)          not null,
   constraint PK_PB_ROLE_MODULE_RIGHT_DENY primary key nonclustered (PB_ROLE_ID, PB_RIGHT_ID)
)
go

execute sp_addextendedproperty 'MS_Description', 
   '��ɫģ��Ȩ�޶��ձ��񶨣�',
   'user', 'dbo', 'table', 'PB_ROLE_MODULE_RIGHT_DENY'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ɫID',
   'user', 'dbo', 'table', 'PB_ROLE_MODULE_RIGHT_DENY', 'column', 'PB_ROLE_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   'Ȩ��ID',
   'user', 'dbo', 'table', 'PB_ROLE_MODULE_RIGHT_DENY', 'column', 'PB_RIGHT_ID'
go

/*==============================================================*/
/* Table: PB_ROLE_MODULE_RIGHT_GRANT                            */
/*==============================================================*/
create table dbo.PB_ROLE_MODULE_RIGHT_GRANT (
   PB_ROLE_ID           varchar(15)          not null,
   PB_RIGHT_ID          varchar(15)          not null,
   constraint PK_PB_ROLE_MODULE_RIGHT_GRANT primary key nonclustered (PB_ROLE_ID, PB_RIGHT_ID)
)
go

execute sp_addextendedproperty 'MS_Description', 
   '��ɫģ��Ȩ�޶��ձ��϶���',
   'user', 'dbo', 'table', 'PB_ROLE_MODULE_RIGHT_GRANT'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ɫID',
   'user', 'dbo', 'table', 'PB_ROLE_MODULE_RIGHT_GRANT', 'column', 'PB_ROLE_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   'Ȩ��ID',
   'user', 'dbo', 'table', 'PB_ROLE_MODULE_RIGHT_GRANT', 'column', 'PB_RIGHT_ID'
go

INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000000')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000001')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000002')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000003')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000004')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000005')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000006')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000007')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000008')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000009')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000010')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000011')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000012')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000013')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000014')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000015')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000016')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000017')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000018')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000019')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000020')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000021')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000022')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000023')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000024')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000025')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000026')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000027')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000028')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000029')
INSERT INTO dbo.PB_ROLE_MODULE_RIGHT_GRANT (PB_ROLE_ID, PB_RIGHT_ID) VALUES ('0000000000', '0000000030')
go

/*==============================================================*/
/* Table: PB_ROLE_TYPE                                          */
/*==============================================================*/
create table dbo.PB_ROLE_TYPE (
   PB_ID                varchar(15)          not null,
   PB_PARENT_ID         varchar(15)          null,
   PB_NAME              varchar(40)          not null,
   PB_REMARK            varchar(200)         null,
   PB_ORDER_ID          int                  not null,
   constraint PK_PB_ROLE_TYPE primary key nonclustered (PB_ID)
)
go

execute sp_addextendedproperty 'MS_Description', 
   '��ɫ����',
   'user', 'dbo', 'table', 'PB_ROLE_TYPE'
go

execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', 'dbo', 'table', 'PB_ROLE_TYPE', 'column', 'PB_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '����ɫ����ID',
   'user', 'dbo', 'table', 'PB_ROLE_TYPE', 'column', 'PB_PARENT_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'PB_ROLE_TYPE', 'column', 'PB_NAME'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', 'dbo', 'table', 'PB_ROLE_TYPE', 'column', 'PB_REMARK'
go

execute sp_addextendedproperty 'MS_Description', 
   '����ID',
   'user', 'dbo', 'table', 'PB_ROLE_TYPE', 'column', 'PB_ORDER_ID'
go

INSERT INTO dbo.PB_ROLE_TYPE (PB_ID, PB_NAME, PB_ORDER_ID) VALUES ('0000000000', '����Ա��', 10)
go

/*==============================================================*/
/* Index: I_PB_ROLE_TYPE_ORDER_ID                               */
/*==============================================================*/
create clustered index I_PB_ROLE_TYPE_ORDER_ID on dbo.PB_ROLE_TYPE (
PB_PARENT_ID ASC,
PB_ORDER_ID ASC
)
go

/*==============================================================*/
/* Table: PB_SEQUENCE                                           */
/*==============================================================*/
create table dbo.PB_SEQUENCE (
   PB_TABLE_NAME        varchar(50)          not null,
   PB_NEXT_ID           int                  not null,
   constraint PK_PB_SEQUENCE primary key nonclustered (PB_TABLE_NAME)
)
go

execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'PB_SEQUENCE'
go

execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'PB_SEQUENCE', 'column', 'PB_TABLE_NAME'
go

execute sp_addextendedproperty 'MS_Description', 
   '��һ������ֵ',
   'user', 'dbo', 'table', 'PB_SEQUENCE', 'column', 'PB_NEXT_ID'
go

INSERT INTO dbo.PB_SEQUENCE VALUES ('PB_DEPARTMENT', 0)
INSERT INTO dbo.PB_SEQUENCE VALUES ('PB_MODULE', 7)
INSERT INTO dbo.PB_SEQUENCE VALUES ('PB_MODULE_TYPE', 4)
INSERT INTO dbo.PB_SEQUENCE VALUES ('PB_MODULE_RIGHT', 31)
INSERT INTO dbo.PB_SEQUENCE VALUES ('PB_ROLE', 1)
INSERT INTO dbo.PB_SEQUENCE VALUES ('PB_ROLE_TYPE', 1)
INSERT INTO dbo.PB_SEQUENCE VALUES ('PB_SYSCODE_TYPE', 7)
INSERT INTO dbo.PB_SEQUENCE VALUES ('PB_SYSCODE', 65)
go

/*==============================================================*/
/* Table: PB_STAFF                                              */
/*==============================================================*/
create table dbo.PB_STAFF (
   PB_LOGIN_ID          varchar(20)          not null,
   PB_PASSWORD          varchar(40)          not null,
   PB_DEPARTMENT_ID     varchar(15)          null,
   PB_CODE              varchar(40)          null,
   PB_NAME              varchar(40)          not null,
   PB_SEX               int                  null,
   PB_MARRIED           int                  null,
   PB_ID_CARD           varchar(18)          null,
   PB_COUNTRY_TAG       varchar(40)          null,
   PB_NATION_TAG        varchar(40)          null,
   PB_POSITION_TAG      varchar(40)          null,
   PB_TITLE_TAG         varchar(40)          null,
   PB_POLITICAL_APPEARANCE_TAG varchar(40)          null,
   PB_DEGREE_TAG        varchar(40)          null,
   PB_BIRTHDAY          datetime             null,
   PB_ENTERS_DAY        datetime             null,
   PB_LEAVES_DAY        datetime             null,
   PB_OFFICE_PHONE      varchar(40)          null,
   PB_EXT_NUMBER        varchar(20)          null,
   PB_FAMILY_PHONE      varchar(40)          null,
   PB_CELL_PHONE        varchar(40)          null,
   PB_EMAIL             varchar(100)         null,
   PB_ADDRESS           varchar(200)         null,
   PB_ZIP_CODE          varchar(20)          null,
   PB_REMARK            varchar(200)         null,
   PB_IS_INNER_USER     int                  not null,
   PB_DISABLED          int                  not null,
   PB_ORDER_ID          int                  not null,
   constraint PK_PB_STAFF primary key nonclustered (PB_LOGIN_ID)
)
go

execute sp_addextendedproperty 'MS_Description', 
   'ְԱ',
   'user', 'dbo', 'table', 'PB_STAFF'
go

execute sp_addextendedproperty 'MS_Description', 
   '��¼ID',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_LOGIN_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '��¼����',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_PASSWORD'
go

execute sp_addextendedproperty 'MS_Description', 
   '����ID',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_DEPARTMENT_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '���',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_CODE'
go

execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_NAME'
go

execute sp_addextendedproperty 'MS_Description', 
   '�Ա�',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_SEX'
go

execute sp_addextendedproperty 'MS_Description', 
   '���',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_MARRIED'
go

execute sp_addextendedproperty 'MS_Description', 
   '���֤��',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_ID_CARD'
go

execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_COUNTRY_TAG'
go

execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_NATION_TAG'
go

execute sp_addextendedproperty 'MS_Description', 
   'ְλ',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_POSITION_TAG'
go

execute sp_addextendedproperty 'MS_Description', 
   'ְ��',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_TITLE_TAG'
go

execute sp_addextendedproperty 'MS_Description', 
   '������ò',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_POLITICAL_APPEARANCE_TAG'
go

execute sp_addextendedproperty 'MS_Description', 
   '���ѧ��',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_DEGREE_TAG'
go

execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_BIRTHDAY'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ְ����',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_ENTERS_DAY'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ְ����',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_LEAVES_DAY'
go

execute sp_addextendedproperty 'MS_Description', 
   '�칫�绰',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_OFFICE_PHONE'
go

execute sp_addextendedproperty 'MS_Description', 
   '�ֻ�����',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_EXT_NUMBER'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ͥ�绰',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_FAMILY_PHONE'
go

execute sp_addextendedproperty 'MS_Description', 
   '�ֻ�',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_CELL_PHONE'
go

execute sp_addextendedproperty 'MS_Description', 
   'Email',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_EMAIL'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ͥסַ',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_ADDRESS'
go

execute sp_addextendedproperty 'MS_Description', 
   '�ʱ�',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_ZIP_CODE'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_REMARK'
go

execute sp_addextendedproperty 'MS_Description', 
   '�Ƿ������û�',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_IS_INNER_USER'
go

execute sp_addextendedproperty 'MS_Description', 
   '�ѽ���',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_DISABLED'
go

execute sp_addextendedproperty 'MS_Description', 
   '����ID',
   'user', 'dbo', 'table', 'PB_STAFF', 'column', 'PB_ORDER_ID'
go

INSERT INTO dbo.PB_STAFF (PB_LOGIN_ID, PB_PASSWORD, PB_NAME, PB_IS_INNER_USER, PB_DISABLED, PB_ORDER_ID) VALUES ('admin', '7c4a8d09ca3762af61e59520943dc26494f8941b', 'admin', 1, 0, 10)
INSERT INTO dbo.PB_STAFF (PB_LOGIN_ID, PB_PASSWORD, PB_NAME, PB_IS_INNER_USER, PB_DISABLED, PB_ORDER_ID) VALUES ('sa', '7c4a8d09ca3762af61e59520943dc26494f8941b', 'sa', 1, 0, 20)
go

/*==============================================================*/
/* Index: I_PB_STAFF_ORDER_ID                                   */
/*==============================================================*/
create clustered index I_PB_STAFF_ORDER_ID on dbo.PB_STAFF (
PB_DEPARTMENT_ID ASC,
PB_DISABLED ASC,
PB_ORDER_ID ASC
)
go

/*==============================================================*/
/* Table: PB_STAFF_MODULE_RIGHT_DENY                            */
/*==============================================================*/
create table dbo.PB_STAFF_MODULE_RIGHT_DENY (
   PB_LOGIN_ID          varchar(20)          not null,
   PB_RIGHT_ID          varchar(15)          not null,
   constraint PK_PB_STAFF_MODULE_RIGHT_DENY primary key nonclustered (PB_LOGIN_ID, PB_RIGHT_ID)
)
go

execute sp_addextendedproperty 'MS_Description', 
   'ְԱģ��Ȩ�޶��ձ��񶨣�',
   'user', 'dbo', 'table', 'PB_STAFF_MODULE_RIGHT_DENY'
go

execute sp_addextendedproperty 'MS_Description', 
   '��¼ID',
   'user', 'dbo', 'table', 'PB_STAFF_MODULE_RIGHT_DENY', 'column', 'PB_LOGIN_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   'Ȩ��ID',
   'user', 'dbo', 'table', 'PB_STAFF_MODULE_RIGHT_DENY', 'column', 'PB_RIGHT_ID'
go

/*==============================================================*/
/* Table: PB_STAFF_MODULE_RIGHT_GRANT                           */
/*==============================================================*/
create table dbo.PB_STAFF_MODULE_RIGHT_GRANT (
   PB_LOGIN_ID          varchar(20)          not null,
   PB_RIGHT_ID          varchar(15)          not null,
   constraint PK_PB_STAFF_MODULE_RIGHT_GRANT primary key nonclustered (PB_LOGIN_ID, PB_RIGHT_ID)
)
go

execute sp_addextendedproperty 'MS_Description', 
   'ְԱģ��Ȩ�޶��ձ��϶���',
   'user', 'dbo', 'table', 'PB_STAFF_MODULE_RIGHT_GRANT'
go

execute sp_addextendedproperty 'MS_Description', 
   '��¼ID',
   'user', 'dbo', 'table', 'PB_STAFF_MODULE_RIGHT_GRANT', 'column', 'PB_LOGIN_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   'Ȩ��ID',
   'user', 'dbo', 'table', 'PB_STAFF_MODULE_RIGHT_GRANT', 'column', 'PB_RIGHT_ID'
go

/*==============================================================*/
/* Table: PB_STAFF_ROLE                                         */
/*==============================================================*/
create table dbo.PB_STAFF_ROLE (
   PB_LOGIN_ID          varchar(20)          not null,
   PB_ROLE_ID           varchar(15)          not null,
   constraint PK_PB_STAFF_ROLE primary key nonclustered (PB_ROLE_ID, PB_LOGIN_ID)
)
go

execute sp_addextendedproperty 'MS_Description', 
   'ְԱ��ɫ���ձ�',
   'user', 'dbo', 'table', 'PB_STAFF_ROLE'
go

execute sp_addextendedproperty 'MS_Description', 
   '��¼ID',
   'user', 'dbo', 'table', 'PB_STAFF_ROLE', 'column', 'PB_LOGIN_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ɫID',
   'user', 'dbo', 'table', 'PB_STAFF_ROLE', 'column', 'PB_ROLE_ID'
go

/*==============================================================*/
/* Table: PB_SYSCODE                                            */
/*==============================================================*/
create table dbo.PB_SYSCODE (
   PB_ID                varchar(15)          not null,
   PB_SYSCODE_TYPE_ID   varchar(15)          not null,
   PB_TAG               varchar(40)          not null,
   PB_NAME              varchar(40)          not null,
   PB_REMARK            varchar(200)         null,
   PB_ORDER_ID          int                  not null,
   constraint PK_PB_SYSCODE primary key nonclustered (PB_ID)
)
go

execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'PB_SYSCODE'
go

execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', 'dbo', 'table', 'PB_SYSCODE', 'column', 'PB_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '�������ID',
   'user', 'dbo', 'table', 'PB_SYSCODE', 'column', 'PB_SYSCODE_TYPE_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ʾ',
   'user', 'dbo', 'table', 'PB_SYSCODE', 'column', 'PB_TAG'
go

execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'PB_SYSCODE', 'column', 'PB_NAME'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', 'dbo', 'table', 'PB_SYSCODE', 'column', 'PB_REMARK'
go

execute sp_addextendedproperty 'MS_Description', 
   '����ID',
   'user', 'dbo', 'table', 'PB_SYSCODE', 'column', 'PB_ORDER_ID'
go

INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000000', '0000000000', 'rights_browse', '���', 10)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000001', '0000000000', 'rights_add', '����', 20)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000002', '0000000000', 'rights_edit', '�༭', 30)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000004', '0000000000', 'rights_delete', 'ɾ��', 40)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000003', '0000000000', 'rights_move', '�ƶ�', 50)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000005', '0000000000', 'rights_print', '��ӡ', 60)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000006', '0000000000', 'rights_download', '����', 70)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000007', '0000000000', 'rights_audit', '���', 80)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000008', '0000000000', 'rights_accredit', '��Ȩ', 90)

INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000009', '0000000001', 'countrys_china', '�й�', 10)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000010', '0000000001', 'countrys_taiwai', '����', 20)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000011', '0000000001', 'countrys_usa', '�ձ�', 30)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000012', '0000000001', 'countrys_japan', '����', 40)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000013', '0000000001', 'countrys_korea', '����', 50)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000014', '0000000001', 'countrys_singapore', '�¼���', 60)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000015', '0000000001', 'countrys_germany', '�¹�', 70)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000016', '0000000001', 'countrys_france', '����', 80)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000017', '0000000001', 'countrys_italy', '�����', 90)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000018', '0000000001', 'countrys_spain', '������', 100)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000019', '0000000001', 'countrys_switzerland', '��ʿ', 110)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000020', '0000000001', 'countrys_england', 'Ӣ��', 120)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000021', '0000000001', 'countrys_russia', '����˹', 130)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000022', '0000000001', 'countrys_australia', '�Ĵ�����', 140)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000023', '0000000001', 'countrys_india', 'ӡ��', 150)

INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000024', '0000000002', 'nations_han', '����', 10)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000025', '0000000002', 'nations_chuang', '׳��', 20)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000026', '0000000002', 'nations_manchu', '����', 30)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000027', '0000000002', 'nations_hui', '����', 40)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000028', '0000000002', 'nations_miao', '����', 50)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000029', '0000000002', 'nations_wei', 'ά�����', 60)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000030', '0000000002', 'nations_yi', '����', 70)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000031', '0000000002', 'nations_tu', '������', 80)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000032', '0000000002', 'nations_meng', '�ɹ���', 90)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000033', '0000000002', 'nations_zang', '����', 100)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000034', '0000000002', 'nations_dong', '����', 110)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000035', '0000000002', 'nations_yao', '����', 120)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000036', '0000000002', 'nations_chao', '������', 130)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000037', '0000000002', 'nations_bai', '����', 140)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000038', '0000000002', 'nations_hani', '������', 150)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000039', '0000000002', 'nations_hasake', '��������', 160)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000040', '0000000002', 'nations_li', '����', 170)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000041', '0000000002', 'nations_dai', '����', 180)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000042', '0000000002', 'nations_she', '���', 190)

INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000043', '0000000003', 'positions_chairman', '���³�', 10)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000044', '0000000003', 'positions_director', '����', 20)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000045', '0000000003', 'positions_generalmanager', '�ܾ���', 30)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000046', '0000000003', 'positions_manager', '���ܾ���', 40)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000047', '0000000003', 'positions_departmanager', '���ž���', 50)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000048', '0000000003', 'positions_employee', 'Ա��', 60)

INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000049', '0000000004', 'titles_high', '�߼�ְ��', 10)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000050', '0000000004', 'titles_middle', '�м�ְ��', 20)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000051', '0000000004', 'titles_primary', '����ְ��', 30)

INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000052', '0000000005', 'politicals_cpcmembers', '�й���Ա', 10)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000053', '0000000005', 'politicals_youths', '������Ա', 20)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000054', '0000000005', 'politicals_democratics', '��������', 30)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000055', '0000000005', 'politicals_non', '�޵���', 40)

INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000056', '0000000006', 'degrees_postdoctoral', '��ʿ��', 10)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000057', '0000000006', 'degrees_doctor', '��ʿ', 20)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000058', '0000000006', 'degrees_master', '˶ʿ', 30)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000059', '0000000006', 'degrees_bachelor', '����', 40)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000060', '0000000006', 'degrees_college', '��ר', 50)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000061', '0000000006', 'degrees_highschool', '����', 60)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000062', '0000000006', 'degrees_junior', '����', 70)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000063', '0000000006', 'degrees_primary', 'Сѧ', 80)
INSERT INTO dbo.PB_SYSCODE (PB_ID, PB_SYSCODE_TYPE_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000064', '0000000006', 'degrees_illiteracy', 'Сѧ����', 90)
go

/*==============================================================*/
/* Index: I_PB_SYSCODE_ORDER_ID                                 */
/*==============================================================*/
create clustered index I_PB_SYSCODE_ORDER_ID on dbo.PB_SYSCODE (
PB_SYSCODE_TYPE_ID ASC,
PB_ORDER_ID ASC
)
go

/*==============================================================*/
/* Index: I_PB_SYSCODE_TAG                                      */
/*==============================================================*/
create unique index I_PB_SYSCODE_TAG on dbo.PB_SYSCODE (
PB_TAG ASC
)
go

/*==============================================================*/
/* Table: PB_SYSCODE_TYPE                                       */
/*==============================================================*/
create table dbo.PB_SYSCODE_TYPE (
   PB_ID                varchar(15)          not null,
   PB_TAG               varchar(20)          not null,
   PB_NAME              varchar(40)          not null,
   PB_REMARK            varchar(200)         null,
   PB_ORDER_ID          int                  not null,
   constraint PK_PB_SYSCODE_TYPE primary key nonclustered (PB_ID)
)
go

execute sp_addextendedproperty 'MS_Description', 
   '�������',
   'user', 'dbo', 'table', 'PB_SYSCODE_TYPE'
go

execute sp_addextendedproperty 'MS_Description', 
   'ID',
   'user', 'dbo', 'table', 'PB_SYSCODE_TYPE', 'column', 'PB_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ʾ',
   'user', 'dbo', 'table', 'PB_SYSCODE_TYPE', 'column', 'PB_TAG'
go

execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'PB_SYSCODE_TYPE', 'column', 'PB_NAME'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', 'dbo', 'table', 'PB_SYSCODE_TYPE', 'column', 'PB_REMARK'
go

execute sp_addextendedproperty 'MS_Description', 
   '����ID',
   'user', 'dbo', 'table', 'PB_SYSCODE_TYPE', 'column', 'PB_ORDER_ID'
go

INSERT INTO dbo.PB_SYSCODE_TYPE (PB_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000000', 'rights', 'Ȩ��', 10)
INSERT INTO dbo.PB_SYSCODE_TYPE (PB_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000001', 'countrys', '����', 20)
INSERT INTO dbo.PB_SYSCODE_TYPE (PB_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000002', 'nations', '����', 30)
INSERT INTO dbo.PB_SYSCODE_TYPE (PB_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000003', 'positions', 'ְλ', 40)
INSERT INTO dbo.PB_SYSCODE_TYPE (PB_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000004', 'titles', 'ְ��', 50)
INSERT INTO dbo.PB_SYSCODE_TYPE (PB_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000005', 'politicals', '������ò', 60)
INSERT INTO dbo.PB_SYSCODE_TYPE (PB_ID, PB_TAG, PB_NAME, PB_ORDER_ID) VALUES ('0000000006', 'degrees', '���ѧ��', 70)
go

/*==============================================================*/
/* Index: I_PB_SYSCODE_TYPE_ORDER_ID                            */
/*==============================================================*/
create clustered index I_PB_SYSCODE_TYPE_ORDER_ID on dbo.PB_SYSCODE_TYPE (
PB_ORDER_ID ASC
)
go

/*==============================================================*/
/* Index: I_PB_SYSCODE_TYPE_TAG                                 */
/*==============================================================*/
create unique index I_PB_SYSCODE_TYPE_TAG on dbo.PB_SYSCODE_TYPE (
PB_TAG ASC
)
go

/*==============================================================*/
/* Table: PB_USER                                               */
/*==============================================================*/
create table dbo.PB_USER (
   PB_LOGIN_ID          varchar(20)          not null,
   PB_PASSWORD          varchar(40)          not null,
   PB_NAME              varchar(40)          null,
   PB_SEX               int                  null,
   PB_BIRTHDAY          datetime             null,
   PB_ID_CARD           varchar(18)          null,
   PB_OFFICE_PHONE      varchar(40)          null,
   PB_FAMILY_PHONE      varchar(40)          null,
   PB_CELL_PHONE        varchar(40)          null,
   PB_EMAIL             varchar(100)         null,
   PB_ADDRESS           varchar(200)         null,
   PB_ZIP_CODE          varchar(20)          null,
   PB_REMARK            varchar(200)         null,
   PB_DISABLED          int                  not null,
   PB_REGISTER_DATE     datetime             not null,
   constraint PK_PB_USER primary key nonclustered (PB_LOGIN_ID)
)
go

execute sp_addextendedproperty 'MS_Description', 
   'ǰ̨�û���',
   'user', 'dbo', 'table', 'PB_USER'
go

execute sp_addextendedproperty 'MS_Description', 
   '��¼ID',
   'user', 'dbo', 'table', 'PB_USER', 'column', 'PB_LOGIN_ID'
go

execute sp_addextendedproperty 'MS_Description', 
   '��¼����',
   'user', 'dbo', 'table', 'PB_USER', 'column', 'PB_PASSWORD'
go

execute sp_addextendedproperty 'MS_Description', 
   '����',
   'user', 'dbo', 'table', 'PB_USER', 'column', 'PB_NAME'
go

execute sp_addextendedproperty 'MS_Description', 
   '�Ա�',
   'user', 'dbo', 'table', 'PB_USER', 'column', 'PB_SEX'
go

execute sp_addextendedproperty 'MS_Description', 
   '��������',
   'user', 'dbo', 'table', 'PB_USER', 'column', 'PB_BIRTHDAY'
go

execute sp_addextendedproperty 'MS_Description', 
   '���֤��',
   'user', 'dbo', 'table', 'PB_USER', 'column', 'PB_ID_CARD'
go

execute sp_addextendedproperty 'MS_Description', 
   '�칫�绰',
   'user', 'dbo', 'table', 'PB_USER', 'column', 'PB_OFFICE_PHONE'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ͥ�绰',
   'user', 'dbo', 'table', 'PB_USER', 'column', 'PB_FAMILY_PHONE'
go

execute sp_addextendedproperty 'MS_Description', 
   '�ֻ�',
   'user', 'dbo', 'table', 'PB_USER', 'column', 'PB_CELL_PHONE'
go

execute sp_addextendedproperty 'MS_Description', 
   'Email',
   'user', 'dbo', 'table', 'PB_USER', 'column', 'PB_EMAIL'
go

execute sp_addextendedproperty 'MS_Description', 
   'ͨѶ��ַ',
   'user', 'dbo', 'table', 'PB_USER', 'column', 'PB_ADDRESS'
go

execute sp_addextendedproperty 'MS_Description', 
   '�ʱ�',
   'user', 'dbo', 'table', 'PB_USER', 'column', 'PB_ZIP_CODE'
go

execute sp_addextendedproperty 'MS_Description', 
   '��ע',
   'user', 'dbo', 'table', 'PB_USER', 'column', 'PB_REMARK'
go

execute sp_addextendedproperty 'MS_Description', 
   '�ѽ���',
   'user', 'dbo', 'table', 'PB_USER', 'column', 'PB_DISABLED'
go

execute sp_addextendedproperty 'MS_Description', 
   'ע��ʱ��',
   'user', 'dbo', 'table', 'PB_USER', 'column', 'PB_REGISTER_DATE'
go

/*==============================================================*/
/* Index: I_PB_USER_REGISTER_DATE                               */
/*==============================================================*/
create clustered index I_PB_USER_REGISTER_DATE on dbo.PB_USER (
PB_REGISTER_DATE DESC
)
go

alter table dbo.PB_MODULE
   add constraint MODULE_TYPE_REF_MODULE foreign key (PB_MODULE_TYPE_ID)
      references dbo.PB_MODULE_TYPE (PB_ID)
go

alter table dbo.PB_MODULE_RIGHT
   add constraint MODULE_REF_MODULE_RIGHT foreign key (PB_MODULE_ID)
      references dbo.PB_MODULE (PB_ID)
go

alter table dbo.PB_ROLE
   add constraint ROLE_TYPE_REF_ROLE foreign key (PB_ROLE_TYPE_ID)
      references dbo.PB_ROLE_TYPE (PB_ID)
go

alter table dbo.PB_ROLE_MODULE_RIGHT_DENY
   add constraint M_R_REF_ROLE_M_R_DENY foreign key (PB_RIGHT_ID)
      references dbo.PB_MODULE_RIGHT (PB_ID)
go

alter table dbo.PB_ROLE_MODULE_RIGHT_DENY
   add constraint ROLE_REF_ROLE_M_R_DENY foreign key (PB_ROLE_ID)
      references dbo.PB_ROLE (PB_ID)
go

alter table dbo.PB_ROLE_MODULE_RIGHT_GRANT
   add constraint M_R_REF_ROLE_M_R_GRANT foreign key (PB_RIGHT_ID)
      references dbo.PB_MODULE_RIGHT (PB_ID)
go

alter table dbo.PB_ROLE_MODULE_RIGHT_GRANT
   add constraint ROLE_REF_ROLE_M_R_GRANT foreign key (PB_ROLE_ID)
      references dbo.PB_ROLE (PB_ID)
go

alter table dbo.PB_STAFF
   add constraint DEPARTMENT_REF_STAFF foreign key (PB_DEPARTMENT_ID)
      references dbo.PB_DEPARTMENT (PB_ID)
go

alter table dbo.PB_STAFF_MODULE_RIGHT_DENY
   add constraint M_R_REF_STAFF_M_R_DENY foreign key (PB_RIGHT_ID)
      references dbo.PB_MODULE_RIGHT (PB_ID)
go

alter table dbo.PB_STAFF_MODULE_RIGHT_DENY
   add constraint STAFF_REF_STAFF_M_R_DENY foreign key (PB_LOGIN_ID)
      references dbo.PB_STAFF (PB_LOGIN_ID)
go

alter table dbo.PB_STAFF_MODULE_RIGHT_GRANT
   add constraint M_R_REF_STAFF_M_R_GRANT foreign key (PB_RIGHT_ID)
      references dbo.PB_MODULE_RIGHT (PB_ID)
go

alter table dbo.PB_STAFF_MODULE_RIGHT_GRANT
   add constraint STAFF_REF_STAFF_M_R_GRANT foreign key (PB_LOGIN_ID)
      references dbo.PB_STAFF (PB_LOGIN_ID)
go

alter table dbo.PB_STAFF_ROLE
   add constraint ROLE_REF_STAFF_ROLE foreign key (PB_ROLE_ID)
      references dbo.PB_ROLE (PB_ID)
go

alter table dbo.PB_STAFF_ROLE
   add constraint STAFF_REF_STAFF_ROLE foreign key (PB_LOGIN_ID)
      references dbo.PB_STAFF (PB_LOGIN_ID)
go

alter table dbo.PB_SYSCODE
   add constraint SYSCODE_TYPE_REF_SYSCODE foreign key (PB_SYSCODE_TYPE_ID)
      references dbo.PB_SYSCODE_TYPE (PB_ID)
go



USE master
GO

/* �Զ����� */
ALTER DATABASE [Gecko17] SET AUTO_SHRINK ON WITH NO_WAIT
GO

/* �򵥻�ԭģʽ */
ALTER DATABASE [Gecko17] SET RECOVERY SIMPLE WITH NO_WAIT
GO

/* ��ӵ�¼�� */
EXEC sp_addlogin 'PB_DB_USER', '1234567890', 'Gecko17'
GO

USE [Gecko17]
GO

/* ����û� */
EXEC sp_adduser 'PB_DB_USER', 'PB_DB_USER'
GO

/* Ϊ�û���ӽ�ɫ */
EXEC sp_addrolemember 'db_datareader', 'PB_DB_USER'
GO
EXEC sp_addrolemember 'db_datawriter', 'PB_DB_USER'
GO
