using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Dal.Repository;

namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Definition of repository classes for summary statement entity objects.
    /// 
    /// In general entity objects that are specifically used by individual subsystems are
    /// located in source files named UnitOfWork."subsystem name".
    /// </summary>
    public partial class UnitOfWork
    {
        #region WorkflowMechanism Repository
        /// <summary>
        /// Provides database access for the ApplicationWorkflowStepAssignment repository functions.
        /// </summary>
        private IWorkflowMechanismRepository _WorkflowMechanismRepository;
        public IWorkflowMechanismRepository WorkflowMechanismRepository { get { return LazyLoadWorkflowMechanismRepository(); } }
        /// <summary>
        /// Lazy load the WorkflowMechanismRepository.
        /// </summary>
        /// <returns>WorkflowMechanismRepository</returns>
        private IWorkflowMechanismRepository LazyLoadWorkflowMechanismRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._WorkflowMechanismRepository == null)
            {
                this._WorkflowMechanismRepository = new WorkflowMechanismRepository(_context);
            }
            return _WorkflowMechanismRepository;
        }
        #endregion
        #region ApplicationSummaryLog Repository
        /// <summary>
        /// Provides database access for the ApplicationSummaryLog repository functions.
        /// </summary>
        private IApplicationSummaryLogRepository _applicationSummaryLogRepository;
        public IApplicationSummaryLogRepository ApplicationSummaryLogRepository { get { return LazyLoadApplicationSummaryLogRepository(); } }
        /// <summary>
        /// Lazy load the ApplicationSummaryLogRepository.
        /// </summary>
        /// <returns>ApplicationSummaryLogRepository</returns>
        private IApplicationSummaryLogRepository LazyLoadApplicationSummaryLogRepository()
        {
            if (this._context == null)
            {
                _context = new P2RMISNETEntities();
            }

            if (this._applicationSummaryLogRepository == null)
            {
                this._applicationSummaryLogRepository = new ApplicationSummaryLogRepository(_context);
            }
            return _applicationSummaryLogRepository;
        }
        #endregion
    }
}
