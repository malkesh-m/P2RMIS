using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.ResultModels;
using Sra.P2rmis.WebModels.MeetingManagement;
using Sra.P2rmis.WebModels.SummaryStatement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Dal.Repository
{
    /// 
    /// Repository for PanelUserAssignment objects.  Provides CRUD methods and 
    /// associated database services.
    ///
    public class PanelUserAssignmentRepository : GenericRepository<PanelUserAssignment>, IPanelUserAssignmentRepository
    {

        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public PanelUserAssignmentRepository(P2RMISNETEntities context)
            : base(context)
        {     
             
        }
        #endregion
		
		#region Services provided
        /// <summary>
        /// Retrieves list of reviewer names that are associated with a session panel.
        /// </summary>
        /// <param name="sessionPanelId">Session panel identifier</param>
        /// <returns>Container of IUserModel models</returns>
        public ResultModel<IUserModel> ListReviewerNames(int sessionPanelId)
        {
            ResultModel<IUserModel> result = new ResultModel<IUserModel>();
            result.ModelList = RepositoryHelpers.ListReviewerNames(context, sessionPanelId);

            return result;
        }
 
        /// <summary>
        /// Gets the panel user assignments for open panels.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IEnumerable<PanelUserAssignment> GetPanelUserAssignmentsForOpenPanels(int userId)
        {
            var puas = GetEager(x => x.UserId == userId, null,
                z1 => z1.ClientParticipantType,
                z1 => z1.SessionPanel.ProgramPanels.Select(z2 => z2.ProgramYear.ClientProgram));
            return puas;
        }
        /// <summary>
        /// Gets the assigned panel application ids.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns></returns>
        public List<int> GetAssignedPanelApplicationIds(int panelUserAssignmentId)
        {
            List<int> listOfAssignedPanelApplicationIds = new List<int>();
            PanelUserAssignment panelUserAssignmentEntity = GetByID(panelUserAssignmentId);
            if (panelUserAssignmentEntity != null)
            {
                if (panelUserAssignmentEntity.RestrictedAssignedFlag)
                {
                    // When "restricted" we pull out only the PanelApplicatons that
                    // the user is assigned to.
                    listOfAssignedPanelApplicationIds = panelUserAssignmentEntity.PanelApplicationReviewerAssignments.
                                                                Select(x => x.PanelApplicationId).ToList();                   
                }
            }
            return listOfAssignedPanelApplicationIds;
        }
        /// <summary>
        /// Gets the client identifier.
        /// </summary>
        /// <param name="panelUserAssignmentId">The panel user assignment identifier.</param>
        /// <returns></returns>
        public int GetClientId(int panelUserAssignmentId)
        {
            var clientId = context.PanelUserAssignments.Where(x => x.PanelUserAssignmentId == panelUserAssignmentId)
                .Select(y => y.SessionPanel.MeetingSession.ClientMeeting.ClientId).FirstOrDefault();
            return clientId;
        }
        #endregion
    }
}
