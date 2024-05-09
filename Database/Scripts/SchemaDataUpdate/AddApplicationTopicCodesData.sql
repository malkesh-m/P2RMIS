set identity_insert [ApplicationTopicCodes] on


-- Insert data for ApplicationTopicCodes only if the table is empty
if (select count(*) from ApplicationTopicCodes) = 0 
AND EXISTS (Select 'X' From ViewApplication WHERE ApplicationId in (143568,143567,143566,143565,143564,143563,143562,143561,143560,143559))

begin

INSERT [dbo].[ApplicationTopicCodes] ([ApplicationTopicCodeId],  [ApplicationId], [TopicCode], [CreatedBy], 
[CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES 
(1,143568, N'Scleroderma, Rheumatoid Arthritis', 4635, CAST(N'2018-07-10T15:29:35.0000000' AS DateTime2), 
4635, CAST(N'2018-07-10T15:29:35.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[ApplicationTopicCodes] ([ApplicationTopicCodeId],  [ApplicationId], [TopicCode], [CreatedBy], 
[CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES 
(2,143567, N'Rheumatoid Arthritis, Arthritis', 4635, CAST(N'2018-07-10T15:30:13.0000000' AS DateTime2), 
4635, CAST(N'2018-07-10T15:30:13.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[ApplicationTopicCodes] ([ApplicationTopicCodeId],  [ApplicationId], [TopicCode], [CreatedBy], 
[CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES 
(3, 143566, N'Rheumatoid Arthritis, Arthritis', 4635, CAST(N'2018-07-10T15:43:30.0000000' AS DateTime2), 
4635, CAST(N'2018-07-10T15:43:30.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[ApplicationTopicCodes] ([ApplicationTopicCodeId],  [ApplicationId], [TopicCode], [CreatedBy], 
[CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES 
(4, 143565, N'Scleroderma', 4635, CAST(N'2018-07-10T15:43:30.0000000' AS DateTime2), 
4635, CAST(N'2018-07-10T15:43:30.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[ApplicationTopicCodes] ([ApplicationTopicCodeId],  [ApplicationId], [TopicCode], [CreatedBy],  
[CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES
(5, 143564, N'Scleroderma, Pulmonary Fibrosis', 4635, CAST(N'2018-07-10T15:43:30.0000000' AS DateTime2), 
4635, CAST(N'2018-07-10T15:43:30.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[ApplicationTopicCodes] ([ApplicationTopicCodeId],  [ApplicationId], [TopicCode], [CreatedBy], 
[CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES 
(6, 143563, N'Rheumatoid Arthritis, Women''s Heart Disease', 4635, CAST(N'2018-07-10T15:43:30.0000000' AS DateTime2), 
4635, CAST(N'2018-07-10T15:43:30.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[ApplicationTopicCodes] ([ApplicationTopicCodeId],  [ApplicationId], [TopicCode], [CreatedBy], 
[CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES 
(7, 143562, N'Rheumatoid Arthritis, Arthritis', 4635, CAST(N'2018-07-10T15:43:30.0000000' AS DateTime2), 
4635, CAST(N'2018-07-10T15:43:30.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[ApplicationTopicCodes] ([ApplicationTopicCodeId],  [ApplicationId], [TopicCode], [CreatedBy], 
[CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES 
(8, 143561, N'Scleroderma, Pulmonary Fibrosis', 4635, CAST(N'2018-07-10T15:43:30.0000000' AS DateTime2), 
4635, CAST(N'2018-07-10T15:43:30.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[ApplicationTopicCodes] ([ApplicationTopicCodeId],  [ApplicationId], [TopicCode], [CreatedBy], 
[CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES 
(9, 143560, N'Scleroderma, Pulmonary Fibrosis', 4635, CAST(N'2018-07-10T15:43:30.0000000' AS DateTime2), 
4635, CAST(N'2018-07-10T15:43:30.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[ApplicationTopicCodes] ([ApplicationTopicCodeId],  [ApplicationId], [TopicCode], [CreatedBy], 
[CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES 
(10, 143559, N'Guillain-Barre Syndrome', 4635, CAST(N'2018-07-10T15:43:30.0000000' AS DateTime2), 
4635, CAST(N'2018-07-10T15:43:30.0000000' AS DateTime2), 0, NULL, NULL)

end

set identity_insert [ApplicationTopicCodes] off