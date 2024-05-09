CREATE FUNCTION [dbo].[SplitReportParameterString]
(
	@ParameterToSplit varchar(max)
)
RETURNS @ParameterValues TABLE
(
	ParameterValue varchar(50)
) WITH SCHEMABINDING
AS
BEGIN
	IF @ParameterToSplit IS NULL
		SET @ParameterToSplit = ''
	--This function converts a comma delimited string list of parameters and returns in a table
	DECLARE @ParamXML xml,
			@ParamOutput varchar(50)
	--Convert comma delimited to xml node structure
	SELECT @ParamXML = CONVERT(xml,'<root><p>' + REPLACE(@ParameterToSplit,',','</p><p>') + '</p></root>')
	--Shred xml nodes back into relational form and insert into output table
	INSERT @ParameterValues
	SELECT T.c.value('.','varchar(50)')
		FROM @ParamXML.nodes('/root/p') T(c)
	RETURN
END
GO
GRANT SELECT
    ON OBJECT::[dbo].[SplitReportParameterString] TO [NetSqlAzMan_Users]
    AS [dbo];

