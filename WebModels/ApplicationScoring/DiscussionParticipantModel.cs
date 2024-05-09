namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Model representing a participant in a discussion
    /// </summary>
    public interface IDiscussionParticipantModel
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        string LastName { get; set; }

        /// <summary>
        /// Gets or sets the type of the participant.
        /// </summary>
        /// <value>
        /// The type of the participant.
        /// </value>
        string ParticipantType { get; set; }

        /// <summary>
        /// Gets or sets the assignment order.
        /// </summary>
        /// <value>
        /// The assignment order.
        /// </value>
        int? AssignmentOrder { get; set; }

        /// <summary>
        /// Gets or sets the participant role.
        /// </summary>
        /// <value>
        /// The participant role.
        /// </value>
        string ParticipantRole { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is moderator.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is moderator; otherwise, <c>false</c>.
        /// </value>
        bool IsModerator { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance represents a chairperson.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is a chairperson; otherwise, <c>false</c>.
        /// </value>
        bool IsChair { get; }
    }

    /// <summary>
    /// Model representing a participant in a discussion
    /// </summary>
    public class DiscussionParticipantModel : IDiscussionParticipantModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscussionParticipantModel"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="participantType">Type of the participant.</param>
        /// <param name="assignmentOrder">The assignment order.</param>
        /// <param name="participantRole">The participant role.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="isModerator">if set to <c>true</c> [is moderator].</param>
        public DiscussionParticipantModel(string firstName, string lastName, string participantType, int? assignmentOrder, string participantRole, string phoneNumber, bool isModerator, bool isChair)
        {
            FirstName = firstName;
            LastName = lastName;
            ParticipantType = participantType;
            AssignmentOrder = assignmentOrder;
            ParticipantRole = participantRole;
            PhoneNumber = phoneNumber;
            IsModerator = isModerator;
            IsChair = isChair;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the type of the participant.
        /// </summary>
        /// <value>
        /// The type of the participant.
        /// </value>
        public string ParticipantType { get; set; }

        /// <summary>
        /// Gets or sets the assignment order.
        /// </summary>
        /// <value>
        /// The assignment order.
        /// </value>
        public int? AssignmentOrder { get; set; }

        /// <summary>
        /// Gets or sets the participant role.
        /// </summary>
        /// <value>
        /// The participant role.
        /// </value>
        public string ParticipantRole { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is moderator.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is moderator; otherwise, <c>false</c>.
        /// </value>
        public bool IsModerator { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance represents a chairperson.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is a chairperson; otherwise, <c>false</c>.
        /// </value>
        public bool IsChair { get; private set; }
        #endregion
    }
}
