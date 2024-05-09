using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.ReviewerRecruitment
{
    /// <summary>
    /// Service Action method to perform CRUD operations on UserCommunicationLog entities.
    /// </summary>
    internal class UserCommunicationLogServiceAction : ServiceAction<UserCommunicationLog>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public UserCommunicationLogServiceAction() { }
        /// <summary>
        /// Initialize the action's data values
        /// </summary>
        /// <param name="entityIdentifier">UserCommunicationLog entity identifier</param>
        /// <param name="toUserEntityId">User entity identifier of user receiving the communication</param>
        /// <param name="communicationMethodId">CommunicationMethod (used to contact the ToUser) entity identifier</param>
        /// <param name="comment">Comment</param>
        public void Populate(int entityIdentifier, int toUserEntityId, int communicationMethodId, string comment)
        {
            this.EntityIdentifier = entityIdentifier;
            this.ToUserEntityId = toUserEntityId;
            this.CommunicationMethodId = communicationMethodId;
            this.Comment = comment;
        }
        /// <summary>
        /// Initialize the action.  Parameters supply the necessary environmental information
        /// to interact with the entity framework.
        /// </summary>
        /// <param name="unitOfWork">Unit of work object</param>
        /// <param name="repository">Entity repository</param>
        /// <param name="saveThis">Flag indicating if the framework should be saved after the operation</param>
        /// <param name="userId">Entity identifier of the user performing the operation</param>
        /// <remarks>
        /// We use this version of InitializeAction (intended for WebModels) because multiple UserCommunicationLog entities can be
        /// manipulated from the service method.
        /// </remarks>
        public new void InitializeAction(IUnitOfWork unitOfWork, IGenericRepository<UserCommunicationLog> repository, bool saveThis, int userId)
        {
            base.InitializeAction(unitOfWork, repository, saveThis, userId);
        }
        #endregion
        #region Attributes
        /// <summary>
        /// User entity identifier of user receiving the communication
        /// </summary>
        protected int ToUserEntityId { get; set; }
        /// <summary>
        /// CommunicationMethod (used to contact the ToUser) entity identifier
        /// </summary>
        protected int CommunicationMethodId { get; set; }
        /// <summary>
        /// Comment
        /// </summary>
        protected string Comment { get; set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the UserCommunicationLog entity with information in the ServiceAction.
        /// </summary>
        protected override void Populate(UserCommunicationLog entity)
        {
            entity.Populate(this.ToUserEntityId, this.CommunicationMethodId, this.Comment);
        }
        /// <summary>
        /// Indicates if the PanelUserRegistrationDocumentItem has data.
        /// </summary>
        protected override bool HasData
        {
            get
            {
                //
                // By definition we are expecting that it will have data.  Even
                // null or the empty string is considered as data.
                //
                return true;
            }
        }
        /// <summary>
        /// Indicates if the data represents a delete. 
        /// </summary>
        protected override bool IsDelete
        {
            get { return string.IsNullOrWhiteSpace(this.Comment); }
        }
        #endregion
    }
}
