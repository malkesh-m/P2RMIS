CREATE ROLE [NetSqlAzMan_Users]
    AUTHORIZATION [dbo];


GO
EXECUTE sp_addrolemember @rolename = N'NetSqlAzMan_Users', @membername = N'NetSqlAzMan_Managers';


GO
EXECUTE sp_addrolemember @rolename = N'NetSqlAzMan_Users', @membername = N'NetSqlAzMan_Administrators';
