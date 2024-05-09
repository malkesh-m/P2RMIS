UPDATE ProgramMechanism SET FundingOpportunityId = PRO_Award_Type_Member.Opportunity_ID, ParentProgramMechanismId = ParentProgramMechanism.ProgramMechanismId, MechanismRelationshipTypeId = 1
FROM ProgramMechanism INNER JOIN
	[$(P2RMIS)].dbo.PRO_Award_Type_Member PRO_Award_Type_Member ON ProgramMechanism.LegacyAtmId = PRO_Award_Type_Member.ATM_ID LEFT OUTER JOIN
	[$(P2RMIS)].dbo.PRO_Award_Type_Member PRO_ATM_Parent ON PRO_Award_Type_Member.ATM_ID = PRO_ATM_Parent.PRE_ATM_ID LEFT OUTER JOIN
	ViewProgramMechanism ParentProgramMechanism ON PRO_ATM_Parent.ATM_ID = ParentProgramMechanism.LegacyAtmId 

