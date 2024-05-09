using Sra.P2rmis.WebModels.PanelManagement;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Web.ViewModels.PanelManagement
{
    /// <summary>
    /// The view model for displaying staffs
    /// </summary>
    public class StaffViewModel
    {
        public StaffViewModel() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="StaffViewModel"/> class.
        /// </summary>
        /// <param name="staff">The staff.</param>
        public StaffViewModel(IAssignedStaffModel staff)
        {
            PanelUserAssignmentId = staff.PanelUserAssignmentId;
            Name = ViewHelpers.ConstructName(staff.LastName, staff.FirstName);
            Email = staff.Email;
            Organization = staff.Organization;
            Role = staff.Role;
        }
        /// <summary>
        /// Gets or sets the panel user assignment identifier.
        /// </summary>
        /// <value>
        /// The panel user assignment identifier.
        /// </value>
        public int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; private set; }
        /// <summary>
        /// Gets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        public string Organization { get; private set; }
        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public string Role { get; private set; }
    }
}