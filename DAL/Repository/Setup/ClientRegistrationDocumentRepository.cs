using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    public interface IClientRegistrationDocumentRepository : IGenericRepository<ClientRegistrationDocument>
    {
        /// <summary>
        /// Gets the contract document.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        ClientRegistrationDocument GetContractDocument(int clientId);
    }

    public class ClientRegistrationDocumentRepository : GenericRepository<ClientRegistrationDocument>, IClientRegistrationDocumentRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ClientRegistrationDocumentRepository(P2RMISNETEntities context) : base(context)
        {
        }
        /// <summary>
        /// Gets the contract document.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        public ClientRegistrationDocument GetContractDocument(int clientId)
        {
            var doc = context.ClientRegistrations.Where(x => x.ClientId == clientId)
                .SelectMany(y => y.ClientRegistrationDocuments.Where(z => z.RegistrationDocumentTypeId
                == RegistrationDocumentType.Indexes.ContractualAgreement)).FirstOrDefault();
            return doc;
        }
        #endregion
    }
}
