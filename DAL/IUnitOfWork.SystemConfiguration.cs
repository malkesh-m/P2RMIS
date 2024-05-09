namespace Sra.P2rmis.Dal
{
    /// <summary>
    /// Identifies the access points to the database entities for lookup entity objects.
    /// </summary>
    public partial interface IUnitOfWork
    {
        /// <summary>
        /// Provides database access for the Gender repository functions.
        /// </summary>
        IGenericRepository<SystemConfiguration> SystemConfigurationRepository { get; }
    }
}
