UPDATE ClientAwardType SET ParentAwardTypeId = ParentAt.ClientAwardTypeId, MechanismRelationshipTypeId = 1
FROM ClientAwardType INNER JOIN
ViewClientAwardType ParentAt ON ClientAwardType.ClientId = ParentAt.ClientId
WHERE LTRIM(RTRIM(ClientAwardType.AwardDescription)) LIKE LTRIM(RTRIM(ParentAt.AwardDescription)) + '%PRE' AND LEN(LTRIM(RTRIM(ClientAwardType.AwardDescription))) <= (LEN(LTRIM(RTRIM(ParentAt.AwardDescription))) + 6)