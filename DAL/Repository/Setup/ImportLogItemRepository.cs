using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sra.P2rmis.Dal.Repository.Setup
{
    public interface IImportLogItemRepository : IGenericRepository<ImportLogItem>
    {
        /// <summary>
        /// Adds the import log item.
        /// </summary>
        /// <param name="importLogId">The import log identifier.</param>
        /// <param name="key">The key.</param>
        /// <param name="content">The content.</param>
        /// <param name="successFlag">if set to <c>true</c> [success flag].</param>
        /// <param name="message">The message.</param>
        void AddImportLogItem(int importLogId, string key, string content, bool successFlag, string message);
        /// <summary>
        /// Gets the by import log identifier.
        /// </summary>
        /// <param name="importLogId">The import log identifier.</param>
        /// <returns></returns>
        IEnumerable<ImportLogItem> GetByImportLogId(int importLogId);
        /// <summary>
        /// Gets the last successful import log item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        ImportLogItem GetLastSuccessfulImportLogItem(string key);
    }

    public class ImportLogItemRepository : GenericRepository<ImportLogItem>, IImportLogItemRepository
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Repository constructor
        /// </summary>
        /// <param name="context">P2RMIS database context</param>
        public ImportLogItemRepository(P2RMISNETEntities context)
            : base(context)
        {
        }
        #endregion        
        /// <summary>
        /// Adds the import log item.
        /// </summary>
        /// <param name="importLogId">The import log identifier.</param>
        /// <param name="key">The key.</param>
        /// <param name="content">The content.</param>
        /// <param name="successFlag">if set to <c>true</c> [success flag].</param>
        /// <param name="message">The message.</param>
        public void AddImportLogItem(int importLogId, string key, string content, bool successFlag, string message)
        {
            var o = new ImportLogItem();
            o.ImportLogId = importLogId;
            o.Key = key;
            o.Content = content;
            o.SuccessFlag = successFlag;
            o.Message = message;

            Add(o);
        }
        /// <summary>
        /// Gets the by import log identifier.
        /// </summary>
        /// <param name="importLogId">The import log identifier.</param>
        /// <returns></returns>
        public IEnumerable<ImportLogItem> GetByImportLogId(int importLogId)
        {
            var os = Get(x => x.ImportLogId == importLogId);
            return os;
        }
        /// <summary>
        /// Gets the last successful import log item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public ImportLogItem GetLastSuccessfulImportLogItem(string key)
        {
            var o = Get(x => x.Key == key && x.SuccessFlag).OrderByDescending(x => x.ImportLogItemId).FirstOrDefault();
            return o;
        }
    }
}
