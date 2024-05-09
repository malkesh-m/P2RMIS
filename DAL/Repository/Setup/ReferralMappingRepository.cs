using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Sra.P2rmis.WebModels.Setup;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    public interface IReferralMappingRepository : IGenericRepository<ReferralMapping>
    {
        /// <summary>
        /// Deletes the referral mapping.
        /// </summary>
        /// <param name="referralMappingId">The referral mapping identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void DeleteReferralMapping(int referralMappingId, int userId);
        /// <summary>
        /// Gets the referral mapping.
        /// </summary>
        /// <param name="referralMappingId">The referral mapping identifier.</param>
        /// <returns></returns>
        ReferralMapping GetReferralMapping(int referralMappingId);
        /// <summary>
        /// Add referral mapping
        /// </summary>
        /// <param name="referralMapping"></param>
        /// <returns></returns>
        int AddReferralMapping(ReferralMapping referralMapping);
        /// <summary>
        /// Gets the referral mapping data model list.
        /// </summary>
        /// <param name="referrals">The referrals.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        IEnumerable<ReferralMappingDataModel> GetReferralMappingDataByReferrals(List<KeyValuePair<string, string>> referrals, int programYearId, int receiptCycle);
        /// <summary>
        /// Gets the referral mapping data by identifier.
        /// </summary>
        /// <param name="referralMappingId">The referral mapping identifier.</param>
        /// <returns></returns>
        IEnumerable<ReferralMappingDataModel> GetReferralMappingDataById(int referralMappingId);
    }
    public class ReferralMappingRepository : GenericRepository<ReferralMapping>, IReferralMappingRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ReferralMappingRepository(P2RMISNETEntities context) : base(context)
        {
        }
        #endregion        
        /// <summary>
        /// Deletes the referral mapping.
        /// </summary>
        /// <param name="referralMappingId">The referral mapping identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void DeleteReferralMapping(int referralMappingId, int userId)
        {
            var o = context.ReferralMappings.Where(x => x.ReferralMappingId == referralMappingId).FirstOrDefault();
            Helper.UpdateDeletedFields(o, userId);
            Delete(o);
        }
        /// <summary>
        /// Gets the referral mapping.
        /// </summary>
        /// <param name="referralMappingId">The referral mapping identifier.</param>
        /// <returns></returns>
        /// <remarks>Includes ReferralMappingData</remarks>
        public ReferralMapping GetReferralMapping(int referralMappingId)
        {
            var o = context.ReferralMappings
                .Include(z => z.ReferralMappingDatas)
                .Where(x => x.ReferralMappingId == referralMappingId).FirstOrDefault();
            return o;
        }
        /// <summary>
        /// Add referral mapping
        /// </summary>
        /// <param name="referralMapping"></param>
        /// <returns>id</returns>
        public int AddReferralMapping(ReferralMapping referralMapping)
        {
           
            var referral =  context.ReferralMappings.Add(referralMapping);
            return referral.ReferralMappingId;
        }
        /// <summary>
        /// Gets the referral mapping data model list.
        /// </summary>
        /// <param name="referrals">The referrals.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <returns></returns>
        public IEnumerable<ReferralMappingDataModel> GetReferralMappingDataByReferrals(List<KeyValuePair<string, string>> referrals, int programYearId, int receiptCycle)
        {
            return RepositoryHelpers.GetReferralMappingDataByReferrals(context, referrals, programYearId, receiptCycle);
        }
        /// <summary>
        /// Gets the referral mapping data by identifier.
        /// </summary>
        /// <param name="referralMappingId">The referral mapping identifier.</param>
        /// <returns></returns>
        public IEnumerable<ReferralMappingDataModel> GetReferralMappingDataById(int referralMappingId)
        {
            return RepositoryHelpers.GetReferralMappingDataById(context, referralMappingId);
        }
    }
}
