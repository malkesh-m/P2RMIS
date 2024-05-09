UPDATE PPL_People SET [Status] = 'Inactive'
WHERE Person_ID NOT IN
--People who have generated deliverables
(SELECT Person_ID
FROM SYS_Users
INNER JOIN PRG_Deliverables ON SYS_Users.UserID = PRG_Deliverables.GeneratedBy OR SYS_Users.UserID = PRG_Deliverables.QCdBy
WHERE QCdOn > '1/1/2018' OR GeneratedOn > '1/1/2018') AND Person_ID NOT IN
--IT Staff
(5014
,15480
,16400
,36000
,39557
,42978
,47664);