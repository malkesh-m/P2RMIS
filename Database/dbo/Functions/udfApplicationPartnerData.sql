/*
Retreives partner data for a given application and index position (for multiple partners)
*/
CREATE FUNCTION [dbo].[udfApplicationPartnerData]
(
	@ApplicationId int,
	@Index int
)
RETURNS TABLE
AS
RETURN
SELECT ApplicationId, LogNumber, FirstName, LastName, GrantId, PerformingOrgName, ContractingOrgName, TotalFunding, DirectCosts, IndirectCosts
FROM
(
SELECT ViewApplication.ApplicationId, ViewApplication.LogNumber, ViewApplicationPersonnel.FirstName, ViewApplicationPersonnel.LastName, ViewApplicationInfo.InfoText AS GrantId, ViewApplicationPersonnel.OrganizationName AS PerformingOrgName, ApplicationPersonnelAdmin.OrganizationName AS ContractingOrgName,
 ViewApplicationBudget.TotalFunding, ViewApplicationBudget.DirectCosts, ViewApplicationBudget.IndirectCosts, DENSE_RANK() OVER(Partition By ParentApplication.ApplicationId Order By ViewApplication.LogNumber) AS Ranking
FROM ViewApplication ParentApplication INNER JOIN
	ViewApplication ON ParentApplication.ApplicationId = ViewApplication.ParentApplicationId LEFT OUTER JOIN
	ViewApplicationPersonnel ON ViewApplication.ApplicationId = ViewApplicationPersonnel.ApplicationId AND ViewApplicationPersonnel.PrimaryFlag = 1 LEFT OUTER JOIN
	ViewApplicationBudget ON ViewApplication.ApplicationId = ViewApplicationBudget.ApplicationId LEFT OUTER JOIN
	ViewApplicationInfo ON ViewApplication.ApplicationId = ViewApplicationInfo.ApplicationId AND ViewApplicationInfo.ClientApplicationInfoTypeId = 1 LEFT OUTER JOIN
	ViewApplicationPersonnel ApplicationPersonnelAdmin ON ViewApplication.ApplicationId = ApplicationPersonnelAdmin.ApplicationId INNER JOIN
	ClientApplicationPersonnelType ON ApplicationPersonnelAdmin.ClientApplicationPersonnelTypeId = ClientApplicationPersonnelType.ClientApplicationPersonnelTypeId AND ClientApplicationPersonnelType.ApplicationPersonnelTypeAbbreviation = 'Admin-1'
WHERE ParentApplication.ApplicationId = @ApplicationId
) Main
WHERE Ranking = @Index