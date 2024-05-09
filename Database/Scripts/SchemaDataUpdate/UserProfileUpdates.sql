--Existing users were not mapped correctly during initial import (except Client)
--Rule is if role = 8, 10, 11 then profile = sra staff
UPDATE UserProfile
SET ProfileTypeId = 3
FROM UserProfile INNER JOIN
[UserInfo] ON UserProfile.UserInfoId = UserInfo.UserInfoId INNER JOIN
[UserSystemRole] ON UserInfo.UserId = UserSystemRole.UserId
Where UserSystemRole.SystemRoleId IN (8, 10, 11)