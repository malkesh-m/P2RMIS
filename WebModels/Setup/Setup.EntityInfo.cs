namespace Sra.P2rmis.WebModels.Setup
{
    /// <summary>
    /// Describes the information returned from Program Setup operations
    /// </summary>
    public class ProgramEntityInfo : BaseEntityInfo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        public ProgramEntityInfo(int entityId) : base(entityId) { }
    }
    /// <summary>
    /// Describes the information returned from ProgramMechanism operations
    /// </summary>
    public class ProgramMechanismEntityInfo : BaseEntityInfo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        public ProgramMechanismEntityInfo(int entityId) : base(entityId) { }
    }
    /// <summary>
    /// Describes the information returned from MechanismTemplateElement operations
    /// </summary>
    public class MechanismTemplateElementEntityInfo : BaseEntityInfo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="entity">Entity created</param>
        public MechanismTemplateElementEntityInfo(int entityId, object entity) : base(entityId)
        {
            this.Entity = entity;
            this.EntityId = entityId;
        }
        #region Attributes
        /// <summary>
        /// The CRUD entity manipulated.  
        /// </summary>
        public object Entity { get; set; }
        /// <summary>
        /// Entity identifier.  Needed to have write access to this but did not want to
        /// provide it for all.
        /// </summary>
        public new int EntityId { get; set; }
        #endregion
        #region Services
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MechanismTemplateEntityId"></param>
        public void SetId(int MechanismTemplateEntityId)
        {
            this.EntityId = MechanismTemplateEntityId;
        }
        #endregion 
    }

    /// <summary>
    /// Describes the information returned from MechanismTemplate operations
    /// </summary>
    public class MechanismTemplateEntityInfo : BaseEntityInfo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        public MechanismTemplateEntityInfo(int entityId) : base(entityId) { }
    }
    /// <summary>
    /// Describes the information returned from ClientProgram operations
    /// </summary>
    public class ClientProgramEntityInfo : BaseEntityInfo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        public ClientProgramEntityInfo(int entityId) : base(entityId) { }
    }
    /// <summary>
    /// Describes the information returned from MeetingSession operations
    /// </summary>
    public class MeetingSessionEntityInfo : BaseEntityInfo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        public MeetingSessionEntityInfo(int entityId) : base(entityId) { }
    }
    /// <summary>
    /// Describes the information returned from SessionPanel operations
    /// </summary>
    public class SessionPanelEntityInfo: BaseEntityInfo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        public SessionPanelEntityInfo(int entityId) : base(entityId) { }
    }

    public class MechanismScoringTemplateEntityInfo: BaseEntityInfo
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        public MechanismScoringTemplateEntityInfo(int entityId) : base(entityId) { }
    }
}
