--Clean up script for user's organization types and addresses
UPDATE UserAddress
SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime()
WHERE DeletedDate IS NULL;

--Set all user orgs to Institution unless they have consumer participation
UPDATE UserInfo
SET ProfessionalAffiliationId = (CASE WHEN UserInfoId IN 
								(Select UserInfoId FROM PanelUserAssignment 
													INNER JOIN UserInfo ON PanelUserAssignment.UserId = UserInfo.UserID
													INNER JOIN ClientParticipantType ON PanelUserAssignment.ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId
												WHERE ClientParticipantType.ConsumerFlag = 1) THEN 2 ELSE 1 END)
WHERE UserInfo.Institution IS NOT NULL;

--Re-import/map all addresses from 1.0
Insert into [dbo].[UserAddress]
([UserInfoID], 
[LegacyAddressID], 
[AddressTypeID],
[PrimaryFlag],
[Address1], 
[Address2],
[Address3], 
[Address4], 
[City],  
[StateId],             
[Zip], 
[CountryId],
[CreatedDate], 
[CreatedBy], 
[ModifiedDate], 
[ModifiedBy] 
)
SELECT ui.UserInfoID
,pa.RA_ID
,(CASE WHEN pa.Address_Type = 'Primary Work' THEN 2 WHEN pa.Address_Type = 'Primary Home' THEN 3 WHEN pa.Address_Type = 'W9 Address' THEN 4 WHEN pa.Address3 IS NOT NULL AND pa.Address3 <> '' THEN 2 ELSE 3 END)          
, pa.Preferred_Address
, pa.Address1
, pa.Address2
, pa.Address3
, pa.Address4
, pa.City
, s.StateId
, pa.Zip_Code
, c.CountryId 
, pa.LAST_UPDATE_DATE
, ViewLegacyUserNameToUserId.UserID
, pa.LAST_UPDATE_DATE
, ViewLegacyUserNameToUserId.UserID
FROM         [$(P2RMIS)].dbo.PPL_Addresses AS pa INNER JOIN
                      [User] AS u ON pa.Person_ID = u.PersonID INNER JOIN
                      UserInfo AS ui ON u.UserID = ui.UserID INNER JOIN
                      [$(P2RMIS)].dbo.PPL_People AS p ON pa.Person_ID = p.Person_ID LEFT OUTER JOIN
                      ViewLegacyUserNameToUserId ON pa.LAST_UPDATED_BY = ViewLegacyUserNameToUserId.UserName LEFT OUTER JOIN
                      Country AS c ON pa.Country = c.CountryAbbreviation LEFT OUTER JOIN
                      State AS s ON pa.State = s.StateAbbreviation;