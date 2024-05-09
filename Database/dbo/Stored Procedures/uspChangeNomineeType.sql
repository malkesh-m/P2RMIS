-- =============================================
-- Author:		Ngan Dinh
-- Create date: 6/3/2019
-- Description:	This stored procedure changes Nominee Type for Nominee
-- =============================================

CREATE PROCEDURE [dbo].[uspChangeNomineeType] 
@NomineeTypeUpdatedTo varchar(20),
@Program varchar(20), 
@Year varchar(8), 
@FName  varchar(50), 
@LName varchar(100),
@Email varchar(100),
@UserChangeNomineeType varchar(100)

AS
BEGIN

	DECLARE @ProgramYearId int
	DECLARE @NomineeId int
	DECLARE @NomineeSponsorId int
	DECLARE @UserId int
	DECLARE @EligibleNomineeType varchar(20)
	DECLARE @IneligibleNomineeType varchar(20)
	DECLARE @SelectedNoviceType varchar(20)

	select @ProgramYearID = b.ProgramYearId
	from ClientProgram a join ViewProgramYear b on a.ClientProgramId = b.ClientProgramId
	where a.ProgramAbbreviation = @Program and b.Year = @Year

	select @NomineeId = NomineeID 
	from Nominee 
	where  LastName = @LName and FirstName =  @FName and Email = @Email
	print @NomineeId

	select @NomineeSponsorId = NomineeSponsorID 
	from NomineeProgram
	where  NomineeId = @NomineeId
	print @NomineeId

	SELECT @UserChangeNomineeType = UserId from dbo.[User] where UserLogin = @UserChangeNomineeType

	Select @EligibleNomineeType = 'Eligible Nominee'
	Select @IneligibleNomineeType = 'Ineligible Nominee'
	Select @SelectedNoviceType = 'Selected Novice'

	if @NomineeTypeUpdatedTo = @EligibleNomineeType
	begin
		update NomineeProgram
		set NomineeTypeId = (Select NomineeTypeId from NomineeType where NomineeType = @EligibleNomineeType), 
			ModifiedDate = dbo.GetP2rmisDateTime() ,
			ModifiedBy = @UserChangeNomineeType
		where ProgramYearId = @ProgramYearID and NomineeId = @NomineeId

		print 'Changed Nominee Type to Eligible Nominee'
	end
	
	else if @NomineeTypeUpdatedTo = @IneligibleNomineeType
	begin
		update NomineeProgram
		set NomineeTypeId = (Select NomineeTypeId from NomineeType where NomineeType = @IneligibleNomineeType), 
			ModifiedDate = dbo.GetP2rmisDateTime() ,
			ModifiedBy = @UserChangeNomineeType
		where ProgramYearId = @ProgramYearID and NomineeId = @NomineeId
	
		print 'Change Nominee Type to Ineligibe Nominee'
	end

	else if @NomineeTypeUpdatedTo =  @SelectedNoviceType 
	BEGIN
		Select @UserId = userId from Nominee where nomineeId = @NomineeId
		if @userId in (select Userid from dbo.[user])
		begin
			update NomineeProgram
			set NomineeTypeId = (Select NomineeTypeId from NomineeType where NomineeType = @SelectedNoviceType), 
				ModifiedDate = dbo.GetP2rmisDateTime() ,
				ModifiedBy = @UserChangeNomineeType
			where ProgramYearId = @ProgramYearID and NomineeId = @NomineeId
		
			print 'User exists, changed Nominee type to Selected Novice only'
		end
		
		else
		begin
			BEGIN TRANSACTION
			SAVE TRANSACTION MySavePoint;
			BEGIN TRY
				update NomineeProgram
				set NomineeTypeId = (Select NomineeTypeId from NomineeType where NomineeType = @SelectedNoviceType), 
					ModifiedDate = dbo.GetP2rmisDateTime() ,
					ModifiedBy = @UserChangeNomineeType
				where ProgramYearId = @ProgramYearID and NomineeId = @NomineeId
		
				INSERT INTO [dbo].[User] (UserLogin, IsActivated,  LastLoginDate,IsLockedOut,LastLockedOutDate,CreatedBy,CreatedDate,DeletedFlag)
					VALUES ((select [dbo].[udfGetNewUserName](@FName, @LName)),1,dbo.GetP2rmisDateTime(),0,dbo.GetP2rmisDateTime(),@UserChangeNomineeType,dbo.GetP2rmisDateTime(),0)
				print 'New User created'
			
				SELECT @UserId = SCOPE_IDENTITY()
				print 'UserId is ' + cast(@UserId as varchar(max))
						
				update Nominee
				set Userid = @UserId where nomineeId = @NomineeId
	
				insert into UserInfo(userId, PrefixId, FirstName, LastName,GenderId,EthnicityId,
					Institution,Position,degreeNotApplicable,ProfessionalAffiliationId,CreatedBy,CreatedDate,DeletedFlag) 
					values (@UserId, 
						(select PrefixId from Nominee where nomineeId = @nomineeId),
						@FName, @LName,
						(select GenderId from Nominee where nomineeId = @nomineeId),
						(select EthnicityId from Nominee where nomineeId = @nomineeId),
						(select Organization from NomineeSponsor where nomineeSponsorId = @nomineeSponsorId),
						(select Title from NomineeSponsor where LegacyNomineeId = @nomineeId),
						0,2,@UserChangeNomineeType,dbo.GetP2rmisDateTime(),0)
				print 'Inserted to UserInfo'

				insert into userAccountStatus(UserId,AccountStatusId,AccountStatusReasonId,CreatedBy,CreatedDate,DeletedFlag)
					values (@UserId,13,1,@UserChangeNomineeType,dbo.GetP2rmisDateTime(),0)
				print 'Inserted to UserAccountStatus'

				insert into  UserAccountStatusChangeLog(UserId,NewAccountStatusId, OldAccountStatusId, NewAccountStatusReasonId, OldAccountStatusReasonId,CreatedBy,CreatedDate,DeletedFlag)
					values (@UserId,13,null,1,null,@UserChangeNomineeType,dbo.GetP2rmisDateTime(),0)
				print 'Inserted to UserAccountStatusChangeLog'

				insert into userSystemRole(UserId,SystemRoleId,CreatedBy,CreatedDate,DeletedFlag)
					values (@UserId, 12,@UserChangeNomineeType, dbo.GetP2rmisDateTime(), 0)
				print 'Inserted to UserSystemRole'

				insert into userClient (UserId,ClientId,CreatedBy,CreatedDate, DeletedFlag)
					values (@UserId, 19,@UserChangeNomineeType, dbo.GetP2rmisDateTime(), 0)
				print 'Inserted UserClient'

				DECLARE @UserInfoId int
				Select @UserInfoId = userInfoId from UserInfo where userId = @UserId
			
				Insert into userProfile(UserInfoId,ProfileTypeId,CreatedBy,CreatedDate, DeletedFlag)
					values(@UserInfoId, 2,@UserChangeNomineeType,dbo.GetP2rmisDateTime(),0)
				print 'Inserted to UserProfile'

				insert into userAddress(userInfoId,AddressTypeId,PrimaryFlag,address1,address2,city,stateId,Zip,CountryId,CreatedBy,CreatedDate, deletedFlag)
					values(@UserInfoId,3,1,
						(select Address1 from Nominee where nomineeId = @NomineeId),
						(select Address2 from Nominee where nomineeId = @NomineeId),
						(select City from Nominee where nomineeId = @NomineeId),
						(select stateId from Nominee where nomineeId = @NomineeId),
						(select ZipCode from Nominee where nomineeId = @NomineeId),
						(select CountryId from Nominee where nomineeId = @NomineeId),
						@UserChangeNomineeType, dbo.GetP2rmisDateTime(),0)
				print 'Inserted to UserAddress'

				insert into userEmail (userInfoId, EmailAddressTypeId, Email, PrimaryFlag,CreatedBy,CreatedDate,DeletedFlag)
					values (@UserInfoId, 1, @Email,1,@UserChangeNomineeType,dbo.GetP2rmisDateTime(),0)			
				print 'Inserted to UserEmail'

				if (NOT(select phone from NomineePhone where nomineeId = @NomineeId and PhoneTypeId = 6) IS NULL
					or (select phone from NomineePhone where nomineeId = @NomineeId and PhoneTypeId = 6) <> '')
				begin
					insert into userPhone (userInfoId, phonetypeId,Phone,PrimaryFlag,CreatedBy,createdDate, deletedFlag)
						values (@UserInfoId, 6, 
								(select phone from NomineePhone where nomineeId = @NomineeId and PhoneTypeId = 6),
								1,@UserChangeNomineeType,dbo.GetP2rmisDateTime(),0)
					print 'Inserted home phone to UserPhone'
				end
				if (NOT(select phone from NomineePhone where nomineeId = @NomineeId and PhoneTypeId = 3) IS NULL
					or (select phone from NomineePhone where nomineeId = @NomineeId and PhoneTypeId = 3) <> '')
				begin
					insert into UserPhone (userInfoId, phonetypeId,Phone,PrimaryFlag,CreatedBy,createdDate, deletedFlag)
						values (@UserInfoId, 3, 
								(select phone from NomineePhone where nomineeId = @NomineeId and PhoneTypeId = 3),
								0,@UserChangeNomineeType,dbo.GetP2rmisDateTime(),0)
				end
				print 'Inserted desk phone to UserPhone'
				
				COMMIT TRANSACTION 
			END TRY

			BEGIN CATCH
				IF @@TRANCOUNT > 0
				BEGIN
					ROLLBACK TRANSACTION MySavePoint; -- rollback to MySavePoint
				END
			END CATCH

		end
	END
END


GO


GRANT EXECUTE
    ON OBJECT::[dbo].[uspChangeNomineeType] TO [NetSqlAzMan_Users]
    AS [dbo];