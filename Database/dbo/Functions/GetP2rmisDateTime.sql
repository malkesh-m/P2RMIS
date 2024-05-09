CREATE FUNCTION [dbo].[GetP2rmisDateTime] ()
RETURNS DATETIME2
AS
BEGIN
	RETURN CAST(SYSDATETIMEOFFSET() at time zone 'Eastern Standard Time' AS datetime2)
END
