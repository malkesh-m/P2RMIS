--Fix for PersonnelTyhpes under the wrong client
UPDATE ApplicationPersonnel SET ClientApplicationPersonnelTypeId = correctedType.ClientApplicationPersonnelTypeId, ModifiedBy = 10, ModifiedDate = dbo.GetP2rmisDateTime()
FROM ApplicationPersonnel
INNER JOIN ViewApplication ON ApplicationPersonnel.ApplicationId = ViewApplication.ApplicationId
INNER JOIN ViewProgramMechanism ON ViewApplication.ProgramMechanismId = ViewProgramMechanism.ProgramMechanismId
INNER JOIN ViewProgramYear ON ViewProgramMechanism.ProgramYearId = ViewProgramYear.ProgramYearId
INNER JOIN ClientProgram ON ViewProgramYear.ClientProgramId = ClientProgram.ClientProgramId
INNER JOIN ClientApplicationPersonnelType currentType ON ApplicationPersonnel.ClientApplicationPersonnelTypeId = currentType.ClientApplicationPersonnelTypeId
INNER JOIN ClientApplicationPersonnelType correctedType ON currentType.ExternalPersonnelTypeId = correctedType.ExternalPersonnelTypeId AND ClientProgram.ClientId = correctedType.ClientId
WHERE ApplicationPersonnel.DeletedFlag = 0 AND currentType.ClientApplicationPersonnelTypeId <> correctedType.ClientApplicationPersonnelTypeId;
--Fix for empty mentors
UPDATE ApplicationPersonnel SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
WHERE FirstName IS NULL AND LastName IS NULL AND OrganizationName IS NULL AND DeletedFlag = 0;
--Fix for duplicates that should have been soft deleted
WITH cte
AS (Select ApplicationPersonnelId, ROW_NUMBER() OVER (Partition By ApplicationId, FirstName, LastName, OrganizationName, ClientApplicationPersonnelTypeId, [Source] Order By DeletedDate asc) AS dupCount
	FROM ApplicationPersonnel
	WHERE DeletedFlag = 0)
UPDATE ApplicationPersonnel SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
FROM ApplicationPersonnel
INNER JOIN cte ON ApplicationPersonnel.ApplicationPersonnelId = cte.ApplicationPersonnelId
WHERE ApplicationPersonnel.DeletedFlag = 0 AND cte.dupCount > 1;