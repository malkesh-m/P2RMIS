UPDATE UserInfo
SET Expertise = PPL_General_Info.RCF_Expertise
FROM UserInfo INNER JOIN
[User] ON UserInfo.UserId = [User].UserId INNER JOIN
[$(P2RMIS)].[dbo].[PPL_General_Info] ON [User].PersonId = PPL_General_Info.Person_Id
WHERE Expertise IS NULL AND PPL_General_Info.RCF_Expertise IS NOT NULL