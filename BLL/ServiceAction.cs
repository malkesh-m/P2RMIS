using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.Bll.Rules;
using System;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// Mark object as a CrudBlock (non entity specific)
    /// </summary>
    public interface ICrudBlock
    {
        Nullable<DateTime> ModifiedDate { get; set; }
    }
    /// <summary>
    /// Object container hosting data and instructions 
    /// for populating the T entity.
    /// </summary>
    /// <typeparam name="T">Entity object</typeparam>
    public abstract class CrudBlock<T>: ICrudBlock where T : class, IStandardDateFields
    {
        #region Attributes
        /// <summary>
        /// User entity identifier of user invoking a CRUD operation
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Indicates if the operation is for Add
        /// </summary>
        protected internal bool IsAdd { get; set; }
        /// <summary>
        /// Indicates if the operation is for Modify
        /// </summary>
        protected internal bool IsModify { get; protected set; }
        /// <summary>
        /// Indicates if the operation is for Delete
        /// </summary>
        protected internal bool IsDelete { get; protected set; }
        /// <summary>
        /// DateTime entity was last changed
        /// </summary>
        public Nullable<DateTime> ModifiedDate { get; set; }
        #endregion
        #region Methods
        /// <summary>
        /// Sets individual properties on the CRUD entity.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <param name="entity">Entity to populate</param>
        internal virtual void Populate(int userId, T entity) { }
        /// <summary>
        /// Indicates if the block has data.
        /// </summary>
        /// <returns>True if the block contains data; false otherwise</returns>
        internal virtual bool HasData() { return true; }
        #endregion
    }
    /// <summary>
    /// Generic object to perform Add, Modify and Deletes on an entity object
    /// that is not represented by a WebModel
    /// </summary>
    /// <typeparam name="T">Entity object</typeparam>
    public abstract class ServiceAction<T>
        where T : class, IStandardDateFields, new()
    {
        #region Constants
        public const bool DoUpdate = true;
        public const bool DoNotUpdate = false;
        /// <summary>
        /// Indicates the entity does not exist.
        /// </summary>
        public int EntityDoesNotExist { get { return 0; } }
        #endregion
        #region Construction & Setup
        /// <summary>
        /// Parameter-less constructor
        /// </summary>
        public ServiceAction()  { }
        /// <summary>
        /// Initialize the action.  Parameters supply the necessary environmental information
        /// to interact with the entity framework.
        /// </summary>
        /// <param name="unitOfWork">Unit of work object</param>
        /// <param name="repository">Entity repository</param>
        /// <param name="saveThis">Flag indicating if the framework should be saved after the operation</param>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="userId">Entity identifier of the user performing the operation</param>
        public void InitializeAction(IUnitOfWork unitOfWork, IGenericRepository<T> repository, bool saveThis, int entityId, int userId)
        {
            this.UnitOfWork = unitOfWork;
            this.Repository = repository;
            this.SaveThis = saveThis;
            this.EntityIdentifier = entityId;
            this.UserId = userId;
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
        /// This version of InitializeAction is only to be used by ServiceModelActionForWebModel.
        /// </remarks>
        public void InitializeAction(IUnitOfWork unitOfWork, IGenericRepository<T> repository, bool saveThis, int userId)
        {
            this.UnitOfWork = unitOfWork;
            this.Repository = repository;
            this.SaveThis = saveThis;
            this.UserId = userId;
        }
        /// <summary>
        /// Data to populate the created or modified entity.
        /// </summary>
        /// <param name="block">CrudBlock</param>
        public void Populate(CrudBlock<T> block)
        {
            this.Block = block;
        }
        /// <summary>
        /// Set RuleEngine to apply to all actions.
        /// </summary>
        /// <param name="ruleMachine">Rule engine to apply to all actions</param>
        internal void Rule(RuleEngine<T> ruleMachine)
        {
            this.RuleMachine = ruleMachine;
        }
        #endregion
        #region Properties
        /// <summary>
        /// RuleEngine to run on CRUD actions.
        /// </summary>
        /// <remarks>Initialized to the default RuleEngine because RuleEngine is not being retrofitted at this time.</remarks>
        internal RuleEngine<T> RuleMachine { get; private set; } = new RuleEngine<T>();
        /// <summary>
        /// Contains data & instructions for populating the CRUDable entity.
        /// </summary>
        protected CrudBlock<T> Block { get; set; }
        /// <summary>
        /// Entity identifier of the user requesting the action
        /// </summary>
        protected int UserId { get; set; }
        /// <summary>
        /// Unit of work 
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; set; }
        /// <summary>
        /// Entity object repository
        /// </summary>
        protected IGenericRepository<T> Repository { get; set; }
        /// <summary>
        /// Indicates if the action should save the added/modified/deleted entity to the framework.
        /// </summary>
        public bool SaveThis { get; set; }
        #endregion
        #region Entity (non-web model) specific properties
        /// <summary>
        /// Entity identifier for the object
        /// </summary>
        public int EntityIdentifier { get; set; }
        #endregion
        #region Edit Actions
        /// <summary>
        /// Access point for performing the operation identified by the supplied data.
        /// </summary>
        public void Execute()
        {
            //
            // Run the RuleEngine. We will always have a RuleEngine, even an empty one.
            //
            this.RuleMachine.Apply();
            //
            // If the RuleEngine is broken we don't want to perform any CRUD operation.
            //
            if (this.RuleMachine.IsBroken)
            {

            }
            else if (IsAdd & HasData)
            {
                 Add();
            }
            else if (IsDelete)
            {
                Delete();
            }
            else if (HasData)
            {
                Modify();
            }
            else
            {
                //
                // Do not have to do anything since there is not data and it is not an add or delete
                //
            }
            Save();
        }
        /// <summary>
        /// Adds an entry.
        /// </summary>
        protected virtual void Add()
        {
            //
            // Perform any pre-add processing.  This step is optional and by default is an empty method.
            //
            PreAdd();
            //
            // Create the entity & populate it.  While we are at it update the necessary time fields 
            // then add it to the repository.
            //
            var entity = new T();
            Populate(entity);
            Helper.UpdateCreatedFields(entity, UserId);
            Helper.UpdateModifiedFields(entity, UserId);

            Repository.Add(entity);
            //
            // Perform any post-add processing.  This step is optional and by default is an empty method.
            //
            PostAdd(entity);
            
        }
        /// <summary>
        /// Deletes an entry.
        /// </summary>
        protected virtual void Delete()
        {
            // Get the entity we want to delete
            //
            var entity = Repository.GetByID(EntityId());
            //
            // Perform any pre-delete processing.  This step is optional and by default is an empty method.
            //
            PreDelete(entity);
            //
            // Update the deleted time fields & delete the entity from the repository.
            //
            Helper.UpdateDeletedFields(entity, UserId);
            Repository.Delete(entity);
            //
            // Perform any post-delete processing.  This step is optional and by default is an empty method.
            //
            PostDelete(entity);
        }
        /// <summary>
        /// Modifies an entry.
        /// </summary>
        protected virtual void Modify()
        {
            // Get the entity we want to modify
            //
            var entity = Repository.GetByID(EntityId());
            //
            // Perform any validation required by Modify.  By default all 
            // requests are valid.  Some Actions my require task specific validation.
            //
            if (IsValidModify(entity))
            {
                //
                // Perform any pre-modify processing.  This step is optional and by default is an empty method.
                //
                PreModify(entity);
                //
                // Populate the entity.  While we are at it update the necessary time field
                // then add it to the repository.
                //
                Populate(entity);
                Helper.UpdateModifiedFields(this.UnitOfWork, entity, UserId);
                Repository.Update(entity);
                //
                // Perform any post-modify processing.  This step is optional and by default is an empty method.
                //
                PostModify(entity);
            }
        }
        #endregion
        #region Required Overwrites
        /// <summary>
        /// Indicates if the data represents a delete.
        /// </summary>
        /// <remarks>
        /// If the caller knows by context that a delete will not be required,
        /// this method does not require to be overwritten.
        /// </remarks>
        protected virtual bool IsDelete
        {
            get { return false; }
        }
        /// <summary>
        /// Populates the entity with the supplied data.
        /// </summary>
        /// <param name="entity">Entity object to populate</param>
        /// <remarks>This method must be overridden.</remarks>
        protected virtual void Populate(T entity)
        {
            Block.Populate(UserId, entity);
        }
        /// <summary>
        /// Indicates if the entity has data.
        /// </summary>
        /// <remarks>
        /// Not required if only deleting.  If only Adding an entity and the caller is certain the
        /// data exists this can be hard coded to return true.
        /// </remarks>
        protected virtual bool HasData
        {
            get
            {
                return false;
            }
        }
        #endregion
        #region Optional Overwrites processing steps
        /// <summary>
        /// Optional pre-add processing logic.  Add any additional processing necessary before the entity object
        /// is added to the framework.
        /// </summary>
        protected virtual void PreAdd()
        {

        }
        /// <summary>
        /// Optional post add processing.  Add any additional processing necessary after the entity object
        /// is added to the framework.                     
        /// </summary>
        protected virtual void PostAdd(T entity)
        {

        }
        /// <summary>
        /// Optional pre-delete processing logic.  Add any additional processing necessary before the entity object
        /// is deleted from the framework.
        /// </summary>
        protected virtual void PreDelete(T entity)
        {

        }
        /// <summary>
        /// Optional post delete processing.  Add any additional processing necessary after the entity object
        /// is deleted from the framework.                     
        /// </summary>
        protected virtual void PostDelete(T entity)
        {

        }
        /// <summary>
        /// Optional pre-modify processing logic.  Add any additional processing necessary before the entity object
        /// is modified.
        /// </summary>
        protected virtual void PreModify(T entity)
        {

        }
        /// <summary>
        /// Optional post modify processing.  Add any additional processing necessary after the entity object
        /// is modified.                     
        /// </summary>
        protected virtual void PostModify(T entity)
        {

        }
        /// <summary>
        /// Saves the entity(s) if configured.
        /// </summary>
        protected virtual void Save()
        {
            //
            // Don't want to do a save if any rules have been broken
            //
            if ((SaveThis) && (!this.RuleMachine.IsBroken))
            {
                UnitOfWork.Save();
            }
        }
        /// <summary>
        /// Overridden IsAdd for non WebModel values 
        /// </summary>
        public virtual bool IsAdd
        {
            get
            {
                return (EntityIdentifier == 0);
            }
        }
        /// <summary>
        /// Returns the entity id of the entity.
        /// </summary>
        /// <returns>Entity identifier of the entity to operate upon</returns>
        protected virtual int EntityId()
        {
            return EntityIdentifier;
        }
        /// <summary>
        /// Validate the Modify operation.  By default the operation is valid.
        /// </summary>
        /// <param name="entity">Target entity to modify</param>
        /// <returns>True if modify can proceed; false otherwise</returns>
        protected virtual bool IsValidModify(T entity)
        {
            return true;
        }
        /// <summary>
        /// Define any post processing here.  By definition there is none but
        /// specific implementations are free to define there own
        /// </summary>
        public virtual void PostProcess()
        {

        }
        #endregion
    }
}