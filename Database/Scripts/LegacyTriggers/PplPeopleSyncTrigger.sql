
CREATE TRIGGER [PplPeopleSyncTrigger]
ON [$(P2RMIS)].dbo.PPL_People
FOR INSERT, UPDATE, DELETE
AS
BEGIN
	SET NOCOUNT ON
		--UPDATE
		IF EXISTS (Select * FROM inserted) AND EXISTS(Select * FROM deleted)
		BEGIN
			UPDATE [$(DatabaseName)].[dbo].[UserInfo]
			SET MilitaryRankId = NULLIF(inserted.Military_Rank, 0), FirstName = inserted.FName, MiddleName = inserted.MI, LastName = inserted.LName, Institution = inserted.Institution,
			Department = inserted.Department, Position = inserted.PTitle, BadgeName = inserted.Badge_Name,
			PrefixId = Prefix.PrefixId, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
			FROM inserted INNER JOIN
			[$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonId INNER JOIN
			[$(DatabaseName)].[dbo].[UserInfo] UserInfo ON U.UserID = UserInfo.UserID LEFT OUTER JOIN
		    [$(DatabaseName)].[dbo].[Prefix] Prefix ON inserted.Prefix = Prefix.PrefixName LEFT OUTER JOIN
		    [$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
			WHERE UserInfo.DeletedFlag = 0

			UPDATE [$(DatabaseName)].[dbo].[UserEmail]
			SET Email = inserted.Email, ModifiedBy = VUN.UserId, ModifiedDate = inserted.LAST_UPDATE_DATE
			FROM inserted INNER JOIN
			[$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonId INNER JOIN
			[$(DatabaseName)].[dbo].[ViewUserInfo] UserInfo ON U.UserID = UserInfo.UserID INNER JOIN
			[$(DatabaseName)].[dbo].[UserEmail] UserEmail ON UserInfo.UserInfoID = UserEmail.UserInfoID LEFT OUTER JOIN
			[$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
			WHERE UserEmail.DeletedFlag = 0
		END
		--INSERT
		ELSE IF EXISTS (Select * FROM inserted)
		BEGIN
			--First insert a new inactive user in the new system (there will have to be 
			--a seperate process of activating legacy account in 2.0 but should not be active by default)
			INSERT INTO [$(DatabaseName)].[dbo].[User]
           ([UserLogin]
           ,[IsActivated]
           ,[PersonID]
		   ,[CreatedBy]
           ,[CreatedDate]
		   ,[ModifiedBy]
           ,[ModifiedDate])
				SELECT DISTINCT [$(DatabaseName)].dbo.udfGetNewUserName(i.FName, i.Lname), 0, i.Person_ID, uu.UserID, ISNULL(i.LAST_UPDATE_DATE, dbo.GetP2rmisDateTime()), uu.UserID, ISNULL(i.LAST_UPDATE_DATE, dbo.GetP2rmisDateTime())
				FROM inserted i
				LEFT OUTER JOIN [$(P2RMIS)].dbo.Sys_Users u on i.LAST_UPDATED_BY = u.UserID
				LEFT OUTER JOIN [$(DatabaseName)].[dbo].[ViewUser] uu on u.Person_ID = uu.PersonID
			
			INSERT INTO [$(DatabaseName)].[dbo].[UserInfo]
           ([UserID]
           ,[MilitaryRankId]
           ,[FirstName]
           ,[MiddleName]
           ,[LastName]
           ,[Institution]
           ,[Department]
           ,[Position]
           ,[BadgeName]
           ,[PrefixId]
		   ,[ProfessionalAffiliationId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		   SELECT U.UserId, MilitaryRank.MilitaryRankId, inserted.FName, inserted.MI, inserted.LName, inserted.Institution, inserted.Department, inserted.PTitle, inserted.Badge_Name,
		   Prefix.PrefixId, CASE WHEN EXISTS (Select 'x' FROM [$(P2RMIS)].[dbo].CON_Nominee_Member CNM WHERE CNM.PersonID = inserted.Person_ID) THEN 2 WHEN inserted.Institution IS NOT NULL AND LTRIM(RTRIM(inserted.Institution)) <> '' THEN 1 ELSE NULL END, VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
		   FROM inserted INNER JOIN
		   [$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonId LEFT OUTER JOIN
		   [$(DatabaseName)].[dbo].[Prefix] Prefix ON inserted.Prefix = Prefix.PrefixName LEFT OUTER JOIN
		   [$(DatabaseName)].[dbo].[MilitaryRank] MilitaryRank ON inserted.Military_Rank = MilitaryRank.MilitaryRankId LEFT OUTER JOIN
		   [$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName  


		   INSERT INTO [$(DatabaseName)].[dbo].[UserEmail]
		   ([UserInfoID]
           ,[Email]
           ,[PrimaryFlag]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		   SELECT UserInfo.UserInfoID, inserted.Email, 1, VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE
		   FROM inserted INNER JOIN
		   [$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonId INNER JOIN
		   [$(DatabaseName)].[dbo].[ViewUserInfo] UserInfo ON U.UserId = UserInfo.UserID LEFT OUTER JOIN
		   [$(DatabaseName)].[dbo].ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName
		   WHERE inserted.Email IS NOT NULL AND LTRIM(RTRIM(inserted.Email)) <> ''

		   --Only consumers should still be added in 1.0 
		   INSERT INTO [$(DatabaseName)].[dbo].[UserProfile]
		              ([UserInfoId]
           ,[ProfileTypeId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		   SELECT UserInfo.UserInfoID, CASE WHEN inserted.Employee = 1 THEN 3 ELSE 1 END, UserInfo.CreatedBy, UserInfo.CreatedDate, UserInfo.ModifiedBy, UserInfo.ModifiedDate
		   FROM inserted
			INNER JOIN  [$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonId 
			INNER JOIN  [$(DatabaseName)].[dbo].[ViewUserInfo] UserInfo ON U.UserId = UserInfo.UserID

			--Account status
			INSERT INTO [$(DatabaseName)].[dbo].[UserAccountStatus]
           ([UserId]
           ,[AccountStatusId]
           ,[AccountStatusReasonId]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
		   SELECT [U].UserID, 3, 9, [U].CreatedBy, [U].CreatedDate, [U].ModifiedBy, [U].ModifiedDate
		   FROM inserted
			INNER JOIN  [$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonId 

			--User client
			INSERT INTO [$(DatabaseName)].[dbo].[UserClient]
           ([UserID]
           ,[ClientID]
           ,[ModifiedBy]
           ,[ModifiedDate]
           ,[CreatedBy]
           ,[CreatedDate])
		   SELECT [U].UserID, ClientTable.ClientId, [U].ModifiedBy, [U].ModifiedDate, [U].CreatedBy, [U].CreatedDate
		   FROM inserted
			INNER JOIN  [$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonId 
			CROSS JOIN (Select 9 AS ClientId UNION ALL Select 19 UNION ALL Select 23) ClientTable;

			--User system role
			IF EXISTS (Select 'X' FROM inserted WHERE Employee = 0 and [Status] = 'Active')
			BEGIN
				INSERT INTO [$(DatabaseName)].[dbo].[UserSystemRole]
				([UserId]
				,[SystemRoleId]
				,[CreatedBy]
				,[CreatedDate]
				,[ModifiedBy]
				,[ModifiedDate])
				SELECT U.UserID, 12,U.CreatedBy, U.CreatedDate, U.ModifiedBy, U.ModifiedDate
				FROM inserted
				INNER JOIN  [$(DatabaseName)].[dbo].[ViewUser] U ON inserted.Person_ID = U.PersonId 
			END
		END
		--DELETE (User and UserInfo)
		ELSE
		BEGIN
		UPDATE [$(DatabaseName)].[dbo].[UserProfile]
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
			[$(DatabaseName)].[dbo].[ViewUser] U ON deleted.Person_ID = U.PersonId INNER JOIN
			[$(DatabaseName)].[dbo].[ViewUserInfo] UserInfo ON U.UserID = UserInfo.UserID INNER JOIN
			[$(DatabaseName)].[dbo].[UserProfile] UserProfile ON UserInfo.UserInfoID = UserProfile.UserInfoID
		WHERE UserProfile.DeletedFlag = 0
		UPDATE [$(DatabaseName)].[dbo].[UserAccountStatus]
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
			[$(DatabaseName)].[dbo].[ViewUser] U ON deleted.Person_ID = U.PersonId INNER JOIN
			[$(DatabaseName)].[dbo].[UserAccountStatus] UserAccountStatus ON U.UserId = UserAccountStatus.UserId
		WHERE UserAccountStatus.DeletedFlag = 0
		UPDATE [$(DatabaseName)].[dbo].[UserSystemRole]
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
			[$(DatabaseName)].[dbo].[ViewUser] U ON deleted.Person_ID = U.PersonId INNER JOIN
			[$(DatabaseName)].[dbo].[UserSystemRole] UserSystemRole ON U.UserId = UserSystemRole.UserId
		WHERE UserSystemRole.DeletedFlag = 0

		UPDATE [$(DatabaseName)].[dbo].[UserEmail]
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
			[$(DatabaseName)].[dbo].[ViewUser] U ON deleted.Person_ID = U.PersonId INNER JOIN
			[$(DatabaseName)].[dbo].[ViewUserInfo] UserInfo ON U.UserID = UserInfo.UserID INNER JOIN
			[$(DatabaseName)].[dbo].[UserEmail] UserEmail ON UserInfo.UserInfoID = UserEmail.UserInfoID
		WHERE UserEmail.DeletedFlag = 0
		UPDATE [$(DatabaseName)].[dbo].[UserInfo]
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
			[$(DatabaseName)].[dbo].[ViewUser] U ON deleted.Person_ID = U.PersonId INNER JOIN
			[$(DatabaseName)].[dbo].[UserInfo] UserInfo ON U.UserID = UserInfo.UserID
		WHERE UserInfo.DeletedFlag = 0
		UPDATE [$(DatabaseName)].[dbo].[User]
		SET	DeletedFlag = 1, DeletedDate = dbo.GetP2rmisDateTime()
		FROM deleted INNER JOIN
			[$(DatabaseName)].[dbo].[User] U ON deleted.Person_ID = U.PersonId
		WHERE U.DeletedFlag = 0
		END
END
