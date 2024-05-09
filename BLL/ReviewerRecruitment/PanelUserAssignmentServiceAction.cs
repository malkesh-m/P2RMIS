using System;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.ReviewerRecruitment
{
    /// <summary>
    /// Service Action method to perform CRUD operations on PanelUserAssignment entities.
    /// </summary>
    internal class PanelUserAssignmentServiceAction : ServiceAction<PanelUserAssignment>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public PanelUserAssignmentServiceAction() { }
        /// <summary>
        /// Populate the ServiceAction attributes
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="reviewertUserId">Reviewer User entity identifier</param>
        /// <param name="clientParticipantTypeId">ClientParticipantType entity identifier</param>
        /// <param name="clinetRoleId">ClientRole entity identifier</param>
        /// <param name="participantMethodId">ParticipantMethod entity identifier</param>
        /// <param name="clientApprovalFlag">Client approval state</param>
        /// <param name="restrictedAccessFlag">Restricted access state</param>
        public void Populate(int sessionPanelId, int reviewertUserId, int clientParticipantTypeId, int? clinetRoleId,
                             int participantMethodId, bool? clientApprovalFlag, bool restrictedAccessFlag)
        {
            this.SessionPanelId = sessionPanelId;
            this.ReviewerUserId = reviewertUserId;
            this.ClientParticipantTypeId = clientParticipantTypeId;
            this.ClinetRoleId = clinetRoleId;
            this.ParticipantMethodId = participantMethodId;
            this.ClientApprovalFlag = clientApprovalFlag;
            this.RestrictedAssignedFlag = restrictedAccessFlag;
        }

        /// <summary>
        /// Populates the paneluserassignment action with the specified attributes.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="userId">The user identifier to be assigned to the panel.</param>
        /// <param name="clientParticipantTypeId">The client participant type identifier.</param>
        public void Populate(int sessionPanelId, int userId, int clientParticipantTypeId, int participationMethodId)
        {
            this.SessionPanelId = sessionPanelId;
            this.ReviewerUserId = userId;
            this.ClientParticipantTypeId = clientParticipantTypeId;
            this.ParticipantMethodId = participationMethodId;
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
        protected int ClientParticipantTypeId { get; set; }
        /// <summary>
        /// ClientRole entity identifier
        /// </summary>
        protected int? ClinetRoleId { get; set; }
        /// <summary>
        /// Restricted assignment indicator
        /// </summary>
        protected bool RestrictedAssignedFlag { get; set; }
        /// <summary>
        /// ParticipationMethod entity identifier
        /// </summary>
        protected int ParticipantMethodId { get; set; }
        /// <summary>
        /// Client approval state (Approved; disapproved; N/A)
        /// </summary>
        protected bool? ClientApprovalFlag { get; set; }
        /// <summary>
        /// User entity identifier of user who approved the reviewer
        /// </summary>
        protected int? ClientApprovedBy { get; set; }
        /// <summary>
        /// Date/Time client approval/disapproval was received
        /// </summary>
        protected DateTime? ClientApprovalDate { get; set; }

        public PanelUserAssignment CreatedPanelUserAssignment { get; private set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the UserCommunicationLog entity with information in the ServiceAction.
        /// </summary>
        /// <param name="entity">PanelUserAssignment entity</param>
        protected override void Populate(PanelUserAssignment entity)
        {
            entity.Populate(this.SessionPanelId, this.ReviewerUserId, this.ClientParticipantTypeId, this.ClinetRoleId,
                            this.ParticipantMethodId, this.ClientApprovalFlag, this.RestrictedAssignedFlag, 
                            this.ClientApprovedBy, this.ClientApprovalDate);
        }
        /// <summary>
        /// Indicates if the PanelUserAssignment has data.
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
        /// Optional pre-modify processing logic.  Add any additional processing necessary before the entity object
        /// is modified.
        /// </summary>
        protected override void PreModify(PanelUserAssignment entity)
        {
            //
            // And now we deal with the ClientApproval values
            //
            ServiceActionHelper.ResolveClientApproval(entity, x => this.ClientApprovedBy = x, x => this.ClientApprovalDate = x, this.ClientApprovalFlag, this.UserId);
        }
        /// <summary>
        /// Optional post add processing.  Add any additional processing necessary after the entity object
        /// is added to the framework.                     
        /// </summary>
        protected override void PostAdd(PanelUserAssignment entity)
        {
            //
            // And now we deal with the ClientApproval values
            //
            ServiceActionHelper.ResolveClientApproval(entity, x => this.ClientApprovedBy = x, x => this.ClientApprovalDate = x, this.ClientApprovalFlag, this.UserId);
            CreatedPanelUserAssignment = entity;
        }
        #endregion
    }
}
