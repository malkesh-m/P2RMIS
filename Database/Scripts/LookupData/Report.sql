SET IDENTITY_INSERT [Report] ON

MERGE INTO [Report] AS Target
USING (VALUES
	(1,'Roster','Roster','Roster style report providing information on panel members including name, degree, institution, department, and RCF expertise.',1,6,1,'PANEL')
	,(2,'Application Counts','ApplicationCounts','This report provides a sortable table summary of application counts based on program, FY and panels.',1,1,1,'PANEL')
	,(3,'Reviewer Information','Reviewer Information','Displays general information for panel reviewers including name, address, phone numbers and demographics, panel bias information, and financial and reviewer document information. Recommended format: Excel',0,3,1,'PANEL')
	,(4,'Final Scores','FinalScores','This report provides a spreadsheet for data on overall scores and evaluation criteria scores in final deliverable format. Blank scoring fields indicate that no final meeting scores are available and expedited applications will appear as "ND" for not discussed.',0,4,1,'PANEL')
	,(5,'Reviewer Demographics','Reviewer Demographics','Deliverable report that shows counts of reviewer demographics by panel. Individual reviewer details can optionally be included. Recommended format: Excel, PDF',1,6,1,'PANEL')
	,(6,'Scores - Phase Comparison','ScoreComparison','This report provides overall and criteria scoring data by phase for the assigned reviewers along with average criteria and overall scores with standard deviation.',1,4,1,'PANEL')
	,(9,'Conflict of Interest Sign Out Sheet','COISignOut','Conflict of Interest Sign Out Sheet, make sure the COI did not participate in the review of the  applications',0,2,1,'PANEL')
	,(10,'Expense Reimbursement','Reimbursement','Coonsulting and travel expenses reimbursement',1,2,1,'PANEL')
	,(13,'Administrative Notes','AdminNotes','This report provides a table of administrative notes included with applications within specified program/panel.',1,1,2,'CYCLE')
	,(14,'Panel Summary Proposal Count','PanelSummaryProposalCount','Count of Proposals per Panel',0,1,1,'PANEL')
	,(15,'Distribution of Scores by Panel','DistributionOfScoresPanel','This report provides a bar graph displaying distribution of overall scores for all panels within a cycle. This report can be run after the final meeting scoring is complete.',1,4,2,'CYCLE')
	,(16,'Distribution of Scores by Mechanism','DistributionOfScoresAward','This report provides a bar graph displaying distribution of overall scores for all mechanisms within a cycle. This report can be run after the final meeting scoring is complete.',1,4,2,'CYCLE')
	,(18,'Score - Individual Scores by Phase','IndividualScores','Report show the individual scores by Phase',0,4,2,'CYCLE')
	,(19,'Pedigree','Pedigree','Detail application information by application number',0,6,1,'PANEL')
	,(22,'Critique Past Due Notice','CritiquePastDueNotice','This report provides a reminder to reviewers of those applications which do not have submitted critiques.',1,2,1,'PANEL')
	,(23,'Order Of Discussion','OrderOfDiscussion','This report provides application and assigned reviewer/COI data listed in the order of discussion.',1,6,1,'PANEL')
	,(24,'Final Scores','FinalScoredel','This report provides a spreadsheet for data on overall scores and evaluation criteria scores in final deliverable format. Blank scoring fields indicate that no final meeting scores are available and expedited applications will appear as "ND" for not discussed.',1,4,1,'PANEL')
	,(28,'Critiques Submission Count','CritiquesSubmissionCounts','This report provides a spreadsheet listing of number of critiques assigned and submitted, along with totals and percentages.',1,3,1,'PANEL')
	,(30,'Reviewer vs Panel Comparison - Raw Data','ReviewerVsPanelComparison','This report provides a spreadsheet with overall scores of reviewer versus other panel members.',1,4,1,'PANEL')
	,(31,'Reviewer vs Panel Comparison - Graph','ReviewerVsPanelComparisonGraph','This report provides a scatter chart for overall scores of reviewer versus other panel members in deliverable format.',1,4,1,'PANEL')
	,(32,'Required Document Checklist','RequiredDocumentChecklist','This report provides a spreadsheet of reviewer information and registration information for panel tracking.',1,3,1,'PANEL')
	,(33,'Summary Statement Phase Tracking','SummaryPhaseTracking','This report provides the current number of applications by summary statement workflow phase.',0,5,2,'CYCLE')
	,(34,'SRO/Editor Productivity','EditorProductivity','This report provides a record of summary statement wrining and editorial check-out durations.',1,5,2,'CYCLE')
	,(35,'Disparate Overall Scores','Disparate Overall Scores','This report provides a view of applications for which the overall scores are disparate in excess of a specified value.',1,4,1,'PANEL')
	,(36,'Summary Statement QC','SummaryStatementQc','This report provides a spreadsheet of application data along with summary statement priority level.',0,5,2,'CYCLE')
	,(37,'Re-reviewed Applications','RereviewApplications','This report provides a spreadsheet listing with application specifics for those applications which have gone through a re-review to include both sets of scores and reviewers.',1,4,2,'CYCLE')
	,(38,'Pre-meeting Score Summary','ScoreSummary','This report provides a spreadsheet for data on pre-meeting overall and evaluation criteria in an editable format.',1,4,1,'PANEL')
	,(39,'	W9 and Vendor Information','W9Address','Provide a spreadsheet with W9 and Vendor information for verification and accounting.',1,6,1,'PANEL')
	,(40,'Thank You Letter Data','ThankYouLetterData','Provide a spreadsheet listing of user data pertaining to scientist and consumer reviewers for use in thank you letters by Help Desk.',1,6,1,'PANEL')
	,(41,'Panel / SRO / Chair List','PanelSROChairList','Provide a listing of panels, SROs and Chairs including application referral counts and peer review dates.',1,6,1,'PANEL')
	,(42,'COI Type','COIType','This report provides a listing of reviewer''s with a conflict of interest along with explanation.',0,6,1,'PANEL')
	,(45,'Conflict of Interest Sign-Out Sheet','CCOISignOutSheet','This report provides list of applications that reviewers have declared a conflict of interest with and allows them to certify that they did not participate in the evaluation of these applications.',1,2,1,'PANEL')
	,(50, 'Concatenated Critiques','Critiques','This report provides concatenated version of critiques',1,6,1,'PANEL')
	,(55, 'Application Panel Assignment QC Report','ApplicationPanelAssignmentQC','This report provides details on applications assigned to a panel including proposal status, reviewer names and types, and conflict of interest information',0,1,2,'CYCLE')
	,(60,'Panel Tents','PanelTents','This report is used to identify panel members to each other during the onsite meetings. The panel tents are printed on a sheet of paper and attached to laptops onsite to indicate who is sitting where in the room.',1,2,1,'PANEL')
	,(65,'Panel Badges','PanelBadges','This report is used to identify panel members to each other during the onsite meetings. They are printed on smaller cardstock for reviewers to wear at meeting to identify them as part of the peer review meeting.',1,2,3,'MEETING')
	,(66,'Reviewer List','ReviewerList','Deliverable report lists names and department/organization for all reviewers associated with ',1,6,1,'PANEL') 
	,(67,'Master Hotel/Travel Report','MeetingManagementTravelData','This report provides travel data for all reviewers associated with the selected meeting.',1,2,1,'PANEL')
	,(68,'Reviewer Potential Personnel COI Report','ReviewerPotentialPersonnelCOI','This report pulls a list application COIs with their first name and last name and compares the list with reviewers last name and first initial for a specific panel.',1,3,1,'PANEL')
	,(69,'Reviewer Potential Institutional COI Report','ReviewerPotentialInstitutionalCOI','This report is a two section grid where the left side of the grid displays applications by PI(applicant) by panel and the right side of the grid displays all reviewer''s names and institutions for that panel.',1,3,1,'PANEL')
	,(70,'Application Panel Assignment QC Report','ApplicationPanelAssignmentQCNew','This report provides details on applications assigned to a panel including proposal status, reviewer names and types, and conflict of interest information',1,1,2,'CYCLE')
	,(71,'Panel Participant Count','PanelParticipantCount','Provides a count of panel participation based on parameters selected.',1,6,1,'PANEL')
	,(72,'Meeting Planning Summary','MeetingPlanningSummary','Provides panel dates, application and panel participant counts, and Chairperson/SRO/RTA listings for meeting planning purposes.',1,2,3,'MEETING')
	,(73,'Multiple Submission','MultipleSubmission','This report identifies potential submissions that may have been submitted by the same applicant.',1,1,2,'CYCLE')
	,(74, 'Master Hotel and Travel Data','MasterHotelAndTravelData','This report provides travel and hotel data for all reviewers associated with the selected meeting.',1,2,3,'MEETING')
	,(75, 'Consumer Contact and Participation Information','ConsumerContactParticipation','This report displays contact and participation information for consumer reviewers for selected program(s) and or panel(s).',1,7,0,'DEFAULT')
	,(76, 'Consumer Ethnicity Count','ConsumerEthnicityCount', 'This report displays the ethnicity breakdown of the consumer reviewers.',1,7,0,'DEFAULT')
	,(77,'Nominator Information','NominatorInformation','This report lists the sponsor details for selected program and fiscal year(s) so that they can contacted by the project team when needed.',1,7,0,'DEFAULT')
	,(78,'SRO by Program','SRObyProgram','This report lists the Scientific Review Officers assigned to all (or selected panels) within a program.',1,9,1,'PANEL') 
	,(79,'Assignments - Individual Reviewers Report','ReviewerAssignments','This report lists the panel participants assignments.',1,9,1,'PANEL') 
 
	,(90,'Application Data QC','ApplicationDataQC','This report provides a spreadsheet of application data to help with the QC of that data and summary statements.',1,1,2,'CYCLE')
	,(91,'Reviewer Expertise - Pre/Post Application Assignment','ReviewerExpertisePrePostAssignmentWithSummary','This report provides reviewer assignments for each expertise level - "High", "Medium", "Low" and "None". It also provides counts on COIs and reviewers with no expertise selection.',1,3,1,'PANEL')
	,(92,'Scoring Setup QC','ScoringSetupQC','This report displays the scoring criteria information for all awards associated with the selected cycle.',1,8,2,'CYCLE')
	,(93,'COI Report','ChairCOI','This report provides a spreadsheet of information about reviewers and CPRIT Chairs including COI information.',1,3,1,'PANEL')
	,(94,'Score Alignment QC','ScoreAlignment','This report provides overall and criteria scoring data by phase for the assigned scientist reviewers.',1,4,1,'PANEL')
    ,(95,'Summary Statement Phase Tracking History','SummaryPhaseTrackingHistory','This report provides the current number of applications by summary statement workflow phase for a specified date.',1,5,2,'CYCLE')
	,(96,'Expedited Review Data','ExpeditedReviewData','This report provides the revised phase scoring data for expedited review analysis for CDMRP',1,3,1,'Panel')
  ) AS Source ([ReportId],[ReportName],[ReportFileName],[ReportDescription],[Active],[ReportGroupId],[ReportParameterGroupId],[ReportParameterGroupDesc])
ON (Target.[ReportId] = Source.[ReportId])
WHEN MATCHED AND (
	NULLIF(Source.[ReportName], Target.[ReportName]) IS NOT NULL OR NULLIF(Target.[ReportName], Source.[ReportName]) IS NOT NULL OR 
	NULLIF(Source.[ReportFileName], Target.[ReportFileName]) IS NOT NULL OR NULLIF(Target.[ReportFileName], Source.[ReportFileName]) IS NOT NULL OR 
	NULLIF(Source.[ReportDescription], Target.[ReportDescription]) IS NOT NULL OR NULLIF(Target.[ReportDescription], Source.[ReportDescription]) IS NOT NULL OR 
	NULLIF(Source.[Active], Target.[Active]) IS NOT NULL OR NULLIF(Target.[Active], Source.[Active]) IS NOT NULL OR 
	NULLIF(Source.[ReportGroupId], Target.[ReportGroupId]) IS NOT NULL OR NULLIF(Target.[ReportGroupId], Source.[ReportGroupId]) IS NOT NULL OR
	NULLIF(Source.[ReportParameterGroupId], Target.[ReportParameterGroupId]) IS NOT NULL OR NULLIF(Target.[ReportParameterGroupId], Source.[ReportParameterGroupId]) IS NOT NULL OR
	NULLIF(Source.[ReportParameterGroupDesc], Target.[ReportParameterGroupDesc]) IS NOT NULL OR NULLIF(Target.[ReportParameterGroupDesc], Source.[ReportParameterGroupDesc]) IS NOT NULL) THEN

 UPDATE SET
  [ReportName] = Source.[ReportName], 
  [ReportFileName] = Source.[ReportFileName], 
  [ReportDescription] = Source.[ReportDescription], 
  [Active] = Source.[Active], 
  [ReportGroupId] = Source.[ReportGroupId],
  [ReportParameterGroupId] = Source.[ReportParameterGroupId],
  [ReportParameterGroupDesc] = Source.[ReportParameterGroupDesc]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ReportId],[ReportName],[ReportFileName],[ReportDescription],[Active],[ReportGroupId],[ReportParameterGroupId],[ReportParameterGroupDesc])
 VALUES(Source.[ReportId],Source.[ReportName],Source.[ReportFileName],Source.[ReportDescription],Source.[Active],Source.[ReportGroupId],Source.[ReportParameterGroupId],Source.[ReportParameterGroupDesc])
;
GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [Report]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[Report] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [Report] OFF
GO