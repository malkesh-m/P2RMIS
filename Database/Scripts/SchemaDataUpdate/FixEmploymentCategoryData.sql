-- Fix Employment Category data
IF EXISTS (SELECT TOP 1 * FROM ProgramPayRate WHERE EmploymentCategoryId IS NULL)
BEGIN
	Update ProgramPayRate set EmploymentCategoryId=1, HonorariumAccepted='Paid' where HonorariumAccepted IN ('true', 'Paid') and EmploymentCategoryId is null
	Update ProgramPayRate set EmploymentCategoryId=2, HonorariumAccepted='Unpaid' where HonorariumAccepted IN ('false', 'Unpaid') and EmploymentCategoryId is null
	Update ProgramPayRate set EmploymentCategoryId=3, HonorariumAccepted='Unpaid w/t' where HonorariumAccepted IN ('false except travel', 'Unpaid w/t') and EmploymentCategoryId is null
END
IF EXISTS (SELECT TOP 1 * FROM SessionPayRate WHERE EmploymentCategoryId IS NULL)
BEGIN
	Update SessionPayRate set EmploymentCategoryId=1, HonorariumAccepted='Paid' where HonorariumAccepted IN ('true', 'Paid') and EmploymentCategoryId is null
	Update SessionPayRate set EmploymentCategoryId=2, HonorariumAccepted='Unpaid' where HonorariumAccepted IN ('false', 'Unpaid') and EmploymentCategoryId is null
	Update SessionPayRate set EmploymentCategoryId=3, HonorariumAccepted='Unpaid w/t' where HonorariumAccepted IN ('false except travel', 'Unpaid w/t') and EmploymentCategoryId is null
END
IF EXISTS (SELECT TOP 1 * FROM PanelUserRegistrationDocumentItem WHERE RegistrationDocumentItemId=8 and Value IN ('true', 'false', 'false except travel'))
BEGIN
	Update PanelUserRegistrationDocumentItem set Value='Paid' where RegistrationDocumentItemId=8 and Value='true'
	Update PanelUserRegistrationDocumentItem set Value='Unpaid' where RegistrationDocumentItemId=8 and Value='false'
	Update PanelUserRegistrationDocumentItem set Value='Unpaid w/t' where RegistrationDocumentItemId=8 and Value='false except travel'
END