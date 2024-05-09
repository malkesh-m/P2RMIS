CREATE FUNCTION [dbo].[udfGetNewUserName]
( @FirstName varchar(50), @LastName varchar(50) ) RETURNS varchar(50) AS 
BEGIN    
DECLARE @ret varchar(50), 
@nameFormatted varchar(50),
@placeHolder char(2) = '00'
SELECT @nameFormatted = LEFT(@FirstName, 1) + LEFT(@LastName, 5) + @placeHolder 
SELECT @ret = @nameFormatted + CAST((Select Count(*) FROM [User] WHERE [UserLogin] LIKE @nameFormatted + '%') + 1 AS varchar(50))
RETURN @ret;  
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[udfGetNewUserName] TO [NetSqlAzMan_Users]
    AS [dbo];