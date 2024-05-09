UPDATE UserApplicationComment
SET PanelApplicationId = PanelApplication.PanelApplicationId
FROM UserApplicationComment INNER JOIN
[Application] ON UserApplicationComment.ApplicationID = [Application].LogNumber INNER JOIN
PanelApplication ON [Application].ApplicationId = PanelApplication.ApplicationId
WHERE UserApplicationComment.PanelApplicationId = 0