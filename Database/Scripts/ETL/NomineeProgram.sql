-- Import data from 1.0 for Consumer Management Moduleinsert into NomineeProgram (PrimaryFlag, NomineeId, LegacyNomineeId, NomineeSponsorId, ProgramYearId, NomineeTypeId, Score, NomineeAffectedId, 
insert into NomineeProgram (PrimaryFlag, NomineeId, LegacyNomineeId, NomineeSponsorId, ProgramYearId, NomineeTypeId, Score, NomineeAffectedId, 
	DiseaseSite, Comments, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, 
	DeletedFlag, DeletedBy, DeletedDate)
select 
	case 
		when a.nomineeID In (816,582  , 3216, 2519,1751,868,1028,2801,1027, 3298, 1737,516,322,1666,872,1193 , 1336, 908  ,1059,697,
			1126,1282,1560,1272,332, 643 ,701, 2810,1041,1003,
			1067,1519 ,2530 ,1818,581 , 2700 ,1969,430,2733,453  ,853,1374 ,
			2146,702 , 2208 ,1219 , 2181 ,1461 , 3029,1043,1042,564,
			1079 , 1466,1333,930,569,1082, 3130  ,703 , 2095, 419, 
			2615 ,421,1001, 2411, 561,726 ) then 0 else 1 end PrimaryFlag,
	Case
		when a.nomineeId = 816  then 1760			when a.NomineeId = 582 then 911
		when a.nomineeId = 3216 then 3217			when a.nomineeId = 2519 then 2525		
		when a.NomiNeeId = 1751 then 2547			when a.NomineeId = 868 then 875		
		when a.NomineeId = 1028 then 934			when a.NomineeId = 2801 then 5395		
		when a.NomineeId = 1027 then 1734			when a.NomineeId = 3298 then 3299
		when a.NomineeId = 1737 then 2056			when a.NomineeId = 516 then 804		
		when a.NomineeId = 322 then 979				when a.NomineeId = 1666 then 1819		
		when a.NomineeId = 872 then 1819			when a.NomineeId = 1193 then 2705	
		when a.NomineeId = 1336 then 1384			when a.NomineeId = 908 then 1503		
		when a.NomineeId = 1059 then 455			when a.NomineeId = 697 then 1496		
		when a.NomineeId = 1126 then 1283			when a.NomineeId = 1282 then 1187		
		when a.NomineeId = 1560 then 1327			when a.NomineeId = 1272 then 2180		
		when a.NomineeId = 332 then 451				when a.NomineeId = 643 then 1428		
		when a.NomineeId = 701 then 885				when a.NomineeId = 2810 then 2805
		when a.NomineeId = 1041 then 2197			when a.NomineeId = 1003 then 1298		
		when a.NomineeId = 1067 then 1298			when a.NomineeId = 1519 then 1684	
		when a.NomineeId = 2530 then 2531			when a.NomineeId = 1818 then 1718 
		when a.NomineeId = 581 then 928				when a.NomineeId = 2700 then 2696 
		when a.NomineeId = 1969 then 1723			when a.NomineeId = 430 then 2354 
		when a.NomineeId = 2733 then 2312 			when a.NomineeId = 453 then 1406 
		when a.NomineeId = 853 then 939  			when a.NomineeId = 1374 then 1371 
		when a.NomineeId = 2146 then 415			when a.NomineeId = 702 then 888 
		when a.NomineeId = 2208 then 2207			when a.NomineeId = 1219 then 1368 
		when a.NomineeId = 2181 then 2205			when a.NomineeId = 1461 then 512
		when a.NomineeId = 3029 then 3030			when a.NomineeId = 1043 then 1271 
		when a.NomineeId = 1042 then 1478			when a.NomineeId = 564 then 878 
		when a.NomineeId = 1079 then 405			when a.NomineeId = 1466 then 1501
		when a.NomineeId = 1333 then 1975			when a.NomineeId = 930 then 637 
		when a.NomineeId = 569 then 905				when a.NomineeId = 1082 then 1256 
		when a.NomineeId = 3130 then 3131			when a.NomineeId = 703 then 886 
		when a.NomineeId = 2095 then 2105			when a.NomineeId = 419 then	1690 
		when a.NomineeId = 2615 then	2616		when a.NomineeId = 421 then 1090 
		when a.NomineeId = 1001 then 1203			when a.NomineeId = 2411 then 1546 
		when a.NomineeId = 561 then 1735 			when a.NomineeId = 726 then 1300
else a.NomineeID end NomineeId, 
a.NomineeId LNID, s.SID, c.ProgramYearId,
	case 	
		when  a.Nominee_Type in ('Selected Novice') then 2 -- Seletecd Novice
		when  a.Nominee_Type in ('Ineligible NomineeIN','Ineligible NomineeLS','Brochure Nominee','Rolling Nominee 1st','Ineligible NomineeNS','Replacement','Drop-Out','2nd Year Rolling Nom','Rolling Nominee 2nd') then 3 -- Inelgible Nominee
		else 1 -- Eligible Nominee
	end NomineeTypeId, 
	case 
		when score is null or score = '' or score = 'TBI' or score = 'p' then 0 
		when score = '10,0' then 10
		when score in ('9,0' , '920' ,  '917' , '911' , '888' , '873' , '867' , '852') then  9
		when score in ( '827', '816','811','8047','780') then 8
		when score = '656' then 7
		else convert(int, round(score, 0)) 
	end score, 
	case	
		when a.Affected in ('Self','S       ') then 1 -- Self
		when a.Affected in ('Self & Family','Self & F') then 2 --Self & Family
		when a.Affected in ('Family') then 3 
		else NULL
	end NomineeAffectedId, 
a.Disease_Site , a.Comments, NULL CreatedBy, a.Date_Entry CreatedDate, v.PersonID ModifiedBy, a.LAST_UPDATE_DATE ModifiedDate, 0 DeletedFlag, NULL DeletedBy, NULL DeletedDate
from ClientProgram b
left join ProgramYear c on  c.ClientProgramId  = b.ClientProgramId
right join (select Nominee_Type, NomineeID, FY, Date_Entry, LAST_UPDATE_DATE, score, Affected, Disease_Site, Comments
 				,case
					when program = 'GWVIRP' then  'GWIRP'	
					when program = 'PTSD-TBI' then 'PH-TBI' 
					when program = 'TSRP' then 'TSCRP' 
					when program =  'ASDRP' then 'ARP'
					when program = 'ASARP' then 'ASADRP'
				else program end Program 
			from [$(P2RMIS)].[dbo].CON_Nominee) a  on a.PROGRAM = b.ProgramAbbreviation and c.Year = a.FY
left join [$(P2RMIS)].[dbo].CON_Nominee_Member m on m.NomineeID = a.NomineeID
join [$(P2RMIS)].[dbo].CON_Nominee_Sponsor s on a.NomineeID = s.NomineeID
left join [User] v on v.PersonID = m.PersonID and v.DeletedFlag = 0
where a.fy > 1999 and nominee_Type <> 'Deceased'
