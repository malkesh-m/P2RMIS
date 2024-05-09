-- Import data from 1.0 for Consumer Management Module
insert into NomineePhone (NomineeId, PhoneTypeId, Phone, Extension,	PrimaryFlag,
	CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, DeletedFlag, DeletedBy, DeletedDate)
select  a.NomineeId, PhoneTypeId , a.phone, null Extension, 0 PrimaryFlag,
null CreatedBy, null CreatedDate, u.userId ModifiedBy, c.LAST_UPDATE_DATE ModifiedDate, 0 DeletedFlag, 
null DeletedBy, null DeletedDate
from (
	select nomineeid, phone, 
		case when PhoneType = 'Phone_Home' then 6 else 3 end PhoneTypeId,
		case when PhoneType = 'Phone_Home' then 1 else 0 end PrimaryFlag
	from [$(P2RMIS)].[dbo].CON_Nominee_Contact 
	unpivot ( 
          phone for phonetype in (Phone_Work, Phone_Home)
	) unpiv ) a
join [$(P2RMIS)].[dbo].CON_Nominee_Contact c on a.NomineeID = c.NomineeID
join [$(P2RMIS)].[dbo].CON_Nominee b on a.NomineeId = b.NomineeId
left join [$(P2RMIS)].[dbo].CON_Nominee_Member m on m.NomineeID = a.NomineeID
left join [User] u on u.PersonID = m.PersonID and u.DeletedFlag = 0
where b.fy > 1999 and b.nominee_Type <> 'Deceased'
and a.nomineeID not in 
(816,582  , 3216, 2519,1751,868,1028,2801,1027, 3298, 1737,516,322,1666,872,1193 , 1336, 908  ,1059,697,
1126,1282,1560,1272,332, 643 ,701, 2810,1041,1003,
1067,1519 ,2530 ,1818,581 , 2700 ,1969,430,2733,453  ,853,1374 ,
2146,702 , 2208 ,1219 , 2181 ,1461 , 3029,1043,1042,564,
1079 , 1466,1333,930,569,1082, 3130  ,703 , 2095, 419, 
2615 ,421,1001, 2411, 561,726 )