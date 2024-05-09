INSERT INTO TrainingDocumentAccess
([TrainingDocumentId]
           ,[MeetingTypeId]
           ,[ClientParticipantTypeId]
           ,[ModifiedBy]
           ,[ModifiedDate])
SELECT TrainingDocument.TrainingDocumentId, MeetingType.MeetingTypeId, ClientParticipantType.ClientParticipantTypeId, VUN.UserID, PRG_Training_Member.Last_Update_Date
FROM [$(P2RMIS)].dbo.PRG_Training_Member PRG_Training_Member INNER JOIN
TrainingDocument ON PRG_Training_Member.TR_ID = TrainingDocument.LegacyTrId INNER JOIN
ProgramYear ON TrainingDocument.ProgramYearId = ProgramYear.ProgramYearId INNER JOIN
ClientProgram ON ProgramYear.ClientProgramId = ClientProgram.ClientProgramId INNER JOIN
ClientParticipantType ON ClientProgram.ClientId = ClientParticipantType.ClientId AND
ClientParticipantType.ReviewerFlag = 1 AND ((CASE WHEN PRG_Training_Member.Part_Type = 'ALL' THEN 1 ELSE 0 END = 1) OR (PRG_Training_Member.Part_Type = ClientParticipantType.LegacyPartTypeId)) INNER JOIN
MeetingType ON ((CASE WHEN PRG_Training_Member.Review_Type = 'ALL' THEN 1 ELSE 0 END = 1) OR (PRG_Training_Member.Review_Type = MeetingType.LegacyMeetingTypeId)) LEFT OUTER JOIN
ViewLegacyUserNameToUserId VUN ON PRG_Training_Member.Last_Updated_By = VUN.UserName
WHERE NOT EXISTS (Select 'X' FROM TrainingDocumentAccess WHERE DeletedFlag = 0 AND TrainingDocumentId = TrainingDocument.TrainingDocumentId AND MeetingTypeId = MeetingType.MeetingTypeId AND ClientParticipantTypeId = ClientParticipantType.ClientParticipantTypeId)