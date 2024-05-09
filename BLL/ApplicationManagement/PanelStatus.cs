using System;

namespace Sra.P2rmis.Bll.ApplicationManagement
{
    /// <summary>
    /// Object containing the SessionPanel's status
    /// </summary>
    public class PanelStatus
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sessionPanelId"><SessionPanel entity identifier/param>
        public PanelStatus(int sessionPanelId)
        {
            this.SessionPanelId = sessionPanelId;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isPostAssigned">Indicates if the panel is in post assignment phase<</param>
        /// <param name="isReleased">Indicates if the panel is released</param>
        /// <param name="endDate">Phase end date & time</param>
        /// <param name="participantType">User's participant type on this panel</param>
        /// <param name="roleName">User's participant role on this panel</param>
        public void Populate(bool isPostAssigned, bool isReleased, DateTime? endDate, string participantType, string roleName, string phaseName, int clientId)
        {
            this.IsReleased = isReleased;
            this.IsPostAssigned = isPostAssigned;
            this.EndDate = endDate;
            this.ParticipantType = participantType;
            this.RoleName = roleName;
            this.PhaseName = phaseName;
            this.ClientId = clientId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// SessionPanel entity identifier
        /// </summary>
        public int SessionPanelId { get; private set; }
        /// <summary>
        /// Are the applications in the SessionPanel released
        /// </summary>
        public bool IsReleased { get; set; }
        /// <summary>
        /// Are the applications in the SessionPanel in Post Assignment status
        /// </summary>
        public bool IsPostAssigned { get; set; }
        /// <summary>
        /// End date of current phase
        /// </summary>
        public DateTime? EndDate { get; private set; }
        /// <summary>
        /// User participation type on this panel
        /// </summary>
        public string ParticipantType { get; private set; }
        /// <summary>
        /// User role on this panel
        /// </summary>
        public string RoleName { get; private set; }
        /// <summary>
        /// Phase name
        /// </summary>
        public string PhaseName { get; private set; }
        /// <summary>
        /// Client Id
        /// </summary>
        public int ClientId { get; private set; }
        #endregion
    }
}
