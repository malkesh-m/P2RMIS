-- Update legacy comments ModifiedBy/Date data

UPDATE   ApplicationBudget
SET      CommentModifiedBy = ModifiedBy, CommentModifiedDate = ModifiedDate
WHERE    (Comments IS NOT NULL) AND (Comments <> '') AND (DeletedFlag = 0) AND (CommentModifiedBy IS NULL) AND (CommentModifiedDate IS NULL)