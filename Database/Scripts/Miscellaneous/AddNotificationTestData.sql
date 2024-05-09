declare @startDate datetime
declare @endDate datetime
set @startDate = dbo.GetP2rmisDateTime()
set @endDate = DATEADD(dd, DATEDIFF(dd, 0, dbo.GetP2rmisDateTime()), 1)
INSERT INTO [Notification] ([Label], [Message], [StartDate], [EndDate], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate])
Values ('Maintenance', 'System will be unavailable on {0} to {1} for maintenance. We apologize for any inconvenience.',
@startDate, @endDate, 10, dbo.GetP2rmisDateTime(), 10, dbo.GetP2rmisDateTime())
