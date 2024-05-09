using System.Collections.Generic;
using System.Text;
using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.WebModels.ApplicationScoring;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model for the Expertise/COI modal
    /// </summary>
    public class ExpertiseCoiViewModel
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor - initialize so model is usable.
        /// </summary>
        public ExpertiseCoiViewModel()
        {
            this.ClientAssignmentTypeList = new List<IAssignmentTypeDropdownList>();
            this.ClientCoiList = new List<IClientCoiDropdownList>();
            this.ClientExpertiseRatingList = new List<IClientExpertiseRatingDropdownList>();
            this.ApplicationInfo = new ApplicationInformationModel();
            this.Collaborators = new List<IPersonnelWithCoi>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// The log number
        /// </summary>
        public string LogNumber;
        /// <summary>
        /// The reviewer name
        /// </summary>
        public string ReviewerName;
        /// <summary>
        /// The list of assignment types
        /// </summary>
        public List<IAssignmentTypeDropdownList> ClientAssignmentTypeList { get; set; }
        /// <summary>
        /// The list of available client COI types
        /// </summary>
        public List<IClientCoiDropdownList> ClientCoiList { get; set; }
        /// <summary>
        /// The list of available client expertise ratings
        /// </summary>
        public List<IClientExpertiseRatingDropdownList> ClientExpertiseRatingList { get; set; }
        /// <summary>
        /// The current assignment type identifier
        /// </summary>
        public int? AssignmentTypeId { get; set; }
        /// <summary>
        /// the clients assignment type Id
        /// </summary>
        public int? ClientAssignmentTypeId { get; set; }
        /// <summary>
        /// The client's current COI type identifier
        /// </summary>
        public int? ClientCoiTypeId { get; set; }
        /// <summary>
        /// The client's current expertise rating identifier
        /// </summary>
        public int? ClientExpertiseRatingId { get; set; }
        /// <summary>
        /// The current comment
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// The client's current session expertise rating identifier
        /// </summary>
        public int? CurrSessionCoiExpertiseRatingId { get; set; }
        /// <summary>
        /// Panel application identifier
        /// </summary>
        public int PanelApplicationId { get; set; }
        /// <summary>
        /// the users panel assignment id
        /// </summary>
        public int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// User Id of reviewer
        /// </summary>
        public int ReviewerUserId { get; set; }
        /// <summary>
        /// Application information
        /// </summary>
        public IApplicationInformationModel ApplicationInfo { get; set; }
        /// <summary>
        /// List of collaborators
        /// </summary>
        public List<IPersonnelWithCoi> Collaborators { get; set; }
        /// <summary>
        /// Reviewer name and organization
        /// </summary>
        /// <remarks>TO BE REFACTORED to ViewHelpers</remarks>
        public string ReviewerNameAndOrganization
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(ApplicationInfo.PiFirstName);
                if (!string.IsNullOrEmpty(ApplicationInfo.PiLastName))
                {
                    if (builder != null) builder.Append(" ");
                    builder.Append(ApplicationInfo.PiLastName);
                }
                if (!string.IsNullOrEmpty(ApplicationInfo.PiOrganization))
                {
                    if (builder != null) builder.Append(", ");
                    builder.Append(ApplicationInfo.PiOrganization);
                }
                return builder.ToString();
            }
        }
        #endregion
    }
}