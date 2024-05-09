namespace Sra.P2rmis.WebModels.ApplicationScoring
{
    /// <summary>
    /// Model representing reviewer information
    /// </summary>
    public interface IReviewerModel
    {
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        int PanelUserAssignmentId { get; set; }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        int SortOrder { get; set; }

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
        /// Gets or sets the assignment type abbreviation.
        /// </summary>
        /// <value>
        /// The assignment type abbreviation.
        /// </value>
        string AssignmentTypeAbbreviation { get; set; }

        /// <summary>
        /// Gets or sets the participant role.
        /// </summary>
        /// <value>
        /// The participant role.
        /// </value>
        string ParticipantRole { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        int UserId { get; set; }
    }

    /// <summary>
    /// Model representing reviewer information
    /// </summary>
    public class ReviewerModel : IReviewerModel
    {
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        /// <value>
        /// The sort order.
        /// </value>
        public int SortOrder { get; set; }
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
        /// Gets or sets the assignment type abbreviation.
        /// </summary>
        /// <value>
        /// The assignment type abbreviation.
        /// </value>
        public string AssignmentTypeAbbreviation { get; set; }
        /// <summary>
        /// Gets or sets the participant role.
        /// </summary>
        /// <value>
        /// The participant role.
        /// </value>
        public string ParticipantRole { get; set; }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }
    }
}
