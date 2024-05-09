using Sra.P2rmis.Dal.Repository.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal
{
    public partial class UnitOfWork
    {
        #region Policy
        /// <summary>
        /// Provides database access for the Policy repository functions.
        /// </summary>
        private IPolicyRepository _PolicyRepository;
        public IPolicyRepository PolicyRepository { get { return LazyLoadPolicy(); } }
        /// <summary>
        /// Lazy load the Policy.
        /// </summary>
        /// <returns>WeekDayType</returns>
        private IPolicyRepository LazyLoadPolicy()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._PolicyRepository == null)
            {
                this._PolicyRepository = new PolicyRepository(_context);
            }
            return _PolicyRepository;
        }
        #endregion

        #region Policy Week
        /// <summary>
        /// Provides database access for the Policy week day repository functions.
        /// </summary>
        private IPolicyWeekDayRepository _PolicyWeekDayRepository;
        public IPolicyWeekDayRepository PolicyWeekDayRepository { get { return LazyLoadPPolicyWeekDayRepository(); } }
        /// <summary>
        /// Lazy load the Policy.
        /// </summary>
        /// <returns>WeekDayType</returns>
        private IPolicyWeekDayRepository LazyLoadPPolicyWeekDayRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._PolicyWeekDayRepository == null)
            {
                this._PolicyWeekDayRepository = new PolicyWeekDayRepository(_context);
            }
            return _PolicyWeekDayRepository;
        }
        #endregion

        #region Policy Network Range
        /// <summary>
        /// Provides database access for the Policy network range repository functions.
        /// </summary>
        private IPolicyNetworkRangeRepository _PolicyNetworkRangeRepository;
        public IPolicyNetworkRangeRepository PolicyNetworkRangeRepository { get { return LazyLoadPPolicyNetworkRangeRepository(); } }
        /// <summary>
        /// Lazy load the Policy.
        /// </summary>
        /// <returns>NetworkRangeType</returns>
        private IPolicyNetworkRangeRepository LazyLoadPPolicyNetworkRangeRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._PolicyNetworkRangeRepository == null)
            {
                this._PolicyNetworkRangeRepository = new PolicyNetworkRangeRepository(_context);
            }
            return _PolicyNetworkRangeRepository;
        }
        #endregion

        #region Policy Week
        /// <summary>
        /// Provides database access for the Policy history repository functions.
        /// </summary>
        private IPolicyHistoryRepository _PolicyHistoryRepository;
        public IPolicyHistoryRepository PolicyHistoryRepository { get { return LazyLoadPPolicyHistoryRepository(); } }
        /// <summary>
        /// Lazy load the Policy.
        /// </summary>
        /// <returns>WeekDayType</returns>
        private IPolicyHistoryRepository LazyLoadPPolicyHistoryRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._PolicyHistoryRepository == null)
            {
                this._PolicyHistoryRepository = new PolicyHistoryRepository(_context);
            }
            return _PolicyHistoryRepository;
        }
        #endregion
    }
}
