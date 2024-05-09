UPDATE [dbo].[ClientApplicationPersonnelType]
   SET [ExternalPersonnelTypeId] = PRO_COI_Type.coitypeid
FROM [ClientApplicationPersonnelType] INNER JOIN
[$(P2RMIS)].dbo.PRO_COI_Type PRO_COI_Type  ON [ClientApplicationPersonnelType].ApplicationPersonnelType = PRO_COI_Type.coitype