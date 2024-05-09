--DROP 2.0 triggers
IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N'Trigger_ApplicationBudget' AND [type] = 'TR')
BEGIN
      DROP TRIGGER [dbo].[Trigger_ApplicationBudget];
END;
IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N'Trigger_ApplicationReviewStatus_PrgPanelProposals' AND [type] = 'TR')
BEGIN
      DROP TRIGGER [dbo].[Trigger_ApplicationReviewStatus_PrgPanelProposals];
END;
IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N'Trigger_ApplicationStage_AssignmentVisibilityFlag' AND [type] = 'TR')
BEGIN
      DROP TRIGGER [dbo].[Trigger_ApplicationStage_AssignmentVisibilityFlag];
END;
IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N'Trigger_ApplicationWorkflow_PrgCritiques_Sync' AND [type] = 'TR')
BEGIN
      DROP TRIGGER [dbo].[Trigger_ApplicationWorkflow_PrgCritiques_Sync];
END;
IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N'Trigger_ApplicationWorkflowStep_PrgCritiquePhase' AND [type] = 'TR')
BEGIN
      DROP TRIGGER [dbo].[Trigger_ApplicationWorkflowStep_PrgCritiquePhase];
END;
IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N'Trigger_PanelApplication_PrgPanelProposals_Sync' AND [type] = 'TR')
BEGIN
      DROP TRIGGER [dbo].[Trigger_PanelApplication_PrgPanelProposals_Sync];
END;
IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N'Trigger_PanelApplicationReviewerAssignment_PrgProposalAssignmentSync' AND [type] = 'TR')
BEGIN
      DROP TRIGGER [dbo].[Trigger_PanelApplicationReviewerAssignment_PrgProposalAssignmentSync];
END;
IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N'Trigger_PanelApplicationRevieweCoiDetail_PrgReviewerPreferencesCoiSync' AND [type] = 'TR')
BEGIN
      DROP TRIGGER [dbo].[Trigger_PanelApplicationRevieweCoiDetail_PrgReviewerPreferencesCoiSync];
END;
IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N'Trigger_PanelApplicationReviewerExpertise_PrgReviewerPreferencesSync' AND [type] = 'TR')
BEGIN
      DROP TRIGGER [dbo].[Trigger_PanelApplicationReviewerExpertise_PrgReviewerPreferencesSync];
END;
IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N'Trigger_PanelStageStep_MtgPhaseMember_Sync' AND [type] = 'TR')
BEGIN
      DROP TRIGGER [dbo].[Trigger_PanelStageStep_MtgPhaseMember_Sync];
END;
IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N'Trigger_PanelUserAssignment_Legacy_Sync' AND [type] = 'TR')
BEGIN
      DROP TRIGGER [dbo].[Trigger_PanelUserAssignment_Legacy_Sync];
END;
IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N'Trigger_UserEmail_Sync_Trigger' AND [type] = 'TR')
BEGIN
      DROP TRIGGER [dbo].[Trigger_UserEmail_Sync_Trigger];
END;
IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N'Trigger_UserInfo_Sync_Trigger' AND [type] = 'TR')
BEGIN
      DROP TRIGGER [dbo].[Trigger_UserInfo_Sync_Trigger];
END;

--Sanitize emails
update useremail set email = 'punnithanfake@example.com';