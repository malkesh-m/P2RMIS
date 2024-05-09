INSERT INTO [UserAlternateContact]
           ([UserInfoId]
           ,[AlternateContactTypeId]
           ,[FirstName]
           ,[LastName]
           ,[EmailAddress]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT UserInfo.UserInfoID, 1, SUBSTRING(Alt_Contact,1,CHARINDEX(' ',Alt_Contact)-1) FName, 
       SUBSTRING(Alt_Contact,CHARINDEX(' ',Alt_Contact)+1,LEN(Alt_Contact)) LName,
       CASE WHEN LTRIM(RTRIM(Alt_Email)) = '' THEN NULL ELSE Alt_Email END AltEmail,
	   UserInfo.ModifiedBy, UserInfo.ModifiedDate
FROM UserInfo INNER JOIN
[User] U ON UserInfo.UserID = U.UserID INNER JOIN
[$(P2RMIS)].dbo.PPL_People PPL_People ON U.PersonID = PPL_People.Person_ID
WHERE LTRIM(RTRIM(Alt_Contact)) <> '' AND CHARINDEX(' ', Alt_Contact) > 0