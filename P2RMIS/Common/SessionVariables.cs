
namespace Sra.P2rmis.Web.Common
{
    public class SessionVariables
    {
        /// <summary>
        /// Name for MeetingSessionId session variable
        /// </summary>
        public const string SessionId = "SessionId";
        /// <summary>
        /// Name for SessionPanelId session variable
        /// </summary>
        public const string PanelId = "PanelId";
        /// <summary>
        /// Name for ClientProgramId session variable
        /// </summary>
        public const string ClientProgramId = "ClientProgramId";
        /// <summary>
        /// The program year identifier session variable
        /// </summary>
        public const string ProgramYearId = "ProgramYearId";
        /// <summary>
        /// The award type identifier session variable
        /// </summary>
        public const string AwardTypeId = "AwardTypeId";
        /// <summary>
        /// The fiscal year identifier
        /// </summary>
        public const string FiscalYearId = "FiscalYearId";
        /// <summary>
        /// The client identifier
        /// </summary>
        public const string ClientId = "ClientId";
        /// <summary>
        /// Name for Cycle session variable
        /// </summary>
        public const string Cycle = "Cycle";
        /// <summary>
        /// Name for session variable that stores a list of session panel ids
        /// </summary>
        public const string PanelIdList = "PanelIdList";

        /// <summary>
        /// Session variable that contains a list of client program ids
        /// </summary>
        public const string ClientProgramIdList = "ClientProgramIdList";

        /// <summary>
        /// Session variable that contains a list of selected fiscal years
        /// </summary>
        public const string FiscalYearList = "FiscalYearList";

        /// <summary>
        /// Session variable that contains a list of selected cycles
        /// </summary>
        public const string CycleList = "CycleList";
        /// <summary>
        /// Name for session variable that indicates if the user has been verified
        /// </summary>
        public const string Verified = "Verified";
        /// <summary>
        /// Whether the credential is permanent
        /// </summary>
        public const string CredentialPermanent = "CredentialPermanent";
        /// <summary>
        /// Name for session variable that contains back button url
        /// </summary>
        public const string BackButton = "BackButton";
        /// <summary>
        /// The active log number
        /// </summary>
        public const string ActiveLogNumber = "ActiveLogNumber";

        /// <summary>
        /// The authorized action list
        /// </summary>
        public const string AuthorizedActionList = "AuthorizedActionList";

        /// <summary>
        /// The authorized client list
        /// </summary>
        public const string AuthorizedClientList = "AuthorizedClientList";
        /// <summary>
        /// Session variable to indicate wether password age is expired
        /// </summary>
        public const string PasswordAgeExpired = "PasswordAgeExpired";
    }
}