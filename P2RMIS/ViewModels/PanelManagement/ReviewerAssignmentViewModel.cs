using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// The view model for the reviewer assignment
    /// </summary>
    public class ReviewerAssignmentViewModel
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor - initialize so model is usable.
        /// </summary>
        public ReviewerAssignmentViewModel()
        {
            this.ClientAssignmentTypeList = new List<IAssignmentTypeDropdownList>();
            this.ClientCoiList = new List<IClientCoiDropdownList>();
            this.ClientExpertiseRatingList = new List<IClientExpertiseRatingDropdownList>();
            this.PresentationOrderList = new List<int>();
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
        /// The list of available presentation orders
        /// </summary>
        public List<int> PresentationOrderList { get; set; }
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
        /// The current presentation order
        /// </summary>
        public int? PresentationOrder { get; set; }
        /// <summary>
        /// The current comment
        /// </summary>
        public string Comment { get; set; }
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
        /// The client's current session expertise rating identifier
        /// </summary>
        public int? CurrSessionCoiExpertiseRatingId { get; set; }
        /// <summary>
        /// The abbreviation for unassigned client assignment type
        /// </summary>
        public const string ClientUnAssignmentTypeAbbreviation = "UnAssign";
        /// <summary>
        /// The index for the unassitgned client assignment type
        /// </summary>
        public const int ClientUnAssignmentTypeId = 0;
        /// <summary>
        /// Add the unassigned client type record to the client assignment type dropdown list
        /// </summary>
        public void AddClientUnAssignmentType()
        {
            IAssignmentTypeDropdownList unassigned = new AssignmentTypeDropdownList();
            unassigned.ClientAssignmentTypeId = ClientUnAssignmentTypeId;
            unassigned.ClientAssignmentTypeAbbreviation = ClientUnAssignmentTypeAbbreviation;

            ClientAssignmentTypeList.Add(unassigned);
            ClientAssignmentTypeList = ClientAssignmentTypeList.OrderByDescending(x => x.ClientAssignmentTypeAbbreviation).ToList();
        }
        #endregion
    }
}