
namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Partial class for Professional Affiliation lookup table
    /// </summary>
    public partial class ProfessionalAffiliation
    {
        /// <summary>
        /// Specific index values.
        /// </summary>
        public class Indexes
        {
            public const int InstitutionOrganization = 1;
            public const int NominatingOrganization = 2;
            public const int Other = 3;
        }
        /// <summary>
        /// Specific Field Names
        /// </summary>
        public class Fields
        {
            public const string ProfessionalAffiliationId = "ProfessionalAffiliationId";
        }
    }
}
