--After restore from P2RMISNet is complete
--Make sure DB is in bulk logged mode
--Delete data from problematic tables for reload
DELETE FROM ApplicationWorkflowStepElementContentHistory;
DELETE FROM ApplicationWorkflowStepElementContent;
DELETE FROM ApplicationWorkflowStepElement;
DELETE FROM ApplicationWorkflowStepWorkLog;
DELETE FROM ApplicationWorkflowStepAssignment;
DELETE FROM ApplicationWorkflowStep;
DELETE FROM ApplicationWorkflow;
DELETE FROM ApplicationTemplateElement;
DELETE FROM ApplicationTemplate;
DELETE FROM MechanismTemplateElementScoring;
DELETE FROM MechanismTemplateElement;
DELETE FROM MechanismTemplate;
DELETE FROM ApplicationStage;
DELETE FROM PanelStageStep;
DELETE FROM PanelStage;
--Run ETL in reverse order
/*
PanelStage*
PanelStageStep*
ApplicationStage*
MechanismTemplate*
MechanismTemplateElement*
MechanismTemplateElementScoring*
ApplicationTemplate*
ApplicationTemplateElement*
ApplicationWorkflow*
ApplicationWorkflowStep*
ApplicationWorkflowStepAssignment*
ApplicationWorkflowStepWorkLog
ApplicationWorkflowStepElement

*/
--These tables should be incremental scripts to insert/update
/*
 ApplicationWorkflowStepElementContent;
 ApplicationWorkflowStepElement;
 ApplicationWorkflowStepWorkLog;
 ApplicationWorkflowStepAssignment;
 ApplicationWorkflowStep;
 ApplicationWorkflow;
 ApplicationTemplateElement;
 ApplicationTemplate;
*/