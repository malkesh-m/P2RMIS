DECLARE @puaId INT
DECLARE @userId INT
SET @userID = 1849

-- CDMRP
-- 109 SR, Reviewer
INSERT INTO PanelUserAssignment 
	(SessionPanelId, UserId, ClientParticipantTypeId, ModifiedDate, ParticipationMethodId) values (2712, @userID, 109, dbo.GetP2rmisDateTime(), 1)

SET @puaId = SCOPE_IDENTITY()
EXEC [dbo].[uspCreatePanelUserRegistration]
	@PanelUserAssignmentId = @puaId,
	@UserId = @userID

IF NOT EXISTS (SELECT TOP 1 * FROM ProgramPayRate
	WHERE ProgramYearId=226 AND ClientParticipantTypeId=109)
	BEGIN
		INSERT INTO ProgramPayRate
		(ProgramYearId, ClientParticipantTypeId, EmploymentCategoryId, HonorariumAccepted, ConsultantFeeText, ConsultantFee, 
		PeriodStartDate, PeriodEndDate, ManagerList, DescriptionOfWork, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, DeletedFlag) VALUES 
		(226,109,1,'Paid','Consultant Fee of $50.00, plus authorized expenses',50.0000,
		'2013-05-22T00:00:00','2014-05-21T00:00:00','Richard Labash, Ph.D., Scientific Review Manager; Joe Gao, Project Manager','As a PRP member, you will successfully complete the tasks set forth below within the designated',10,'2015-09-18T00:00:00',10,'2015-09-18T00:00:00',0)
		INSERT INTO ProgramPayRate
		(ProgramYearId, ClientParticipantTypeId, EmploymentCategoryId, HonorariumAccepted, ConsultantFeeText, ConsultantFee, 
		PeriodStartDate, PeriodEndDate, ManagerList, DescriptionOfWork, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, DeletedFlag) VALUES 
		(226,109,2,'Unpaid','No authorized expenses',0.0000,'2013-05-22T00:00:00','2014-05-21T00:00:00','Richard Labash, Ph.D., Scientific Review Manager; Joe Gao, Project Manager','As a PRP member, you will successfully complete the tasks set forth below within the designated',10,'2015-09-18T00:00:00',10,'2015-09-18T00:00:00',0)
	END

IF NOT EXISTS (SELECT TOP 1 * FROM SessionPayRate
	WHERE MeetingSessionId=783 AND ClientParticipantTypeId=109)
	BEGIN
		INSERT INTO SessionPayRate
		(MeetingSessionId, ClientParticipantTypeId, EmploymentCategoryId, HonorariumAccepted, ConsultantFeeText, ConsultantFee, 
		PeriodStartDate, PeriodEndDate, ManagerList, DescriptionOfWork, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, DeletedFlag) VALUES 
		(783,109,1,'Paid','Consultant Fee of $50.00, plus authorized expenses',750.0000,'2015-07-22T00:00:00','2015-09-21T00:00:00','Richard Labash, Ph.D., Scientific Review Manager; Joe Gao, Project Manager','As a PRP member, you will successfully complete the tasks set forth below within the designated',10,'2015-09-18T00:00:00',10,'2015-09-18T00:00:00',0)
		INSERT INTO SessionPayRate
		(MeetingSessionId, ClientParticipantTypeId, EmploymentCategoryId, HonorariumAccepted, ConsultantFeeText, ConsultantFee, 
		PeriodStartDate, PeriodEndDate, ManagerList, DescriptionOfWork, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, DeletedFlag) VALUES 
		(783,109,2,'Unpaid','None',0.0000,'2015-07-22T00:00:00','2015-09-21T00:00:00','Richard Labash, Ph.D., Scientific Review Manager; Joe Gao, Project Manager','As a PRP member, you will successfully complete the tasks set forth below within the designated',10,'2015-09-18T00:00:00',10,'2015-09-18T00:00:00',0)
	END

-- CPRIT
-- 120 SRA/SRO, Not Reviewer
INSERT INTO PanelUserAssignment 
    (SessionPanelId, UserId, ClientParticipantTypeId, ModifiedDate, ParticipationMethodId) values (1947, @userID, 120, dbo.GetP2rmisDateTime(), 1)

SET @puaId = SCOPE_IDENTITY()
EXEC [dbo].[uspCreatePanelUserRegistration]
	@PanelUserAssignmentId = @puaId,
	@UserId = @userID

IF NOT EXISTS (SELECT TOP 1 * FROM ProgramPayRate
	WHERE ProgramYearId=153 AND ClientParticipantTypeId=120)
	BEGIN
		INSERT INTO ProgramPayRate
		(ProgramYearId, ClientParticipantTypeId, EmploymentCategoryId, HonorariumAccepted, ConsultantFeeText, ConsultantFee, 
		PeriodStartDate, PeriodEndDate, ManagerList, DescriptionOfWork, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, DeletedFlag) VALUES 
		(153,120,1,'Paid','Consultant Fee of $50.00, plus authorized expenses',50.0000,
		'2013-05-22T00:00:00','2014-05-21T00:00:00','Richard Labash, Ph.D., Scientific Review Manager; Joe Gao, Project Manager','As a PRP member, you will successfully complete the tasks set forth below within the designated',10,'2015-09-18T00:00:00',10,'2015-09-18T00:00:00',0)
		INSERT INTO ProgramPayRate
		(ProgramYearId, ClientParticipantTypeId, EmploymentCategoryId, HonorariumAccepted, ConsultantFeeText, ConsultantFee, 
		PeriodStartDate, PeriodEndDate, ManagerList, DescriptionOfWork, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, DeletedFlag) VALUES 
		(153,120,2,'Unpaid','No authorized expenses',0.0000,'2013-05-22T00:00:00','2014-05-21T00:00:00','Richard Labash, Ph.D., Scientific Review Manager; Joe Gao, Project Manager','As a PRP member, you will successfully complete the tasks set forth below within the designated',10,'2015-09-18T00:00:00',10,'2015-09-18T00:00:00',0)
	END

IF NOT EXISTS (SELECT TOP 1 * FROM SessionPayRate
	WHERE MeetingSessionId=282 AND ClientParticipantTypeId=120)
	BEGIN
		INSERT INTO SessionPayRate
		(MeetingSessionId, ClientParticipantTypeId, EmploymentCategoryId, HonorariumAccepted, ConsultantFeeText, ConsultantFee, 
		PeriodStartDate, PeriodEndDate, ManagerList, DescriptionOfWork, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, DeletedFlag) VALUES 
		(282,120,1,'Paid','Consultant Fee of $50.00, plus authorized expenses',750.0000,'2015-07-22T00:00:00','2015-09-21T00:00:00','Richard Labash, Ph.D., Scientific Review Manager; Joe Gao, Project Manager','As a PRP member, you will successfully complete the tasks set forth below within the designated',10,'2015-09-18T00:00:00',10,'2015-09-18T00:00:00',0)
		INSERT INTO SessionPayRate
		(MeetingSessionId, ClientParticipantTypeId, EmploymentCategoryId, HonorariumAccepted, ConsultantFeeText, ConsultantFee, 
		PeriodStartDate, PeriodEndDate, ManagerList, DescriptionOfWork, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate, DeletedFlag) VALUES 
		(282,120,2,'Unpaid','None',0.0000,'2015-07-22T00:00:00','2015-09-21T00:00:00','Richard Labash, Ph.D., Scientific Review Manager; Joe Gao, Project Manager','As a PRP member, you will successfully complete the tasks set forth below within the designated',10,'2015-09-18T00:00:00',10,'2015-09-18T00:00:00',0)
	END