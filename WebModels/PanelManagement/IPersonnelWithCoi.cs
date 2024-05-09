namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for personnel with COI
    /// </summary>
    public interface IPersonnelWithCoi
    {
        /// <summary>
        /// The first name of the personnel
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        /// The last name of the personnel
        /// </summary>
        string LastName { get; set; }
        /// <summary>
        /// The organization
        /// </summary>
        string Organization { get; set; }
        /// <summary>
        /// The type of the Conflict of Interest
        /// </summary>
        string CoiType { get; set; }
        /// <summary>
        /// The source of the Conflict of Interest
        /// </summary>
        string CoiSource { get; set; }
    }
}
