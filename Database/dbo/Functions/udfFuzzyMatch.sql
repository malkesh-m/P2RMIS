CREATE FUNCTION [dbo].[udfFuzzyMatch](@str1 NVARCHAR(255), @str2 NVARCHAR(255)) 
RETURNS bit
AS
BEGIN
	DECLARE @matched BIT
	IF SOUNDEX(@str1) = SOUNDEX(@str2)
		SET @matched = 1
	ELSE
		SET @matched = 0
	RETURN @matched
END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[udfFuzzyMatch] TO [NetSqlAzMan_Users]
    AS [dbo];