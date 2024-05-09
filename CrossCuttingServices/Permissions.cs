namespace Sra.P2rmis.CrossCuttingServices
{
    /// <summary>
    /// Permissions class defines the permission strings and methods 
    /// for manipulating or checking permissions.
    /// </summary>
    public class Permissions
    {
        /// <summary>
        /// Permissions associated with functions associated with report functionality.
        /// </summary>
        public class Reports
        {
            /// <summary>
            /// Permission definition for viewing reports
            /// </summary>
            public const string ViewProgramOrPanel = "View Program Reports,View Panel Reports";

            /// <summary>
            /// The view program level reports
            /// </summary>
            public const string ViewProgramLevel = "View Program Reports";

            /// <summary>
            /// The view panel level reports
            /// </summary>
            public const string ViewPanelLevel = "View Panel Reports";
        }
        /// <summary>
        /// Permissions associated with functions associated with user functionality.
        /// </summary>
        public class User
        {
            /// <summary>
            /// Permission definition for administrating users.
            /// </summary>
            public const string Administrate = "User Administration";
            /// <summary>
            /// Permission definition for creating users.
            /// </summary>
            public const string Create = "Create New User";
            /// <summary>
            /// Permission definition for editing existing users.
            /// </summary>
            public const string Edit = "Edit Existing User";
            /// <summary>
            /// Permission definition for setting users security information.
            /// </summary>
            public const string SetSecurity = "Set Security Information";
            /// <summary>
            /// Permission definition for setting users personal information.
            /// </summary>
            public const string SetPersonal = "Set Personal Information";
        }
        /// <summary>
        /// Permissions associated with functions associated with registration documents functionality.
        /// </summary>
        public class RegistrationDocument
        {
            /// <summary>
            /// View and modify registration documents for a panel.
            /// </summary>
            public const string ViewAndModify = "View and Modify Registration Documents";

            /// <summary>
            /// Allows a user to modify a panel reviewers contract/pma.
            /// </summary>
            public const string CustomizeContract = "Modify Contract Documents";
        }
        /// <summary>
        /// Permissions associated with functions associated with applications functionality.
        /// </summary>
        public class Application
        {
            /// <summary>
            /// Permission definition to view an application scoring summary.
            /// </summary>
            public const string ViewScoringSummary = "View Application Scoring Summary";
        }

        /// <summary>
        /// Permissions associated with functions associated with the search function.
        /// </summary>
        public class Search
        {
            /// <summary>
            /// Permission definition to view an panel scoring summary.
            /// </summary>
            public const string All = "Search Open Programs,Search Assigned Programs";
            /// <summary>
            /// Permission definition to view an panel summary.
            /// </summary>
            public const string Open = "Search Open Programs";
            /// <summary>
            /// Permission definition to view an panel assigned scoring summary.
            /// </summary>
            public const string Assigned = "Search Assigned Programs";
        }
        /// <summary>
        /// Permissions associated with functions associated with the note function.
        /// </summary> 
        public class Note
        {
            /// <summary>
            /// Permission definition to access a discussion note.
            /// </summary>
            public const string AccessDiscussion = "Access Discussion Note";
            /// <summary>
            /// Permission definition to access a general note.
            /// </summary>
            public const string AccessGeneral = "Access General Note";
            /// <summary>
            /// Permission definition to access a admin note.
            /// </summary>
            public const string AccessAdmin = "Access Admin Note";

        }
        /// <summary>
        /// Permissions associated with summary statement processes.
        /// </summary> 
        public class SummaryStatement
        {
            /// <summary>
            /// Permission definition to manage summary statements.
            /// </summary>
            public const string Manage = "Manage Summary Statement";
            /// <summary>
            /// Permission definition to process summary statements.
            /// </summary>
            public const string Process = "Process Summary Statement";

            /// <summary>
            /// Permission definition to review summary statements.
            /// </summary>
            public const string Review = "Review Summary Statement";

            /// <summary>
            /// Permission definition to manage or review summary statements.
            /// </summary>
            public const string ManageOrProcess = "Manage Summary Statement,Process Summary Statement";

            /// <summary>
            /// Permission definition to manage or review summary statements.
            /// </summary>
            public const string ManageOrReview = "Manage Summary Statement,Review Summary Statement";

            /// <summary>
            /// Permission definition to process or review summary statements.
            /// </summary>
            public const string ProcessOrReview = "Process Summary Statement,Review Summary Statement";

            /// <summary>
            /// Permission definition to manage or review summary statements.
            /// </summary>
            public const string ManageOrProcessOrReview = "Manage Summary Statement,Process Summary Statement,Review Summary Statement";

            /// <summary>
            /// Permission definition to accept or reject summary statement changes.
            /// </summary>
            public const string AcceptTrackChanges = "Accept Summary Statement Track Changes";

            /// <summary>
            /// Permission definition to process summary statement changes or access application scoring.
            /// </summary>
            public const string ProcessOrAccessApplicationScoring = "Process Summary Statement,View Online Scoring All Panels,Score Application";
            /// <summary>
            /// Permission definition to check in a summary statement into any phase.
            /// </summary>
            public const string CheckIntoAnyPhase = "Check into any phase";
            /// <summary>
            /// The editing only
            /// </summary>
            public const string EditingOnly = "Check out SS in editing phase";
            /// <summary>
            /// The writing only
            /// </summary>
            public const string WritingOnly = "Check out SS in writing phase";
            /// <summary>
            /// Permission definition to display only the assigned panels
            /// </summary>
            public const string DisplayAssignedPanels = "Display Assigned Panels";
            /// <summary>   Permission to access admin notes. </summary>
            public const string AccessAdminNote = "Access Admin Note";
            /// <summary>   Permission to access general notes. </summary>
            public const string AccessGeneralNote = "Access General Note";
            /// <summary>   Permission to access discussion notes. </summary>
            public const string AccessDiscussionNote = "Access Discussion Note";
            /// <summary>   Permission to access assigned reviewer notes. </summary>
            public const string AccessUnassignedReviewerNote = "Access Unassigned Reviewer Note";
        }
        /// <summary>
        /// Permissions associated with panel management application.
        /// </summary>
        public class PanelManagement
        {
            /// <summary>
            /// The manage panel permission name
            /// </summary>
            public const string Manage = "Manage Panel";
            /// <summary>
            /// The manage panel or documents permission name
            /// </summary>
            public const string ManagePanelOrDocuments = "Manage Panel,View and Modify Registration Documents";
            /// <summary>
            /// Allows user to modify panel reopen dates
            /// </summary>
            public const string ModifyReopenDates = "Modify Panel Reopen Dates"; // SRM            
            /// <summary>
            /// The process
            /// </summary>
            public const string Process = "Process Panel";
            /// <summary>
            /// Permission to manage staffs
            /// </summary>
            public const string ProcessStaffs = "Process Staffs";
            /// <summary>
            /// Permission to add/modify/delete panel applications
            /// </summary>
            public const string ManagePanelApplication = "Manage Panel Application";
            /// <summary>
            /// Permission to add/modify/date panel reviewer assignments
            /// </summary>
            public const string ManageApplicationReviewer = "Manage Application Reviewer";
            /// <summary>
            /// Send panel emails to reviewers through Panel Managemenet.
            /// </summary>
            public const string SendPanelCommunication = "Send Panel Communication";
            /// <summary>
            /// Add, modify or remove a reviewers expertise raitng or assingment to an application.
            /// </summary>
            public const string ManageReviewerAssignmentExpertise = "Manage Application Reviewer Assignment and Expertise";
            /// <summary>
            /// View reviewers expertise levels and assignments to applications, and modify reviewers expertise/assignments.
            /// </summary>
            public const string ViewReviewerAssignmentExpertise = "View/Edit Application Reviewer Assignment and Expertise";
            /// <summary>
            /// Submit, reset to edit, or edit critiques through panel management.
            /// </summary>
            public const string ManagePanelCritiques = "Manage Panel Critiques";
            /// <summary>
            /// View a panels critiques through panel management.
            /// </summary>
            public const string ViewPanelCritiques = "View Panel Critiques";
            /// <summary>
            /// Manage a panels discussion order through panel management.
            /// </summary>
            public const string ManageDiscussionOrder = "Manage Order of Discussion";
            /// <summary>
            /// Provide ratings and comments regarding a panel reviewers performance through panel management.
            /// </summary>
            public const string EvaluateReviewers = "Evaluate Reviewers";
            /// <summary>
            /// Provides read access to the panel management reviewers list
            /// </summary>
            public const string ViewPanelReviewers = "View Panel Reviewers";
            /// <summary>
            /// Allows user to access panel management features to panels which they are not assigned
            /// </summary>
            public const string ManageUnassignedPanel = "Manage Unassigned Panel";

            /// <summary>
            /// Allows users to request transferring an application to a new panel.
            /// </summary>
            public const string RequestApplicationTransfer = "Request Panel Application Transfer";
        }
        /// <summary>
        /// Permissions associated with reviewer information
        /// </summary>
        public class Reviewer
        {
            /// <summary>
            /// The view reviewers
            /// </summary>
            public const string ViewReviewers = "View Reviewers";
        }
        /// <summary>
        /// Permission associated with system setup
        /// </summary>
        public class Setup
        {
            /// <summary>
            /// The manage setup permission name
            /// </summary>
            public const string Manage = "Manage Setup";

            /// <summary>
            /// Allows user to import application data to the system.
            /// </summary>
            public const string ImportData = "Import Application Data";

            /// <summary>
            /// Allows user to generate deliverable data for export to external systems.
            /// </summary>
            public const string GenerateDeliverable = "Generate Deliverables";
            /// <summary>
            /// Access to the setup menu
            /// </summary>
            public const string Menu = "Manage Setup,Import Application Data,Generate Deliverables,Manage Document";
			/// <summary>
            /// The manage document
            /// </summary>
            public const string ManageDocument = "Manage Document";
            /// <summary>
            /// The menu
            /// </summary>
            public const string General = "Manage Setup,Import Application Data,Generate Deliverables,Manage Document,Manage Fee Schedule";
            /// <summary>
            /// Manage referral mapping
            /// </summary>
            public const string ManageReferralMapping = "Manage Referral Mapping";
            /// <summary>
            /// The manage fee schedule
            /// </summary>
            public const string ManageFeeSchedule = "Manage Fee Schedule";
            /// <summary>
            /// Manage Applications
            /// </summary>
            public const string ManageApplications = "Manage Applications";
            /// <summary>
            /// Manage Application Withdraw
            /// </summary>
            public const string ManageApplicationWithdraw = "Manage Application Withdraw";
            /// <summary>
            /// Manage personnel permission
            /// </summary>
            public const string ManagePersonnel = "Manage Personnel";
            /// <summary>
            /// Manage w9 addresses
            /// </summary>
            public const string ManageW9Addresses = "Manage W9 Addresses";
            /// <summary>
            /// Data transfer and generate, so that the Transfer Data page will load both tabs
            /// </summary>
            public const string DataTransferAndGenerate = "Import Application Data,Generate Deliverables";
        }

        /// <summary>
        /// Permissions associated with view templates
        /// </summary>
        public class Templates
        {
            /// <summary>
            /// The view template permission name
            /// </summary>
            public const string ViewTemplate = "View Template";
        }
        /// <summary>
        /// Permissions associated with user profile management.
        /// </summary>
        public class UserProfileManagement
        {
            /// <summary>
            /// The manage user accounts permission value
            /// </summary>
            public const string ManageUserAccounts = "Manage User Accounts";
            /// <summary>
            /// Manage user personal information dependent upon a requestors assignment
            /// </summary>
            public const string RestrictedManageUserAccounts = "Restricted Manage User Accounts";
        }
        /// <summary>
        /// Permissions specific to the MyWorkspace module.
        /// </summary>
        public class MyWorkspace
        {
            /// <summary>
            /// The score applications permission value
            /// </summary>
            public const string ScoreApplications = "Score Application";
            /// <summary>
            /// The access application scoring permission value
            /// </summary>
            public const string AccessMyWorkspace = "Access My Workspace";
            /// <summary>
            /// Provides access to the critique page for clients
            /// </summary>
            public const string ViewCritique = "View critique";
        }
        /// <summary>
        /// Permissions specific to the Manage Application Scoring module.
        /// </summary>
        public class ManageApplicationScoring
        {
            /// <summary>
            /// The view online scoring assigned panels permission value
            /// </summary>
            public const string ViewOnlineScoringAssignedPanels = "View Online Scoring Assigned Panels";
            /// <summary>
            /// The view online scoring all panels permission value
            /// </summary>
            public const string ViewOnlineScoringAllPanels = "View Online Scoring All Panels";
            /// <summary>
            /// The read only permission value
            /// </summary>
            public const string AccessManageApplicationScoring = "View Online Scoring Assigned Panels,View Online Scoring All Panels";

            /// <summary>
            /// The view scoreboard permission value
            /// </summary>
            public const string ViewScoreboard = "View Scoreboard";
            /// <summary>
            /// The edit score status permission value
            /// </summary>
            public const string EditScoreStatus = "Edit Score Status";
            /// <summary>
            /// The edit score card permission value
            /// </summary>
            public const string EditScoreCard = "Edit Score Card";
            /// <summary>
            /// The edit assignment type permission value
            /// </summary>
            public const string EditAssignmentType = "Edit Assignment Type";
            /// <summary>
            /// Provides access to the critique page for clients
            /// </summary>
            public const string ViewCritique = "View critique";
        }
        /// <summary>
        /// Permissions specific to the Manage Library module.
        /// </summary>
        public class Library
        {
            /// <summary>
            /// Standard access to the library section
            /// </summary>
            public const string AccessLibrary = "Access Library";
            /// <summary>
            /// Access the library section for any participant type
            /// </summary>
            public const string AccessFullLibrary = "Access Full Library";
            /// <summary>
            /// Access the library section for any program without requiring an associated panel assignment
            /// </summary>
            public const string AccessLibraryAnyProgram = "Access Library Any Program";
        }
        /// <summary>
        /// Permissions specific to Reviewer Recruitment module.
        /// </summary>
        public class ReviewerRecruitment
        {
            /// <summary>
            /// Manage the Reviewer Recruitment WorkList
            /// </summary>
            public const string ManageWorkList = "Manage Work List";
            /// <summary>
            /// Modify ParticipantType, ParticipantMethod, Participant Level after reviewer has been assigned to panel.
            /// Limited to RTA, Task Lead and PM roles
            /// </summary>
            public const string ModifyParticipantPostAssignment = "Modify participant information post panel assignment";
            /// <summary>
            /// Modify ParticipantType, ParticipantMethod, Participant Level after reviewer has been assigned to panel.
            /// but before contract is signed.  Limited to SRO roles
            /// </summary>
            public const string ModifyParticipantPostAssignmentLimited = "Modify limited participant information post panel assignment";
        }

        /// <summary>
        /// Permissions specific to the Task Tracking module
        /// </summary>
        public class TaskTracking
        {
            /// <summary>
            /// The submit task
            /// </summary>
            public const string SubmitTask = "Submit Task";
        }
        /// <summary>
        /// Permission for Meeting Management
        /// </summary>
        public class MeetingManagement
        {
            /// <summary>
            /// The meeting management access
            /// </summary>
            public const string MeetingManagementAccess = "Meeting Management";
            /// <summary>
            /// The permission to manage travel flights.
            /// </summary>
            public const string ManageTravelFlights = "Manage Travel Flights";
        }
        /// <summary>
        /// Permission for Consumer Management
        /// </summary>
        public class ConsumerManagement
        {
            /// <summary>
            /// The consumer management access
            /// </summary>
            public const string ConsumerManagementAccess = "Consumer Management";
            /// <summary>
            /// The permission to add/modify/search for a consumer
            /// </summary>
            public const string ManageConsumers = "Manage Consumers";
        }
        /// <summary>
        /// Permission for Security
        /// </summary>
        public class SecurityManagement
        {
            /// <summary>
            /// Allows a user to manage security policy
            /// </summary>
            public const string PolicyManagement = "Security Policy Management";
            /// <summary>
            /// Allows a user to manage user security policy
            /// </summary>
            public const string UserSecurityManagement = "User Security Management";
            /// <summary>
            /// Allows a user to view security information
            /// </summary>
            public const string ViewSecurityInformation = "View Security Information";
        }
    }
}
