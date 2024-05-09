using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    public interface IApplicationInfoRepository : IGenericRepository<ApplicationInfo>
    {
        ApplicationInfo Add(int applicationId, int clientApplicationInfoTypeId, string infoText, int userId);

        ApplicationInfo GetByClientApplicationInfoTypeId(int clientApplicationInfoTypeId);

        void Update(ApplicationInfo applicationInfo, string infoText, int userId);
    }

    public class ApplicationInfoRepository : GenericRepository<ApplicationInfo>, IApplicationInfoRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ApplicationInfoRepository(P2RMISNETEntities context) : base(context)
        {
        }
        #endregion 

        public ApplicationInfo Add(int applicationId, int clientApplicationInfoTypeId, string infoText, int userId)
        {
            var o = new ApplicationInfo();
            o.ApplicationId = applicationId;
            o.ClientApplicationInfoTypeId = clientApplicationInfoTypeId;
            o.InfoText = infoText;
            Helper.UpdateCreatedFields(o, userId);
            Helper.UpdateModifiedFields(o, userId);
            Add(o);

            return o;
        }

        public ApplicationInfo GetByClientApplicationInfoTypeId(int clientApplicationInfoTypeId)
        {
            var o = Get(x => x.ClientApplicationInfoTypeId == clientApplicationInfoTypeId).FirstOrDefault();
            return o;
        }

        public void Update(ApplicationInfo applicationInfo, string infoText, int userId)
        {
            if (applicationInfo.InfoText != infoText)
            {
                applicationInfo.InfoText = infoText;
                Helper.UpdateModifiedFields(applicationInfo, userId);
            }
        }
    }
}
