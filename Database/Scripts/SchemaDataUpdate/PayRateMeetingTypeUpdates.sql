--First remove all current pay rates
UPDATe ProgramPayRate SET DeletedFlag = 1, DeletedBy = 10, DeletedDate = dbo.GetP2rmisDateTime() WHERE DeletedFlag = 0
--Reload new pay rates with corrected meeting types
INSERT INTO [$(DatabaseName)].[dbo].[ProgramPayRate]
           ([ProgramYearId]
           ,[ClientParticipantTypeId]
		   ,[ParticipantMethodId]
		   ,[RestrictedAssignedFlag]
		   ,[MeetingTypeId]
		   ,[EmploymentCategoryId]
           ,[HonorariumAccepted]
           ,[ConsultantFeeText]
		   ,[ConsultantFee]
           ,[PeriodStartDate]
           ,[PeriodEndDate]
           ,[ManagerList]
           ,[DescriptionOfWork]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT py.ProgramYearId, cpt.ClientParticipantTypeId, PartTypeMapping.NewParticipantMethod, PartTypeMapping.RestrictedAssignedFlag, MeetingType.MeetingTypeId, CASE inserted.EC_ID WHEN 13 THEN 1 WHEN 14 THEN 2 ELSE 3 END, CASE inserted.EC_ID WHEN 13 THEN 'Paid' WHEN 14 THEN 'Unpaid' ELSE 'Unpaid w/t' END, inserted.Consultant_Fee, inserted.Fixed_Amount,
	inserted.Period_Start, inserted.Period_End, inserted.SRA_Managers, inserted.Work_Description, VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE 
	FROM [$(P2RMIS)].dbo.PRG_Part_Pay_Rate inserted
	INNER JOIN [$(DatabaseName)].[dbo].ClientProgram cp ON inserted.Program = cp.LegacyProgramId 
	INNER JOIN [$(DatabaseName)].[dbo].ProgramYear py ON cp.ClientProgramId = py.ClientProgramId AND inserted.FY = py.[Year] 
	CROSS APPLY [$(DatabaseName)].[dbo].udfLegacyToNewParticipantTypeMap(inserted.Part_Type, 0, 1, cp.ClientId) PartTypeMapping
	INNER JOIN [$(DatabaseName)].[dbo].ClientParticipantType cpt ON PartTypeMapping.NewParticipantTypeAbbreviation = cpt.ParticipantTypeAbbreviation AND cp.ClientId = cpt.ClientId
	LEFT OUTER JOIN [$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	CROSS JOIN [$(DatabaseName)].[dbo].MeetingType MeetingType
	WHERE inserted.EC_ID IN (13, 14) AND (MeetingType.MeetingTypeId = CASE WHEN inserted.Part_Type IN ('OR', 'OCH', 'OCR') THEN 3 END OR MeetingType.MeetingTypeId = CASE WHEN inserted.Part_Type IN ('TC', 'CHT', 'CRT') THEN 2 END OR MeetingType.MeetingTypeId = CASE WHEN inserted.Part_Type IN ('TC','CHT','CRT','CR','SR','AH','CH') THEN 1 END)

	INSERT INTO [$(DatabaseName)].[dbo].[ProgramPayRate]
           ([ProgramYearId]
           ,[ClientParticipantTypeId]
		   ,[ParticipantMethodId]
		   ,[RestrictedAssignedFlag]
		   ,[MeetingTypeId]
		   ,[EmploymentCategoryId]
           ,[HonorariumAccepted]
           ,[ConsultantFeeText]
		   ,[ConsultantFee]
           ,[PeriodStartDate]
           ,[PeriodEndDate]
           ,[ManagerList]
           ,[DescriptionOfWork]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
	SELECT py.ProgramYearId, cpt.ClientParticipantTypeId, PartTypeMapping.NewParticipantMethod, PartTypeMapping.RestrictedAssignedFlag, MeetingType.MeetingTypeId, CASE inserted.EC_ID WHEN 13 THEN 1 WHEN 14 THEN 2 ELSE 3 END, CASE inserted.EC_ID WHEN 13 THEN 'Paid' WHEN 14 THEN 'Unpaid' ELSE 'Unpaid w/t' END, inserted.Consultant_Fee, inserted.Fixed_Amount,
	inserted.Period_Start, inserted.Period_End, inserted.SRA_Managers, inserted.Work_Description, VUN.UserId, inserted.LAST_UPDATE_DATE, VUN.UserId, inserted.LAST_UPDATE_DATE 
	FROM [$(P2RMIS)].dbo.PRG_Part_Pay_Rate inserted
	INNER JOIN [$(DatabaseName)].[dbo].ClientProgram cp ON inserted.Program = cp.LegacyProgramId 
	INNER JOIN [$(DatabaseName)].[dbo].ProgramYear py ON cp.ClientProgramId = py.ClientProgramId AND inserted.FY = py.[Year] 
	CROSS APPLY [$(DatabaseName)].[dbo].udfLegacyToNewParticipantTypeMap(inserted.Part_Type, 1, 1, cp.ClientId) PartTypeMapping
	INNER JOIN [$(DatabaseName)].[dbo].ClientParticipantType cpt ON PartTypeMapping.NewParticipantTypeAbbreviation = cpt.ParticipantTypeAbbreviation AND cp.ClientId = cpt.ClientId
	LEFT OUTER JOIN [$(DatabaseName)].dbo.ViewLegacyUserNameToUserId VUN ON inserted.LAST_UPDATED_BY = VUN.UserName 
	CROSS JOIN [$(DatabaseName)].[dbo].MeetingType MeetingType
	WHERE inserted.EC_ID IN (13, 14) AND inserted.Part_Type IN ('SR', 'OR', 'TC', 'AH') AND (MeetingType.MeetingTypeId = CASE WHEN inserted.Part_Type IN ('OR', 'OCH', 'OCR') THEN 3 END OR MeetingType.MeetingTypeId = CASE WHEN inserted.Part_Type IN ('TC', 'CHT', 'CRT') THEN 2 END OR MeetingType.MeetingTypeId = CASE WHEN inserted.Part_Type IN ('TC','CHT','CRT','CR','SR','AH','CH') THEN 1 END)

