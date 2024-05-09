SET IDENTITY_INSERT [dbo].[WorkflowStep] ON 

--Standardizes workflow across environments for upcoming release
UPDATE Workflow SET WorkflowName = 'CPRIT Res/Prod/Rec' WHERE WorkflowId IN (75, 76)
--Delete and re-add all workflow steps
DELETE FROM WorkflowStep;
SET IDENTITY_INSERT [dbo].[WorkflowStep] ON 

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (1, 2, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (3, 2, 1, N'SQC', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (4, 2, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (5, 2, 2, N'EQC', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (6, 2, 10, N'EQR', 5, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (7, 2, 9, N'SMR', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (9, 2, 10, N'Editing-2', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (10, 2, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (11, 2, 9, N'SMR-2', 9, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (647, 2, 10, N'Editing-3', 10, 1, 11, CAST(N'2015-07-16 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-07-16 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (648, 2, 10, N'Production', 11, 1, 11, CAST(N'2015-07-16 00:00:00.0000000' AS DateTime2), 11, CAST(N'2015-07-16 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (660, 3, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (661, 3, 1, N'SQC', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (662, 3, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (663, 3, 2, N'EQC', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (664, 3, 10, N'EQR', 5, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (665, 3, 9, N'SMR', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (666, 3, 10, N'Editing-2', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (667, 3, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (668, 3, 9, N'SMR-2', 9, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (669, 3, 10, N'Editing-3', 10, 1, 11, CAST(N'2015-07-16 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-07-16 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (670, 3, 10, N'Production', 11, 1, 11, CAST(N'2015-07-16 00:00:00.0000000' AS DateTime2), 11, CAST(N'2015-07-16 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (21, 4, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (22, 6, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (23, 6, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (24, 7, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (25, 8, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (26, 9, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (27, 9, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (28, 9, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (29, 10, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (30, 10, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (31, 11, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (32, 11, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (33, 11, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (34, 11, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (35, 11, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (36, 11, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (37, 11, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (38, 11, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (39, 11, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (220, 12, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (221, 12, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (222, 12, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (223, 12, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (224, 12, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (225, 12, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (226, 12, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (227, 12, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (228, 12, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (409, 13, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (430, 14, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (431, 14, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (472, 15, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (493, 16, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (514, 17, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (515, 17, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (516, 17, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (577, 18, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (578, 18, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (579, 19, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (580, 19, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (517, 20, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (518, 20, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (519, 20, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (494, 21, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (473, 22, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (432, 23, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (433, 23, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (410, 24, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (229, 25, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (230, 25, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (231, 25, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (232, 25, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (233, 25, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (234, 25, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (235, 25, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (236, 25, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (237, 25, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (40, 26, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (41, 26, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (42, 26, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (43, 26, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (44, 26, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (45, 26, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (46, 26, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (47, 26, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (48, 26, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (49, 27, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (50, 27, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (51, 27, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (52, 27, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (53, 27, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (54, 27, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (55, 27, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (56, 27, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (57, 27, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (238, 28, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (239, 28, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (240, 28, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (241, 28, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (242, 28, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (243, 28, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (244, 28, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (245, 28, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (246, 28, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (411, 29, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (434, 30, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (435, 30, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (474, 31, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (495, 32, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (520, 33, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (521, 33, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (522, 33, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (581, 34, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (582, 34, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (583, 35, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (584, 35, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (523, 36, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (524, 36, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (525, 36, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (496, 37, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (475, 38, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (436, 39, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (437, 39, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (412, 40, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (247, 41, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (248, 41, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (249, 41, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (250, 41, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (251, 41, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (252, 41, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (253, 41, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (254, 41, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (255, 41, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (58, 42, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (59, 42, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (60, 42, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (61, 42, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (62, 42, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (63, 42, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (64, 42, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (65, 42, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (66, 42, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (67, 43, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (68, 43, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (69, 43, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (70, 43, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (71, 43, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (72, 43, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (73, 43, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (74, 43, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (75, 43, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (256, 44, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (257, 44, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (258, 44, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (259, 44, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (260, 44, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (261, 44, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (262, 44, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (263, 44, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (264, 44, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (413, 45, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (438, 46, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (439, 46, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (476, 47, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (497, 48, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (526, 49, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (527, 49, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (528, 49, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (585, 50, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (586, 50, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (587, 51, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (588, 51, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (529, 52, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (530, 52, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (531, 52, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (498, 53, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (477, 54, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (440, 55, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (441, 55, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (414, 56, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (265, 57, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (266, 57, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (267, 57, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (268, 57, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (269, 57, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (270, 57, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (271, 57, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (272, 57, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (273, 57, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (76, 58, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (77, 58, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (78, 58, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (79, 58, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (80, 58, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (81, 58, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (82, 58, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (83, 58, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (84, 58, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (166, 59, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (167, 59, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (168, 59, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (169, 59, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (170, 59, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (171, 59, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (172, 59, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (173, 59, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (174, 59, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (355, 60, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (356, 60, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (357, 60, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (358, 60, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (359, 60, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (360, 60, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (361, 60, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (362, 60, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (363, 60, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (424, 61, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (460, 62, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (461, 62, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (487, 63, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (508, 64, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (559, 65, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (560, 65, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (561, 65, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (607, 66, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (608, 66, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (609, 67, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (610, 67, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (562, 68, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (563, 68, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (564, 68, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (509, 69, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (488, 70, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (462, 71, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (463, 71, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (425, 72, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (364, 73, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (365, 73, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (366, 73, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (367, 73, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (368, 73, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (369, 73, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (370, 73, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (371, 73, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (372, 73, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (175, 74, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (176, 74, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (177, 74, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (178, 74, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (179, 74, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (180, 74, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (181, 74, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (182, 74, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (183, 74, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (671, 75, 2, N'Editing', 1, 1, 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (672, 75, 2, N'QA Editing', 2, 1, 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (673, 75, 10, N'Query Review', 3, 1, 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (674, 75, 10, N'SM Review', 4, 1, 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (675, 75, 10, N'Proofread', 5, 1, 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (676, 75, 10, N'All Edits Done', 6, 1, 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (677, 75, 10, N'Final', 7, 1, 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (678, 76, 1, N'Writing', 1, 1, 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (682, 76, 1, N'SQA', 2, 1, 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (679, 76, 2, N'Editing', 3, 1, 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (680, 76, 2, N'QA Editing', 4, 1, 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (681, 76, 10, N'Query Review', 5, 1, 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (683, 76, 10, N'SM Review', 6, 1, 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (684, 76, 10, N'Proofread', 7, 1, 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (685, 76, 10, N'All Edits Done', 8, 1, 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (686, 76, 10, N'Final', 9, 1, 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 10, CAST(N'2016-06-02 15:30:51.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (464, 78, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (465, 78, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (489, 79, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (510, 80, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (565, 81, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (566, 81, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (567, 81, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (611, 82, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (612, 82, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (613, 83, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (614, 83, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (568, 84, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (569, 84, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (570, 84, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (511, 85, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (490, 86, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (466, 87, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (467, 87, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (427, 88, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (382, 89, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (383, 89, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (384, 89, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (385, 89, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (386, 89, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (387, 89, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (388, 89, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (389, 89, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (390, 89, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (193, 90, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (194, 90, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (195, 90, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (196, 90, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (197, 90, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (198, 90, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (199, 90, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (200, 90, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (201, 90, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (202, 91, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (203, 91, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (204, 91, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (205, 91, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (206, 91, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (207, 91, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (208, 91, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (209, 91, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (210, 91, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (391, 92, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (392, 92, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (393, 92, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (394, 92, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (395, 92, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (396, 92, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (397, 92, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (398, 92, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (399, 92, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (428, 93, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (468, 94, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (469, 94, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (491, 95, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (512, 96, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (571, 97, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (572, 97, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (573, 97, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (615, 98, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (616, 98, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (617, 99, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (618, 99, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (574, 100, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (575, 100, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (576, 100, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (513, 101, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (492, 102, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (470, 103, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (471, 103, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (429, 104, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (400, 105, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (401, 105, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (402, 105, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (403, 105, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (404, 105, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (405, 105, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (406, 105, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (407, 105, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (408, 105, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (211, 106, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (212, 106, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (213, 106, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (214, 106, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (215, 106, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (216, 106, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (217, 106, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (218, 106, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (219, 106, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (112, 107, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (113, 107, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (114, 107, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (115, 107, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (116, 107, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (117, 107, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (118, 107, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (119, 107, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (120, 107, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (301, 108, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (302, 108, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (303, 108, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (304, 108, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (305, 108, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (306, 108, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (307, 108, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (308, 108, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (309, 108, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (418, 109, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (448, 110, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (449, 110, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (481, 111, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (502, 112, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (541, 113, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (542, 113, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (543, 113, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (595, 114, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (596, 114, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (597, 115, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (598, 115, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (544, 116, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (545, 116, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (546, 116, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (503, 117, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (482, 118, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (450, 119, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (451, 119, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (419, 120, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (310, 121, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (311, 121, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (312, 121, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (313, 121, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (314, 121, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (315, 121, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (316, 121, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (317, 121, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (318, 121, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (121, 122, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (122, 122, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (123, 122, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (124, 122, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (125, 122, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (126, 122, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (127, 122, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (128, 122, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (129, 122, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (130, 123, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (131, 123, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (132, 123, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (133, 123, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (134, 123, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (135, 123, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (136, 123, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (137, 123, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (138, 123, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (319, 124, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (320, 124, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (321, 124, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (322, 124, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (323, 124, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (324, 124, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (325, 124, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (326, 124, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (327, 124, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (420, 125, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (452, 126, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (453, 126, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (483, 127, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (504, 128, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (547, 129, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (548, 129, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (549, 129, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (599, 130, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (600, 130, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (601, 131, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (602, 131, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (550, 132, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (551, 132, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (552, 132, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (505, 133, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (484, 134, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (454, 135, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (455, 135, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (421, 136, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (328, 137, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (329, 137, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (330, 137, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (331, 137, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (332, 137, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (333, 137, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (334, 137, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (335, 137, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (336, 137, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (139, 138, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (140, 138, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (141, 138, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (142, 138, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (143, 138, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (144, 138, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (145, 138, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (146, 138, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (147, 138, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (148, 139, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (149, 139, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (150, 139, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (151, 139, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (152, 139, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (153, 139, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (154, 139, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (155, 139, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (156, 139, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (337, 140, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (338, 140, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (339, 140, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (340, 140, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (341, 140, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (342, 140, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (343, 140, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (344, 140, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (345, 140, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (422, 141, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (456, 142, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (457, 142, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (485, 143, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (506, 144, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (553, 145, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (554, 145, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (555, 145, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (603, 146, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (604, 146, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (605, 147, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (606, 147, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (556, 148, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (557, 148, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (558, 148, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (507, 149, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (486, 150, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (458, 151, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (459, 151, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (423, 152, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (346, 153, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (347, 153, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (348, 153, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (349, 153, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (350, 153, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (351, 153, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (352, 153, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (353, 153, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (354, 153, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (157, 154, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (158, 154, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (159, 154, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (160, 154, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (161, 154, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (162, 154, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (163, 154, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (164, 154, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (165, 154, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (85, 155, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (86, 155, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (87, 155, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (88, 155, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (89, 155, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (90, 155, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (91, 155, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (92, 155, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (93, 155, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (274, 156, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (275, 156, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (276, 156, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (277, 156, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (278, 156, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (279, 156, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (280, 156, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (281, 156, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (282, 156, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (415, 157, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (442, 158, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (443, 158, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (478, 159, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (499, 160, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (532, 161, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (533, 161, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (534, 161, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (589, 162, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (590, 162, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (591, 163, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (592, 163, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (535, 164, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (536, 164, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (537, 164, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (500, 165, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (479, 166, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (444, 167, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (445, 167, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (416, 168, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (283, 169, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (284, 169, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (285, 169, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (286, 169, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (287, 169, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (288, 169, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (289, 169, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (290, 169, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (291, 169, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (94, 170, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (95, 170, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (96, 170, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (97, 170, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (98, 170, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (99, 170, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (100, 170, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (101, 170, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (102, 170, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (103, 171, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (104, 171, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (105, 171, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (106, 171, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (107, 171, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (108, 171, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (109, 171, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (110, 171, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (111, 171, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (292, 172, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (293, 172, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (294, 172, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (295, 172, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (296, 172, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (297, 172, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (298, 172, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (299, 172, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (300, 172, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (417, 173, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (446, 174, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (447, 174, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (480, 175, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (501, 176, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (538, 177, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (539, 177, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (540, 177, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (593, 178, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (594, 178, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-04 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)



IF EXISTS (Select 'X' FROM Workflow WHERE ClientId = 23)
BEGIN
INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (619, 179, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (620, 179, 1, N'Scientific QA', 2, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (621, 179, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (622, 179, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (623, 179, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (624, 179, 3, N'Proofread', 6, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (625, 179, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (626, 179, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (627, 179, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (628, 180, 1, N'Writing', 1, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (629, 180, 1, N'Scientific QA', 2, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (630, 180, 2, N'Editing', 3, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (631, 180, 2, N'QA Editing', 4, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (632, 180, 9, N'SM Review', 5, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (633, 180, 3, N'Proofread', 6, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (634, 180, 2, N'All Edits Done', 7, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (635, 180, 3, N'Client Review', 8, 0, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (636, 180, 7, N'Production', 9, 1, 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 11, CAST(N'2014-06-30 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (637, 181, 1, N'The Only Step', 1, 1, 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 10, CAST(N'2014-12-01 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (638, 182, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (639, 182, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (640, 183, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (641, 184, 8, N'Meeting', 1, 1, 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (642, 185, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (643, 185, 6, N'Revised', 2, 1, 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (644, 185, 7, N'Online Discussion', 3, 0, 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (645, 186, 5, N'Preliminary', 1, 1, 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)

INSERT [dbo].[WorkflowStep] ([WorkflowStepId], [WorkflowId], [StepTypeId], [StepName], [StepOrder], [ActiveDefault], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedFlag], [DeletedBy], [DeletedDate]) VALUES (646, 186, 7, N'Online Discussion', 2, 0, 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 10, CAST(N'2015-02-05 00:00:00.0000000' AS DateTime2), 0, NULL, NULL)
END

SET IDENTITY_INSERT [dbo].[WorkflowStep] OFF
