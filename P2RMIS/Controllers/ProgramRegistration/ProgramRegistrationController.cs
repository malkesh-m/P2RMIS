using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sra.P2rmis.Bll;
using Sra.P2rmis.Bll.ProgramRegistration;
using Sra.P2rmis.Bll.UserProfileManagement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Web.ViewModels.ProgramRegistration;
using Sra.P2rmis.WebModels.ProgramRegistration;
using Sra.P2rmis.WebModels.UserProfileManagement;
using Sra.P2rmis.CrossCuttingServices.FileServices;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;
using Sra.P2rmis.CrossCuttingServices.MessageServices;

namespace Sra.P2rmis.Web.Controllers.ProgramRegistration
{
    /// <summary>
    /// 
    /// </summary>
    public class ProgramRegistrationController : ProgramRegistrationBaseController
    {
        #region Construction; Setup & Disposal
        /// <summary>
        /// Default constructor.  Private default constructor along with the private property setters
        /// controls construction & instantiation.
        /// </summary>
        private ProgramRegistrationController()
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="programRegistrationService">Program Registration service</param>
        /// <param name="userProfileManagementService">UserProfileManagement service</param>
        public ProgramRegistrationController(IProgramRegistrationService programRegistrationService, IUserProfileManagementService userProfileManagementService,
            ILookupService theLookupService, IUserProfileManagementService theProfileService)
        {
            this.theProgramRegistrationService = programRegistrationService;
            this.theUserProfileManagementService = userProfileManagementService;
            this.theLookupService = theLookupService;
            this.theProfileService = theProfileService;
        }
        #endregion
        #region Services Provided
        //
        // GET: /ProgramRegistration/
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Populate the registration wizard
        /// </summary>
        /// <param name="participationId">The panel user assignment identifier</param>
        /// <returns>The partial view of the registration wizard</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult RegistrationWizard(int participationId, int startTab)
        {
            RegistrationWizardViewModel model = new RegistrationWizardViewModel();

            int userId = GetUserId();
            try
            {
                // Get registration wizard data
                var wizard = theProgramRegistrationService.RegistrationWizardLoad(participationId);
                // Populate document data from wizard
                PopulateDocumentData(wizard, model, startTab);
                // Get partial view name
                model.PartialViewName = GetPartialViewName(model.Form.FormKey);
                model.ParticipationId = participationId;
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                model = new RegistrationWizardViewModel();
                HandleExceptionViaElmah(e);
            }
            return PartialView(ViewNames.RegistrationWizard, model);
        }

        /// <summary>
        /// Save registration form data.
        /// </summary>
        /// <param name="participationId">Panel user assignment identifier</param>
        /// <param name="panelRegistrationDocumentId">Panel registration document identifier</param>
        /// <param name="formData">Form data in JSON format</param>
        /// <param name="sortOrder">The order index of the current form</param>
        /// <param name="goNext">True if going to the next step; false if going to the previous step.</param>
        /// <param name="htmlContent">The HTML content of the document for PDF creation</param>
        /// <returns>The partial view of the next or previous step</returns>
        [ValidateInput(false)]
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult SaveRegistrationForm(int participationId, int? panelRegistrationDocumentId, string formData, int sortOrder, string htmlContent)
        {
            RegistrationFormViewModel model = new RegistrationFormViewModel();
            string pvName;
            try
            {
                // Save data
                if (panelRegistrationDocumentId != null)
                {
                    // Get registration document
                    var doc = theProgramRegistrationService.GetRegistrationDocument((int)panelRegistrationDocumentId);
                    if (!doc.DocumentSigned)
                    {
                        // This handles both instances of the Wizard's tabs: those that have data (PaneuUserRegistrationDocumentItems) 
                        // and those that do not.  The last time the user paged through still needs to be recorded.
                        List<KeyValuePair<int, string>> contents = (formData != string.Empty) ? JsonConvert.DeserializeObject<List<KeyValuePair<int, string>>>(formData) : new List<KeyValuePair<int, string>>();
                        theProgramRegistrationService.SaveRegistrationForm(contents, (int)panelRegistrationDocumentId, GetUserId());
                        //customized contract data is stored in an external file so no html version is saved
                        if (!doc.ContractData.IsContractCustomized)
                        {
                            // Format the html content that works best with PDF creation
                            htmlContent = HtmlServices.GetHtmlContentAsPdfCompatible(htmlContent);
                            // Save document HTML content
                            theProgramRegistrationService.SaveDocumentContent(htmlContent, (int)panelRegistrationDocumentId);
                        }
                    }
                }

                // Get registration wizard data
                RegistrationWizardViewModel registrationWizardViewModel = new RegistrationWizardViewModel();
                var wizard = theProgramRegistrationService.RegistrationWizardLoad(participationId);
                // Populate document data from wizard
                PopulateDocumentData(wizard, registrationWizardViewModel, sortOrder);                
                model = registrationWizardViewModel;
                model.PhoneTypeDropdown = theLookupService.ListPhoneType().ModelList.ToList();
                var userInfoId = theProfileService.GetUserInfoId(GetUserId());

                // Get partial view name
                pvName = GetPartialViewName(model.Form.FormKey);
                if (pvName == "_EmContact")
                {
                    // Get values for Emergency Contact Wizard
                    var contactPerson = theUserProfileManagementService.GetEmergencyContactPerson(userInfoId);
                    if (contactPerson == null)
                    {
                        contactPerson = new UserAlternateContactPersonModel();
                        UserAlternateContactPersonModel.InitializeModel(contactPerson as UserAlternateContactPersonModel);

                        theProfileService.EnsureSuffientModels<UserAlternateContactPersonPhoneModel>(contactPerson.AlternateContactPhone, 
                            UserAlternateContactPersonPhoneModel.MinimumEntries, UserAlternateContactPersonPhoneModel.InitializeModel);
                    }
                    model.GetAlternateContactPersons = contactPerson;
                    theProfileService.EnsureSuffientModels<UserAlternateContactPersonPhoneModel>(contactPerson.AlternateContactPhone,
                    UserAlternateContactPersonPhoneModel.MinimumEntries, UserAlternateContactPersonPhoneModel.InitializeModel);
                }
            }
            catch (Exception e)
            {
                //
                // reset the view model to a know state & log the error
                //
                model = new RegistrationFormViewModel();
                pvName = ViewNames.AcknowledgeNda;
                HandleExceptionViaElmah(e);
            }
            return PartialView(pvName, model);
        }
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult SaveEmergencyContactPage(string FirstName, string LastName, string PhoneNumber, int PhoneNumberType, bool PhoneNumberCheckbox, string AlternatePhoneNumber, int AlternatePhoneNumberType, bool AlternatePhoneNumberCheckbox, int? UserAlternateContactId, int? UserAlternateContactTypeId)
        {
            UserAlternateContactPersonModel userContact = new UserAlternateContactPersonModel();
            var userInfoId = theProfileService.GetUserInfoId(GetUserId());
            var newContact = theUserProfileManagementService.GetEmergencyContactPerson(userInfoId);
            if (newContact != null)
            {
                theProfileService.EnsureSuffientModels<UserAlternateContactPersonPhoneModel>(newContact.AlternateContactPhone,
         UserAlternateContactPersonPhoneModel.MinimumEntries, UserAlternateContactPersonPhoneModel.InitializeModel);
            }

            // If emergency contact is imported then save
            bool isValid;
            try
            {
                if(newContact == null)
                {
                    userContact = new UserAlternateContactPersonModel();
                    UserAlternateContactPersonModel.InitializeModel(userContact as UserAlternateContactPersonModel);

                    theProfileService.EnsureSuffientModels<UserAlternateContactPersonPhoneModel>(userContact.AlternateContactPhone,
                        UserAlternateContactPersonPhoneModel.MinimumEntries, UserAlternateContactPersonPhoneModel.InitializeModel);

                    userContact.FirstName = FirstName;
                    userContact.LastName = LastName;
                    userContact.AlternateContactPhone[0].International = PhoneNumberCheckbox;
                    userContact.AlternateContactPhone[0].Number = PhoneNumber;
                    userContact.AlternateContactPhone[0].PhoneTypeId = PhoneNumberType;
                    userContact.AlternateContactPhone[0].PrimaryFlag = true;

                    if (userContact.AlternateContactPhone.Count > 1)
                    {
                        userContact.AlternateContactPhone[1].International = AlternatePhoneNumberCheckbox;
                        userContact.AlternateContactPhone[1].Number = AlternatePhoneNumber;
                        userContact.AlternateContactPhone[1].PhoneTypeId = AlternatePhoneNumberType;
                        userContact.AlternateContactPhone[1].PrimaryFlag = false;
                    }
                    userContact.UserAlternateContactTypeId = 4;
                    theUserProfileManagementService.SaveAlternateContact(userInfoId, userContact, GetUserId());
                }else if (newContact.UserAlternateContactTypeId == 4)
                {
                    newContact.FirstName = FirstName;
                    newContact.LastName = LastName;
                    if(newContact.AlternateContactPhone[0].PrimaryFlag == true)
                    {
                        newContact.AlternateContactPhone[0].International = PhoneNumberCheckbox;
                        newContact.AlternateContactPhone[0].Number = PhoneNumber;
                        newContact.AlternateContactPhone[0].PhoneTypeId = PhoneNumberType;
                    }
                    else
                    {
                        newContact.AlternateContactPhone[0].International = AlternatePhoneNumberCheckbox;
                        newContact.AlternateContactPhone[0].Number = AlternatePhoneNumber;
                        newContact.AlternateContactPhone[0].PhoneTypeId = AlternatePhoneNumberType;
                    }
                    if (newContact.AlternateContactPhone.Count > 1)
                    {
                        if (newContact.AlternateContactPhone[1].PrimaryFlag == true)
                        {
                            newContact.AlternateContactPhone[1].International = PhoneNumberCheckbox;
                            newContact.AlternateContactPhone[1].Number = PhoneNumber;
                            newContact.AlternateContactPhone[1].PhoneTypeId = PhoneNumberType;
                        }
                        else
                        {
                            newContact.AlternateContactPhone[1].International = AlternatePhoneNumberCheckbox;
                            newContact.AlternateContactPhone[1].Number = AlternatePhoneNumber;
                            newContact.AlternateContactPhone[1].PhoneTypeId = AlternatePhoneNumberType;
                        }
                    }
                    theUserProfileManagementService.SaveAlternateContact(userInfoId, newContact, GetUserId());
                }
                else
                {
                    userContact = new UserAlternateContactPersonModel();
                    UserAlternateContactPersonModel.InitializeModel(userContact as UserAlternateContactPersonModel);

                    theProfileService.EnsureSuffientModels<UserAlternateContactPersonPhoneModel>(userContact.AlternateContactPhone,
                        UserAlternateContactPersonPhoneModel.MinimumEntries, UserAlternateContactPersonPhoneModel.InitializeModel);

                    userContact.FirstName = FirstName;
                    userContact.LastName = LastName;
                    userContact.AlternateContactPhone[0].International = PhoneNumberCheckbox;
                    userContact.AlternateContactPhone[0].Number = PhoneNumber;
                    userContact.AlternateContactPhone[0].PhoneTypeId = PhoneNumberType;
                    userContact.AlternateContactPhone[0].PrimaryFlag = true;

                    if (userContact.AlternateContactPhone.Count > 1)
                    {
                        userContact.AlternateContactPhone[1].International = AlternatePhoneNumberCheckbox;
                        userContact.AlternateContactPhone[1].Number = AlternatePhoneNumber;
                        userContact.AlternateContactPhone[1].PhoneTypeId = AlternatePhoneNumberType;
                        userContact.AlternateContactPhone[1].PrimaryFlag = false;
                    }
                    userContact.UserAlternateContactTypeId = 4;
                    theUserProfileManagementService.SaveAlternateContact(userInfoId, userContact, GetUserId());
                }


                isValid = true;
            }
            catch (Exception e)
            {
                isValid = false;
                HandleExceptionViaElmah(e);
            }
            return Json(new { isValid } );
        }
        /// <summary>
        /// Save registration form
        /// </summary>
        /// <param name="password">The password string</param>
        /// <param name="formData">The form data in JSON format</param>
        /// <returns>The results in JSON format</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.None, NoStore = true,Duration = 0)]
        public ActionResult SignRegistrationForm(string password, string formData)
        {
            var isValid = false;
            var signatures = new List<IConfirmedModel>();
            var isRegistrationCompleted = false;
            try
            {
                isValid = theUserProfileManagementService.DoesMatchCurrentPassword(password, GetUserId());
                if (isValid)
                {
                    List<KeyValuePair<int, string>> contents = JsonConvert.DeserializeObject<List<KeyValuePair<int, string>>>(formData);
                    // Call BL to sign agreements
                    signatures = theProgramRegistrationService.SaveConfirm(contents, GetUserId()).ModelList.ToList();
                    for (var i = 0; i < signatures.Count; i++) {
                        var panelRegistrationDocumentId = signatures[i].PanelUserRegistrationDocumentId;
                        var signedDate = signatures[i].Date;
                        isRegistrationCompleted = !(signatures[i].IsRegistrationNotComplete);
                        if (signedDate != "")
                        {
                            SaveSignature(panelRegistrationDocumentId);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                isValid = false;
                HandleExceptionViaElmah(e);
            }
            return Json(new { status = isValid, isRegistrationCompleted = isRegistrationCompleted, signatures = signatures });
        }        
        /// <summary>
        /// Get document PDF file
        /// </summary>
        /// <param name="panelRegistrationDocumentId">The panel registration document identifier</param>
        /// <returns>The document PDF file</returns>
        [Sra.P2rmis.Web.Common.Authorize]
        public ActionResult GetDocumentPdf(int panelRegistrationDocumentId, bool preview = true)
        {

            if (preview)
            {
                var fileUrl = $"/ProgramRegistration/GetDocumentPdf?panelRegistrationDocumentId={panelRegistrationDocumentId}&preview=false";
                //file and download URL is same as this is PDF document
                return PdfViewer(fileUrl, fileUrl);
            }
            else
            {
                (string name, byte[] contents) file = (Guid.NewGuid().ToString(), null);

                try
                {
                    file = theProgramRegistrationService.GetRegistrationDocumentFile(panelRegistrationDocumentId, BaseUrl, DepPath);
                }
                catch (Exception e)
                {
                    HandleExceptionViaElmah(e);
                    //
                    // Redirect to file not found error page
                    //
                    return RedirectToAction("FileNotFound", "ErrorPage");
                }
                return File(file.contents, FileConstants.MimeTypes.Pdf, file.name);
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Get partial view's name by a form key
        /// </summary>
        /// <param name="formKey"></param>
        /// <returns>The file name of the partial view</returns>
        private string GetPartialViewName(string formKey)
        {
            string pvName = string.Empty;
            switch (formKey)
            {
                case Routing.ProgramRegistration.WorkflowStepKey.Acknowledgement:
                    pvName = ViewNames.AcknowledgeNda;
                    break;
                case Routing.ProgramRegistration.WorkflowStepKey.AcknowlegementCprit:
                    pvName = ViewNames.AcknowledgeNdaCprit;
                    break;
                case Routing.ProgramRegistration.WorkflowStepKey.BiasCoi:
                    pvName = ViewNames.BiasCoi;
                    break;
                case Routing.ProgramRegistration.WorkflowStepKey.BiasCoiCprit:
                    pvName = ViewNames.BiasCoiCprit;
                    break;
                case Routing.ProgramRegistration.WorkflowStepKey.Contract:
                    pvName = ViewNames.Contract;
                    break;
                case Routing.ProgramRegistration.WorkflowStepKey.ContractCprit:
                    pvName = ViewNames.ContractCprit;
                    break;
                case Routing.ProgramRegistration.WorkflowStepKey.EmContact:
                    pvName = ViewNames.EmContact;
                    break;
                case Routing.ProgramRegistration.WorkflowStepKey.Sign:
                    pvName = ViewNames.ConfirmSign;
                    break;
                default:
                    break;
            }
            return pvName;
        }
        /// <summary>
        /// Populate document data from wizard model to view model
        /// </summary>
        /// <param name="wizardModel">The wizard model</param>
        /// <param name="viewModel">The view model</param>
        /// <param name="sortOrder">The order index of the current form</param>
        private void PopulateDocumentData(IDictionary<string, IWizardModel> wizardModel, 
                                          RegistrationWizardViewModel viewModel,
                                          int sortOrder)
        {
            var workflow = new RegistrationWorkflow();
            workflow.WorkflowSteps = ((RegistrationWorkflow)wizardModel["w"]).WorkflowSteps;
            // Add IsSigned values
            foreach (var step in workflow.WorkflowSteps)
            {
                var doc = ((DocumentForm)wizardModel.Where(x => x.Key == step.Key).FirstOrDefault().Value);
                step.Value.IsSigned = doc.DocumentSigned;
                step.Value.DocumentVersion = doc.DocumentVersion;
                step.Value.DateSigned = ViewHelpers.FormatEtDateTime(doc.DocumentSignedDateTime);
                step.Value.NameSigned = doc.SignedByName;
                step.Value.IsReadyForSignature = !(doc.Contents.Any(x => x.Value != null && x.Value.IsRequired
                        && (x.Value.ItemValue == null || x.Value.ItemValue == string.Empty)));
                step.Value.IsSignedOffLine = doc.SignedOffLine != null && (bool)doc.SignedOffLine;
                step.Value.IsBypassed = doc.ContractData.IsContractBypassed;
                step.Value.IsCustomized = doc.ContractData.IsContractCustomized;
            }

            workflow.WorkflowSteps.Add(Routing.ProgramRegistration.WorkflowStepKey.EmContact,
                new RegistrationStep(workflow.WorkflowSteps.Count + 1, 0, Routing.ProgramRegistration.WorkflowStepValue.EmContact, true));
            // Add the last step which is required
            workflow.WorkflowSteps.Add(Routing.ProgramRegistration.WorkflowStepKey.Sign, 
                new RegistrationStep(workflow.WorkflowSteps.Count + 1, 0, Routing.ProgramRegistration.WorkflowStepValue.ConfirmSign, true));
            // Get the form keys
            var currentKey = workflow.WorkflowSteps.Where(x => x.Value.SortOrder == sortOrder).FirstOrDefault().Key;
            var form = (DocumentForm)wizardModel.Where(x => x.Key == currentKey).FirstOrDefault().Value;
            if (form == null) {
                form = new DocumentForm();
                form.FormKey = currentKey;
                if (currentKey == Routing.ProgramRegistration.WorkflowStepKey.Sign)
                    form.ArePayRatesUploaded = RetrievePayRateIndicatorFromAnyDocument(wizardModel);
            } 
            if (!String.IsNullOrEmpty(form.DocumentContent))
                form.DocumentWebContent = HtmlServices.GetNodeHtmlById(form.DocumentContent, DocumentFormContent);
            // Get the business category lookup data when needed
            viewModel.Workflow = workflow;
            viewModel.Form = form;
            viewModel.BusinessCategoryLookup = (sortOrder == 1) ? new BusinessCategoryLookup() : null;
        }
        /// <summary>
        /// Retrieves an indication that the pay rates have been uploaded.
        /// </summary>
        /// <param name="wizardModel">The wizard model</param>
        /// <returns>Value of a form's ArePayRatesUploaded property.</returns>
        private bool RetrievePayRateIndicatorFromAnyDocument(IDictionary<string, IWizardModel> wizardModel)
        {
            var form = (DocumentForm)wizardModel.LastOrDefault().Value;
            return form.ArePayRatesUploaded;
        }       
        /// <summary>
        /// Save signature
        /// </summary>
        /// <param name="panelRegistrationDocumentId">The panel registration document identifier</param>
        private void SaveSignature(int panelRegistrationDocumentId)
        {
            // Get registration document

            decimal? consultantFee = null;
            var doc = theProgramRegistrationService.GetRegistrationDocument(panelRegistrationDocumentId);
            bool isContract = (doc.FormKey == Routing.ProgramRegistration.WorkflowStepKey.Contract ||
                doc.FormKey == Routing.ProgramRegistration.WorkflowStepKey.ContractCprit);
            bool isCustomizedContract = doc.ContractData.IsContractCustomized;
            
            // Format the html content that works best with PDF creation
            string signatureContent = ViewHelpers.FormatSignature(doc.SignedByName, doc.DocumentSignedDateTime);

            if (!isCustomizedContract)
            {
                var htmlContent = doc.DocumentContent;
                htmlContent = HtmlServices.UpdateDocumentSignature(htmlContent, signatureContent);
                if (isContract)
                {
                    var formattedSignatureDate = (doc.FormKey == Routing.ProgramRegistration.WorkflowStepKey.Contract) ?
                            ViewHelpers.FormatContractDate(doc.DocumentSignedDateTime) : ViewHelpers.FormatDate(doc.DocumentSignedDateTime);
                    consultantFee = Convert.ToDecimal(HtmlServices.GetNodeHtmlById(htmlContent, "consultantFeeAmount"));

                    htmlContent = HtmlServices.UpdateDocumentSignatureOnContract(htmlContent, formattedSignatureDate);
                }
                // Save document HTML content
                theProgramRegistrationService.SaveDocumentContent(htmlContent, panelRegistrationDocumentId);
            }
            if (isContract)
            {
                string signatureBlock = HtmlServices.GetHtmlDocumentSignature(doc.FiscalYear, doc.ProgramAbbreviation, doc.PanelAbbreviation, doc.DocumentId, doc.DocumentVersion, signatureContent, MessageService.RegistrationFormHelpText);
                //save additional contract specific content
                theProgramRegistrationService.SaveContractContentOnSign(panelRegistrationDocumentId, consultantFee, GetUserId(), doc.ContractData.IsContractCustomized, signatureBlock, BaseUrl, DepPath);
            }
        }


        #endregion
    }
}