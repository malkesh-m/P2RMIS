--Set accounts to active and legacy status so they can log in off of 1.0
UPDATE UserAccountStatus SET AccountStatusId = 3, AccountStatusReasonId = 9
FROM UserAccountStatus INNER JOIN
[User] ON UserAccountStatus.UserId = [User].UserID INNER JOIN
[UserInfo] ON [User].UserId = UserInfo.UserID INNER JOIN
[UserProfile] ON [UserInfo].UserInfoID = UserProfile.UserInfoId
WHERE [User].[PersonID] IS NOT NULL AND
[UserAccountStatus].AccountStatusId = 13 AND
[UserAccountStatus].AccountStatusReasonId IN (1, 7) AND
[UserProfile].ProfileTypeId = 1
--Update profile type from prospect to review for these individuals
UPDATE UserProfile SET ProfileTypeId = 2
FROM UserProfile INNER JOIN
[UserInfo] ON UserProfile.UserInfoId = UserInfo.UserInfoID INNER JOIN
[UserAccountStatus] ON UserInfo.UserID = UserAccountStatus.UserId
WHERE UserAccountStatus.AccountStatusReasonId = 9