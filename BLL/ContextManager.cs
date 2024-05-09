using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// Wrapper object
    /// </summary>
    public struct ServiceContext
    {
        /// <summary>
        /// Object providing Database access.
        /// </summary>
        internal IUnitOfWork UnitOfWork { get; set; }
    }
    /// <summary>
    /// Context manager.  Creates & returns a UnitOfWork object for server initialization
    /// for transaction support between servers.
    /// </summary>
    public static class ContextManager
    {
        /// <summary>
        /// Creates a container for the UnitOfWork
        /// </summary>
        /// <returns>ServiceContext wrapper containing a UnitOfWork object</returns>
        public static ServiceContext Create()
        {
            ServiceContext result = new ServiceContext();
            result.UnitOfWork = new UnitOfWork();
            return result;
        }
    }
}
