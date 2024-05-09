UPDATE UserAddress SET CountryId = CASE WHEN PPL_Addresses.Country = 'USA' THEN 223 WHEN PPL_Addresses.Country = 'CAN' THEN 36 END
FROM UserAddress INNER JOIN
	ViewUserInfo ON UserAddress.UserInfoID = ViewUserInfo.UserInfoID INNER JOIN
	ViewUser ON ViewUserInfo.UserID = ViewUser.UserID INNER JOIN
	[$(P2RMIS)].dbo.PPL_Addresses PPL_Addresses ON ViewUser.PersonID = PPL_Addresses.Person_ID
WHERE PPL_Addresses.Address_Type = 'W9 Address'