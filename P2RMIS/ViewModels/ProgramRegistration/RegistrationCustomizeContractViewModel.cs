using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sra.P2rmis.WebModels.Lists;

namespace Sra.P2rmis.Web.UI.Models
{
    public class RegistrationCustomizeContractViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramRegistrationStatusViewModel"/> class.
        /// </summary>
        public RegistrationCustomizeContractViewModel()
        {
            this.ContractStatusTypeList = new List<IListEntry>();
        }


        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the Contract Type dropdown list.
        /// </summary>
        /// <value>
        /// The Contract Type dropdown list.
        /// </value>
        public List<IListEntry> ContractStatusTypeList { get; set; }

        /// <summary>
        /// Panelist last name
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Contract Bypass reason
        /// </summary>
        public string BypassReason { get; set; }
        /// <summary>
        /// Contract fee amount
        /// </summary>
        public decimal? FeeAmount { get; set; }

        /// <summary>
        /// Customized contract file
        /// </summary>
        public HttpPostedFileBase CustomContractFile { get; set; }

        /// <summary>
        /// Contract status
        /// </summary>
        public int ContractStatusId { get; set; }

        /// <summary>
        /// Identifier for a panel user registration document
        /// </summary>
        public int PanelUserRegistrationDocumentId { get; set; }

        /// <summary>
        /// Whether the user can add an addendum for the current contract file
        /// </summary>
        public bool CanAddAddendum { get; set; }

        /// <summary>
        /// Identifier for a panel user assignment
        /// </summary>
        public int PanelUserAssignmentId { get; set; }
        #endregion
    }
}