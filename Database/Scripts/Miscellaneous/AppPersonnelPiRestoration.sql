--This script was ran on 1/31/2019 to fix personnel records that were incorrectly removed by 1.0 trigger. Prior to running we queried to make sure there were no duplicates to be restored.
UPDATE ApplicationPersonnel SET DeletedFlag = 0, DeletedBy = 999, DeletedDate = NULL
FROM ApplicationPersonnel
INNER JOIN ClientApplicationPersonnelType ON ApplicationPersonnel.ClientApplicationPersonnelTypeId = ClientApplicationPersonnelType.ClientApplicationPersonnelTypeId
INNER JOIN Application ON ApplicationPersonnel.ApplicationId = Application.ApplicationId
WHERE ApplicationPersonnel.DeletedFlag = 1 AND ApplicationPersonnel.DeletedDate > '1/30/2019' AND ClientApplicationPersonnelType.ApplicationPersonnelTypeAbbreviation IS NOT NULL;
