UPDATE MechanismTemplateElement SET SummaryIncludeFlag = CASE WHEN PRO_Evaluation_Criteria_Member.Eval_Criteria = 'DE' THEN 0 ELSE 1 END, SummarySortOrder = PRO_Evaluation_Criteria_Member.SS_Sort_Order
FROM MechanismTemplateElement
INNER JOIN [$(P2RMIS)].dbo.PRO_Evaluation_Criteria_Member PRO_Evaluation_Criteria_Member ON MechanismTemplateElement.LegacyEcmId = PRO_Evaluation_Criteria_Member.ECM_ID