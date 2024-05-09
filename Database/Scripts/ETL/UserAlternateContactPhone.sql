INSERT INTO [UserAlternateContactPhone]
           ([UserAlternateContactId]
           ,[PhoneTypeId]
           ,[PhoneNumber]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT UserAlternateContact.UserAlternateContactId, 3, PPL_People.Alt_Phone, UserAlternateContact.ModifiedBy, UserAlternateContact.ModifiedDate
FROM UserAlternateContact INNER JOIN
UserInfo ON UserAlternateContact.UserInfoId = UserInfo.UserInfoID INNER JOIN
[User] U ON UserInfo.UserID = U.UserID INNER JOIN
[$(P2RMIS)].dbo.PPL_People PPL_People ON U.PersonID = PPL_People.Person_ID
WHERE PPL_People.Alt_Phone IS NOT NULL AND LTRIM(RTRIM(PPL_People.Alt_Phone)) <> ''
UNION ALL
SELECT UserAlternateContact.UserAlternateContactId, 4, PPL_People.Alt_Fax, UserAlternateContact.ModifiedBy, UserAlternateContact.ModifiedDate
FROM UserAlternateContact INNER JOIN
UserInfo ON UserAlternateContact.UserInfoId = UserInfo.UserInfoID INNER JOIN
[User] U ON UserInfo.UserID = U.UserID INNER JOIN
[$(P2RMIS)].dbo.PPL_People PPL_People ON U.PersonID = PPL_People.Person_ID
WHERE PPL_People.Alt_Fax IS NOT NULL AND LTRIM(RTRIM(PPL_People.Alt_Fax)) <> ''