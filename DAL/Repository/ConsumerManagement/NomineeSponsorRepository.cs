using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.ConsumerManagement
{
    public interface INomineeSponsorRepository : IGenericRepository<NomineeSponsor>
    {
        /// <summary>
        /// Add nominee sponsor
        /// </summary>
        /// <param name="nomineeSponsor">The nominee sponsor</param>
        /// <param name="userId">The user identifier</param>
        /// <returns></returns>
        NomineeSponsor Add(NomineeSponsor nomineeSponsor, int userId);
        /// <summary>
        /// Nominating Organizations
        /// </summary>
        /// <returns>list</returns>
        List<KeyValuePair<int, string>> NominatingOrganizations(string prefix, string findtype);
    }
    public class NomineeSponsorRepository : GenericRepository<NomineeSponsor>, INomineeSponsorRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public NomineeSponsorRepository(P2RMISNETEntities context)
            : base(context)
        {
        }
        #endregion

        /// <summary>
        /// Add nominee sponsor
        /// </summary>
        /// <param name="nomineeSponsor">The nominee sponsor</param>
        /// <param name="userId">The user identifier</param>
        /// <returns></returns>
        public NomineeSponsor Add(NomineeSponsor nomineeSponsor, int userId)
        {
            Helper.UpdateCreatedFields(nomineeSponsor, userId);
            Helper.UpdateModifiedFields(nomineeSponsor, userId);
            foreach (var nomineePhone in nomineeSponsor.NomineeSponsorPhones)
            {
                Helper.UpdateCreatedFields(nomineePhone, userId);
                Helper.UpdateModifiedFields(nomineePhone, userId);
            }
            context.NomineeSponsors.Add(nomineeSponsor);
            return nomineeSponsor;
        }

        /// <summary>
        /// Nominating Organizations
        /// </summary>
        /// <returns>list</returns>
        public List<KeyValuePair<int, string>> NominatingOrganizations(string prefix, string findtype)
        {
            if (findtype == "contains")
            {
                return context.NominatingOrganizations
                     .Where(x => x.OrganizationName.Contains(prefix))
                     .AsEnumerable()
                     .Select(x => new KeyValuePair<int, string>(x.OrganizationId, x.OrganizationName)).ToList();
            }

            return context.NominatingOrganizations
                .Where(x => x.OrganizationName.StartsWith(prefix))
                .AsEnumerable()
                .Select(x => new KeyValuePair<int, string>(x.OrganizationId, x.OrganizationName)).ToList();

         }

    }
}
