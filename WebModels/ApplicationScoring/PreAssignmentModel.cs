
namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Interface for the Reviewer Pre-Assignment view.
    /// </summary>
    public interface IPreAssignmentModel : IBaseAssignmentModel
    {
        /// <summary>
        /// User's experience level on the application
        /// </summary>
        string ExpertiseLevel { get; }
        /// <summary>
        /// Conflict flag (True if a COI; False if not)
        /// </summary>
        bool? ConflictFlag { get; }
        /// <summary>
        /// The organization
        /// </summary>
        string PIOrganization { get; }
        /// <summary>
        /// True data is blinded; False data is not blinded
        /// </summary>
        bool Blinded { get; }
    }
    /// <summary>
    /// Web model for the reviewers PreAssignment view
    /// </summary>
    public class PreAssignmentModel: BaseAssignmentModel, IPreAssignmentModel
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        public PreAssignmentModel(bool blinded): base()
        {
            this.Blinded = blinded;
        }
        /// <summary>
        /// Populates the general data sections of the web model.
        /// </summary>
        /// <param name="applicationLogNumber">Application log number</param>
        /// <param name="title">Application title</param>
        /// <param name="pIFirstName">Principal investigator first name</param>
        /// <param name="pILastName">Principal investigator last name</param>
        /// <param name="mechanism">Mechanism</param>
        /// <param name="pIOrganization">The organization</param>
        public void Populate(string applicationLogNumber, string title, string pIFirstName, string pILastName, string mechanism, string pIOrganization)
        {
            //
            // first do the base level attributes
            // 
            base.Populate(applicationLogNumber, title, pIFirstName, pILastName, mechanism);
            //
            // Now do the model specific
            //
            this.PIOrganization = pIOrganization;
        }
        /// <summary>
        /// Populates the entity identifier section of the web model
        /// </summary>
        /// <param name="panelApplicationId">PanelApplication entity identifier</param>
        /// <param name="applicationId">Application entity identifier</param>
        /// <param name="sessionPanelAbbreviation">Session panel abbreviation</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        public void PopulateEntityIdentifiers(int panelApplicationId, int applicationId, int sessionPanelId, string sessionPanelAbbreviation, string sessionPanelName, int panelUserAssignmentId)
        {
            //
            // first do the base level attributes
            // 
            base.PopulateEntityIdentifiers(panelApplicationId, applicationId, sessionPanelId, sessionPanelAbbreviation, sessionPanelName);
            //
            // Now do the model specific
            //
            this.PanelUserAssignmentId = panelUserAssignmentId;
        }
        /// <summary>
        /// Populate the experience/COI portion of the web model
        /// </summary>
        /// <param name="conflictFlag">Indicates if the experience is COI</param>
        /// <param name="experiencLevel">User's experience level on the application</param>
        public void PopulateExperience(bool? conflictFlag, string experiencLevel)
        {
            this.ConflictFlag = conflictFlag;
            this.ExpertiseLevel = experiencLevel;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// User's experience level
        /// </summary>
        public string ExpertiseLevel { get; private set; }
        /// <summary>
        /// Conflict flag (True if a COI; False if not)
        /// </summary>
        public bool? ConflictFlag { get; private set; }
        /// <summary>
        /// The organization
        /// </summary>
        public string PIOrganization { get; private set; }
        /// <summary>
        /// True data is blinded; False data is not blinded
        /// </summary>
        public bool Blinded { get; private set; }
        #endregion

    }
}
