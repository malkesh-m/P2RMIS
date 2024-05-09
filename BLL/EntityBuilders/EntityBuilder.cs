using System;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.EntityBuilders
{
    /// <summary>
    /// Constructs a Entity from an model object.
    /// </summary>
    /// <typeparam name="Model">Model object</typeparam>
    /// <typeparam name="Entity">Entity type to construct</typeparam>
    public class EntityBuilder<Model, Entity>
    {
        #region Properties
        /// <summary>
        /// Model object
        /// </summary>
        protected Model _model { get; set; }
        /// <summary>
        /// Flag indicating if any value in the entity has been changed.
        /// </summary>
        public bool IsDirty { get; protected set; }
        /// <summary>
        /// User id
        /// </summary>
        protected int _userId { get; set; }
        /// <summary>
        /// List of validation error messages
        /// </summary>
        public List<string> Errors { get; protected set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model">A model object to turn into an entity object.</param>
        /// <param name="userId">User id</param>
        public EntityBuilder(Model model, int userId)
        {
            this._model = model;
            this._userId = userId;
            IsDirty = false;
            Errors = new List<string>();
        }
        #endregion
        #region BuilderServices
        /// <summary>
        /// Build method takes the model object & converts it into an entity object.  All 
        /// instances of the builder must override this.
        /// </summary>
        /// <returns>Constructs a entity from an model object</returns>
        public virtual Entity Build()
        {
            return default(Entity);
        }
        /// <summary>
        /// Build method takes the model object & updates an entity object.  All 
        /// instances of the builder must override this.
        /// </summary>
        /// <returns>Updated entity object</returns>
        public virtual Entity Update(Entity entity)
        {
            return default(Entity);
        }
        /// <summary>
        /// Tests if the old value is the same as the new value.  If it is then it updates the property and sets the 
        /// entity dirty indicator.
        /// </summary>
        /// <param name="entityValue">Old property value</param>
        /// <param name="modelValue">New property value</param>
        /// <param name="updateEntityProperty">Action performed if the newValue does not equal the old value</param>
        protected void UpdatePropertyIfChanged<T>(T entityValue, T modelValue, Action<T> updateEntityProperty)
        {
            if (!modelValue.Equals(entityValue))
            {
                updateEntityProperty(modelValue);
                this.IsDirty = true;
            }
        }
        /// <summary>
        /// Checks for a valid entity object.  Any specific relationships between object attributes should be checked here.
        /// 
        /// Each builder class must override this method.
        /// </summary>
        /// <returns>TGrue if the entity object is valid; false otherwise</returns>
        public virtual List<string> Validate(Entity entityValue) 
        {
            return Errors;
        }
        #endregion
        #region Builder helpers
        /// <summary>
        /// Tests if value is null or contains only white space.  If it does it adds the message to the
        /// list of messages.
        /// </summary>
        /// <param name="value">String to test</param>
        /// <param name="message">Error message</param>
        /// <param name="result">List of error messages</param>
        protected void TestStringForWhiteSpace(string value, string message, List<string> result)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result.Add(message);
            }
        }
        #endregion
    }

}
