using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.ConsumerManagement
{
    public interface INomineeProgramRepository : IGenericRepository<NomineeProgram>
    {
        /// <summary>
        /// Add nominee program
        /// </summary>
        /// <param name="nomineeProgram">Nominee program</param>
        /// <param name="userId">User identifier</param>
        /// <returns></returns>
        NomineeProgram Add(NomineeProgram nomineeProgram, int userId);
    }
    public class NomineeProgramRepository : GenericRepository<NomineeProgram>, INomineeProgramRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public NomineeProgramRepository(P2RMISNETEntities context)
            : base(context)
        {
        }
        #endregion

        /// <summary>
        /// Add nominee program
        /// </summary>
        /// <param name="nomineeProgram">Nominee program</param>
        /// <param name="userId">User identifier</param>
        /// <returns></returns>
        public NomineeProgram Add(NomineeProgram nomineeProgram, int userId)
        {
            Helper.UpdateCreatedFields(nomineeProgram, userId);
            Helper.UpdateModifiedFields(nomineeProgram, userId);
            context.NomineePrograms.Add(nomineeProgram);
            return nomineeProgram;
        }
    }
}
