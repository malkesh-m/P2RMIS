--Only those where CoiFlag = 1 should be mapped to Ids from the PRO_COI_Type table
UPDATE ClientApplicationPersonnelType SET ExternalPersonnelTypeId = NULL WHERE CoiFlag = 0;

--Add missing COI types 
INSERT INTO [dbo].[ClientApplicationPersonnelType]
           ([ClientId]
           ,[ApplicationPersonnelType]
           ,[CoiFlag]
           ,[ExternalPersonnelTypeId])
     VALUES
           (9
           ,'Co-Program Director'
           ,1
           ,39);

INSERT INTO [dbo].[ClientApplicationPersonnelType]
           ([ClientId]
           ,[ApplicationPersonnelType]
           ,[CoiFlag]
           ,[ExternalPersonnelTypeId])
     VALUES
           (19
           ,'Post Award Business Official'
           ,1
           ,38);

INSERT INTO [dbo].[ClientApplicationPersonnelType]
           ([ClientId]
           ,[ApplicationPersonnelType]
           ,[CoiFlag]
           ,[ExternalPersonnelTypeId])
     VALUES
           (19
           ,'Advocate'
           ,1
           ,41);

INSERT INTO [dbo].[ClientApplicationPersonnelType]
           ([ClientId]
           ,[ApplicationPersonnelType]
           ,[CoiFlag]
           ,[ExternalPersonnelTypeId])
     VALUES
           (19
           ,'Alternate Business Official'
           ,1
           ,42);

INSERT INTO [dbo].[ClientApplicationPersonnelType]
           ([ClientId]
           ,[ApplicationPersonnelType]
           ,[CoiFlag]
           ,[ExternalPersonnelTypeId])
     VALUES
           (20
           ,'COI - Conflict of Interest'
           ,1
           ,23);

INSERT INTO [dbo].[ClientApplicationPersonnelType]
           ([ClientId]
           ,[ApplicationPersonnelType]
           ,[CoiFlag]
           ,[ExternalPersonnelTypeId])
     VALUES
           (20
           ,'other role on project'
           ,1
           ,24);

INSERT INTO [dbo].[ClientApplicationPersonnelType]
           ([ClientId]
           ,[ApplicationPersonnelType]
           ,[CoiFlag]
           ,[ExternalPersonnelTypeId])
     VALUES
           (20
           ,'PD/PI'
           ,1
           ,31);

INSERT INTO [dbo].[ClientApplicationPersonnelType]
           ([ClientId]
           ,[ApplicationPersonnelType]
           ,[CoiFlag]
           ,[ExternalPersonnelTypeId])
     VALUES
           (20
           ,'collaborator'
           ,1
           ,3);
