using Sra.P2rmis.WebModels.Setup;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Sra.P2rmis.Dal;
using System;
using Sra.P2rmis.CrossCuttingServices;
using Sra.P2rmis.CrossCuttingServices.OpenXmlServices;
using Sra.P2rmis.WebModels.HelperClasses;
using Sra.P2rmis.CrossCuttingServices.HttpServices;
using System.Threading.Tasks;
using Sra.P2rmis.CrossCuttingServices.MessageServices;

namespace Sra.P2rmis.Bll.Setup
{
    public partial interface ISetupService
    {
        /// <summary>
        /// Retrieves the mechanism transfer details.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <returns></returns>
        IEnumerable<ITransferDataModel> RetrieveMechanismTransferDetails(int programYearId, int cycle);

        /// <summary>
        /// Retrieves the program deliverable details regarding generation and QC.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <returns>Model populated with deliverable information</returns>
        IEnumerable<IProgramDeliverableModel> RetrieveProgramDeliverableDetails(int programYearId, int cycle);

        /// <summary>
        /// Gets the deliverable XML.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="program">The program.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="clientDataDeliverableId">The client data deliverable identifier.</param>
        /// <returns></returns>
        string GetDeliverableXml(string fiscalYear, string program, int? cycle, int clientDataDeliverableId);
        /// <summary>
        /// Marks the deliverable as qc (quality control) checked.
        /// </summary>
        /// <param name="programCycleDeliverableId">The program cycle deliverable identifier.</param>
        /// <param name="userId">The user identifier.</param>
        void MarkDeliverableQc(int programCycleDeliverableId, int userId);

        /// <summary>
        /// Generates the deliverable.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <param name="clientDeliverableId">The client deliverable identifier.</param>
        /// <param name="userId">The user identifier.</param>
        int GenerateDeliverable(int programYearId, int receiptCycle, int clientDeliverableId, int userId);

        IDeliverableFileModel DownloadExcelDeliverable(int programCycleDeliverableId);
        /// <summary>
        /// Gets the award.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <returns></returns>
        IAwardModel GetAward(int programMechanismId);
        /// <summary>
        /// Transfers the data.
        /// </summary>
        /// <param name="clientTransferTypeId">The client transfer type identifier.</param>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <param name="credentialKey">The credential key.</param>
        /// <param name="url">The URL.</param>
        /// <param name="userId">The user identifier.</param>
        int TransferData(int clientTransferTypeId, int programMechanismId, string credentialKey, string url, int userId);
        /// <summary>
        /// Gets the import log.
        /// </summary>
        /// <param name="importLogId">The import log identifier.</param>
        /// <returns></returns>
        IImportLogModel GetImportLog(int importLogId);
    }
    public partial class SetupService
    {
        /// <summary>
        /// Retrieves the mechanism application data transfer details.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <returns>Model collection of application import data</returns>
        public IEnumerable<ITransferDataModel> RetrieveMechanismTransferDetails(int programYearId, int cycle)
        {
            ValidateInt(programYearId, FullName(nameof(SetupService), nameof(RetrieveMechanismTransferDetails)), nameof(programYearId));
            ValidateInt(cycle, FullName(nameof(SetupService), nameof(RetrieveMechanismTransferDetails)), nameof(cycle));

            var mechanisms = this.UnitOfWork.ProgramMechanismRepository.Select().Where(x => x.ProgramYearId == programYearId && x.ReceiptCycle == cycle);
            IEnumerable<ITransferDataModel> results = mechanisms.Select(x => new TransferDataModel
            {
                Mechanism = x.ClientAwardType.AwardDescription,
                ProgramMechanismId = x.ProgramMechanismId,
                FundingOpportunityId = x.FundingOpportunityId,
                ReceiptDate = x.ReceiptDeadline,
                LastSuccessfulImportDate = x.ProgramMechanismImportLogs.Select(y => y.ImportLog).Where(
                    z => z.SuccessFlag == true).OrderByDescending(z => z.Timestamp)
                    .FirstOrDefault().Timestamp,
                LastImportDate = x.ProgramMechanismImportLogs.Select(y => y.ImportLog).Max(y => y.Timestamp),
                LastImportLogId = x.ProgramMechanismImportLogs.OrderByDescending(y => y.CreatedDate).FirstOrDefault().ImportLogId,
                HasError = x.ProgramMechanismImportLogs.OrderByDescending(y => y.CreatedDate)
                    .FirstOrDefault().ImportLog.SuccessFlag == false
            });
            return results;
        }
        /// <summary>
        /// Retrieves the program deliverable details regarding generation and QC.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="cycle">The cycle.</param>
        /// <returns>Model populated with deliverable information</returns>
        public IEnumerable<IProgramDeliverableModel> RetrieveProgramDeliverableDetails(int programYearId, int cycle)
        {
            ValidateInt(programYearId, FullName(nameof(SetupService), nameof(RetrieveProgramDeliverableDetails)), nameof(programYearId));
            ValidateInt(cycle, FullName(nameof(SetupService), nameof(RetrieveProgramDeliverableDetails)), nameof(cycle));

            var clientDeliverables = this.UnitOfWork.ProgramYearRepository.Select().Where(x => x.ProgramYearId == programYearId).Select(x => x.ClientProgram.Client).SelectMany(x => x.ClientDataDeliverables);
            Expression<Func<ProgramCycleDeliverable, bool>> filter = y => y.ProgramYearId == programYearId && 
                ((y.ClientDataDeliverable.ApiMethod != ClientDataDeliverable.ApiMethods.PanelDeliverable && y.ReceiptCycle == cycle) ||
                (y.ClientDataDeliverable.ApiMethod == ClientDataDeliverable.ApiMethods.PanelDeliverable && y.ReceiptCycle == null));
            IEnumerable<IProgramDeliverableModel> results = clientDeliverables.Select(x => new ProgramDeliverableModel
            {
                DeliverableName = x.Label,
                GeneratedDate = x.ProgramCycleDeliverables.AsQueryable().Where(filter).FirstOrDefault().GeneratedDate,
                GeneratedByName = x.ProgramCycleDeliverables.AsQueryable().Where(filter).FirstOrDefault().User.UserLogin,
                QcDate = x.ProgramCycleDeliverables.AsQueryable().Where(filter).FirstOrDefault().QcDate,
                QcFlag = x.ProgramCycleDeliverables.AsQueryable().Where(filter).FirstOrDefault().QcFlag,
                QcByName = x.ProgramCycleDeliverables.AsQueryable().Where(filter).FirstOrDefault().User1.UserLogin,
                ClientDataDeliverableId = x.ClientDataDeliverableId,
                ProgramCycleDeliverableId = x.ProgramCycleDeliverables.AsQueryable().Where(filter).FirstOrDefault().ProgramCycleDeliverableId,
                SortOrder = x.SortOrder,
                ProgramYearId = programYearId,
                ReceiptCycle = cycle
            });
            return results.OrderBy(x => x.SortOrder);
        }
        /// <summary>
        /// Gets the deliverable XML.
        /// </summary>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="program">The program.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="clientDataDeliverableId">The client data deliverable identifier.</param>
        /// <returns></returns>
        public string GetDeliverableXml(string fiscalYear, string program, int? cycle, int clientDataDeliverableId)
        {
            var programYear = GetProgramYear(program, fiscalYear);
            var deliverable = default(ProgramCycleDeliverable);
            var clientDeliverable = UnitOfWork.ClientDataDeliverableRepository.GetByID(clientDataDeliverableId);
            cycle = clientDeliverable.ApiMethod != ClientDataDeliverable.ApiMethods.PanelDeliverable ? cycle : null;
            if (programYear != null)
            {
                deliverable = UnitOfWork.ProgramCycleDeliverableRepository.Select().Where(x => x.ProgramYearId == programYear.ProgramYearId && x.ReceiptCycle == cycle && 
                                    x.ClientDataDeliverableId == clientDataDeliverableId)
                                .OrderByDescending(y => y.ModifiedDate).FirstOrDefault();
            }
            var output = GenerateDeliverableOutput(deliverable != null,
                deliverable != null && deliverable.QcFlag,
                deliverable != null ? deliverable.DeliverableFile : string.Empty, 
                fiscalYear, cycle, clientDataDeliverableId);
            return output;
        }
        /// <summary>
        /// Marks the deliverable as qc (quality control) checked.
        /// </summary>
        /// <param name="programCycleDeliverableId">The program cycle deliverable identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public void MarkDeliverableQc(int programCycleDeliverableId, int userId)
        {
            ValidateInt(programCycleDeliverableId, FullName(nameof(SetupService), nameof(MarkDeliverableQc)), nameof(programCycleDeliverableId));
            ValidateInt(userId, FullName(nameof(SetupService), nameof(MarkDeliverableQc)), nameof(userId));

            var theDeliverable = UnitOfWork.ProgramCycleDeliverableRepository.GetByID(programCycleDeliverableId);
            theDeliverable.MarkQc(userId);
            Helper.UpdateModifiedFields(theDeliverable, userId);
            UnitOfWork.Save();
        }

        /// <summary>
        /// Generates the deliverable.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <param name="clientDeliverableId">The client deliverable identifier.</param>
        /// <param name="userId">The user identifier.</param>
        public int GenerateDeliverable(int programYearId, int receiptCycle, int clientDeliverableId, int userId)
        {
            ValidateInt(programYearId, FullName(nameof(SetupService), nameof(GenerateDeliverable)), nameof(programYearId));
            ValidateInt(receiptCycle, FullName(nameof(SetupService), nameof(GenerateDeliverable)), nameof(receiptCycle));
            ValidateInt(clientDeliverableId, FullName(nameof(SetupService), nameof(GenerateDeliverable)), nameof(clientDeliverableId));
            ValidateInt(userId, FullName(nameof(SetupService), nameof(GenerateDeliverable)), nameof(userId));

            //Get the deliverable metadata
            var clientDeliverable = UnitOfWork.ClientDataDeliverableRepository.GetByID(clientDeliverableId);
            //Get the deliverable data
            var deliverableData = GetDeliverableData(clientDeliverable, programYearId, receiptCycle);
            //Save record
            int? receiptCycleToSave = clientDeliverable.ApiMethod != ClientDataDeliverable.ApiMethods.PanelDeliverable ?
                (int?)receiptCycle : null;
            int newId = SaveDeliverableRecord(programYearId, receiptCycleToSave, clientDeliverableId, userId, deliverableData.Item1, deliverableData.Item2);
            return newId;
        }

        /// <summary>
        /// Downloads the excel deliverable.
        /// </summary>
        /// <param name="programCycleDeliverableId">The program cycle deliverable identifier.</param>
        /// <returns>Model containing file data to be returned to user</returns>
        public IDeliverableFileModel DownloadExcelDeliverable(int programCycleDeliverableId)
        {
            ValidateInt(programCycleDeliverableId, FullName(nameof(SetupService), nameof(DownloadExcelDeliverable)), nameof(programCycleDeliverableId));

            var programCycleDeliverable = UnitOfWork.ProgramCycleDeliverableRepository.GetByID(programCycleDeliverableId);
            IDeliverableFileModel theModel = new DeliverableFileModel(programCycleDeliverable.ProgramYear.ClientProgram.ProgramAbbreviation, programCycleDeliverable.ProgramYear.Year, programCycleDeliverable.ReceiptCycle, programCycleDeliverable.ClientDataDeliverable.Label,
                programCycleDeliverable.QcDataFile);
            //convert the file to xls
            return theModel;
        }

        /// <summary>
        /// Generates deliverable output.
        /// </summary>
        /// <param name="hasDeliverable">Whether there was a deliverable XML.</param>
        /// <param name="hasQcd">Whether the XML has been QC'd</param>
        /// <param name="deliverableXml">The deliverable XML.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <param name="cycle">The cycle.</param>
        /// <param name="clientDeliverableId">The client deliverable identifier.</param>
        /// <returns></returns>
        internal string GenerateDeliverableOutput(bool hasDeliverable, bool hasQcd, string deliverableXml, 
            string fiscalYear, int? cycle, int clientDeliverableId)
        {
            var output = default(string);
            //Get the deliverable metadata
            var clientDeliverable = UnitOfWork.ClientDataDeliverableRepository.GetByID(clientDeliverableId);
            switch (clientDeliverable.ApiMethod)
            {
                case ClientDataDeliverable.ApiMethods.BudgetDeliverable:
                case ClientDataDeliverable.ApiMethods.ScoringDeliverable:
                    if (hasDeliverable && hasQcd)
                        output = deliverableXml;
                    else if (hasDeliverable)
                        output = @"Deliverable has not been QC'd yet";
                    else
                        output = @"No Deliverable for Fiscal Year, Program, Receipt Cycle, and Deliverable Type requested";
                    break;
                case ClientDataDeliverable.ApiMethods.PanelDeliverable:
                    if (hasDeliverable && hasQcd)
                        output = deliverableXml;
                    else
                        output = GetNoPanelXml();
                    break;
                case ClientDataDeliverable.ApiMethods.CriteriaDeliverable:
                    if (hasDeliverable && hasQcd)
                        output = deliverableXml;
                    else
                        output = GetNoCriteriaXml(fiscalYear, (int)cycle);
                    break;
                default:
                    throw new InvalidOperationException("The configured repository method could not be found");
            }
            return output;
        }
        /// <summary>
        /// Get Panel XML when there are no matching panels.
        /// </summary>
        /// <returns></returns>
        public string GetNoPanelXml()
        {
            var panelContainer = new PanelDeliverableContainer();
            var packagedData = XMLServices.SerializeWithUTF8AndEmptyNamespace(panelContainer);
            return packagedData;
        }
        /// <summary>
        /// Get Criteria XML when there are no matching criteria.
        /// </summary>
        /// <param name="fiscalYear">Fiscal year.</param>
        /// <param name="receiptCycle">Receipt cycle.</param>
        /// <returns></returns>
        public string GetNoCriteriaXml(string fiscalYear, int receiptCycle)
        {
            var criteriaContainer = new CriteriaDeliverableContainer(fiscalYear, receiptCycle);
            var packagedData = XMLServices.SerializeWithUTF8AndEmptyNamespace(criteriaContainer);
            return packagedData;
        }
        /// <summary>
        /// Gets the deliverable data.
        /// </summary>
        /// <param name="clientDeliverable">The client deliverable.</param>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <returns>Tuple with XML formatted data and excel QC file</returns>
        /// <exception cref="InvalidOperationException">The configured repository method could not be found</exception>
        internal Tuple<string, byte[]> GetDeliverableData(ClientDataDeliverable clientDeliverable, int programYearId, int receiptCycle)
        {
            string packagedData = string.Empty;
            byte[] qcFile = null;
            switch (clientDeliverable.ApiMethod)
            {
                case ClientDataDeliverable.ApiMethods.BudgetDeliverable:
                    var budgetData = UnitOfWork.DeliverableRepository.GetBudgetDeliverable(programYearId, receiptCycle);
                    var budgetContainer = new BudgetDeliverableContainer(budgetData);
                    packagedData = XMLServices.SerializeWithUTF8AndEmptyNamespace(budgetContainer);
                    qcFile = ExcelServices.CreateExcel(budgetContainer.Rows.ToDataTable());
                    break;
                case ClientDataDeliverable.ApiMethods.ScoringDeliverable:
                    var scoreData = UnitOfWork.DeliverableRepository.GetScoreDeliverable(programYearId, receiptCycle);
                    var scoringContainer = new ScoringDeliverableContainer(scoreData);
                    packagedData = XMLServices.SerializeWithUTF8AndEmptyNamespace(scoringContainer);
                    qcFile = ExcelServices.CreateExcel(scoringContainer.Rows.ToDataTable());
                    break;
                case ClientDataDeliverable.ApiMethods.PanelDeliverable:
                    var panelData = UnitOfWork.DeliverableRepository.GetPanelDeliverable(programYearId);
                    var panelContainer = new  PanelDeliverableContainer(panelData);
                    packagedData = XMLServices.SerializeWithUTF8AndEmptyNamespace(panelContainer);
                    qcFile = ExcelServices.CreateExcel(panelContainer.Rows.ToDataTable());
                    break;
                case ClientDataDeliverable.ApiMethods.CriteriaDeliverable:
                    var criteriaData = UnitOfWork.DeliverableRepository.GetCriteriaDeliverable(programYearId, receiptCycle);
                    var criteriaContainer = new CriteriaDeliverableContainer(criteriaData);
                    packagedData = XMLServices.SerializeWithUTF8AndEmptyNamespace(criteriaContainer);
                    qcFile = ExcelServices.CreateExcel(criteriaContainer.MechanismsHolder.Mechanisms
                        .Select(x => x.CriteriaHolder).SelectMany(y => y.CriterionList).ToDataTable());
                    break;
                default:
                    throw new InvalidOperationException("The configured repository method could not be found");
            }
            return new Tuple<string, byte[]>(packagedData, qcFile);
        }

        /// <summary>
        /// Saves the deliverable record to database.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <param name="clientDeliverableId">The client deliverable identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="deliverableFile">The deliverable file.</param>
        /// <param name="qcFile">The qc file.</param>
        /// <returns>Identity value of new/modified record</returns>
        internal int SaveDeliverableRecord(int programYearId, int? receiptCycle, int clientDeliverableId, int userId, string deliverableFile, byte[] qcFile)
        {
            //check if the current record exists
            var currentRecord = UnitOfWork.ProgramCycleDeliverableRepository.Get(x => x.ProgramYearId == programYearId && x.ReceiptCycle == receiptCycle && x.ClientDataDeliverableId == clientDeliverableId).FirstOrDefault();
            ProgramCycleDeliverable newRecord = null;
            //add/update entity
            if (currentRecord != null)
                newRecord = UpdateRegeneratedDeliverableRecord(currentRecord, userId, deliverableFile, qcFile);
            else
                newRecord = AddDeliverableRecord(programYearId, receiptCycle, clientDeliverableId, userId, deliverableFile, qcFile);
            //save in DB
            UnitOfWork.Save();
            return newRecord.ProgramCycleDeliverableId;
        }

        /// <summary>
        /// Adds a new deliverable record.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="receiptCycle">The receipt cycle.</param>
        /// <param name="clientDeliverableId">The client deliverable identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="deliverableFile">The deliverable file.</param>
        internal ProgramCycleDeliverable AddDeliverableRecord(int programYearId, int? receiptCycle, int clientDeliverableId, int userId, string deliverableFile, byte[] qcFile)
        {
            ProgramCycleDeliverable theDeliverable = new ProgramCycleDeliverable();
            theDeliverable.Populate(programYearId, receiptCycle, clientDeliverableId, userId, deliverableFile, qcFile);
            Helper.UpdateCreatedFields(theDeliverable, userId);
            Helper.UpdateModifiedFields(theDeliverable, userId);
            UnitOfWork.ProgramCycleDeliverableRepository.Add(theDeliverable);
            return theDeliverable;
        }

        /// <summary>
        /// Updates the regenerated deliverable record.
        /// </summary>
        /// <param name="theDeliverable">The deliverable.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="deliverableFile">The deliverable file.</param>
        /// <param name="qcFile">The qc file.</param>
        /// <returns></returns>
        internal ProgramCycleDeliverable UpdateRegeneratedDeliverableRecord(ProgramCycleDeliverable theDeliverable, int userId, string deliverableFile, byte[] qcFile)
        {
            theDeliverable.RegenerateDeliverable(userId, deliverableFile, qcFile);
            Helper.UpdateModifiedFields(theDeliverable, userId);
            UnitOfWork.ProgramCycleDeliverableRepository.Update(theDeliverable);
            return theDeliverable;
        }
        /// <summary>
        /// Gets the award.
        /// </summary>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <returns></returns>
        public IAwardModel GetAward(int programMechanismId)
        {
            var programMechanism = UnitOfWork.ProgramMechanismRepository.GetByID(programMechanismId);
            var award = new AwardModel
            {
                AwardTypeId = programMechanism.ClientAwardTypeId,
                AwardAbbreviation = programMechanism.ClientAwardType.AwardAbbreviation,
                AwardDescription = programMechanism.ClientAwardType.AwardDescription,
                LegacyAwardTypeId = programMechanism.ClientAwardType.LegacyAwardTypeId,
                ReceiptCycle = programMechanism.ReceiptCycle,
                ProgramAbbreviation = programMechanism.ProgramYear.ClientProgram.ProgramAbbreviation,
                Year = programMechanism.ProgramYear.Year
            };
            return award;
        }
        /// <summary>
        /// Transfers the data.
        /// </summary>
        /// <param name="clientTransferTypeId">The client transfer type identifier.</param>
        /// <param name="programMechanismId">The program mechanism identifier.</param>
        /// <param name="credentialKey">The credential key.</param>
        /// <param name="url">The URL.</param>
        /// <param name="userId">The user identifier.</param>
        public int TransferData(int clientTransferTypeId, int programMechanismId, string credentialKey, string url, int userId)
        {
            var piImporter = new ProposalInfoDocImporter(UnitOfWork, credentialKey, url, clientTransferTypeId, programMechanismId, userId);
            piImporter.StartImport();
            return piImporter.ImportLogId;
        }
        /// <summary>
        /// Gets the import log.
        /// </summary>
        /// <param name="importLogId">The import log identifier.</param>
        /// <returns></returns>
        public IImportLogModel GetImportLog(int importLogId)
        {
            var model = new ImportLogModel();
            var o = UnitOfWork.ImportLogRepository.GetByID(importLogId);
            if (o != null)
            {
                model = new ImportLogModel(o.ImportLogId, o.Timestamp, o.Message,
                    o.ImportLogItems.Where(x => !x.SuccessFlag).ToList()
                    .ConvertAll(x => x.Key + ": " + x.Message), 
                    o.ImportLogItems.Count(x => x.SuccessFlag), o.SuccessFlag);
            }
            return model;
        }
    }
}