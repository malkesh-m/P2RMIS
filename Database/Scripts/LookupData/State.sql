SET IDENTITY_INSERT [State] ON

MERGE INTO [State] AS Target
USING (VALUES
  (1,'AK','ALASKA')
 ,(2,'AL','ALABAMA')
 ,(3,'AR','ARKANSAS')
 ,(4,'AS','AMERICAN SAMOA')
 ,(5,'AZ','ARIZONA')
 ,(6,'CA','CALIFORNIA')
 ,(7,'CO','COLORADO')
 ,(8,'CT','CONNECTICUT')
 ,(9,'DC','DISTRICT OF COLUMBIA')
 ,(10,'DE','DELAWARE')
 ,(11,'FL','FLORIDA')
 ,(12,'GA','GEORGIA')
 ,(13,'GU','GUAM')
 ,(14,'HI','HAWAII')
 ,(15,'IA','IOWA')
 ,(16,'ID','IDAHO')
 ,(17,'IL','ILLINOIS')
 ,(18,'IN','INDIANA')
 ,(19,'KS','KANSAS')
 ,(20,'KY','KENTUCKY')
 ,(21,'LA','LOUISIANA')
 ,(22,'MA','MASSACHUSETTS')
 ,(23,'MD','MARYLAND')
 ,(24,'ME','MAINE')
 ,(25,'MI','MICHIGAN')
 ,(26,'MN','MINNESOTA')
 ,(27,'MO','MISSOURI')
 ,(28,'MP','NORTHERN MARIANA ISLANDS')
 ,(29,'MS','MISSISSIPPI')
 ,(30,'MT','MONTANA')
 ,(31,'NC','NORTH CAROLINA')
 ,(32,'ND','NORTH DAKOTA')
 ,(33,'NE','NEBRASKA')
 ,(34,'NH','NEW HAMPSHIRE')
 ,(35,'NJ','NEW JERSEY')
 ,(36,'NM','NEW MEXICO')
 ,(37,'NV','NEVADA')
 ,(38,'NY','NEW YORK')
 ,(39,'OH','OHIO')
 ,(40,'OK','OKLAHOMA')
 ,(41,'OR','OREGON')
 ,(42,'PA','PENNSYLVANIA')
 ,(43,'PR','PUERTO RICO')
 ,(44,'PW','PALAU')
 ,(45,'RI','RHODE ISLAND')
 ,(46,'SC','SOUTH CAROLINA')
 ,(47,'SD','SOUTH DAKOTA')
 ,(48,'TN','TENNESSEE')
 ,(49,'TX','TEXAS')
 ,(50,'UT','UTAH')
 ,(51,'VA','VIRGINIA')
 ,(52,'VI','VIRGIN ISLANDS')
 ,(53,'VT','VERMONT')
 ,(54,'WA','WASHINGTON')
 ,(55,'WI','WISCONSIN')
 ,(56,'WV','WEST VIRGINIA')
 ,(57,'WY','WYOMING')
 ,(58,'AB','ALBERTA')
 ,(59,'BC','BRITISH COLUMBIA')
 ,(60,'MB','MANITOBA')
 ,(61,'NB','NEW BRUNSWICK')
 ,(62,'NL','NEWFOUNDLAND and LABRADOR')
 ,(63,'NT','NORTHWEST TERRITORIES')
 ,(64,'NS','NOVA SCOTIA')
 ,(65,'NU','NUNAVUT')
 ,(66,'ON','ONTARIO')
 ,(67,'PE','PRINCE EDWARD ISLAND')
 ,(68,'QC','QUEBEC')
 ,(69,'SK','SASKATCHEWAN')
 ,(70,'YT','YUKON')
 ,(71,'OT','OTHER')
 ,(72,'AP','APO/FPO CENTRAL AND SOUTH AMERICA')
 ,(73,'AE','ARMED FORCES AFRICA/EUROPE/MIDDLE EAST')
 ,(74,'AP','ARMED FORCES PACIFIC')
 ,(75,'NF','NEWFOUNDLAND')
 ,(76,'PQ','QUEBEC')
 ,(77,'TW','TAIWAN')
) AS Source ([StateId],[StateAbbreviation],[StateName])
ON (Target.[StateId] = Source.[StateId])
WHEN MATCHED AND (
	NULLIF(Source.[StateAbbreviation], Target.[StateAbbreviation]) IS NOT NULL OR NULLIF(Target.[StateAbbreviation], Source.[StateAbbreviation]) IS NOT NULL OR 
	NULLIF(Source.[StateName], Target.[StateName]) IS NOT NULL OR NULLIF(Target.[StateName], Source.[StateName]) IS NOT NULL) THEN
 UPDATE SET
  [StateAbbreviation] = Source.[StateAbbreviation], 
  [StateName] = Source.[StateName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([StateId],[StateAbbreviation],[StateName])
 VALUES(Source.[StateId],Source.[StateAbbreviation],Source.[StateName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [State]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[State] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [State] OFF
GO