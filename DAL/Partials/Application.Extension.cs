using System;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's Application object.
    /// </summary>
    public partial class Application : IStandardDateFields
    {
        /// <summary>
        /// Locates the default workflow for the application.
        /// </summary>
        /// <returns>ApplicationDefaultWorkflow for the application if one exists; null otherwise</returns>
        public ApplicationDefaultWorkflow FindDefaultWorkflow()
        {
            return this.ApplicationDefaultWorkflows.FirstOrDefault(x => x.ApplicationId == this.ApplicationId);
        }
        /// <summary>
        /// Creates & populates a new ApplicationDefaultWorkflow entity for this application.
        /// </summary>
        /// <param name="workflowId">Workflow identifier to associate with the application</param>
        /// <param name="userId">User identifier of the person requesting the change</param>
        /// <returns>ApplicationDefaultWorkflow created</returns>
        public ApplicationDefaultWorkflow AddDefaultWorkflow(int workflowId, int userId)
        {
            ApplicationDefaultWorkflow defaultWorkflow = new ApplicationDefaultWorkflow();
            defaultWorkflow.Populate(this.ApplicationId, workflowId, userId);
            return defaultWorkflow;
        }
        /// <summary>
        /// Returns the primary investigator
        /// </summary>
        /// <returns>ApplicationPersonnel entity identifying the primary investigator; null otherwise</returns>
        public ApplicationPersonnel PrimaryInvestigator()
        {
            return this.ApplicationPersonnels.Where(x => x.PrimaryFlag == true).DefaultIfEmpty(ApplicationPersonnel.Default).First();
        }
        /// <summary>
        /// Returns the review order
        /// </summary>
        /// <returns>Review order</returns>
        /// <remarks> no reference</remarks>
        public Nullable<int> ReviewOrder()
        {
            return this.PanelApplications.FirstOrDefault().ReviewOrder;
        }
        /// <summary>
        /// Determine the PanelApplication entity identifier.
        /// </summary>
        /// <returns>PanelApplication entity identifier; null if none</returns>
        public Nullable<int> PanelApplicationId()
        {
            int? result = null;

            var panelApplicationEntity = this.PanelApplications.FirstOrDefault();
            if (panelApplicationEntity != null)
            {
                result = panelApplicationEntity.PanelApplicationId;
            }
            return result;
        }
        /// <summary>
        /// Determines whether this instance is released.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is released; otherwise, <c>false</c>.
        /// </returns>
        public bool IsReleased()
        {
            bool isReleased = false;
            var panelApplicationEntity = this.PanelApplications.FirstOrDefault();
            if (panelApplicationEntity != null)
            {
                isReleased = panelApplicationEntity.IsReleased();
            }
            return isReleased;
        }
        /// <summary>
        /// Wrapper to return the award abbreviation for the Mechanism
        /// </summary>
        /// <returns>Award abbreviation</returns>
        public string AwardAbbreviation()
        {
            return this.ProgramMechanism.AwardAbbreviation();
        }
        /// <summary>
        /// Wrapper to return the ProgramMechanism's BlindedFlag;
        /// </summary>
        /// <returns>ProgramMechanism's BlindedFlag</returns>
        public bool Blinded()
        {
            return this.ProgramMechanism.BlindedFlag;
        }
    }
}
