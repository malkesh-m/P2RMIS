using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.WebModels.HelperClasses;
using Sra.P2rmis.WebModels.Files;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model for the communications
    /// </summary>
    public class CommunicationViewModel : PanelManagementViewModel
    {
        #region Constants
        public new const string SubTabController = "/PanelManagement/";
        public new const string SubTab1 = "Compose Email";
        public new const string SubTab2 = "Email Logs";
        public new const string SubTab1Route = "Communication";
        public new const string SubTab2Route = "CommunicationLog";
        public new const string SubTab1Link = SubTabController + SubTab1Route;
        public new const string SubTab2Link = SubTabController + SubTab2Route;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CommunicationViewModel()
            : base()
        {
            ///instantiate tab list
            List<TabItem> theTabList = new List<TabItem>();
            ///add items to list
            theTabList.Add(new TabItem() { TabOrder = 1, TabName = SubTab1, TabLink = SubTab1Link });
            theTabList.Add(new TabItem() { TabOrder = 2, TabName = SubTab2, TabLink = SubTab2Link });

            ///set property to the tab list
            this.SubTabs = theTabList;

            // instantiate panel administrator list
            this.PanelAdministrators = new List<IEmailAddress>();
            // instantiate available reviewer email addresses
            this.AvailableReviewerEmailAddresses = new List<IEmailAddress>();
            // instantiate selected reviewer email addresses
            this.SelectedPanelUserAssignmentIds = new List<string>();
            //
            // Default to no email templates
            //
            this.EmailTemplates = new List<ITemplateFileInfoModel>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// The session panel identifier for this session
        /// </summary>
        public int SessionPanelId { get; set; }
        /// <summary>
        /// The communication's originator's email address
        /// </summary>
        public string SenderEmailAddress { get; set; }
        /// <summary>
        /// List of panel administrators
        /// </summary>
        ///
        public List<IEmailAddress> PanelAdministrators { get; set; }
        /// <summary>
        /// List of the available reviewers' email address  
        /// </summary>
        public List<IEmailAddress> AvailableReviewerEmailAddresses { get; set; }
        /// <summary>
        /// List of the Selected reviewers' panel user assignment identifiers
        /// </summary>
        public List<string> SelectedPanelUserAssignmentIds { get; set; }
        /// <summary>
        /// The reviewers to show on the email list
        /// </summary>
        public List<SelectListItem> ReviewerEmailsToShow
        {
            get
            {
                return (AvailableReviewerEmailAddresses != null) ?
                    AvailableReviewerEmailAddresses.Select(x => new SelectListItem { Text = ViewHelpers.ConstructNameWithComma(x.FirstName, x.LastName) + ViewHelpers.ValueInParenthesis(x.ParticipantTypeAbbreviation), Value = x.PanelUserAssignmentId.ToString() }).OrderBy(o => o.Text).ToList() :
                    new List<SelectListItem>();
            }
        }
        /// <summary>
        /// The administrators email list
        /// </summary>
        public List<SelectListItem> PanelAdministratorEmailsToShow
        {
            get
            {
                return (PanelAdministrators != null) ?
                    PanelAdministrators.Select(x => new SelectListItem { Text = ViewHelpers.ConstructNameWithComma(x.FirstName, x.LastName) + ViewHelpers.ValueInParenthesis(x.ParticipantTypeAbbreviation), Value = x.PanelUserAssignmentId.ToString() }).OrderBy(o => o.Text).ToList() :
                    new List<SelectListItem>();
            }
        }
        /// <summary>
        /// The email template list
        /// </summary>
        public List<ITemplateFileInfoModel> EmailTemplates { get; set; }
        /// <summary>
        /// The email template list ( do not think this is used)
        /// </summary>
        public List<SelectListItem> Templates
        {
            get
            {
                return new List<SelectListItem>();
            }
        }

        /// <summary>
        /// Whether the logged in user cannot send email from their current address
        /// </summary>
        /// <remarks>This is currently the case if a personal email is set as preferred or they somehow don't have an email in the system</remarks>
        public bool CannotSendEmailWithCurrentAddress => string.IsNullOrWhiteSpace(SenderEmailAddress);
        #endregion
    }
}