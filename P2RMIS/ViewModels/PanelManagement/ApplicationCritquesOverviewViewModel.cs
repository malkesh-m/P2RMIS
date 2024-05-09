using System;
using System.Collections.Generic;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Web.UI.Models
{
   
        /// <summary>
    /// The view model for the individual application critiques overview
    /// </summary>
    public class ApplicationCritiquesOverviewViewModel
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor - initialize so model is usable.
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        public ApplicationCritiquesOverviewViewModel(int panelApplicationId, int sessionPanelId)
        {
            this.ApplicationCritiques = new ApplicationCritiqueModel();
            this.PanelApplicationId = panelApplicationId;
            this.SessionPanelId = sessionPanelId;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Object for an application's critique information
        /// </summary>
        public IApplicationCritiqueModel ApplicationCritiques { get; set; }
        /// <summary>
        /// Id for an application assignment to a panel
        /// </summary>
        public int PanelApplicationId { get; private set; }
        /// <summary>
        /// SessionPanel entity identifier
        /// </summary>
        public int SessionPanelId { get; private set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is meeting phase started.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is meeting phase started; otherwise, <c>false</c>.
        /// </value>
        public bool IsMeetingPhaseStarted { get; set; }
        #endregion
        #region Helpers
        /// <summary>
        /// Helper checking if the critique is able to be edited.
        /// </summary>
        /// <param name="dateSubmitted">Date/time critique was edited</param>
        /// <param name="startDate">Date/time phase was started</param>
        /// <param name="endDate">Date/time phase was ended</param>
        /// <param name="reOpenStartDate">Date/time phase was reopened</param>
        /// <param name="reOpenEndDate">Date/time reopen phase was ended</param>
        /// <param name="lastSubmittedStep">Last step that has a submitted critique</param>
        /// <param name="currentStep">Step to determine if critique can be edited</param>
        /// <returns></returns>
        public bool IsAbleToEditCritique(Nullable<DateTime> dateSubmitted, DateTime startDate, DateTime endDate, DateTime? reOpenStartDate, DateTime? reOpenEndDate, int lastSubmittedStep, int currentStep)
        {
            bool result = false;
            //
            // if the critique was already submitted for this step then we don't need to allow edit
            //
            if (ViewHelpers.IsCritiqueSubmitted(dateSubmitted))
            { }
            //
            // Otherwise the phase must be open.  If it is open then if this step is 1 greater than the last submitted step.
            //
            else if (ViewHelpers.WithInDates(dateSubmitted, startDate, endDate, reOpenStartDate, reOpenEndDate))
            {
                result = currentStep == (lastSubmittedStep + 1);
            }
            return result;        
        }
        /// <summary>
        /// Standard panel management critique status format
        /// </summary>
        /// <param name="thisCritiquesInformation">Critique information object</param>
        /// <returns>Critique status in string format</returns>
        public string FormatCritiqueStatus(ICritiquePhaseInformation thisCritiquesInformation)
        {
            return (thisCritiquesInformation.ContentExists && thisCritiquesInformation.PhaseStartDate <= GlobalProperties.P2rmisDateTimeNow) ? 
                (thisCritiquesInformation.DateSubmitted == null ? 
                Invariables.Labels.PanelManagement.Critiques.NotSubmitted : 
                Invariables.Labels.PanelManagement.Critiques.Submitted) : 
                Invariables.Labels.PanelManagement.Critiques.NotStarted;
        }
        #endregion
    }
}