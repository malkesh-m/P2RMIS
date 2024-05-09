using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Sra.P2rmis.Dal
{
    [MetadataType(typeof(SystemTemplateMetaData))]
    public partial class SystemTemplate//: IValidatableObject
    {
        public const string RequestTransfer = "P2RMIS Application Transfer Request";
        public const string RequestReviewerTransfer = "P2RMIS Reviewer Transfer Request";
        public const string RequestReviewerRelease = "P2RMIS Reviewer Release Request";
        public const string CritiqueReset = "P2RMIS Critique Reset";
        /// <summary>
        /// System templates by name
        /// </summary>
        public class Indexes
        {
            
            /// <summary>
            /// Template for sending a new user their user name
            /// </summary>
            public const string SYSTEM_TEMPLATE_NEW_USER = "P2RMIS Client Access - New User";
            /// <summary>
            /// Template for sending a new user their temporary password
            /// </summary>
            public const string SYSTEM_TEMPLATE_NEW_USER_PW = "P2RMIS Client Access - New User (PW)";
            /// <summary>
            /// Template for resetting a user's account
            /// </summary>
            public const string SYSTEM_TEMPLATE_RESET_USER_ACCOUNT = "P2RMIS Client Access - Reset User Account";
            /// <summary>
            /// Template for resetting a user's account password
            /// </summary>
            public const string SYSTEM_TEMPLATE_RESET_USER_ACCOUNT_PW = "P2RMIS Client Access - Reset User Account (PW)";
            /// <summary>
            /// Template for notifying a user of their assignment to a panel
            /// </summary>
            public const string SYSTEM_TEMPLATE_REVIEWER_ASSIGNMENT_NOTIFICATION = "P2RMIS Reviewer Assignment Notification";
            /// <summary>
            /// Template for Password Change Notification
            /// </summary>
            public const string SYSTEM_TEMPLATE_PASSWORD_CHANGE_NOTIFICATION = "P2RMIS Client Access - User Password Change Notification";

            /// <summary>
            /// The discussion board comment template identifier
            /// </summary>
            public const int DiscussionBoardCommentTemplate = 8;

            /// <summary>
            /// The new ticket created
            /// </summary>
            public const int NewTicketCreated = 12;
            public const int StartingNewDiscussion = 1012;
        }
            public class SystemTemplateMetaData
            {
                [HiddenInput(DisplayValue = false)]
                public int TemplateId { get; set; }

              
                [Required(ErrorMessage = "Template Name Required")]
                [StringLength(100, ErrorMessage = "Length must be under 100 characters")]
                [Remote("IsNameAvailable", "EmailTemplate", ErrorMessage = "A template with this name already exists.", AdditionalFields = "TemplateId")]
                public string Name { get; set; }

                [HiddenInput(DisplayValue=false)]
                public Nullable<System.Guid> rowguid { get; set; }

            }

     }
}
