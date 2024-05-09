using Sra.P2rmis.WebModels.Setup;
using System.Linq;
using System.Collections.Generic;
using Sra.P2rmis.Dal;
using System;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.DocumentServices;
using Sra.P2rmis.WebModels.HelperClasses;
using Sra.P2rmis.CrossCuttingServices.HttpServices;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using Sra.P2rmis.CrossCuttingServices.MessageServices;
using System.Web;
using System.Net;
using System.Net.Http;
using Sra.P2rmis.CrossCuttingServices.ConfigurationServices;

namespace Sra.P2rmis.Bll.Setup
{
    public class ProposalInfoDocImporter
    {
        /// <summary>
        /// Constructor for importing proposal info
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="credentialKey"></param>
        /// <param name="url"></param>
        /// <param name="clientTransferTypeId"></param>
        /// <param name="programMechanismId"></param>
        /// <param name="userId"></param>
        public ProposalInfoDocImporter(IUnitOfWork unitOfWork, string credentialKey, string url, int clientTransferTypeId, int programMechanismId, int userId)
        {
            UnitOfWork = unitOfWork;
            CredentialKey = credentialKey;
            Url = url;
            ClientTransferTypeId = clientTransferTypeId;
            ProgramMechanismId = programMechanismId;
            UserId = userId;
        }
        /// <summary>
        /// Unit of work
        /// </summary>
        public IUnitOfWork UnitOfWork { get; private set; }
        /// <summary>
        /// Credential key
        /// </summary>
        public string CredentialKey { get; private set; }
        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; private set; }
        /// <summary>
        /// Client transfer type identifier
        /// </summary>
        public int ClientTransferTypeId { get; private set; }
        /// <summary>
        /// Program mechanism identifier
        /// </summary>
        public int ProgramMechanismId { get; private set; }
        /// <summary>
        /// User identifier
        /// </summary>
        public int UserId { get; private set; }
        /// <summary>
        /// Import log identifier
        /// </summary>
        public int ImportLogId { get; private set; }

        /// <summary>
        /// Start import process
        /// </summary>
        public void StartImport()
        {
            // Initialize
            ImportLogId = InitializeImportLog(ClientTransferTypeId, Url);
            var programMechanism = UnitOfWork.ProgramMechanismRepository.GetByID(ProgramMechanismId);
            var clientId = programMechanism.ProgramYear.ClientProgram.ClientId;
            // Import applications
            var returnTask = Task.Run(() =>
            {
                ImportApplications(clientId);
                FixApplicationParentRelationship();
            });
        }
        /// <summary>
        /// Initializes the import log.
        /// </summary>
        /// <param name="clientTransferTypeId">The client transfer type identifier.</param>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        private int InitializeImportLog(int clientTransferTypeId, string url)
        {
            // Initialize import log entity
            var importLog = UnitOfWork.ImportLogRepository.AddImportLog(clientTransferTypeId, url);
            UnitOfWork.Save();

            return importLog.ImportLogId;
        }
        /// <summary>
        /// Imports the applications.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        private void ImportApplications(int clientId)
        {
            var successFlag = false;
            var logMessage = default(string);
            // Call service to get XML
            HttpClientHandler handler = null;
            if (!String.IsNullOrEmpty(ConfigManager.OutgoingProxy))
            {
                handler = new HttpClientHandler()
                {
                    Proxy = new WebProxy(ConfigManager.OutgoingProxy),
                    UseProxy = true,
                };
            }
            var hc = handler != null ?
                new CoreHttpClient(CredentialKey, handler) :
                new CoreHttpClient(CredentialKey);
            var xml = hc.GetStringAsyncResult(Url);
            // Parse XML and save ImportLogItem           
            if (xml != null)
            {
                var xmlObjects = XMLServices.DeserializeToObject(xml, typeof(ProposalInfoTransferModel)) as ProposalInfoTransferModel;
                if (xmlObjects != null)
                {
                    try
                    {
                        var successfulItemCount = 0;
                        var failedItemCount = 0;
                        if (xmlObjects.ProposalInfoList != null && xmlObjects.ProposalInfoList.Count > 0)
                        {
                            for (var i = 0; i < xmlObjects.ProposalInfoList.Count; i++)
                            {
                                var pi = xmlObjects.ProposalInfoList[i];
                                var content = XMLServices.Serialize<ProposalInfo>(pi);
                                var hasChanges = ContainImportLogItemChanges(pi.LogNo.Value, content);
                                if (hasChanges)
                                {
                                    // Import
                                    string message = default(string);
                                    var docItemImporter = new ProposalInfoItemImporter(UnitOfWork, pi, ProgramMechanismId, UserId);
                                    docItemImporter.StartImport(clientId);
                                    if (docItemImporter.Valid)
                                    {
                                        // Save application
                                        try
                                        {
                                            UnitOfWork.Save();
                                            successfulItemCount += 1;
                                        }
                                        catch (Exception ex)
                                        {
                                            UnitOfWork.DetachAllEntities();
                                            message = String.Format(MessageService.UnexpectedExceptionMessage, ex.Message);
                                            docItemImporter.Messages.Add(message);
                                            failedItemCount += 1;
                                        }
                                    }
                                    else
                                    {
                                        message = string.Join(" ", docItemImporter.Messages.ToArray());
                                        failedItemCount += 1;
                                    }
                                    // Save to ImportLogItem
                                    AddImportLogItem(ImportLogId, pi.LogNo.Value, content, docItemImporter.Valid, message, false);
                                    UnitOfWork.Save();
                                }
                            }
                            int unchangedItemCount = xmlObjects.ProposalInfoList.Count - successfulItemCount - failedItemCount;
                            logMessage = String.Format(MessageService.ImportLogMessage, xmlObjects.ProposalInfoList.Count, successfulItemCount, unchangedItemCount, failedItemCount);
                            successFlag = failedItemCount == 0 ? true : false;
                        }
                        else
                        {
                            // No applications
                            logMessage = MessageService.NoApplications;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Unexpected exception
                        logMessage = String.Format(MessageService.UnexpectedExceptionMessage, ex.Message);
                    }
                }
                else
                {
                    // Unexpected XML format
                    logMessage = MessageService.InvalidXml;
                }
            }
            else
            {
                // Failed to connect
                logMessage = MessageService.FailedToConnect;
            }
            // Finalize
            FinalizeImportLog(ImportLogId, ProgramMechanismId, successFlag, xml, logMessage, UserId);
        }
        /// <summary>
        /// Fix application parent relationship
        /// </summary>
        private void FixApplicationParentRelationship()
        {
            Regex r = new Regex(@".+P[0-9]{1,2}$", RegexOptions.IgnoreCase);
            var corruptedApps = UnitOfWork.ApplicationRepository.Get(x =>
                x.ProgramMechanismId == ProgramMechanismId &&
                x.ParentApplicationId == null).AsEnumerable().Where(y =>
                r.IsMatch(y.LogNumber));
            if (corruptedApps.Count() > 0)
            {
                foreach(var app in corruptedApps)
                {
                    var parentLogNumber = Regex.Replace(app.LogNumber, @"P[0-9]{1,2}$", string.Empty);
                    var parentApplication = UnitOfWork.ApplicationRepository.GetByLogNumber(parentLogNumber);
                    if (parentApplication != null)
                        app.ParentApplicationId = parentApplication.ApplicationId;
                }
                UnitOfWork.Save();
            }
        }
        /// <summary>
        /// Add import log item.
        /// </summary>
        /// <param name="importLogId"></param>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <param name="successFlag"></param>
        /// <param name="message"></param>
        /// <param name="saveOrNot"></param>
        private void AddImportLogItem(int importLogId, string key, string content, bool successFlag, string message, bool saveOrNot)
        {
            UnitOfWork.ImportLogItemRepository.AddImportLogItem(importLogId, key, content, successFlag, message);
            if (saveOrNot)
                UnitOfWork.Save();
        }
        /// <summary>
        /// Finalizes the import log.
        /// </summary>
        /// <param name="importLogId">The import log identifier.</param>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <param name="successFlag">if set to <c>true</c> [success flag].</param>
        /// <param name="content">The content.</param>
        /// <param name="message">The message.</param>
        /// <param name="userId">The user identifier.</param>
        private void FinalizeImportLog(int importLogId, int programMechanismId, bool successFlag, string content, string message, int userId)
        {
            // Finalize import log entities
            UnitOfWork.ImportLogRepository.SetImportLogStatus((int)importLogId, successFlag, content, message);
            UnitOfWork.ProgramMechanismImportLogRepository.Add(programMechanismId, (int)importLogId, userId);
            UnitOfWork.Save();
        }
        /// <summary>
        /// Whether the item's content was changed or not.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private bool ContainImportLogItemChanges(string key, string content)
        {
            var o = UnitOfWork.ImportLogItemRepository.GetLastSuccessfulImportLogItem(key);
            if (o != null && o.Content == content)
                return false;
            else
                return true;
        }
    }

    /// <summary>
    /// This class imports each ProposalInfo item
    /// </summary>
    public class ProposalInfoItemImporter
    {
        public ProposalInfoItemImporter(IUnitOfWork unitOfWork, ProposalInfo proposalInfo, int programMechanismId, int userId)
        {
            UnitOfWork = unitOfWork;
            ProposalInfo = proposalInfo;
            ProgramMechanismId = programMechanismId;
            UserId = userId;
        }

        public const string GrantId = "Grant ID";
        public const string PrincipalInvestigator = "Principal Investigator";
        public const string Admin1 = "Admin-1";
        public const string Mentor = "Mentor";
        public const string Compliant = "Compliant";

        /// <summary>
        /// Unit of work
        /// </summary>
        public IUnitOfWork UnitOfWork { get; private set; }
        /// <summary>
        /// Proposal info from xml
        /// </summary>
        public ProposalInfo ProposalInfo { get; private set; }
        /// <summary>
        /// Program mechanism identifier
        /// </summary>
        public int ProgramMechanismId { get; private set; }
        /// <summary>
        /// user identifier
        /// </summary>
        public int UserId { get; private set; }
        /// <summary>
        /// Application
        /// </summary>
        public Application Application { get; private set; }
        /// <summary>
        /// Compliance status
        /// </summary>
        public ComplianceStatu ComplianceStatus { get; private set; }
        /// <summary>
        /// Client application info type
        /// </summary>
        public ClientApplicationInfoType ClientApplicationInfoType { get; private set; }
        /// <summary>
        /// PI type
        /// </summary>
        public ClientApplicationPersonnelType PrincipalInvestigatorType { get; private set; }
        /// <summary>
        /// Contract representative type
        /// </summary>
        public ClientApplicationPersonnelType ContractRepresentativeType { get; private set; }
        /// <summary>
        /// Mentor type
        /// </summary>
        public ClientApplicationPersonnelType MentorType { get; private set; }
        /// <summary>
        /// COI types
        /// </summary>
        public List<ClientApplicationPersonnelType> CoiTypes { get; private set; } = new List<ClientApplicationPersonnelType>();
        /// <summary>
        /// Client identifier
        /// </summary>
        public int ClientId { get; private set; }
        /// <summary>
        /// Error messages
        /// </summary>
        public List<string> Messages { get; set; } = new List<string>();
        /// <summary>
        /// Whether the data are valid
        /// </summary>
        public bool Valid {
            get
            {
                return Messages.Count == 0;
            }
        }

        /// <summary>
        /// Start the import process.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        public void StartImport(int clientId)
        {
            ClientId = clientId;
            ValidateApplication();
            if (Valid)
            {
                ImportApplication();
                ImportApplicationComplianceAndInfo();
                ImportApplicationBudget();
                ImportApplicationPersonnel();
            }
        }
        /// <summary>
        /// Validate application
        /// </summary>
        private void ValidateApplication()
        {
            // Get application
            Application = UnitOfWork.ApplicationRepository.GetByLogNumber(ProposalInfo.LogNo.Value);
            if (Application != null && Application.ProgramMechanismId != ProgramMechanismId
                && Application.IsReleased())
            {
                Messages.Add(MessageService.ProgramMechanismInvalidChange);
            }
            // Project start date & end date
            if (!String.IsNullOrEmpty(ProposalInfo.StartDate.Value))
            {
                DateTime pd = default(DateTime);
                bool isStartDateValid = DateTime.TryParse(ProposalInfo.StartDate.Value, out pd);
                if (!isStartDateValid)
                {
                    Messages.Add(MessageService.InvalidStartDate);
                }
            }
            if (!String.IsNullOrEmpty(ProposalInfo.EndDate.Value))
            {
                DateTime pd = default(DateTime);
                bool isEndDateValid = DateTime.TryParse(ProposalInfo.EndDate.Value, out pd);
                if (!isEndDateValid)
                {
                    Messages.Add(MessageService.InvalidEndDate);
                }
            }
            if (!String.IsNullOrEmpty(ProposalInfo.WithdrawnDate.Value))
            {
                DateTime wd = default(DateTime);
                bool isWithdrawnDateValid = DateTime.TryParse(ProposalInfo.WithdrawnDate.Value, out wd);
                if (!isWithdrawnDateValid)
                {
                    Messages.Add(MessageService.InvalidWithdrawnDate);
                }
            }

            if (Application != null && Application.ProgramMechanism.ProgramYear.ClientProgram.ProgramAbbreviation != ProposalInfo.Program.Value)
            {
                Messages.Add(MessageService.InvalidProgramValue);
            }
            if (Application != null && Application.ProgramMechanism.ProgramYear.Year != ProposalInfo.Fy.Value)
            {
                Messages.Add(MessageService.InvalidFyValue);
            }
            if (Application != null && Application.ProgramMechanism.ReceiptCycle.ToString() != ProposalInfo.ReceiptCycle.Value)
            {
                Messages.Add(MessageService.InvalidReceiptCycle);
            }
            // Compliance status
            if (string.IsNullOrEmpty(ProposalInfo.ComplianceStatus.Value))
                ProposalInfo.ComplianceStatus.Value = Compliant;
            ComplianceStatus = UnitOfWork.ComplianceStatusRepository.GetByNoDashLabel(ProposalInfo.ComplianceStatus.Value);
            if (ComplianceStatus == null)
            {
                Messages.Add(MessageService.InvalidComplianceStatus);
            }
            // Client application info type
            ClientApplicationInfoType = UnitOfWork.ClientApplicationInfoTypeRepository.GetByDescription(ClientId, GrantId);
            if (ClientApplicationInfoType == null)
            {
                Messages.Add(MessageService.MissingGrantIdType);
            }
            // Budget
            if (!String.IsNullOrEmpty(ProposalInfo.TotalFunding.Value))
            {
                decimal tf = default(decimal);
                bool isTotalFundingValid = Decimal.TryParse(ProposalInfo.TotalFunding.Value, out tf);
                if (!isTotalFundingValid)
                {
                    Messages.Add(MessageService.InvalidTotalFundingValue);
                }
            }
            if (!String.IsNullOrEmpty(ProposalInfo.RequestedDirect.Value))
            {
                decimal tf = default(decimal);
                bool isRequestedDirectValid = Decimal.TryParse(ProposalInfo.RequestedDirect.Value, out tf);
                if (!isRequestedDirectValid)
                {
                    Messages.Add(MessageService.InvalidRequestedDirectValue);
                }
            }
            if (!String.IsNullOrEmpty(ProposalInfo.RequestedIndirect.Value))
            {
                decimal tf = default(decimal);
                bool isRequestedIndirectValid = Decimal.TryParse(ProposalInfo.RequestedIndirect.Value, out tf);
                if (!isRequestedIndirectValid)
                {
                    Messages.Add(MessageService.InvalidRequestedIndirectValue);
                }
            }
            // Personnel
            PrincipalInvestigatorType = UnitOfWork.ClientApplicationPersonnelTypeRepository.GetByApplicationPersonnelType(ClientId, PrincipalInvestigator);
            if (PrincipalInvestigatorType == null)
            {
                Messages.Add(MessageService.CouldNotMatchPiType);
            }
            ContractRepresentativeType = UnitOfWork.ClientApplicationPersonnelTypeRepository.GetByApplicationPersonnelType(ClientId, Admin1);
            if (ContractRepresentativeType == null)
            {
                Messages.Add(MessageService.CouldNotMatchCrType);
            }
            MentorType = UnitOfWork.ClientApplicationPersonnelTypeRepository.GetByApplicationPersonnelType(ClientId, Mentor);
            if (MentorType == null)
            {
                Messages.Add(MessageService.CouldNotMatchMentorType);
            }
            for (var i = 0; i < ProposalInfo.CoiDataList.Count; i++)
            {
                var coi = ProposalInfo.CoiDataList[i];
                if (!String.IsNullOrEmpty(coi.CoiType.Value))
                {
                    int ct = default(int);
                    bool isCoiTypeValid = int.TryParse(coi.CoiType.Value, out ct);
                    if (isCoiTypeValid)
                    {
                        var externalPersonnelTypeId = coi.CoiType != null ? Convert.ToInt32(coi.CoiType.Value) : (int?)null;
                        var capt = UnitOfWork.ClientApplicationPersonnelTypeRepository.GetByExternalPersonnelTypeId(ClientId, externalPersonnelTypeId);
                        if (capt != null)
                        {
                            CoiTypes.Add(capt);
                        }
                        else
                        {
                            Messages.Add(String.Format(MessageService.CouldNotMatchCoiType, coi.CoiType.Value));
                        }
                    }
                    else
                    {
                        Messages.Add(String.Format(MessageService.InvalidCoiType, coi.CoiType.Value));
                    }
                }
                else
                {
                    Messages.Add(MessageService.MissingCoiType);
                }
            }
        }
        /// <summary>
        /// Import application
        /// </summary>
        private void ImportApplication()
        {
            var startDate = ProposalInfo.StartDate.Value != null ? Convert.ToDateTime(ProposalInfo.StartDate.Value) : (DateTime?)null;
            var endDate = ProposalInfo.EndDate.Value != null ? Convert.ToDateTime(ProposalInfo.EndDate.Value) : (DateTime?)null;
            var withdrawnDate = ProposalInfo.WithdrawnDate.Value != null ? Convert.ToDateTime(ProposalInfo.WithdrawnDate.Value) : (DateTime?)null;
            if (Application == null)
            {
                Application = UnitOfWork.ApplicationRepository.Add(ProposalInfo.LogNo.Value, ProgramMechanismId, ProposalInfo.ProposalTitle.Value, ProposalInfo.Keywords.Value, startDate, endDate, withdrawnDate, UserId);
            }
            else
            {
                UnitOfWork.ApplicationRepository.Update(Application, ProposalInfo.ProposalTitle.Value, ProposalInfo.Keywords.Value, startDate, endDate, withdrawnDate, UserId);
            }
        }
        /// <summary>
        /// Import application compliance and application info
        /// </summary>
        private void ImportApplicationComplianceAndInfo()
        {
            // Application compliance
            var appCompliance = Application.ApplicationCompliances.FirstOrDefault();
            if (appCompliance == null)
            {
                appCompliance = UnitOfWork.ApplicationComplianceRepository.Add(Application.ApplicationId, ComplianceStatus.ComplianceStatusId, UserId);
            }
            else
            {
                UnitOfWork.ApplicationComplianceRepository.Update(appCompliance, ComplianceStatus.ComplianceStatusId, UserId);
            }
            // Application info
            var appInfo = Application.ApplicationInfoes.Where(x => x.ClientApplicationInfoTypeId == ClientApplicationInfoType.ClientApplicationInfoTypeId).FirstOrDefault();
            if (appInfo == null)
            {
                if (!string.IsNullOrEmpty(ProposalInfo.GgTrackingNbr.Value))
                {
                    UnitOfWork.ApplicationInfoRepository.Add(Application.ApplicationId, ClientApplicationInfoType.ClientApplicationInfoTypeId, ProposalInfo.GgTrackingNbr.Value, UserId);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(ProposalInfo.GgTrackingNbr.Value))
                {
                    UnitOfWork.ApplicationInfoRepository.Update(appInfo, ProposalInfo.GgTrackingNbr.Value, UserId);
                }
                else
                {
                    Helper.UpdateDeletedFields(appInfo, UserId);
                    UnitOfWork.ApplicationInfoRepository.Delete(appInfo);
                }
            }
        }
        /// <summary>
        /// Import application budget
        /// </summary>
        private void ImportApplicationBudget()
        {
            var budget = Application.ApplicationBudgets.FirstOrDefault();
            var totalFunding = ProposalInfo.TotalFunding.Value != null ? Convert.ToDecimal(ProposalInfo.TotalFunding.Value) : (decimal?)null;
            var requestedDirect = ProposalInfo.RequestedDirect.Value != null ? Convert.ToDecimal(ProposalInfo.RequestedDirect.Value) : (decimal?)null;
            var requestedIndirect = ProposalInfo.RequestedIndirect.Value != null ? Convert.ToDecimal(ProposalInfo.RequestedIndirect.Value) : (decimal?)null;
            if (budget == null)
            {
                // Add
                UnitOfWork.ApplicationBudgetRepository.Add(Application.ApplicationId, totalFunding, requestedDirect, requestedIndirect, UserId);
            }
            else
            {
                // Update
                UnitOfWork.ApplicationBudgetRepository.Update(budget, totalFunding, requestedDirect, requestedIndirect, UserId);
            }
        }
        /// <summary>
        /// Import application personnel
        /// </summary>
        private void ImportApplicationPersonnel()
        {
            // PI
            var pi = Application.ApplicationPersonnels.Where(x => x.PrimaryFlag).FirstOrDefault();
            if (pi == null)
            {

                pi = UnitOfWork.ApplicationPersonnelRepository.Add(Application.ApplicationId, PrincipalInvestigatorType.ClientApplicationPersonnelTypeId,
                    ProposalInfo.PiFirstName.Value, ProposalInfo.PiLastName.Value, ProposalInfo.PiMiddleInitial.Value, ProposalInfo.PiOrganization.Value, ProposalInfo.PiState.Value,
                    ProposalInfo.PiEmail.Value, ProposalInfo.PiTelephone.Value, true, null, UserId);
            }
            else
            {
                UnitOfWork.ApplicationPersonnelRepository.Update(pi,
                    ProposalInfo.PiFirstName.Value, ProposalInfo.PiLastName.Value, ProposalInfo.PiMiddleInitial.Value, ProposalInfo.PiOrganization.Value, ProposalInfo.PiState.Value,
                    ProposalInfo.PiEmail.Value, ProposalInfo.PiTelephone.Value, true, UserId);
            }
            // CR
            var cr = UnitOfWork.ApplicationPersonnelRepository.GetByApplicationPersonnelType(Application.ApplicationId, Admin1);
            if (cr == null)
            {
                cr = UnitOfWork.ApplicationPersonnelRepository.Add(Application.ApplicationId, ContractRepresentativeType.ClientApplicationPersonnelTypeId,
                    ProposalInfo.CrFirstName.Value, ProposalInfo.CrLastName.Value, ProposalInfo.CrMiddleInitial.Value, ProposalInfo.CrOrganization.Value, ProposalInfo.CrState.Value,
                    ProposalInfo.CrEmail.Value, ProposalInfo.CrTelephone.Value, false, null, UserId);
            }
            else
            {
                UnitOfWork.ApplicationPersonnelRepository.Update(cr,
                        ProposalInfo.CrFirstName.Value, ProposalInfo.CrLastName.Value, ProposalInfo.CrMiddleInitial.Value, ProposalInfo.CrOrganization.Value, ProposalInfo.CrState.Value,
                        ProposalInfo.CrEmail.Value, ProposalInfo.CrTelephone.Value, false, UserId);
            }
            // Mentor
            var mentor = UnitOfWork.ApplicationPersonnelRepository.GetByApplicationPersonnelType(Application.ApplicationId, Mentor);
            if (mentor == null)
            {
                if (!string.IsNullOrWhiteSpace(ProposalInfo.MentorFirstName.Value) || !string.IsNullOrWhiteSpace(ProposalInfo.MentorLastName.Value))
                    mentor = UnitOfWork.ApplicationPersonnelRepository.Add(Application.ApplicationId, MentorType.ClientApplicationPersonnelTypeId,
                        ProposalInfo.MentorFirstName.Value, ProposalInfo.MentorLastName.Value, ProposalInfo.MentorMiddleInitial.Value, null, null,
                        null, null, false, null, UserId);
            }
            else
            {
                UnitOfWork.ApplicationPersonnelRepository.UpdateName(mentor,
                    ProposalInfo.MentorFirstName.Value, ProposalInfo.MentorLastName.Value, ProposalInfo.MentorMiddleInitial.Value, UserId);
            }
            // COI
            /* Temporarily disabled due to mismatch with egs
            UnitOfWork.ApplicationPersonnelRepository.DeleteCoiList(Application.ApplicationId, UserId);
            for (var i = 0; i < ProposalInfo.CoiDataList.Count; i++)
            {
                var coi = ProposalInfo.CoiDataList[i];
                UnitOfWork.ApplicationPersonnelRepository.Add(Application.ApplicationId, CoiTypes[i].ClientApplicationPersonnelTypeId,
                    coi.CoiFirstName.Value, coi.CoiLastName.Value, null, coi.CoiOrgName.Value, null, coi.CoiEmail.Value, coi.CoiPhone.Value, false, coi.CoiSource.Value, UserId);
            }
            */
        }
    }
}
