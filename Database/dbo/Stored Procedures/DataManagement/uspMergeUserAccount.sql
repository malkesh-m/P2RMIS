CREATE PROCEDURE [dbo].[uspMergeUserAccount]
	@UserIdToKeep int,
	@UserIdToMerge int
AS
BEGIN
--First merge data that needs merged
UPDATE PanelUserPotentialAssignment SET UserId = @UserIdToKeep
WHERE UserId = @UserIdToMerge AND DeletedFlag = 0;
UPDATE PanelUserAssignment SET UserId = @UserIdToKeep
WHERE UserId = @UserIdToMerge AND DeletedFlag = 0;
UPDATE PanelUserAssignment SET UserId = @UserIdToKeep
WHERE UserId = @UserIdToMerge AND DeletedFlag = 0;
UPDATE UserApplicationComment  SET UserId = @UserIdToKeep
WHERE UserId = @UserIdToMerge AND DeletedFlag = 0;
UPDATE UserCommunicationLog SET UserId = @UserIdToKeep
WHERE UserId = @UserIdToMerge AND DeletedFlag = 0;
UPDATE UserEvaluationLog SET UserInfoId = @UserIdToKeep
FROM UserEvaluationLog 
INNER JOIN UserInfo ON UserEvaluationLog.UserInfoId = UserInfo.UserInfoID
WHERE UserInfo.UserId = @UserIdToMerge AND UserEvaluationLog.DeletedFlag = 0;
UPDATE ApplicationWorkflowStepAssignment SET UserId = @UserIdToKeep
WHERE UserId = @UserIdToMerge AND DeletedFlag = 0;
--Next soft delete the related data that doesn't need merged
UPDATE UserAlternateContactPhone SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
FROM UserAlternateContactPhone
	INNER JOIN UserAlternateContact ON UserAlternateContactPhone.UserAlternateContactId = UserAlternateContact.UserAlternateContactId
	INNER JOIN UserInfo ON UserAlternateContact.UserInfoId = UserInfo.UserInfoID
WHERE UserAlternateContactPhone.DeletedFlag = 0 AND UserInfo.UserID = @UserIdToMerge;
UPDATE UserInfoChangeLog SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
FROM UserInfoChangeLog
	INNER JOIN UserInfo ON UserInfoChangeLog.UserInfoId = UserInfo.UserInfoID
WHERE UserInfoChangeLog.DeletedFlag = 0 AND UserInfo.UserID = @UserIdToMerge;
UPDATE UserAccountStatusChangeLog SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
WHERE UserId = @UserIdToMerge AND DeletedFlag = 0;
UPDATE UserAccountRecovery SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
WHERE UserId = @UserIdToMerge AND DeletedFlag = 0;
UPDATE UserAccountStatus SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
WHERE UserId = @UserIdToMerge AND DeletedFlag = 0;
UPDATE UserEmail SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
FROM UserEmail	
	INNER JOIN UserInfo ON UserEmail.UserInfoId = UserInfo.UserInfoID
WHERE UserEmail.DeletedFlag = 0 AND UserInfo.UserID = @UserIdToMerge;
UPDATE UserDegree SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
FROM UserDegree
	INNER JOIN UserInfo ON UserDegree.UserInfoId = UserInfo.UserInfoID
WHERE UserDegree.DeletedFlag = 0 AND UserInfo.UserID = @UserIdToMerge;
UPDATE UserWebsite SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
FROM UserWebsite
	INNER JOIN UserInfo ON UserWebsite.UserInfoId = UserInfo.UserInfoID
WHERE UserWebsite.DeletedFlag = 0 AND UserInfo.UserID = @UserIdToMerge;
UPDATE UserResume SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
FROM UserResume
	INNER JOIN UserInfo ON UserResume.UserInfoId = UserInfo.UserInfoID
WHERE UserResume.DeletedFlag = 0 AND UserInfo.UserID = @UserIdToMerge;
UPDATE UserAlternateContact SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
FROM UserAlternateContact
	INNER JOIN UserInfo ON UserAlternateContact.UserInfoId = UserInfo.UserInfoID
WHERE UserAlternateContact.DeletedFlag = 0 AND UserInfo.UserID = @UserIdToMerge;
UPDATE UserAddress SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
FROM UserAddress
	INNER JOIN UserInfo ON UserAddress.UserInfoId = UserInfo.UserInfoID
WHERE UserAddress.DeletedFlag = 0 AND UserInfo.UserID = @UserIdToMerge;
UPDATE UserPhone SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
FROM UserPhone
	INNER JOIN UserInfo ON UserPhone.UserInfoId = UserInfo.UserInfoID
WHERE UserPhone.DeletedFlag = 0 AND UserInfo.UserID = @UserIdToMerge;
UPDATE UserClient SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
WHERE UserClient.DeletedFlag = 0 AND UserClient.UserID = @UserIdToMerge;
UPDATE UserSystemRole SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
WHERE UserSystemRole.DeletedFlag = 0 AND UserSystemRole.UserID = @UserIdToMerge;
UPDATE UserInfo SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
WHERE UserInfo.DeletedFlag = 0 AND UserInfo.UserID = @UserIdToMerge;
UPDATE [User] SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
WHERE DeletedFlag = 0 AND UserID = @UserIdToMerge;
UPDATE [User] SET PersonID = CASE WHEN [User].PersonID IS NULL AND DeletedUser.PersonID IS NOT NULL THEN [DeletedUser].PersonID ELSE [User].PersonID END
FROM [User] 
CROSS JOIN [User] DeletedUser
WHERE [User].UserID = @UserIdToKeep AND DeletedUser.UserID = @UserIdToMerge;
END
