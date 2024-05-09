using System.Linq;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's PanelUserRegistration object. 
    /// </summary>
    public partial class PanelUserRegistration : IStandardDateFields
    {
        /// <summary>
        /// Indicates if all documents associated with this registration are signed.
        /// </summary>
        /// <returns>True if all documents are signed; false otherwise.</returns>
        public bool AreAllDocumentsSigned()
        {
            return (this.PanelUserRegistrationDocuments.Where(x => x.DateSigned != null).Count() == this.PanelUserRegistrationDocuments.Count());
        }
        /// <summary>
        /// Returns an indication if a fee is accepted for this panel registration.
        /// </summary>
        /// <returns>Payment category value (true/false) as string</returns>
        public string PaymentCategory()
        {
            //
            // Then retrieve all PanelUserRegistrationDocumentItems over all documents
            //
            var feeAccepted = this.PanelUserRegistrationDocuments.SelectMany(x => x.PanelUserRegistrationDocumentItems). 
                //
                // Now search over these for the one item that indicates if they will receive payment
                //
                Where(x => x.RegistrationDocumentItemId == RegistrationDocumentItem.Indexes.ConsultantFeeAccepted).FirstOrDefault();
            //
            // If one is there (and it should be) but one never knows
            // return the value.
            //
            return (feeAccepted == null) ? string.Empty : feeAccepted.Value;
        }
    }
}
