
-- Import data from 1.0 for Consumer Management Module
insert into NomineeSponsorPhone (NomineeSponsorId, PhoneTypeId, Phone, Extension,	PrimaryFlag,	CreatedBy, CreatedDate, ModifiedBy,	ModifiedDate, DeletedFlag, DeletedBy, DeletedDate)
select  SID, 9 PhoneTypeId, Phone_Work, null Extension, 1 PrimaryFlag, 
	null CreatedBy, null CreatedDate, null ModifiedBy, null ModifiedDate, 0 DeletedFlag, 
null DeletedBy, null DeletedDate
from [$(P2RMIS)].[dbo].CON_Nominee_Sponsor a
join [$(P2RMIS)].[dbo].CON_Nominee b on a.NomineeId = b.NomineeId
where fy > 1999 and nominee_Type <> 'Deceased'
