
namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom properties for Entity Framework's Registration Document Item object. Contains lookup values within RegistrationDocumentItem lookup table.
    /// </summary>
    public partial class RegistrationDocumentItem
    {
        /// <summary>
        /// Lookup values for items
        /// </summary>
        public class Indexes
        {
            public const int FinancialDisclosure = 1;
            public const int FinancialDisclosureDetails = 2;
            public const int AdditionalDisclosure = 3;
            public const int AdditionalDisclosureDetails = 4;
            public const int ConsultantFeeAccepted = 8;
            public const int BusinessCategory = 9;
        }
    }
}
