
USE [$(P2RMIS)]
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[stpr_PRG_Critique_Submit] TO [NetSqlAzMan_Users]
    AS [dbo];
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[stpr_Rev_Assignments_Delete] TO [NetSqlAzMan_Users]
    AS [dbo];
GO
USE [$(DatabaseName)]
GO