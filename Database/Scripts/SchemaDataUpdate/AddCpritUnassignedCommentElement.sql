INSERT INTO [dbo].[ClientElement]
           ([ClientId]
           ,[ElementTypeId]
           ,[ElementIdentifier]
           ,[ElementAbbreviation]
           ,[ElementDescription]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
     VALUES
           (9
           ,4
           ,'Addl Cmnt'
           ,'Additional Comments'
           ,'Additional Comments'
           ,10
           ,dbo.GetP2rmisDateTime()
           ,10
           ,dbo.GetP2rmisDateTime())
