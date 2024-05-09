--Insert data from legacy database
INSERT INTO UserPhone (UserInfoID, PhoneTypeId, LegacyPhoneId, Phone, PrimaryFlag, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
SELECT UserInfoID, PhoneTypeId, PN_ID, PN_Number, CASE PN_Type WHEN 'Business Phone' THEN 1 ELSE 0 END, vun.UserId, opn.LAST_UPDATE_DATE, vun.UserId, opn.LAST_UPDATE_DATE
FROM [$(P2RMIS)].dbo.PPL_Phone_Numbers opn INNER JOIN
[$(P2RMIS)].dbo.PPL_People ppl ON opn.Person_ID = ppl.Person_ID INNER JOIN
[User] u ON ppl.Person_ID = u.PersonID INNER JOIN
UserInfo ui ON u.UserID = ui.UserId INNER JOIN
PhoneType pt ON opn.PN_Type = pt.PhoneType LEFT OUTER JOIN
ViewLegacyUserNameToUserId vun ON opn.LAST_UPDATED_BY = vun.UserName 