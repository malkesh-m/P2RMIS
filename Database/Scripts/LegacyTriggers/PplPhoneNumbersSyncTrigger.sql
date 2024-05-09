
CREATE TRIGGER [PplPhoneNumbersSyncTrigger]
ON [$(P2RMIS)].[dbo].[PPL_Phone_Numbers]
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
	--UPDATE
	IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM [$(DatabaseName)].[dbo].[ViewUserPhone] ViewUserPhone INNER JOIN inserted ON ViewUserPhone.LegacyPhoneId = inserted.PN_ID)
	UPDATE [$(DatabaseName)].[dbo].[UserPhone] 
	SET PhoneTypeId = PhoneType.PhoneTypeId, Phone = inserted.PN_Number, PrimaryFlag = CASE inserted.PN_Type WHEN 'Business Phone' THEN 1 ELSE 0 END,
	ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
	[$(DatabaseName)].[dbo].[UserPhone] UserPhone ON inserted.PN_ID = UserPhone.LegacyPhoneId INNER JOIN
	[$(DatabaseName)].[dbo].[PhoneType] PhoneType ON inserted.PN_Type = PhoneType.PhoneType LEFT OUTER JOIN
	[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	WHERE UserPhone.DeletedFlag = 0
	--INSERT
	ELSE IF EXISTS (Select * FROM inserted) 
	INSERT INTO [$(DatabaseName)].[dbo].[UserPhone]
           ([UserInfoID]
           ,[PhoneTypeId]
           ,[LegacyPhoneId]
           ,[Phone]
           ,[PrimaryFlag]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT DISTINCT UserInfo.UserInfoID, PhoneType.PhoneTypeId, inserted.PN_ID, inserted.PN_Number, CASE inserted.PN_Type WHEN 'Business Phone' THEN 1 ELSE 0 END, 
	VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
	FROM inserted INNER JOIN
		[$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonId INNER JOIN
		[$(DatabaseName)].[dbo].[ViewUserInfo] UserInfo ON U.UserId = UserInfo.UserID INNER JOIN
		[$(DatabaseName)].[dbo].[PhoneType] PhoneType ON inserted.PN_Type = PhoneType.PhoneType LEFT OUTER JOIN
		[$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	WHERE inserted.PN_Number IS NOT NULL AND LTRIM(RTRIM(inserted.PN_Number)) <> ''
	--DELETE
	ELSE
	UPDATE [$(DatabaseName)].[dbo].[UserPhone] 
	SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
	FROM deleted INNER JOIN
		[$(DatabaseName)].[dbo].[UserPhone] UserPhone ON deleted.PN_ID = UserPhone.LegacyPhoneId
	WHERE UserPhone.DeletedFlag = 0
END
