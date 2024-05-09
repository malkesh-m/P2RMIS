using Sra.P2rmis.Bll.ModelBuilders.Setup;
using Sra.P2rmis.Bll.Views;
using Sra.P2rmis.WebModels.Setup;
using Sra.P2rmis.Dal;
using Sra.P2rmis.Bll.Rules;
using Sra.P2rmis.Bll.Setup.Blocks;
using Sra.P2rmis.Bll.Setup.Actions;
using Sra.P2rmis.WebModels.Lists;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Sra.P2rmis.Bll.Setup
{
    /// <summary>
    /// Provides services for System Setup.
    /// </summary>
    public partial interface ISetupService
    {
        Container<ISessionSetupMeetingListEntryModel> RetrieveSessionSetupMeetingList(int clientId);
        Container<ISessionSetupModel> RetrieveSessionSetupGrid(int clientMeetingId);
        Container<IAddPanelModel> RetrieveAddSessionPanelModal(int meetingSessionId);
        Container<IAddSessionModalModel> RetrieveAddSessionModal(int clientMeetingId);
        Container<IModifySessionModalModel> RetrieveModifySessionModal(int meetingSessionId);
        Container<IUpdatePanelModel> RetrieveUpdateSessionPanelModal(int sessionPanelId);
        ServiceState AddSession(int clientId, int clientMeetingId, string sessionAbbreviation, string sessionDescription, List<IGenericListEntry<Nullable<int>, IPhaseModel>> phases, DateTime startDate, DateTime endDate, int userId);
        ServiceState ModifySession(int meetingSessionId, int clientMeetingId, string sessionAbbreviation, string sessionDescription, List<IGenericListEntry<Nullable<int>, IPhaseModel>> phases, DateTime startDate, DateTime endDate, int userId);
        ServiceState DeleteSession(int meetingSessionId, int userId);
        ServiceState AddProgramMeeting(int programYearId, int clientMeetingId, int userId);      
        ServiceState AddSessionPanel(int programYearId, int clientMeetingId, int meetingSessionId, string panelAbbreviation, string panelName, int userId);
        ServiceState ModifySessionPanel(int sessionPanelId, int meetingSessionId, string panelAbbreviation, string panelName, int userId);
        ServiceState MoveSessionPanel(int sessionPanelId, int newMeetingSessionId, string panelAbbreviation, string panelName, int userId);
        ServiceState DeleteSessionPanel(int sessionPanelId, int userId);
    }
    /// <summary>
    /// Provides services for System Setup Session setup
    /// </summary>
    public partial class SetupService
    {
        /// <summary>
        /// Retrieves a container of a client's meetings for the session setup view.
        /// </summary>
        /// <param name="clientId">Client entity identifier</param>
        /// <returns>Container of ISessionSetupMeetingListEntryModel model for the given Client id</returns>
        public virtual Container<ISessionSetupMeetingListEntryModel> RetrieveSessionSetupMeetingList(int clientId)
        {
            ValidateInt(clientId, FullName(nameof(SetupService), nameof(RetrieveSessionSetupMeetingList)), nameof(clientId));

            SessionSetupMeetingListEntryModelBuilder builder = new SessionSetupMeetingListEntryModelBuilder(this.UnitOfWork, clientId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Retrieves a container of a client meetings's sessions.
        /// </summary>
        /// <param name="clientMeetingId">ClientMeeting entity identifier</param>
        /// <returns>Container of ISessionSetupModel model for the given clientMeeting id</returns>
        public virtual Container<ISessionSetupModel> RetrieveSessionSetupGrid(int clientMeetingId)
        {
            ValidateInt(clientMeetingId, FullName(nameof(SetupService), nameof(RetrieveSessionSetupGrid)), nameof(clientMeetingId));

            SessionSetupModelBuilder builder = new SessionSetupModelBuilder(this.UnitOfWork, clientMeetingId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Retrieve the data model to populate the Add SessionPanel modal.
        /// </summary>
        /// <param name="meetingSessionId">MeetingSession entity identifier</param>
        /// <returns>Container of IAddPanelModel model for the given MeetingSession entity</returns>
        public virtual Container<IAddPanelModel> RetrieveAddSessionPanelModal(int meetingSessionId)
        {
            ValidateInt(meetingSessionId, FullName(nameof(SetupService), nameof(RetrieveAddSessionPanelModal)), nameof(meetingSessionId));

            AddPanelModalModelBuilder builder = new AddPanelModalModelBuilder(this.UnitOfWork, meetingSessionId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Retrieve the data model to populate the Update SessionPanel modal.
        /// </summary>
        /// <param name="sessionPanelId">SessionPanel (being updated) entity identifier</param>
        /// <returns>Container of IUpdatePanelModel model for the given SessionPanel entity</returns>
        public virtual Container<IUpdatePanelModel> RetrieveUpdateSessionPanelModal(int sessionPanelId)
        {
            ValidateInt(sessionPanelId, FullName(nameof(SetupService), nameof(RetrieveUpdateSessionPanelModal)), nameof(sessionPanelId));

            UpdatePanelModalModelBuilder builder = new UpdatePanelModalModelBuilder(this.UnitOfWork, sessionPanelId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Retrieves a container to populate an Add Session modal.
        /// </summary>
        /// <param name="clientMeetingId">Target ClientMeeting entity identifier for the new MeetingSession</param>
        /// <returns>Container of IAddSessionModalModel model for the given ClientMeeting entity</returns>
        public virtual Container<IAddSessionModalModel> RetrieveAddSessionModal(int clientMeetingId)
        {
            ValidateInt(clientMeetingId, FullName(nameof(SetupService), nameof(RetrieveAddSessionModal)), nameof(clientMeetingId));

            AddSessionModalModelBuilder builder = new AddSessionModalModelBuilder(this.UnitOfWork, clientMeetingId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Retrieves a container to populate an Add Session modal.
        /// </summary>
        /// <param name="clientMeetingId">Target ClientMeeting entity identifier for the MeetingSession</param>
        /// <param name="meetingSessionId">Target MeetingSession entity identifier being modifier</param>
        /// <returns>Container of IModifySessionModalModel model for the given MeetingSession entity</returns>
        public virtual Container<IModifySessionModalModel> RetrieveModifySessionModal(int meetingSessionId)
        {
            string name = FullName(nameof(SetupService), nameof(RetrieveModifySessionModal));
            ValidateInt(meetingSessionId, name, nameof(meetingSessionId));

            ModifySessionModalModelBuilder builder = new ModifySessionModalModelBuilder(this.UnitOfWork, meetingSessionId);
            builder.BuildContainer();
            return builder.Results;
        }
        /// <summary>
        /// Adds the session.
        /// </summary>
        /// <param name="clientId">The client identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="sessionAbbreviation">The session abbreviation.</param>
        /// <param name="sessionDescription">The session description.</param>
        /// <param name="phases">The phases.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public virtual ServiceState AddSession(int clientId, int clientMeetingId, string sessionAbbreviation, string sessionDescription, List<IGenericListEntry<Nullable<int>, IPhaseModel>> phases, DateTime startDate, DateTime endDate, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(AddSession));
            ValidateInteger(clientMeetingId, name, nameof(clientMeetingId));
            ValidateString(sessionAbbreviation, name, nameof(sessionAbbreviation));
            ValidateString(sessionDescription, name, nameof(sessionDescription));
            ValidateInteger(userId, name, nameof(userId));
            //
            // Then we create the Session block &  operation and add it.
            //
            var meeting = RetrieveMeetingSetupModal(clientId, clientMeetingId).Model;
            SessionBlock block = new SessionBlock(clientMeetingId, sessionAbbreviation, sessionDescription, startDate, endDate);
            for (var i = 0; i < phases.Count; i++)
            {
                SessionPhase phase = CreateSessionPhase(phases[i].Index, phases[i].Value);
                block.AddSessionPhase(phase);
            }
             
            block.ConfigureAdd();

            return DoCrud(block, null, CrudAction.Add, 0, true, userId);
         }
        /// <summary>
        /// Creates the session phase.
        /// </summary>
        /// <param name="phaseId">The phase identifier.</param>
        /// <param name="phaseModel">The phase model.</param>
        /// <returns></returns>
        public SessionPhase CreateSessionPhase(int? phaseId, IPhaseModel phaseModel)
        {
            var phase = new SessionPhase();
            if (phaseId != null)
                phase.SessionPhaseId = (int)phaseId;
            phase.StepTypeId = phaseModel.StepTypeId;
            phase.StartDate = phaseModel.StartDate != null ? (DateTime)phaseModel.StartDate : DateTime.MinValue;
            phase.EndDate = phaseModel.EndDate != null ? (DateTime)phaseModel.EndDate : DateTime.MinValue;
            phase.ReopenDate = phaseModel.ReopenDate;
            phase.CloseDate = phaseModel.CloseDate;
            return phase;
        }
        /// <summary>
        /// Modifies the session.
        /// </summary>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="sessionAbbreviation">The session abbreviation.</param>
        /// <param name="sessionDescription">The session description.</param>
        /// <param name="phases">The phases.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public virtual ServiceState ModifySession(int meetingSessionId, int clientMeetingId, string sessionAbbreviation, string sessionDescription, List<IGenericListEntry<Nullable<int>, IPhaseModel>> phases, DateTime startDate, DateTime endDate, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(ModifySession));
            ValidateInteger(meetingSessionId, name, nameof(meetingSessionId));
            ValidateString(sessionAbbreviation, name, nameof(sessionAbbreviation));
            ValidateString(sessionDescription, name, nameof(sessionDescription));
            ValidateInteger(userId, name, nameof(userId));
            //
            // Then we create the Session block &  operation and add it.
            //
            SessionBlock block = new SessionBlock(meetingSessionId, clientMeetingId, sessionAbbreviation, sessionDescription, startDate, endDate);
            for (var i = 0; i < phases.Count; i++)
            {
                SessionPhase phase = CreateSessionPhase(phases[i].Index, phases[i].Value);
                block.AddSessionPhase(phase);
            }
            block.ConfigureModify();

            return DoCrud(block, null, CrudAction.Modify, meetingSessionId, true, userId);
        }
        /// <summary>
        /// Deletes the session.
        /// </summary>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public ServiceState DeleteSession(int meetingSessionId, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(DeleteSession));
            ValidateInteger(meetingSessionId, name, nameof(meetingSessionId));
            ValidateInteger(userId, name, nameof(userId));
            //
            // 1) create the P'Block & populate
            //
            SessionBlock block = new SessionBlock(meetingSessionId);
            block.ConfigureDelete();

            return DoCrud(block, null, CrudAction.Delete, meetingSessionId, true, userId);
        }
        /// <summary>
        /// Adds the program meeting.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public ServiceState AddProgramMeeting(int programYearId, int clientMeetingId, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(AddProgramMeeting));
            ValidateInteger(programYearId, name, nameof(programYearId));
            ValidateInteger(clientMeetingId, name, nameof(clientMeetingId));
            ValidateInteger(userId, name, nameof(userId));
            //
            // Then we create the Session block &  operation and add it.
            //
            ProgramMeetingBlock block = new ProgramMeetingBlock(programYearId, clientMeetingId);
            block.ConfigureAdd();

            return DoCrud(block, null, CrudAction.Add, 0, true, userId);
        }
        /// <summary>
        /// Adds the session panel.
        /// </summary>
        /// <param name="programYearId">The program year identifier.</param>
        /// <param name="clientMeetingId">The client meeting identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="panelAbbreviation">The panel abbreviation.</param>
        /// <param name="panelName">Name of the panel.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public virtual ServiceState AddSessionPanel(int programYearId, int clientMeetingId, int meetingSessionId, string panelAbbreviation, string panelName, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(AddSessionPanel));
            ValidateInteger(programYearId, name, nameof(programYearId));
            ValidateInteger(clientMeetingId, name, nameof(clientMeetingId));
            ValidateInteger(meetingSessionId, name, nameof(meetingSessionId));
            ValidateString(panelAbbreviation, name, nameof(panelAbbreviation));
            ValidateString(panelName, name, nameof(panelName));
            ValidateInteger(userId, name, nameof(userId));
            //
            // Then we create the Session block &  operation and add it.
            //
            var session = RetrieveModifySessionModal(meetingSessionId).Model;
            var preMeetingStepTypeIds = session.PreMeetingPhases.Where(x => x.Index != null).Select(x => x.Value.StepTypeId).ToList();
            var meetingStepTypeIds = session.MeetingPhases.Select(x => x.Value.StepTypeId).ToList();
            var preMeetingWorkflowId = UnitOfWork.WorkflowRepository.GetWorkflow(preMeetingStepTypeIds, session.ClientId).WorkflowId;
            var meetingWorkflowId = UnitOfWork.WorkflowRepository.GetWorkflow(meetingStepTypeIds, session.ClientId)?.WorkflowId;
            PanelBlock block = new PanelBlock(meetingSessionId, panelAbbreviation, panelName);
            block.SetProgramYearId(programYearId);
            block.SetDates(session.SessionStart, session.SessionEnd);
            block.SetPanelStages(session.PreMeetingPhases.Where(x => x.Index != null).ToList(), preMeetingWorkflowId, session.MeetingPhases.ToList(), meetingWorkflowId);
            block.ConfigureAdd();

            return DoCrud(block, null, CrudAction.Add, 0, true, userId);
        }
        /// <summary>
        /// Creates the panel stage.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="reviewStageId">The review stage identifier.</param>
        /// <param name="stageOrder">The stage order.</param>
        /// <returns></returns>
        public PanelStage CreatePanelStage(int? sessionPanelId, int reviewStageId, int stageOrder)
        {
            var stage = new PanelStage();
            if (sessionPanelId != null)
                stage.SessionPanelId = (int)sessionPanelId;
            stage.ReviewStageId = reviewStageId;
            stage.StageOrder = stageOrder;
            return stage;
        }
        /// <summary>
        /// Modifies the session panel.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="meetingSessionId">The meeting session identifier.</param>
        /// <param name="panelAbbreviation">The panel abbreviation.</param>
        /// <param name="panelName">Name of the panel.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public virtual ServiceState ModifySessionPanel(int sessionPanelId, int meetingSessionId, string panelAbbreviation, string panelName, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(ModifySession));
            ValidateInteger(sessionPanelId, name, nameof(sessionPanelId));
            ValidateInteger(meetingSessionId, name, nameof(meetingSessionId));
            ValidateString(panelAbbreviation, name, nameof(panelAbbreviation));
            ValidateString(panelName, name, nameof(panelName));
            ValidateInteger(userId, name, nameof(userId));
            //
            // Then we create the Session block &  operation and add it.
            //
            SessionPanel entity = UnitOfWork.SessionPanelRepository.GetByID(sessionPanelId);
            PanelBlock block = new PanelBlock(sessionPanelId, meetingSessionId, panelAbbreviation, panelName);
            block.SetProgramYearId(entity.GetProgramYearId());
            block.ConfigureModify();

            return DoCrud(block, entity, CrudAction.Modify, sessionPanelId, true, userId);
        }
        /// <summary>
        /// Moves the session panel.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="newMeetingSessionId">The new meeting session identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public ServiceState MoveSessionPanel(int sessionPanelId, int newMeetingSessionId, string panelAbbreviation, string panelName, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(MoveSessionPanel));
            ValidateInteger(sessionPanelId, name, nameof(sessionPanelId));
            ValidateInteger(newMeetingSessionId, name, nameof(newMeetingSessionId));
            ValidateString(panelAbbreviation, name, nameof(panelAbbreviation));
            ValidateString(panelName, name, nameof(panelName));
            ValidateInteger(userId, name, nameof(userId));
            //
            // Then we create the Session block &  operation and add it.
            //
            var session = RetrieveModifySessionModal(newMeetingSessionId).Model;
            var preMeetingStepTypeIds = session.PreMeetingPhases.Where(x => x.Index != null).Select(x => x.Value.StepTypeId).ToList();
            var meetingStepTypeIds = session.MeetingPhases.Select(x => x.Value.StepTypeId).ToList();
            var preMeetingWorkflowId = UnitOfWork.WorkflowRepository.GetWorkflow(preMeetingStepTypeIds, session.ClientId).WorkflowId;
            var meetingWorkflowId = UnitOfWork.WorkflowRepository.GetWorkflow(meetingStepTypeIds, session.ClientId).WorkflowId;
            
            PanelBlock block = new PanelBlock(sessionPanelId, newMeetingSessionId, panelAbbreviation, panelName);
            block.SetDates(session.SessionStart, session.SessionEnd);
            block.SetPanelStages(session.PreMeetingPhases.Where(x => x.Index != null).ToList(), preMeetingWorkflowId, session.MeetingPhases.ToList(), meetingWorkflowId);
            block.ConfigureModify();

            return DoCrud(block, null, CrudAction.Modify, sessionPanelId, true, userId);
        }

        /// <summary>
        /// Deletes the session panel.
        /// </summary>
        /// <param name="sessionPanelId">The session panel identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public ServiceState DeleteSessionPanel(int sessionPanelId, int userId)
        {
            string name = FullName(nameof(SetupService), nameof(DeleteSessionPanel));
            ValidateInteger(sessionPanelId, name, nameof(sessionPanelId));
            ValidateInteger(userId, name, nameof(userId));
            //
            // 1) create the P'Block & populate
            //
            PanelBlock block = new PanelBlock(sessionPanelId);
            block.ConfigureDelete();

            return DoCrud(block, null, CrudAction.Delete, sessionPanelId, true, userId);
        }
        /// <summary>
        /// The CRUD operation for the MeetingSession entity
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="operation">The operation.</param>
        /// <param name="entityId">The entity identifier.</param>
        /// <param name="doUpdate">if set to <c>true</c> [do update].</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        internal virtual ServiceState DoCrud(SessionBlock block, MeetingSession entity, CrudAction operation, int entityId, bool doUpdate, int userId)
        {
            //
            // 1) Get the rules we need to apply
            //
            RuleEngine<MeetingSession> rules = RuleEngineConstructors.CreateMeetingSessionEngine(UnitOfWork, entity, operation, block);
            //
            // 2) Create the action & execute it
            //
            MeetingSessionServiceAction action = new MeetingSessionServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.MeetingSessionRepository, doUpdate, entityId, userId);
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
        /// The CRUD operation for the SessionPanel entity
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="operation">The operation.</param>
        /// <param name="entityId">The entity identifier.</param>
        /// <param name="doUpdate">if set to <c>true</c> [do update].</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        internal virtual ServiceState DoCrud(PanelBlock block, SessionPanel entity, CrudAction operation, int entityId, bool doUpdate, int userId)
        {
            //
            // 1) Get the rules we need to apply
            //
            RuleEngine<SessionPanel> rules = RuleEngineConstructors.CreateSessionPanelEngine(UnitOfWork, entity, operation, block);
            //
            // 2) Create the action & execute it
            //
            SessionPanelServiceAction action = new SessionPanelServiceAction();
            action.InitializeAction(this.UnitOfWork, this.UnitOfWork.SessionPanelRepository, doUpdate, entityId, userId);
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
    }
}
