using Sra.P2rmis.WebModels.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    public interface IReferralMappingDataRepository : IGenericRepository<ReferralMappingData>
    {
        /// <summary>
        /// Deletes the referral mapping data.
        /// </summary>
        /// <param name="referralMappingId">The referral mapping identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void DeleteReferralMappingData(int referralMappingId, int userId);
        /// <summary>
        /// Get referral mapping data
        /// </summary>
        /// <returns></returns>
        List<ReferralMappingModel> GetReferralMappingModels(int id);
    }
    public class ReferralMappingDataRepository : GenericRepository<ReferralMappingData>, IReferralMappingDataRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ReferralMappingDataRepository(P2RMISNETEntities context) : base(context)
        {
        }
        #endregion        
        /// <summary>
        /// Deletes the referral mapping data.
        /// </summary>
        /// <param name="referralMappingId">The referral mapping identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteReferralMappingData(int referralMappingId, int userId)
        {
            var os = context.ReferralMappingDatas.Where(x => x.ReferralMappingId == referralMappingId);
            foreach (var o in os)
            {
                Helper.UpdateDeletedFields(o, userId);
                Delete(o);
            }
        }
        /// <summary>
        /// Get Referral mapping 
        /// </summary>
        /// <returns></returns>
        public List<ReferralMappingModel> GetReferralMappingModels(int id)
        {
            return RepositoryHelpers.GetReferralMapping(context, id).ToList();
        }
    }
}
