using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    public interface IApplicationBudgetRepository : IGenericRepository<ApplicationBudget>
    {
        ApplicationBudget Add(int applicationId, decimal? totalFunding, decimal? directCosts, decimal? indirectCosts, int userId);

        void Update(ApplicationBudget applicationBudget, decimal? totalFunding, decimal? directCosts, decimal? indirectCosts, int userId);
    }

    public class ApplicationBudgetRepository : GenericRepository<ApplicationBudget>, IApplicationBudgetRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ApplicationBudgetRepository(P2RMISNETEntities context) : base(context)
        {
        }
        #endregion 

        public ApplicationBudget Add(int applicationId, decimal? totalFunding, decimal? directCosts, decimal? indirectCosts, int userId)
        {
            var o = new ApplicationBudget();
            o.ApplicationId = applicationId;
            o.TotalFunding = totalFunding;
            o.DirectCosts = directCosts;
            o.IndirectCosts = indirectCosts;

            Helper.UpdateCreatedFields(o, userId);
            Helper.UpdateModifiedFields(o, userId);
            Add(o);

            return o;
        }

        public void Update(ApplicationBudget applicationBudget, decimal? totalFunding, decimal? directCosts, decimal? indirectCosts, int userId)
        {
            if (applicationBudget.TotalFunding != totalFunding || applicationBudget.DirectCosts != directCosts ||
                applicationBudget.IndirectCosts != indirectCosts)
            {
                applicationBudget.TotalFunding = totalFunding;
                applicationBudget.DirectCosts = directCosts;
                applicationBudget.IndirectCosts = indirectCosts;
                Helper.UpdateModifiedFields(applicationBudget, userId);
            }
        }
    }
}
