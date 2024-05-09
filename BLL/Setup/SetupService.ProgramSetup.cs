using Sra.P2rmis.Bll.ModelBuilders.Setup;
using Sra.P2rmis.Bll.Rules;
using Sra.P2rmis.Bll.Setup.Actions;
using Sra.P2rmis.Bll.Setup.Blocks;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.Dal;
using Sra.P2rmis.WebModels.Lists;
using Sra.P2rmis.WebModels.Setup;
using System;
using System.Linq;
using System.Collections.Generic;
using Sra.P2rmis.WebModels.PanelManagement;

namespace Sra.P2rmis.Bll.Setup
{
    #region Services
    /// <summary>
    /// Provides services for System Setup.
    /// </summary>
    public partial interface ISetupService
    {
        IEnumerable<IProgramSetupModel> GetProgramsByFiscalYear(int clientId, string fiscalYear);
        Container<IProgramSetupModel> RetrieveProgramSetupWizard(int clientId, int programYearId);
        ServiceState AddProgram(int clientId, Nullable<int> clientProgramId, string programDescription, string programAbbreviation, string programYear, bool activate, int userId);
        ServiceState ModifyProgram(int clientProgramId, int programYearId, bool activate, int userId);
        ServiceState DeleteProgram(int clientProgramId, int programYearId, int userId);
        ServiceState CheckForLastProgramYear(int clientProgramId, int programYearId, int userId);

        Container<IGenericListEntry<int, string>> RetrieveSessionAssignProgramFiscalYearList(int clientId);
        Container<IGenericListEntry<int, string>> RetrieveSessionAssignProgramList(int clientId, string year);
        /// <summary>
        /// Gets the program year.
        /// </summary>
        /// <param name="programAbbreviation">The program abbreviation.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <returns></returns>
        ProgramYearModel GetProgramYear(string programAbbreviation, string fiscalYear);
    }
    /// <summary>
    /// Provides services for System Setup, Program Setup.
    /// </summary>
    public partial class SetupService
    {
        /// <summary>
        /// Gets programs by fiscal year
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <returns></returns>
        public IEnumerable<IProgramSetupModel> GetProgramsByFiscalYear(int clientId, string fiscalYear)
        {
            string name = FullName(nameof(SetupService), nameof(GetProgramsByFiscalYear));
            ValidateInt(clientId, name, nameof(clientId));

            var programs = UnitOfWork.ProgramYearRepository.Select().Where(x => 
                (string.IsNullOrEmpty(fiscalYear) || x.Year == fiscalYear)
                && x.ClientProgram.ClientId == clientId)
                .Select(x => new ProgramSetupModel
                {
                    // information from Client entity
                    ClientAbrv = x.ClientProgram.Client.ClientAbrv,
                    ClientDesc = x.ClientProgram.Client.ClientDesc,
                    CycleCount = x.ProgramMechanism.Select(y => y.ReceiptCycle).Distinct().Count(),
                    ClientMeetingCount = x.ProgramMeetings.Count(),
                    ClientId = x.ClientProgram.ClientId,
                    // information from ClientProgram entity
                    ProgramDescription = x.ClientProgram.ProgramDescription,
                    ProgramAbbreviation = x.ClientProgram.ProgramAbbreviation,
                    ClientProgramId = x.ClientProgramId,
                    // information from ProgramYear entity
                    Active = !x.DateClosed.HasValue,
                    Year = x.Year,
                    ProgramMechanismCount = x.ProgramMechanism.Count(),
                    ProgramYearId = x.ProgramYearId,
                    IsApplicationsReleased = x.ProgramPanels
                        .Select(y => y.SessionPanel)
                        .SelectMany(y => y.ProgramPanels)
                        .Select(y => y.SessionPanel)
                        .SelectMany(y => y.PanelApplications)
                        .SelectMany(y => y.ApplicationStages).Where(y => y.ReviewStageId == ReviewStage.Indexes.Asynchronous)
                        .Any(y => y.AssignmentVisibilityFlag)
                    })
                //
                //  And now we order the results as required
                //
                .OrderBy(x => x.ClientAbrv)
                .ThenByDescending(x => x.Year)
                .ThenBy(x => x.ProgramAbbreviation).ToList();
            return programs;
        }
        /// <summary>
        /// Retrieves a container to populate the ProgramSetup wizard.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="programYearId">ProgramYear entity identifier</param>
        /// <returns>Container of IProgramSetupModel models</returns>
        public virtual Container<IProgramSetupModel> RetrieveProgramSetupWizard(int clientId, int programYearId)
        {
            string name = FullName(nameof(SetupService), nameof(RetrieveProgramSetupWizard));
            ValidateInt(clientId, name, nameof(clientId));
            ValidateInt(programYearId, name, nameof(programYearId));

            ProgramSetupWizardModelBuilder builder = new ProgramSetupWizardModelBuilder(this.UnitOfWork, clientId, programYearId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Retrieves a container to populate an Add Session modal Program list when no programs 
        /// have been assigned.
        /// </summary>
        /// <param name="clientId">ClientId entity identifier</param>
        /// <param name="year">Fiscal year (YYYY) selected</param>
        /// <returns>Container of IGenericListEntry models to populate the list</returns>
        public virtual Container<IGenericListEntry<int, string>> RetrieveSessionAssignProgramList(int clientId, string year)
        {
            string name = FullName(nameof(SetupService), nameof(RetrieveSessionAssignProgramList));
            ValidateInt(clientId, name, nameof(clientId));
            ValidateString(year, name, nameof(year));

            SessionAssignProgramProgramsBuilder builder = new SessionAssignProgramProgramsBuilder(this.UnitOfWork, clientId, year);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Retrieves a container to populate an Add Session modal Fiscal Year list when no programs 
        /// have been assigned.
        /// </summary>
        /// <param name="clientId">ClientId entity identifier</param>
        /// <returns>Container of IGenericListEntry models to populate the list</returns>
        public virtual Container<IGenericListEntry<int, string>> RetrieveSessionAssignProgramFiscalYearList(int clientId)
        {
            ValidateInt(clientId, FullName(nameof(SetupService), nameof(RetrieveSessionAssignProgramFiscalYearList)), nameof(clientId));

            SessionAssignProgramFiscalYearBuilder builder = new SessionAssignProgramFiscalYearBuilder(this.UnitOfWork, clientId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Service method to add a new ProgramYear to an existing ClientProgram
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="clientProgramId">ClientProgram entity identifier to add the ProgramYear to</param>
        /// <param name="programDescription">Program description for newly created programs</param>
        /// <param name="programAbbreviation">Program abbreviation for newly created programs</param>
        /// <param name="programYear">ProgramYear</param>
        /// <param name="activate">Indicates the program year is enabled</param>
        /// <param name="userId">User (making the change) entity identifier</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        public ServiceState AddProgram(int clientId, Nullable<int> clientProgramId, string programDescription, string programAbbreviation, string programYear, bool activate, int userId)
        {
            //
            // There are two cases we need to consider:
            //      1) both a ClientProgram & ProgramYear entity are created
            //      2) only a ProgramYear is created.
            // we can detect this by the ClientProgramId.  It is only supplied when both are created.
            //
            ServiceState result = (!clientProgramId.HasValue)?
                                  AddProgramAndProgramYear(clientId, programDescription, programAbbreviation, programYear, activate, userId):
                                  AddProgramYear(clientProgramId.Value, programYear, activate, userId);
            return result;
        }
        /// <summary>
        /// Adds a new ClientProgram and a ProgramYear as specified by the selections
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <param name="programDescription">Program description for newly created programs</param>
        /// <param name="programAbbreviation">Program abbreviation for newly created programs</param>
        /// <param name="programYear">ProgramYear</param>
        /// <param name="activate">Indicates the program year is enabled</param>
        /// <param name="userId">User (making the change) entity identifier</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        internal virtual ServiceState AddProgramAndProgramYear(int clientId, string programDescription, string programAbbreviation, string programYear, bool activate, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(AddProgramAndProgramYear));
            ValidateInteger(clientId, name, nameof(clientId));
            ValidateString(programDescription, name, nameof(programDescription));
            ValidateString(programAbbreviation, name, nameof(programAbbreviation));
            ValidateString(programYear, name, nameof(programYear));
            ValidateInteger(userId, name, nameof(userId));
            //
            // 1) create the P'Block & populate
            //
            ProgramSetupBlock block = new ProgramSetupBlock(userId, clientId, programDescription, programAbbreviation, programYear, activate);
            block.ConfigureAdd();
            //
            // 2) Get the rules we need to apply
            //
            RuleEngine<ProgramYear> rules = RuleEngineConstructors.CreateProgramYearEngine(UnitOfWork, null, CrudAction.Add, block);
            //
            // 3) Create the action & execute it
            //
            ProgramYearServiceAction action = new ProgramYearServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.ProgramYearRepository, ServiceAction<ProgramYear>.DoUpdate, 0, userId);
            action.Populate(block);
            action.Rule(rules);
            action.Execute();
            action.PostProcess();
            //
            // We pull the status off of the rules engine even though the action has ran after.  If
            // there was a failure an exception would have been thrown ignoring us all.
            //
            return new ServiceState(!rules.IsBroken, rules.Messages, action.EntityInfo);
        }
        /// <summary>
        /// Service method to add a new FiscalYear to a ClientProgram
        /// </summary>
        /// <param name="clientProgramId">ClientProgram entity identifier to add the ProgramYear to</param>
        /// <param name="programYear">ProgramYear</param>
        /// <param name="activate">Indicates the program year is enabled</param>
        /// <param name="userId">User (making the change) entity identifier</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        internal virtual ServiceState AddProgramYear(int clientProgramId, string programYear, bool activate, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(AddProgramYear));
            ValidateInteger(clientProgramId, name, nameof(clientProgramId));
            ValidateString(programYear, name, nameof(programYear));
            ValidateInteger(userId, name, nameof(userId));
            //
            // 1) create the P'Block & populate
            //
            ProgramSetupBlock block = new ProgramSetupBlock(userId, clientProgramId, programYear, activate);
            block.ConfigureAdd();
            //
            // 2) Get the rules we need to apply
            //
            RuleEngine<ProgramYear> rules = RuleEngineConstructors.CreateProgramYearEngine(UnitOfWork, null, CrudAction.Add, block);
            //
            // 3) Create the action & execute it
            //
            ProgramYearServiceAction action = new ProgramYearServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.ProgramYearRepository, ServiceAction<ProgramYear>.DoUpdate, 0, userId);
            action.Populate(block); 
            action.Rule(rules);
            action.Execute();
            action.PostProcess();
            //
            // We pull the status off of the rules engine even though the action has ran after.  If
            // there was a failure an exception would have been thrown ignoring us all.
            //
            return new ServiceState(!rules.IsBroken, rules.Messages, action.EntityInfo);
        }
        /// <summary>
        /// Modify a ClientProgram's ProgramYear entity.
        /// </summary>
        /// <param name="clientProgramId">ClientProgram entity identifier containing the ProgramYear</param>
        /// <param name="programYearId">ProgramYear (to modify) entity identifier</param>
        /// <param name="activate">Indicates the program year is enabled</param>
        /// <param name="userId">User (making the change) entity identifier</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        public ServiceState ModifyProgram(int clientProgramId, int programYearId, bool activate, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(DeleteProgram));
            ValidateInteger(clientProgramId, name, nameof(clientProgramId));
            ValidateInteger(programYearId, name, nameof(programYearId));
            ValidateInteger(userId, name, nameof(userId));
            //
            // 1) create the P'Block & populate
            //
            ProgramSetupBlock block = new ProgramSetupBlock(userId, clientProgramId, programYearId, null, activate);
            block.ConfigureModify();
            //
            // 2) Get the rules we need to apply
            //
            ProgramYear entity = UnitOfWork.ProgramYearRepository.GetByID(programYearId);
            RuleEngine<ProgramYear> rules = RuleEngineConstructors.CreateProgramYearEngine(UnitOfWork, entity, CrudAction.Modify, block);
            //
            // 3) Create the action & execute it
            //
            ProgramYearServiceAction action = new ProgramYearServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.ProgramYearRepository, ServiceAction<ProgramYear>.DoUpdate, programYearId, userId);
            action.Populate(block);
            action.Rule(rules);
            action.Execute();
            //
            // We pull the status off of the rules engine even though the action has ran after.  If
            // there was a failure an exception would have been thrown ignoring us all.
            //
            return new ServiceState(!rules.IsBroken, rules.Messages);
        }
        /// <summary>
        /// Delete a ClientProgram's ProgramYear entity.
        /// </summary>
        /// <param name="clientProgramId">ClientProgram entity identifier containing the ProgramYear</param>
        /// <param name="programYearId">ProgramYear (to modify) entity identifier</param>
        /// <param name="userId">User (making the change) entity identifier</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        public ServiceState DeleteProgram(int clientProgramId, int programYearId, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(DeleteProgram));
            ValidateInteger(clientProgramId, name, nameof(clientProgramId));
            ValidateInteger(programYearId, name, nameof(programYearId));
            ValidateInteger(userId, name, nameof(userId));
            //
            // 1) create the P'Block & populate
            //
            ProgramSetupBlock block = new ProgramSetupBlock(userId, clientProgramId, programYearId);
            block.ConfigureDelete();
            //
            // 2) Get the rules we need to apply
            //
            ProgramYear entity = UnitOfWork.ProgramYearRepository.GetByID(programYearId);
            RuleEngine<ProgramYear> rules = RuleEngineConstructors.CreateProgramYearEngine(UnitOfWork, entity, CrudAction.Delete, block);
            //
            // 3) Create the action & execute it
            //
            ProgramYearServiceAction action = new ProgramYearServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.ProgramYearRepository, ServiceAction<ProgramYear>.DoUpdate, programYearId, userId);
            action.Populate(block);
            action.Rule(rules);
            action.Execute();
            //
            // We pull the status off of the rules engine even though the action has ran after.  If
            // there was a failure an exception would have been thrown ignoring us all.
            //
            return new ServiceState(!rules.IsBroken, rules.Messages);
        }
        /// <summary>
        /// Delete a ClientProgram's ProgramYear entity.
        /// </summary>
        /// <param name="clientProgramId">ClientProgram entity identifier containing the ProgramYear</param>
        /// <param name="programYearId">ProgramYear (to modify) entity identifier</param>
        /// <param name="userId">User (making the change) entity identifier</param>
        /// <returns>ServiceState object containing status & error messages</returns>
        public ServiceState CheckForLastProgramYear(int clientProgramId, int programYearId, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(DeleteProgram));
            ValidateInteger(clientProgramId, name, nameof(clientProgramId));
            ValidateInteger(programYearId, name, nameof(programYearId));
            ValidateInteger(userId, name, nameof(userId));
            //
            // 1) create the P'Block & populate
            //
            ProgramSetupBlock block = new ProgramSetupBlock(userId, clientProgramId, programYearId);
            //block.ConfigureDelete();
            //
            // 2) Get the rules we need to apply
            //
            ProgramYear entity = UnitOfWork.ProgramYearRepository.GetByID(programYearId);
            RuleEngine<ProgramYear> rules = RuleEngineConstructors.CheckForLastProgramYear(UnitOfWork, entity, CrudAction.Delete, block);
            //
            // 3) Create the action & execute it
            //
            ProgramYearServiceAction action = new ProgramYearServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.ProgramYearRepository, ServiceAction<ProgramYear>.DoNotUpdate, programYearId, userId);
            action.Populate(block);
            action.Rule(rules);
            action.Execute();
            //
            // We pull the status off of the rules engine even though the action has ran after.  If
            // there was a failure an exception would have been thrown ignoring us all.
            //
            return new ServiceState(!rules.IsBroken, rules.Messages);
        }
        /// <summary>
        /// Gets the program year.
        /// </summary>
        /// <param name="programAbbreviation">The program abbreviation.</param>
        /// <param name="fiscalYear">The fiscal year.</param>
        /// <returns></returns>
        public ProgramYearModel GetProgramYear(string programAbbreviation, string fiscalYear)
        {            
            var o = UnitOfWork.ProgramYearRepository.Get(x => x.ClientProgram.ProgramAbbreviation == programAbbreviation &&
                x.Year == fiscalYear).FirstOrDefault();
            var model = o != null ? new ProgramYearModel()
                    {
                        ProgramAbbreviation = programAbbreviation,
                        ProgramYearId = o.ProgramYearId,
                        FY = fiscalYear,
                        DateClosed = o.DateClosed
                    } : null;
            return model;
        }
    }
    #endregion
}
