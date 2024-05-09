namespace Sra.P2rmis.Web.Common
{
    /// <summary>
    /// Common values.
    /// </summary>
    public static class Invariables
    {
        /// <summary>
        /// Person's key such as Last Name, First Name, User ID, or Username
        /// </summary>
        public struct PersonKey
        {
            public const string LastName = "Last Name";
            public const string FirstName = "First Name";
            public const string UserId = "User ID";
            public const string Username = "Username";
            public const string Name = "Name";
        }
        /// <summary>
        /// P2rmis specific application configuration keys in Web.config.
        /// </summary>
        public static class AppConfigKey
        {
            public const string CacheHours = "CacheHours";
            public const string CacheMinutes = "CacheMinutes";
            public const string CacheSeconds = "CacheSeconds";
        }
        /// <summary>
        /// P2rmis Session keys.
        /// </summary>
        public static class SessionKey
        {


            /// <summary>
            /// The client identifier in session
            /// </summary>
            public const string ClientId = "ClientId";

            /// <summary>
            /// Number of times the user failed login.
            /// </summary>
            public const string FailedLoginCount = "FailedLoginCount";
            /// <summary>
            /// User login name.
            /// </summary>
            public const string UserLogin = "UserLogin";
            /// <summary>
            /// UserID of user for this session.
            /// </summary>
            public const string UserID = "UserID";
            /// <summary>
            /// Flag indicating user has been verified.
            /// </summary>
            public const string Verified = "Verified";
            /// <summary>
            /// Users user name.
            /// </summary>
            public const string Username = "username";
            /// <summary>
            /// User's email address.
            /// </summary>
            public const string Email = "email";
            /// <summary>
            /// Number of times user failed answer sign-on.
            /// </summary>
            public const string AnswerCount = "answerCount";
            /// <summary>
            /// Security question number selected by user.
            /// </summary>
            public const string QuestionNumber = "QuestionNumber";
            /// <summary>
            /// PanelSession session variable used in Application Scoring
            /// </summary>
            public const string PanelSession = "AsPanel";
            /// <summary>
            /// The program session used in Library
            /// </summary>
            public const string LibraryProgramSession = "LibraryProgram";
        }
        /// <summary>
        /// Common labels and column title names.  Any name that is used across views are defined  and
        /// referenced here.
        /// 
        /// Note the convention.  Entries in the Labels class should be terminated by "Name"
        /// </summary>
        public static class Labels
        {
            /// <summary>
            /// A summary statement priority
            /// </summary>
            public static string PriorityOneName = "Priority 1";
            /// <summary>
            /// A summary statement priority
            /// </summary>
            public static string PriorityTwoName = "Priority 2";
            /// <summary>
            /// A summary statement priority
            /// </summary>
            public static string NoPriorityName = "No Priority";
            /// <summary>
            /// Application identifier
            /// </summary>
            public static string ApplicationIdName = "Application ID";
            /// <summary>
            /// The application's average score
            /// </summary>
            public static string OverallScoreName = "Avg Overall Score";
            /// <summary>
            /// The application's average score
            /// </summary>
            public static string ScoreName = "Avg Overall Score";
            /// <summary>
            /// User entered comment or note indicator
            /// </summary>
            public static string NotesName = "Notes";
            /// <summary>
            /// AssignedReviewer
            /// </summary>
            public static string AssignedReviewerName = "User";
            /// <summary>
            /// Workflow Phase
            /// </summary>
            public static string WorkflowPhaseName = "Phases";
            /// <summary>
            /// Date the application's summary statement was released to start processing.
            /// </summary>
            public static string PostedDateName = "Posted Date";
            /// <summary>
            /// Date the application's summary statement was checked out.
            /// </summary>
            public static string CheckoutDateName = "Check-Out Date";
            /// <summary>
            /// Display header of the Program column.
            /// </summary>
            public static string ProgramName = "Program";
            /// <summary>
            /// Display header of the Panel column.
            /// </summary>
            public static string PanelName = "Panel";
            /// <summary>
            /// Display header of the the Available Date column.
            /// </summary>
            public static string AvailableDateName = "Available Date";
            /// <summary>
            /// Display header of the the Award column.
            /// </summary>
            public static string AwardName = "Mechanism";
            /// <summary>
            /// Display header of the the Action column.
            /// </summary>
            public static string ActionName = "Action";
            /// <summary>
            /// Display header of the the Application column.
            /// </summary>
            public static string ApplicationName = "Application";
            /// <summary>
            /// Display header of the the Start Date column.
            /// </summary>
            public static string StartDateName = "Start Date";
            /// <summary>
            /// Display header of the the User column.
            /// </summary>
            public static string UserName = "User";
            /// <summary>
            /// Display header of the the Research Topic Area column.
            /// </summary>
            public static string ResearchTopicAreaName = "Topic Area";
            /// <summary>
            /// Default filter view menu title
            /// </summary>
            public static string FilterMenuTitle = "Select Filters";
            /// <summary>
            /// Default header of Current User column
            /// </summary>
            public static string CurrentUserName = "Current User";
            /// <summary>
            /// The unassign
            /// </summary>
            public static string Unassign = "Unassign";
            /// <summary>
            /// Default header of Assign To column
            /// </summary>
            public static string AssignToName = "Assign To";
            /// <summary>
            /// Default header of Phase column
            /// </summary>
            public static string PhaseName = "Current Phase";
            /// <summary>
            /// Display header of the primary investigator's name
            /// </summary>
            public static string PrimaryInvestigatorName = "PI";
            /// <summary>
            /// Display header of the application title
            /// </summary>
            public static string ApplicationTitle = "Application Title";
            /// <summary>
            /// Display header of the primary investigator's organization
            /// </summary>
            public static string PrimaryInvestigatorOrganization = "PI Organization";
            /// <summary>
            /// Display header of the conflict of interest
            /// </summary>
            public static string ConflictOfInterest = "COI";
            /// <summary>
            /// Display header of the refer request
            /// </summary>
            public static string ReferRequest = "Refer Request";
            /// <summary>
            /// Display header of the overview
            /// </summary>
            public static string Overview = "Overview";
            /// <summary>
            /// Display header of the name
            /// </summary>
            public static string Name = "Name";
            /// <summary>
            /// Display header of the organization
            /// </summary>
            public static string Organization = "Organization";
            /// <summary>
            /// Display header of the conflict of interest type
            /// </summary>
            public static string CoiType = "COI Type";
            /// <summary>
            /// Display header of the Partner PI
            /// </summary>
            public static string PartnerPI = "Partner PI";
            /// <summary>
            /// Display header of the Principal Investigator
            /// </summary>
            public static string PrincipalInvestigatorName = "Principal Investigator";
            /// <summary>
            /// Display header of the Application Documents
            /// </summary>
            public static string ApplicationDocumentsName = "Documents";
            /// <summary>
            /// Display header of the Coi Source
            /// </summary>
            public static string CoiSourceName = "COI Source";
            /// <summary>
            /// Display header row number
            /// </summary>
            public static string RowNum= "Row";
            /// <summary>
            /// Display header of Fiscal Yr
            /// </summary>
            public static string FiscalYear = "Fiscal Yr";
            /// <summary>
            /// Display header of Client
            /// </summary>
            public static string Client = "Client";
            /// <summary>
            /// Display header of Participant Type
            /// </summary>
            public static string ParticipantType = "Participant Type";
            /// <summary>
            /// Display header of Participant Role
            /// </summary>
            public static string Role = "Role";
            /// <summary>
            /// Display header of Panel End Date
            /// </summary>
            public static string PanelEndDate = "Panel End";
            /// <summary>
            /// Display header of Registration
            /// </summary>
            public static string Registration = "Registration";
            /// <summary>
            /// Display label of a registration to be started
            /// </summary>
            public static string StartRegistration = "Start";
            /// <summary>
            /// Display label of a continued registration
            /// </summary>
            public static string ContinueRegistration = "Continue";
            /// <summary>
            /// Display label of a completed registration
            /// </summary>
            public static string CompleteRegistration = "Completed";
            /// <summary>
            /// The non applicable label
            /// </summary>
            public static string NonApplicable = "N/A";
            /// <summary>
            /// The yes label
            /// </summary>
            public static string Yes = "Yes";
            /// <summary>
            /// The no label
            /// </summary>
            public static string No = "No";
            /// <summary>
            /// The full
            /// </summary>
            public static string Full = "Full";
            /// <summary>
            /// The complete label
            /// </summary>
            public static string Complete = "Complete";
            /// <summary>
            /// The potential label
            /// </summary>
            public static string Potential = "Potential";
            /// <summary>
            /// Registration state of incomplete
            /// </summary>
            public static string Incomplete = "Incomplete";
            /// <summary>
            /// The online discussion phase
            /// </summary>
            public static string OnlineDiscussionPhase = "Online Discussion";
            /// <summary>
            /// The phase title tag
            /// </summary>
            public static string PhaseTitleTag = "Critique/Score Submission";
            /// <summary>
            /// The add label
            /// </summary>
            public static string Add = "Add";
            /// <summary>
            /// The edit label
            /// </summary>
            public static string Edit = "Edit";
            /// <summary>
            /// The view label
            /// </summary>
            public static string View = "View";
            /// <summary>
            /// The start label
            /// </summary>
            public static string Start = "Start";
            /// <summary>
            /// The non applicable without slash
            /// </summary>
            public static string NA = "NA";

            /// <summary>
            /// App ID display header
            /// </summary>
            public static string AppId = "App ID";

            /// <summary>
            /// Cycle display header
            /// </summary>
            public static string Cycle = "Cycle";

            /// <summary>
            /// Award display header
            /// </summary>
            public static string Award = "Award";

            /// <summary>
            /// Score display header
            /// </summary>
            public static string Score = "Score";

            /// <summary>
            /// Topic Area display header
            /// </summary>
            public static string TopicArea = "Topic Area";
            /// <summary>
            /// Labels for the Panel Management application
            /// </summary>
            public static class PanelManagement
            {
                public static class Messages
                {
                    public static string NoResultsFound = "No matching records were found.  Please try again.";
                    public static string SelectPanel = "Please select a Panel to start.";
                }
                /// <summary>
                /// Critique tab's labels
                /// </summary>
                public static class Critiques
                {
                    /// <summary>
                    /// Label for number of revision column
                    /// </summary>
                    public static string NumberOfReviewersName = "# of Reviewers";
                    /// <summary>
                    /// Label for percentage submitted per phase columns
                    /// </summary>
                    public static string PercentageSubmittedName = "% Submitted for ";
                    /// <summary>
                    /// Label for critique phase
                    /// </summary>
                    public static string PhaseName = "Phase: ";
                    /// <summary>
                    /// Label for critique start time
                    /// </summary>
                    public static string StartDateTimeName = "Start Date/Time: ";
                    /// <summary>
                    /// Label for critique end time
                    /// </summary>
                    public static string EndDateTimeName = "End Date/Time: ";
                    /// <summary>
                    /// Label for critique Re-open time
                    /// </summary>
                    public static string ReopenDateTimeName = "Re-Open Date/Time: ";
                    /// <summary>
                    /// Label for critique close time
                    /// </summary>
                    public static string CloseDateTimeName = "Close Date/Time: ";
                    /// <summary>
                    /// Link label for Viewing a critique
                    /// </summary>
                    public static string View = "View";
                    /// <summary>
                    /// Link label for Resetting a critique
                    /// </summary>
                    public static string ResetToEdit = "Reset To Edit";
                    /// <summary>
                    /// Link label for Submitting a critique
                    /// </summary>
                    public static string Submit = "Submit";
                    /// <summary>
                    /// Label for "Not Submitted" status
                    /// </summary>
                    public static string NotSubmitted = "Not Submitted";
                    /// <summary>
                    /// Label for "Submitted" status
                    /// </summary>
                    public static string Submitted = "Submitted";
                    /// <summary>
                    /// Label for "Not Started" status
                    /// </summary>
                    public static string NotStarted = "Not Started";
                    /// <summary>
                    /// Text for NA
                    /// </summary>
                    public static string NonApplicable = "NA";
                    /// <summary>
                    /// Label for a criteria score
                    /// </summary>
                    public static string CriteriaScoreLabel = "Score: ";
                    /// <summary>
                    /// Display header of Date
                    /// </summary>
                    public static string Date = "Date";
                    /// <summary>
                    /// Display header of From
                    /// </summary>
                    public static string From = "From";
                    /// <summary>
                    /// Display header of Subject
                    /// </summary>
                    public static string Subject = "Subject";
                    /// <summary>
                    /// Link label for Editing a critique
                    /// </summary>
                    public static string Edit = "Edit";
                }
                /// <summary>
                /// Communication tab's labels
                /// </summary>
                public static class Communication
                {
                    /// <summary>
                    /// Display header of Date
                    /// </summary>
                    public static string Date = "Date";
                    /// <summary>
                    /// Display header of From
                    /// </summary>
                    public static string From = "From";
                    /// <summary>
                    /// Display header of Subject
                    /// </summary>
                    public static string Subject = "Subject";
                    /// <summary>
                    /// The system
                    /// </summary>
                    public static string System = "System";
                }
            }
            /// <summary>
            /// Labels for the User Profile Management
            /// </summary>
            public static class UserProfileManagement
            {
                public static string NoParticipation = "No program assignments were found.";

                /// <summary>
                /// First Name
                /// </summary>
                public const string FirstName = "First Name";
                /// <summary>
                /// Last Name
                /// </summary>
                public const string LastName = "Last Name";
                /// <summary>
                /// Email
                /// </summary>
                public const string Email = "Email";
                /// <summary>
                /// UserName
                /// </summary>
                public const string UserName = "Username";
                /// <summary>
                /// UserId
                /// </summary>
                public const string UserId = "User ID";
            }
        }

        /// <summary>
        /// Common parameters for URLs.
        /// </summary>
        public static class UrlParameters
        {
            public const string TemplateName = "TemplateName";

            /// <summary>
            /// Indicate the report identifier
            /// </summary>
            public const string ReportId = "reportId";
            /// <summary>
            /// Indicate the application workflow identifier
            /// </summary>
            public const string ApplicationWorkflowId = "ApplicationWorkflowId";
            /// <summary>
            /// Indicate the application log number
            /// </summary>
            public const string ApplicationLogNumber = "ApplicationLogNumber";
            /// <summary>
            /// Indicate the program abbreviation
            /// </summary>
            public const string ProgramAbrv = "ProgramAbrv";
            /// <summary>
            /// Indicate the cycle
            /// </summary>
            public const string Cycle = "Cycle";
            /// <summary>
            /// Indicate the fiscal year
            /// </summary>
            public const string FiscalYear = "FiscalYear";
            /// <summary>
            /// Indicate the format of files
            /// </summary>
            public const string Format = "Format";
        }

        public static class ReviewerExpertise
        {
            public const string NoSelectionExpertiseViewId = "expertiseNoSelection";
            public const string HighExpertise = "High";
            public const string MediumExpertise = "Med";
            public const string LowExpertise = "Low";
            public const string NoExpertise = "None";
            public const string CoiExpetise = "COI";
            public const string NoSelectionExpertise = "No Selection";
            public const string HighExpertiseViewId = "expertiseHigh";
            public const string MediumExpertiseViewId = "expertiseMed";
            public const string LowExpertiseViewId = "expertiseLow";
            public const string NoExpertiseViewId = "expertiseNone";
            public const string CoiExpertiseViewId = "expertiseCOI";

            public const string ScientistViewId = "legendScientist";
            public const string SpecialistViewId = "legendSpecial";
            public const string ConsumerViewId = "legendConsumer";
        }
        public static class Reviewer
        {
            public const string Yes = "Y";
            public const string No = "N";
        }
        /// <summary>
        /// Keys for TempData
        /// </summary>
        public static class TempDataKeys
        {
            /// <summary>
            /// Login verification messages
            /// </summary>
            public const string LoginVerification = "LoginVerification";
        }

        /// <summary>
        /// MyWorkspace constants
        /// </summary>
        public static class MyWorkspace
        {
            public const string Assigned = "Assigned";
            public const string Unassigned = "Unassigned";
            public const string Blinded = "Blinded";
            public const string Coi = "COI";
            public const string NonApplicable = "N/A";
            public const string Expired = "Expired";
            public const string Start = "Start";
            public const string StartPrelim = "Start (Prelim)";
            public const string Edit = "Edit";
            public const string EditPrelim = "Edit (Prelim)";
            public const string View = "View";
            public const string Integer = "Integer";
            public const string Adjectival = "Adjectival";
            //TO BE REFACTORED
            public const string Preliminary = "Preliminary";
            public const string Revised = "Revised";

            public const int MaxTitleLengthBeforeCropping = 30;
            public const int MaxLongTitleLengthBeforeCropping = 75;
            public const int MaxPiNameLengthBeforeCropping = 30;
        }
        /// <summary>
        /// Rating constants
        /// </summary>
        public static class Rating
        {
            public const string NonApplicable = "N/A";
            public const int MinimumRating = 1;
            public const int MaximumRating = 5;
        }

        /// <summary>
        /// Lookup reference for MimeType values for files returned form controller method
        /// </summary>
        public static class MimeTypes
        {
            public const string Zip = "application/zip";
        }       
    }
    /// <summary>
    /// Embedded Viewer Constants
    /// </summary>
    public static class PdfViewer
    {
        public const string WindowName = "p2rmisPdfViewerWindow";
        public const string WarningModalTitle = "Data Security Terms & Conditions";
    }
}