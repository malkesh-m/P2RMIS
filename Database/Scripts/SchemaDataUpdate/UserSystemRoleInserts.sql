--INSERTS UserSystemRole values for reviewers as well as adds netsqlazman permissions
INSERT INTO [dbo].[UserSystemRole]
           ([UserID]
           ,[SystemRoleId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT [User].UserId, 12, 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime()
FROM [User] INNER JOIN
[UserInfo] ON [User].UserID = [UserInfo].UserID INNER JOIN
[UserProfile] ON [UserInfo].UserInfoId = UserProfile.UserInfoId
WHERE [UserProfile].ProfileTypeId = 1 AND [User].UserID NOT IN (Select UserId FROM UserSystemRole);


INSERT INTO [dbo].[UserClient]
           ([UserID]
           ,[ClientID]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[CreatedBy]
           ,[CreatedDate])
     SELECT [User].UserID, Client.ClientID, 10, '9/18/2015', 10, '9/18/2015'
     FROM [User] INNER JOIN
     [UserInfo] ON [User].UserID = UserInfo.UserID INNER JOIN
     [UserProfile] ON UserInfo.UserInfoID = UserProfile.UserInfoID CROSS JOIN
     [Client]
     WHERE UserProfile.ProfileTypeId = 1 AND [User].UserID NOT IN (
		SELECT UserId FROM UserClient)
