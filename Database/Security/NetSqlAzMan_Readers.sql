CREATE ROLE [NetSqlAzMan_Readers]
    AUTHORIZATION [dbo];


GO
EXECUTE sp_addrolemember @rolename = N'NetSqlAzMan_Readers', @membername = N'NetSqlAzMan_Users';


GO
EXECUTE sp_addrolemember @rolename = N'NetSqlAzMan_Readers', @membername = N'NetSqlAzMan_Managers';


GO
EXECUTE sp_addrolemember @rolename = N'NetSqlAzMan_Readers', @membername = N'NetSqlAzMan_Administrators';
