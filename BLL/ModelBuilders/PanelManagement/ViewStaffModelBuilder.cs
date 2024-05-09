using Sra.P2rmis.Dal;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels.PanelManagement;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.PanelManagement
{
    /// <summary>
    /// Builds one or more WorkList web models for the specified client.
    /// </summary>
    internal class ViewStaffModelBuilder : ContainerModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        public ViewStaffModelBuilder(IUnitOfWork unitOfWork, int sessionPanelId)
            : base(unitOfWork)
        {
            this.SessionPanelId = sessionPanelId;
            this.Results = new Container<IAssignedStaffModel>();
        }
        #endregion
        #region Attributes
        /// <summary>
        /// SessionPanel entity identifier
        /// </summary>
        protected int SessionPanelId {get; set;}
        /// <summary>
        /// ContainerModelBuilder results.
        /// </summary>
        public Container<IAssignedStaffModel> Results { get; private set; }
        #endregion
        #region Builder
        /// <summary>
        /// Build a container of WorkList models.
        /// </summary>
        /// <returns>Model container of WorkList models.</returns>
        public override void BuildContainer()
        {
            //
            // retrieve the SessionPanel
            //
            SessionPanel sessionPanelEntity = GetThisSessionPanel(this.SessionPanelId);
            Filter(sessionPanelEntity.PanelUserAssignments);
        }
        /// <summary>
        /// Filter the SessionPanel's PanelUserAssignment for SROs & RTAs.
        /// </summary>
        /// <param name="collection">Collection of PanelUserAssignment entities</param>
        protected void Filter(ICollection<PanelUserAssignment> collection)
        {
            //
            // This retrieves all users that are assigned to the panel that are not reviewers
            //
            var filteredResults = collection.Where(x => x.ClientParticipantType.IsSro() || x.ClientParticipantType.IsRta() || x.IsCpritChair()).
                //
                // and now we build the model
                //
                Select(x => new AssignedStaffModel(x.User.UserInfoEntity().FirstName, x.User.UserInfoEntity().LastName, x.User.GetUserSystemRoleName(),
                    x.User.PrimaryUserEmailAddress(), x.User.UserInfoEntity().Institution, x.PanelUserAssignmentId));
            //
            // and finally we set up the results for return
            //
            this.Results.ModelList = filteredResults.ToList();
        }
        #endregion
    }
}
