INSERT INTO [dbo].[UserAccountStatus]
           ([UserId]
           ,[AccountStatusId]
           ,[AccountStatusReasonId]
           ,[CreatedDate]
           ,[ModifiedDate])
		   SELECT [User].UserID, CASE WHEN UserProfile.ProfileTypeId <> 1 AND [User].account_status_id = 11 THEN 3 WHEN [User].account_status_id IN (1,2,3,4,7) THEN 3 ELSE 13 END, CASE WHEN UserProfile.ProfileTypeId = 1 AND [User].account_status_id = 11 THEN 1 WHEN [User].account_status_id IN (1,4,7,11) THEN 2 WHEN [User].account_status_id = 5 THEN 3 WHEN [User].account_status_id = 9 THEN 4 WHEN [User].account_status_id = 8 THEN 5
		   WHEN [User].account_status_id IN (10, 12) THEN 6 WHEN [User].account_status_id = 6 THEN 7 ELSE 8 END, [User].account_status_date, [User].account_status_date
		   FROM [User] INNER JOIN
		   [UserInfo] ON [User].UserID = UserInfo.UserID INNER JOIN
		   [UserProfile] ON UserInfo.UserInfoID = UserProfile.UserInfoId
