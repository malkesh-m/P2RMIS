--FIRST UPDATE ADDRESSES TO THE CORRECT ADDRESS TYPE
--2 lines = Home
--All other except already W9 = Work
UPDATE UserAddress
SET AddressTypeId = 3
FROM         UserAddress AS UA
WHERE     (AddressID IN
                          (SELECT     TOP (1) AddressID
                            FROM          UserAddress
                            WHERE      (AddressTypeId = 1) AND (LTRIM(RTRIM(Address3)) = '') AND (LTRIM(RTRIM(Address4)) = '') AND (UserInfoID = UA.UserInfoID) OR
                                                   (AddressTypeId = 1) AND (LTRIM(RTRIM(Address4)) = '') AND (UserInfoID = UA.UserInfoID) AND (Address3 IS NULL) OR
                                                   (AddressTypeId = 1) AND (LTRIM(RTRIM(Address3)) = '') AND (UserInfoID = UA.UserInfoID) AND (Address4 IS NULL) OR
                                                   (AddressTypeId = 1) AND (UserInfoID = UA.UserInfoID) AND (Address3 IS NULL) AND (Address4 IS NULL)))
UPDATE UserAddress
SET AddressTypeId = 2
WHERE AddressTypeId = 1

--INSERT "Institution addresses" for Institution, Department, Position
INSERT INTO UserAddress
           ([UserInfoID]
           ,[AddressTypeLkpID]
           ,[AddressTypeId]
           ,[PrimaryFlag]
           ,[Institution]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT UserInfo.UserInfoID, 2, 2, 0, UserInfo.Institution, UserInfo.ModifiedBy, UserInfo.ModifiedDate
FROM UserInfo
WHERE UserInfo.Institution IS NOT NULL AND LTRIM(RTRIM(UserInfo.Institution)) <> ''

--INSERT "Positions" for the newly inserted institutional addresses
INSERT INTO [UserPosition]
           ([UserAddressId]
           ,[PositionTitle]
           ,[DepartmentTitle]
           ,[PrimaryFlag]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT UserAddress.AddressID, UserInfo.Position, UserInfo.Department, 1, UserInfo.ModifiedBy, UserInfo.ModifiedDate
FROM UserAddress INNER JOIN
	UserInfo ON UserAddress.UserInfoID = UserInfo.UserInfoID
WHERE ((UserInfo.Position IS NOT NULL AND LTRIM(RTRIM(UserInfo.Position)) <> '') OR (UserInfo.Department IS NOT NULL AND LTRIM(RTRIM(UserInfo.Department)) <> ''))
AND UserAddress.Institution IS NOT NULL
