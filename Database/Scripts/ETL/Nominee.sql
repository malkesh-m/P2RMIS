-- Import data from 1.0 for Consumer Management Module
SET IDENTITY_INSERT Nominee ON
insert into Nominee(NomineeId, PrefixId, LastName, MiddleName, FirstName, 
	GenderId, EthnicityId, UserId, Email, DOB, Address1, Address2, City, StateId, ZipCode, CountryId,
	CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, DeletedFlag, DeletedBy, DeletedDate)
select  a.NomineeID,
	case
		when a.Nominee_Prefix = 'Dr.' then 1
		when a.Nominee_Prefix = 'Mr.' then 3
		when a.Nominee_Prefix = 'Ms.' then 5
		else NULL
	end PrefixId,
	LName, a.MI, FName,
	case
		when a.Gender = 'Female' then 1
		when a.Gender = 'Male' then 2
		else NULL
	end GenderId,
	case
		when a.Ethnicity in ('Jewish Ashkenazi','OT','Other') then 9 -- Other
		when a.Ethnicity in ('WH',' White','White','6','American Indian or Alaskan Native') then 8 --White
		when a.Ethnicity in ('NHPI') then 7 -- Native Hawaiian or Pacific Islander
		when a.Ethnicity in ('Hispanic','HL','Hispanic or Latino') then 6 -- Hispanic or Latino
		when a.Ethnicity in ('African-American','Black or African American','4','Black','AA','African American') then 5 -- Black or African American	
		when a.Ethnicity in ('Asian','AS') then 4 -- Asian
		when a.Ethnicity in ('AI') then 3 -- American Indian		
		when a.Ethnicity in ('AN') then 2 -- Alaskan Native		
		else 1 -- None
	end EthnicityId,
	u.UserID, a.Email, a.DOB, t.Address1, t.Address2, t.City,	State.StateId, Zip_Code, Country.CountryId, NULL CreatedBy, a.Date_Entry CreatedDate, v.PersonID ModifiedBy, a.LAST_UPDATE_DATE ModifiedDate, 	0 as DeletedFlag, NULL DeletedBy, NULL DeletedDate
from [$(P2RMIS)].[dbo].CON_Nominee a 
left join [$(P2RMIS)].[dbo].CON_Nominee_Member m on m.NomineeID = a.NomineeID
left join [User] u on u.PersonID = m.PersonID and u.DeletedFlag = 0
join [$(P2RMIS)].[dbo].CON_Nominee_Contact t on t.NomineeID = a.NomineeID
left join [State] State on State.StateAbbreviation = t.State
left join country Country on Country.countryAbbreviation = t.country 
left join [User] v on v.PersonID = m.PersonID and v.DeletedFlag = 0
where a.FY > 1999 and nominee_Type <> 'Deceased'
and a.nomiNeeId not in 
(816,582  , 3216, 2519,1751,868,1028,2801,1027, 3298, 1737,516,322,1666,872,1193 , 1336, 908  ,1059,697,
1126,1282,1560,1272,332, 643 ,701, 2810,1041,1003,
1067,1519 ,2530 ,1818,581 , 2700 ,1969,430,2733,453  ,853,1374 ,
2146,702 , 2208 ,1219 , 2181 ,1461 , 3029,1043,1042,564,
1079 , 1466,1333,930,569,1082, 3130  ,703 , 2095, 419, 
2615 ,421,1001, 2411, 561,726 )
SET IDENTITY_INSERT Nominee OFF
