--There is no need to update addresses based on legacy info, we can't really tell which address it is mapped to
UPDATE    ua
SET              ua.AddressTypeId = u2.AddressTypeLkpID, ua.StateId = u2.USStateLkpID, ua.CountryId = u2.CountryLkpID
FROM         UserAddress AS ua INNER JOIN
                      State ON ua.USStateLkpID = State.StateId INNER JOIN
                      Country ON ua.CountryLkpID = Country.CountryId INNER JOIN
                      UserAddress AS u2 ON ua.AddressID = u2.AddressID



Insert into [dbo].[UserAddress]

([UserInfoID], 
[LegacyAddressID], 
[AddressTypeID],
[AddressTypeLkpID],
[PrimaryFlag],
[Address1], 
[Address2],
[Address3], 
[Address4], 
[City],  
[USStateLkpID], 
[StateId],             
[StateOther], 
[Zip], 
[CountryLkpID],
[CountryId],
[CreatedDate], 
[CreatedBy], 
[ModifiedDate], 
[ModifiedBy] 

)
SELECT ui.UserInfoID
,pa.RA_ID
,(CASE WHEN pa.Address_Type = 'Primary Work' THEN 2 WHEN pa.Address_Type = 'Primary Home' THEN 3 WHEN pa.Address_Type = 'W9 Address' THEN 4 ELSE 1 END)
                
,(CASE WHEN pa.Address_Type = 'Primary Work' THEN 2 WHEN pa.Address_Type = 'Primary Home' THEN 3 WHEN pa.Address_Type = 'W9 Address' THEN 4 ELSE 1 END)
               
, pa.Preferred_Address
, pa.Address1
, pa.Address2
, pa.Address3
, pa.Address4
, pa.City
, ls.USStateID
, s.StateId
, CASE WHEN s.StateId IS NULL THEN pa.State ELSE NULL END
, pa.Zip_Code
, lc.CountryID
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
                      Country AS c INNER JOIN
                      LookupCountry AS lc ON c.CountryAbbreviation = lc.CountryCode ON pa.Country = lc.CountryCode LEFT OUTER JOIN
                      State AS s INNER JOIN
                      LookupUSState AS ls ON s.StateAbbreviation = ls.USStateName ON pa.State = ls.USStateName
