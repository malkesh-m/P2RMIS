using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    public interface IClientApplicationInfoTypeRepository : IGenericRepository<ClientApplicationInfoType>
    {
        ClientApplicationInfoType GetByDescription(int clientId, string description);
    }

    public class ClientApplicationInfoTypeRepository : GenericRepository<ClientApplicationInfoType>, IClientApplicationInfoTypeRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ClientApplicationInfoTypeRepository(P2RMISNETEntities context) : base(context)
        {
        }
        #endregion 

        public ClientApplicationInfoType GetByDescription(int clientId, string description)
        {
            var o = Get(x => x.ClientId == clientId && x.InfoTypeDescription == description).FirstOrDefault();
            return o;
        }
    }
}
