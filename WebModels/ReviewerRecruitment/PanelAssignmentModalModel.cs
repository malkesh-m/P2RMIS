using System;
using System.Collections.Generic;
using System.Linq;
using Sra.P2rmis.WebModels.Lists;

namespace Sra.P2rmis.WebModels.ReviewerRecruitment
{
    #region Interfaces
    /// <summary>
    /// Model interface for the Panel Assignment model
    /// </summary>
    public interface IPanelAssignmentModalModel : IBuiltModel
    {
        #region Construction & Setup
        /// <summary>
        /// Populates the model with session information.
        /// </summary>
        /// <param name="meetingType">Meeting type</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        void SessionStuff(string meetingType, int sessionPanelId);
        /// <summary>
        /// Initializes the values associated with the header section.
        /// </summary>
        /// <param name="date">DateTime the reviewer was added as a potential reviewer or assigned.</param>
        /// <param name="meetingType">Meeting type</param>
        void SetHeaderStuff(DateTime? date, string meetingType);
        /// <summary>
        /// Initializes the values associated with the Participation drop downs section.
        /// </summary>
        /// <param name="participationRoleId">ClientRole entity identifier</param>
        /// <param name="participationTypeId">ClientParticipantType entity identifier</param>
        void SetParticipationDropdowns(int? participationRoleId, int? participationTypeId);
        /// <summary>
        /// Indicates if the user has been assigned.
        /// </summary>
        /// <param name="isAssigned">Indicates if the user is assigned as a reviewer</param>
        void SetAssignment(bool isAssigned);
        /// <summary>
        /// Initializes the values associated with the Participation method section.
        /// </summary>
        /// <param name="participationMethod">ParticipationMehhod entity identifier</param>
        void SetParticipationMethod(int? participationMethod);
        /// <summary>
        /// Initializes the values associated with the Participation level section.
        /// </summary>
        /// <param name="level">Participation level value</param>
        /// <param name="clientApproved">Indicates if the client approved the reviewer</param>
        void SetParticipationLevelAndClientApproval(string level, bool? clientApproved);
        /// <summary>
        /// Set any unrelated indexes.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <param name="panelUserPotentialAssignmentId">PanelUserPotentialAssignment entity identifier </param>
        void SetIndexesStuff(int sessionPanelId, int userId, int? panelUserPotentialAssignmentId);
        /// <summary>
        /// Sets the PotentialAddedDate
        /// </summary>
        /// <param name="date">DateTime the reviewer was added as a potential reviewer</param>
        void SetPotentialDate(DateTime? date);
        #endregion
        /// <summary>
        /// Meeting type
        /// </summary>
        string MeetingType { get; }
        /// <summary>
        /// Date the reviewer was added as a potential reviewer
        /// </summary>
        DateTime? PotentialAddedDate { get; }
        /// <summary>
        /// Date the reviewer was assigned
        /// </summary>
        DateTime? AssignedDate { get; }
        /// <summary>
        /// ParticipationMethod entity identifier
        /// /// </summary>
        int? ParticipationMethodId { get; }
        /// <summary>
        /// Indicates if the reviewer was assigned
        /// </summary>
        bool IsAssigned { get; }
        /// <summary>
        /// Indicates if the client approved of the reviewer
        /// </summary>
        bool? ClientApproved { get; }
        /// <summary>
        /// ParticipantRole entity identifier
        /// </summary>
        int? ParticipantRoleId { get; }
        /// <summary>
        /// ParticipantType entity identifier
        /// </summary>
        int? ParticipantTypeId { get; }
        /// <summary>
        /// Level
        /// </summary>
        string Level { get; }
        /// <summary>
        /// PanelUserPotentialAssignment entity identifier
        /// </summary>
        int? PanelUserPotentialAssignmentId { get; }
        /// <summary>
        /// PanelUserAssignment entity identifier
        /// </summary>
        int? PanelUserAssignmentId { get; }
        /// <summary>
        /// User entity identifier
        /// </summary>
        int UserId { get; }
        /// <summary>
        /// SessionPanel entity identifier
        /// </summary>
        int SessionPanelId { get; }
        /// <summary>
        /// Participation history
        /// </summary>
        ICollection<IParticipationHistoryModel> ParticipationHistory { get; }
        /// <summary>
        /// Gets the program participation history.
        /// </summary>
        /// <value>
        /// The program participation history.
        /// </value>
        ICollection<IProgramParticipationHistoryModel> ProgramParticipationHistory { get; }
        /// <summary>
        /// Sort the Participation history by some BR or other.
        /// </summary>
        void Sort();
    }
    /// <summary>
    /// Model for a single line of the ParticipationHistory grid
    /// </summary>
    public interface IParticipationHistoryModel
    {
        /// <summary>
        /// Program's client
        /// </summary>
        string ClientName { get; }
        /// <summary>
        /// Fiscal year of program panel
        /// </summary>
        string FiscalYear { get; }
        /// <summary>
        /// Program abbreviation
        /// </summary>
        string ProgramAbbreviation { get; }
        /// <summary>
        /// Panel abbreviation
        /// </summary>
        string PanelAbbreviation { get; set; }
        /// <summary>
        /// Gets the panel identifier.
        /// </summary>
        /// <value>
        /// The panel identifier.
        /// </value>
        int PanelId { get; }
        /// <summary>
        /// Gets the program year identifier.
        /// </summary>
        /// <value>
        /// The program year identifier.
        /// </value>
        int ProgramYearId { get; }
        /// <summary>
        /// Reviewers participation type on the panel
        /// </summary>
        string ParticipationType { get; set; }
        /// <summary>
        /// Reviewers participation on the panel
        /// </summary>
        string Level { get; }
        /// <summary>
        /// Participation role
        /// </summary>
        string ParticipationRole { get; }
        /// <summary>
        /// Meeting type
        /// </summary>
        string MeetingType { get; set; }
        /// <summary>
        /// Panel end date
        /// </summary>
        ///         /// <summary>
        /// Method
        /// </summary>
        string ParticipationMethodLabel { get; set; }
        DateTime? PanelEndDate { get; set; }
        /// <summary>
        /// Indicates if registration is complete
        /// </summary>
        bool IsRegistrationComplete { get; set; }
        /// <summary>
        /// Indicates if the participation history entry was a potential assignment
        /// </summary>
        bool IsPotential { get; }
        /// <summary>
        /// Gets the sro list.
        /// </summary>
        /// <value>
        /// The sro list.
        /// </value>
        List<Tuple<string, string, string>> SroList { get; set; }
    }
    /// <summary>
    /// Program Participation history information
    /// </summary>
    public interface IProgramParticipationHistoryModel
    {
        /// <summary>
        /// Gets the program year identifier.
        /// </summary>
        /// <value>
        /// The program year identifier.
        /// </value>
        int ProgramYearId { get; }

        /// <summary>
        /// Gets the client participant identifier.
        /// </summary>
        /// <value>
        /// The client participant identifier.
        /// </value>
        int ClientParticipantTypeId { get; }
        /// <summary>
        /// Gets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        string Program { get; }
        /// <summary>
        /// Gets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        string FiscalYear { get; }
    }
    #endregion
    #region Web Models
    /// <summary>
    /// Model for the Panel Assignment model
    /// </summary>
    public class PanelAssignmentModalModel : IPanelAssignmentModalModel
    {
        #region Constror & Setup
        public PanelAssignmentModalModel()
        {
            this.ParticipationHistory = new List<IParticipationHistoryModel>();
            this.ProgramParticipationHistory = new List<IProgramParticipationHistoryModel>();
        }
        /// <summary>
        /// Populates the model with session information.
        /// </summary>
        /// <param name="meetingType">Meeting type</param>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        public void SessionStuff(string meetingType, int sessionPanelId)
        {
            this.MeetingType = meetingType;
            this.SessionPanelId = sessionPanelId;
        }
        /// <summary>
        /// Initializes the values associated with the header section.
        /// </summary>
        /// <param name="date">DateTime the reviewer was added as a potential reviewer or assigned.</param>
        /// <param name="meetingType">Meeting type</param>
        public void SetHeaderStuff(DateTime? date, string meetingType)
        {
            //
            // We depends on IsAssigned being set before this method is called
            //
            if (IsAssigned)
            {
                this.AssignedDate = date;
            }
            else
            {
                SetPotentialDate(date);
            }
            this.MeetingType = meetingType;
        }
        /// <summary>
        /// Initializes the values associated with the Participation drop downs section.
        /// </summary>
        /// <param name="participationRoleId">ClientRole entity identifier</param>
        /// <param name="participationTypeId">ClientParticipantType entity identifier</param>
        public void SetParticipationDropdowns(int? participationRoleId, int? participationTypeId)
        {
            this.ParticipantRoleId = participationRoleId;
            this.ParticipantTypeId = participationTypeId;
        }
        /// <summary>
        /// Indicates if the user has been assigned.
        /// </summary>
        /// <param name="isAssigned">Indicates if the user is assigned as a reviewer</param>
        public void SetAssignment(bool isAssigned)
        {
            this.IsAssigned = isAssigned;
        }
        /// <summary>
        /// Initializes the values associated with the Participation method section.
        /// </summary>
        /// <param name="participationMethod">ParticipationMehhod entity identifier</param>
        public void SetParticipationMethod(int? participationMethod)
        {
            this.ParticipationMethodId = participationMethod;
        }
        /// <summary>
        /// Initializes the values associated with the Participation level section.
        /// </summary>
        /// <param name="level">Participation level value</param>
        /// <param name="clientApproved">Indicates if the client approved the reviewer</param>
        public void SetParticipationLevelAndClientApproval(string level, bool? clientApproved)
        {
            this.Level = level;
            this.ClientApproved = clientApproved;
        }
        /// <summary>
        /// Set any unrelated indexes.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <param name="panelUserPotentialAssignmentId">PanelUserPotentialAssignment entity identifier </param>
        public void SetIndexesStuff(int sessionPanelId, int userId, int? panelUserPotentialAssignmentId)
        {
            this.SessionPanelId = sessionPanelId;
            this.UserId = userId;
            //
            //  We interpret the type of entity based on the assignment state.
            // 
            if (this.IsAssigned)
            {
                this.PanelUserAssignmentId = panelUserPotentialAssignmentId;
            }
            else
            {
                this.PanelUserPotentialAssignmentId = panelUserPotentialAssignmentId;
            }
        }
        /// <summary>
        /// Sets the PotentialAddedDate
        /// </summary>
        /// <param name="date">DateTime the reviewer was added as a potential reviewer</param>
        public void SetPotentialDate(DateTime? date)
        {
            this.PotentialAddedDate = date;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Meeting type
        /// </summary>
        public string MeetingType { get; private set; }
        /// <summary>
        /// Date the reviewer was added as a potential reviewer
        /// </summary>
        public DateTime? PotentialAddedDate { get; private set; }
        /// <summary>
        /// Date the reviewer was assigned
        /// </summary>
        public DateTime? AssignedDate { get; private set; }
        /// <summary>
        /// ParticipationMethod entity identifier
        /// /// </summary>
        public int? ParticipationMethodId { get; private set; }
        /// <summary>
        /// Indicates if the reviewer was assigned
        /// </summary>
        public bool IsAssigned { get; private set; }
        /// <summary>
        /// Indicates if the client approved of the reviewer
        /// </summary>
        public bool? ClientApproved { get; private set; }
        /// <summary>
        /// ParticipantRole entity identifier
        /// </summary>
        public int? ParticipantRoleId { get; private set; }
        /// <summary>
        /// ParticipantType entity identifier
        /// </summary>
        public int? ParticipantTypeId { get; private set; }
        /// <summary>
        /// Level
        /// </summary>
        public string Level { get; private set; }
        /// <summary>
        /// PanelUserPotentialAssignment entity identifier
        /// </summary>
        public int? PanelUserPotentialAssignmentId { get; private set; }
        /// <summary>
        /// PanelUserAssignment entity identifier
        /// </summary>
        public int? PanelUserAssignmentId { get; private set; }
        /// <summary>
        /// User entity identifier
        /// </summary>
        public int UserId { get; private set; }
        /// <summary>
        /// SessionPanel entity identifier
        /// </summary>
        public int SessionPanelId { get; private set; }
        /// <summary>
        /// Participation history
        /// </summary>
        public ICollection<IParticipationHistoryModel> ParticipationHistory { get; private set; }
        /// <summary>
        /// Gets the program participation history.
        /// </summary>
        public ICollection<IProgramParticipationHistoryModel> ProgramParticipationHistory { get; private set; }
        #endregion
        #region Services
        /// <summary>
        /// Sort the history models by some business rule
        /// </summary>
        public void Sort()
        {
            this.ParticipationHistory = this.ParticipationHistory.OrderByDescending(x => x.PanelEndDate).ToList();
        }
        #endregion
    }
    /// <summary>
    /// Model for a single line of the ParticipationHistory grid
    /// </summary>
    public class ParticipationHistoryModel : IParticipationHistoryModel
    {
        #region Constror & Setup
        /// <summary>
        /// Initialize client and program information.
        /// </summary>
        /// <param name="clientName">Client name</param>
        /// <param name="fiscalYear">Fiscal year as YYYY</param>
        /// <param name="programAbbreviation">Program abbreviation</param>
        /// <param name="panelAbbreviation">Panel abbreviation</param>
        public void SetClientStuff(string clientName, string fiscalYear, string programAbbreviation, string panelAbbreviation, int panelId, int programYearId)
        {
            this.ClientName = clientName;
            this.FiscalYear = fiscalYear;
            this.ProgramAbbreviation = programAbbreviation;
            this.PanelAbbreviation = panelAbbreviation;
            this.PanelId = panelId;
            this.ProgramYearId = programYearId;
        }
        /// <summary>
        /// Initialize meeting information.
        /// </summary>
        /// <param name="meetingType">Meeting type</param>
        /// <param name="panelEndDate">Panel end data</param>
        public void SetMeetingStuff(string meetingType, DateTime? panelEndDate)
        {
            this.MeetingType = meetingType;
            this.PanelEndDate = panelEndDate;

        }
        /// <summary>
        /// Initialize reviewer information.
        /// </summary>
        /// <param name="participationRole">Reviewer participation role</param>
        /// <param name="participationType">Reviewer participation type</param>
        /// <param name="level">Reviewer participation level (partial or full)</param>
        /// <param name="isRegistrationComplete">Indicates if the users registration is complete</param>
        /// <param name="method">Method (remote or )</param>
        /// <param name="isPotential">Indicates if the history entry is from a potential assignment</param>
        public void SetReviewerStuff(string participationRole, string participationType, string level, bool isRegistrationComplete, string method, bool isPotential)
        {
            this.ParticipationRole = participationRole;
            this.ParticipationType = participationType;
            this.Level = level;
            this.IsRegistrationComplete = isRegistrationComplete;
            this.ParticipationMethodLabel = method;
            this.IsPotential = isPotential;
        }
        /// <summary>
        /// Sets the sro list.
        /// </summary>
        /// <param name="sroList">The sro list.</param>
        public void SetSroList(List<Tuple<string, string, string>> sroList)
        {
            this.SroList = sroList;
        }
        /// <summary>
        /// Sets the program history.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientParticipantTypeId">The client participant type identifier.</param>
        public void SetProgramHistory(int programYearId, int clientParticipantTypeId)
        {
            this.ProgramYearId = programYearId;
            this.ClientParticipantTypeId = clientParticipantTypeId;
        }
        #endregion
        #region Attributes
        public int ClientParticipantTypeId { get; private set; }
        /// <summary>
        /// Program's client
        /// </summary>
        public string ClientName { get; private set; }
        /// <summary>
        /// Fiscal year of program panel
        /// </summary>
        public string FiscalYear { get; private set; }
        /// <summary>
        /// Program abbreviation
        /// </summary>
        public string ProgramAbbreviation { get; private set; }
        /// <summary>
        /// Panel abbreviation
        /// </summary>
        public string PanelAbbreviation { get; set; }
        /// <summary>
        /// Gets the panel identifier.
        /// </summary>
        /// <value>
        /// The panel identifier.
        /// </value>
        public int PanelId { get; private set; }
        /// <summary>
        /// Gets the program year identifier.
        /// </summary>
        /// <value>
        /// The program year identifier.
        /// </value>
        public int ProgramYearId { get; private set; }
        /// <summary>
        /// Reviewers participation type on the panel
        /// </summary>
        public string ParticipationType { get; set; }
        /// <summary>
        /// Reviewers participation on the panel
        /// </summary>
        public string Level { get; private set; }
        /// <summary>
        /// Participation role
        /// </summary>
        public string ParticipationRole { get; private set; }
        /// <summary>
        /// Meeting type
        /// </summary>
        public string MeetingType { get; set; }
        /// <summary>
        /// Method
        /// </summary>
        public string ParticipationMethodLabel { get; set; }
        /// <summary>
        /// Panel end date
        /// </summary>
        public DateTime? PanelEndDate { get; set; }
        /// <summary>
        /// Indicates if registration is complete
        /// </summary>
        public bool IsRegistrationComplete { get; set; }
        /// <summary>
        /// Indicates if the participation history entry was a potential assignment
        /// </summary>
        public bool IsPotential { get; private set; }
        /// <summary>
        /// Gets the sro list.
        /// </summary>
        /// <value>
        /// The sro list.
        /// </value>
        public List<Tuple<string, string, string>> SroList { get; set; }
        #endregion
    }

    #endregion
    public class ProgramUserAssignmentHistory : IProgramParticipationHistoryModel
    {
        #region Constror & Setup
        public ProgramUserAssignmentHistory()
        {
        }
        /// <summary>
        /// Gets the program year identifier.
        /// </summary>
        /// <value>
        /// The program year identifier.
        /// </value>
        public int ProgramYearId { get; private set; }
        /// <summary>
        /// Gets the program year identifier.
        /// </summary>
        /// <value>
        /// The program year identifier.
        /// </value>
        public int ClientParticipantTypeId { get; private set; }
        /// <summary>
        /// Gets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        public string Program { get; private set; }
        /// <summary>
        /// Gets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        public string FiscalYear { get; private set; }
        /// <summary>
        /// Sets the program history.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientParticipantTypeId">The client participant type identifier.</param>
        /// <param name="program">The program.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        public void SetProgramHistory(int programYearId, int clientParticipantTypeId)
        {
            this.ProgramYearId = programYearId;
            this.ClientParticipantTypeId = clientParticipantTypeId;
        }
        public void SetProgramYear(string program, string fiscalYear)
        {
            this.Program = program;
            this.FiscalYear = fiscalYear;
        }
    }
}
#endregion