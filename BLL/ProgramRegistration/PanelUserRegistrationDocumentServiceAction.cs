using System;
using System.Collections.Generic;
using Sra.P2rmis.Dal;
using Sra.P2rmis.CrossCuttingServices;

namespace Sra.P2rmis.Bll.ProgramRegistration
{
    public class PanelUserRegistrationDocumentServiceAction: ServiceAction<PanelUserRegistrationDocument>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public PanelUserRegistrationDocumentServiceAction()
        {
        }
        /// <summary>
        /// Initialize the action's data values
        /// </summary>
        public virtual void Populate(bool oneOrMoreItemsChanged)
        {
            this.OneOrMoreItemsChanged = oneOrMoreItemsChanged;
        }
        #endregion
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        private bool OneOrMoreItemsChanged { get; set;}
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the UserApplicationComment entity with information in the ServiceAction.
        /// </summary>
        /// <param name="entity">The entity to populate</param>
        protected override void Populate(PanelUserRegistrationDocument entity)
        {  
            entity.Populate((this.OneOrMoreItemsChanged) ? GlobalProperties.P2rmisDateTimeNow : entity.DateCompleted);
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
                // null or the empty string should be stored.
                //
                return true;
            }
        }
        /// <summary>
        /// Indicates if the data represents a delete.
        /// </summary>
        protected override bool IsDelete { get { return false; } }
        /// <summary>
        /// Indicates if the data represents an add.
        /// </summary>
        public override bool IsAdd  { get { return false; } }
        #endregion
    }
    /// <summary>
    /// Service action for processing the updates to PanelUserRegistrationDocuments for the "Confirm" tab of the
    /// program registration wizard.
    /// </summary>
    public class PanelUserRegistrationDocumentCommitServiceAction: PanelUserRegistrationDocumentServiceAction
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public PanelUserRegistrationDocumentCommitServiceAction()
        {
        }
        /// <summary>
        /// Initialize the action's data values
        /// </summary>
        /// <param name="panelUserRegistrationDocumentId">PanelUserRegistrationDocument entity identifier</param>
        /// <param name="isConfirmed">Indicates if the document is confirmed</param>
        public void Populate(int panelUserRegistrationDocumentId, string isConfirmed)
        {
            this.IsConfirmed = isConfirmed;
            this.EntityIdentifier = panelUserRegistrationDocumentId;
            this.Document = null;
        }
        #endregion
        #region Properties
        /// <summary>
        /// Indicates if the document is confirmed<
        /// </summary>
        private string IsConfirmed { get; set; }
        /// <summary>
        /// The PanelUserRegistrationDocument under edit made available to the caller environment.
        /// </summary>
        public PanelUserRegistrationDocument Document { get; private set; }
        #endregion
        #region Required overrides
        /// <summary>
        /// Populate the PanelUserRegistrationDocument entity with information in the ServiceAction.
        /// </summary>
        /// <param name="entity">The entity to populate</param>
        protected override void Populate(PanelUserRegistrationDocument entity)
        {
            bool result = false;
            Boolean.TryParse(this.IsConfirmed, out result);
            
            if ((!entity.DateSigned.HasValue) & (result))
            {
                entity.DateSigned = GlobalProperties.P2rmisDateTimeNow;
                entity.SignedBy = UserId;
            }
        }
        /// <summary>
        /// Make the entity under edit available.
        /// </summary>
        /// <param name="entity">PanelUserRegistrationDocument entity under edit</param>
        protected override void PostModify(PanelUserRegistrationDocument entity)
        {
            this.Document = entity;
        }
        #endregion
    }
    /// <summary>
    /// Service action for processing the updates to PanelUserRegistrationDocuments for registration contracts that are
    /// signed off line.
    /// </summary>
    public class PanelUserRegistrationDocumentSignedOffLineServiceAction: PanelUserRegistrationDocumentServiceAction
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public PanelUserRegistrationDocumentSignedOffLineServiceAction()
        {
        }
        /// <summary>
        /// Override Populate with this signature.
        /// </summary>
        public override void Populate(bool oneOrMoreItemsChanged)
        {
            throw new System.InvalidOperationException("Populate(bool oneOrMoreItemsChanged) is not supported for PanelUserRegistrationDocumentSignedOffLineServiceAction");
        }
        #endregion
        #region Required overrides
        /// <summary>
        /// Populate the PanelUserRegistrationDocument entity with information in the ServiceAction.
        /// </summary>
        /// <param name="entity">The entity to populate</param>
        protected override void Populate(PanelUserRegistrationDocument entity)
        {
            DateTime now = GlobalProperties.P2rmisDateTimeNow;

            entity.DocumentFile = null;
            entity.SignedOfflineFlag = true;
            entity.SignedBy = this.UserId;
            entity.DateCompleted = now;
            entity.DateSigned = now;
        }
        #endregion
    }
    #region MyRegion
    /// <summary>
    /// PanelUserRegistrationDocumentServiceAction version to add a PanelUserRegistrationDocument entity.
    /// </summary>
    public class PanelUserRegistrationDocumentAddServiceAction : PanelUserRegistrationDocumentServiceAction
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public PanelUserRegistrationDocumentAddServiceAction()
        {
        }
        /// <summary>
        /// Populate the ServiceAction to add a PanelUserRegistrationDocument with the specified value
        /// </summary>
        /// <param name="panelUserRegistrationId">PanelUserRegistration entity identifier</param>
        /// <param name="clientRegistrationDocumentId">ClientRegistrationDocument entity identifier</param>
        /// <param name="panelUserRegistrationDocumentItems">Collection of PanelUserRegistrationDocumentItems to add</param>
        public void Populate(int panelUserRegistrationId, int clientRegistrationDocumentId, ICollection<PanelUserRegistrationDocumentItem> panelUserRegistrationDocumentItems, bool? signedOfflineFlag)
        {
            this.PanelUserRegistrationId = panelUserRegistrationId;
            this.ClientRegistrationDocumentId = clientRegistrationDocumentId;
            this.PanelUserRegistrationDocumentItems = panelUserRegistrationDocumentItems;
            this.SignedOfflineFlag = signedOfflineFlag;
        }
        /// <summary>
        /// Override Populate with this signature.
        /// </summary>
        public override void Populate(bool oneOrMoreItemsChanged)
        {
            throw new System.InvalidOperationException("Populate(bool oneOrMoreItemsChanged) is not supported for PanelUserRegistrationDocumentSignedOffLineServiceAction");
        }
        #endregion
        #region Properties
        /// <summary>
        /// PanelUserRegistration entity identifier for Added PanelUserRegistrationDocument
        /// </summary>
        private int PanelUserRegistrationId { get; set; }
        /// <summary>
        /// ClientRegistrationDocument entity identifier for Added PanelUserRegistrationDocument
        /// </summary>
        private int ClientRegistrationDocumentId { get; set; }
        /// <summary>
        /// PanelUserRegistrationDocumentItems of Added PanelUserRegistrationDocument
        /// </summary>
        private ICollection<PanelUserRegistrationDocumentItem> PanelUserRegistrationDocumentItems { get; set; }
        /// <summary>
        /// Indicates if the registration document was signed off line.
        /// </summary>
        private bool? SignedOfflineFlag { get; set; }
        #endregion
        #region Required overrides
        /// <summary>
        /// Indicates if the PanelUserRegistrationDocumentItem has data.  Since this is supposed to 
        /// be an Add we expect it will have data.
        /// </summary>
        protected override bool HasData { get { return true; } }
        /// <summary>
        /// Overridden IsAdd.  Indicates when a new PanelUserRegistrationDocument entity should be
        /// created.
        /// </summary>
        public override bool IsAdd { get { return true; } }
        /// <summary>
        /// Populate the PanelUserRegistrationDocument entity with information in the ServiceAction.
        /// </summary>
        /// <param name="entity">The entity to populate</param>
        protected override void Populate(PanelUserRegistrationDocument entity)
        {
            entity.PanelUserRegistrationId = this.PanelUserRegistrationId;
            entity.ClientRegistrationDocumentId = this.ClientRegistrationDocumentId;
            entity.PanelUserRegistrationDocumentItems = this.PanelUserRegistrationDocumentItems;
            entity.SignedOfflineFlag = this.SignedOfflineFlag;
        }
        #endregion
    }

    /// <summary>
    /// PanelUserRegistrationDocumentServiceAction version to delete a PanelUserRegistrationDocument entity.
    /// </summary>
    public class PanelUserRegistrationDocumentDeleteServiceAction : PanelUserRegistrationDocumentServiceAction
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public PanelUserRegistrationDocumentDeleteServiceAction()
        {
        }
        /// <summary>
        /// Override Populate with this signature.
        /// </summary>
        public override void Populate(bool oneOrMoreItemsChanged)
        {
            throw new System.InvalidOperationException("Populate(bool oneOrMoreItemsChanged) is not supported for PanelUserRegistrationDocumentDeleteServiceAction");
        }
        #endregion
        #region Properties
        #endregion
        #region Required overrides
        /// <summary>
        /// Indicates if the data represents a delete.
        /// </summary>
        protected override bool IsDelete { get { return true; } }
        #endregion
    }
    #endregion
}
