using Sra.P2rmis.Bll.ProgramRegistration;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.OptionService.ResetContract
{
    /// <summary>
    /// Defines the parameter values for resetting of contracts.
    /// </summary>
    internal class OptionInitializeBlockContractToaster : OptionInitializeBlock
    {
        #region Construction & Setup
        /// <summary>
        /// Default constructor
        /// </summary>
        internal OptionInitializeBlockContractToaster() : base() { }
        /// <summary>
        /// ContractToaster parameter block initialization
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork for accessing the database</param>
        /// <param name="userId">User entity identifier</param>
        /// <param name="panelUserAssignmentId">PanelUserAssignment identifier</param>
        internal virtual void Initialize(IUnitOfWork unitOfWork, int panelUserAssignmentId, int userId, int? clientParticipantTypeId, int? participantMethodId, bool restrictedAssignedFlag)
        {
            this.UnitOfWork = unitOfWork;
            this.UserId = userId;
            this.PanelUserAssignmentId = panelUserAssignmentId;
            this.ClientParticipantTypeId = clientParticipantTypeId;
            this.ParticipantMethodId = participantMethodId;
            this.RestrictedAssignedFlag = restrictedAssignedFlag;
        }
        #endregion
        #region Properties
        /// <summary>
        /// PanelUserAssignment identifier - identifies the reviewer whose contract is reset
        /// </summary>
        internal int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Gets or sets the client participant type identifier.
        /// </summary>
        /// <value>
        /// The client participant type identifier.
        /// </value>
        internal int? ClientParticipantTypeId { get; set; }
        /// <summary>
        /// Gets or sets the participant method identifier.
        /// </summary>
        /// <value>
        /// The participant method identifier.
        /// </value>
        internal int? ParticipantMethodId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [restricted assigned flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [restricted assigned flag]; otherwise, <c>false</c>.
        /// </value>
        internal bool RestrictedAssignedFlag { get; set; }
        #endregion
    }
    /// <summary>
    /// Implements the business rules to reset a contract.
    /// </summary>
    internal class OptionContractToaster : OptionAction, IOptionAction
    {
        #region Construction & Setup
        /// <summary>
        /// Default constructor
        /// </summary>
        public OptionContractToaster() : base() { }
        /// <summary>
        /// Initialize the options value
        /// </summary>
        public override void Initialize(IOptionInitializeBlock block)
        {
            var a = block as OptionInitializeBlockContractToaster;
            base.Initialize(block);
            this.PanelUserAssignmentId = a.PanelUserAssignmentId;
            this.ClientParticipantTypeId = a.ClientParticipantTypeId;
            this.ParticipantMethodId = a.ParticipantMethodId;
            this.RestrictedAssignedFlag = a.RestrictedAssignedFlag;
        }
        #endregion
        #region Properties
        private int PanelUserAssignmentId { get; set; }
        /// <summary>
        /// Gets or sets the client participant type identifier.
        /// </summary>
        /// <value>
        /// The client participant type identifier.
        /// </value>
        private int? ClientParticipantTypeId { get; set; }
        /// <summary>
        /// Gets or sets the participant method identifier.
        /// </summary>
        /// <value>
        /// The participant method identifier.
        /// </value>
        private int? ParticipantMethodId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [restricted assigned flag].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [restricted assigned flag]; otherwise, <c>false</c>.
        /// </value>
        private bool RestrictedAssignedFlag { get; set; }
        #endregion
        #region Services
        /// <summary>
        /// Default implementation of an option's action.  
        /// </summary>
        public override void Execute()
        {
            UpdateContractDocument(PanelUserAssignmentId, UserId, ClientParticipantTypeId, ParticipantMethodId, RestrictedAssignedFlag);
        }
        /// <summary>
        /// Implements the business rules surrounding the updating of a PanelUserAssignment after
        /// the contract has been signed.
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <param name="userId">User entity identifier of user making the change</param>
        internal virtual void UpdateContractDocument(int panelUserAssignmentId, int userId, int? clientParticipantTypeId, int? participantMethodId, bool restrictedAssignedFlag)
        {
            //
            // First we need to find the PanelUserRegistrationDocument that represents the contract
            //
            PanelUserAssignment panelUserAssignmentEntity = UnitOfWork.PanelUserAssignmentRepository.GetByID(panelUserAssignmentId);
            PanelUserRegistrationDocument panelUserRegistrationDocumentEntity = panelUserAssignmentEntity.FindContractDocument();
            //
            // Then if there is a document; it is signed we copy certain fields (calling it CleanCloning)l delete the original
            // and then add the cleaned clone in it's place.
            //
            if (
                (panelUserRegistrationDocumentEntity != null) &&
                //only remove the contract if a participant type, method, or restriction changed which might affect pay rates
                (
                    panelUserAssignmentEntity.RestrictedAssignedFlag != restrictedAssignedFlag ||
                    panelUserAssignmentEntity.ClientParticipantTypeId != clientParticipantTypeId ||
                    panelUserAssignmentEntity.ParticipationMethodId != participantMethodId
                )
               )
            {
                PanelUserRegistrationDocument panelUserRegistrationDocumentClonedEntity = panelUserRegistrationDocumentEntity.CleanClone(userId);
                ClearRegistrationCompletedDate(panelUserRegistrationDocumentEntity.PanelUserRegistrationId, userId);
                DeleteContractDocument(panelUserRegistrationDocumentEntity, userId);
                AddContractDocument(panelUserRegistrationDocumentClonedEntity, userId);
            }
        }
        /// <summary>
        /// Delete a PanelUserRegistrationDocument that is a contract. 
        /// </summary>
        /// <param name="entity">PanelUserRegistrationDocument entity to be deleted</param>
        /// <param name="userId">User entity identifier of user deleting the PanelUserRegistrationDocument entity</param>
        internal virtual void DeleteContractDocument(PanelUserRegistrationDocument entity, int userId)
        {
            PanelUserRegistrationDocumentDeleteServiceAction serviceAction = new PanelUserRegistrationDocumentDeleteServiceAction();
            serviceAction.InitializeAction(this.UnitOfWork, UnitOfWork.PanelUserRegistrationDocumentRepository, ServiceAction<PanelUserRegistrationDocument>.DoNotUpdate, entity.PanelUserRegistrationDocumentId, userId);
            serviceAction.Execute();
        }
        /// <summary>
        /// Adds a previously cloned PanelUserRegistrationDocument entity
        /// </summary>
        /// <param name="entity">Cloned PanelUserRegistrationDocument entity</param>
        /// <param name="userId">User entity identifier of user creating PanelUserRegistrationDocument entity</param>
        internal virtual void AddContractDocument(PanelUserRegistrationDocument entity, int userId)
        {
            PanelUserRegistrationDocumentAddServiceAction serviceAction = new PanelUserRegistrationDocumentAddServiceAction();
            serviceAction.InitializeAction(this.UnitOfWork, UnitOfWork.PanelUserRegistrationDocumentRepository, ServiceAction<PanelUserRegistrationDocument>.DoNotUpdate, userId);
            serviceAction.Populate(entity.PanelUserRegistrationId, entity.ClientRegistrationDocumentId, entity.PanelUserRegistrationDocumentItems, entity.SignedOfflineFlag);

            serviceAction.Execute();
        }
        /// <summary>
        /// Clears the completion date & time of the PanelUserRegistration.
        /// </summary>
        /// <param name="panelUserRegistrationId">PanelUserRegistration entity identifier</param>
        /// <param name="userId">User entity identifier of user making the change</param>
        internal virtual void ClearRegistrationCompletedDate(int panelUserRegistrationId, int userId)
        {
            PanelUserRegistration panelUserRegistration = UnitOfWork.PanelUserRegistrationRepository.GetByID(panelUserRegistrationId);
            PanelUserRegistrationServiceAction editAction = new PanelUserRegistrationServiceAction();
            editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.PanelUserRegistrationRepository, ServiceAction<PanelUserRegistration>.DoNotUpdate, panelUserRegistration.PanelUserRegistrationId, userId);
            editAction.Populate(panelUserRegistration.RegistrationStartDate, null);
            editAction.Execute();
        }
        #endregion
    }
}