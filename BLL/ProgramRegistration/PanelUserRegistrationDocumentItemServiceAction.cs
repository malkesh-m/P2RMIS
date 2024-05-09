using System;
using Sra.P2rmis.Dal;

namespace Sra.P2rmis.Bll.ProgramRegistration
{
    /// <summary>
    /// 
    /// </summary>
    public class PanelUserRegistrationDocumentItemServiceAction : ServiceAction<PanelUserRegistrationDocumentItem>
    {
        #region Constructor & set up
        /// <summary>
        /// Constructor
        /// </summary>
        public PanelUserRegistrationDocumentItemServiceAction()
        {
        }
        /// <summary>
        /// Initialize the action's data values
        /// </summary>
        public void Populate(int panelUserRegistrationDocumentId, int panelUserRegisterationDocumentItemId, int registrationDocumentId, string value)
        {
            this.PanelUserRegistrationDocumentId = panelUserRegistrationDocumentId;
            this.RegistrationDocumentId = registrationDocumentId;
            this.Value = value;
            this.EntityIdentifier = panelUserRegisterationDocumentItemId;
            //
            //  Initialize this ServiceAction's property to indicate the data changed.
            //
            this.ItemValueChanged = false;
        }
        #endregion
        #region Properties
        /// <summary>
        /// The key
        /// </summary>
        private int RegistrationDocumentId { get; set; }
        /// <summary>
        /// The value
        /// </summary>
        private string Value { get; set; }
        /// <summary>
        /// Parent entity for the PanelUserRegistrationDocumentItem entity
        /// </summary>
        private int PanelUserRegistrationDocumentId { get; set; }
        /// <summary>
        /// Indicates if the PanelUserRegistrationDocumentItem value has changed
        /// </summary>
        public bool ItemValueChanged { get; set; }
        #endregion
        #region Required Overrides
        /// <summary>
        /// Populate the UserApplicationComment entity with information in the ServiceAction.
        /// </summary>
        protected override void Populate(PanelUserRegistrationDocumentItem entity)
        {
            entity.Populate(this.PanelUserRegistrationDocumentId, this.RegistrationDocumentId, this.Value);
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
        protected override bool IsDelete
        {
            get { return false; }
        }
        #endregion
        #region Optional Overrides
        /// <summary>
        /// Optional post add processing.                
        /// </summary>
        protected override void PreAdd()
        {
            //
            // we deal with one special case, strings.  If the value coming from the UI is a string it will 
            // be an empty string instead of a null.
            //
            this.ItemValueChanged = (this.Value == "" || this.Value == null)? false: true;
        }
        /// <summary>
        /// Optional post modify processing
        /// </summary>
        /// <param name="entity">the PanelUserRegistrationDocumentItem entity before any processing</param>
        protected override void PreModify(PanelUserRegistrationDocumentItem entity)
        {
            this.ItemValueChanged = !string.Equals(entity.Value, this.Value, StringComparison.OrdinalIgnoreCase);
          }
        #endregion
    }
}
