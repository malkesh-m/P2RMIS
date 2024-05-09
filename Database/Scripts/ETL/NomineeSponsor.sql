-- Import data from 1.0 for Consumer Management Module
SET IDENTITY_INSERT NomineeSponsor ON
 insert into NomineeSponsor(NomineeSponsorId, LegacyNomineeId, Organization, LastName, FirstName, 
	Email, Title, Address1, Address2, City, StateId, ZipCode, CountryId,
	CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, DeletedFlag, DeletedBy, DeletedDate)
select a.SID, 
a.NomineeId, 
Nominating_Org, 
a.LName, a.FName, a.Email, title, Address1, Address2, City, State.StateId, Zip_Code, Country.CountryId, NULL CreatedBy, NULL CreatedDate, v.PersonID ModifiedBy, a.LAST_UPDATE_DATE ModifiedDate, 0 DeletedFlag, NULL DeletedBy, NULL DeletedDate
from [$(P2RMIS)].[dbo].CON_Nominee_Sponsor a 
join [$(P2RMIS)].[dbo].CON_Nominee b on a.NomineeId = b.NomineeId
left join [$(P2RMIS)].[dbo].CON_Nominee_Member m on m.NomineeID = a.NomineeID
left join State on State.StateAbbreviation = a.State
left join Country on Country.CountryAbbreviation = a.Country and CountryAbbreviation = 'US'
left join [User] v on v.PersonID = m.PersonID and v.DeletedFlag = 0
where b.fy > 1999 and nominee_Type <> 'Deceased'
SET IDENTITY_INSERT NomineeSponsor OFF