using System;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ClientRegistrationDocument object. 
    /// </summary>	
    public partial class ClientRegistrationDocument
    {
        /// <summary>
        /// Document route values
        /// </summary>
        public class DocumentRoutes
        {
            public const string Acknowledgement = "Acknowledgement";
            public const string AcknowledgementCprit = "AcknowledgementCprit";
            public const string BiasCoi = "BiasCoi";
            public const string BiasCoiCprit = "BiasCoiCprit";
            public const string Contract = "Contract";
            public const string ContractCprit = "ContractCprit";
            public const string EmContact = "EmContact";
        }
        /// <summary>
        /// Indicates if this document is a contract
        /// </summary>
        /// <returns>True if the document is a contract; false otherwise</returns>
        public bool IsContract()
        {
            return (this.RegistrationDocumentTypeId == RegistrationDocumentType.Indexes.ContractualAgreement);
        }
    }
}
