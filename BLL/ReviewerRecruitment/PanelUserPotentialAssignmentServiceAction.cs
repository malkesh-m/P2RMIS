using Sra.P2rmis.Dal;
using System;

namespace Sra.P2rmis.Bll.ReviewerRecruitment
{
    /// <summary>
    /// Service Action method to perform CRUD operations on PanelUserPotentialAssignment entities.
    /// </summary>
    internal class PanelUserPotentialAssignmentServiceAction : ServiceAction<PanelUserPotentialAssignment>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public PanelUserPotentialAssignmentServiceAction() { }
        /// <summary>
        /// Initialize the action's data values
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="reviewertUserId">Reviewer entity identifier</param>
        /// <param name="clientParticipantTypeId">ClientParticipantType entity identifier</param>
        /// <param name="clinetRoleId">ClientRole entity identifier</param>
        /// <param name="participantMethodId">ParticipantMethod entity identifier</param>
        /// <param name="clientApprovalFlag">Client approval state</param>
        /// <param name="restrictedAccessFlag">Restricted access state</param>
        /// <param name="recruitedFlag">Indicates if the reviewer was assigned to the panel</param>
        public void Populate(int sessionPanelId, int reviewertUserId, int? clientParticipantTypeId, int? clientRoleId, 
                             int? participantMethodId, bool? clientApprovalFlag, bool restrictedAccessFlag, bool recruitedFlag)
        {
            this.SessionPanelId = sessionPanelId;
            this.ReviewerUserId = reviewertUserId;
            this.ClientParticipantTypeId = clientParticipantTypeId;
            this.ClientRoleId = clientRoleId;
            this.ParticipantMethodId = participantMethodId;
            this.ClientApprovalFlag = clientApprovalFlag;
            this.RestrictedAssignedFlag = restrictedAccessFlag;
            this.RecruitedFlag = recruitedFlag;
        }
        /// <summary>
        /// Initialize the action's data values - default
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="reviewerUserId">Reviewer entity identifier</param>
        /// <param name="clientRoleId">ClientRole entity identifier</param>
        /// <param name="restrictedAccessFlag">Restricted access state</param>
        /// <param name="recruitedFlag">Indicates if the reviewer was assigned to the panel</param>
        public void Populate(int sessionPanelId, int reviewerUserId, int clientRoleId, bool restrictedAccessFlag)
        {
            // default: ClientParticipantTypeId = null, ParticipantMethoId = null, ClientApprovalFlag = null, RecruitedFlag = false
            Populate(sessionPanelId, reviewerUserId, null, clientRoleId, null, null, restrictedAccessFlag, false);
        }
        /// <summary>
        /// Initialize the actions data values to cause the record to be deleted
        /// </summary>
        public void Populate()
        {
            ToDelete = true;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// SessionPanel entity identifier
        /// </summary>
        protected int SessionPanelId { get; set; }
        /// <summary>
        /// Reviewer User entity identify
        /// </summary>
        protected int ReviewerUserId { get; set; }
        /// <summary>
        /// ClientParticipantType entity identifier
        /// </summary>
        protected int? ClientParticipantTypeId { get; set; }
        /// <summary>
        /// ClientRole entity identifier
        /// </summary>
        protected int? ClientRoleId { get;  set; }
        /// <summary>
        /// Restricted assignment indicator
        /// </summary>
        protected bool RestrictedAssignedFlag { get; set; }
        /// <summary>
        /// ParticipationMethod entity identifier
        /// </summary>
        protected int? ParticipantMethodId { get; set; }
        /// <summary>
        /// Client approval state (Approved; disapproved; N/A)
        /// </summary>
        protected bool? ClientApprovalFlag { get; set; }
        /// <summary>
        /// Indicates if the reviewer was assigned to the panel
        /// </summary>
        protected bool RecruitedFlag { get; set; }
        /// <summary>
        /// Date/Time reviewer was recruited to the panel
        /// </summary>
        protected DateTime? RecruitedDate { get; set; }
        /// <summary>
        /// User entity identifier of user who approved the reviewer
        /// </summary>
        protected int? ClientApprovedBy { get; set; }
        /// <summary>
        /// Indicates if the panel user potential assignment is to be deleted
        /// </summary>
        protected bool ToDelete { get; set; }
        /// <summary>
        /// 
        /// Date/Time client approval/disapproval was received
        /// </summary>
        protected DateTime? ClientApprovalDate { get; set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the UserCommunicationLog entity with information in the ServiceAction.
        /// </summary>
        /// <param name="entity">PanelUserPotentialAssignment entity</param>
        protected override void Populate(PanelUserPotentialAssignment entity)
        {
            entity.Populate(this.SessionPanelId, this.ReviewerUserId, this.ClientParticipantTypeId, this.ClientRoleId,
                            this.ParticipantMethodId, this.ClientApprovalFlag, this.RestrictedAssignedFlag, this.RecruitedFlag,
                            this.RecruitedDate, this.ClientApprovedBy, this.ClientApprovalDate);
        }
        /// <summary>
        /// Indicates if the PanelUserPotentialAssignment has data.  The UI guarantees it.
        /// </summary>
        protected override bool HasData
        {
            get
            {
                //
                // By definition we are expecting that it will have data.  Even
                // null or the empty string is considered as data.
                //
                return true;
            }
        }
        #endregion
        #region Optional overrides
        /// <summary>
        /// Compute values for RecruitedDate; ClientApprovalDate; ClientApprovalFlag & ClientApprovedBy
        /// </summary>
        /// <param name="entity">PanelUserPotentialAssignment entity</param>
        protected override void PreModify(PanelUserPotentialAssignment entity)
        {
            //
            // Determine if any change should be made to the RecruitedFlag & RecruitedDate
            //
            ServiceActionHelper.ResolveRecruited(this.RecruitedFlag, this.RecruitedDate, x => this.RecruitedDate = x);
            //
            // And now we deal with the ClientApproval values
            //
            ServiceActionHelper.ResolveClientApproval(entity, x => this.ClientApprovedBy = x, x => this.ClientApprovalDate = x, this.ClientApprovalFlag, this.UserId);
        }
        /// <summary>
        /// Do we delete the entity?
        /// </summary>
        protected override bool IsDelete
        {
            get
            {
                return this.ToDelete;
            }
        }

        #endregion
    }
}
