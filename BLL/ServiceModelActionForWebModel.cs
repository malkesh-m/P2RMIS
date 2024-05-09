using System;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Dal.Interfaces;
using Sra.P2rmis.WebModels;

namespace Sra.P2rmis.Bll
{
    /// <summary>
    /// Generic object to perform Add, Modify and Deletes on an entity object
    /// described with a web model.
    /// </summary>
    /// <typeparam name="T">Entity object</typeparam>
    /// <typeparam name="M">Web model</typeparam>
    public abstract class ServiceModelActionForWebModel<T, M> : ServiceAction<T>
        where T : class, IStandardDateFields, new()
        where M : class, IEditable
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="unitOfWork">Unit of work object</param>
        /// <param name="repository">Entity repository</param>
        /// <param name="saveThis">Flag indicating if the framework should be saved after the operation</param>
        /// <param name="userId">Entity identifier of the user performing the operation</param>
        /// <param name="model">WebModel class object containing data</param>
        public virtual void InitializeAction(IUnitOfWork unitOfWork, IGenericRepository<T> repository, bool saveThis, int userId, IEditable model)
        {
            this.Model = model as M;
            base.InitializeAction(unitOfWork, repository, saveThis, userId);
        }
        #endregion
        #region Properties
        /// <summary>
        /// Property on WebModel that returns the entity index value.
        /// </summary>
        protected Func<M, int> Index { get; set; }
        /// <summary>
        /// The WebModel object containing data
        /// </summary>
        protected M Model { get; set; }
        #endregion
        #region Base class overwrites
        /// <summary>
        /// Returns the entity identifier for this entity
        /// </summary>
        /// <returns></returns>
        protected override int EntityId()
        {
            return Index(Model);
        }
        /// <summary>
        /// Indicates if the WebModel represents a delete
        /// </summary>
        protected override bool IsDelete
        {
            get { return Model.IsDeletable; }
        }
        /// <summary>
        /// Indicates if the WebModel has data
        /// </summary>
        protected override bool HasData
        {
            get { return Model.HasData(); }
        }
        /// <summary>
        /// Indicates if the WebModel is an add
        /// </summary>
        public override bool IsAdd
        {
            get { return false; }
        }
        #endregion
    }
}
