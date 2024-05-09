using Sra.P2rmis.Bll.ModelBuilders;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.CrossCuttingServices.FileServices;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.ProgramRegistration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sra.P2rmis.Bll.ProgramRegistration
{
    #region Service Interface
    /// <summary>
    /// ProgramRegistrationService provides the business related functions for
    /// the Program Registration Application.
    /// </summary>
    public interface IProgramRegistrationService
    {
        /// <summary>
        /// Retrieve the data for the registration wizard tab.
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <returns>The wizard workflow & a collection of DocumentForm objects</returns>
        IDictionary<string, IWizardModel> RegistrationWizardLoad(int panelUserAssignmentId);
        /// <summary>
        /// Get registration document
        /// </summary>
        /// <param name="registrationDocumentId">The registration document identifier</param>
        /// <returns>The IDocumentForm object</returns>
        IDocumentForm GetRegistrationDocument(int registrationDocumentId);
        /// <summary>
        /// Save the data on a Registration wizard tab.
        /// </summary>
        /// <param name="panelUserAssignmentId"></param>
        /// <param name="contents"></param>
        /// <param name="panelRegistrationDocumentId"></param>
        /// <param name="userId"></param>
        void SaveRegistrationForm(List<KeyValuePair<int, string>> contents, int panelRegistrationDocumentId, int userId);
        /// <summary>
        /// Save the html content of a document
        /// </summary>
        /// <param name="documentContent">The document's HTML content</param>
        /// <param name="panelRegistrationDocumentId">The panel registration document identifier</param>
        void SaveDocumentContent(string documentContent, int panelRegistrationDocumentId);
        /// <summary>
        /// Get the html content of a document
        /// </summary>
        /// <param name="panelRegistrationDocumentId">The panel registration document identifier</param>
        string GetDocumentContent(int panelRegistrationDocumentId);
        /// <summary>
        /// Key value identifying the workflow in the collection returned for the load.
        /// </summary>
        string WorkflowType { get; }
        /// <summary>
        /// Save the final page of the wizard, the confirmation 
        /// </summary>
        /// <param name="confirms">KeyValue pair identifying the documents that were confirmed</param>
        /// <param name="userId">User entity identifier of user confirming the documents</param>
        /// <returns>Container of ConfirmModels identifying the documents confirmed</returns>
        Container<IConfirmedModel> SaveConfirm(List<KeyValuePair<int, string>> confirms, int userId);
        /// <summary>
        /// Retrieve the start & completion dates.
        /// </summary>
        /// <param name="panelUserRegistratonDocumentId">PanelUserRegistrationDocument entity identifier of a registration document</param>
        /// <returns>Web Model consisting of the start & complete registration dates</returns>
        IPanelRegistrationDatesWebModel RetrieveRegistrationDates(int panelUserRegistratonDocumentId);
        /// <summary>
        /// Retrieve Program Registration Status for one or more session panels.
        /// </summary>
        /// <param name="sessionPanelIds">Collection of SessionPanel entity identifiers</param>
        /// <returns>Container of IProgramRegistrationWebModel objects</returns>
        Container<IProgramRegistrationWebModel> GetRegistrationStatus(ICollection<int> sessionPanelIds);
        /// <summary>
        /// Indicates if all the users registration are complete.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>False - the users registration are complete; True otherwise</returns>
        bool AreUsersRegistrationInComplete(int userId);
        /// <summary>
        /// Indicates if the users contract has been updated after registration is complete.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>False - contract not updated; True if it was</returns>
        bool IsRegistrationContractUpdated(int userId);
        /// <summary>
        /// Save the ht ml content of a document off line.
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        void SaveDocumentOffline(int panelUserAssignmentId, int userId);
        /// <summary>
        /// Checks users registration status on a per program basis.
        /// </summary>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>UserRegistrtionStatusModel</returns>
        UserRegistrtionStatusModel CheckUserRegistrationStatusForSpecifiProgram(int programYearId, int userId);
        /// <summary>
        /// Saves contract specific document content
        /// </summary>
        /// <param name="panelUserRegistrationDocumentId">Document identifier</param>
        /// <param name="consultantFeeAmount">Amount paid specified by contract</param>
        /// <param name="userId">User saving the contract content</param>
        /// <param name="isContractCustomized">Whether the contract document is customized (pdf content uploaded by user)</param>
        /// <param name="signatureBlock">Html representation of the users signature</param>
        /// <param name="baseUrl">Base URL of the web application</param>
        /// <param name="depPath">Path to dep file for html to pdf conversion</param>
        void SaveContractContentOnSign(int panelUserRegistrationDocumentId, decimal? consultantFeeAmount, int userId, bool isContractCustomized, string signatureBlock, string baseUrl, string depPath);
        /// <summary>
        /// Saves contract specific document content.
        /// </summary>
        /// <param name="panelUserRegistrationDocumentId">Document identifier</param>
        /// <param name="contractStatusId">Contract status identifier</param>
        /// <param name="feeAmount">Amount paid specified by contract</param>
        /// <param name="bypassReason">Reason to bypass contract</param>
        /// <param name="contractContents">Binary represenation of the custom contract</param>
        /// <param name="userId">User saving the contract content</param>
        /// <param name="baseUrl">Base URL for pdf processing (selectPdf)</param>
        /// <param name="depPath">Dep path for pdf processing (selectPdf)</param>
        void SaveContractContent(int panelUserRegistrationDocumentId, int contractStatusId, decimal? feeAmount, string bypassReason, byte[] contractContents, int userId, string baseUrl, string depPath);
        /// <summary>
        /// Gets the file contents and user=facing file name for a registration document
        /// </summary>
        /// <param name="registrationDocumentId">Identifier for a registration document instance</param>
        /// <param name="baseUrl">Base URL of application</param>
        /// <param name="depPath">Dep Path for select pdf</param>
        /// <returns>Tuple containing file name and byte array of pdf contents</returns>
        (string Name, byte[] FileContents) GetRegistrationDocumentFile(int registrationDocumentId, string baseUrl, string depPath);
    }
    #endregion
    #region Service Implementation
    /// <summary>
    /// ProgramRegistrationService provides services to perform business related functions for
    /// the ProgramRegistration Application.
    /// </summary>
    public class ProgramRegistrationService : ServerBase, IProgramRegistrationService
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public ProgramRegistrationService()
        {
            UnitOfWork = new UnitOfWork();
        }
        #endregion
        #region Attributes
        /// <summary>
        /// Key value identifying the workflow in the collection returned for the load.
        /// </summary>
        public string WorkflowType { get { return "w"; } }
        /// <summary>
        /// Initial size for RegistrationWizardLoad return container.
        /// </summary>
        private int WizardResultSize { get { return 5; } }
        /// <summary>
        /// Gets the short name of the ack nda.
        /// </summary>
        /// <value>
        /// The short name of the ack nda.
        /// </value>
        private string AckNdaShortName { get { return "AckNDA";  } }
        /// <summary>
        /// Gets the short name of the bias coi.
        /// </summary>
        /// <value>
        /// The short name of the bias coi.
        /// </value>
        private string BiasCoiShortName { get { return "BiasCOI"; } }
        /// <summary>
        /// Gets the short name of the contract.
        /// </summary>
        /// <value>
        /// The short name of the contract.
        /// </value>
        private string ContractShortName { get { return "Contract"; } }
        /// <summary>
        /// Gets the short name of the emergency contact.
        /// </summary>
        /// <value>
        /// The short name of the emergency contact.
        /// </value>
        private string EmContactShortName { get { return "EmContact";  } }
        /// <summary>
        /// Contains mapping between the individual tabs & the key/value pairs on the tab.
        /// </summary>
        private readonly Dictionary<string, List<int>> ReviewerDocumentItemToTabMapping = 
            new Dictionary<string, List<int>>() {
                    {ClientRegistrationDocument.DocumentRoutes.Acknowledgement, new List<int> {
                            RegistrationDocumentItem.Indexes.ConsultantFeeAccepted, 
                            RegistrationDocumentItem.Indexes.BusinessCategory}},
                    {ClientRegistrationDocument.DocumentRoutes.AcknowledgementCprit, new List<int> {
                            RegistrationDocumentItem.Indexes.ConsultantFeeAccepted, 
                            RegistrationDocumentItem.Indexes.BusinessCategory}},
                    {ClientRegistrationDocument.DocumentRoutes.BiasCoi, new List<int> {
                            RegistrationDocumentItem.Indexes.FinancialDisclosure, 
                            RegistrationDocumentItem.Indexes.FinancialDisclosureDetails,
                            RegistrationDocumentItem.Indexes.AdditionalDisclosure,
                            RegistrationDocumentItem.Indexes.AdditionalDisclosureDetails
                        }},
                    {ClientRegistrationDocument.DocumentRoutes.BiasCoiCprit, new List<int> {}},
                    {ClientRegistrationDocument.DocumentRoutes.Contract, new List<int> {}},
                    {ClientRegistrationDocument.DocumentRoutes.ContractCprit, new List<int> {}},
                    {ClientRegistrationDocument.DocumentRoutes.EmContact, new List<int> {}}
                };
        /// <summary>
        /// Object for Registration start date to lock on.
        /// </summary>
        private Object registrationStartDateLock = new Object();
        #endregion
        #region Services
        /// <summary>
        /// Retrieve the data for the registration wizard tab.
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <returns>The wizard workflow & a collection of DocumentForm objects</returns>
        public IDictionary<string, IWizardModel> RegistrationWizardLoad(int panelUserAssignmentId)
        {
            this.ValidateInteger(panelUserAssignmentId, "ProgramRegistrationService.LoadRegistration", "panelUserAssignmentId");

            IDictionary<string, IWizardModel> result = new Dictionary<string, IWizardModel>(WizardResultSize);

            PanelUserAssignment panelUserAssignmentEntity = UnitOfWork.PanelUserAssignmentRepository.GetByID(panelUserAssignmentId);
            //
            // First we build the workflow
            //
            result[WorkflowType] = ConstructWorkflowModel(panelUserAssignmentEntity);
            //
            // Then we get the common information that is the same across the wizard's steps
            //
            IRegistrationDocument common = PopulateRegistrationDocument(panelUserAssignmentEntity);
            //
            // Now we construct the registration documents
            //
            return CreateWizardRegistrationDocuments(panelUserAssignmentEntity, common, result);
        }
        /// <summary>
        /// Get registration document
        /// </summary>
        /// <param name="registrationDocumentId">The registration document identifier</param>
        /// <returns>The IDocumentForm object</returns>
        public IDocumentForm GetRegistrationDocument(int registrationDocumentId)
        {
            this.ValidateInteger(registrationDocumentId, "ProgramRegistrationService.GetRegistrationDocument", "registrationDocumentId");
            //
            // Pull in the main entities for populate method
            //
            PanelUserRegistrationDocument registrationDocumentEntity = UnitOfWork.PanelUserRegistrationDocumentRepository.GetByID(registrationDocumentId);
            //
            // We get the common information that is the same across the wizard's steps
            //
            IRegistrationDocument common = PopulateRegistrationDocument(registrationDocumentEntity.PanelUserRegistration.PanelUserAssignment);
            //
            // We want the user who signed the document
            //
            string signedByName = GetSignedbyName(registrationDocumentEntity);
            string fileName = GetFileName(registrationDocumentEntity);
            //
            // Only populate the contract data if the current doc is a contract
            //      
            IContractModel contractModel = new ContractModel();
            if (registrationDocumentEntity.ClientRegistrationDocument.RegistrationDocumentTypeId ==
                    RegistrationDocumentType.Indexes.ContractualAgreement)
            {
                contractModel.IsContractCustomized = IsContractCustomized(registrationDocumentEntity.PanelUserRegistrationDocumentContracts.FirstOrDefault()?.ContractStatusId);
                contractModel.ContractFileLocation = registrationDocumentEntity.PanelUserRegistrationDocumentContracts.FirstOrDefault()?.ContractFileLocation;
            }
            IDocumentForm documentFormEntity = new DocumentForm(common);
            documentFormEntity.Populate(registrationDocumentEntity.ClientRegistrationDocument.DocumentRoute, registrationDocumentEntity.PanelUserRegistrationDocumentId, 
                registrationDocumentEntity.ClientRegistrationDocument.DocumentVersion.ToString(), registrationDocumentEntity.ClientRegistrationDocument.DocumentUpdateDate, 
                registrationDocumentEntity.DateSigned, registrationDocumentEntity.SignedBy, signedByName, registrationDocumentEntity.DocumentFile, registrationDocumentEntity.SignedOfflineFlag, fileName, contractModel);
            
            return documentFormEntity;
        }

        /// <summary>
        /// Gets the file contents and user=facing file name for a registration document
        /// </summary>
        /// <param name="registrationDocumentId">Identifier for a registration document instance</param>
        /// <param name="baseUrl">Base URL of application</param>
        /// <param name="depPath">Dep Path for select pdf</param>
        /// <returns>Tuple containing file name and byte array of pdf contents</returns>
        public (string Name, byte[] FileContents) GetRegistrationDocumentFile(int registrationDocumentId, string baseUrl, string depPath)
        {
            byte[] fileContents = null;
            string name = Guid.NewGuid().ToString();
            IDocumentForm doc = GetRegistrationDocument(registrationDocumentId);
            //we first check if the file generated is a customized contract, these are already pdf and require no conversion
            if (doc.ContractData.IsContractCustomized)
            {
                fileContents = S3Service.GetFileContents(doc.ContractData.ContractFileLocation, ConfigManager.S3ContractFolderName);
            }
            else
            {
                var htmlContent = doc.DocumentContent;
                var footerText = "Document ID: " + registrationDocumentId.ToString();
                fileContents = PdfServices.CreatePdf(htmlContent, footerText, baseUrl, depPath);
            }
            name = string.Format("{0}{1}", doc.FileName, ".pdf");
            return (name, fileContents);
        }
        /// <summary>
        /// Create the wizard's tab document representation
        /// </summary>
        /// <param name="panelUserAssignmentEntity">PanelUserAssignment entity object for the user on this panel</param>
        /// <param name="common">RegistrationDocument populated with data values that are the same across all tabs</param>
        /// <param name="result">Service returned collection</param>
        /// <returns>The wizard workflow & a collection of DocumentForm objects</returns>
        internal virtual IDictionary<string, IWizardModel> CreateWizardRegistrationDocuments(PanelUserAssignment panelUserAssignmentEntity, IRegistrationDocument common, IDictionary<string, IWizardModel> result)
        {
            var r = panelUserAssignmentEntity.PanelUserRegistrations.First();

            foreach (var registrationDocumentEntity in r.PanelUserRegistrationDocuments)
            {
                //
                // We want the user who signed the document.  However there are cases where the document is not singed yet.
                // So we need to test.
                //
                string signedByName = GetSignedbyName(registrationDocumentEntity);
                IContractModel contractData = new ContractModel();
                //
                // Only populate the contract data if the current document is the contract
                if (registrationDocumentEntity.ClientRegistrationDocument.RegistrationDocumentTypeId ==
                    RegistrationDocumentType.Indexes.ContractualAgreement)
                {
                    UserInfo userInfoEntity = panelUserAssignmentEntity.User.UserInfoes.First();
                    contractData = new ContractModel(common.ClientAbbreviation, common.FiscalYear, common.ProgramDescription, common.PanelName,
                        userInfoEntity.VendorNameOrFullNameWithDegree(), panelUserAssignmentEntity.ClientParticipantType.ParticipantTypeName, panelUserAssignmentEntity.PeriodOfPerformanceStartDate(), panelUserAssignmentEntity.PeriodOfPerformanceEndDate(),
                        panelUserAssignmentEntity.PanelManagerList(), panelUserAssignmentEntity.ConsultantFeeText(), panelUserAssignmentEntity.DescriptionOfWork(), userInfoEntity.W9AddressOrPrimaryUserAddress().Address1,
                        userInfoEntity.W9AddressOrPrimaryUserAddress().Address2, userInfoEntity.W9AddressOrPrimaryUserAddress().Address3, userInfoEntity.W9AddressOrPrimaryUserAddress().Address4, userInfoEntity.W9AddressOrPrimaryUserAddress().City,
                        userInfoEntity.W9AddressOrPrimaryUserAddress().StateAbbreviation(), userInfoEntity.W9AddressOrPrimaryUserAddress().Zip, userInfoEntity.W9AddressOrPrimaryUserAddress().Country?.CountryAbbreviation, userInfoEntity.IsW9Verified(), panelUserAssignmentEntity.ConsultantFeeAmount(),
                        IsContractBypassed(panelUserAssignmentEntity.ContractStatusId()), IsContractCustomized(panelUserAssignmentEntity.ContractStatusId()));
                }
                DocumentForm documentFormEntity = new DocumentForm(common);
                if (registrationDocumentEntity.DateSigned != null)
                    documentFormEntity.Populate(registrationDocumentEntity.ClientRegistrationDocument.DocumentRoute, registrationDocumentEntity.PanelUserRegistrationDocumentId, registrationDocumentEntity.ClientRegistrationDocument.DocumentVersion.ToString(), registrationDocumentEntity.ClientRegistrationDocument.DocumentUpdateDate,
                        registrationDocumentEntity.DateSigned, registrationDocumentEntity.SignedBy, signedByName, contractData, registrationDocumentEntity.DocumentFile, registrationDocumentEntity.SignedOfflineFlag);
                else
                    documentFormEntity.Populate(registrationDocumentEntity.ClientRegistrationDocument.DocumentRoute, registrationDocumentEntity.PanelUserRegistrationDocumentId, registrationDocumentEntity.ClientRegistrationDocument.DocumentVersion.ToString(), registrationDocumentEntity.ClientRegistrationDocument.DocumentUpdateDate,
                        registrationDocumentEntity.DateSigned, registrationDocumentEntity.SignedBy, signedByName, contractData, registrationDocumentEntity.SignedOfflineFlag);
                documentFormEntity.SetReviewerItemKeys(RegistrationDocumentItem.Indexes.FinancialDisclosure, RegistrationDocumentItem.Indexes.FinancialDisclosureDetails, RegistrationDocumentItem.Indexes.AdditionalDisclosure,
                                                       RegistrationDocumentItem.Indexes.AdditionalDisclosureDetails, RegistrationDocumentItem.Indexes.ConsultantFeeAccepted, RegistrationDocumentItem.Indexes.BusinessCategory);
                //
                // Now populate the key/value pairs.  First we reserve the values which are populated by nulls.  Then any specific values
                // overwrite the nulls.
                //
                ReviewerDocumentItemToTabMapping[documentFormEntity.FormKey].ForEach(x => documentFormEntity[x] = new DocumentItemRequired());
                registrationDocumentEntity.ClientRegistrationDocument.ClientRegistrationDocumentItems.ToList().ForEach(
                    x =>
                    {
                        documentFormEntity[x.RegistrationDocumentItemId] = new DocumentItemRequired(registrationDocumentEntity
                            .PanelUserRegistrationDocumentItems.Where(y => y.RegistrationDocumentItemId == x.RegistrationDocumentItemId).DefaultIfEmpty(new PanelUserRegistrationDocumentItem()).First().Value, 
                            GetRequiredFlag(registrationDocumentEntity, x), x.RequiredMessage);
                    });

                //
                // now add it to the list (key type may be temporary)
                //
                result[documentFormEntity.FormKey] = documentFormEntity;
            }
            return result;
        }
        /// <summary>
        /// Get the boolean flag whether the item is required or not
        /// </summary>
        /// <param name="registrationDocument">The panel user registration document identifier</param>
        /// <param name="registrationDocumentItemEntity">The client registration document item identifier</param>
        /// <returns>A boolean flag whether the item is required or not</returns>
        internal virtual bool GetRequiredFlag(PanelUserRegistrationDocument registrationDocument, ClientRegistrationDocumentItem registrationDocumentItemEntity)
        {
            // Handles conditionally required flag
            var requiredFlag = registrationDocumentItemEntity.RequiredFlag;
            if (registrationDocumentItemEntity.RegistrationDocumentItemId == RegistrationDocumentItem.Indexes.FinancialDisclosureDetails)
            {
                requiredFlag = registrationDocument.PanelUserRegistrationDocumentItems
                    .Any(x => x.RegistrationDocumentItemId == RegistrationDocumentItem.Indexes.FinancialDisclosure &&
                        x.Value != null && x.Value.ToLower() == "true");
            }
            else if (registrationDocumentItemEntity.RegistrationDocumentItemId == RegistrationDocumentItem.Indexes.AdditionalDisclosureDetails)
            {
                requiredFlag = registrationDocument.PanelUserRegistrationDocumentItems
                    .Any(x => x.RegistrationDocumentItemId == RegistrationDocumentItem.Indexes.AdditionalDisclosure &&
                        x.Value != null && x.Value.ToLower() == "true");
            }
            return requiredFlag;
        }
        /// <summary>
        /// Retrieve the full name of the user who signed the document, if it is signed.
        /// </summary>
        /// <param name="registrationDocumentEntity">PanelUserRegistrationDocument entity</param>
        /// <returns>Full name of the user who signed the document</returns>
        internal virtual string GetSignedbyName(PanelUserRegistrationDocument registrationDocumentEntity)
        {
            //
            // We want the user who signed the document.  However there are cases where the document is not singed yet.
            // So we need to test.
            //
            string result = string.Empty;
            if (registrationDocumentEntity.SignedBy.HasValue)
            {
                User userEntity = UnitOfWork.UserRepository.GetByID(registrationDocumentEntity.SignedBy.Value);
                result = userEntity.FullName();
            }
            return result;
        }
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="registrationDocumentEntity">The registration document entity.</param>
        /// <returns></returns>
        internal virtual string GetFileName(PanelUserRegistrationDocument registrationDocumentEntity)
        {
            return String.Format("{0}_{1}_{2}", GetDocumentOwnerUsername(registrationDocumentEntity),
                GetFormShortName(registrationDocumentEntity.ClientRegistrationDocument.DocumentRoute),
                registrationDocumentEntity.PanelUserRegistrationDocumentId);
        }

        internal virtual string GetFilePathForStorage(PanelUserRegistrationDocument registrationDocumentEntity)
        {
            var fileName = GetFileName(registrationDocumentEntity);
            var panelInfo = registrationDocumentEntity.PanelUserRegistration.PanelUserAssignment.SessionPanel;
            return String.Format("/{0}/{1}/{2}/{3}/{4}_{5}.pdf",
                    registrationDocumentEntity.ClientRegistrationDocument.ClientRegistration.ClientId,
                    panelInfo.GetFiscalYear(), panelInfo.GetProgramAbbreviation(), panelInfo.PanelAbbreviation, fileName, FileServices.GetTimestampForFileName(GlobalProperties.P2rmisDateTimeNow));
        }
        /// <summary>
        /// Gets the username of the owner of the document.
        /// </summary>
        /// <param name="registrationDocumentEntity">The registration document entity.</param>
        /// <returns></returns>
        internal virtual string GetDocumentOwnerUsername(PanelUserRegistrationDocument registrationDocumentEntity)
        {
            string result = registrationDocumentEntity.PanelUserRegistration.PanelUserAssignment.User.UserLogin;
            return result;
        }
        /// <summary>
        /// Gets the short name of the form.
        /// </summary>
        /// <param name="formKey">The form key.</param>
        /// <returns></returns>
        internal string GetFormShortName(string formKey)
        {
            string shortName = formKey;
            switch (formKey)
            {
                case ClientRegistrationDocument.DocumentRoutes.Acknowledgement:
                case ClientRegistrationDocument.DocumentRoutes.AcknowledgementCprit:
                    shortName = AckNdaShortName;
                    break;
                case ClientRegistrationDocument.DocumentRoutes.BiasCoi:
                case ClientRegistrationDocument.DocumentRoutes.BiasCoiCprit:
                    shortName = BiasCoiShortName;
                    break;
                case ClientRegistrationDocument.DocumentRoutes.Contract:
                case ClientRegistrationDocument.DocumentRoutes.ContractCprit:
                    shortName = ContractShortName;
                    break;
                case ClientRegistrationDocument.DocumentRoutes.EmContact:
                    shortName = EmContactShortName;
                    break;
                default:
                    break;
            }
            return shortName;
        }

        /// <summary>
        /// Construct the registration workflow model.
        /// </summary>
        /// <param name="panelUserAssignmentEntity">PanelUserAssignment entity</param>
        /// <returns>Registration workflow model</returns>
        internal virtual IWizardModel ConstructWorkflowModel(PanelUserAssignment panelUserAssignmentEntity)
        {
            RegistrationWorkflow result = new RegistrationWorkflow();

            var panelUserRegistrationDocuments = panelUserAssignmentEntity.PanelUserRegistrations.FirstOrDefault().PanelUserRegistrationDocuments;
            var clientRegistrationDocumentEntityCollection = panelUserRegistrationDocuments.Select(x => x.ClientRegistrationDocument).OrderBy(x => x.SortOrder);
            //
            // For each client document construct a workflow step & add it to the workflow.  The documents
            // were ordered when they were retrieved from the PanelUserRegistration and are constructed in order.
            //
            foreach(var clientRegistrationDocumentEntity in clientRegistrationDocumentEntityCollection)
            {
                var panelUserRegistrationDocumentId = panelUserRegistrationDocuments.Where(x => x.ClientRegistrationDocumentId == clientRegistrationDocumentEntity.ClientRegistrationDocumentId).FirstOrDefault().PanelUserRegistrationDocumentId;
                result.WorkflowSteps[clientRegistrationDocumentEntity.DocumentRoute] = new RegistrationStep(clientRegistrationDocumentEntity.SortOrder, panelUserRegistrationDocumentId, clientRegistrationDocumentEntity.DocumentName, clientRegistrationDocumentEntity.RequiredFlag, clientRegistrationDocumentEntity.ConfirmationText);
            }

            return result;
        }
        /// <summary>
        /// Retrieves information common to each client registration document.
        /// </summary>
        /// <param name="panelUserAssignmentEntity">PanelUserAssignment entity</param>
        /// <returns>RegistrationDocument entity populated with common information</returns>
        internal virtual IRegistrationDocument PopulateRegistrationDocument(PanelUserAssignment panelUserAssignmentEntity)
        {
            //
            // it is my assumption that a specific session panel belongs to one program panel even though it is represented by a collection.  I believe this is 
            // the same across all wizard web models
            //
            ProgramYear programYearEntity = panelUserAssignmentEntity.SessionPanel.ProgramPanels.First().ProgramYear;

            return new RegistrationDocument(
                                            programYearEntity.Year,
                                            programYearEntity.ClientProgram.ProgramAbbreviation,
                                            programYearEntity.ClientProgram.ProgramDescription,
                                            panelUserAssignmentEntity.SessionPanel.PanelAbbreviation,
                                            panelUserAssignmentEntity.SessionPanel.PanelName,
                                            programYearEntity.ClientProgram.Client.ClientAbrv,
                                            panelUserAssignmentEntity.PanelUserSessionPayRate() != null ||
                                            panelUserAssignmentEntity.PanelUserProgramPayRate() != null
                                            );
        }
        /// <summary>
        /// Save the data on a Registration wizard tab.
        /// </summary>
        /// <param name="formContents">Form contents as key/value pairs</param>
        /// <param name="panelRegistrationDocumentId">PanelRegistrationDocument entity identifier</param>
        /// <param name="userId">User entity identifier of the user saving the registration from</param>
        public void SaveRegistrationForm(List<KeyValuePair<int, string>> formContents, int panelRegistrationDocumentId, int userId)
        {
            ValidateSaveRegistrationFormParameters(formContents, panelRegistrationDocumentId, userId);
            //
            // Retrieve the PanelRegistrationDocument
            //
            var panelRegistrationDocumentEntity = UnitOfWork.PanelUserRegistrationDocumentRepository.GetByID(panelRegistrationDocumentId);
            //
            // Create the ServiceAction to perform the Crud operations & initialize it
            //
            PanelUserRegistrationDocumentItemServiceAction itemEditAction = new PanelUserRegistrationDocumentItemServiceAction();
            itemEditAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.PanelUserRegistrationDocumentItemRepository, ServiceAction<PanelUserRegistrationDocumentItem>.DoNotUpdate, 0, userId);
            //
            // Now for each entry in the "form" data passes in we match it to any existing PanelUserRegisterationDocumentItems, 
            // populate the ServiceAction & execute the action.
            //
            // It may seem an a bit odd way to initialize the flag below but is was a simple way to force an update to the PanelRegistrationDocument
            // to record the time the tab was last 'paged' through.
            //
            bool anyItemChanged = formContents.Count() == 0;//false;
            foreach (var documentItem in formContents)
            {
                int panelRegistrationDocumentItemId = MatchFormDataToPanelUserRegistrationdocumentItem(documentItem, panelRegistrationDocumentEntity.PanelUserRegistrationDocumentItems);
                itemEditAction.Populate(panelRegistrationDocumentId, panelRegistrationDocumentItemId, documentItem.Key, documentItem.Value);
                itemEditAction.Execute();
                anyItemChanged |= itemEditAction.ItemValueChanged;
            }
            PanelUserRegistrationDocumentServiceAction documentEditAction = new PanelUserRegistrationDocumentServiceAction();
            documentEditAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.PanelUserRegistrationDocumentRepository, ServiceAction<PanelUserRegistrationDocument>.DoNotUpdate, panelRegistrationDocumentId, userId);
            documentEditAction.Populate(anyItemChanged);
            documentEditAction.Execute();
            //
            // Now we update the registration start time
            //
            UpdateRegistartionStartDateTime(panelRegistrationDocumentEntity.PanelUserRegistration, userId);            
            //
            // Now that we have added all of the document items for this document, we save them
            //
            UnitOfWork.Save();
        }
        /// <summary>
        /// Save the html content of a document
        /// </summary>
        /// <param name="documentContent">The document's HTML content</param>
        /// <param name="panelRegistrationDocumentId">The panel registration document identifier</param>
        public void SaveDocumentContent(string documentContent, int panelRegistrationDocumentId)
        {
            ValidateSaveDocumentContentParameters(documentContent, panelRegistrationDocumentId);
            //
            // Retrieve the PanelRegistrationDocument
            //
            var panelRegistrationDocumentEntity = UnitOfWork.PanelUserRegistrationDocumentRepository.GetByID(panelRegistrationDocumentId);
            //
            // Update the html content
            //
            panelRegistrationDocumentEntity.DocumentFile = documentContent;
            //
            // Save the html content
            //
            UnitOfWork.Save();
        }
        /// <summary>
        /// Set the html content of a document to null and new signed date
        /// </summary>
        /// <param name="documentContent">The document's HTML content</param>
        /// <param name="panelRegistrationDocumentId">The panel registration document identifier</param>
        /// <param name="contractStatus">The contract status identifier</param>
        internal void SaveDocumentContentForModifiedContract(int panelRegistrationDocumentId, int contractStatus, int userid)
        {
            ValidateInt(panelRegistrationDocumentId, "ProgramRegristrationService.ValidateSaveDocumentContentParameters", "panelRegistrationDocumentId");
            ValidateInt(contractStatus, "ProgramRegristrationService.ValidateSaveDocumentContentParameters", "panelRegistrationDocumentId");
            //
            // Retrieve the PanelRegistrationDocument
            //
            var panelRegistrationDocumentEntity = UnitOfWork.PanelUserRegistrationDocumentRepository.GetByID(panelRegistrationDocumentId);
            //
            // Update the html content
            //
            if (contractStatus == ContractStatus.Keys.Bypass)
            {
                panelRegistrationDocumentEntity.DocumentFile = null;
                panelRegistrationDocumentEntity.DateSigned = GlobalProperties.P2rmisDateTimeNow;
                panelRegistrationDocumentEntity.SignedBy = userid;
            }
            else
            {
                panelRegistrationDocumentEntity.DocumentFile = null;
                panelRegistrationDocumentEntity.DateSigned = null;
                panelRegistrationDocumentEntity.SignedBy = null;
            }
        }
        /// <summary>
        /// Saves contract specific document content when a user signs their document
        /// </summary>
        /// <param name="panelUserRegistrationDocumentId">Document identifier</param>
        /// <param name="consultantFeeAmount">Amount paid specified by contract</param>
        /// <param name="userId">User saving the contract content</param>
        /// <param name="isContractCustomized">Whether the contract document is customized (pdf content uploaded by user)</param>
        /// <param name="signatureBlock">Html representation of the users signature</param>
        /// <param name="baseUrl">Base URL of the web application</param>
        /// <param name="depPath">Path to dep file for html to pdf conversion</param>
        public void SaveContractContentOnSign(int panelUserRegistrationDocumentId, decimal? consultantFeeAmount, int userId, bool isContractCustomized, string signatureBlock, string baseUrl, string depPath)
        {
            var panelRegistrationDocumentEntity = UnitOfWork.PanelUserRegistrationDocumentRepository.GetByID(panelUserRegistrationDocumentId);
            var panelRegistrationDocumentContractEntity = panelRegistrationDocumentEntity.PanelUserRegistrationDocumentContracts.FirstOrDefault();
            //if the contract is customized we also need to save the signed pdf as part of the save
            if (isContractCustomized)
            {
                ProcessCustomContractSignature(panelRegistrationDocumentContractEntity.ContractFileLocation, signatureBlock, baseUrl, depPath);
                Helper.UpdateModifiedFields(panelRegistrationDocumentContractEntity, userId);
            }
            //add/update contract entity with explicit consultantFee amount
            else if (panelRegistrationDocumentContractEntity != null)
            {
                panelRegistrationDocumentContractEntity.FeeAmount = consultantFeeAmount;
                Helper.UpdateModifiedFields(panelRegistrationDocumentContractEntity, userId);
            }
            else
            {
                PanelUserRegistrationDocumentContract newContractEntity = new PanelUserRegistrationDocumentContract();
                newContractEntity.Populate(consultantFeeAmount, ContractStatus.Keys.Original);
                Helper.UpdateCreatedFields(newContractEntity, userId);
                Helper.UpdateModifiedFields(newContractEntity, userId);
                panelRegistrationDocumentEntity.PanelUserRegistrationDocumentContracts.Add(newContractEntity);
                UnitOfWork.PanelUserRegistrationDocumentContractRepository.Add(newContractEntity);
            }
            //Save the changes
            UnitOfWork.Save();
        }
        /// <summary>
        /// Saves contract specific document content.
        /// </summary>
        /// <param name="panelUserRegistrationDocumentId">Document identifier</param>
        /// <param name="feeAmount">Amount paid specified by contract</param>
        /// <param name="bypassReason">Reason to bypass contract</param>
        /// <param name="contractContents">Binary represenation of the custom contract</param>
        /// <param name="userId">User saving the contract content</param>
        /// <param name="baseUrl">Base URL for pdf processing (selectPdf)</param>
        /// <param name="depPath">Dep path for pdf processing (selectPdf)</param>
        public void SaveContractContent(int panelUserRegistrationDocumentId, int contractStatusId, decimal? feeAmount, string bypassReason, byte[] contractContents,  int userId, string baseUrl, string depPath)
        {
            var panelRegistrationDocumentEntity = UnitOfWork.PanelUserRegistrationDocumentRepository.GetByID(panelUserRegistrationDocumentId);
            var panelRegistrationDocumentContractEntity = panelRegistrationDocumentEntity.PanelUserRegistrationDocumentContracts.FirstOrDefault();

            string documentPath = SaveCustomizedContractFile(contractStatusId, contractContents, panelRegistrationDocumentEntity, baseUrl, depPath);
            SaveDocumentContractInfo(contractStatusId, feeAmount, bypassReason, userId, panelRegistrationDocumentEntity, panelRegistrationDocumentContractEntity, documentPath);
            //if signed the customized contract resets signature (except bypass)
            if (panelRegistrationDocumentEntity.IsSigned() || contractStatusId == ContractStatus.Keys.Bypass)
            {
                // Set document HTML content to null
                SaveDocumentContentForModifiedContract(panelUserRegistrationDocumentId, contractStatusId, userId);
            }
            //Save all the changes
            UnitOfWork.Save();
        }

        /// <summary>
        /// Saves the file contents and returns the path it was stored
        /// </summary>
        /// <param name="contractStatusId">Current status of the contract</param>
        /// <param name="contractContents">Binary represenation of the custom contract</param>
        /// <param name="panelRegistrationDocumentEntity">Parent document entity object</param>
        /// <returns>Relative path the file is stored</returns>
        internal string SaveCustomizedContractFile(int contractStatusId, byte[] contractContents, PanelUserRegistrationDocument panelRegistrationDocumentEntity, string baseUrl, string depPath)
        {
            //first process any file based updates
            string documentPath = null;
            if (contractStatusId == ContractStatus.Keys.AddAddendum)
            {
                documentPath = GetFilePathForStorage(panelRegistrationDocumentEntity);
                var currentFile = GetRegistrationDocumentFile(panelRegistrationDocumentEntity.PanelUserRegistrationDocumentId, baseUrl, depPath);
                var mergedFile = PdfServices.MergePdf(currentFile.FileContents, contractContents);
                ProcessCustomContractUpload(documentPath, mergedFile);
            }
            else if (contractStatusId == ContractStatus.Keys.Replace)
            {
                documentPath = GetFilePathForStorage(panelRegistrationDocumentEntity);
                ProcessCustomContractUpload(documentPath, contractContents);
            }

            return documentPath;
        }

        /// <summary>
        /// Sets up the PanelUserRegistrationDocumentContract entity for save
        /// </summary>
        /// <param name="contractStatusId">Identifier for a contract status</param>
        /// <param name="feeAmount">Amount paid specified by contract</param>
        /// <param name="bypassReason">Reason to bypass contract</param>
        /// <param name="userId">User saving the contract content</param>
        /// <param name="panelRegistrationDocumentEntity">Parent document entity object</param>
        /// <param name="panelRegistrationDocumentContractEntity">Contract to be add/updated entity object</param>
        /// <param name="documentPath">Path the document is stored in S3</param>
        internal void SaveDocumentContractInfo(int contractStatusId, decimal? feeAmount, string bypassReason, int userId, PanelUserRegistrationDocument panelRegistrationDocumentEntity, PanelUserRegistrationDocumentContract panelRegistrationDocumentContractEntity, string documentPath)
        {
            if (panelRegistrationDocumentContractEntity != null)
            {
                panelRegistrationDocumentContractEntity.Populate(feeAmount, contractStatusId, bypassReason, documentPath);
                Helper.UpdateModifiedFields(panelRegistrationDocumentContractEntity, userId);
            }
            else
            {
                PanelUserRegistrationDocumentContract newContractEntity = new PanelUserRegistrationDocumentContract();
                newContractEntity.Populate(feeAmount, contractStatusId, bypassReason, documentPath);
                Helper.UpdateCreatedFields(newContractEntity, userId);
                Helper.UpdateModifiedFields(newContractEntity, userId);
                panelRegistrationDocumentEntity.PanelUserRegistrationDocumentContracts.Add(newContractEntity);
                UnitOfWork.PanelUserRegistrationDocumentContractRepository.Add(newContractEntity);
            }
        }

        /// <summary>
        /// Processes the contract signature for a custom contract in S3
        /// </summary>
        /// <param name="contractFileLocation">Relative location of contract file in S3</param>
        /// <param name="signatureBlock">Signature block to append</param>
        /// <param name="baseUrl">Base URL of site (for pdf conversion utility)</param>
        /// <param name="depPath">Path to the pdf conversion utility dep file</param>
        internal void ProcessCustomContractSignature(string contractFileLocation, string signatureBlock, string baseUrl, string depPath)
        {
            var contractFile = S3Service.GetFileContents(contractFileLocation, ConfigManager.S3ContractFolderName);
            var signatureBlockPdf = PdfServices.CreatePdf(signatureBlock, string.Empty, baseUrl, depPath);
            var signedContract = PdfServices.MergePdf(contractFile, signatureBlockPdf);
            S3Service.WriteFileContents(signedContract, contractFileLocation, ConfigManager.S3ContractFolderName);
        }

        /// <summary>
        /// Uploads the custom contract to it's destination
        /// </summary>
        /// <param name="contractFileLocation">File path relative to the base contract folder</param>
        /// <param name="contractContents">Binary represenation of the custom contract</param>
        internal void ProcessCustomContractUpload(string contractFileLocation, byte[] contractContents)
        {
            S3Service.WriteFileContents(contractContents, contractFileLocation, ConfigManager.S3ContractFolderName);
        }
        /// <summary>
        /// Mark a Contract document as being signed 'Off-line'
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        public void SaveDocumentOffline(int panelUserAssignmentId, int userId)
        {
            ValidateSaveDocumentOfflineParameters(panelUserAssignmentId, userId);
            //
            // First thing we need to do is to find the PanelUserRegistratiionDocument entity for the contract
            //
            PanelUserAssignment panelUserAssignmentEntity = UnitOfWork.PanelUserAssignmentRepository.GetByID(panelUserAssignmentId);
            //
            // Create the edit action to update the PanelUserRegistrationDocument to do all the heavy lifting
            //
            PanelUserRegistrationDocumentSignedOffLineServiceAction editAction = new PanelUserRegistrationDocumentSignedOffLineServiceAction();
            editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.PanelUserRegistrationDocumentRepository, ServiceAction<PanelUserRegistrationDocument>.DoNotUpdate, panelUserAssignmentEntity.FindContractDocument().PanelUserRegistrationDocumentId, userId);
            editAction.Execute();
            //
            // Now that we have updated contract document, we check if registration is complete and update.
            //
            UpdateRegistartionCompletionDateTime(panelUserAssignmentEntity.RetrieveAcknowledgeNdaPanelUserRegistrationDocumentId().Value, userId);
            //
            // Now we save everything
            //
            UnitOfWork.Save();
        }
        /// <summary>
        /// Get the html content of a document
        /// </summary>
        /// <param name="panelRegistrationDocumentId">The panel registration document identifier</param>
        public string GetDocumentContent(int panelRegistrationDocumentId)
        {
            ValidateGetDocumentContentParameters(panelRegistrationDocumentId);
            //
            // Retrieve the PanelRegistrationDocument
            //
            var panelRegistrationDocumentEntity = UnitOfWork.PanelUserRegistrationDocumentRepository.GetByID(panelRegistrationDocumentId);
            //
            // Get the document content
            //
            return panelRegistrationDocumentEntity.DocumentFile;
        }
        /// <summary>
        /// Updates (if necessary) the registration start time.
        /// </summary>
        /// <param name="panelUserRegistrationEntity"></param>
        /// <param name="userId">Current user's entity identifier</param>
        internal virtual void UpdateRegistartionStartDateTime(PanelUserRegistration panelUserRegistrationEntity, int userId)
        {
            UpdateRegistartionDateTimes(panelUserRegistrationEntity, x => x.RegistrationStartDate, GlobalProperties.P2rmisDateTimeNow, null, userId);
        }
        /// <summary>
        /// Updates (if necessary) the registration completion time.
        /// </summary>
        /// <param name="panelUserRegistrationDocumentId"></param>
        /// <param name="userId">Current user's entity identifier</param>
        internal virtual void UpdateRegistartionCompletionDateTime(int panelUserRegistrationDocumentId, int userId)
        {
            PanelUserRegistrationDocument panelUserRegistrationDocumentEntity = this.UnitOfWork.PanelUserRegistrationDocumentRepository.GetByID(panelUserRegistrationDocumentId);
            PanelUserRegistration panelUserRegistrationEntity = panelUserRegistrationDocumentEntity.PanelUserRegistration;
            if (panelUserRegistrationEntity.AreAllDocumentsSigned())
            {
                UpdateRegistartionDateTimes(panelUserRegistrationEntity, x => x.RegistrationCompletedDate, panelUserRegistrationEntity.RegistrationStartDate, GlobalProperties.P2rmisDateTimeNow, userId);
            }
        }
        /// <summary>
        /// Updates (if necessary) the registration times.  (This does all the heavy lifting)
        /// </summary>
        /// <param name="panelUserRegistrationEntity">PanelUserRegistrationEntity</param>
        /// <param name="property">Property to test so we do not overwrite.</param>
        /// <param name="registrationStartDateTime">Start date/time</param>
        /// <param name="registrationCompletionDateTime">Completion date/time</param>
        /// <param name="userId">Current user's entity identifier</param>
        internal virtual void UpdateRegistartionDateTimes(PanelUserRegistration panelUserRegistrationEntity, Func<PanelUserRegistration, DateTime?> property, DateTime? registrationStartDateTime, DateTime? registrationCompletionDateTime, int userId)
        {
            lock (registrationStartDateLock)
            {
                if (!property(panelUserRegistrationEntity).HasValue)
                {
                    PanelUserRegistrationServiceAction editAction = new PanelUserRegistrationServiceAction();
                    editAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.PanelUserRegistrationRepository, ServiceAction<PanelUserRegistration>.DoNotUpdate, panelUserRegistrationEntity.PanelUserRegistrationId, userId);
                    editAction.Populate(registrationStartDateTime, registrationCompletionDateTime);
                    editAction.Execute();
                }
            }
        }
        /// <summary>
        /// Sign a document and indicate if the registration has completed.
        /// </summary>
        /// <param name="confirms">List of KeyValue pairs where the key is the PanelUserRegistrationDocumentId & Value indicates the dpcument is isgned</param>
        /// <param name="userId">User entity identifier</param>
        public Container<IConfirmedModel> SaveConfirm(List<KeyValuePair<int, string>> confirms, int userId)
        {
            ValidateSaveConfirmcParameters(confirms, userId);
            Container<IConfirmedModel> container = new Container<IConfirmedModel>();
            List<IConfirmedModel> list = new List<IConfirmedModel>();
            //
            // we create our editor and initialize it
            //
            PanelUserRegistrationDocumentCommitServiceAction documentEditAction = new PanelUserRegistrationDocumentCommitServiceAction();
            documentEditAction.InitializeAction(this.UnitOfWork, this.UnitOfWork.PanelUserRegistrationDocumentRepository, ServiceAction<PanelUserRegistrationDocument>.DoNotUpdate, 0, userId);
            //
            // Now we just iterate over the selections
            //
            foreach (var x in confirms)
            {
                documentEditAction.Populate(x.Key, x.Value);
                documentEditAction.Execute();
                //
                // We only return a result if it was signed
                //
                if (documentEditAction.Document.SignedBy.HasValue)
                {
                    var u = UnitOfWork.UserRepository.GetByID(documentEditAction.Document.SignedBy);
                    var tm = new ConfirmedModel(x.Key, u.FullName(), ViewHelpers.FormatEtDateTime(documentEditAction.Document.DateSigned));
                    list.Add(tm);
                }
            }
            container.ModelList = list;
            //
            // Now that we have updated all of the documents, we check if registration is complete and update.
            //
            UpdateRegistartionCompletionDateTime(confirms[0].Key, userId);
            //
            // Now that the registration date is updated we can go back and determine if the registration is complete
            //
            UpdateConfirmModelRegistrationCompleteDates(list, confirms[0].Key);
            UnitOfWork.Save();

            return container;
        }
        /// <summary>
        /// Update the ConfirmModels with an indication if the document is complete.
        /// </summary>
        /// <param name="list">A list of IConfirmModel objects</param>
        /// <param name="panelUserRegistrationDocumentId">PanelUserRegistrationDocument entity identifier from the registration</param>
        internal virtual void UpdateConfirmModelRegistrationCompleteDates(List<IConfirmedModel> list, int panelUserRegistrationDocumentId)
        {
            //
            // Retrieve any PanelUserRegistrationDocument associated with this SaveConfirm & then determine if it is not complete
            //
            IPanelRegistrationDatesWebModel model = RetrieveRegistrationDates(panelUserRegistrationDocumentId);
            bool isRegistrationNotComplete = ProgramRegistrationService.IsRegistrationNotCompleted(model.RegistrationCompletedDate);
            //
            // & Then update each entry in the list.
            //
            list.ForEach(x => x.IsRegistrationNotComplete = isRegistrationNotComplete);
        }
        /// <summary>
        /// Match the document item from the wizard to the PanelUserRegistrationDocumentItem from the PanelUserRegistrationDocument
        /// </summary>
        /// <param name="documentItemFromWizard">Key/value pair from the wizard</param>
        /// <param name="documentItems">Collection of PanelUserRegistrationDocumentItems from the PanelUserRegistrationDocument</param>
        /// <returns>PanelUserRegistrationDocumentItem entity identifier for the documentIten from the wizard; 0 if no match made.</returns>
        internal virtual int MatchFormDataToPanelUserRegistrationdocumentItem(KeyValuePair<int, string> documentItemFromWizard, ICollection<PanelUserRegistrationDocumentItem> documentItems)
        {
            var x = documentItems.FirstOrDefault(y => y.RegistrationDocumentItemId == documentItemFromWizard.Key);
            return (x != null)? x.PanelUserRegistrationDocumentItemId: 0;
        }
        /// <summary>
        /// Indicates if a status is valid for this panel.
        /// </summary>
        /// <param name="panelAbbreviation">Panel Abbreviation</param>
        /// <returns>True if the Panel Abbreviation is supplied; false otherwise</returns>
        public static bool IsRegistrationStateValid(string panelAbbreviation)
        {
            return !(string.IsNullOrEmpty(panelAbbreviation) || string.IsNullOrWhiteSpace(panelAbbreviation));
        }
        /// <summary>
        /// Indicates if a registrations is not started
        /// </summary>
        /// <param name="registrationStartDate">Registration start date</param>
        /// <returns>True if the registration has not started.</returns>
        public static bool IsRegistrationNotStarted(DateTime? registrationStartDate)
        {
            return (!registrationStartDate.HasValue);
        }
        /// <summary>
        /// Indicates if a registrations is not complete
        /// </summary>
        /// <param name="registrationStartDate">Registration start date</param>
        /// <param name="registrationCompletionDate">Registration completion date</param>
        /// <returns>True if the registration has not completed.</returns>
        public static bool IsRegistrationContinued(DateTime? registrationStartDate, DateTime? registrationCompletionDate)
        {
            return registrationStartDate.HasValue & !registrationCompletionDate.HasValue;
        }
        /// <summary>
        /// Indicates if a registrations is not complete
        /// </summary>
        /// <param name="registrationStartDate">Registration start date</param>
        /// <param name="registrationCompletionDate">Registration completion date</param>
        /// <returns>True if the registration has not completed.</returns>
        public static bool IsRegistrationNotCompleted(DateTime? registrationCompletionDate)
        {
            return !registrationCompletionDate.HasValue;
        }

        /// <summary>
        /// Retrieve the start & completion dates.
        /// </summary>
        /// <param name="panelUserRegistratonDocumentId">PanelUserRegistrationDocument entity identifier of a registration document</param>
        /// <returns>Web Model consisting of the start & complete registration dates</returns>
        public IPanelRegistrationDatesWebModel RetrieveRegistrationDates(int panelUserRegistratonDocumentId)
        {
            this.ValidateInteger(panelUserRegistratonDocumentId, "ProgramRegristrationService.RetrieveRegistrationDates", "panelUserRegistratonDocumentId");

            PanelUserRegistrationDocument panelUserRegistrationDocumentEntity = this.UnitOfWork.PanelUserRegistrationDocumentRepository.GetByID(panelUserRegistratonDocumentId);
            PanelUserRegistration panelUserRegistrationEntity = panelUserRegistrationDocumentEntity.PanelUserRegistration;

            return new PanelRegistrationDatesWebModel(panelUserRegistrationEntity.RegistrationStartDate, panelUserRegistrationEntity.RegistrationCompletedDate);
        }
        #endregion
        #region Registration Status
        /// <summary>
        /// Retrieve Program Registration Status for one or more session panels.
        /// </summary>
        /// <param name="sessionPanelIds">Collection of SessionPanel entity identifiers</param>
        /// <returns>Container of IProgramRegistrationWebModel objects</returns>
        public Container<IProgramRegistrationWebModel> GetRegistrationStatus(ICollection<int> sessionPanelIds)
        {
            ValidateCollection(sessionPanelIds, "ProgramRegistrationService.GetRegistrationStatus", "sessionPanelIds");

            Container<IProgramRegistrationWebModel> result = new Container<IProgramRegistrationWebModel>();
            List<IProgramRegistrationWebModel> list = new List<IProgramRegistrationWebModel>();

            foreach (int sessionPanelid in sessionPanelIds)
            {
                SessionPanel sessionPanelEntity = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelid);
                list.AddRange(RetrieveSessionPanelistsForSession(sessionPanelEntity));
            }
            ///
            // After we have all the models, we sort them by panel
            //
            result.ModelList = list.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
            return result;
        }
        /// <summary>
        /// Retrieves the data for each panelist in a session.
        /// </summary>
        /// <param name="sessionPanelEntity">SessionPanelEntity</param>
        /// <returns>List of IProgramRegistrationWebModel objects for the panelist in a session</returns>
        internal virtual IEnumerable<IProgramRegistrationWebModel> RetrieveSessionPanelistsForSession(SessionPanel sessionPanelEntity)
        {
            List<IProgramRegistrationWebModel> list = new List<IProgramRegistrationWebModel>();
            var programYearId = sessionPanelEntity.GetProgramYear().ProgramYearId;
            foreach (var panelUserAssignmentEntity in sessionPanelEntity.PanelUserAssignments)
            {
                //
                // Filter out all but reviewers  (no need to construct others)
                //
                if (panelUserAssignmentEntity.ClientParticipantType.ReviewerFlag || panelUserAssignmentEntity.ClientParticipantType.ElevatedChairpersonFlag)
                {
                    ProgramRegistrationWebModel model = new ProgramRegistrationWebModel();
                    //
                    // Now that we have a model, we populate the model.    Most of the heavy lifting is done in
                    // the entity extension classes.
                    //

                    model.ProgramYearId = programYearId;
                    model.SetUserInformation(panelUserAssignmentEntity.LastName(), panelUserAssignmentEntity.FirstName(), panelUserAssignmentEntity.UserId, panelUserAssignmentEntity.ClientParticipantType.ParticipantTypeAbbreviation, sessionPanelEntity.PanelAbbreviation, panelUserAssignmentEntity.User.VerifiedDate, panelUserAssignmentEntity.PaymentCategory(), panelUserAssignmentEntity.PanelUserAssignmentId, sessionPanelEntity.SessionPanelId, panelUserAssignmentEntity.RestrictedAssignedFlag);
                    model.SetDocumentDates(panelUserAssignmentEntity.DateTimeAcknowledgementNdaSigned(), panelUserAssignmentEntity.DateTimeBiasCoiSigned(), panelUserAssignmentEntity.DateTimeContractSigned(), panelUserAssignmentEntity.ResumeReceivedDateTime());
                    model.SetSingedOffLineIndicators(panelUserAssignmentEntity.RetrieveAcknowledgeNdaSignedOffline(), panelUserAssignmentEntity.RetrieveBiasCoiSignedSignedOffline(), panelUserAssignmentEntity.RetrieveContractSignedSignedOffline());
                    var w9UserAddress = panelUserAssignmentEntity.W9Address();
                    model.SetW9Information(panelUserAssignmentEntity.User.UserInfoEntity().VendorName(), w9UserAddress.Address1, w9UserAddress.StateAbbreviation(), w9UserAddress.Zip, panelUserAssignmentEntity.W9VerifiedDateTime(), w9UserAddress.Address2, w9UserAddress.Address3, w9UserAddress.Address4, w9UserAddress.City, w9UserAddress.CountryAbbreviation(), w9UserAddress.CountryName(), w9UserAddress.CountryId, panelUserAssignmentEntity.User.GetW9Status(), panelUserAssignmentEntity.User.GetW9StatusDate(), panelUserAssignmentEntity.User.UserInfoEntity().VendorName());
                    model.SetDocumentIds(panelUserAssignmentEntity.RetrieveAcknowledgeNdaPanelUserRegistrationDocumentId(), panelUserAssignmentEntity.RetrieveBiasCoiSignedPanelUserRegistrationDocumentId(), panelUserAssignmentEntity.RetrieveContractSignedPanelUserRegistrationDocumentId());
                    var defaultContractStatus = UnitOfWork.ContractStatusRepository.GetByID(ContractStatus.Keys.Original);
                    if (model.ContractPanelUserRegistrationDocumentId != null)
                    {
                        // get contact data with id   
                        var contStatus = panelUserAssignmentEntity.RetrieveRegistrationDocumentContractStatus((int)model.ContractPanelUserRegistrationDocumentId) ?? defaultContractStatus;
                        var fee = panelUserAssignmentEntity.RetrieveRegistrationDocumentFeeAmount((int)model.ContractPanelUserRegistrationDocumentId);

                        model.SetContractInfo(contStatus.ContractStatusId, contStatus.StatusLabel, fee, SetContractStatusFlag(contStatus.ContractStatusId,ContractStatus.Keys.Original),
                            SetContractStatusFlag(contStatus.ContractStatusId, ContractStatus.Keys.Bypass), SetContractStatusFlag(contStatus.ContractStatusId, ContractStatus.Keys.Regenerate), SetCanAddAddendum(contStatus.ContractStatusId, model.ContractDateSigned));  
                    }
                    //
                    // Add the model to the list
                    //
                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// Indicates if all the users registration are complete.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>False - the users registration are complete; True otherwise</returns>
        public bool AreUsersRegistrationInComplete(int userId)
        {
            ValidateInt(userId, "PanelRegistrationService.AreUsersRegistrationComplete", "userId");

            User userEntity = UnitOfWork.UserRepository.GetByID(userId);

            return userEntity.AreUsersRegistrationInComplete();
        }
        /// <summary>
        /// Indicates if the users contract has been updated after registration is complete.
        /// </summary>
        /// <param name="userId">User entity identifier</param>
        /// <returns>False - contract not updated; True if it was</returns>
        public bool IsRegistrationContractUpdated(int userId)
        {
            ValidateInt(userId, "PanelRegistrationService.IsRegistrationContractUpdated", "userId");

            var result = UnitOfWork.PanelUserAssignmentRepository.Select()
                        .Where(x => x.UserId == userId)
                        .SelectMany(x => x.PanelUserRegistrations)
                        .Where(x => x.RegistrationCompletedDate != null)
                        .SelectMany(x => x.PanelUserRegistrationDocuments)
                        .Any(x => x.ClientRegistrationDocument.RegistrationDocumentTypeId == RegistrationDocumentType.Indexes.ContractualAgreement
                            && x.DateSigned == null);
            return result;
        }
        /// <summary>
        /// Checks users registration status on a per program basis.
        /// </summary>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        /// <returns>UserRegistrtionStatusModel</returns>
        public UserRegistrtionStatusModel CheckUserRegistrationStatusForSpecifiProgram(int programYearId, int userId)
        {
            UserRegistrtionStatusModelBuilder builder = new UserRegistrtionStatusModelBuilder(this.UnitOfWork, programYearId, userId);

            return builder.Build() as UserRegistrtionStatusModel;
        }

        /// <summary>
        /// Set the flag for contract status.
        /// </summary>
        /// <param name="statusId">current contract status id</param>
        /// <param name="key">Key to compare with</param>
        /// <returns>Null - if not it; True otherwise</returns>
        public bool SetContractStatusFlag(int statusId, int key)
        {
            if (statusId == key)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets whether the current contract can have an addendum added to it
        /// </summary>
        /// <param name="contractStatusId">Contract status identifier</param>
        /// <param name="signedDate">Date contract was signed</param>
        /// <returns></returns>
        internal bool SetCanAddAddendum(int contractStatusId, DateTime? signedDate)
        {
            return IsContractCustomized(contractStatusId) || (signedDate != null && !IsContractBypassed(contractStatusId));
        }
        #endregion
        #region Helpers
        /// <summary>
        /// Validate the parameters for SaveRegistrationForm
        /// </summary>
        /// <param name="formContents">Form contents as key/value pairs</param>
        /// <param name="panelRegistrationDocumentId">PanelRegistrationDocument entity identifier</param>
        /// <param name="userId">User entity identifier of the user saving the registration from</param>
        private void ValidateSaveRegistrationFormParameters(List<KeyValuePair<int, string>> formContents, int panelRegistrationDocumentId, int userId)
        {
            ValidateCollectionExists<KeyValuePair<int, string>>(formContents, "ProgramRegristrationService.SaveRegistrationForm", "contents");
            ValidateInt(panelRegistrationDocumentId, "ProgramRegristrationService.SaveRegistrationForm", "panelRegistrationDocumentId");
            ValidateInt(userId, "ProgramRegristrationService.SaveRegistrationForm", "userId");
        }
        /// <summary>
        /// Validate the parameters for SaveDocumentContent
        /// </summary>
        /// <param name="documentContent">The document's HTML content</param>
        /// <param name="panelRegistrationDocumentId">PanelRegistrationDocument entity identifier</param>
        private void ValidateSaveDocumentContentParameters(string documentContent, int panelRegistrationDocumentId)
        {
            ValidateString(documentContent, "ProgramRegristrationService.ValidateSaveDocumentContentParameters", "contents");
            ValidateInt(panelRegistrationDocumentId, "ProgramRegristrationService.ValidateSaveDocumentContentParameters", "panelRegistrationDocumentId");
        }
        /// <summary>
        /// Validate the parameters for SaveConfirm
        /// </summary>
        /// <param name="confirms"></param>
        /// <param name="userId">User entity identifier of the user saving the registration from</param>
        private void ValidateSaveConfirmcParameters(List<KeyValuePair<int, string>> confirms, int userId)
        {
            ValidateCollectionExists<KeyValuePair<int, string>>(confirms, "ProgramRegristrationService.SaveConfirm", "confirms");
            ValidateInt(userId, "ProgramRegristrationService.SaveConfirm", "userId");
        }
        /// <summary>
        /// Validate the parameters for GetDocumentContent
        /// </summary>
        /// <param name="panelRegistrationDocumentId">PanelRegistrationDocument entity identifier</param>
        private void ValidateGetDocumentContentParameters(int panelRegistrationDocumentId)
        {
            ValidateInt(panelRegistrationDocumentId, "ProgramRegristrationService.ValidateGetDocumentContentParameters", "panelRegistrationDocumentId");
        }
        /// <summary>
        /// Validate the parameters for SaveDocumentOffline
        /// </summary>
        /// <param name="panelUserAssignmentId">PanelUserAssignment entity identifier</param>
        /// <param name="userId">User entity identifier</param>
        private void ValidateSaveDocumentOfflineParameters(int panelUserAssignmentId, int userId)
        {
            ValidateInt(panelUserAssignmentId, "ProgramRegistrationService.SaveDocumentOffline", "panelUserAssignmentId");
            ValidateInt(userId, "ProgramRegistrationService.SaveDocumentOffline", "userId");
        }

        /// <summary>
        /// Whether the contract is bypassed
        /// </summary>
        /// <param name="contractStatusId">Identifier for a contract status</param>
        /// <returns>true if bypassed; otherwise false</returns>
        private bool IsContractBypassed (int? contractStatusId)
        {
            return contractStatusId == ContractStatus.Keys.Bypass;
        }

        /// <summary>
        /// Whether the contract is customized rather than system generated
        /// </summary>
        /// <param name="contractStatusId">Identifier for a contract status</param>
        /// <returns>true if customized; otherwise false</returns>
        private bool IsContractCustomized (int? contractStatusId)
        {
            return contractStatusId == ContractStatus.Keys.AddAddendum || contractStatusId == ContractStatus.Keys.Replace;
        }
        #endregion
    }
    #endregion
}
