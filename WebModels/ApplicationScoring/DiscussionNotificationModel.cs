
using System;

namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    public interface IDiscussionNotificationModel
    {
        /// <summary>
        /// Gets or sets the participant email.
        /// </summary>
        /// <value>
        /// The participant email.
        /// </value>
        string ParticipantEmail { get; set; }

        /// <summary>
        /// Gets or sets the participant name prefix.
        /// </summary>
        /// <value>
        /// The participant prefix.
        /// </value>
        string ParticipantPrefix { get; set; }

        /// <summary>
        /// Gets or sets the first name of the participant.
        /// </summary>
        /// <value>
        /// The first name of the participant.
        /// </value>
        string ParticipantFirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the participant.
        /// </summary>
        /// <value>
        /// The last name of the participant.
        /// </value>
        string ParticipantLastName { get; set; }

        /// <summary>
        /// Gets or sets the type of the author participant.
        /// </summary>
        /// <value>
        /// The authors participant type.
        /// </value>
        string AuthorParticipantType { get; set; }

        /// <summary>
        /// Gets or sets the author assignment order.
        /// </summary>
        /// <value>
        /// The author assignment order.
        /// </value>
        int? AuthorAssignmentOrder { get; set; }

        /// <summary>
        /// Gets or sets the author system role.
        /// </summary>
        /// <value>
        /// The author system role.
        /// </value>
        string AuthorSystemRole { get; set; }

        /// <summary>
        /// Gets or sets the author participant role.
        /// </summary>
        /// <value>
        /// The author participant role.
        /// </value>
        string AuthorParticipantRole { get; set; }

        /// <summary>
        /// Gets or sets the log number.
        /// </summary>
        /// <value>
        /// The log number.
        /// </value>
        string LogNumber { get; set; }

        /// <summary>
        /// Gets or sets the application title.
        /// </summary>
        /// <value>
        /// The application title.
        /// </value>
        string ApplicationTitle { get; set; }

        /// <summary>
        /// Gets or sets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        string FiscalYear { get; set; }

        /// <summary>
        /// Gets or sets the program abbreviation.
        /// </summary>
        /// <value>
        /// The program abbreviation.
        /// </value>
        string ProgramAbbreviation { get; set; }

        /// <summary>
        /// Gets or sets the panel abbreviation.
        /// </summary>
        /// <value>
        /// The panel abbreviation.
        /// </value>
        string PanelAbbreviation { get; set; }

        /// <summary>
        /// Gets or sets the phase end date time.
        /// </summary>
        /// <value>
        /// The phase end date time.
        /// </value>
        DateTime? PhaseEndDateTime { get; set; }

        /// <summary>
        /// Gets or sets the comment date time.
        /// </summary>
        /// <value>
        /// The comment date time.
        /// </value>
        DateTime? CommentDateTime { get; set; }
        /// <summary>
        /// for new online Discussion
        /// </summary>
        bool IsNewOnlineDiscussion { get; set; }
        /// <summary>
        /// get re open date
        /// </summary>
        DateTime? PhaseReOpenDatetime { get; set; }
        /// <summary>
        /// close end date if re-open
        /// </summary>
        DateTime? PhaseCloseDatetime { get; set; }
    }

    /// <summary>
    /// Model representing content of a discussion board notification template
    /// </summary>
    public class DiscussionNotificationModel : IDiscussionNotificationModel
    {
        #region Constructor


        #endregion
        #region Properties

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscussionNotificationModel"/> class.
        /// </summary>
        /// <param name="participantEmail">The participant email.</param>
        /// <param name="participantPrefix">The participant prefix.</param>
        /// <param name="participantFirstName">First name of the participant.</param>
        /// <param name="participantLastName">Last name of the participant.</param>
        /// <param name="authorParticipantType">Type of the author participant.</param>
        /// <param name="authorAssignmentOrder">The author assignment order.</param>
        /// <param name="authorSystemRole">The author system role.</param>
        /// <param name="authorParticipantRole">The author participant role.</param>
        /// <param name="logNumber">The log number.</param>
        /// <param name="applicationTitle">The application title.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="programAbbreviation">The program abbreviation.</param>
        /// <param name="panelAbbreviation">The panel abbreviation.</param>
        /// <param name="phaseEndDateTime">The phase end date time.</param>
        /// <param name="commentDateTime">The comment date time.</param>
        public DiscussionNotificationModel(string participantEmail, string participantPrefix, string participantFirstName, string participantLastName, string authorParticipantType, int? authorAssignmentOrder, string authorSystemRole, string authorParticipantRole, string logNumber, string applicationTitle, string fiscalYear, string programAbbreviation, string panelAbbreviation, DateTime phaseEndDateTime, DateTime commentDateTime, bool isNew, DateTime? phaseReOpenDateTime, DateTime? phaseCloseDatetime)
        {
            ParticipantEmail = participantEmail;
            ParticipantPrefix = participantPrefix;
            ParticipantFirstName = participantFirstName;
            ParticipantLastName = participantLastName;
            AuthorParticipantType = authorParticipantType;
            AuthorAssignmentOrder = authorAssignmentOrder;
            AuthorSystemRole = authorSystemRole;
            AuthorParticipantRole = authorParticipantRole;
            LogNumber = logNumber;
            ApplicationTitle = applicationTitle;
            FiscalYear = fiscalYear;
            ProgramAbbreviation = programAbbreviation;
            PanelAbbreviation = panelAbbreviation;
            PhaseEndDateTime = (phaseReOpenDateTime != null) ? phaseCloseDatetime : phaseEndDateTime;
            CommentDateTime = commentDateTime;
            IsNewOnlineDiscussion = isNew;
            PhaseReOpenDatetime = phaseReOpenDateTime;
            PhaseCloseDatetime = phaseCloseDatetime;
        }

        /// <summary>
        /// Gets or sets the participant email.
        /// </summary>
        /// <value>
        /// The participant email.
        /// </value>
        public string ParticipantEmail { get; set; }
        /// <summary>
        /// Gets or sets the participant name prefix.
        /// </summary>
        /// <value>
        /// The participant prefix.
        /// </value>
        public string ParticipantPrefix { get; set; }
        /// <summary>
        /// Gets or sets the first name of the participant.
        /// </summary>
        /// <value>
        /// The first name of the participant.
        /// </value>
        public string ParticipantFirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name of the participant.
        /// </summary>
        /// <value>
        /// The last name of the participant.
        /// </value>
        public string ParticipantLastName { get; set; }
        /// <summary>
        /// Gets or sets the type of the author participant.
        /// </summary>
        /// <value>
        /// The authors participant type.
        /// </value>
        public string AuthorParticipantType { get; set; }
        /// <summary>
        /// Gets or sets the author assignment order.
        /// </summary>
        /// <value>
        /// The author assignment order.
        /// </value>
        public int? AuthorAssignmentOrder { get; set; }
        /// <summary>
        /// Gets or sets the author system role.
        /// </summary>
        /// <value>
        /// The author system role.
        /// </value>
        public string AuthorSystemRole { get; set; }
        /// <summary>
        /// Gets or sets the author participant role.
        /// </summary>
        /// <value>
        /// The author participant role.
        /// </value>
        public string AuthorParticipantRole { get; set; }
        /// <summary>
        /// Gets or sets the log number.
        /// </summary>
        /// <value>
        /// The log number.
        /// </value>
        public string LogNumber { get; set; }
        /// <summary>
        /// Gets or sets the application title.
        /// </summary>
        /// <value>
        /// The application title.
        /// </value>
        public string ApplicationTitle { get; set; }
        /// <summary>
        /// Gets or sets the fiscal year.
        /// </summary>
        /// <value>
        /// The fiscal year.
        /// </value>
        public string FiscalYear { get; set; }
        /// <summary>
        /// Gets or sets the program abbreviation.
        /// </summary>
        /// <value>
        /// The program abbreviation.
        /// </value>
        public string ProgramAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the panel abbreviation.
        /// </summary>
        /// <value>
        /// The panel abbreviation.
        /// </value>
        public string PanelAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the phase end date time.
        /// </summary>
        /// <value>
        /// The phase end date time.
        /// </value>
        public DateTime? PhaseEndDateTime { get; set; }
        /// <summary>
        /// Gets or sets the comment date time.
        /// </summary>
        /// <value>
        /// The comment date time.
        /// </value>
        public DateTime? CommentDateTime { get; set; }
        /// <summary>
        /// for new online discussion
        /// </summary>
        public bool IsNewOnlineDiscussion { get; set; }
        /// <summary>
        /// get re open date
        /// </summary>
        public DateTime? PhaseReOpenDatetime { get; set; }
        public DateTime? PhaseCloseDatetime { get; set; }
        #endregion
    }
}
