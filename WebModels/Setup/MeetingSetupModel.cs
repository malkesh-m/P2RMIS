using System;

namespace Sra.P2rmis.WebModels.Setup
{
    #region Meeting Modal base
    /// <summary>
    /// Base Data model returned for the meeting setup models
    /// </summary>
    public interface IMeetingSetupBaseModel
    {
        #region Attributes
        string MeetingAbbreviation { get; set; }
        string MeetingDescription { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        #endregion
        #region Indexes
        int ClientMeetingId { get; set; }
        int ClientId { get; set; }
        #endregion
    }
    /// <summary>
    /// Base Data model returned for the meeting setup models
    /// </summary>
    public class MeetingSetupBaseModel : IMeetingSetupBaseModel
    {
        #region Attributes
        /// <summary>
        /// The meeting abbreviation
        /// </summary>
        public string MeetingAbbreviation { get; set; }
        /// <summary>
        /// Meeting description
        /// </summary>
        public string MeetingDescription { get; set; }
        /// <summary>
        /// Meeting start date
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Meeting end date
        /// </summary>
        public DateTime EndDate { get; set; }
        #endregion
        #region Indexes
        /// <summary>
        /// This meeting's ClientMeeting entity id
        /// </summary>
        public int ClientMeetingId { get; set; }
        /// <summary>
        /// Client entity identifier 
        /// </summary>
        public int ClientId { get; set; }
        #endregion
    }
    #endregion
    #region Meeting Setup Grid
    #region Interface
    /// <summary>
    /// Data model returned for the meeting setup grid
    /// </summary>
    public interface IMeetingSetupModel: IMeetingSetupBaseModel
    {
        #region Attributes
        string HotelName { get; set; }
        string MeetingTypeName { get; set; }
        int ProgramCount { get; set; }
        int SessionCount { get; set; }
        int PanelCount { get; set; }
        bool HasPanelPassed { get; set; }
        Nullable<DateTime> ModifiedDate { get; set; }
        bool IsSessionsAssigned { get; set; }        
        #endregion
    }
    #endregion
    #region class
    /// <summary>
    /// Data model returned for the meeting setup grid
    /// </summary>
    public class MeetingSetupModel : MeetingSetupBaseModel, IMeetingSetupModel
    {
        #region Attributes
        /// <summary>
        /// Hotel where meeting is(was) held
        /// </summary>
        public string HotelName { get; set; }
        /// <summary>
        /// This meeting's type
        /// </summary>
        public string MeetingTypeName { get; set; }
        /// <summary>
        /// Number of programs in this meeting
        /// </summary>
        public int ProgramCount { get; set; }
        /// <summary>
        /// Number of sessions in this meeting
        /// </summary>
        public int SessionCount { get; set; }
        /// <summary>
        /// Number of panels in this meeting
        /// </summary>
        public int PanelCount { get; set; }
        /// <summary>
        /// Convenient method indicating if the meeting end date has passed.
        /// </summary>
        public bool HasPanelPassed { get; set; }
        /// <summary>
        /// DateTime stamp when meeting was last changed
        /// </summary>
        public Nullable<DateTime> ModifiedDate { get; set; }
        /// <summary>
        /// Indicates if sessions have been assigned.
        /// </summary>
        public bool IsSessionsAssigned { get; set; }
        #endregion
    }
    #endregion
    #endregion
    #region Add Meeting Modal
    /// <summary>
    /// Data model returned for the Add/Edit meeting setup models
    /// </summary>
    public interface IMeetingSetupModalModel: IMeetingSetupBaseModel
    {
        #region Attributes
        string ClientAbrv { get; set; }
        /// <summary>
        /// Meeting location
        /// </summary>
        string MeetingLocation { get; set; }
        int MeetingTypeId { get; set; }
        Nullable<int> HotelId { get; set; }
        Nullable<DateTime> CreatedDate { get; set; }
        bool IsOnSite { get; set; }
        /// <summary>
        /// Whether the meeting contains sessions
        /// </summary>
        bool HasSessions { get; set; }
        #endregion
    }
    /// <summary>
    /// Data model returned for the Add/Edit meeting setup models
    /// </summary>
    public class MeetingSetupModalModel : MeetingSetupBaseModel, IMeetingSetupModalModel
    {
        #region Attributes
        /// <summary>
        /// Client abbreviation.
        /// </summary>
        public string ClientAbrv { get; set; }
        /// <summary>
        /// Meeting location
        /// </summary>
        public string MeetingLocation { get; set; }
        /// <summary>
        /// MeetingType entity identifier
        /// </summary>
        public int MeetingTypeId { get; set; }
        /// <summary>
        /// Hotel entity identifier
        /// </summary>
        public Nullable<int> HotelId { get; set; }
        /// <summary>
        /// Changed Datetime stamp when entity was retrieved 
        /// </summary>
        public Nullable<DateTime> CreatedDate { get; set; }
        /// <summary>
        /// Indicates if the meeting is on site.
        /// </summary>
        public bool IsOnSite { get; set; }
        /// <summary>
        /// Whether the meeting contains sessions
        /// </summary>
        public bool HasSessions { get; set; }
        #endregion
    }
    #endregion
}
