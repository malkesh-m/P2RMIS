/*Mapping rules
If user is staff then profile type is SRA Staff - 3
Else If user has Client Role then Client - 4
Else If user has participation then Reviewer - 2
Else User is prospect - 1
*/
INSERT INTO [UserProfile]
           ([UserInfoId]
           ,[ProfileTypeId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT UserInfo.UserInfoID, CASE WHEN Employee = 1 THEN 3 WHEN UserSystemRole.RoleLkpID IS NOT NULL THEN 4
	ELSE 1 END, UserInfo.ModifiedBy, UserInfo.ModifiedDate
FROM UserInfo INNER JOIN
[User] U ON UserInfo.UserID = U.UserID LEFT OUTER JOIN
[$(P2RMIS)].dbo.[PPL_People] PPL_People ON U.PersonID = PPL_People.Person_ID LEFT OUTER JOIN
(SELECT COUNT(*) AS TheCount, Person_ID
FROM [$(P2RMIS)].dbo.PRG_Participants
GROUP BY Person_ID) Part ON PPL_People.Person_ID = Part.Person_ID LEFT OUTER JOIN
UserSystemRole ON U.UserID = UserSystemRole.UserID AND UserSystemRole.RoleLkpID = 4