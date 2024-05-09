
namespace Sra.P2rmis.WebModels.UserProfileManagement
{
    /// <summary>
    /// Professional Affiliation 
    /// </summary>
    public interface IProfessionalAffiliationModel
    {
        /// <summary>
        /// Type of professional affiliation
        /// </summary>
        int? ProfessionalAffiliationId { get; set; }
        /// <summary>
        /// institution name
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Department
        /// </summary>
        string Department { get; set; }
        /// <summary>
        /// Position
        /// </summary>
        string Position { get; set; }
    }
}
