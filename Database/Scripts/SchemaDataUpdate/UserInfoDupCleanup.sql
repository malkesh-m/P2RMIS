--Clean up duplicate primary phone numbers
UPDATE UserPhone SET DeletedFlag = 1, DeletedBy = 1, DeletedDate = dbo.GetP2rmisDateTime()
FROM UserPhone INNER JOIN
[UserInfo] ON UserPhone.UserInfoID = UserInfo.UserInfoID INNER JOIN
	(SELECT PhoneId, DENSE_RANK() OVER (Partition By UserInfoId Order By ModifiedDate desc, PhoneId) AS Ranking
	FROM UserPhone
	WHERE PrimaryFlag = 1 AND DeletedFlag = 0) PhoneTable ON UserPhone.PhoneID = PhoneTable.PhoneID
WHERE PhoneTable.Ranking > 1
--Clean up duplicate legacy phone numbers
UPDATE UserPhone SET DeletedFlag = 1, DeletedBy = 1, DeletedDate = dbo.GetP2rmisDateTime()
FROM UserPhone INNER JOIN
[UserInfo] ON UserPhone.UserInfoID = UserInfo.UserInfoID INNER JOIN
	(SELECT PhoneId, DENSE_RANK() OVER (Partition By UserInfoId, Phone Order By ModifiedDate desc, PhoneId) AS Ranking
	FROM UserPhone
	WHERE DeletedFlag = 0) PhoneTable ON UserPhone.PhoneID = PhoneTable.PhoneID
WHERE PhoneTable.Ranking > 1
--Clean up duplicate primary addresses
UPDATE UserAddress SET DeletedFlag = 1, DeletedBy = 1, DeletedDate = dbo.GetP2rmisDateTime()
FROM UserAddress INNER JOIN
[UserInfo] ON UserAddress.UserInfoID = UserInfo.UserInfoID INNER JOIN
	(SELECT AddressId, DENSE_RANK() OVER (Partition By UserInfoId Order By ModifiedDate desc, AddressId) AS Ranking
	FROM UserAddress
	WHERE PrimaryFlag = 1 AND DeletedFlag = 0) DupTable ON UserAddress.AddressID = DupTable.AddressID
WHERE DupTable.Ranking > 1
--Clean up duplicate primary email
UPDATE UserEmail SET DeletedFlag = 1, DeletedBy = 1, DeletedDate = dbo.GetP2rmisDateTime()
FROM UserEmail INNER JOIN
[UserInfo] ON UserEmail.UserInfoID = UserInfo.UserInfoID INNER JOIN
	(SELECT EmailId, DENSE_RANK() OVER (Partition By UserInfoId Order By ModifiedDate desc, EmailId) AS Ranking
	FROM UserEmail
	WHERE PrimaryFlag = 1 AND DeletedFlag = 0) DupTable ON UserEmail.EmailID = DupTable.EmailID
WHERE DupTable.Ranking > 1