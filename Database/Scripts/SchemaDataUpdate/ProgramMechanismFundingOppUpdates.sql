UPDATE ProgramMechanism SET FundingOpportunityId = ATM.Opportunity_ID
FROM [$(P2RMIS)].dbo.PRO_Award_Type_Member ATM INNER JOIN
	 [dbo].[ProgramMechanism] ProgramMechanism ON ATM.ATM_ID = ProgramMechanism.LegacyAtmId 
WHERE ISNULL(ProgramMechanism.FundingOpportunityId, '') <> ISNULL(ATM.Opportunity_ID, '') AND ISNULL(ATM.Opportunity_ID, '') <> '';