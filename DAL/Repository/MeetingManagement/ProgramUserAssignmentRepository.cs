using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.MeetingManagement
{
    public interface IProgramUserAssignmentRepository : IGenericRepository<ProgramUserAssignment>
    {
    }

    public class ProgramUserAssignmentRepository : GenericRepository<ProgramUserAssignment>, IProgramUserAssignmentRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ProgramUserAssignmentRepository(P2RMISNETEntities context) : base(context) { }
        #endregion
    }
}
