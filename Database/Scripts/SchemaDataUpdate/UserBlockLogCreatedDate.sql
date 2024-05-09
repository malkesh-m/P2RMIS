UPDATE [UserBlockLog]
SET CreatedDate = ModifiedDate
WHERE CreatedDate IS NULL;