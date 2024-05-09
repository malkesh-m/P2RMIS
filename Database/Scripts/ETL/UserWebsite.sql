INSERT INTO [UserWebsite]
           ([UserInfoId]
           ,[WebsiteTypeId]
           ,[WebsiteAddress]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT UserInfo.UserInfoID, 1, PPL_People.Personal_Url, UserInfo.ModifiedBy, UserInfo.ModifiedDate
FROM UserInfo INNER JOIN
[User] U ON UserInfo.UserID = U.UserID INNER JOIN
[$(P2RMIS)].dbo.PPL_People PPL_People ON U.PersonID = PPL_People.Person_ID
WHERE PPL_People.Personal_Url IS NOT NULL AND LTRIM(RTRIM(PPL_People.Personal_Url)) <> ''