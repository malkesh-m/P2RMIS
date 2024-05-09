CREATE ROLE [NetSqlAzMan_Managers]
    AUTHORIZATION [dbo];


GO
EXECUTE sp_addrolemember @rolename = N'NetSqlAzMan_Managers', @membername = N'NetSqlAzMan_Administrators';

