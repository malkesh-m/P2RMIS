using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    public interface IClientApplicationPersonnelTypeRepository : IGenericRepository<ClientApplicationPersonnelType>
    {
        ClientApplicationPersonnelType GetByApplicationPersonnelType(int clientId, string applicationPersonnelType);

        ClientApplicationPersonnelType GetByExternalPersonnelTypeId(int clientId, int? externalPersonnelTypeId);
    }

    public class ClientApplicationPersonnelTypeRepository : GenericRepository<ClientApplicationPersonnelType>, IClientApplicationPersonnelTypeRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ClientApplicationPersonnelTypeRepository(P2RMISNETEntities context) : base(context)
        {
        }
        #endregion 

        public ClientApplicationPersonnelType GetByApplicationPersonnelType(int clientId, string applicationPersonnelType)
        {
            var o = Get(x => x.ClientId == clientId && x.ApplicationPersonnelType == applicationPersonnelType).FirstOrDefault();
            return o;
        }

        public ClientApplicationPersonnelType GetByExternalPersonnelTypeId(int clientId, int? externalPersonnelTypeId)
        {
            var o = Get(x => x.ExternalPersonnelTypeId == externalPersonnelTypeId && x.ClientId == clientId).FirstOrDefault();
            return o;
        }
    }
}
