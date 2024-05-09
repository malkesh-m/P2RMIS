CREATE VIEW [dbo].[ViewApplicationPersonnelPrimary]
AS
SELECT        TOP (100) PERCENT ApplicationPersonnelId, ApplicationId, ClientApplicationPersonnelTypeId, FirstName, LastName, MiddleInitial, OrganizationName, PhoneNumber, EmailAddress, Source, StateAbbreviation, CreatedBy, 
                         CreatedDate, ModifiedBy, ModifiedDate, DeletedBy, DeletedDate
FROM            dbo.ApplicationPersonnel
WHERE        (DeletedFlag = 0) AND (PrimaryFlag = 1)

GO
GRANT SELECT ON [ViewApplicationInfo] TO [Netsqlazman_Users]