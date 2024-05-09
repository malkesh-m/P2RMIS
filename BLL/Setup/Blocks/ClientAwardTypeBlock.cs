using Sra.P2rmis.Dal;
using System;

namespace Sra.P2rmis.Bll.Setup.Blocks
{
    /// <summary>
    /// Crud block to use for ClientAwardType ServiceAction.
    /// </summary>
    internal class ClientAwardTypeBlock : CrudBlock<ClientAwardType>, ICrudBlock
    {
        #region Construction & Setup
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="clientId">Client entity identifier of CLient owning this type</param>
        /// <param name="awardAbbreviation">Award abbreviation</param>
        /// <param name="awardDescription">Award description</param>
        /// <param name="mechanismRelationshipTypeId"></param>
        /// <param name="parentAwardTypeId"></param>
        public ClientAwardTypeBlock(int clientId, string awardAbbreviation, string awardDescription, Nullable<int> mechanismRelationshipTypeId, Nullable<int> parentAwardTypeId)
        {
            this.ClientId = clientId;
            this.AwardAbbreviation = awardAbbreviation;
            this.AwardDescription = awardDescription;
            this.MechanismRelationshipTypeId = mechanismRelationshipTypeId;
            this.ParentAwardTypeId = parentAwardTypeId;
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Client entity identifier of CLient owning this type
        /// </summary>
        private int ClientId { get; set; }
        /// <summary>
        /// Award abbreviation<
        /// </summary>
        private string AwardAbbreviation { get; set; }
        /// <summary>
        /// Award description<
        /// </summary>
        private string AwardDescription { get; set; }
        /// <summary>
        /// MechanismRelationshipTypeId identifier.  Only used for creation of Pre-App awards if they do not exist
        /// </summary>
        private Nullable<int> MechanismRelationshipTypeId { get; set;}
        /// <summary>
        /// ClientAwardType identifier of parent 
        /// </summary>
        private Nullable<int> ParentAwardTypeId { get; set; }
        #endregion
        #region Services
        /// <summary>
        /// Configures block for an Add operation
        /// </summary>
        internal void ConfigureAdd()
        {
            IsAdd = true;
        }
        /// <summary>
        /// Sets individual properties on the CRUD entity.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <param name="entity">ClientAwardType to populate</param>
        internal override void Populate(int userId, ClientAwardType entity)
        {
            entity.AwardAbbreviation = this.AwardAbbreviation;
            entity.AwardDescription = this.AwardDescription;
            entity.ClientId = this.ClientId;
            entity.MechanismRelationshipTypeId = MechanismRelationshipType.Indexes.PreApplication;
            entity.ParentAwardTypeId = this.ParentAwardTypeId;
        }
        #endregion
    }
}
