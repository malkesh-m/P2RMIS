using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    public interface IComplianceStatusRepository : IGenericRepository<ComplianceStatu>
    {
        /// <summary>
        /// Gets the by label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns></returns>
        ComplianceStatu GetByNoDashLabel(string label);
    }

    public class ComplianceStatusRepository : GenericRepository<ComplianceStatu>, IComplianceStatusRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ComplianceStatusRepository(P2RMISNETEntities context) : base(context)
        {
        }
        #endregion

        public const string DashChar = "-";
        /// <summary>
        /// Gets the by label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns></returns>
        public ComplianceStatu GetByNoDashLabel(string label)
        {            
            var o = Get(x => x.ComplianceStatusLabel.Replace(DashChar, String.Empty) == label).FirstOrDefault();
            return o;
        }
    }
}
