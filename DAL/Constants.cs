namespace Sra.P2rmis.Dal.Common
{
    static public class Constants
    {
        #region Constants & Messages
        /// <summary>
        /// Message used indicting that a repository does not support a GenericRepository function.
        /// </summary>
        public const string NotSupportedMessage = "{0} method is not supported for this repository.";
        #endregion
        public const string SYSTEM_DATE_FORMAT = "dd MMM yyyy  hh:mm:ss tt";


#region LookupTemplateCategory Stage Type

            //LookupTemplateCategory
            public const string TEMPLATE_CAT_EMAIL_STR = "Email Template";
            public const string TEMPLATE_CAT_CRITIQUE_STR = "Critique Template";
            public const string TEMPLATE_CAT_COMPLIANCE_STR = "Compliance Template";
            public const string TEMPLATE_CAT_SS_STR = "SS Template";

            //LookupTemplateStage
            public const string TEMPLATE_STAGE_DRAFT_STR = "Draft";
            public const string TEMPLATE_STAGE_PUBLISHED_STR = "Published";
            public const string TEMPLATE_STAGE_SUPERSEDED_STR = "Superseded";

            //LookupTemplateType
            public const string TEMPLATE_TYPE_SYSTEM_GENERATED_STR = "System Generated";
            public const string TEMPLATE_TYPE_USER_GENERATED_STR = "User Generated";

            //SystemTemplate Names
            public const string SYSTEM_TEMPLATE_NEW_USER = "P2RMIS Client Access - New User";
            public const string SYSTEM_TEMPLATE_NEW_USER_PW = "P2RMIS Client Access - New User (PW)";
            public const string SYSTEM_TEMPLATE_RESET_USER_ACCOUNT = "P2RMIS Client Access - Reset User Account";
            public const string SYSTEM_TEMPLATE_RESET_USER_ACCOUNT_PW = "P2RMIS Client Access - Reset User Account (PW)";

#endregion

#region User Related or Account related Lookups

            //LookupStage
            public const string STAGE_PENDING = "Pending";
            public const string STAGE_CONFIRMED = "Confirmed";
            public const string STAGE_ACTIVATED = "Activated";
            public const string STAGE_INVITED = "Invited";
            public const string STAGE_LOCKED = "Locked";
            public const string STAGE_DEACTIVATED = "Deactivated";
            public const string STAGE_RESET_PENDING_CONFIRMATION = "Reset-Pending Confirmation";
            public const string STAGE_INVITATION_EXPIRED = "Invitation Expired";
            public const string STAGE_PASSWORD_EXPIRED = "Password Expired";

          

            //LookupAddressType
            public const string LOOKUP_ADDRESS_TYPE_BUSINESS = "Business";
            public const string LOOKUP_ADDRESS_TYPE_HOME = "Home";
            public const string LOOKUP_ADDRESS_TYPE_W9 = "W-9";

            //LookupRole

            public const string LOOKUP_ROLE_SYSTEM_CLIENT_STAFF = "Client";
            public const string LOOKUP_ROLE_SYSTEM_SRA_STAFF = "SRA Staff";
            public const string LOOKUP_ROLE_SYSTEM_SRO = "SRO";
            public const string LOOKUP_ROLE_SYSTEM_WEBMASTER = "Webmaster";

            //LookupGender
            public const string LOOKUP_GENDER_FEMALE = "Female";
            public const string LOOKUP_GENDER_MALE = "Male";

            //LookupRole context
            public const string LOOKUP_ROLE_CONTEXT_SYSTEM = "system";

            //Client lookups
            public const int LOOKUP_CLIENT_CDMRP = 19;
            public const int LOOKUP_CLIENT_SRA = 8;

#endregion
     
        
    }
}