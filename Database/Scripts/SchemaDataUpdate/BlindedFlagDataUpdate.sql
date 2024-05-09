UPDATE ProgramMechanism
SET BlindedFlag = 1
FROM ProgramMechanism INNER JOIN
[$(P2RMIS)].dbo.PRO_Award_Type_Member ATM ON ProgramMechanism.LegacyAtmId = ATM.ATM_ID
WHERE ATM.reviewtypeID = 2