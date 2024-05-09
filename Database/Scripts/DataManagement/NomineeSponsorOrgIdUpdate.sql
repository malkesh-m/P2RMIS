-- Update with OrganizationId from the NominatingOrganization table.
UPDATE  NomineeSponsor
SET  OrganizationId = nor.OrganizationId
FROM  NomineeSponsor INNER JOIN
      NominatingOrganization AS nor ON NomineeSponsor.Organization = nor.OrganizationName
WHERE NomineeSponsor.OrganizationId is null 
