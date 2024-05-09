﻿--Historical program level data mapping
INSERT INTO [dbo].[ProgramSessionPayRate]
           ([ProgramYearId]
           ,[MeetingSessionId]
           ,[MeetingTypeId]
           ,[ClientParticipantTypeId]
           ,[ParticipantMethodId]
           ,[RestrictedAssignedFlag]
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
SELECT [ProgramYearId]
      ,NULL
      ,[MeetingTypeId]
      ,[ClientParticipantTypeId]
      ,[ParticipantMethodId]
      ,[RestrictedAssignedFlag]
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
      ,[ModifiedDate]
  FROM [dbo].[ProgramPayRate]
  WHERE DeletedFlag = 0;
--Session level data mapping
INSERT INTO [dbo].[ProgramSessionPayRate]
           ([ProgramYearId]
           ,[MeetingSessionId]
           ,[MeetingTypeId]
           ,[ClientParticipantTypeId]
           ,[ParticipantMethodId]
           ,[RestrictedAssignedFlag]
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
SELECT DISTINCT [ProgramYearId]
      ,[SessionPayRate].[MeetingSessionId]
      ,NULL
      ,[ClientParticipantTypeId]
      ,[ParticipantMethodId]
      ,[RestrictedAssignedFlag]
      ,[EmploymentCategoryId]
      ,[HonorariumAccepted]
      ,[ConsultantFeeText]
      ,[ConsultantFee]
      ,[PeriodStartDate]
      ,[PeriodEndDate]
      ,[ManagerList]
      ,[DescriptionOfWork]
      ,[SessionPayRate].[CreatedBy]
      ,[SessionPayRate].[CreatedDate]
      ,[SessionPayRate].[ModifiedBy]
      ,[SessionPayRate].[ModifiedDate]
  FROM [dbo].[SessionPayRate]
  INNER JOIN ViewMeetingSession ON SessionPayRate.MeetingSessionId = ViewMeetingSession.MeetingSessionId
  INNER JOIN ProgramMeeting ON ViewMeetingSession.ClientMeetingId = ProgramMeeting.ClientMeetingId
  WHERE SessionPayRate.DeletedFlag = 0 AND ProgramMeeting.DeletedFlag = 0;