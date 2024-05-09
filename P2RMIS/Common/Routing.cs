
using Microsoft.Practices.Unity;

namespace Sra.P2rmis.Web.Common
{
    public class Routing
    {
        /// <summary>
        /// P2Rmis controller names
        /// </summary>
        public class P2rmisControllers
        {
            public const string TaskTracking = "TaskTracking";
            public const string Worklist = "Worklist";
            public const string SessionDetails = "SessionDetails";
            public const string User = "User";
            public const string SummaryStatement = "SummaryStatement";
            public const string UserProfile = "UserProfileManagement";
            public const string ProgramRegistration = "ProgramRegistration";
            public const string PanelManagement = "PanelManagement";
            public const string MyWorkspace = "MyWorkspace";
            public const string ManageApplicationScoring = "ManageApplicationScoring";
            public const string Home = "Home";
            public const string Library = "Library";
            public const string ProgramRegistrationStatus = "ProgramRegistrationStatus";
            public const string SummaryStatementProcessing = "SummaryStatementProcessing";
            public const string Account = "Account";
            public const string HotelAndTravel = "HotelAndTravel";
            public const string UserProfileManagement = "UserProfileManagement";
        }
        /// <summary>
        /// SessionDetailsController ActionMethod names.
        /// </summary>
        public class SessionDetailsActionMethods
        {
            public const string PanelDetails = "PanelDetails";
            public const string Details = "Details";
            public const string SessionDetails = "SessionDetails";
            public const string MeetingOccurring = "MeetingOccurring";
        }
        /// <summary>
        /// User Controller ActionMethod names
        /// </summary>
        public class UserActionMethods
        {
            public const string Create = "Create";
            public const string Edit = "Edit";
            public const string MyAccount = "MyAccount";
            public const string MyAccountSave = "MyAccountSave";
            public const string MyAccountVerify = "MyAccountVerify";
            public const string ANSManagement = "ANSManagement";
            public const string Delete = "Delete";
            public const string DeleteConfirmed = "DeleteConfirmed";
            public const string Index = "Index";
            public const string GridUserData = "GridUserData";
            public const string Invite = "Invite";
            public const string Lock = "Lock";
            public const string Deactivate = "Deactivate";
            public const string DeactivateConfirm = "DeactivateConfirm";
            public const string Activate = "Activate";
            public const string ActivateConfirm = "ActivateConfirm";
            public const string ResetPending = "ResetPending";
            public const string PasswordExpiredExpire = "PasswordExpiredExpire";
        }
        /// <summary>
        /// UserProfileController ActionMethod names
        /// </summary>
        public class UserProfileManagement
        {
            /// <summary>
            /// Define the actions
            /// </summary>
            public const string CreateProfile = "CreateProfile";
            public const string ViewUser = "ViewUser";
            public const string ViewMyProfile = "ViewUser";
            public const string ViewNewUser = "ViewNewUser";
            public const string ViewCreateNewUser = "ViewCreateNewUser";
            public const string PasswordManagement = "PasswordManagement";
            public const string SendCredentials = "SendCredentials";
            public const string Unlock = "Unlock";
            public const string ReActivate = "ReActivate";
            public const string DeActivate = "DeActivate";
            public const string GetSystemRolesForProfileType = "GetSystemRolesForProfileType";
            public const string ViewParticipationHistory = "ViewParticipationHistory";
            public const string ViewUserToVerify = "ViewUserToVerify";
            public const string ViewResumeByUserInfoId = "ViewResumeByUserInfoId";
            public const string SaveProfile = "SaveProfile";
            public const string SaveMyProfile = "SaveMyProfile";
            /// <summary>
            /// Define the view
            /// </summary>
            public class Views
            {
                public const string Profile = "Profile";
                public const string PasswordManagementView = "PasswordManagement";
            }
            /// <summary>
            /// Parameter names for controller unlock method.
            /// </summary>
            public class UnlockParameters
            {
                public const string TargetId = "targetUserId";
                public const string ccountStatusReasonId = "accountStatusReasonId";
            }
            /// <summary>
            /// Parameter names for controller unlock method.
            /// </summary>
            public class DeactivateParameters
            {
                public const string TargetId = "targetUserId";
                public const string AccountStatusReasonId = "accountStatusReasonId";
            }
            /// <summary>
            /// Parameter names for controller GetSystemRolesForProfileType Ajax method.
            /// </summary>
            public class GetSystemRolesForProfileTypeParameters
            {
                public const string ProfileTypeId = "profileTypeId";
            }
            /// <summary>
            /// Parameter names for controller view resume method.
            /// </summary>
            public class ViewResumeParameters
            {
                public const string UserInfoId = "UserInfoId";
            }
            /// <summary>
            /// Parameter names for controller method ViewUser.
            /// </summary>
            public class ViewUserParameters
            {
                public const string UserInfoId = "userInfoId";
            }
        }
        /// <summary>
        /// SummaryStatementController ActionMethod names
        /// </summary>
        public class SummaryStatement
        {
            /// <summary>
            /// Define the actions
            /// </summary>
            public const string ViewApplicationModal = "ViewApplicationModal";
        }
        public class SummaryStatementProcessing
        {
            /// <summary>
            /// Define the actions
            /// </summary>
            public const string ListWorkflowSteps = "ListWorkflowSteps";
            public const string CheckInToStep = "CheckInToStep";
            public const string UploadFile = "UploadFile";
        }
        /// <summary>
        /// PanelManagementController ActionMethod names
        /// </summary>
        public class PanelManagement
        {
            public const string SearchReviewers = "SearchForReviewers";
            /// <summary>
            /// Critiques tab
            /// </summary>
            public const string ManageCritiques = "ManageCritiques";
            /// <summary>
            /// The communication log
            /// </summary>
            public const string CommunicationLogModal = "CommLog";
            /// <summary>
            /// The panel assignment modal
            /// </summary>
            public const string PanelAssignmentModal = "PanelAssignment";
            /// <summary>
            /// The rating evaluation modal
            /// </summary>
            public const string RatingEvaluationModal = "RatingEvaluation";
            /// <summary>
            /// Define the actions
            /// </summary>
            public const string PIInformation = "PIInformation";
            /// <summary>
            /// The save communication logs
            /// </summary>
            public const string SaveCommunicationLogs = "SaveCommunicationLogs";
            /// <summary>
            /// Assignment type
            /// </summary>
            public const string SearchStaff = "SearchForStaff";

            public class AssignmentType
            {
                public const string PanelUserAssignment = "PanelUserAssignment";
            }
            /// <summary>
            /// Assignment status
            /// </summary>
            public class AssignmentStatus
            {
                public const string Assigned = "Assigned";
                public const string Potential = "Potential";
            }
            /// <summary>
            /// Returns the program for a given client
            /// </summary>
            public const string GetProgramPanelsForSpecificClient = "GetProgramPanelsForSpecificClient";
            /// <summary>
            /// Returns the program for a given client
            /// </summary>
            public const string GetProgramPanelsForProgramYear = "GetProgramPanelsForProgramYear";
        }
        /// <summary>
        /// MyWorkspaceController ActionMethod names
        /// </summary>
        public class MyWorkspace
        {
            /// <summary>
            /// Define the actions
            /// </summary>
            public const string Index = "Index";
            public const string Critique = "Critique";
            public const string SetSessionPanel = "SetSessionPanel";
            public const string SaveSuccessful = "Saved Successfully";
            public const string SaveUnsuccessful = "Error while Saving";
            public const string SaveScore = "SaveScore";
            public const string SaveReviewersCritique = "SaveReviewersCritique";
            public const string SaveReviewersCritiqueAndScore = "SaveReviewersCritiqueAndScore";
            public const string IsCritiquesAlreadySubmitted = "IsCritiquesAlreadySubmitted";
            public const string PollScore = "PollScore";
            public const string Scorecard = "Scorecard";
            public const string Scoreboard = "Scoreboard";
            public const string ProceedToScoring = "ProceedToScoring";
            public const string EndScoring = "EndScoring";
            public const string PollActiveApplicationId = "PollActiveApplicationId";
            public const string GetScorableApplications = "GetScorableApplications";
            public const string GetActiveOrScoringApplication = "GetActiveOrScoringApplication";
            public const string SubmitCritique = "SubmitCritique";
            public const string CanSubmitCritique = "CanSubmitCritique";
            public const string SetAbstains = "SetAbstains";
            public const string GetIncompleteOverallWarningModal = "GetIncompleteOverallWarningModal";
            public const string GetIncompleteCritiqueWarningModal = "GetIncompleteCritiqueWarningModal";
            public const string GetNotificationOfSubmitModal = "GetNotificationOfSubmitModal";
            public const string GetConfirmationOfSuccessModal = "GetConfirmationOfSuccessModal";
            public const string ViewUsersCritiqueAction = "ViewUsersCritique";
            public const string CritiquePanel = "CritiquePanel";
            public const string ProcessIncompleteCritiqueFromApplicationScoring = "ProcessIncompleteCritiqueFromApplicationScoring";
            public const string ManageApplicationScoring = "ManageApplicationScoring";
            public const string GetNoScoresModal = "GetNoScoresModal";
            public const string AlreadySubmittedCritique = "AlreadySubmittedCritique";
            /// <summary>
            /// Application status
            /// </summary>
            public class Status
            {
                public const string Scoring = "Scoring";
                public const string Active = "Active";
            }
            /// <summary>
            /// Scoring views
            /// </summary>
            public class Views
            {
                public const string Scoreboard = "Scoreboard";
                public const string Critique = "Critique";
            }
        }
        /// <summary>
        /// ManageApplicationScoringController ActionMethod names
        /// </summary>
        public class ManageApplicationScoring
        {    
            /// <summary>
            /// Define the actions
            /// </summary>
            public const string Index = "Index";
            public const string AppStatus = "AppStatus";
            public const string AlreadyActiveApp = "AlreadyActiveApp";
            public const string GetSessionsAndPanels = "GetSessionsAndPanels";
            public const string GetPanels = "GetPanels";
            public const string ApplicationDetails = "ApplicationDetails";
            public const string RevStatus = "RevStatus";
            public const string SaveAdminNote = "SaveAdminNote";
        }
        /// <summary>
        /// ProgramRegistrationController ActionMethod names
        /// </summary>
        public class ProgramRegistration
        {
            /// <summary>
            /// Define the actions
            /// </summary>
            public const string Index = "Index";
            public const string RegistrationWizard = "RegistrationWizard";
            public const string SaveRegistrationForm = "SaveRegistrationForm";
            public const string SignRegistrationForm = "SignRegistrationForm";
            public const string GetDocumentPdf = "GetDocumentPdf";
            public const string GetProgramYears = "GetProgramYears";
            /// <summary>
            /// Key for the workflow step
            /// </summary>
            public class WorkflowStepKey
            {
                public const string Acknowledgement = "Acknowledgement";
                public const string AcknowlegementCprit = "AcknowledgementCprit";
                public const string BiasCoi = "BiasCoi";
                public const string BiasCoiCprit = "BiasCoiCprit";
                public const string Contract = "Contract";
                public const string ContractCprit = "ContractCprit";
                public const string EmContact = "EmContact";
                public const string Sign = "Sign";
            }
            /// <summary>
            /// Value for the workflow step
            /// </summary>
            public class WorkflowStepValue
            {
                public const string EmContact = "Emergency Contact";
                public const string ConfirmSign = "Confirm/Sign";
            }
            /// <summary>
            /// Key for the content
            /// </summary>
            public class ContentKey
            {
                public const string BiasDeclaration = "BiasDeclaration";
                public const string ApplicationSubmitted = "ApplicationSubmitted";
                public const string TitleOnApplication = "TitleOnApplication";
                public const string RoleOnApplication = "RoleOnApplication";
                public const string OrganizationBias = "OrganizationBias";
                public const string OrganizationBiasDetails = "OrganizationBiasDetails";
                public const string ApplicationParticipated = "ApplicationParticipated";
                public const string ConsultantFeeAccepted = "ConsultantFeeAccepted";
                public const string BusinessCategory = "BusinessCategory";
            }
        }
        /// <summary>
        /// HomeController ActionMethod names
        /// </summary>
        public class Home
        {
            /// <summary>
            /// Define the actions
            /// </summary>
            public const string Index = "Index";
            public const string Dashboard = "Dashboard";
            public const string PrivacyPolicy = "PrivacyPolicy";
            public const string Copyright = "Copyright";
            public const string RadEditor = "RadEditor";
        }
        /// <summary>
        /// Library ActionMethod names
        /// </summary>
        public class Library
        {
            /// <summary>
            /// Define the actions
            /// </summary>
            public const string Index = "Index";
            public const string SetProgramYear = "SetProgramYear";
            public const string MarkReviewed = "MarkReviewed";
        }
        /// <summary>
        /// Worklist ActionMethod names
        /// </summary>
        public class Worklist
        {
            /// <summary>
            /// The save reviewed
            /// </summary>
            public const string SaveReviewed = "SaveReviewed";
        }
        /// <summary>
        /// AccountController ActionMethod names
        /// </summary>
        public class Account
        {
            public const string LogOn = "LogOn";
            public const string LogOff = "LogOff";
            public const string Reset = "Reset";
            public const string Reset2 = "Reset2";
            public const string Reset3 = "Reset3";
        }
    }
}