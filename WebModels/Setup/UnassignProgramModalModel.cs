using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.Setup
{
    #region IUnassignProgramModalModel
    /// <summary>
    /// Models all of a meeting's assigned programs
    /// </summary>
    public interface IUnassignProgramModalModel
    {
        string ClientAbrv { get; set; }
        string MeetingAbbr { get; set; }
        string MeetingName { get; set; }

        List<IUnassignProgramListModel> AssignPrograms { get; set; }
    }
    /// <summary>
    /// Models all of a meeting's assigned programs
    /// </summary>
    public class UnassignProgramModalModel : IUnassignProgramModalModel
    {
        public UnassignProgramModalModel(string clientAbbreviation, string meetingAbbr, string meetingName, List<IUnassignProgramListModel> list )
        {
            this.ClientAbrv = clientAbbreviation;
            this.MeetingAbbr = meetingAbbr;
            this.MeetingName = meetingName;
            this.AssignPrograms = list;
        }
        public string ClientAbrv { get; set; }
        public string MeetingAbbr { get; set; }
        public string MeetingName { get; set; }
        public List<IUnassignProgramListModel> AssignPrograms { get; set; }
    }
    #endregion
    #region UnassignProgramListModel
    /// <summary>
    /// Models a single meeting's assigned program.
    /// </summary>
    public interface IUnassignProgramListModel
    {
        #region Attributes
        string ProgramAbbreviation { get; set; }
        string Year { get; set; }
        bool IsPanelAssigned { get; set; }
        #endregion
        #region Indexes
        int ProgramMeetingId { get; set; }
        int ClientMeetingId { get; set; }
        int ClientProgramId { get; set; }
        int ProgramYearId { get; set; }
        #endregion
    }
    /// <summary>
    /// Models a meeting's assigned program.
    /// </summary>
    public class UnassignProgramListModel : IUnassignProgramListModel
    {
        #region Attributes
        /// <summary>
        /// Program abbreviation
        /// </summary>
        public string ProgramAbbreviation { get; set; }
        /// <summary>
        /// Program year
        /// </summary>
        public string Year { get; set; }
        /// <summary>
        /// Indicates of one or more panels have been assigned to the program.
        /// </summary>
        public bool IsPanelAssigned { get; set; }
        #endregion
        #region Indexes
        /// <summary>
        /// ProgramMeeting entity identifier
        /// </summary>
        public int ProgramMeetingId { get; set; }
        /// <summary>
        /// ClientMeeting entity identifier
        /// </summary>
        public int ClientMeetingId { get; set; }
        /// <summary>
        /// ClientProgram entity identifier
        /// </summary>
        public int ClientProgramId { get; set; }
        /// <summary>
        /// ProgramYear entity identifier
        /// </summary>
        public int ProgramYearId { get; set; }
        #endregion
    }
    #endregion
}
