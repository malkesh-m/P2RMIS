using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.Setup.Actions
{
    /// <summary>
    /// Object to perform Add, Modify and Deletes on ClientAwardType entities
    /// </summary>
    internal class ClientAwardTypeServiceAction : ServiceAction<ClientAwardType>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public ClientAwardTypeServiceAction() { }
        #endregion
        #region Attributes
        /// <summary>
        /// This is the CRUD'ed ClientAwardType
        /// </summary>
        public ClientAwardType CRUDedEntity { get; set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// We tell the service action how to populate the entity with the data.
        /// </summary>
        /// <param name="entity">ClientAwardType entity being populated</param>
        protected override void Populate(ClientAwardType entity)
        {
            this.Block.Populate(this.UserId, entity);
        }
        /// <summary>
        /// And we tell it how to determine if we have data
        /// </summary>
        protected override bool HasData { get { return this.Block.HasData(); } }
        #endregion
        #region Optional Overrides
        /// <summary>
        /// What happens after an add is done.
        /// </summary>
        protected override void PostAdd(ClientAwardType entity)
        {
            //
            // And we remember the ClientAwardType we just created.
            //
            this.CRUDedEntity = entity;
        }
        #endregion
    }
}
