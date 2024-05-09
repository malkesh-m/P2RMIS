using Sra.P2rmis.Dal;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.Setup.Blocks
{
    /// <summary>
    /// Crud block to use for MeetingSession setup.
    /// </summary>
    internal class SessionBlock : CrudBlock<MeetingSession>, ICrudBlock
    {
        #region Construction & Setup
        public SessionBlock(int clientMeetingId, string sessionAbbreviation, string sessionDescription, DateTime startDate, DateTime endDate)
        {
            this.ClientMeetingId = clientMeetingId;
            this.SessionAbbreviation = sessionAbbreviation;
            this.SessionDescription = sessionDescription;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
        public SessionBlock(int meetingSessionId, int clientMeetingId, string sessionAbbreviation, string sessionDescription, DateTime startDate, DateTime endDate)
        {
            this.SessionAbbreviation = sessionAbbreviation;
            this.SessionDescription = sessionDescription;
            this.MeetingSessionId = meetingSessionId;
            this.ClientMeetingId = clientMeetingId;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
        public SessionBlock(int meetingSessionId)
        {
            this.MeetingSessionId = meetingSessionId;
        }
        #endregion

        public Nullable<int> MeetingSessionId { get; private set; }
        #region Attributes
        /// <summary>
        /// ClientMeeting entity identifier
        /// </summary>
        public int ClientMeetingId { get; private set; }
        /// <summary>
        /// Session abbreviation
        /// </summary>
        public string SessionAbbreviation { get; private set; }
        /// <summary>
        /// Session description
        /// </summary>
        public string SessionDescription { get; private set; }
        /// <summary>
        /// Meeting start date & time
        /// </summary>
        public Nullable<DateTime> StartDate { get; private set; }
        /// <summary>
        /// Meeting end date & time
        /// </summary>
        public Nullable<DateTime> EndDate { get; private set; }
        /// <summary>
        /// Gets the phase collection.
        /// </summary>
        /// <value>
        /// The phase collection.
        /// </value>
        public List<SessionPhase> PhaseCollection { get; set; } = new List<SessionPhase>();
        #endregion
        #region Methods
        /// <summary>
        /// Configures block for an Add operation
        /// </summary>
        internal void ConfigureAdd()
        {
            IsAdd = true;
        }
        /// <summary>
        /// Configures block for an Add operation
        /// </summary>
        internal void ConfigureModify()
        {
            IsModify = true;
        }
        /// <summary>
        /// Configures block for an Add operation
        /// </summary>
        internal void ConfigureDelete()
        {
            IsDelete = true;
        }
        /// <summary>
        /// Adds the session phase.
        /// </summary>
        /// <param name="phase">The phase.</param>
        internal void AddSessionPhase(SessionPhase phase)
        {
            this.PhaseCollection.Add(phase);
        }

        /// <summary>
        /// Sets individual properties on the CRUD entity.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <param name="entity">MeetingSession entity to populate</param>
        internal override void Populate(int userId, MeetingSession entity)
        {
            entity.ClientMeetingId = this.ClientMeetingId;
            entity.SessionAbbreviation = this.SessionAbbreviation;
            entity.SessionDescription = this.SessionDescription;
            if (this.StartDate != null)
                entity.StartDate = this.StartDate;
            if (this.EndDate != null)
                entity.EndDate = this.EndDate;

            PhaseCollection.ForEach(x =>
            {
                var o = entity.SessionPhases.FirstOrDefault(y => y.StepTypeId == x.StepTypeId);
                if (o != null)
                {
                    o.StartDate = x.StartDate;
                    o.EndDate = x.EndDate;
                    o.ReopenDate = x.ReopenDate;
                    o.CloseDate = x.CloseDate;
                }
                else if (x.StartDate != null && x.EndDate != null)
                    entity.SessionPhases.Add(x);
            });
        }
        #endregion
    }
}
