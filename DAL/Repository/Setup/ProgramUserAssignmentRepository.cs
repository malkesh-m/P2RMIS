using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    public interface IProgramUserAssignmentRepository : IGenericRepository<ProgramUserAssignment>
    {
        ProgramUserAssignment AddAssignment(int userId, int programYearId,
            int? clientMeetingId, int clientParticipantTypeId, int loggedInUserId);
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
        /// <summary>
        /// Add assignment.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="clientParticipantTypeId">The client participant type identifier.</param>
        /// <param name="loggedInUserId">The logged-in user identifier.</param>
        /// <returns></returns>
        public ProgramUserAssignment AddAssignment(int userId, int programYearId,
            int? clientMeetingId, int clientParticipantTypeId, int loggedInUserId)
        {
            var o = new ProgramUserAssignment();
            o.UserId = userId;
            o.ProgramYearId = programYearId;
            o.ClientMeetingId = clientMeetingId;
            o.ClientParticipantTypeId = clientParticipantTypeId;
            Helper.UpdateCreatedFields(o, loggedInUserId);
            Helper.UpdateModifiedFields(o, loggedInUserId);
            return o;
        }
    }
}
