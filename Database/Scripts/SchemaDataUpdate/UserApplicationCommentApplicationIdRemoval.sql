/*There is some bad data in UserApplicationComment where panel application Ids are missing,
we re-link what we can the rest are bogus log numbers (3 total) and need deleted so we can make PanelApplicationId not null and remove AppId. */
--Need to use dynamic sql for conditional include to avoid parse errors
EXEC sp_executesql N'
UPDATE UserApplicationComment
SET PanelApplicationId = PanelApplication.PanelApplicationId
FROM            UserApplicationComment INNER JOIN
                         Application ON UserApplicationComment.ApplicationID = Application.LogNumber INNER JOIN
                         PanelApplication ON Application.ApplicationId = PanelApplication.ApplicationId
WHERE        (UserApplicationComment.PanelApplicationId IS NULL);

DELETE FROM UserApplicationComment WHERE PanelApplicationId IS NULL;';