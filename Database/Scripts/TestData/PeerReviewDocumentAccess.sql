INSERT [dbo].[PeerReviewDocumentAccess] ([PeerReviewDocumentId], [MeetingTypeIds], [ClientParticipantTypeIds], [ParticipationMethodIds], [RestrictedAssignedFlag], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (1, NULL, NULL, NULL, NULL, 10, CAST(N'2017-11-07 00:00:00.0000000' AS DateTime2), 10, CAST(N'2017-11-07 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[PeerReviewDocumentAccess] ([PeerReviewDocumentId], [MeetingTypeIds], [ClientParticipantTypeIds], [ParticipationMethodIds], [RestrictedAssignedFlag], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (3, NULL, NULL, NULL, NULL, 10, CAST(N'2017-11-07 00:00:00.0000000' AS DateTime2), 10, CAST(N'2017-11-07 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[PeerReviewDocumentAccess] ([PeerReviewDocumentId], [MeetingTypeIds], [ClientParticipantTypeIds], [ParticipationMethodIds], [RestrictedAssignedFlag], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (4, NULL, NULL, NULL, NULL, 10, CAST(N'2017-11-07 00:00:00.0000000' AS DateTime2), 10, CAST(N'2017-11-07 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[PeerReviewDocumentAccess] ([PeerReviewDocumentId], [MeetingTypeIds], [ClientParticipantTypeIds], [ParticipationMethodIds], [RestrictedAssignedFlag], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (6, '1', NULL, NULL, NULL, 10, CAST(N'2017-11-07 00:00:00.0000000' AS DateTime2), 10, CAST(N'2017-11-07 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[PeerReviewDocumentAccess] ([PeerReviewDocumentId], [MeetingTypeIds], [ClientParticipantTypeIds], [ParticipationMethodIds], [RestrictedAssignedFlag], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (2, NULL, NULL, '1', 0, 10, CAST(N'2017-11-07 00:00:00.0000000' AS DateTime2), 10, CAST(N'2017-11-07 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[PeerReviewDocumentAccess] ([PeerReviewDocumentId], [MeetingTypeIds], [ClientParticipantTypeIds], [ParticipationMethodIds], [RestrictedAssignedFlag], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate])
SELECT 5, NULL, CAST(ClientParticipantTypeId AS varchar(8)), NULL, 0, 10, CAST(N'2017-11-07 00:00:00.0000000' AS DateTime2), 10, CAST(N'2017-11-07 00:00:00.0000000' AS DateTime2), 0, NULL, NULL
FROM ClientParticipantType
WHERE ClientId = 19 AND ParticipantTypeAbbreviation = 'SR'
