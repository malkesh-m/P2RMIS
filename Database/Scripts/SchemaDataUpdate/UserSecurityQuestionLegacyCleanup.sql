DELETE FROM UserAccountRecovery
WHERE EXISTS
(Select 'X'
FROM ViewUserAccountRecovery INNER JOIN
ViewUserAccountRecovery ViewUserAccountRecovery2 ON  ViewUserAccountRecovery.UserId = ViewUserAccountRecovery2.UserId INNER JOIN
ViewUserAccountRecovery ViewUserAccountRecovery3 ON  ViewUserAccountRecovery.UserId = ViewUserAccountRecovery3.UserId
WHERE UserAccountRecovery.UserAccountRecoveryId = (CASE UserAccountRecovery.QuestionOrder WHEN 1 THEN ViewUserAccountRecovery.UserAccountRecoveryId WHEN 2 THEN ViewUserAccountRecovery2.UserAccountRecoveryId WHEN 3 THEN ViewUserAccountRecovery3.UserAccountRecoveryId END) AND ViewUserAccountRecovery.QuestionOrder = 1 AND ViewUserAccountRecovery2.QuestionOrder = 2 AND ViewUserAccountRecovery3.QuestionOrder = 3 AND ViewUserAccountRecovery.RecoveryQuestionId = ViewUserAccountRecovery2.RecoveryQuestionId AND ViewUserAccountRecovery2.RecoveryQuestionId = ViewUserAccountRecovery3.RecoveryQuestionId
) AND UserAccountRecovery.QuestionOrder <> 1