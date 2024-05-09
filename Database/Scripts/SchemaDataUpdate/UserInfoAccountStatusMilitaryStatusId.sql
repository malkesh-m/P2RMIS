--SET Inactive Users from the old system as Inactive-ineligible in current system
UPDATE [User]
SET account_status_id = 12
FROM [User] U INNER JOIN
[$(P2RMIS)].dbo.PPL_People PPL_People ON U.PersonID = PPL_People.Person_ID
WHERE PPL_People.Status = 'Inactive' OR PPL_People.Status = 'Deceased'
--SET everyone else who was mapped to Deactive to Inactive-Eligible
UPDATE [User]
SET account_status_id = 11
WHERE account_status_id = 6
--Update Military Status Id and Rank based on legacy P2RMIS Military Status
UPDATE [UserInfo]
SET MilitaryStatusTypeId = CASE WHEN PPL_People.Military_Active_Code = 'A' THEN 1 WHEN PPL_People.Military_Active_Code = 'R' THEN 2 ELSE NULL END,
MilitaryRankId = MilitaryRank.MilitaryRankId
FROM UserInfo INNER JOIN
[User] U ON UserInfo.UserID = U.UserID INNER JOIN
[$(P2RMIS)].dbo.PPL_People PPL_People ON U.PersonID = PPL_People.Person_ID INNER JOIN
[MilitaryRank] ON PPL_People.Military_Rank = MilitaryRank.MilitaryRankId