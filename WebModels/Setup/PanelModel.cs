using Sra.P2rmis.WebModels.Lists;
using System;
using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.Setup
{
    #region Base Panel Model
    /// <summary>
    /// Base data model for the Add & Modify Panel modals
    /// </summary>
    public interface IBasePanelModel
    {
        string ClientAbrv { get; set; }
        string MeetingDescription { get; set; }
        string SessionAbbreviation { get; set; }
        Nullable<DateTime> StartDate { get; set; }
        Nullable<DateTime> EndDate { get; set; }
        int MeetingSessionId { get; set; }
    }
    /// <summary>
    /// Base data model for the Add & Modify Panel modals
    /// </summary>
    public class BasePanelModel: IBasePanelModel
    {
        #region Attributes
        /// <summary>
        /// Client abbreviation
        /// </summary>
        public string ClientAbrv { get; set; }
        /// <summary>
        /// Meeting description
        /// </summary>
        public string MeetingDescription { get; set; }
        /// <summary>
        /// Session abbreviation
        /// </summary>
        public string SessionAbbreviation { get; set; }
        /// <summary>
        /// Session start date
        /// </summary>
        public Nullable<DateTime> StartDate { get; set; }
        /// <summary>
        /// Session end date
        /// </summary>
        public Nullable<DateTime> EndDate { get; set; } 
        #endregion
        #region Indexes
        public int MeetingSessionId { get; set; }
        #endregion
    }
    #endregion
    #region Add Panel Model
    /// <summary>
    /// Data model for the Add Panel modal
    /// </summary>
    public interface IAddPanelModel: IBasePanelModel
    {
        IEnumerable<IGenericListEntry<string, string>> Panels { get; set; }
        IEnumerable<IGenericListEntry<int, string>> Programs { get; set; }
        int ClientMeetingId { get; set; }
    }
    /// <summary>
    /// Data model for the Add Panel modal
    /// </summary>
    public class AddPanelModel : BasePanelModel, IAddPanelModel
    {
        /// <summary>
        /// List of the Session's panels
        /// </summary>
        public IEnumerable<IGenericListEntry<string, string>> Panels { get; set; }
        /// <summary>
        /// List of Programs assigned to the Session
        /// </summary>
        public IEnumerable<IGenericListEntry<int, string>> Programs { get; set; }
        /// <summary>
        /// Gets or sets the client meeting identifier.
        /// </summary>
        /// <value>
        /// The client meeting identifier.
        /// </value>
        public int ClientMeetingId { get; set; }
    }
    #endregion
    #region Update Panel Model
    /// <summary>
    /// Data model for the Update Panel modal
    /// </summary>
    public interface IUpdatePanelModel : IBasePanelModel
    {
        string PanelAbbreviation { get; set; }
        string PanelName { get; set; }
        int SessionPanelId { get; set; }
        IEnumerable<IGenericListEntry<int, string>> Sessions { get; set; }
        bool AreApplicationsReleased { get; set; }
        bool AreApplicationsAssigned { get; set; }
        bool AreUsersAssigned { get; set; }
        /// <summary>
        /// Program abbreviation
        /// </summary>
        string ProgramAbbr { get; set; }
        /// <summary>
        /// Year
        /// </summary>
        string Year { get; set; }
    }
    /// <summary>
    /// Data model for the Update Panel modal
    /// </summary>
    public class UpdatePanelModel : BasePanelModel, IUpdatePanelModel
    {
        #region Attributes
        /// <summary>
        /// Current Panel abbreviation
        /// </summary>
        public string PanelAbbreviation { get; set; }
        /// <summary>
        /// Current Panel name
        /// </summary>
        public string PanelName { get; set; }
        /// <summary>
        /// List of Meeting Sessions (for move)
        /// </summary>
        public IEnumerable<IGenericListEntry<int, string>> Sessions { get; set; }
        /// <summary>
        /// Indicates if applications have been released to reviewers.  I believe this property can be removed.
        /// </summary>
        public bool AreApplicationsReleased { get; set; }
        /// <summary>
        /// Indicates if applications have been assigned to this panel
        /// </summary>
        public bool AreApplicationsAssigned { get; set; }
        /// <summary>
        /// Indicates if users (reviewers) have been assigned to this panel
        /// </summary>
        public bool AreUsersAssigned { get; set; }
        /// <summary>
        /// Program abbreviation
        /// </summary>
        public string ProgramAbbr { get; set; }
        /// <summary>
        /// Year
        /// </summary>
        public string Year { get; set; }
        #endregion
        #region Indexes
        /// <summary>
        /// SessionPanel entity identifier
        /// </summary>
        public int SessionPanelId { get; set; }
        #endregion
    }
    #endregion

}
