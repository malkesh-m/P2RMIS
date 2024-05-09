-- =============================================
-- Author:		Ngan Dinh
-- Create date: 6/9/2019
-- Description:	This stored procedure add new Nomineee and related information to Nominee and Nominee's Sponsor
-- =============================================


CREATE PROCEDURE [dbo].[uspAddNominee] 
@Program varchar(20),
@Year varchar(8),
@Score int,
@Affected varchar(16) = null,
@Primary varchar(3),
@DieaseSite varchar(20),
@Comments varchar(255) = null,
@Prefix varchar(5) = null,
@LastName varchar(100),
@FirstName varchar(50),
@Email varchar(100),
@DOB datetime2(0) = null,
@Gender varchar(6) = null,
@Ethnicity varchar(50) = null,
@Address1 varchar(100),
@Address2 varchar(100) = null,
@City varchar(100),
@State varchar(2),
@Zip varchar(20),
@Country varchar(5),
@PhoneType1 varchar(50) = null,
@Phone1 varchar(50) = null,
@PhoneType2 varchar(50) = null,
@Phone2 varchar(50) = null,
@Organization varchar(125),
@NSLastName varchar(100),
@NSFirstName varchar(50),
@NSEmail varchar(100),
@NSTitle varchar(100) = null,
@NSAddress1 varchar(100),
@NSAddress2 varchar(100) = null,
@NSCity varchar(100),
@NSState varchar(2),
@NSZip varchar(20),
@NSCountry varchar(5),
@NSPhoneType1 varchar(50) = null,
@NSPhone1 varchar(50) = null,
@NSPhoneType2 varchar(50) = null,
@NSPhone2 varchar(50) = null,
@CreatedBy varchar(100),
@UserAddNominee varchar(100)

AS
BEGIN

	IF (@Email in (select Email from Nominee))
	BEGIN
		print 'This person already exists.'
		RETURN
	END
	ELSE
		BEGIN TRANSACTION
		SAVE TRANSACTION MySavePoint;
		BEGIN
			DECLARE @ProgramYearId int
			DECLARE @PrefixId int
			DECLARE @GenderId int
			DECLARE @EthnicityId int
			DECLARE @StateId int
			DECLARE @CountryId int
			DECLARE @PhoneType1Id int
			DECLARE @PhoneType2Id int
			DECLARE @AffectedId int
			DECLARE @PrimaryFlag bit
			DECLARE @NStateId int
			DECLARE @NSCountryId int
			DECLARE @NSPhoneType1Id int
			DECLARE @NSPhoneType2Id int
			DECLARE @CreatedById int
			DECLARE @NomineeSponsorId int
			DECLARE @NomineeId int
	
			SELECT @ProgramYearID = b.ProgramYearId
				from ClientProgram a join ViewProgramYear b on a.ClientProgramId = b.ClientProgramId
				where a.ProgramAbbreviation = @Program and b.Year = @Year
			SELECT @PrefixId = PrefixId from Prefix where PrefixName = @Prefix
			SELECT @GenderId = GenderId from Gender where Gender = @Gender
			SELECT @EthnicityId = EthnicityId from Ethnicity where Ethnicity = @Ethnicity
			SELECT @StateId = StateId from State where StateAbbreviation = @State
			SELECT @CountryId = CountryId from Country where CountryAbbreviation = @Country
			SELECT @PhoneType1Id = PhoneTypeId from PhoneType where PhoneType = @PhoneType1
			SELECT @PhoneType2Id = PhoneTypeId from PhoneType where PhoneType = @PhoneType2
			SELECT @AffectedId = NomineeAffectedId from NomineeAffected where NomineeAffected = @Affected
			SELECT @PrimaryFlag = case when @Primary = 'Yes' then 1 else 0 end
			SELECT @NStateId = StateId from State where StateAbbreviation = @NSState
			SELECT @NSCountryId = CountryId from Country where CountryAbbreviation = @NSCountry
			SELECT @NSPhoneType1Id = PhoneTypeId from PhoneType where PhoneType = @NSPhoneType1
			SELECT @NSPhoneType2Id = PhoneTypeId from PhoneType where PhoneType = @NSPhoneType2
			SELECT @CreatedById = UserId from dbo.[User] where UserLogin = @CreatedBy
			SELECT @UserAddNominee = UserId from dbo.[User] where UserLogin = @UserAddNominee
			 
			BEGIN TRY
				-- Insert into Nominee
				Insert into Nominee (PrefixId, LastName, MiddleName, FirstName, GenderId, EthnicityId, UserId, Email, DOB,
					Address1, Address2, City, StateId, ZipCode, CountryId, 
					CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, DeletedFlag, DeletedBy, DeletedDate)
				values (@PrefixId, @LastName, null, @FirstName, @GenderId, @EthnicityId, null, @Email, @DOB, 
					@Address1, @Address2, @City, @StateId, @Zip, @CountryId, 
					@CreatedById, dbo.GetP2rmisDateTime(), @UserAddNominee, dbo.GetP2rmisDateTime(), 0, null, null)
				print 'Succesfully added to Nominee' 
				
				-- Get NomineeId just inserted to Nomine
				SELECT @NomineeId =  SCOPE_IDENTITY() 

				-- Insert into NomineeSponsor
				Insert into NomineeSponsor (LegacyNomineeId, Organization, LastName, FirstName, Email, Title, 
					Address1, Address2, City, StateId, ZipCode, CountryId,
					CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, DeletedFlag, DeletedBy, DeletedDate)
				values(null, @Organization, @NSLastName, @NSFirstName, @NSEmail, @NSTitle, 
					@NSAddress1, @NSAddress2, @NSCity, @NStateId, @NSZip, @NSCountryId, 
					@CreatedById, dbo.GetP2rmisDateTime(), @UserAddNominee, dbo.GetP2rmisDateTime(), 0, null, null)
				print 'Succesfully added to NomineeSponsor'

				-- Get NomineeSponsorId just inserted from NomineeSponsor
				SELECT @NomineeSponsorId = SCOPE_IDENTITY() 
		
				-- Insert into NomineeSponsorPhone
				if (@NSPhone1 IS NOT NULL and @NSPhoneType1Id IS NOT NULL
						or (@NSPhone1 <> '' and @NSPhoneType1Id <> ''))
				begin
					Insert into NomineeSponsorPhone (NomineeSponsorId, PhoneTypeId, Phone, Extension, PrimaryFlag,
						CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, DeletedFlag, DeletedBy, DeletedDate)
					values (@NomineeSponsorId, @NSPhoneType1Id, @NSPhone1, null, 
						(select case when @NSPhoneType1 = 'Desk' then 1 else 0 end), 
						@CreatedById, dbo.GetP2rmisDateTime(), @UserAddNominee, dbo.GetP2rmisDateTime(), 0, null, null)
					print 'Inserted ' + @NSPhoneType1 + ' to NomineeSponsorPhone'
				end
		
				if (@NSPhone2 IS NOT NULL and @NSPhoneType2Id IS NOT NULL
						or (@NSPhone2 <> '' and @NSPhoneType2Id <> ''))
				begin
					Insert into NomineeSponsorPhone (NomineeSponsorId, PhoneTypeId, Phone, Extension, PrimaryFlag,
						CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, DeletedFlag, DeletedBy, DeletedDate)
					values (@NomineeSponsorId, @NSPhoneType2Id, @NSPhone2, null, 
						(select case when @NSPhoneType2 = 'Desk' then 1 else 0 end), 
						@CreatedById, dbo.GetP2rmisDateTime(), @UserAddNominee, dbo.GetP2rmisDateTime(), 0, null, null)
					print 'Inserted ' + @NSPhoneType2 + ' to NomineeSponsorPhone'
				end

				print 'Succesfully added to NomineeSponsorPhone'

				-- Insert into NomineePhone
				if (@Phone1 IS NOT NULL and @PhoneType1Id IS NOT NULL
						or (@Phone1 <> '' and @PhoneType1Id <> ''))
				begin
					Insert into NomineePhone (NomineeId, PhoneTypeId, Phone, Extension, PrimaryFlag,
						CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, DeletedFlag, DeletedBy, DeletedDate)
					values (@NomineeId, @PhoneType1Id, @Phone1, null, 
						(select case when @PhoneType1 = 'Home' then 1 else 0 end), 
						@CreatedById, dbo.GetP2rmisDateTime(), @UserAddNominee, dbo.GetP2rmisDateTime(), 0, null, null)
					print 'Inserted ' + @PhoneType1 + ' to NomineePhone'
				end
		
				if (@Phone2 IS NOT NULL and @PhoneType2Id IS NOT NULL
						or (@Phone2 <> '' and @PhoneType2Id <> ''))
				begin
					Insert into NomineePhone (NomineeId, PhoneTypeId, Phone, Extension, PrimaryFlag,
						CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, DeletedFlag, DeletedBy, DeletedDate)
					values (@NomineeId, @PhoneType2Id, @Phone2, null, 
					(select case when @PhoneType2 = 'Home' then 1 else 0 end), 
						@CreatedById, dbo.GetP2rmisDateTime(), @UserAddNominee, dbo.GetP2rmisDateTime(), 0, null, null)
				print 'Inserted ' + @PhoneType2 + ' to NomineePhone'
				end
	
				print 'Succesfully added to NomineePhone'

				-- Insert into NomineeProgram
				Insert into NomineeProgram (NomineeId, LegacyNomineeId, NomineeSponsorId, ProgramYearId, NomineeTypeId,
					Score,NomineeAffectedId, PrimaryFlag, DiseaseSite, Comments,
					CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, DeletedFlag, DeletedBy, DeletedDate)
				values (@NomineeId, null, @NomineeSponsorId, @ProgramYearId, 
					(select NomineeTypeId from NomineeType where NomineeType = 'Eligible Nominee'), 
					@Score, @AffectedId, @PrimaryFlag, @DieaseSite, @Comments,
					@CreatedById, dbo.GetP2rmisDateTime(), @UserAddNominee, dbo.GetP2rmisDateTime(), 0, null, null)
		
				print 'Succesfully added to NomineeProgram'

				COMMIT TRANSACTION 
			END TRY

			BEGIN CATCH
				IF @@TRANCOUNT > 0
				BEGIN
					ROLLBACK TRANSACTION MySavePoint; -- rollback to MySavePoint
				END
			END CATCH

	END
END



GO

GRANT EXECUTE
    ON OBJECT::[dbo].[uspAddNominee] TO [NetSqlAzMan_Users]
    AS [dbo];