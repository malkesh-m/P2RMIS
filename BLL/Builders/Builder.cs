namespace Sra.P2rmis.Bll.Builders
{
    /// <summary>
    /// Constructs a model from an entity object.
    /// </summary>
    /// <typeparam name="Entity">Entity object</typeparam>
    /// <typeparam name="Model">Model type to construct</typeparam>
    public class Builder<Entity, Model>
    {
        #region Properties
        /// <summary>
        /// Entity object
        /// </summary>
        protected Entity _entity { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="entity">A database entity to turn into a model</param>
        public Builder(Entity entity)
        {
            this._entity = entity;
        }
        #endregion
        #region BuilderServices
        /// <summary>
        /// Build method takes the entity object & converts it into a model object.  All 
        /// instances of the builder must override this.
        /// </summary>
        /// <returns>Constructs a model from an entity object</returns>
        public virtual Model Build()
        {
            return default(Model);
        }
        #endregion
    }
}
