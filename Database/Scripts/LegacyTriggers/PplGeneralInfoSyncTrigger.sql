
CREATE TRIGGER [PplGeneralInfoSyncTrigger]
ON [$(P2RMIS)].[dbo].[PPL_General_Info]
FOR INSERT, UPDATE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
	UPDATE [$(DatabaseName)].[dbo].[UserInfo]
    SET GenderId = Gender.GenderId, EthnicityId = Ethnicity.EthnicityId, Expertise = inserted.RCF_Expertise, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
	[$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonId INNER JOIN
	[$(DatabaseName)].[dbo].[UserInfo] UserInfo ON U.UserID = UserInfo.UserID LEFT OUTER JOIN
	[$(DatabaseName)].[dbo].[Gender] Gender ON inserted.Gender = Gender.Gender LEFT OUTER JOIN
	[$(P2RMIS)].[dbo].[PRG_Ethnicity] PRG_Ethnicity ON inserted.Ethnicity = PRG_Ethnicity.Ethnicity_ID LEFT OUTER JOIN
	[$(DatabaseName)].[dbo].[Ethnicity] Ethnicity ON PRG_Ethnicity.Description = Ethnicity.Ethnicity LEFT OUTER JOIN
	[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	WHERE UserInfo.DeletedFlag = 0
	--INSERT (UserInfo should already exist at this point so it is update only)
	ELSE IF EXISTS (Select * FROM inserted) 
	UPDATE [$(DatabaseName)].[dbo].[UserInfo]
    SET GenderId = Gender.GenderId, EthnicityId = Ethnicity.EthnicityId, Expertise = inserted.RCF_Expertise, VendorId = inserted.Vendor_ID, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
	[$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonId INNER JOIN
	[$(DatabaseName)].[dbo].[UserInfo] UserInfo ON U.UserID = UserInfo.UserID LEFT OUTER JOIN
	[$(DatabaseName)].[dbo].[Gender] Gender ON inserted.Gender = Gender.Gender LEFT OUTER JOIN
	[$(P2RMIS)].[dbo].[PRG_Ethnicity] PRG_Ethnicity ON inserted.Ethnicity = PRG_Ethnicity.Ethnicity_ID LEFT OUTER JOIN
	[$(DatabaseName)].[dbo].[Ethnicity] Ethnicity ON PRG_Ethnicity.Description = Ethnicity.Ethnicity LEFT OUTER JOIN
	[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	WHERE UserInfo.DeletedFlag = 0

	--Vendor Update
	IF EXISTS (Select * FROM inserted INNER JOIN
	[$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonId INNER JOIN
	[$(DatabaseName)].[dbo].[ViewUserInfo] UserInfo ON U.UserID = UserInfo.UserID INNER JOIN
	[$(DatabaseName)].[dbo].[ViewUserVendor] UserVendor ON UserInfo.UserInfoId = UserVendor.UserInfoId
	WHERE inserted.Vendor_Id IS NOT NULL AND inserted.Vendor_Id <> UserVendor.VendorId)
	
	UPDATE [$(DatabaseName)].[dbo].[UserVendor]
	SET VendorId = inserted.Vendor_Id
	FROM inserted INNER JOIN
	[$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonId INNER JOIN
	[$(DatabaseName)].[dbo].[ViewUserInfo] UserInfo ON U.UserID = UserInfo.UserID INNER JOIN
	[$(DatabaseName)].[dbo].[UserVendor] UserVendor ON UserInfo.UserInfoId = UserVendor.UserInfoId LEFT OUTER JOIN
	[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	WHERE UserVendor.DeletedFlag = 0 AND UserVendor.ActiveFlag = 1

	--Vendor Insert
	ELSE IF EXISTS (Select * FROM inserted INNER JOIN
	[$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonId INNER JOIN
	[$(DatabaseName)].[dbo].[ViewUserInfo] UserInfo ON U.UserID = UserInfo.UserID LEFT OUTER JOIN
	[$(DatabaseName)].[dbo].[ViewUserVendor] UserVendor ON UserInfo.UserInfoId = UserVendor.UserInfoId AND UserVendor.ActiveFlag = 1
	WHERE inserted.Vendor_Id IS NOT NULL AND UserVendor.UserVendorId IS NULL)
	BEGIN
	INSERT INTO [$(DatabaseName)].[dbo].UserVendor (UserInfoId, VendorId, VendorName, VendorTypeId, ActiveFlag, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
	SELECT UserInfo.UserInfoId, inserted.Vendor_Id, '', 1, 1, VUN.UserId, inserted.Last_Update_Date, VUN.UserId, inserted.Last_Update_Date
	FROM inserted INNER JOIN
	[$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonId INNER JOIN
	[$(DatabaseName)].[dbo].[ViewUserInfo] UserInfo ON U.UserID = UserInfo.UserID LEFT OUTER JOIN
	[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	UPDATE [$(DatabaseName)].[dbo].VendorIdAssigned SET AssignedFlag = 1
	WHERE VendorId IN (SELECT Vendor_Id FROM inserted)
	END

	--Vendor Delete
	ELSE IF EXISTS (Select * FROM deleted INNER JOIN
	[$(DatabaseName)].[dbo].[ViewUser] U ON deleted.Person_ID = U.PersonId INNER JOIN
	[$(DatabaseName)].[dbo].[ViewUserInfo] UserInfo ON U.UserID = UserInfo.UserID INNER JOIN
	[$(DatabaseName)].[dbo].[ViewUserVendor] UserVendor ON UserInfo.UserInfoId = UserVendor.UserInfoId AND UserVendor.ActiveFlag = 1
	WHERE deleted.Vendor_Id IS NULL AND UserVendor.UserVendorId IS NOT NULL)

	UPDATE [$(DatabaseName)].[dbo].[UserVendor]
	SET DeletedFlag = 1, DeletedBy = VUN.UserId, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
	[$(DatabaseName)].[dbo].[ViewUser] U ON deleted.Person_ID = U.PersonId INNER JOIN
	[$(DatabaseName)].[dbo].[ViewUserInfo] UserInfo ON U.UserID = UserInfo.UserID INNER JOIN
	[$(DatabaseName)].[dbo].[UserVendor] UserVendor ON UserInfo.UserInfoId = UserVendor.UserInfoId LEFT OUTER JOIN
	[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON deleted.LAST_UPDATED_BY = VUN.UserName 
	WHERE UserVendor.DeletedFlag = 0 AND UserVendor.ActiveFlag = 1
END
