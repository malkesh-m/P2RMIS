using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    public interface IAirportRepository : IGenericRepository<Airport>
    {
    }

    public class AirportRepository : GenericRepository<Airport>, IAirportRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public AirportRepository(P2RMISNETEntities context) : base(context)
        {
        }
        #endregion 
    }
}
