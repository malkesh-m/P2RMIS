using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    public interface IProgramMechanismImportLogRepository : IGenericRepository<ProgramMechanismImportLog>
    {
        /// <summary>
        /// Adds the specified program mechanism identifier.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <param name="importLogId">The import log identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void Add(int programMechanismId, int importLogId, int userId);
    }

    public class ProgramMechanismImportLogRepository : GenericRepository<ProgramMechanismImportLog>, IProgramMechanismImportLogRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ProgramMechanismImportLogRepository(P2RMISNETEntities context)
            : base(context)
        {
        }
        #endregion                
        /// <summary>
        /// Adds the specified program mechanism identifier.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <param name="importLogId">The import log identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void Add(int programMechanismId, int importLogId, int userId)
        {
            var o = new ProgramMechanismImportLog();
            o.ProgramMechanismId = programMechanismId;
            o.ImportLogId = importLogId;
            Helper.UpdateCreatedFields(o, userId);
            Helper.UpdateModifiedFields(o, userId);

            Add(o);
        }
    }
}
