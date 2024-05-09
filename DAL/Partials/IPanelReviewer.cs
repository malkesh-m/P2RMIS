using System;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Interface identifying the common attributes between PanelUserAssignment & 
    /// PanelUserPotentialAssignments entities.
    /// 
    /// Additionally specifies any extension methods that are implemented to support
    /// common functions.
    /// </summary>
    public interface IPanelReviewer
    {
        #region Attributes
        int UserId { get; set; }
        int SessionPanelId { get; set; }
        int? ClientRoleId { get; set; }
        ClientParticipantType ClientParticipantType { get; set; }
        SessionPanel SessionPanel { get; set; }
        ClientRole ClientRole { get; set; }
        ParticipationMethod ParticipationMethod { get; set; }
        User User { get; set; }
        DateTime? CreatedDate { get; set; }
        bool? ClientApprovalFlag { get; set; }
        bool RestrictedAssignedFlag { get; set; }
        Nullable<int> ClientApprovalBy { get; set; }
        Nullable<System.DateTime> ClientApprovalDate { get; set; }
        #endregion
        #region Methods
        /// <summary>
        /// Converts the restricted access value to a human readable value
        /// </summary>
        /// <returns>Partial if the RestrictedAssignedFlag is true; Full otherwise</returns>
        string Level();
        /// <summary>
        /// Determines if this reviewer is a potential chair.  By definition
        /// if one person recommended the reviewer as a potential chair then
        /// the result is true;
        /// </summary>
        /// <returns>True if one or more users recommended the reviewer as a chair false otherwise</returns>
        bool IsPotentialChair();
        /// <summary>
        /// Returns the entity identifier.
        /// </summary>
        /// <returns>Entity identifier</returns>
        int EntityId();
        /// <summary>
        /// Returns an indication if the reviewer has been assigned
        /// </summary>
        /// <returns>True if the user has been assigned; false otherwise</returns>
        bool IsAssigned();
        /// <summary>
        /// Indicates if the reviewer has been recruited;
        /// </summary>
        /// <returns></returns>
        bool IsRecruited();
        /// <summary>
        /// Checks whether the user has any incomplete registrations
        /// </summary>
        /// <returns>True if registration is incomplete; otherwise false</returns>
        bool IsRegistrationComplete();
        /// <summary>
        /// Wrapper around ParticipationMethodid properties to reduced to the same type.  In 
        /// PanelUserPotentialAssighment the property type is int? while the type is int in
        /// PanelUserAssighment.
        /// </summary>
        /// <returns>ParticipationMethodId</returns>
        int? BoxParticipationMethodId();
        /// <summary>
        /// Wrapper around ClientParticipationTypeId properties to reduced to the same type.  In 
        /// PanelUserPotentialAssighment the property type is int? while the type is int in
        /// PanelUserAssighment.
        /// </summary>
        /// <returns>ClientParticipationTypeId</returns>
        int? BoxClientParticipationTypeId();
        /// <summary>
        /// Indicates if the contract document is signed
        /// </summary>
        /// <returns>True if the contract document is signed; false otherwise</returns>
        bool IsContractSigned();
        #endregion
    }
}
