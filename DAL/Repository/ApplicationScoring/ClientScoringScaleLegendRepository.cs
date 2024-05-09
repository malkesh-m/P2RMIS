using System;
using Sra.P2rmis.Dal.Common;

namespace Sra.P2rmis.Dal.Repository
{
    /// <summary>
    /// Repository for ClientScoringScaleLegendRepository objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>
    public interface IClientScoringScaleLegendRepository : IGenericRepository<ClientScoringScaleLegend>
    {
    }
    /// <summary>
    /// Repository for lientScoringScaleLegendRepository objects.  Provides CRUD methods and 
    /// associated database services.
    /// </summary>   
    public class ClientScoringScaleLegendRepository : GenericRepository<ClientScoringScaleLegend>, IClientScoringScaleLegendRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ClientScoringScaleLegendRepository(P2RMISNETEntities context)
            : base(context)
        {     
             
        }
        #endregion
        #region Services Provided

        #endregion
        #region Services Not Provided
        /// <summary>
        /// Delete an object
        /// </summary>
        /// <param name="id"></param>
        public override void Delete(object id)
        {
            
            string message = string.Format(Constants.NotSupportedMessage, "Delete(id)");
            throw new NotSupportedException(message);
        }

        public override void Delete(ClientScoringScaleLegend entityToDelete)
        {
            string message = string.Format(Constants.NotSupportedMessage, "Delete(object)");
            throw new NotSupportedException(message);
        }

        #endregion
    }
}
