using Sra.P2rmis.Bll.ModelBuilders.Setup;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Setup;
using System.Linq;
using System.Collections.Generic;
using Sra.P2rmis.Bll.PanelManagement;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.Dal.ResultModels;

namespace Sra.P2rmis.Bll.Setup
{
    /// <summary>
    /// Provides services for System Setup.
    /// </summary>
    public partial interface ISetupService
    {
        /// <summary>
        /// Retrieves a container of physical years for the given client.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IProgramYearModel models for the given client</returns>
        Container<IFilterableProgramYearModel> RetrieveClientProgramYears(int clientId);
        /// <summary>
        /// Retrieve a container of filterable (by Active) programs
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="year">ProgramYear</param>
        /// <returns>Container of IFilterableProgramModel models for the given client, program year</returns>
        Container<IFilterableProgramModel> RetrieveFilterablePrograms(int clientId, string year);
        /// <summary>
        /// Retrieves a container of data to populate the Award/Mechanism grid.
        /// </summary>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <returns>Container of IAwardMechanismModel models for the given program year</returns>
        Container<IAwardMechanismModel> RetrieveAwardMechanismSetup(int programYearId);
        /// <summary>
        /// Retrieves a container to populate the Award Setup wizard.
        /// </summary>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <param name="programMechanismId">ProgramMechanism entity identifier</param>
        /// <returns>Container of IAwardSetupWizardModel models for the given ProgramYear, ProgramMechanism id</returns>
        Container<IAwardSetupWizardModel> RetrieveAwardSetupModal(int programYearId, int programMechanismId);

        /// <summary>
        /// Determines whether [has panel applications] [the specified program year identifier].
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns>
        ///   <c>true</c> if [has panel applications] [the specified program year identifier]; otherwise, <c>false</c>.
        /// </returns>
        bool HasPanelApplications(int programYearId, int receiptCycle);
        /// <summary>
        /// Resets the referral mapping.
        /// </summary>
        /// <param name="referralMappingId">The referral mapping identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        bool ResetReferralMapping(int referralMappingId, int userId);
        /// <summary>
        /// Releases the referral mapping.
        /// </summary>
        /// <param name="panelManagementService">The panel management service.</param>
        /// <param name="referralMappingId">The referral mapping identifier.</param>
        /// <param name="sessionPanelIds">The session panel ids.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        bool ReleaseReferralMapping(IPanelManagementService panelManagementService, int referralMappingId, List<int> sessionPanelIds, int userId);
        /// <summary>
        /// get referral mapping
        /// </summary>
        /// <param name="referralMappingId"></param>
        /// <returns></returns>
        List<ReferralMappingModel> GetReferralMapping(int referralMappingId);
        /// <summary>
        /// Gets the uploaded referral mapping.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        List<ReferralMapping> GetUploadedReferralMapping(int programYearId, int receiptCycle);
        /// <summary>
        /// Validates the uploaded referral mapping.
        /// </summary>
        /// <param name="applicationNotreleased">The application notreleased.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        void ValidateUploadedReferralMapping(List<ReferralMappingModel> applicationNotreleased, int programYearId, int receiptCycle);
        /// <summary>
        /// Gets the peer review data XML.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        string GetPeerReviewDataXml(int clientId);

        /// <summary>
        /// Gets summary setup info for a given mechanism.
        /// </summary>
        /// <param name="programMechanismId">Mechanism identifier</param>
        /// <returns></returns>
        ISummarySetupModel GetSummarySetupInfo(int programMechanismId);
    }
    /// <summary>
    /// Provides services for System Setup.
    /// </summary>
    public partial class SetupService : ServerBase, ISetupService
    {
        #region Construction & Setup & Disposal
        /// <summary>
        /// Default constructor
        /// </summary>
        public SetupService()
        {
            UnitOfWork = new UnitOfWork(); 
        }
        #endregion
        #region Services
        /// <summary>
        /// Retrieves a container of physical years for the given client.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of IProgramYearModel models for the given client</returns>
        public virtual Container<IFilterableProgramYearModel> RetrieveClientProgramYears(int clientId)
        {
            string name = FullName(nameof(SetupService), nameof(RetrieveClientProgramYears));
            ValidateInt(clientId, name, nameof(clientId));

            ProgramYearModelBuilder builder = new ProgramYearModelBuilder(this.UnitOfWork, clientId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Retrieve a container of filterable (by Active) programs
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="year">ProgramYear</param>
        /// <returns>Container of IFilterableProgramModel models for the given client, program year</returns>
        public virtual Container<IFilterableProgramModel> RetrieveFilterablePrograms(int clientId, string year)
        {
            string name = FullName(nameof(SetupService), nameof(RetrieveFilterablePrograms));
            ValidateInt(clientId, name, nameof(clientId));
            ValidateString(year, name, nameof(year));

            FilterableProgramModelBuilder builder = new FilterableProgramModelBuilder(this.UnitOfWork, clientId, year);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Get filtered programs
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="year">The year.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <returns></returns>
        public IEnumerable<IFilterableProgramModel> GetFilterablePrograms(int clientId, string year, int clientMeetingId)
        {
            var programs = UnitOfWork.ClientProgramRepository.ProgramSetup(new List<int>() { clientId })
                .Where(x => x.Year == year && !x.DateClosed.HasValue && x.ProgramMeetings.Count(y =>
                y.ClientMeetingId == clientMeetingId) > 0).ToList().ConvertAll(
                    x =>
                    new FilterableProgramModel()
                    {
                        ProgramYearId = x.ProgramYearId,
                        ClientProgramId = x.ClientProgramId,
                        ProgramAbbreviation = x.ClientProgram.ProgramAbbreviation,
                        ProgramDescription = x.ClientProgram.ProgramDescription,
                        IsActive = true
                    });
            return programs;
        }
        /// <summary>
        /// Retrieves a container of data to populate the Award/Mechanism grid.
        /// </summary>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <returns>Container of IAwardMechanismModel models for the given program year</returns>
        public virtual Container<IAwardMechanismModel> RetrieveAwardMechanismSetup(int programYearId)
        {
            ValidateInt(programYearId, FullName(nameof(SetupService), nameof(RetrieveAwardMechanismSetup)), nameof(programYearId));

            AwardMechanismSetupModelBuilder builder = new AwardMechanismSetupModelBuilder(this.UnitOfWork, programYearId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Retrieves a container to populate the Award Setup wizard.
        /// </summary>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <param name="programMechanismId">ProgramMechanism entity identifier</param>
        /// <returns>Container of IAwardSetupWizardModel models for the given ProgramYear, ProgramMechanism id</returns>
        public virtual Container<IAwardSetupWizardModel> RetrieveAwardSetupModal(int programYearId, int programMechanismId)
        {
            string name = FullName(nameof(SetupService), nameof(RetrieveAwardSetupModal));
            ValidateInt(programYearId, name, nameof(programYearId));
            ValidateInt(programMechanismId, name, nameof(programMechanismId));

            AwardSetupWizardModelBuilder builder = new AwardSetupWizardModelBuilder(this.UnitOfWork, programYearId, programMechanismId);
            builder.BuildContainer();
            return builder.Results;
        }

        /// <summary>
        /// Determines whether [has panel applications] [the specified program year identifier].
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns>
        ///   <c>true</c> if [has panel applications] [the specified program year identifier]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasPanelApplications(int programYearId, int receiptCycle)
        {
            var os = UnitOfWork.PanelApplicationRepository.GetPanelApplications(programYearId, receiptCycle).ToList();
            return os.Count > 0;
        }
        /// <summary>
        /// Resets the referral mapping.
        /// </summary>
        /// <param name="referralMappingId">The referral mapping identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public bool ResetReferralMapping(int referralMappingId, int userId)
        {
            bool flag = false;
            // Reset if ProgramYear/Cycle doesn't contain assigned applications
            var rm = UnitOfWork.ReferralMappingRepository.GetByID(referralMappingId);
            if (rm != null && !HasPanelApplications(rm.ProgramYearId, (int)rm.ReceiptCycle))
            {
                UnitOfWork.ReferralMappingDataRepository.DeleteReferralMappingData(referralMappingId, userId);
                UnitOfWork.ReferralMappingRepository.DeleteReferralMapping(referralMappingId, userId);
                UnitOfWork.Save();
                flag = true;
            }
            return flag;
        }
        /// <summary>
        /// Releases the referral mapping.
        /// </summary>
        /// <param name="panelManagementService">The panel management service.</param>
        /// <param name="referralMappingId">The referral mapping identifier.</param>
        /// <param name="sessionPanelIds">The session panel ids.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public bool ReleaseReferralMapping(IPanelManagementService panelManagementService, int referralMappingId, List<int> sessionPanelIds, int userId)
        {
            bool flag = false;
            // Release if none of the panels contain assigned applications
            var rm = UnitOfWork.ReferralMappingRepository.GetReferralMapping(referralMappingId);
            var os = UnitOfWork.PanelApplicationRepository.GetPanelApplications(rm.ProgramYearId, rm.ReceiptCycle).ToList();
            if (os.Count(x => sessionPanelIds.Contains(x.SessionPanelId)) == 0) {
                var panelApplicationsList = new List<KeyValuePair<int, List<int>>>();
                foreach (var sessionPanelId in sessionPanelIds)
                {
                    var applicationIds = rm.ReferralMappingDatas.Where(x => x.SessionPanelId == sessionPanelId).ToList()
                        .ConvertAll(x => x.ApplicationId);
                    panelApplicationsList.Add(new KeyValuePair<int, List<int>>(sessionPanelId, applicationIds));
                }
                panelManagementService.AddBatchPanelApplications(panelApplicationsList, userId);
                flag = true;
            }
            return flag;
        }
        /// <summary>
        /// get referral mapping
        /// </summary>
        /// <param name="referralMappingId"></param>
        /// <returns></returns>
        public List<ReferralMappingModel> GetReferralMapping(int referralMappingId)
        {
            return UnitOfWork.ReferralMappingDataRepository.GetReferralMappingModels(referralMappingId).ToList();
        }
        /// <summary>
        /// Gets the uploaded referral mapping.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns></returns>
        public List<ReferralMapping> GetUploadedReferralMapping(int programYearId, int receiptCycle)
        {
            return UnitOfWork.ReferralMappingRepository.Select().Where(x => x.ProgramYearId == programYearId && x.ReceiptCycle == receiptCycle).ToList();
        }
        /// <summary>
        /// Validates the uploaded referral mapping.
        /// </summary>
        /// <param name="applicationNotreleased">The application notreleased.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        public void ValidateUploadedReferralMapping(List<ReferralMappingModel> applicationNotreleased, int programYearId, int receiptCycle)
        {
            var logNumber = UnitOfWork.ApplicationRepository.FindApplications(programYearId, receiptCycle);
            var panelAbbr = UnitOfWork.ApplicationRepository.FindPanelAbbreviations(programYearId, receiptCycle).Distinct();
        }
        /// <summary>
        /// Gets the peer review data XML.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <returns></returns>
        public string GetPeerReviewDataXml(int clientId)
        {
            var os = UnitOfWork.PanelApplicationRepository.GetPeerReviewData(clientId);
            var container = GetPeerReviewModelContainer(os);
            var outXml = XMLServices.Serialize(container);
            return outXml;
        }
        /// <summary>
        /// Gets the peer review model container.
        /// </summary>
        /// <param name="peerReviewData">The peer review data.</param>
        /// <returns></returns>
        private PeerReviewModelContainer GetPeerReviewModelContainer(List<PeerReviewResultModel> peerReviewData)
        {
            var container = new PeerReviewModelContainer();
            var models = new List<PeerReviewModel>();
            for (var i = 0; i < peerReviewData.Count; i++)
            {
                var o = peerReviewData[i];
                if (i == 0 || o.ApplicationId != models[models.Count - 1].ApplicationId)
                {
                    // Add new entity
                    var model = new PeerReviewModel();
                    model.ApplicationId = o.ApplicationId;
                    model.PanelApplicationId = o.PanelApplicationId;
                    model.LogNumber = o.LogNumber;
                    model.PanelName = o.PanelName;
                    model.PanelAbbreviation = o.PanelAbbreviation;
                    model.StartDate = o.StartDate;
                    model.EndDate = o.EndDate;
                    model.MeetingTypeName = o.MeetingTypeName;
                    model.AssignmentReleaseDate = o.AssignmentReleaseDate;
                    model.ReviewStatusId = o.ReviewStatusId;
                    model.ReviewStatusLabel = o.ReviewStatusLabel;
                    model.AvgScore = o.AvgScore;
                    model.StDev = o.StDev;
                    model.ScreeningTcDate = o.ScreeningTcDate;
                    model.ReceiptCycle = o.ReceiptCycle;
                    model.Year = o.Year;
                    var reviewer = new PeerReviewReviewerModel();
                    reviewer.FirstName = o.FirstName;
                    reviewer.LastName = o.LastName;
                    reviewer.ClientAssignmentTypeId = o.ClientAssignmentTypeId;
                    reviewer.AssignmentLabel = o.AssignmentLabel;
                    reviewer.SortOrder = o.SortOrder;
                    reviewer.CoiSignedDate = o.CoiSignedDate;
                    reviewer.ResolutionDate = o.ResolutionDate;
                    reviewer.ScreeningTcCritiqueDate = o.ScreeningTcCritiqueDate;
                    model.Reviewers.ReviewerList.Add(reviewer);
                    models.Add(model);
                }
                else
                {
                    // Add reviewer to the last entity
                    var model = models[models.Count - 1];
                    var reviewer = new PeerReviewReviewerModel();
                    reviewer.FirstName = o.FirstName;
                    reviewer.LastName = o.LastName;
                    reviewer.ClientAssignmentTypeId = o.ClientAssignmentTypeId;
                    reviewer.AssignmentLabel = o.AssignmentLabel;
                    reviewer.SortOrder = o.SortOrder;
                    reviewer.CoiSignedDate = o.CoiSignedDate;
                    reviewer.ResolutionDate = o.ResolutionDate;
                    reviewer.ScreeningTcCritiqueDate = o.ScreeningTcCritiqueDate;
                    model.Reviewers.ReviewerList.Add(reviewer);
                }
            }
            container.PeerReviewData = models;
            container.RowCount = models.Count;
            return container;
        }
        /// <summary>
        /// Gets summary setup info for a given mechanism.
        /// </summary>
        /// <param name="programMechanismId">Mechanism identifier</param>
        /// <returns></returns>
        public ISummarySetupModel GetSummarySetupInfo(int programMechanismId)
        {
            var mech = UnitOfWork.ProgramMechanismRepository.Select()
                                    .Where(w => w.ProgramMechanismId == programMechanismId);
            var summarySetupModel = mech.Select(x => new SummarySetupModel()
            {
                Program = x.ProgramYear.ClientProgram.ProgramAbbreviation,
                Award = x.ClientAwardType.AwardAbbreviation,
                FiscalYear = x.ProgramYear.Year,
                Client = x.ClientAwardType.Client.ClientAbrv,
                ClientId = x.ClientAwardType.ClientId,
                SelectedStandardSummaryTemplateId = x.ProgramMechanismSummaryStatements.FirstOrDefault(y => y.ReviewStatusId != ReviewStatu.Triaged).ClientSummaryTemplateId,
                SelectedExpeditedSummaryTemplateId = x.ProgramMechanismSummaryStatements.FirstOrDefault(y => y.ReviewStatusId == ReviewStatu.Triaged).ClientSummaryTemplateId,
                LastUpdatedByLastName = x.User.UserInfoes.FirstOrDefault().LastName,
                LastUpdatedByFirstName = x.User.UserInfoes.FirstOrDefault().FirstName,
                LastUpdateDate = x.SummarySetupLastUpdatedDate,
                ReviewerDescriptions = x.SummaryReviewerDescriptions.Where(w => w.AssignmentOrder != null).Select(y => new SummaryStatementReviewerDescription()
                {
                    SummaryReviewerDescriptionId = y.SummaryReviewerDescriptionId,
                    AssignmentOrder = (int)y.AssignmentOrder,
                    CustomOrder = y.CustomOrder,
                    DisplayName = y.DisplayName
                })
            }).FirstOrDefault();

            summarySetupModel.AvailableSummaryTemplates = UnitOfWork.ClientSummaryTemplateRepository
                                                        .Get(x => x.ClientId == summarySetupModel.ClientId
                                                        && (x.ActiveFlag || x.ClientSummaryTemplateId == summarySetupModel.SelectedStandardSummaryTemplateId || x.ClientSummaryTemplateId == summarySetupModel.SelectedExpeditedSummaryTemplateId))
                                                        .Select(x => new SummaryTemplate()
                                                        {
                                                            ClientSummaryTemplateId = x.ClientSummaryTemplateId,
                                                            TemplateName = x.TemplateName,
                                                            IsExpedited = x.ExpeditedFlag
                                                        });


            return summarySetupModel;
        }
        #endregion
    }
}