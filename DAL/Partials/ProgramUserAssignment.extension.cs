using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.WebModels.ReviewerRecruitment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Custom methods for Entity Framework's ProgramUserAssignment object. 
    /// </summary>
    public partial class ProgramUserAssignment : IStandardDateFields
    {

        public void Populate(int programYearId, int userId, int clientParticipantId)
        {
            ProgramYearId = programYearId;
            UserId = userId;
            ClientParticipantTypeId = clientParticipantId;
        }
    }
}
