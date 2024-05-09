using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.ConsumerManagement
{
    public interface INomineeRepository : IGenericRepository<Nominee>
    {
        /// <summary>
        /// Add nominee
        /// </summary>
        /// <param name="nominee">The nominee</param>
        /// <param name="userId">User identifer</param>
        /// <returns></returns>
        Nominee Add(Nominee nominee, int userId);
    }
    public class NomineeRepository : GenericRepository<Nominee>, INomineeRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public NomineeRepository(P2RMISNETEntities context)
            : base(context)
        {
        }
        #endregion
        /// <summary>
        /// Add nominee
        /// </summary>
        /// <param name="nominee">The nominee</param>
        /// <param name="userId">User identifer</param>
        /// <returns></returns>
        public Nominee Add(Nominee nominee, int userId)
        {
            Helper.UpdateCreatedFields(nominee, userId);
            Helper.UpdateModifiedFields(nominee, userId);
            foreach (var nomineePhone in nominee.NomineePhones)
            {
                Helper.UpdateCreatedFields(nomineePhone, userId);
                Helper.UpdateModifiedFields(nomineePhone, userId);
            }
            context.Nominees.Add(nominee);
            return nominee;
        }

    }
}
