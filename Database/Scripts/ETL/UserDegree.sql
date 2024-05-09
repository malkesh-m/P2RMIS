
--Existing data
Insert into  [dbo].[UserDegree]

([UserInfoID], 
[DegreeId], 
[CreatedDate], 
[CreatedBy],
[ModifiedDate],
[ModifiedBy]
)

SELECT ui.UserInfoID, d.DegreeID, ui.CreatedDate, ui.CreatedBy, ui.ModifiedDate, ui.ModifiedBy
FROM  UserInfo ui LEFT OUTER JOIN
LookupDegree ld ON ui.DegreeLkpID = ld.DegreeID INNER JOIN
Degree d ON ld.DegreeName = d.DegreeName LEFT OUTER JOIN
               UserDegree ud ON ui.UserInfoID = ud.UserInfoId
WHERE (NOT (ui.DegreeLkpID IS NULL));

---from old P2RMIS
Insert into  [dbo].[UserDegree]

([UserInfoID], 
[DegreeId], 
[LegacyDegreeId], 
[Major], 
[CreatedDate], 
[CreatedBy],
[ModifiedDate],
[ModifiedBy]
)
SELECT UserInfo.UserInfoID, CASE WHEN ld.DegreeID IS NULL THEN 1 ELSE ld.DegreeID END AS Expr1, pd.Degree_ID, pd.Major, 
               people.LAST_UPDATE_DATE,  u.UserID, people.LAST_UPDATE_DATE,  u.UserID
FROM  Degree ld RIGHT OUTER JOIN
               [$(P2RMIS)].dbo.PPL_Degrees AS pd ON ld.DegreeName = pd.Degree FULL OUTER JOIN
               UserInfo INNER JOIN
               [User] AS u ON UserInfo.UserID = u.UserID FULL OUTER JOIN
               [$(P2RMIS)].dbo.PPL_People AS people ON u.PersonID = people.Person_ID ON pd.Person_ID = people.Person_ID
WHERE (u.UserID IS NOT NULL) AND (NOT (pd.Degree_ID IS NULL))