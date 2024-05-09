--Adds CPRIT client users to new role
Update UserSystemRole Set SystemRoleId = 29, ModifiedBy = 10, ModifiedDate = dbo.GetP2rmisDateTime()
WHERE DeletedFlag = 0 AND UserID IN (Select UserInfo.UserID 
FROM ViewUserInfo UserInfo INNER JOIN
ViewUserProfile ON UserInfo.UserInfoID = ViewUserProfile.UserInfoId INNER JOIN
ViewUserClient ON UserInfo.UserID = ViewUserClient.UserID
WHERE ViewUserProfile.ProfileTypeId = 4 AND ViewUserClient.ClientID = 9)