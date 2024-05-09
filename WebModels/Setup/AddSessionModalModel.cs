using Sra.P2rmis.WebModels.Lists;
using System;
using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.Setup
{
    #region Model for phase list
    /// <summary>
    /// Data model for the Session's Phase list
    /// </summary>
    public interface IPhaseModel
    {
        string ReviewPhase { get; set; }
        int StepTypeId { get; set; }
        int ReviewStageId { get; set; }
        Nullable<DateTime> StartDate { get; set; }
        Nullable<DateTime> EndDate { get; set; }
        /// <summary>
        /// Re-open date/time
        /// </summary>
        Nullable<DateTime> ReopenDate { get; set; }
        /// <summary>
        /// Close date/time
        /// </summary>
        Nullable<DateTime> CloseDate { get; set; }
    }
    /// <summary>
    /// Data model for the Session's Phase list
    /// </summary>
    public class PhaseModel: IPhaseModel
    {
        /// <summary>
        /// Review phase name
        /// </summary>
        public string ReviewPhase { get; set; }
        /// <summary>
        /// Gets or sets the review stage identifier.
        /// </summary>
        /// <value>
        /// The review stage identifier.
        /// </value>
        public int ReviewStageId { get; set; }
        /// <summary>
        /// Gets or sets the step type identifier.
        /// </summary>
        /// <value>
        /// The step type identifier.
        /// </value>
        public int StepTypeId { get; set; }
        /// <summary>
        /// Phase start date
        /// </summary>
        public Nullable<DateTime> StartDate { get; set; }
        /// <summary>
        /// Phase end date
        /// </summary>
        public Nullable<DateTime> EndDate { get; set; }
        /// <summary>
        /// Re-open date/time
        /// </summary>
        public Nullable<DateTime> ReopenDate { get; set; }
        /// <summary>
        /// Close date/time
        /// </summary>
        public Nullable<DateTime> CloseDate { get; set; }
    }
    #endregion
    #region Base modal modal
    /// <summary>
    /// Base data model to for the Add Session modal
    /// </summary>
    public interface IBaseSessionModalModel
    {
        /// <summary>
        /// Client identifier
        /// </summary>
        int ClientId { get; set; }
        string ClientAbrv { get; set; }
        string MeetingDescription { get; set; }
        string MeetingTypeName { get; set; }
        int MeetingTypeId { get; set; }
        Nullable<DateTime> MeetingStartDate { get; set; }
        Nullable<DateTime> MeetingEndDate { get; set; }
        IEnumerable<IGenericListEntry<Nullable<int>, IPhaseModel>> PreMeetingPhases { get; set; }
        IEnumerable<IGenericListEntry<Nullable<int>, IPhaseModel>> MeetingPhases { get; set; }
        int ClientMeetingId { get; set; }
    }
    /// <summary>
    /// Base data model to for the Add Session modal
    /// </summary>
    public class BaseSessionModalModel : IBaseSessionModalModel
    {
        /// <summary>
        /// Client identifier
        /// </summary>
        public int ClientId { get; set; }
        /// <summary>
        /// Client abbreviation
        /// </summary>
        public string ClientAbrv { get; set; }
        /// <summary>
        /// Meeting description
        /// </summary>
        public string MeetingDescription { get; set; }
        /// <summary>
        /// Meeting type name
        /// </summary>
        public string MeetingTypeName { get; set; }
        /// <summary>
        /// MeetingType entity identifier
        /// </summary>
        public int MeetingTypeId { get; set; }
        /// <summary>
        /// Meeting start date
        /// </summary>
        public Nullable<DateTime> MeetingStartDate { get; set; }
        /// <summary>
        /// Meeting end date 
        /// </summary>
        public Nullable<DateTime> MeetingEndDate { get; set; }
        /// <summary>
        /// List of pre-meeting phases
        /// </summary>
        public IEnumerable<IGenericListEntry<Nullable<int>, IPhaseModel>> PreMeetingPhases { get; set; } = new List<IGenericListEntry<Nullable<int>, IPhaseModel>>();
        /// <summary>
        /// List of meeting phases
        /// </summary>
        public IEnumerable<IGenericListEntry<Nullable<int>, IPhaseModel>> MeetingPhases { get; set; } = new List<IGenericListEntry<Nullable<int>, IPhaseModel>>();
        /// <summary>
        /// The ClientMeeting the session belongs to
        /// </summary>
        public int ClientMeetingId { get; set; }
    }
    #endregion
    #region AddSessionModalModel
    /// <summary>
    /// Data model populated for the Add Session modal
    /// </summary>
    public interface IAddSessionModalModel : IBaseSessionModalModel
    {
        //
        // Add does not have any other properties than the base
        //
    }
    /// <summary>
    /// Data model populated for the Add Session modal
    /// </summary>
    public class AddSessionModalModel : BaseSessionModalModel, IAddSessionModalModel
    {
        //
        // Add does not have any other properties than the base
        //
    }
    #endregion
    #region ModifySessionModalModel
    /// <summary>
    /// Data model describing the Modify Session modal
    /// </summary>
    public interface IModifySessionModalModel : IBaseSessionModalModel
    {
        int MeetingSessionId { get; set; }
        string SessionAbbreviation { get; set; }
        string SessionDescription { get; set; }
        Nullable<DateTime> SessionStart { get; set; }
        Nullable<DateTime> SessionEnd { get; set; }
        /// <summary>
        /// Whether any application has been released.
        /// </summary>
        bool HasApplicationsBeenReleased { get; set; }
        /// <summary>
        /// Whether the scoring has been set up.
        /// </summary>
        bool HasScoringBeenSetup { get; set; }
    }
    /// <summary>
    /// Data model populated for the Modify Session modal
    /// </summary>
    public class ModifySessionModalModel : BaseSessionModalModel, IModifySessionModalModel
    {
        #region Attributes        
        /// <summary>
        /// Session Abbreviation
        /// </summary>
        public string SessionAbbreviation { get; set; }
        /// <summary>
        /// Session name
        /// </summary>
        public string SessionDescription { get; set; }
        /// <summary>
        /// Session start date
        /// </summary>
        public Nullable<DateTime> SessionStart { get; set; }
        /// <summary>
        /// Session end date
        /// </summary>
        public Nullable<DateTime> SessionEnd { get; set; }
        /// <summary>
        /// MeetingSession entity identifier of the session under modify
        /// </summary>
        public int MeetingSessionId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance has applications been released.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has applications been released; otherwise, <c>false</c>.
        /// </value>
        public bool HasApplicationsBeenReleased { get; set; }
        /// <summary>
        /// Whether the scoring has been set up.
        /// </summary>
        public bool HasScoringBeenSetup { get; set; }
        #endregion
    }
    #endregion

}
