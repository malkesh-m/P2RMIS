
UPDATE [$(P2RMIS)].dbo.PRG_Training SET Cat_Type = 'EL' WHERE Cat_Type = 'VD';
UPDATE [$(P2RMIS)].dbo.PRG_Training SET Cat_Type = 'OT' WHERE Cat_Type IS NULL;
--Clean dups in training table
DELETE FROM [$(P2RMIS)].dbo.PRG_Training_Member 
WHERE Part_Type <> 'All' AND TR_ID IN (SELECT TR_ID FROM [$(P2RMIS)].dbo.PRG_Training_Member Ins WHERE Ins.Review_Type = Review_Type AND Ins.Part_Type = 'ALL');
DELETE FROM [$(P2RMIS)].dbo.PRG_Training_Member 
WHERE Review_Type <> 'All' AND TR_ID IN (SELECT TR_ID FROM [$(P2RMIS)].dbo.PRG_Training_Member Ins WHERE Ins.Part_Type = Part_Type AND Ins.Review_Type = 'ALL');

MERGE INTO [$(P2RMIS)].dbo.[PRG_Training_Category_LU] AS Target
USING (VALUES
  ('BA','Broad Agency Announcement',1,'2015-12-01T00:00:00','chenson')
 ,('CS','Critique Samples',2,'2010-01-19T13:40:00','yliu')
 ,('EL','Tutorials',9,'2010-01-19T13:40:00','yliu')
 ,('HB','Handbooks',3,'2010-01-19T13:40:00','yliu')
 ,('IA','Instructions for Applicants',4,'2015-12-01T00:00:00','chenson')
 ,('OT','Other',6,'2010-01-19T15:14:00','yliu')
 ,('OV','Overviews',5,'2015-12-01T00:00:00','chenson')
 ,('PA','Program Announcements',7,'2010-01-19T13:40:00','yliu')
 ,('RA','Request for Applications',8,'2015-12-01T00:00:00','chenson')
 ,('UG','User Guides',10,'2010-01-19T13:40:00','yliu')
) AS Source ([Cat_Type],[Cat_Desc],[Sort_Order],[Last_Update_Date],[Last_Updated_By])
ON (Target.[Cat_Type] = Source.[Cat_Type])
WHEN MATCHED AND (
	NULLIF(Source.[Cat_Desc], Target.[Cat_Desc]) IS NOT NULL OR NULLIF(Target.[Cat_Desc], Source.[Cat_Desc]) IS NOT NULL OR 
	NULLIF(Source.[Sort_Order], Target.[Sort_Order]) IS NOT NULL OR NULLIF(Target.[Sort_Order], Source.[Sort_Order]) IS NOT NULL OR 
	NULLIF(Source.[Last_Update_Date], Target.[Last_Update_Date]) IS NOT NULL OR NULLIF(Target.[Last_Update_Date], Source.[Last_Update_Date]) IS NOT NULL OR 
	NULLIF(Source.[Last_Updated_By], Target.[Last_Updated_By]) IS NOT NULL OR NULLIF(Target.[Last_Updated_By], Source.[Last_Updated_By]) IS NOT NULL) THEN
 UPDATE SET
  [Cat_Desc] = Source.[Cat_Desc], 
  [Sort_Order] = Source.[Sort_Order], 
  [Last_Update_Date] = Source.[Last_Update_Date], 
  [Last_Updated_By] = Source.[Last_Updated_By]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([Cat_Type],[Cat_Desc],[Sort_Order],[Last_Update_Date],[Last_Updated_By])
 VALUES(Source.[Cat_Type],Source.[Cat_Desc],Source.[Sort_Order],Source.[Last_Update_Date],Source.[Last_Updated_By])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;
