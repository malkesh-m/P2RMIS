UPDATE UserInfo
SET AcademicRankId = AcademicRank.AcademicRankId
FROM [UserInfo] INNER JOIN
[User] ON UserInfo.UserID = [User].UserID INNER JOIN
[$(P2RMIS)].dbo.PPL_General_Info PPL_General_Info ON [User].PersonID = PPL_General_Info.Person_ID INNER JOIN
[AcademicRank] ON PPL_General_Info.Academic_Rank = AcademicRank.Rank
WHERE UserInfo.AcademicRankId IS NULL 