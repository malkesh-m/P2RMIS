
CREATE TRIGGER [PplAddressesSyncTrigger]
ON [$(P2RMIS)].[dbo].[PPL_Addresses]
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	BEGIN
		UPDATE [$(DatabaseName)].[dbo].[UserAddress]
		SET PrimaryFlag = inserted.Preferred_Address, Address1 = inserted.Address1, Address2 = inserted.Address2,
		Address3 = inserted.Address3, Address4 = inserted.Address4, City = inserted.City, StateId = State.StateId,
		StateOther = CASE WHEN State.StateId IS NULL THEN inserted.State ELSE NULL END, Zip = inserted.Zip_Code,
		CountryId = Country.CountryId, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
		FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[UserAddress] UserAddress ON inserted.RA_ID = UserAddress.LegacyAddressID LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].[State] State ON inserted.State = State.StateAbbreviation LEFT OUTER JOIN
		[$(DatabaseName)].[dbo].[Country] Country ON CASE WHEN inserted.Country = 'USA' THEN 'US' WHEN inserted.Country = 'CAN' THEN 'CA' ELSE inserted.Country END = Country.CountryAbbreviation LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
		WHERE UserAddress.DeletedFlag = 0
		--If vendor name is updated we need to update
		IF UPDATE(Vendor_Name) AND (Select Count(*) From inserted WHERE inserted.Address_Type = 'W9 Address') > 0
		BEGIN
				UPDATE UserVendor SET VendorName = inserted.Vendor_Name
				FROM [$(DatabaseName)].[dbo].UserInfo UserInfo INNER JOIN
				[$(DatabaseName)].[dbo].UserVendor UserVendor ON UserInfo.UserInfoId = UserVendor.UserInfoId INNER JOIN
				[$(DatabaseName)].[dbo].[ViewUser] [User] ON UserInfo.UserID = [User].UserID INNER JOIN
				inserted ON [User].PersonID = inserted.Person_ID
	WHERE inserted.Address_Type = 'W9 Address' AND UserInfo.DeletedFlag = 0  AND UserVendor.DeletedFlag = 0 AND UserVendor.ActiveFlag = 1
		END
	END
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted) 
	BEGIN
	INSERT INTO [$(DatabaseName)].[dbo].[UserAddress]
           ([UserInfoID]
           ,[LegacyAddressID]
           ,[AddressTypeId]
           ,[PrimaryFlag]
           ,[Address1]
           ,[Address2]
           ,[Address3]
           ,[Address4]
           ,[City]
           ,[StateId]
           ,[StateOther]
           ,[Zip]
           ,[CountryId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT UserInfo.UserInfoID, inserted.RA_ID, (CASE WHEN inserted.Address_Type = 'Primary Work' THEN 2 WHEN inserted.Address_Type = 'Primary Home' THEN 3 WHEN inserted.Address_Type = 'W9 Address' THEN 4 WHEN inserted.Address3 IS NOT NULL AND inserted.Address3 <> '' THEN 2 ELSE 3 END), 
	inserted.Preferred_Address, inserted.Address1, inserted.Address2, inserted.Address3, inserted.Address4, inserted.City, State.StateId, CASE WHEN State.StateId IS NULL THEN inserted.State ELSE NULL END, 
	inserted.Zip_Code, Country.CountryId, VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
	[$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonID INNER JOIN
	[$(DatabaseName)].[dbo].[ViewUserInfo] UserInfo ON U.UserID = UserInfo.UserID LEFT OUTER JOIN
	[$(DatabaseName)].[dbo].[State] State ON inserted.State = State.StateAbbreviation LEFT OUTER JOIN
	[$(DatabaseName)].[dbo].[Country] Country ON CASE WHEN inserted.Country = 'USA' THEN 'US' WHEN inserted.Country = 'CAN' THEN 'CA' ELSE inserted.Country END = Country.CountryAbbreviation LEFT OUTER JOIN
	[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	--If vendor name is updated we need to update
		IF UPDATE(Vendor_Name) AND (Select Count(*) From inserted WHERE inserted.Address_Type = 'W9 Address') > 0
		BEGIN
			UPDATE UserVendor SET VendorName = inserted.Vendor_Name, VendorTypeId = CASE inserted.Inst_Vendor_ID WHEN 0 THEN 1 ELSE 2 END 
			FROM [$(DatabaseName)].[dbo].ViewUserInfo UserInfo INNER JOIN
				[$(DatabaseName)].[dbo].[ViewUser] [User] ON UserInfo.UserID = [User].UserID INNER JOIN
				[$(DatabaseName)].[dbo].UserVendor UserVendor ON UserInfo.UserInfoId = UserVendor.UserInfoId INNER JOIN
				inserted ON [User].PersonID = inserted.Person_ID
			WHERE inserted.Address_Type = 'W9 Address' AND UserVendor.ActiveFlag = 1 AND UserVendor.DeletedFlag = 0
		END
	END
	--DELETE
	ELSE
	BEGIN
	UPDATE [$(DatabaseName)].[dbo].[UserAddress]
	SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
		 [$(DatabaseName)].[dbo].[UserAddress] UserAddress ON deleted.RA_ID = UserAddress.LegacyAddressID
	WHERE UserAddress.DeletedFlag = 0
	--If vendor name is updated we need to set null
	UPDATE UserVendor SET DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM [$(DatabaseName)].[dbo].UserInfo UserInfo INNER JOIN
				[$(DatabaseName)].[dbo].UserVendor UserVendor ON UserInfo.UserInfoId = UserVendor.UserInfoId INNER JOIN
				[$(DatabaseName)].[dbo].[ViewUser] [User] ON UserInfo.UserID = [User].UserID INNER JOIN
				deleted ON [User].PersonID = deleted.Person_ID
	WHERE deleted.Address_Type = 'W9 Address' AND UserInfo.DeletedFlag = 0  AND UserVendor.DeletedFlag = 0 AND UserVendor.ActiveFlag = 1
	
	END
END
