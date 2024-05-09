
Update   ue SET ue.ModifiedBy=ue.CreatedBy, ue.ModifiedDate=ue.CreatedDate
FROM  UserEmail ue INNER JOIN
               UserInfo ui ON ue.UserInfoID = ui.UserInfoID INNER JOIN
               [User]  u ON ui.UserID = u.UserID
WHERE (ue.ModifiedBy IS NULL) AND (ue.ModifiedDate IS NULL)




Insert into [dbo].[UserEmail]

([UserInfoID], 
[Email], 
[PrimaryFlag], 
[CreatedBy], 
[CreatedDate], 
[ModifiedBy], 
[ModifiedDate]


)
SELECT UserInfo.UserInfoID, people.Email, 1, un.UserID, people.LAST_UPDATE_DATE, un.UserID, people.LAST_UPDATE_DATE
FROM  UserInfo INNER JOIN
               [User] AS u ON UserInfo.UserID = u.UserID LEFT OUTER JOIN
               [$(P2RMIS)].dbo.PPL_People AS people ON u.PersonID = people.Person_ID LEFT OUTER JOIN
			   ViewLegacyUserNameToUserId un ON people.LAST_UPDATED_BY = un.UserName
WHERE (u.UserID IS NOT NULL) AND (NOT (people.Email IS NULL))