using Sra.P2rmis.Dal;
using System;

namespace Sra.P2rmis.Bll.Setup.Blocks
{
    /// <summary>
    /// Crud block to use for ClientMeeting (Meeting) setup.
    /// </summary>
    internal class MeetingBlock : CrudBlock<ClientMeeting>, ICrudBlock
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="meetingAbbreviation">Meeting abbreviation</param>
        /// <param name="meetingDescription">Meeting description</param>
        /// <param name="startDate">Meeting start date & time</param>
        /// <param name="endDate">Meeting end date & time</param>
        /// <param name="meetingLocation">Meeting location</param>
        /// <param name="meetingTypeId">Selected meeting type entity identifier</param>
        /// <param name="hotelId">Selected hotel entity identifier</param>
        public MeetingBlock(int clientId, string meetingAbbreviation, string meetingDescription, DateTime startDate, DateTime endDate, string meetingLocation, int meetingTypeId, int? hotelId)
        {
            this.ClientId = clientId;
            this.MeetingAbbreviation = meetingAbbreviation;
            this.MeetingDescription = meetingDescription;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.MeetingLocation = meetingLocation;
            this.MeetingTypeId = meetingTypeId;
            this.HotelId = hotelId;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="clientMeetingId">Client meeting identifier</param>
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="clientAbrv">Client abbreviation</param>
        /// <param name="meetingAbbreviation">Meeting abbreviation</param>
        /// <param name="meetingDescription">Meeting description</param>
        /// <param name="startDate">Meeting start date & time</param>
        /// <param name="endDate">Meeting end date & time</param>
        /// <param name="meetingLocation">Meeting location</param>
        /// <param name="meetingTypeId">Selected meeting type entity identifier</param>
        /// <param name="hotelId">Selected hotel entity identifier</param>
        public MeetingBlock(int clientMeetingId, int clientId, string clientAbrv, string meetingAbbreviation, string meetingDescription, DateTime startDate, DateTime endDate, string meetingLocation, int meetingTypeId, int? hotelId) :
            this(clientId, meetingAbbreviation, meetingDescription, startDate, endDate, meetingLocation, meetingTypeId, hotelId)
        {
            this.ClientMeetingId = clientMeetingId;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingBlock"/> class.
        /// </summary>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        public MeetingBlock(int clientMeetingId)
        {
            this.ClientMeetingId = clientMeetingId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// ClientMeeting entity identifier
        /// </summary>
        public Nullable<int> ClientMeetingId { get; private set; }
        /// <summary>
        /// Client entity identifier
        /// </summary>
        public int ClientId { get; private set; }
        /// <summary>
        /// Meeting abbreviation
        /// </summary>
        public string MeetingAbbreviation { get; private set; }
        /// <summary>
        /// Meeting description
        /// </summary>
        public string MeetingDescription { get; private set; }
        /// <summary>
        /// Meeting start date & time
        /// </summary>
        public DateTime StartDate { get; private set; }
        /// <summary>
        /// Meeting end date & time
        /// </summary>
        public DateTime EndDate { get; private set; }
        /// <summary>
        /// Meeting location
        /// </summary>
        public string MeetingLocation { get; private set; }
        /// <summary>
        /// Selected meeting type entity identifier
        /// </summary>
        public int MeetingTypeId { get; private set; }
        /// <summary>
        /// Selected hotel entity identifier
        /// </summary>
        public int? HotelId { get; private set; }
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
        /// Sets individual properties on the CRUD entity.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <param name="entity">ClientMeeting entity to populate</param>
        internal override void Populate(int userId, ClientMeeting entity)
        {
            entity.ClientId = this.ClientId;
            entity.MeetingAbbreviation = this.MeetingAbbreviation;
            entity.MeetingDescription = this.MeetingDescription;
            entity.StartDate = this.StartDate;
            entity.EndDate = this.EndDate;
            entity.MeetingLocation = this.MeetingLocation;
            entity.MeetingTypeId = this.MeetingTypeId;
            entity.HotelId = this.HotelId;
        }
        #endregion
    }
}
