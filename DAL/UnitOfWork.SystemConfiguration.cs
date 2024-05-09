using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal
{
    public partial class UnitOfWork
    {
        #region SystemConfiguration Repository
        /// <summary>
        /// Provides database access for the Gender repository functions.
        /// </summary>
        private IGenericRepository<SystemConfiguration> _SystemConfiguration;
        public IGenericRepository<SystemConfiguration> SystemConfigurationRepository { get { return LazyLoadSystemConfiguration(); } }
        /// <summary>
        /// Lazy load the SystemConfiguration.
        /// </summary>
        /// <returns>SystemConfiguration</returns>
        private IGenericRepository<SystemConfiguration> LazyLoadSystemConfiguration()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._SystemConfiguration == null)
            {
                this._SystemConfiguration = new GenericRepository<SystemConfiguration>(_context);
            }
            return _SystemConfiguration;
        }
        #endregion
    }
}
