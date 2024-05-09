SET IDENTITY_INSERT [LookupUSState] ON
 
MERGE INTO [LookupUSState] AS Target
USING (VALUES

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
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
 ,(43,'PA','PENNSYLVANIA')
 ,(44,'PR','PUERTO RICO')
 ,(45,'PW','PALAU')
 ,(46,'RI','RHODE ISLAND')
 ,(47,'SC','SOUTH CAROLINA')
 ,(48,'SD','SOUTH DAKOTA')
 ,(49,'TN','TENNESSEE')
 ,(50,'TX','TEXAS')
 ,(51,'UT','UTAH')
 ,(52,'VA','VIRGINIA')
 ,(53,'VI','VIRGIN ISLANDS')
 ,(54,'VT','VERMONT')
 ,(55,'WA','WASHINGTON')
 ,(56,'WI','WISCONSIN')
 ,(57,'WV','WEST VIRGINIA')
 ,(58,'WY','WYOMING')
 ,(59,'AB','Alberta')
 ,(60,'BC','British Columbia')
 ,(61,'MB','Manitoba')
 ,(62,'NB','New Brunswick')
 ,(63,'NL','Newfoundland and Labrador')
 ,(64,'NT','Northwest Territories')
 ,(65,'NS','Nova Scotia')
 ,(66,'NU','Nunavut')
 ,(67,'ON','Ontario')
 ,(68,'PE','Prince Edward Island')
 ,(69,'QC','Quebec')
 ,(70,'SK','Saskatchewan')
 ,(71,'YT','Yukon')
 ,(98,'OT','OTHER')
 ,(99,'','')

) AS Source ([USStateID],[USStateName],[USStateFullName])
ON (Target.[USStateID] = Source.[USStateID])
WHEN MATCHED AND (Target.[USStateName] <> Source.[USStateName] OR Target.[USStateFullName] <> Source.[USStateFullName]) THEN
 UPDATE SET
 [USStateName] = Source.[USStateName], 
[USStateFullName] = Source.[USStateFullName]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([USStateID],[USStateName],[USStateFullName])
 VALUES(Source.[USStateID],Source.[USStateName],Source.[USStateFullName])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;
 
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [LookupUSState]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[LookupUSState] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO
 
SET IDENTITY_INSERT [LookupUSState] OFF
GO