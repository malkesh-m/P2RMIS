﻿CREATE VIEW [dbo].[ViewMeetingRegistrationComment]
	AS SELECT [MeetingRegistrationCommentId]
	  ,[MeetingRegistrationId]
	  ,[InternalComments]
      ,[CreatedBy]
      ,[CreatedDate]
      ,[ModifiedBy]
      ,[ModifiedDate]
      ,[DeletedFlag]
      ,[DeletedBy]
      ,[DeletedDate]
  FROM [dbo].[MeetingRegistrationComment]
  WHERE DeletedFlag = 0