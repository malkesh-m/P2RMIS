UPDATE [ApplicationPersonnel] SET StateAbbreviation = CASE ClientApplicationPersonnelType.ApplicationPersonnelTypeAbbreviation WHEN 'PI' THEN PRO_Proposal.PI_State WHEN 'Admin-1' THEN PRO_Proposal.Admin1_State END
FROM ApplicationPersonnel INNER JOIN
[ViewApplication] ON ApplicationPersonnel.ApplicationId = ViewApplication.ApplicationId INNER JOIN
[ClientApplicationPersonnelType] ON ApplicationPersonnel.ClientApplicationPersonnelTypeId = ClientApplicationPersonnelType.ClientApplicationPersonnelTypeId INNER JOIN
[$(P2RMIS)].dbo.PRO_Proposal PRO_Proposal ON ViewApplication.LogNumber = PRO_Proposal.Log_No
WHERE ApplicationPersonnel.DeletedFlag = 0 AND ClientApplicationPersonnelType.ApplicationPersonnelTypeAbbreviation IN ('PI','Admin-1')