UPDATE ProgramMechanism Set PartneringPiAllowedFlag = ISNULL(PRO_Award_Type_Member.Partnering_PI_Allowed, 0)
FROM ProgramMechanism INNER JOIN
[$(P2RMIS)].dbo.PRO_Award_Type_Member PRO_Award_Type_Member ON ProgramMechanism.LegacyAtmId = PRO_Award_Type_Member.ATM_ID
