namespace Sra.P2rmis.WebModels.PanelManagement
{
    /// <summary>
    /// Data model for personnel with COI
    /// </summary>
    public class PersonnelWithCoi : IPersonnelWithCoi
    {
        /// <summary>
        /// The first name of the personnel
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The last name of the personnel
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// The organization
        /// </summary>
        public string Organization { get; set; }
        /// <summary>
        /// The type of the Conflict of Interest
        /// </summary>
        public string CoiType { get; set; }
        /// <summary>
        /// The source of the Conflict of Interest
        /// </summary>
        public string CoiSource { get; set; }
    }
}
