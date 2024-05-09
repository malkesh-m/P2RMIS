using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    public interface IImportLogRepository : IGenericRepository<ImportLog>
    {
        /// <summary>
        /// Adds the import log.
        /// </summary>
        /// <param name="clientTransferTypeId">The client transfer type identifier.</param>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        ImportLog AddImportLog(int clientTransferTypeId, string url);
        /// <summary>
        /// Sets the import log status.
        /// </summary>
        /// <param name="importLogId">The import log identifier.</param>
        /// <param name="successFlag">if set to <c>true</c> [success flag].</param>
        /// <param name="content">The content.</param>
        /// <param name="message">The message.</param>
        void SetImportLogStatus(int importLogId, bool successFlag, string content, string message);
    }

    public class ImportLogRepository : GenericRepository<ImportLog>, IImportLogRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ImportLogRepository(P2RMISNETEntities context)
            : base(context)
        {
        }
        #endregion        
        /// <summary>
        /// Adds the import log.
        /// </summary>
        /// <param name="clientTransferTypeId">The client transfer type identifier.</param>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public ImportLog AddImportLog(int clientTransferTypeId, string url)
        {
            var o = new ImportLog();
            o.ClientTransferTypeId = clientTransferTypeId;
            o.Url = url;
            o.SuccessFlag = null;
            o.Timestamp = GlobalProperties.P2rmisDateTimeNow;

            Add(o);

            return o;
        }
        /// <summary>
        /// Sets the import log status.
        /// </summary>
        /// <param name="importLogId">The import log identifier.</param>
        /// <param name="successFlag">if set to <c>true</c> [success flag].</param>
        /// <param name="content">The content.</param>
        /// <param name="message"></param>
        public void SetImportLogStatus(int importLogId, bool successFlag, string content, string message)
        {
            var o = GetByID(importLogId);
            o.SuccessFlag = successFlag;
            o.Content = content;
            o.Message = message;
        }
    }
}
