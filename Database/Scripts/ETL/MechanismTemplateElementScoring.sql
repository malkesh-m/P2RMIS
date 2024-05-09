--This will duplicate 'Meeting' scoring_phase for Pre-Meeting and Meeting stage which should not cause problems
INSERT INTO MechanismTemplateElementScoring
                      (MechanismTemplateElementId, ClientScoringId, StepTypeId, ModifiedBy, ModifiedDate)
--Get all scoring_phases in legacy structure for pre-meeting stage
SELECT MechanismTemplateElement.MechanismTemplateElementId, ClientScoringScale.ClientScoringId, CASE PRO_ECM_Scoring.Scoring_phase WHEN 'Initial' THEN 5 WHEN 'Revised' THEN 6 WHEN 'Meeting' THEN 7 END, VUN.UserId, PRO_ECM_Scoring.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.PRO_ECM_Scoring INNER JOIN
[$(P2RMIS)].dbo.PRO_Evaluation_Criteria_Member ON PRO_ECM_Scoring.ECM_ID = PRO_Evaluation_Criteria_Member.ECM_ID INNER JOIN
[$(P2RMIS)].dbo.PRO_Award_Type_Member ON PRO_Evaluation_Criteria_Member.ATM_ID = PRO_Award_Type_Member.ATM_ID INNER JOIN
ViewProgramMechanism ProgramMechanism ON PRO_Award_Type_Member.ATM_ID = ProgramMechanism.LegacyAtmId INNER JOIN
ViewProgramYear ProgramYear ON ProgramMechanism.ProgramYearId = ProgramYear.ProgramYearId INNER JOIN
ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
ViewMechanismTemplate MechanismTemplate ON ProgramMechanism.ProgramMechanismId = MechanismTemplate.ProgramMechanismId AND ReviewStageId = 1 INNER JOIN
ViewMechanismTemplateElement MechanismTemplateElement ON MechanismTemplate.MechanismTemplateId = MechanismTemplateElement.MechanismTemplateId AND PRO_Evaluation_Criteria_Member.ECM_ID = MechanismTemplateElement.LegacyEcmId INNER JOIN
ClientScoringScale ON PRO_ECM_Scoring.Hi_Val = ClientScoringScale.HighValue AND
ISNULL(PRO_ECM_Scoring.Hi_Val_Desc, '') = ISNULL(ClientScoringScale.HighValueDescription, '') AND
PRO_ECM_Scoring.Middle_Val = ClientScoringScale.MiddleValue AND
ISNULL(PRO_ECM_Scoring.Middle_Val_Desc, '') = ISNULL(ClientScoringScale.MiddleValueDescription, '') AND
PRO_ECM_Scoring.Low_Val = ClientScoringScale.LowValue AND
ISNULL(PRO_ECM_Scoring.Low_Val_Desc, '') = ISNULL(ClientScoringScale.LowValueDescription, '') AND
PRO_ECM_Scoring.Scoring_Type = ClientScoringScale.ScoreType AND ClientProgram.ClientId = ClientScoringScale.ClientId LEFT OUTER JOIN
ViewLegacyUserNameToUserId VUN ON PRO_ECM_Scoring.LAST_UPDATED_BY = VUN.UserName
WHERE NOT EXISTS (Select 'X' FROM ViewMechanismTemplateElementScoring WHERE MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId AND StepTypeId = CASE PRO_ECM_Scoring.Scoring_phase WHEN 'Initial' THEN 5 WHEN 'Revised' THEN 6 WHEN 'Meeting' THEN 7 END)
GROUP BY MechanismTemplateElement.MechanismTemplateElementId, ClientScoringScale.ClientScoringId, PRO_ECM_Scoring.Scoring_Phase, PRO_Evaluation_Criteria_Member.Overall_Eval, VUN.UserId, PRO_ECM_Scoring.LAST_UPDATE_DATE
UNION ALL
--Get only scoring_phase of Meeting in legacy structure for meeting stage
SELECT MechanismTemplateElement.MechanismTemplateElementId, ClientScoringScale.ClientScoringId, 8, VUN.UserId, PRO_ECM_Scoring.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.PRO_ECM_Scoring INNER JOIN
[$(P2RMIS)].dbo.PRO_Evaluation_Criteria_Member ON PRO_ECM_Scoring.ECM_ID = PRO_Evaluation_Criteria_Member.ECM_ID INNER JOIN
[$(P2RMIS)].dbo.PRO_Award_Type_Member ON PRO_Evaluation_Criteria_Member.ATM_ID = PRO_Award_Type_Member.ATM_ID INNER JOIN
ViewProgramMechanism ProgramMechanism ON PRO_Award_Type_Member.ATM_ID = ProgramMechanism.LegacyAtmId INNER JOIN
ViewProgramYear ProgramYear ON ProgramMechanism.ProgramYearId = ProgramYear.ProgramYearId INNER JOIN
ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
ViewMechanismTemplate MechanismTemplate ON ProgramMechanism.ProgramMechanismId = MechanismTemplate.ProgramMechanismId AND ReviewStageId = 2 INNER JOIN
ViewMechanismTemplateElement MechanismTemplateElement ON MechanismTemplate.MechanismTemplateId = MechanismTemplateElement.MechanismTemplateId AND PRO_Evaluation_Criteria_Member.ECM_ID = MechanismTemplateElement.LegacyEcmId INNER JOIN
ClientScoringScale ON PRO_ECM_Scoring.Hi_Val = ClientScoringScale.HighValue AND
ISNULL(PRO_ECM_Scoring.Hi_Val_Desc, '') = ISNULL(ClientScoringScale.HighValueDescription, '') AND
PRO_ECM_Scoring.Middle_Val = ClientScoringScale.MiddleValue AND
ISNULL(PRO_ECM_Scoring.Middle_Val_Desc, '') = ISNULL(ClientScoringScale.MiddleValueDescription, '') AND
PRO_ECM_Scoring.Low_Val = ClientScoringScale.LowValue AND
ISNULL(PRO_ECM_Scoring.Low_Val_Desc, '') = ISNULL(ClientScoringScale.LowValueDescription, '') AND
PRO_ECM_Scoring.Scoring_Type = ClientScoringScale.ScoreType AND ClientProgram.ClientId = ClientScoringScale.ClientId LEFT OUTER JOIN
ViewLegacyUserNameToUserId VUN ON PRO_ECM_Scoring.LAST_UPDATED_BY = VUN.UserName
WHERE NOT EXISTS (Select 'X' FROM ViewMechanismTemplateElementScoring WHERE MechanismTemplateElementId = MechanismTemplateElement.MechanismTemplateElementId AND StepTypeId = 8)
GROUP BY MechanismTemplateElement.MechanismTemplateElementId, ClientScoringScale.ClientScoringId, PRO_ECM_Scoring.Scoring_Phase, PRO_Evaluation_Criteria_Member.Overall_Eval, VUN.UserId, PRO_ECM_Scoring.LAST_UPDATE_DATE
HAVING Scoring_Phase = 'Meeting'
