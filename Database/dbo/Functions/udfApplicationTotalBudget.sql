/*
Retreives partner data for a given application and index position (for multiple partners)
*/
CREATE FUNCTION [dbo].[udfApplicationTotalBudget]
(
	@ApplicationId int
)
RETURNS TABLE
AS
RETURN
SELECT TOP(1) ISNULL(ParentBudget.TotalFunding, 0) + SUM(ISNULL(ViewApplicationBudget.TotalFunding, 0)) AS TotalFunding, ISNULL(ParentBudget.DirectCosts, 0) + SUM(ISNULL(ViewApplicationBudget.DirectCosts, 0)) AS TotalDirectCosts , ISNULL(ParentBudget.IndirectCosts, 0) + SUM(ISNULL(ViewApplicationBudget.IndirectCosts, 0)) AS TotalIndirectCosts
FROM ViewApplication ParentApplication INNER JOIN
	ViewApplicationBudget ParentBudget ON ParentApplication.ApplicationId = ParentBudget.ApplicationId LEFT OUTER JOIN
	ViewApplication ON ParentApplication.ApplicationId = ViewApplication.ParentApplicationId LEFT OUTER JOIN
	ViewApplicationBudget ON ViewApplication.ApplicationId = ViewApplicationBudget.ApplicationId 
WHERE ParentApplication.ApplicationId = @ApplicationId
GROUP BY ParentApplication.ApplicationId, ISNULL(ParentBudget.TotalFunding, 0), ISNULL(ParentBudget.DirectCosts, 0), ISNULL(ParentBudget.IndirectCosts, 0)