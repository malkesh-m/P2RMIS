CREATE PROCEDURE [dbo].[uspRemoveApplicationWorkflow]
	@ApplicationWorkflowId int,
	@UserId int
AS
BEGIN
DECLARE @CurrentDateTime DATETIME2(0) = dbo.GetP2rmisDateTime();
	--Removes all data related to application workflow and children in reverse order
	UPDATE awsec
	SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
	FROM ApplicationWorkflowStepElementContent awsec
	INNER JOIN ViewApplicationWorkflowStepElement awse ON awsec.ApplicationWorkflowStepElementId = awse.ApplicationWorkflowStepElementId
	INNER JOIN ViewApplicationWorkflowStep aws ON awse.ApplicationWorkflowStepId = aws.ApplicationWorkflowStepId
	WHERE aws.ApplicationWorkflowId = @ApplicationWorkflowId AND awsec.DeletedFlag = 0;
	UPDATE awse
	SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
	FROM ApplicationWorkflowStepElement awse
	INNER JOIN ViewApplicationWorkflowStep aws ON awse.ApplicationWorkflowStepId = aws.ApplicationWorkflowStepId
	WHERE aws.ApplicationWorkflowId = @ApplicationWorkflowId AND awse.DeletedFlag = 0;
	UPDATE awsa
	SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
	FROM ApplicationWorkflowStepAssignment awsa
	INNER JOIN ViewApplicationWorkflowStep aws ON awsa.ApplicationWorkflowStepId = aws.ApplicationWorkflowStepId
	WHERE aws.ApplicationWorkflowId = @ApplicationWorkflowId AND awsa.DeletedFlag = 0;
	UPDATE awswl
	SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
	FROM ApplicationWorkflowStepWorkLog awswl
	INNER JOIN ViewApplicationWorkflowStep aws ON awswl.ApplicationWorkflowStepId = aws.ApplicationWorkflowStepId
	WHERE aws.ApplicationWorkflowId = @ApplicationWorkflowId AND awswl.DeletedFlag = 0;
	UPDATE aws
	SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
	FROM ApplicationWorkflowStep aws
	WHERE aws.ApplicationWorkflowId = @ApplicationWorkflowId AND aws.DeletedFlag = 0;
	UPDATE ApplicationWorkflowSummaryStatement
	SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
	WHERE ApplicationWorkflowId = @ApplicationWorkflowId AND DeletedFlag = 0;
	UPDATE ApplicationWorkflow
	SET DeletedFlag = 1, DeletedBy = @UserId, DeletedDate = @CurrentDateTime
	WHERE ApplicationWorkflow.ApplicationWorkflowId = @ApplicationWorkflowId AND ApplicationWorkflow.DeletedFlag = 0;
END
GO
	GRANT EXECUTE
    ON OBJECT::[dbo].[uspRemoveApplicationWorkflow] TO [NetSqlAzMan_Users]
    AS [dbo];