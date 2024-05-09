CREATE VIEW [dbo].ViewApplication AS
SELECT [ApplicationId]
      ,[ProgramMechanismId]
      ,[ParentApplicationId]
      ,[LogNumber]
      ,[ApplicationTitle]
      ,[ResearchArea]
      ,[Keywords]
      ,[ProjectStartDate]
      ,[ProjectEndDate]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
	  ,[WithdrawnFlag]
	  ,[WithdrawnBy]
	  ,[WithdrawnDate]

  FROM [dbo].[Application]
WHERE [DeletedFlag] = 0
GO
GRANT SELECT ON [ViewApplication] TO [web-p2rmis]

