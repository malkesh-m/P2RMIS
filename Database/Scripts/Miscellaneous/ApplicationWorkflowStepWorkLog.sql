-- Fix empty CheckInUserId values (column recently added)
UPDATE [ApplicationWorkflowStepWorkLog] SET CheckInUserId=UserId WHERE CheckInDate IS NOT NULL AND (CheckInUserId=0 OR CheckInUserId IS NULL)
UPDATE [ApplicationWorkflowStepWorkLog] SET CheckInUserId=NULL WHERE CheckInDate IS NULL