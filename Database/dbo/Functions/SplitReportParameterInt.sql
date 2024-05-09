CREATE FUNCTION [dbo].[SplitReportParameterInt]
(
	@ParameterToSplit varchar(max)
)
RETURNS @ParameterValues TABLE
(
	ParameterValue int
) WITH SCHEMABINDING
AS
BEGIN
	IF @ParameterToSplit IS NULL
		SET @ParameterToSplit = 0
	--This function converts a comma delimited string list of integer parameters and returns in a table
	DECLARE @ParamXML xml
	--Convert comma delimited to xml node structure
	SELECT @ParamXML = CONVERT(xml,'<root><p>' + REPLACE(@ParameterToSplit,',','</p><p>') + '</p></root>')
	--Shred xml nodes back into relational form and insert into output table
	INSERT @ParameterValues
	SELECT T.c.value('.','int')
		FROM @ParamXML.nodes('/root/p') T(c)
	RETURN
END
GO
GRANT SELECT
    ON OBJECT::[dbo].[SplitReportParameterInt] TO [NetSqlAzMan_Users]
    AS [dbo];
