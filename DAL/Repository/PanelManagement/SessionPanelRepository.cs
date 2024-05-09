using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.HelperClasses;
using Sra.P2rmis.WebModels.PanelManagement;
using System;
using Sra.P2rmis.CrossCuttingServices;
namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Repository for SessionPanel objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public class SessionPanelRepository: GenericRepository<SessionPanel>, ISessionPanelRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public SessionPanelRepository(P2RMISNETEntities context)
            : base(context)
        {     
             
        }
        #endregion
		#region Services provided
        /// <summary>
        /// Retrieves the list of expertise types for the specified session panel.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier (tells which session)</param
        /// <param name="panelApplicationId">Panel application identifier (tells which application)</param>
        /// <param name="panelUserAssignmentId">Panel user assignment identifier (tells which reviewer)</param>
        /// <returns>Result model containing IExpertiseAssignments object</returns>        
        public ResultModel<IExpertiseAssignments> GetExpertiseAssignments(int sessionPanelId, int panelApplicationId, int panelUserAssignmentId)
        {
            ResultModel<IExpertiseAssignments> result = new ResultModel<IExpertiseAssignments>();
            result.ModelList = RepositoryHelpers.GetExpertiseAssignments(context, sessionPanelId, panelApplicationId, panelUserAssignmentId);

            return result;
        }
        /// <summary>
        /// Retrieves lists of primary email addresses.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Container of IEmailAddress models</returns>
        public ResultModel<IEmailAddress> ListPanelUserEmailAddresses(int sessionPanelId)
        {
            ResultModel<IEmailAddress> result = new ResultModel<IEmailAddress>();
            //result.ModelList = RepositoryHelpers.ListPanelUserEmailAddresses(context, sessionPanelId);
            result.ModelList = RepositoryHelpers.ListPanelUserEmailAddresses(context.SessionPanels, sessionPanelId);

            return result;
        }
        /// <summary>
        /// Get a session panel's program year information
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>IProgramYearModel model</returns>
        public IProgramYearModel GetProgramYear(int sessionPanelId)
        {
            return RepositoryHelpers.GetProgramYear(context, sessionPanelId);
        }
        /// <summary>
        /// Gets the Session Panel by identifier with panel application information.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns>Session panel instance with panel and application data eager loaded</returns>
        public SessionPanel GetByIDWithPanelApplicationInfo(int sessionPanelId)
        {
            var result = GetEager(x => x.SessionPanelId == sessionPanelId, null,
                x => x.PanelApplications.Select(y => y.Application),
                x => x.PanelApplications.Select(y => y.PanelApplicationReviewerAssignments.Select(y2 => y2.ClientAssignmentType.AssignmentType)),
                x => x.PanelStages.Select(y => y.PanelStageSteps)
            );
            return result.FirstOrDefault();
        }

        /// <summary>
        /// Gets the panel by identifier with panel application statuses.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <returns>Session panel instance with panel application status info eager loaded</returns>
        public SessionPanel GetByIDWithPanelApplicationStatus(int sessionPanelId)
        {
            var result = GetEager(x => x.SessionPanelId == sessionPanelId, null,
                x => x.PanelApplications.Select(y => y.Application),
                x => x.PanelApplications.Select(y => y.ApplicationReviewStatus.Select(y2 => y2.ReviewStatu.ReviewStatusType))
            );
            return result.FirstOrDefault();
        }
        /// <summary>
        /// Retrieves panel stage steps/phases.
        /// </summary>
        /// <returns>PanelStageStepModel collection</returns>
        public List<PanelStageStepModel> GetPanelStageStepsStatus(int sessionPanelId)
        {
            var results = new List<PanelStageStepModel>();

            //
            // determine if the MOD is active, meaning there is at least one comment
            // and check if the panelStageStep is open since they are used several times
            // and need not be recalculated each time
            //

            var workflowSteps = RepositoryHelpers.GetPanelStageStepStatus(context, sessionPanelId);
            workflowSteps.ToList().ForEach(x => results.Add(x));
            return results;
        }

        public SessionPanel GetSessionPanelId(int programYearId, string referralMappingPanelAbbrv)
        {
            var program = context.ProgramPanels.Find(programYearId).SessionPanelId;
            return context.SessionPanels.Find(program);
        }
        #endregion
        #region Services not provided

        #endregion
        #region Overwritten services provided

        #endregion
    }
}
