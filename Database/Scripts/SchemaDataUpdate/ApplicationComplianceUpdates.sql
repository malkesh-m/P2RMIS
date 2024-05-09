--Import all compliance statuses from 1.0
INSERT INTO ApplicationCompliance ([ApplicationId]
           ,[ComplianceStatusId]
           ,[Comment]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT [ViewApplication].ApplicationId, [ComplianceStatus].ComplianceStatusId, PRO_Compliance.Comment, VUN.UserId, dbo.GetP2rmisDateTime()
FROM [$(P2RMIS)].dbo.PRO_Compliance PRO_Compliance INNER JOIN
[ViewApplication] ON PRO_Compliance.Log_No = [ViewApplication].LogNumber INNER JOIN
[ComplianceStatus] ON PRO_Compliance.Compliance_Status = ComplianceStatus.ComplianceStatusLabel LEFT OUTER JOIN
ViewLegacyUserNameToUserId VUN ON PRO_Compliance.Last_Updated_By = VUN.UserName
WHERE NOT EXISTS (Select 'X' FROM ApplicationCompliance WHERE ApplicationId = ViewApplication.ApplicationId AND ComplianceStatusId = [ComplianceStatus].ComplianceStatusId AND DeletedFlag = 0)

--Import withdrawn statuses
UPDATE [Application] SET [WithdrawnFlag] = CASE WHEN PRO_Proposal.WITHDRAWN_DATE IS NOT NULL THEN 1 ELSE 0 END, [WithdrawnBy] = VUN.UserID, [WithdrawnDate] = PRO_Proposal.WITHDRAWN_DATE
FROM [Application] INNER JOIN
[$(P2RMIS)].dbo.PRO_Proposal PRO_Proposal ON [Application].LogNumber = PRO_Proposal.LOG_NO LEFT OUTER JOIN
ViewLegacyUserNameToUserId VUN ON PRO_Proposal.Withdrawn_By = VUN.UserName
WHERE DeletedFlag = 0 AND WithdrawnDate IS NULL AND PRO_Proposal.WITHDRAWN_DATE IS NOT NULL
