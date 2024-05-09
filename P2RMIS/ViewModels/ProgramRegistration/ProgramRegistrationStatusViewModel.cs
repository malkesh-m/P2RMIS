using System.Collections.Generic;
using Sra.P2rmis.WebModels.Criteria;
using Sra.P2rmis.WebModels.ProgramRegistration;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.UI.Models
{
    /// <summary>
    /// View model for the program registration status page
    /// </summary>
    public class ProgramRegistrationStatusViewModel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramRegistrationStatusViewModel"/> class.
        /// </summary>
        public ProgramRegistrationStatusViewModel()
        {
            this.ClientPrograms = new List<IClientProgramModel>();
            this.ProgramYears = new List<IProgramYearModel>();
            this.SessionPanels = new List<ISessionPanelModel>();
            this.RegistrationStatusList = new List<IProgramRegistrationWebModel>();
            this.SelectedSessionPanels = new List<int>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the session panels dropdown list.
        /// </summary>
        /// <value>
        /// The session panels dropdown list.
        /// </value>
        public List<ISessionPanelModel> SessionPanels { get; set; }
        /// <summary>
        /// Gets or sets the program years dropdown list.
        /// </summary>
        /// <value>
        /// The program years dropdown list.
        /// </value>
        public List<IProgramYearModel> ProgramYears { get; set; }
        /// <summary>
        /// Gets or sets the client programs dropdown list.
        /// </summary>
        /// <value>
        /// The client programs dropdown list.
        /// </value>
        public List<IClientProgramModel> ClientPrograms { get; set; }
        /// <summary>
        /// Gets or sets the selected session panels.
        /// </summary>
        /// <value>
        /// The selected session panels by the user.
        /// </value>
        public List<int> SelectedSessionPanels { get; set; }
        /// <summary>
        /// Gets or sets the selected program year.
        /// </summary>
        /// <value>
        /// The selected program year by the user.
        /// </value>
        public int SelectedProgramYear { get; set; }
        /// <summary>
        /// Gets or sets the selected client program.
        /// </summary>
        /// <value>
        /// The selected client program by the user.
        /// </value>
        public int SelectedClientProgram { get; set; }
        /// <summary>
        /// Gets or sets the registration status list.
        /// </summary>
        /// <value>
        /// The values for the registration status grid.
        /// </value>
        public List<IProgramRegistrationWebModel> RegistrationStatusList { get; set; }

        /// <summary>
        /// Whether the user has permission to customize reviewer contracts
        /// </summary>
        public bool HasCustomizeContractPermissions { get; set; }
        #endregion
    }
}