
namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for assigned staff
    /// </summary>
    /// <seealso cref="Sra.P2rmis.WebModels.PanelManagement.IAssignedStaff" />
    public class AssignedStaffModel : IAssignedStaffModel
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="firstName">User first name</param>
        /// <param name="lastName">User last name</param>
        /// <param name="role">Role on panel</param>
        public AssignedStaffModel(string firstName, string lastName, string role)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Role = role;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AssignedStaffModel"/> class.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="role">The role.</param>
        /// <param name="email">The email.</param>
        /// <param name="organization">The organization.</param>
        public AssignedStaffModel(string firstName, string lastName, string role, string email, string organization, int panelUserAssignmentId) : this(firstName, lastName, role)
        {
            this.Email = email;
            this.Organization = organization;
            this.PanelUserAssignmentId = panelUserAssignmentId;
        }
        #endregion        
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public int PanelUserAssignmentId { get; set; }
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
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public string Role { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        public string Organization { get; set; }
    }
    /// <summary>
    /// Interface for assigned staff
    /// </summary>
    public interface IAssignedStaffModel
    {
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        int PanelUserAssignmentId { get; set; }
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
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        string Role { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        string Email { get; set; }
        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        string Organization { get; set; }
    }
}
