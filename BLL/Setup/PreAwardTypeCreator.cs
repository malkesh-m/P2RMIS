using Sra.P2rmis.Bll.Setup.Actions;
using Sra.P2rmis.Bll.Setup.Blocks;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.Dal;
using System.Linq;

namespace Sra.P2rmis.Bll.Setup
{
    /// <summary>
    /// Object to locate & create ClientAwardTypes for Pre-Awards mechanism.
    /// </summary>
    internal class PreAwardTypeCreator
    {
        #region Constructor & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork providing database access</param>
        /// <param name="entity">ClientAwardType selected</param>
        /// <param name="userId">User entity identifier of the user adding the award</param>

        internal PreAwardTypeCreator(IUnitOfWork unitOfWork, ClientAwardType entity, int userId)
        {
            this.UnitOfWork = unitOfWork;
            this.Entity = entity;
            this.AwardDescription = entity.AwardDescription;
            this.AwardAbbreviation = entity.AwardAbbreviation;
            this.ClientId = entity.ClientId;
            this.UserId = userId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// UnitOfWork for database access
        /// </summary>
        private IUnitOfWork UnitOfWork { get; set; }
        /// <summary>
        /// ClientAwardType selected
        /// </summary>
        private ClientAwardType Entity { get; set; }
        /// <summary>
        /// ClientTypeAward description
        /// </summary>
        private string AwardDescription { get; set; }
        /// <summary>
        /// ClientTypeAward abbreviation
        /// </summary>
        private string AwardAbbreviation { get; set; }
        /// <summary>
        /// Client entity identifier
        /// </summary>
        private int ClientId { get; set; }
        /// <summary>
        /// User entity id of user making change
        /// </summary>
        private int UserId { get; set; }
        /// <summary>
        /// Creator result (either newly created or existing ClientTypeAward)
        /// </summary>
        internal ClientAwardType PreAppClientAwardType { get; private set; }
        #endregion
        #region Services
        /// <summary>
        /// Locates or creates a ClientAwardType
        /// </summary>
        /// <returns></returns>
        internal virtual ClientAwardType LocatePreClientAwardType()
        {
            //
            // Retrieve an existing PreApp ClientAwardType entity.  One may
            // not exist.  In which case one will need to be created.
            //
            ClientAwardType entity = RetrievePreClientAwardType();
            if (entity == null)
            {
                entity = CreatePreClientAwardType();
            }
            return entity;
        }
        /// <summary>
        /// Determines if a PreAward ClientAward exists for the selected ClientAward type.
        /// </summary>
        /// <returns>ClientAwardType entity id if a PreAward ClientAward exists; false otherwise</returns>
        internal virtual ClientAwardType RetrievePreClientAwardType()
        {
            return UnitOfWork.ClientAwardTypeRepository.Get(x => x.ParentAwardTypeId == Entity.ClientAwardTypeId).FirstOrDefault();
        }
        /// <summary>
        /// Creates a Pre-ClientAwardType to match the ClientAwardType
        /// </summary>
        /// <returns>Created ClientAwardType</returns>
        internal virtual ClientAwardType CreatePreClientAwardType()
        {
            ClientAwardTypeBlock block = new ClientAwardTypeBlock(this.ClientId, PreTypeThis(this.AwardAbbreviation), PreTypeThis(this.AwardDescription), MechanismRelationshipType.Indexes.PreApplication, Entity.ClientAwardTypeId);
            block.ConfigureAdd();

            ClientAwardTypeServiceAction action = new ClientAwardTypeServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.ClientAwardTypeRepository, ServiceAction<ClientAwardType>.DoNotUpdate, 0, this.UserId);
            action.Populate(block);
            action.Execute();

            return action.CRUDedEntity;
        }
        /// <summary>
        /// Change any label to a Pre label.
        /// </summary>
        /// <param name="label">Label to change</param>
        /// <returns>Label converted to a Pre value</returns>
        internal virtual string PreTypeThis(string label)
        {
            return $"{label}{ConfigManager.PreAwardMarker}";
        }
        /// <summary>
        /// Determines if a PreApp ClientType entity is referenced, exists and
        /// updates or creates one if necessary.
        /// </summary>
        internal virtual void Execute()
        {
            this.PreAppClientAwardType = this.LocatePreClientAwardType();
        }
        #endregion
    }
}
