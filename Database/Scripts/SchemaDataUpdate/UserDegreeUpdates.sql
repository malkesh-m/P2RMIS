--We totally wipe all records and re-import for simplicity
DELETE FROM UserDegree WHERE LegacyDegreeId IS NOT NULL;
INSERT INTO UserDegree
           ([UserInfoId]
           ,[DegreeId]
           ,[LegacyDegreeId]
           ,[Major]
		   ,[CreatedDate]
           ,[CreatedBy]
		   ,[ModifiedDate]
           ,[ModifiedBy])
SELECT UserInfo.UserInfoID, CASE WHEN ld.DegreeID IS NULL THEN 1 ELSE ld.DegreeID END AS Expr1, pd.Degree_ID, pd.Major, 
               people.LAST_UPDATE_DATE,  u.UserID, people.LAST_UPDATE_DATE,  u.UserID
FROM  Degree ld INNER JOIN
               [$(P2RMIS)].dbo.PPL_Degrees AS pd ON ld.DegreeName = CASE WHEN pd.Degree = 'M.D.,Ph.D.' THEN 'M.D./Ph.D.' ELSE pd.Degree END INNER JOIN
               UserInfo  INNER JOIN
               [User] AS u ON UserInfo.UserID = u.UserID INNER JOIN
               [$(P2RMIS)].dbo.PPL_People AS people ON u.PersonID = people.Person_ID ON pd.Person_ID = people.Person_ID;