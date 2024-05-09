using System.Collections.Generic;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.ApplicationScoring;

namespace Sra.P2rmis.Bll.ApplicationScoring
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ApplicationScoringService
    {
        #region Services
        /// <summary>
        /// Creates and populates a web model containing data for PreAssignmentApplications Current Assignment grid
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>Container of PreAssignmentModel objects</returns>
        public Container<IPreAssignmentModel> RetrievePreAssignmentApplications(int sessionPanelId, int userId)
        {
            VerifyRetrievePreAssignmentApplicationsParameters(sessionPanelId,  userId);

            Container<IPreAssignmentModel> container = new Container<IPreAssignmentModel>();
            List<PreAssignmentModel> list = new List<PreAssignmentModel>();

            SessionPanel sessionPanelEntity = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);
            int panelUserAssignmentId = sessionPanelEntity.PanelUserAssignmentId(userId);

            var panelApplications = UnitOfWork.PanelApplicationRepository.GetPanelApplicationsForPreAssigned(sessionPanelId);
            foreach (PanelApplication panelApplicationEntity in panelApplications)
            {
                //
                // For convenience define these locals
                //
                var applicationEntity = panelApplicationEntity.Application;
                var applicationPersonnelEntity = applicationEntity.PrimaryInvestigator();
                PanelApplicationReviewerExpertise panelApplicationReviewerExpertiseEntity = panelApplicationEntity.GetUsersExpertiseOnApplication(panelUserAssignmentId);
                //
                //  create a model & add it to the list
                //
                PreAssignmentModel model = new PreAssignmentModel(applicationEntity.Blinded());
                list.Add(model);
                //
                // now populate the model
                //
                string firstNameValue = IsBlindedValue(applicationEntity.Blinded(), string.Empty, applicationPersonnelEntity.FirstName);
                string lastNameValue = IsBlindedValue(applicationEntity.Blinded(), BlindedString, applicationPersonnelEntity.LastName);
                string organizationValue = IsBlindedValue(applicationEntity.Blinded(), string.Empty, applicationPersonnelEntity.OrganizationName);

                model.Populate(applicationEntity.LogNumber, applicationEntity.ApplicationTitle, firstNameValue, lastNameValue, applicationEntity.AwardAbbreviation(), organizationValue);
                model.PopulateEntityIdentifiers(panelApplicationEntity.PanelApplicationId, applicationEntity.ApplicationId, sessionPanelId, sessionPanelEntity.PanelAbbreviation, sessionPanelEntity.PanelName, panelUserAssignmentId);
                //
                // There may or may not be an experience
                //
                if (panelApplicationReviewerExpertiseEntity != null)
                {
                    model.PopulateExperience(panelApplicationReviewerExpertiseEntity.Conflict(), panelApplicationReviewerExpertiseEntity.Rating());
                }
            }
            container.ModelList = list;

            return container;
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Verify the parameters for RetrievePreAssignmentApplications
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        private void VerifyRetrievePreAssignmentApplicationsParameters(int sessionPanelId, int userId)
        {
            ValidateInt(sessionPanelId, "ApplicationScoringService.RetrievePreAssignmentApplications", "sessionPanelId");
            ValidateInt(userId, "ApplicationScoringService.RetrievePreAssignmentApplications", "userId");
        }
        /// <summary>
        /// Controls display of blinded data
        /// </summary>
        /// <param name="value">Data value</param>
        /// <returns>Data value if not blinded; "Blind" string otherwise</returns>
        public static string IsBlindedValue(bool isBlind, string Blind, string value)
        {
            return (isBlind) ? Blind : value;
        }
        #endregion
    }
}
