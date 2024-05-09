using Sra.P2rmis.Dal;
using System.Linq;

namespace Sra.P2rmis.Bll.OptionService
{
    /// <summary>
    /// Interface for base class of Optional functionality.
    /// </summary>
    internal interface IOptionAction
    {
        #region Setup
        /// <summary>
        /// Default implementation of an option's action.  
        /// </summary>
        void Initialize(IOptionInitializeBlock block);
        #endregion
        #region Services
        /// <summary>
        /// Action performed
        /// </summary>
        void Execute();
        /// <summary>
        /// Default implementation for determining if the option is wanted.
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork to access the database </param>
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="systemConfigurationId">SystemConfiguration entity identifier</param>
        /// <returns>True the option is wanted; false is not wanted</returns>
        bool DetermineIfWanted(IUnitOfWork unitOfWork, int clientId, int systemConfigurationId);
        #endregion
    }
    /// <summary>
    /// Base class for Optional functionality.  Also serves as a null OptionAction
    /// </summary>
    internal class OptionAction : IOptionAction
    {
        #region Construction & Setup
        /// <summary>
        /// Default constructor
        /// </summary>
        public OptionAction() { }
        /// <summary>
        /// Initialize the options properties
        /// </summary>
        public virtual void Initialize(IOptionInitializeBlock block)
        {
            this.UnitOfWork = block.UnitOfWork;
            this.UserId = block.UserId;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Unit of work 
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; set; }
        /// <summary>
        /// Entity identifier of the user requesting the action
        /// </summary>
        protected int UserId { get; set; }
        #endregion
        #region Services
        /// <summary>
        /// Default implementation of an option's action.  
        /// </summary>
        public virtual void Execute()
        {
            //
            // By default we do nothing
            //
        }
        /// <summary>
        /// Default implementation for determining if the option is wanted.
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork to access the database </param>
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="systemConfigurationId">SystemConfiguration entity identifier</param>
        /// <returns>True the option is wanted; false is not wanted</returns>
        public virtual bool DetermineIfWanted(IUnitOfWork unitOfWork, int clientId, int systemConfigurationId)
        {
            //
            // First retrieve the specified SystemConfiguration
            //
            SystemConfiguration systemConfigurationEntity = unitOfWork.SystemConfigurationRepository.GetByID(systemConfigurationId);
            //
            // Then see if this client has an entry in the collection of ClientConfigurations.  If they do return the configured value.  We
            // assume that TRUE means they want the option.  Since this is the default we can do that.  If not then override it in the
            // specific OptionAction.  Otherwise we return false.
            //
            ClientConfiguration clientConfigurationEntity =  systemConfigurationEntity.ClientConfigurations.Where(x => x.ClientId == clientId).FirstOrDefault();
            return (clientConfigurationEntity != null) ? clientConfigurationEntity.ConfigurationValue : false;
        }
        #endregion
    }
}
