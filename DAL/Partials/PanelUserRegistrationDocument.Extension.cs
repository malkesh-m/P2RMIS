using System;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's PanelUserRegistrationDocument object. 
    /// </summary>
    public partial class PanelUserRegistrationDocument : IStandardDateFields
    {
        /// <summary>
        /// Populate the entity.
        /// </summary>
        /// <param name="dateCompleted">Date the document was completed.  It can be null</param>
        public void Populate(Nullable<DateTime> dateCompleted)
        {
            this.DateCompleted = dateCompleted;
        }
        /// <summary>
        /// Indicates if the PanelUserRegistrationDocument has been signed.
        /// </summary>
        /// <returns>True if the document has been signed; false otherwise</returns>
        public bool IsSigned()
        {
            return (this.DateSigned != null);
        }
        /// <summary>
        /// Creates a clone of the existing entity but without the document
        /// being signed
        /// </summary>
        /// <returns>PanelUserRegistrationDocument cloned but unsigned</returns>
        public PanelUserRegistrationDocument CleanClone(int userId)
        {
            var result = new PanelUserRegistrationDocument();
            result.PanelUserRegistrationId = this.PanelUserRegistrationId;
            result.ClientRegistrationDocumentId = this.ClientRegistrationDocumentId;
            result.PanelUserRegistrationDocumentItems = this.PanelUserRegistrationDocumentItems;
            result.SignedOfflineFlag = this.SignedOfflineFlag;
            return result;
        }
    }
}
