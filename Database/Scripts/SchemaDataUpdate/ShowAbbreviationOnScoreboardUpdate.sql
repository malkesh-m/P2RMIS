﻿UPDATE MechanismTemplateElement SET ShowAbbreviationOnScoreboard = 1
FROM MechanismTemplateElement INNER JOIN
[$(P2RMIS)].dbo.PRO_Evaluation_Criteria_Member PRO_Evaluation_Criteria_Member ON MechanismTemplateElement.LegacyEcmId = PRO_Evaluation_Criteria_Member.ECM_ID
WHERE PRO_Evaluation_Criteria_Member.Criteria_Display = 1