﻿SET IDENTITY_INSERT [MilitaryRank] ON

MERGE INTO [MilitaryRank] AS Target
USING (VALUES
  (1,'1LT','First Lieutenant','Army',43)
 ,(2,'1st Lt','First Lieutenant','Air Force',14)
 ,(3,'1stLt','First Lieutenant','Marines',100)
 ,(4,'2d Lt','Second Lieutenant','Air Force',13)
 ,(5,'2LT','Second Lieutenant','Army',42)
 ,(6,'2ndLt','Second Lieutenant','Marines',99)
 ,(7,'ADM','Admiral Chief of Naval Ops/Commandant of the CG','Navy',133)
 ,(8,'BG','Brigadier General','Army',48)
 ,(9,'BGen','Brigadier General','Marines',105)
 ,(10,'Brig Gen','Brigadier General','Air Force',19)
 ,(11,'Capt','Captain','Air Force',15)
 ,(12,'Capt','Captain','Marines',101)
 ,(13,'CAPT','Captain','Navy',129)
 ,(14,'CDR','Commander','Navy',128)
 ,(15,'Col','Colonel','Air Force',18)
 ,(16,'COL','Colonel','Army',47)
 ,(17,'Col','Colonel','Marines',104)
 ,(18,'CPT','Captain','Army',44)
 ,(19,'ENS','Ensign','Navy',124)
 ,(20,'FADM','Fleet Admiral','Navy',134)
 ,(21,'GA','General of the Army','Army',52)
 ,(22,'Gen','General Air Force Chief of Staff','Air Force',22)
 ,(23,'GEN','General','Army',51)
 ,(24,'Gen','General','Marines',108)
 ,(25,'LCDR','Lieutenant Commander','Navy',127)
 ,(26,'LT','Lieutenant','Navy',126)
 ,(27,'Lt Col','Lieutenant Colonel','Air Force',17)
 ,(28,'Lt Gen','Lieutenant General','Air Force',21)
 ,(29,'LTC','Lieutenant Colonel','Army',46)
 ,(30,'LtCol','Lieutenant Colonel','Marines',103)
 ,(31,'LTG','Lieutenant General','Army',50)
 ,(32,'LtGen','Lieutenant General','Marines',107)
 ,(33,'LTJG','Lieutenant Junior Grade','Navy',125)
 ,(34,'Maj','Major','Air Force',16)
 ,(35,'MAJ','Major','Army',45)
 ,(36,'Maj','Major','Marines',102)
 ,(37,'Maj Gen','Major General','Air Force',20)
 ,(38,'MajGen','Major General','Marines',106)
 ,(39,'MG','Major General','Army',49)
 ,(40,'RDML','Rear Admiral (Lower Half)','Navy',130)
 ,(41,'RADM','Rear Admiral (Upper Half)','Navy',131)
 ,(42,'VADM','Vice Admiral','Navy',132)
 ,(43,'ADM','Admiral Chief of Naval Ops / Commandant of the CG','Coast Guard',81)
 ,(44,'CAPT','Captain','Coast Guard',77)
 ,(45,'CDR','Commander','Coast Guard',76)
 ,(46,'ENS','Ensign','Coast Guard',72)
 ,(47,'LCDR','Lieutenant Commander','Coast Guard',75)
 ,(48,'LT','Lieutenant','Coast Guard',74)
 ,(49,'LTJG','Lieutenant Junior Grade','Coast Guard',73)
 ,(50,'RDML','Rear Admiral (Lower Half)','Coast Guard',78)
 ,(51,'RADM','Rear Admiral (Upper Half)','Coast Guard',79)
 ,(52,'VADM','Vice Admiral','Coast Guard',80)
 ,(53,'ADM','Admiral','US Public Health Service',144)
 ,(54,'VADM','Vice Admiral','US Public Health Service',143)
 ,(55,'RADM','Rear Admiral','US Public Health Service',142)
 ,(56,'RADM (PHS)','Rear Admiral (lower half)','US Public Health Service',141)
 ,(57,'CAPT','Captain','US Public Health Service',140)
 ,(58,'CDR','Commander','US Public Health Service',139)
 ,(59,'LCDR','Lieutenant Commander','US Public Health Service',138)
 ,(60,'LT','Lieutenant','US Public Health Service',137)
 ,(61,'LTJG','Lieutenant (junior grade)','US Public Health Service',136)
 ,(62,'ENS','Ensign','US Public Health Service',135)
 ,(63,'Cpl','Corporal','Marines',85)
 ,(64,'MSgt','Master Sergeant','Marines',89)
 ,(65,'AB','Airman Basic','Air Force',1)
 ,(66,'Amn','Airman','Air Force',2)
 ,(67,'A1C','Airman First Class','Air Force',3)
 ,(68,'SrA','Senior Airman','Air Force',4)
 ,(69,'SSgt','Staff Sergeant','Air Force',5)
 ,(70,'TSgt','Technical Sergeant','Air Force',6)
 ,(71,'MSgt','Master Sergeant','Air Force',7)
 ,(72,'1stSgt','First Sergeant','Air Force',8)
 ,(73,'SMSgt','Senior Master Sergeant','Air Force',9)
 ,(74,'CMSgt','Chief Master Sergeant','Air Force',10)
 ,(75,'CCM','Command Chief Master Sergeant','Air Force',11)
 ,(76,'CMSAF','Chief Master Sergeant of the Air Force','Air Force',12)
 ,(77,'GAF','General of the Air Force','Air Force',23)
 ,(78,'PVT','Private','Army',24)
 ,(79,'PV2','Private 2','Army',25)
 ,(80,'PFC','Private First Class','Army',26)
 ,(81,'SPC','Specialist','Army',27)
 ,(82,'CPL','Corporal','Army',28)
 ,(83,'SGT','Sergeant','Army',29)
 ,(84,'SSG','Staff Sergeant','Army',30)
 ,(85,'SFC','Sergeant First Class','Army',31)
 ,(86,'MSG','Master Sergeant','Army',32)
 ,(87,'1SG','First Sergeant','Army',33)
 ,(88,'SGM','Sergeant Major','Army',34)
 ,(89,'CSM','Command Sergeant Major','Army',35)
 ,(90,'SMA','Sergeant Major of the Army','Army',36)
 ,(91,'WO1','Warrant Officer','Army',37)
 ,(92,'CW2','Chief Warrant Officer 2','Army',38)
 ,(93,'CW3','Chief Warrant Officer 3','Army',39)
 ,(94,'CW4','Chief Warrant Officer 4','Army',40)
 ,(95,'CW5','Chief Warrant Officer 5','Army',41)
 ,(96,'SR','Seaman Recruit','Coast Guard',53)
 ,(97,'SA','Seaman Apprentice','Coast Guard',54)
 ,(98,'FA','Fireman Apprentice','Coast Guard',55)
 ,(99,'AA','Airman Apprentice','Coast Guard',56)
 ,(100,'SN','Seaman','Coast Guard',57)
 ,(101,'FN','Fireman','Coast Guard',58)
 ,(102,'AN','Airman','Coast Guard',59)
 ,(103,'PO3','Petty Officer 3rd Class','Coast Guard',60)
 ,(104,'PO2','Petty Officer 2nd Class','Coast Guard',61)
 ,(105,'PO1','Petty Officer 1st Class','Coast Guard',62)
 ,(106,'CPO','Chief Petty Officer','Coast Guard',63)
 ,(107,'SCPO','Senior Chief Petty Officer','Coast Guard',64)
 ,(108,'MCPO','Master Chief Petty Officer','Coast Guard',65)
 ,(109,'CMC','Command Master Chief Petty Officer','Coast Guard',66)
 ,(110,'MCPOCG','Master Chief Petty Officer of the Coast Guard','Coast Guard',67)
 ,(111,'CWO2','Chief Warrant Officer 2','Coast Guard',68)
 ,(112,'CWO3','Chief Warrant Officer 3','Coast Guard',69)
 ,(113,'CWO4','Chief Warrant Officer 4','Coast Guard',70)
 ,(114,'CWO5','Chief Warrant Officer 5','Coast Guard',71)
 ,(115,'Pvt','Private','Marines',82)
 ,(116,'PFC','Private First Class','Marines',83)
 ,(117,'LCpL','Lance Corporal','Marines',84)
 ,(118,'Sgt','Sergeant','Marines',86)
 ,(119,'SSgt','Staff Sergeant','Marines',87)
 ,(120,'GySgt','Gunnery Sergeant','Marines',88)
 ,(121,'1stSgt','First Sergeant','Marines',90)
 ,(122,'MGySgt','Master Gunnery Sergeant','Marines',91)
 ,(123,'SgtMaj','Sergeant Major','Marines',92)
 ,(124,'SgtMaj MarCor','Sergeant Major of the Marine Corps','Marines',93)
 ,(125,'WO','Warrant Officer','Marines',94)
 ,(126,'CWO2','Chief Warrant Officer 2','Marines',95)
 ,(127,'CWO3','Chief Warrant Officer 3','Marines',96)
 ,(128,'CWO4','Chief Warrant Officer 4','Marines',97)
 ,(129,'CWO5','Chief Warrant Officer 5','Marines',98)
 ,(130,'SR','Seaman Recruit','Navy',109)
 ,(131,'SA','Seaman Apprentice','Navy',110)
 ,(132,'SN','Seaman','Navy',111)
 ,(133,'PO3','Petty Officer 3rd Class','Navy',112)
 ,(134,'PO2','Petty Officer 2nd Class','Navy',113)
 ,(135,'PO1','Petty Officer 1st Class','Navy',114)
 ,(136,'CPO','Chief Petty Officer','Navy',115)
 ,(137,'SCPO','Senior Chief Petty Officer','Navy',116)
 ,(138,'MCPO','Master Chief Petty Officer','Navy',117)
 ,(139,'FLTCM','Fleet/Commander Master Chief Petty Officer','Navy',118)
 ,(140,'MCPON','Master Chief Petty Officer of the Navy','Navy',119)
 ,(141,'CWO2','Chief Warrant Officer 2','Navy',120)
 ,(142,'CWO3','Chief Warrant Officer 3','Navy',121)
 ,(143,'CWO4','Chief Warrant Officer 4','Navy',122)
 ,(144,'CWO5','Chief Warrant Officer 5','Navy',123)
) AS Source ([MilitaryRankId],[MilitaryRankAbbreviation],[MilitaryRankName],[Service],[SortOrder])
ON (Target.[MilitaryRankId] = Source.[MilitaryRankId])
WHEN MATCHED AND (
	NULLIF(Source.[MilitaryRankAbbreviation], Target.[MilitaryRankAbbreviation]) IS NOT NULL OR NULLIF(Target.[MilitaryRankAbbreviation], Source.[MilitaryRankAbbreviation]) IS NOT NULL OR 
	NULLIF(Source.[MilitaryRankName], Target.[MilitaryRankName]) IS NOT NULL OR NULLIF(Target.[MilitaryRankName], Source.[MilitaryRankName]) IS NOT NULL OR 
	NULLIF(Source.[Service], Target.[Service]) IS NOT NULL OR NULLIF(Target.[Service], Source.[Service]) IS NOT NULL OR 
	NULLIF(Source.[SortOrder], Target.[SortOrder]) IS NOT NULL OR NULLIF(Target.[SortOrder], Source.[SortOrder]) IS NOT NULL) THEN
 UPDATE SET
  [MilitaryRankAbbreviation] = Source.[MilitaryRankAbbreviation], 
  [MilitaryRankName] = Source.[MilitaryRankName], 
  [Service] = Source.[Service], 
  [SortOrder] = Source.[SortOrder]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([MilitaryRankId],[MilitaryRankAbbreviation],[MilitaryRankName],[Service],[SortOrder])
 VALUES(Source.[MilitaryRankId],Source.[MilitaryRankAbbreviation],Source.[MilitaryRankName],Source.[Service],Source.[SortOrder])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [MilitaryRank]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[MilitaryRank] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [MilitaryRank] OFF
GO