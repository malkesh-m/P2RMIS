INSERT INTO [dbo].[ProgramMechanismSummaryStatement]
           ([ProgramMechanismId]
           ,[ReviewStatusId]
           ,[TemplateLocation]
           ,[StoredProcedureName])
SELECT ViewProgramMechanism.ProgramMechanismId, ReviewStatus.ReviewStatusId, 'template.docx', 'uspSSData'
FROM ViewProgramMechanism
INNER JOIN ViewProgramYear ON ViewProgramMechanism.ProgramYearId = ViewProgramYear.ProgramYearId
INNER JOIN ClientProgram ON ViewProgramYear.ClientProgramId = ClientProgram.ClientProgramId
CROSS JOIN ReviewStatus
WHERE ReviewStatus.ReviewStatusTypeId = 1 AND
((ClientProgram.ProgramAbbreviation IN ('BCRP','LCRP') AND ViewProgramYear.[Year] = 2016) OR
(ClientProgram.ProgramAbbreviation IN ('CPRIT RES','CPRIT PDEV') AND ViewProgramYear.[Year] = 2017)) AND
NOT EXISTS (Select 1 FROM ProgramMechanismSummaryStatement WHERE ProgramMechanismSummaryStatement.ProgramMechanismId = ViewProgramMechanism.ProgramMechanismId AND ProgramMechanismSummaryStatement.ReviewStatusId = ReviewStatus.ReviewStatusId)
