using System.Collections.Generic;

namespace Sra.P2rmis.WebModels.ProgramRegistration
{
    /// <summary>
    /// Data model for the registration workflow
    /// </summary>
    public class RegistrationWorkflow : IRegistrationWorkflow, IWizardModel
    {
        /// <summary>
        /// The workflow steps
        /// </summary>
        public Dictionary<string, RegistrationStep> WorkflowSteps { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
        public RegistrationWorkflow()
        {
            WorkflowSteps = new Dictionary<string, RegistrationStep>();
        }
    }
    /// <summary>
    /// Workflow registration step
    /// </summary>
    public class RegistrationStep
    {
        #region Constructor & set up
        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="sortOrder">Workflow step sort order</param>
        /// <param name="documentId">Document identifier</param>
        /// <param name="documentName">Document name</param>
        /// <param name="isRequired">Indicates if the step is required</param>
        public RegistrationStep(int sortOrder, int documentId, string documentName, bool isRequired)
        {
            this.SortOrder = sortOrder;
            this.DocumentId = documentId;
            this.DocumentName = documentName;
            this.IsRequired = isRequired;
            this.ConfirmationText = string.Empty;
        }
        /// <summary>
        /// Construction
        /// </summary>
        /// <param name="sortOrder">Workflow step sort order</param>
        /// <param name="documentName">Document name</param>
        /// <param name="isRequired">Indicates if the step is required</param>
        /// <param name="confirmationText">Document confirmation text</param>
        public RegistrationStep(int sortOrder, int documentId, string documentName, bool isRequired, string confirmationText): this(sortOrder, documentId, documentName, isRequired)
        {
            this.ConfirmationText = confirmationText;
        }
        #endregion
        /// <summary>
        /// Registration workflow step sort order
        /// </summary>
        public int SortOrder { get; private set;}
        /// <summary>
        /// Document identifier
        /// </summary>
        public int DocumentId { get; private set; }
        /// <summary>
        /// Workflow step name
        /// </summary>
        public string DocumentName { get; private set; }
        /// <summary>
        /// Document version
        /// </summary>
        public string DocumentVersion { get; set; }
        /// <summary>
        /// Indicates the workflow step is required
        /// </summary>
        public bool IsRequired { get; private set; }
        /// <summary>
        /// Ready for signature when all required fields are filled
        /// </summary>
        public bool IsReadyForSignature { get; set; }
        /// <summary>
        /// Indicates whether the document is signed
        /// Null if not applicable
        /// </summary>
        public bool? IsSigned { get; set; }
        /// <summary>
        /// Indicates whether the document is signed offline
        /// </summary>
        public bool IsSignedOffLine { get; set; }
        /// <summary>
        /// Whether the document was bypassed
        /// </summary>
        public bool IsBypassed { get; set; }

        /// <summary>
        /// Whether the document was a custom uploaded pdf
        /// </summary>
        public bool IsCustomized { get; set; }
        /// <summary>
        /// The date/time signed
        /// </summary>
        public string DateSigned { get; set; }
        /// <summary>
        /// The user's name signed
        /// </summary>
        public string NameSigned { get; set; }
        /// <summary>
        /// Confirmation text for final page
        /// </summary>
        public string ConfirmationText { get; set; }
    }
}
