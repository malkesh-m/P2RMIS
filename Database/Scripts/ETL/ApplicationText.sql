DECLARE @BatchSize INT = 10

WHILE 1 = 1
BEGIN 


INSERT INTO [ApplicationText]
           ([ApplicationId]
           ,[ClientApplicationTextTypeId]
           ,[BodyText]
           ,[AbstractFlag]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT app.ApplicationId, catt.ClientApplicationTextTypeId, protext.BodyText, CASE WHEN pm.AbstractFormat = 'Data' THEN 1 ELSE 0 END, 
		vun.UserID, protext.LAST_UPDATED_DATE
FROM [$(P2RMIS)].dbo.PRO_Text protext INNER JOIN
[$(P2RMIS)].dbo.PRO_Text_Type protexttype ON protext.TextTypeID = protexttype.TextTypeID INNER JOIN
	[ViewApplication] app ON protext.LOG_NO = app.LogNumber INNER JOIN
	[ViewProgramMechanism] pm ON app.ProgramMechanismId = pm.ProgramMechanismId INNER JOIN
	[ClientAwardType] cat ON pm.ClientAwardTypeId = cat.ClientAwardTypeId INNER JOIN
	[ClientApplicationTextType] catt ON protexttype.TextType = catt.TextType AND cat.ClientId = catt.ClientId LEFT OUTER JOIN
	ViewLegacyUserNameToUserId vun ON protext.LAST_UPDATED_BY = vun.UserName
WHERE NOT EXISTS (Select 1 From ViewApplicationText WHERE ApplicationId = app.ApplicationId AND ClientApplicationTextTypeId = catt.ClientApplicationTextTypeId)
IF @@ROWCOUNT < @BatchSize BREAK

END