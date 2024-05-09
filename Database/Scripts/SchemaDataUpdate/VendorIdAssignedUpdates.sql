UPDATE VendorIdAssigned
SET AssignedFlag = 1
WHERE DeletedFlag = 0 AND VendorId IN
(Select VendorId FROM UserVendor WHERE DeletedFlag = 0)