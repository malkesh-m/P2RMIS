using Sra.P2rmis.Dal;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.ModelBuilders.Permissions
{
    /// <summary>
    /// Checks if an SRO user can access a resource if the target user has an assignment 
    /// to one of the SRO's panels.
    /// </summary>
    internal class SroAssignmentPermissionBuilder: ModelBuilderBase
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        /// <param name="sroUserId"></param>
        /// <param name="targetUserInfoId"></param>
        public SroAssignmentPermissionBuilder(IUnitOfWork unitOfWork, int sroUserId, int targetUserInfoId)
            : base(unitOfWork, sroUserId)
        {
            this.TargetUserInfoId = targetUserInfoId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// The UserInfo entity identifier of the user whose resource the SRO wants to view.
        /// </summary>
        private int TargetUserInfoId { get; set; }
        #endregion
        #region Services
        /// <summary>
        /// Determines if the SRO cam access a resource depending upon the assignment of the target user.
        /// </summary>
        /// <returns>True if the user can access the resource; false otherwise</returns>
        public bool Check()
        {
            //
            // Get the SRO & target User entities.
            // 
            User sroUserEntity = this.GetThisUser(UserId);
            User targetUserEnity = this.GetThisUserInfoEntity(TargetUserInfoId).User;
            //
            // Now find the Session Panels that the user is assigned as an SRO.
            //
            IEnumerable<int> sroSessionEntityIdentifiers = SessionAccess(sroUserEntity.PanelUserAssignments.Where(x => x.ClientParticipantType.IsSro()));

            var d = (sroSessionEntityIdentifiers.Intersect(SessionAccess(targetUserEnity.PanelUserAssignments)).Count() > 0) || 
                    (sroSessionEntityIdentifiers.Intersect(SessionAccess(targetUserEnity.PanelUserPotentialAssignments)).Count() > 0);

            return d;
        }
        /// <summary>
        /// Selects the SessionPanelId of all the IPanelReviewer objects.
        /// </summary>
        /// <param name="assignments">Enumeration of the SessionPanel entity identifiers</param>
        /// <returns></returns>
        protected IEnumerable<int> SessionAccess(IEnumerable<IPanelReviewer> assignments)
        {
            return assignments.Select(x => x.SessionPanelId);
        }
        #endregion
    }
}
