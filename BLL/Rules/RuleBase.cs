using Sra.P2rmis.Dal;
using System.Collections.Generic;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Dal.Interfaces;

namespace Sra.P2rmis.Bll.Rules
{
    /// <summary>
    /// Identifies the available CRUD actions
    /// </summary>
    internal enum CrudAction
    {
        // apply the rule to an Add (Create) action
        Add,
        // apply the rule to a Modify action
        Modify,
        // apply the rule to a Delete action
        Delete,
        // The default action - which should never happen.
        Default
    }
    /// <summary>
    /// Base implementation of attributes & methods common to all business rules.
    /// </summary>
    internal interface IRuleBase
    {
        #region Attributes
        string Message { get; }
        bool IsBroken { get; }
        IList<CrudAction> Actions { get; }
        #endregion
        #region Services
        void Apply(ICrudBlock block);
        #endregion
    }
    /// <summary>
    /// Base implementation of attributes & Services common to all business rules.
    /// </summary>
    internal abstract class RuleBase<Tentity>: IRuleBase where Tentity : class, IStandardDateFields
    {
        #region Construction & setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork object providing access to database</param>
        /// <param name="entity">Entity to apply rule to</param>
        /// <param name="actions">Run engine on this CRUD action</param>
        internal RuleBase(IUnitOfWork unitOfWork, Tentity entity, IList<CrudAction> actions)
        {
            this.UnitOfWork = unitOfWork;
            this.Entity = entity;
            this.Actions = actions;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Unit of work providing access to entity objects
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; set; }
        /// <summary>
        /// Message to return when rule fails
        /// </summary>
        public string Message { get; protected set; } = string.Empty;
        /// <summary>
        /// Entity to validate
        /// </summary>
        protected Tentity Entity { get; set; }
        /// <summary>
        /// Indicates if the rule has been broken.
        /// </summary>
        public bool IsBroken { get; protected set; }
        /// <summary>
        /// Itemizes the CRUD actions that the rule applies
        /// </summary>
        public IList<CrudAction> Actions { get; set; } = new List<CrudAction>();
        #endregion
        #region Services
        /// <summary>
        /// Apply the rule and update the rule state.
        /// </summary>
        public virtual void Apply(ICrudBlock block) { }
        /// <summary>
        /// Log the rule execution and it's result.  I hope someday we will
        /// get a log to help in debugging the live system.
        /// </summary>
        public virtual void Log() { }
        #endregion
    }
}
