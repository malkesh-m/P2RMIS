--Inserts MechanismTemplate data for existing summary mechanism templates
UPDATE MechanismTemplate
SET ProgramMechanismId = ProgramMechanism.ProgramMechanismId, ReviewStageId = 3
FROM ProgramMechanism INNER JOIN
MechanismTemplate ON ProgramMechanism.LegacyAtmId = MechanismTemplate.MechanismId
WHERE MechanismTemplate.ProgramMechanismId IS NULL

UPDATE MechanismTemplateElement
SET LegacyEcmId = ECM_ID
FROM MechanismTemplateElement INNER JOIN
MechanismTemplate ON MechanismTemplateElement.MechanismTemplateId = MechanismTemplateElement.MechanismTemplateId INNER JOIN
ProgramMechanism ON MechanismTemplate.ProgramMechanismId = ProgramMechanism.ProgramMechanismId INNER JOIN
ClientElement ON MechanismTemplateElement.ClientElementId = ClientElement.ClientElementId INNER JOIN
[$(P2RMIS)].dbo.PRO_Evaluation_Criteria_Member ON ClientElement.ElementIdentifier = PRO_Evaluation_Criteria_Member.EVAL_CRITERIA
AND ProgramMechanism.LegacyAtmId = PRO_Evaluation_Criteria_Member.ATM_ID
WHERE LegacyEcmId IS NULL

--Insert MechanismTemplateElementScoring items for existing Summary Statement template (temporary until stored procedure is written to set this up when summary is started)
INSERT INTO MechanismTemplateElementScoring
                      (MechanismTemplateElementId, ClientScoringId, StepTypeId, ModifiedBy, ModifiedDate)
SELECT MechanismTemplateElement.MechanismTemplateElementId, ClientScoringScale.ClientScoringId, step.StepTypeId, VUN.UserId, PRO_ECM_Scoring.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.PRO_ECM_Scoring INNER JOIN
[$(P2RMIS)].dbo.PRO_Evaluation_Criteria_Member ON PRO_ECM_Scoring.ECM_ID = PRO_Evaluation_Criteria_Member.ECM_ID INNER JOIN
[$(P2RMIS)].dbo.PRO_Award_Type_Member ON PRO_Evaluation_Criteria_Member.ATM_ID = PRO_Award_Type_Member.ATM_ID INNER JOIN
ProgramMechanism ON PRO_Award_Type_Member.ATM_ID = ProgramMechanism.LegacyAtmId INNER JOIN
ProgramYear ON ProgramMechanism.ProgramYearId = ProgramYear.ProgramYearId INNER JOIN
ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
MechanismTemplate ON ProgramMechanism.ProgramMechanismId = MechanismTemplate.ProgramMechanismId AND ReviewStageId = 3 AND ReviewStatusId = 2 INNER JOIN
MechanismTemplateElement ON MechanismTemplate.MechanismTemplateId = MechanismTemplateElement.MechanismTemplateId AND PRO_Evaluation_Criteria_Member.ECM_ID = MechanismTemplateElement.LegacyEcmId INNER JOIN
ClientScoringScale ON PRO_ECM_Scoring.Hi_Val = ClientScoringScale.HighValue AND
ISNULL(PRO_ECM_Scoring.Hi_Val_Desc, '') = ISNULL(ClientScoringScale.HighValueDescription, '') AND
PRO_ECM_Scoring.Middle_Val = ClientScoringScale.MiddleValue AND
ISNULL(PRO_ECM_Scoring.Middle_Val_Desc, '') = ISNULL(ClientScoringScale.MiddleValueDescription, '') AND
PRO_ECM_Scoring.Low_Val = ClientScoringScale.LowValue AND
ISNULL(PRO_ECM_Scoring.Low_Val_Desc, '') = ISNULL(ClientScoringScale.LowValueDescription, '') AND
PRO_ECM_Scoring.Scoring_Type = ClientScoringScale.ScoreType AND ClientProgram.ClientId = ClientScoringScale.ClientId LEFT OUTER JOIN
ViewLegacyUserNameToUserId VUN ON PRO_ECM_Scoring.LAST_UPDATED_BY = VUN.UserName CROSS JOIN
(Select StepTypeId FROM StepType WHERE StepTypeId IN (1,2,3,4)) step
GROUP BY MechanismTemplateElement.MechanismTemplateElementId, ClientScoringScale.ClientScoringId, PRO_ECM_Scoring.Scoring_Phase, PRO_Evaluation_Criteria_Member.Overall_Eval, VUN.UserId, PRO_ECM_Scoring.LAST_UPDATE_DATE, step.StepTypeId
HAVING Scoring_Phase = 'Meeting'

INSERT INTO MechanismTemplateElementScoring
                      (MechanismTemplateElementId, ClientScoringId, StepTypeId, ModifiedBy, ModifiedDate)
SELECT MechanismTemplateElement.MechanismTemplateElementId, ClientScoringScale.ClientScoringId, step.StepTypeId, VUN.UserId, PRO_ECM_Scoring.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.PRO_ECM_Scoring INNER JOIN
[$(P2RMIS)].dbo.PRO_Evaluation_Criteria_Member ON PRO_ECM_Scoring.ECM_ID = PRO_Evaluation_Criteria_Member.ECM_ID INNER JOIN
[$(P2RMIS)].dbo.PRO_Award_Type_Member ON PRO_Evaluation_Criteria_Member.ATM_ID = PRO_Award_Type_Member.ATM_ID INNER JOIN
ProgramMechanism ON PRO_Award_Type_Member.ATM_ID = ProgramMechanism.LegacyAtmId INNER JOIN
ProgramYear ON ProgramMechanism.ProgramYearId = ProgramYear.ProgramYearId INNER JOIN
ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
MechanismTemplate ON ProgramMechanism.ProgramMechanismId = MechanismTemplate.ProgramMechanismId AND ReviewStageId = 3 AND ReviewStatusId = 1 INNER JOIN
MechanismTemplateElement ON MechanismTemplate.MechanismTemplateId = MechanismTemplateElement.MechanismTemplateId AND PRO_Evaluation_Criteria_Member.ECM_ID = MechanismTemplateElement.LegacyEcmId INNER JOIN
ClientScoringScale ON PRO_ECM_Scoring.Hi_Val = ClientScoringScale.HighValue AND
ISNULL(PRO_ECM_Scoring.Hi_Val_Desc, '') = ISNULL(ClientScoringScale.HighValueDescription, '') AND
PRO_ECM_Scoring.Middle_Val = ClientScoringScale.MiddleValue AND
ISNULL(PRO_ECM_Scoring.Middle_Val_Desc, '') = ISNULL(ClientScoringScale.MiddleValueDescription, '') AND
PRO_ECM_Scoring.Low_Val = ClientScoringScale.LowValue AND
ISNULL(PRO_ECM_Scoring.Low_Val_Desc, '') = ISNULL(ClientScoringScale.LowValueDescription, '') AND
PRO_ECM_Scoring.Scoring_Type = ClientScoringScale.ScoreType AND ClientProgram.ClientId = ClientScoringScale.ClientId LEFT OUTER JOIN
ViewLegacyUserNameToUserId VUN ON PRO_ECM_Scoring.LAST_UPDATED_BY = VUN.UserName CROSS JOIN
(Select StepTypeId FROM StepType WHERE StepTypeId IN (1,2,3,4)) step
GROUP BY MechanismTemplateElement.MechanismTemplateElementId, ClientScoringScale.ClientScoringId, PRO_ECM_Scoring.Scoring_Phase, PRO_Evaluation_Criteria_Member.Overall_Eval, VUN.UserId, PRO_ECM_Scoring.LAST_UPDATE_DATE, step.StepTypeId
HAVING Scoring_Phase = 'Revised'