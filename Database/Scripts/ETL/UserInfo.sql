--Update existing records for new structure

  Update  ui SET ui.Institution=ua.Institution, ui.Department=ua.Department , ui.Position = ua.Position, ui.GenderId = ui.GenderLkpID 
	FROM  UserAddress AS ua INNER JOIN
    UserInfo AS ui ON ua.UserInfoID = ui.UserInfoID INNER JOIN
	UserInfo AS ui2 ON ui.UserInfoID = ui2.UserInfoID
	WHERE ua.AddressTypeLkpID = 2

-- insert p2rmisnet UserInfo table with data from old p2rmis ppl_people, and new p2rmis prefiex, military, gender and ethnicities tables
INSERT INTO [dbo].[UserInfo]
	([UserID]
	,[PrefixLkpID]
	,[MilitaryRankId]
	,[FirstName]
	,[MiddleName]
	,[LastName]
	,[Institution]
	,[Position]
	,[Department]
	,[GenderLkpID]
	,[PrefixId]
	,[GenderId]
	,[EthnicityId]
	,[BadgeName]
	,[FullName]
	,[CreatedDate]
	,[CreatedBy]
	,[ModifiedDate]
	,[ModifiedBy]
)

SELECT     u.UserID, lp.PrefixId, MilitaryRank.MilitaryRankId, people.Fname, people.MI, people.Lname, people.Institution, people.PTitle, people.Department, lg.GenderId, le.EthnicityId, lp.PrefixId AS Expr1, 
                      lg.GenderId AS Expr2, le.EthnicityId AS Expr3, people.Badge_Name, people.Fname + ' ' + people.Lname AS Expr4, u.ModifiedDate, u.ModifiedBy, 
                      u.ModifiedDate AS Expr5, u.ModifiedBy AS Expr6
FROM         Gender AS lg INNER JOIN
                      [$(P2RMIS)].dbo.PPL_General_Info AS general ON lg.Gender = general.Gender RIGHT OUTER JOIN
                      Prefix AS lp RIGHT OUTER JOIN
                      [$(P2RMIS)].dbo.PPL_People AS people ON lp.PrefixName = people.Prefix RIGHT OUTER JOIN
                      [User] AS u ON people.Person_ID = u.PersonID ON general.Person_ID = people.Person_ID LEFT OUTER JOIN
                      [$(P2RMIS)].dbo.PPL_Military_Rank_LU AS military INNER JOIN
                      MilitaryRank ON military.Service = MilitaryRank.Service AND military.Military_Rank = MilitaryRank.MilitaryRankAbbreviation ON 
                      people.Military_Rank = military.Rank_ID LEFT OUTER JOIN
                      Ethnicity AS le INNER JOIN
                      [$(P2RMIS)].dbo.PRG_Ethnicity AS ethn ON le.Ethnicity = ethn.Description ON general.Ethnicity = ethn.Ethnicity_ID
WHERE     (u.UserID IS NOT NULL) AND (NOT (u.PersonID IS NULL)) AND (u.UserID NOT IN (SELECT UserId FROM Userinfo))