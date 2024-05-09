using Sra.P2rmis.Dal.Interfaces;
using System;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's PanelUserPotentialAssignment object. 
    /// </summary>
    public partial class PanelUserPotentialAssignment: IPanelReviewer, IStandardDateFields
    {
        /// <summary>
        /// Populate the PanelUserPotentialAssignment's properties
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">Reviewer User entity identifier</param>
        /// <param name="clientParticipantTypeId">ClientParticipantType entity identifier</param>
        /// <param name="clientRoleId">ClientRole entity identifier</param>
        /// <param name="participantMethodId">ParticipantMethod entity identifier</param>
        /// <param name="clientApprovalFlag">Client approval state</param>
        /// <param name="restrictedAssignedFlag">Restricted access state</param>
        /// <param name="recruitedFlag">Indicates if the reviewer was assigned to the panel</param>
        /// <param name="recruitedDate">Reviewer recruited date/time</param>
        /// <param name="clientApprovedBy">User entity identifier who approved the reviewer for the client</param>
        /// <param name="clientApprovalDate">Date/Time reviewer was approved by the client</param>
        public void Populate(int sessionPanelId, int userId, int? clientParticipantTypeId, int? clientRoleId,
                             int? participantMethodId, bool? clientApprovalFlag, bool restrictedAssignedFlag, bool recruitedFlag, 
                             DateTime? recruitedDate, int? clientApprovedBy, DateTime? clientApprovalDate)
        {
            //
            // Populate the common fields
            //
            Helper.Populate(this, sessionPanelId, userId,
                            clientRoleId, clientApprovalFlag, restrictedAssignedFlag,
                            clientApprovedBy, clientApprovalDate);
            //
            // Now we do the non-common fields
            //
            this.ClientParticipantTypeId = clientParticipantTypeId;
            this.ParticipationMethodId = participantMethodId;
            this.RecruitedFlag = recruitedFlag;
            this.RecruitedDate = recruitedDate;
        }
        /// <summary>
        /// Converts the restricted access value to a human readable value
        /// </summary>
        /// <returns>Partial if the RestrictedAssignedFlag is true; Full otherwise</returns>
        public string Level()
        {
            return Helper.Level(this.RestrictedAssignedFlag);
        }
        /// <summary>
        /// Determines if this reviewer is a potential chair.  By definition
        /// if one person recommended the reviewer as a potential chair then
        /// the result is true;
        /// </summary>
        /// <returns>True if one or more users recommended the reviewer as a chair false otherwise</returns>
        public bool IsPotentialChair()
        {
            return false;
        }
        /// <summary>
        /// Returns the entity identifier.
        /// </summary>
        /// <returns>Entity identifier</returns>
        public int EntityId()
        {
            return this.PanelUserPotentialAssignmentId;
        }
        /// <summary>
        /// Returns an indication if the reviewer has been assigned. 
        /// Since we are a "potential assignment" entity we return "false".
        /// </summary>
        /// <returns>True if the user has been assigned; false otherwise</returns>
        public bool IsAssigned()
        {
            return false;
        }
        /// <summary>
        /// Indicates if the reviewer has been recruited;
        /// </summary>
        /// <returns>Value of RecruitedFlag</returns>
        public bool IsRecruited()
        {
            return RecruitedFlag;
        }
        /// <summary>
        /// Checks whether the user has any incomplete registrations
        /// </summary>
        /// <returns>Always false</returns>
        public bool IsRegistrationComplete()
        {
            return false;
        }
        /// <summary>
        /// Indicates if the contract document is signed
        /// </summary>
        /// <returns>True if the contract document is signed; false otherwise</returns>
        public bool IsContractSigned()
        {
            return false;
        }
        /// <summary>
        /// Determines if the entity's fiscal year is greater than the target year.
        /// </summary>
        /// <param name="target">Target fiscal year</param>
        /// <returns>True if the fiscal year is greater than the the target; false otherwise</returns>
        public bool FiscalYearGreaterThan(int target)
        {
            return (SessionPanel.GetNumericFiscalYear() >= target);
        }
        /// <summary>
        /// Wrapper around ParticipationMethodid properties to reduced to the same typ.  In 
        /// PanelUserPotentialAssighment the property type is int? while the type is int in
        /// PanelUserAssighment.
        /// </summary>
        /// <returns>ParticipationMethodId</returns>
        public int? BoxParticipationMethodId()
        {
            return this.ParticipationMethodId;
        }
        /// <summary>
        /// Wrapper around ClientParticipationTypeId properties to reduced to the same type.  In 
        /// PanelUserPotentialAssighment the property type is int? while the type is int in
        /// PanelUserAssighment.
        /// </summary>
        /// <returns>ClientParticipationTypeId</returns>
        public int? BoxClientParticipationTypeId()
        {
            return this.ClientParticipantTypeId;
        }
    }
}
