using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    public interface IApplicationComplianceRepository : IGenericRepository<ApplicationCompliance>
    {
        ApplicationCompliance Add(int applicationId, int complianceStatusId, int userId);

        void Update(ApplicationCompliance applicationCompliance, int complianceStatusId, int userId);
    }

    public class ApplicationComplianceRepository : GenericRepository<ApplicationCompliance>, IApplicationComplianceRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ApplicationComplianceRepository(P2RMISNETEntities context) : base(context)
        {
        }
        #endregion 

        public ApplicationCompliance Add(int applicationId, int complianceStatusId, int userId)
        {
            var o = new ApplicationCompliance();
            o.ApplicationId = applicationId;
            o.ComplianceStatusId = complianceStatusId;

            Helper.UpdateCreatedFields(o, userId);
            Helper.UpdateModifiedFields(o, userId);
            Add(o);

            return o;
        }

        public void Update(ApplicationCompliance applicationCompliance, int complianceStatusId, int userId)
        {
            if (applicationCompliance.ComplianceStatusId != complianceStatusId)
            {
                applicationCompliance.ComplianceStatusId = complianceStatusId;            
                Helper.UpdateModifiedFields(applicationCompliance, userId);
            }
        }
    }
}
