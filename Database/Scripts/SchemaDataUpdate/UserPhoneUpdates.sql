-- Remove references to deleted PhoneTypeIds
DELETE FROM UserPhone WHERE PhoneTypeId = 1 OR PhoneTypeId = 7